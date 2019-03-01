namespace ZPL
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ButtonSaveZpl = new System.Windows.Forms.Button();
            this.ButtonLoadZpl = new System.Windows.Forms.Button();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ButtonCaptureZpl = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.ComboBoxPort = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.ButtonSendZpl = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.Location = new System.Drawing.Point(12, 101);
            this.TextBox1.MaxLength = 500000;
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox1.Size = new System.Drawing.Size(703, 393);
            this.TextBox1.TabIndex = 19;
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.Filter = "ZPL Files|*.zpl|Text Files|*.txt|All Files|*.*";
            // 
            // ButtonSaveZpl
            // 
            this.ButtonSaveZpl.Location = new System.Drawing.Point(131, 25);
            this.ButtonSaveZpl.Name = "ButtonSaveZpl";
            this.ButtonSaveZpl.Size = new System.Drawing.Size(110, 23);
            this.ButtonSaveZpl.TabIndex = 11;
            this.ButtonSaveZpl.Text = "&Save ZPL File";
            this.ButtonSaveZpl.UseVisualStyleBackColor = true;
            this.ButtonSaveZpl.Click += new System.EventHandler(this.ButtonSaveZpl_Click);
            // 
            // ButtonLoadZpl
            // 
            this.ButtonLoadZpl.Location = new System.Drawing.Point(12, 25);
            this.ButtonLoadZpl.Name = "ButtonLoadZpl";
            this.ButtonLoadZpl.Size = new System.Drawing.Size(110, 23);
            this.ButtonLoadZpl.TabIndex = 10;
            this.ButtonLoadZpl.Text = "&Load ZPL File";
            this.ButtonLoadZpl.UseVisualStyleBackColor = true;
            this.ButtonLoadZpl.Click += new System.EventHandler(this.ButtonLoadZpl_Click);
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.Filter = "ZPL Files|*.zpl|Text Files|*.txt|All Files|*.*";
            // 
            // ButtonCaptureZpl
            // 
            this.ButtonCaptureZpl.Location = new System.Drawing.Point(12, 58);
            this.ButtonCaptureZpl.Name = "ButtonCaptureZpl";
            this.ButtonCaptureZpl.Size = new System.Drawing.Size(110, 23);
            this.ButtonCaptureZpl.TabIndex = 12;
            this.ButtonCaptureZpl.Text = "&Capture ZPL";
            this.ButtonCaptureZpl.UseVisualStyleBackColor = true;
            this.ButtonCaptureZpl.Click += new System.EventHandler(this.ButtonCaptureZpl_Click);
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // ComboBoxPort
            // 
            this.ComboBoxPort.FormattingEnabled = true;
            this.ComboBoxPort.Items.AddRange(new object[] {
            "9100",
            "9101",
            "9102"});
            this.ComboBoxPort.Location = new System.Drawing.Point(418, 27);
            this.ComboBoxPort.Name = "ComboBoxPort";
            this.ComboBoxPort.Size = new System.Drawing.Size(110, 21);
            this.ComboBoxPort.TabIndex = 17;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(415, 11);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(29, 13);
            this.Label3.TabIndex = 16;
            this.Label3.Text = "P&ort:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(299, 11);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(58, 13);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "&Hostname:";
            // 
            // txtHostname
            // 
            this.txtHostname.Location = new System.Drawing.Point(302, 27);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(110, 20);
            this.txtHostname.TabIndex = 15;
            // 
            // ButtonSendZpl
            // 
            this.ButtonSendZpl.Location = new System.Drawing.Point(302, 58);
            this.ButtonSendZpl.Name = "ButtonSendZpl";
            this.ButtonSendZpl.Size = new System.Drawing.Size(110, 23);
            this.ButtonSendZpl.TabIndex = 18;
            this.ButtonSendZpl.Text = "Send ZPL to &Printer";
            this.ButtonSendZpl.UseVisualStyleBackColor = true;
            this.ButtonSendZpl.Click += new System.EventHandler(this.ButtonSendZpl_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(128, 63);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(39, 13);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "Label1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(83, 26);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(82, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 506);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.ButtonSaveZpl);
            this.Controls.Add(this.ButtonLoadZpl);
            this.Controls.Add(this.ButtonCaptureZpl);
            this.Controls.Add(this.ComboBoxPort);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtHostname);
            this.Controls.Add(this.ButtonSendZpl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(593, 465);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.Button ButtonSaveZpl;
        internal System.Windows.Forms.Button ButtonLoadZpl;
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        internal System.Windows.Forms.Button ButtonCaptureZpl;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.ComboBox ComboBoxPort;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtHostname;
        internal System.Windows.Forms.Button ButtonSendZpl;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

