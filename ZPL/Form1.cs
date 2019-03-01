using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace ZPL
{
    public partial class Form1 : Form
    {

        private bool boolCapturing = false;
        private Thread CaptureThread = null;

        private TcpListener server;            


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName;
            Label1.Text = "";
            ComboBoxPort.SelectedIndex = 0;
        }

        public void ThreadSafeDelegate(MethodInvoker method)
        {
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private async void ButtonSendZpl_Click(object sender, EventArgs e)
        {
            
             string strHostname = txtHostname.Text;
        string strPort = ComboBoxPort.Text;
        string strSendText = TextBox1.Text;

        if (string.IsNullOrEmpty(strHostname)) {
            MessageBox.Show("Enter a hostname.", "Send ZPL to printer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtHostname.Focus();
            return;
        }

        if (string.IsNullOrEmpty(strPort)) {
            MessageBox.Show("Select a port number.", "Send ZPL to printer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            ComboBoxPort.Focus();
            return;
        }

        
        if (string.IsNullOrEmpty(strSendText)) {
            MessageBox.Show("Enter some ZPL to send.", "Send ZPL to printer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            TextBox1.Focus();
            return;
        }

            int intPort = 9100;
            int.TryParse(ComboBoxPort.Text, out intPort);

            EnableControls(false);
            bool boolStatus = await SendData(strHostname, intPort, strSendText);
            EnableControls(true);
            if (boolStatus) {
            MessageBox.Show("ZPL sent", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else {
            MessageBox.Show("Failed to send ZPL", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
             
        }
        
        
         
    private async Task<bool> SendData(string HostAddress, int intPort, string strPrintData) {

        NetworkStream PrintedStream;  //Stream to transmit the bytes to the Network printer 
        //IPAddress HostIPAddress; //IP Address of the Printer 
        TcpClient PrinterPort = new TcpClient(); //TCPClient to comunicate 


        try {
            if (! string.IsNullOrEmpty(strPrintData)) {

                    //Convert the string into bytes 
                    var DataBytes = System.Text.Encoding.ASCII.GetBytes(strPrintData); //Contains the String converted into bytes 

                    //If Not connected then connect 
                    if (!PrinterPort.Client.Connected) {
                        PrinterPort.Connect(HostAddress, intPort);
                    }

                    PrintedStream = PrinterPort.GetStream();

                    //Send the stream to the printer 
                    if (PrintedStream.CanWrite )
                    {
                        int bytesRemaining = DataBytes.Length;
                        int pos = 0;
                        int bufferMax = 100;

                        while (bytesRemaining > 0)
                        {
                            int bufferSize = bytesRemaining < bufferMax ? bytesRemaining : bufferMax;
                            
                            await PrintedStream.WriteAsync(DataBytes, pos, bufferSize);
                            //PrintedStream.Write(DataBytes, pos, bufferSize);
                            pos+=bufferSize;
                            bytesRemaining -= bufferSize;
                        }
                        
                    }

                //Close the connections 
                PrintedStream.Close();
                PrinterPort.Close();
            }
            return true;
        } catch (Exception ex) {
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            return false;
        }
    }
             


        private void ButtonSaveZpl_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            
            string strText = TextBox1.Text;
            if (string.IsNullOrEmpty(strText))
            {
                MessageBox.Show("Nothing to save!", "Save ZPL File",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            strText = strText.Replace("\r\n", "\n");

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK ) {
                using (StreamWriter outFile = new StreamWriter(SaveFileDialog1.FileName)) {
                    outFile.Write(strText);
                    outFile.Close();
                }
            }
             
        }

        private void ButtonLoadZpl_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void LoadFile()
        {
            
             if (OpenFileDialog1.ShowDialog() == DialogResult.OK) {
            string strFilename = OpenFileDialog1.FileName;
            StreamReader objReader = new StreamReader(strFilename);
            
            string strLine = "";
            string strNewText = "";
            do {
                strLine = objReader.ReadLine();
                if (!string.IsNullOrEmpty(strLine)) {
                    strNewText += strLine + "\r\n";
                }
            } while (strLine != null);
            objReader.Close();
            TextBox1.Text = strNewText;
            }
             
        }

        private void EnableControls(bool enable)
        {
            ButtonLoadZpl.Enabled = enable;
            ButtonCaptureZpl.Enabled = boolCapturing ? true : enable;
            ButtonSaveZpl.Enabled = enable;
            ButtonSendZpl.Enabled = enable;
            TextBox1.Enabled = enable;
            txtHostname.Enabled = enable;
            ComboBoxPort.Enabled = enable;

        }

        private void ButtonCaptureZpl_Click(object sender, EventArgs e)
        {
            

       if (boolCapturing) {
           boolCapturing = false;
           ButtonCaptureZpl.Text = "Capture ZPL";
           server.Stop();
           EnableControls(true);
                Label1.Text = "";
                return;
       }
       Label1.Text = "";
       boolCapturing = true;
       Timer1.Start();

        int port = 9100;
            int.TryParse(ComboBoxPort.Text, out port);

            //CaptureThread = new Thread(new ThreadStart(CaptureZpl));
            CaptureThread = new Thread(new ParameterizedThreadStart(CaptureZpl));

       CaptureThread.Start(port);

       TextBox1.Text = "";
       EnableControls(false);
       
       Label1.Text = "Listening on port " + ComboBoxPort.Text + "...";
            
        }

        private void CaptureZpl(object port)
        {

            server = null;
            try
            {


                //string hostName = Dns.GetHostName();
                //IPAddress serverIP = Dns.Resolve(hostName).AddressList[0];

                server = new TcpListener(IPAddress.Any, (int)port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                var bytes = new byte[1024];
                string data;

                // Enter the listening loop.
                while (CaptureThread.ThreadState == ThreadState.Running)
                {


                    Console.WriteLine("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    i = stream.Read(bytes, 0, bytes.Length);
                    while (i != 0)
                    {

                        // Translate data bytes to a ASCII string.
                        string newData = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.Write(newData);
                        data += newData;
                        



                        //(string.Format("<= RX ({0}) [ {1}]", inData.Length, ByteArrayToHexString(inData)) + Environment.NewLine); });

                        i = stream.Read(bytes, 0, bytes.Length);

                        if (!boolCapturing)
                        {
                            client.Close();
                            server.Stop();
                        }

                    }

                    ThreadSafeDelegate(delegate {
                        TextBox1.Text += data;
                        TextBox1.ScrollToCaret();

                    });


                    // Shutdown and end connection
                    client.Close();
                }

                server.Stop();


            } catch (SocketException e) {
                Console.WriteLine("SocketException: {0}", e.Message);
         } finally {
                Console.WriteLine("finally...");
         }
              
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            if (boolCapturing ) {
                ButtonCaptureZpl.Text = "Stop Capture";
            } else {
                ButtonCaptureZpl.Text = "Capture ZPL";
                if (CaptureThread != null) {
                    if (CaptureThread.IsAlive) { server.Stop(); }
                }

                Timer1.Stop();
            }
             
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (CaptureThread != null) {
                if (CaptureThread.IsAlive ) {server.Stop();}
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }
    }
}
