Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Threading

Public Class Form1
    Public boolCapturing As Boolean = False
    Private CaptureThread As Thread = Nothing
    Delegate Sub SetTextCallback(ByVal [text] As String)

    Private server As TcpListener

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LoadFile()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SaveFile()
    End Sub

    Private Sub LoadFile()
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim strFilename As String
            strFilename = OpenFileDialog1.FileName
            Dim objReader As New StreamReader(strFilename)
            'TextBox1.Text = objReader.ReadToEnd
            Dim strLine As String = ""
            Dim strNewText As String = ""
            Do
                strLine = objReader.ReadLine
                If Not strLine Is Nothing Then
                    strNewText = strNewText & strLine & vbCrLf
                End If
            Loop Until strLine Is Nothing
            objReader.Close()
            TextBox1.Text = strNewText
        End If
    End Sub

    Private Sub SaveFile()
        'Dim objWriter As StreamWriter
        Dim strText As String = TextBox1.Text
        If strText = "" Then
            MsgBox("Nothing to save!", MsgBoxStyle.Exclamation, "Save ZPL File")
            Exit Sub
        End If

        strText = strText.Replace(vbCrLf, vbLf)

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Using outFile As New StreamWriter(SaveFileDialog1.FileName)
                outFile.Write(strText)
                outFile.Close()
            End Using
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If boolCapturing = True Then
            boolCapturing = False
            Button3.Text = "Capture ZPL"
            server.Stop()
            Button1.Enabled = True
            Button2.Enabled = True
            Button4.Enabled = True
            TextBox1.Enabled = True
            txtHostname.Enabled = True
            ComboBoxPort.Enabled = True
            Label1.Text = ""
            Exit Sub
        End If

        boolCapturing = True
        Timer1.Start()

        CaptureThread = New Thread( _
    New ThreadStart(AddressOf CaptureZPL))
        CaptureThread.Start()

        TextBox1.Text = ""

        Button1.Enabled = False
        Button2.Enabled = False
        Button4.Enabled = False
        ComboBoxPort.Enabled = False
        TextBox1.Enabled = False
        txtHostname.Enabled = False
        Label1.Text = "Listening on port " & ComboBoxPort.Text & "..."
    End Sub


    Private Sub CaptureZPL()


        server = Nothing
        Try
            ' Set the TcpListener on port 9100
            'Dim port As Int32 = 9100
            Dim port As Int32 = ComboBoxPort.Text

            'Dim hostName As String = Dns.GetHostName
            'Dim serverIP As IPAddress = Dns.Resolve(hostName).AddressList(0)

            server = New TcpListener(IPAddress.Any, port)

            ' Start listening for client requests.
            server.Start()

            ' Buffer for reading data
            Dim bytes(1024) As Byte
            Dim data As String = Nothing

            ' Enter the listening loop.
            Do


                Debug.WriteLine("Waiting for a connection... ")

                ' Perform a blocking call to accept requests.
                ' You could also user server.AcceptSocket() here.
                Dim client As TcpClient = server.AcceptTcpClient
                Debug.WriteLine("Connected!")

                data = Nothing

                ' Get a stream object for reading and writing
                Dim stream As NetworkStream = client.GetStream

                Dim i As Int32

                ' Loop to receive all the data sent by the client.
                i = stream.Read(bytes, 0, bytes.Length)
                While (i <> 0)

                    ' Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i)
                    Debug.Write(data)

                    If TextBox1.InvokeRequired Then
                        ' It's on a different thread, so use Invoke.
                        Dim f As New SetTextCallback(AddressOf SetText)
                        Dim NewText As String = data
                        Me.Invoke(f, New Object() {[NewText]})
                    End If

                    i = stream.Read(bytes, 0, bytes.Length)

                    If boolCapturing = False Then
                        client.Close()
                        server.Stop()
                    End If

                End While


                ' Shutdown and end connection
                client.Close()
            Loop

            server.Stop()


        Catch e As SocketException
            Debug.WriteLine("SocketException: {0}", e.ToString)
        Finally
            Debug.WriteLine("finally...")
        End Try

    End Sub 'CaptureZPL


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If boolCapturing Then
            Button3.Text = "Stop Capture"
        Else
            Button3.Text = "Capture ZPL"
            If Not IsNothing(CaptureThread) Then
                If CaptureThread.IsAlive Then server.Stop()
            End If

            Timer1.Stop()
        End If
    End Sub

    Private Sub SetText(ByVal strReceivedText As String)
        If InStr(strReceivedText, "^") Then strReceivedText = strReceivedText.Replace("^", vbCrLf & "^")
        If InStr(strReceivedText, "~") Then strReceivedText = strReceivedText.Replace("~", vbCrLf & "~")

        If strReceivedText.StartsWith(vbCrLf) Then strReceivedText = Strings.Right(strReceivedText, Len(strReceivedText) - Len(vbCrLf))
        TextBox1.Text = TextBox1.Text & strReceivedText
        TextBox1.SelectionStart = TextBox1.Text.Length

        TextBox1.ScrollToCaret()
        Debug.WriteLine(strReceivedText)
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(CaptureThread) Then
            If CaptureThread.IsAlive Then server.Stop()
        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = ""
        ComboBoxPort.SelectedIndex = 0
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim strHostname As String = txtHostname.Text
        Dim strPort As String = ComboBoxPort.Text
        Dim strSendText As String = TextBox1.Text

        If strHostname = "" Then
            MsgBox("Enter a hostname.", MsgBoxStyle.Exclamation, "Send ZPL to printer")
            txtHostname.Focus()
            Exit Sub
        End If

        If strPort = "" Then
            MsgBox("Select a port number.", MsgBoxStyle.Exclamation, "Send ZPL to printer")
            ComboBoxPort.Focus()
            Exit Sub
        End If

        If strSendText = "" Then
            MsgBox("Enter some ZPL to send.", MsgBoxStyle.Exclamation, "Send ZPL to printer")
            TextBox1.Focus()
            Exit Sub
        End If

        If StartSending(strSendText) Then
            MsgBox("ZPL sent.", MsgBoxStyle.Information, Application.ProductName)
        Else
            MsgBox("Failed to send ZPL.", MsgBoxStyle.Information, Application.ProductName)
        End If

    End Sub



    Function StartSending(ByVal strText As String) As Boolean

        Dim arrText() As String
        arrText = Split(strText, vbCrLf)

        Dim strHostname As String = txtHostname.Text
        Dim intPort As Integer = CInt(ComboBoxPort.Text)

        If intPort < 0 Then Exit Function
        If strHostname = "" Then Exit Function
        If strText = "" Then Exit Function

        Dim boolSuccess As Boolean = True
        For Each strLine In arrText
            If SendData(strHostname, intPort, strLine & vbLf) = False Then
                boolSuccess = False
                Exit For
            End If
        Next


        StartSending = boolSuccess

    End Function


    Function SendData(ByVal HostAddress As String, ByVal intPort As Integer, ByVal strPrintData As String) As Boolean
        Dim DataBytes() As Byte 'Contains the String converted into bytes 

        Dim PrintedStream As NetworkStream 'Stream to transmit the bytes to the Network printer 
        'Dim HostIPAddress As System.Net.IPAddress 'IP Address of the Printer 
        Dim PrinterPort As New System.Net.Sockets.TcpClient 'TCPClient to comunicate 


        Try
            If strPrintData IsNot Nothing Then

                'Convert the string into bytes 
                DataBytes = System.Text.Encoding.ASCII.GetBytes(strPrintData)

                'If Not connected then connect 
                If Not PrinterPort.Client.Connected Then
                    PrinterPort.Connect(HostAddress, intPort)
                End If

                PrintedStream = PrinterPort.GetStream

                'Send the stream to the printer 
                If PrintedStream.CanWrite Then
                    PrintedStream.Write(DataBytes, 0, DataBytes.Length)
                End If

                'Close the connections 
                PrintedStream.Close()
                PrinterPort.Close()
            End If
            SendData = True
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Application.ProductName)
            SendData = False
        End Try
    End Function

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub txtHostname_TextChanged(sender As Object, e As EventArgs) Handles txtHostname.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub ComboBoxPort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPort.SelectedIndexChanged

    End Sub
End Class
