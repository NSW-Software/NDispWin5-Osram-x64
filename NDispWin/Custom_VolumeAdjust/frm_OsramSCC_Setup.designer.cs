namespace NDispWin
{
    partial class frm_OsramSCC_Setup
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
            this.tbox_ClientTx = new System.Windows.Forms.TextBox();
            this.btn_ClientTx = new System.Windows.Forms.Button();
            this.lbox_Log = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ClientConnect = new System.Windows.Forms.Button();
            this.tbox_ClientPort = new System.Windows.Forms.TextBox();
            this.tbox_ClientIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tmr_Display = new System.Windows.Forms.Timer(this.components);
            this.rbtn_Left = new System.Windows.Forms.RadioButton();
            this.rbtn_Right = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_ServerTx = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbox_ServerTx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_ServerConnect = new System.Windows.Forms.Button();
            this.tbox_ServerPort = new System.Windows.Forms.TextBox();
            this.tbox_ServerIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtn_Standalone = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbox_ClientTx
            // 
            this.tbox_ClientTx.Location = new System.Drawing.Point(70, 71);
            this.tbox_ClientTx.Name = "tbox_ClientTx";
            this.tbox_ClientTx.Size = new System.Drawing.Size(349, 20);
            this.tbox_ClientTx.TabIndex = 3;
            // 
            // btn_ClientTx
            // 
            this.btn_ClientTx.Location = new System.Drawing.Point(425, 69);
            this.btn_ClientTx.Name = "btn_ClientTx";
            this.btn_ClientTx.Size = new System.Drawing.Size(75, 23);
            this.btn_ClientTx.TabIndex = 1;
            this.btn_ClientTx.Text = "Tx";
            this.btn_ClientTx.UseVisualStyleBackColor = true;
            this.btn_ClientTx.Click += new System.EventHandler(this.btn_Tx_Click);
            // 
            // lbox_Log
            // 
            this.lbox_Log.FormattingEnabled = true;
            this.lbox_Log.Location = new System.Drawing.Point(12, 339);
            this.lbox_Log.Name = "lbox_Log";
            this.lbox_Log.Size = new System.Drawing.Size(506, 251);
            this.lbox_Log.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbox_ClientTx);
            this.groupBox1.Controls.Add(this.btn_ClientTx);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_ClientConnect);
            this.groupBox1.Controls.Add(this.tbox_ClientPort);
            this.groupBox1.Controls.Add(this.tbox_ClientIPAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SCC Comm/Left Comm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Tx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // btn_ClientConnect
            // 
            this.btn_ClientConnect.Location = new System.Drawing.Point(166, 17);
            this.btn_ClientConnect.Name = "btn_ClientConnect";
            this.btn_ClientConnect.Size = new System.Drawing.Size(75, 23);
            this.btn_ClientConnect.TabIndex = 1;
            this.btn_ClientConnect.Text = "Connect";
            this.btn_ClientConnect.UseVisualStyleBackColor = true;
            this.btn_ClientConnect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // tbox_ClientPort
            // 
            this.tbox_ClientPort.Location = new System.Drawing.Point(70, 45);
            this.tbox_ClientPort.Name = "tbox_ClientPort";
            this.tbox_ClientPort.Size = new System.Drawing.Size(90, 20);
            this.tbox_ClientPort.TabIndex = 3;
            this.tbox_ClientPort.Text = "9000";
            this.tbox_ClientPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbox_ClientIPAddress
            // 
            this.tbox_ClientIPAddress.Location = new System.Drawing.Point(70, 19);
            this.tbox_ClientIPAddress.Name = "tbox_ClientIPAddress";
            this.tbox_ClientIPAddress.Size = new System.Drawing.Size(90, 20);
            this.tbox_ClientIPAddress.TabIndex = 2;
            this.tbox_ClientIPAddress.Text = "192.168.0.32";
            this.tbox_ClientIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(443, 12);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // tmr_Display
            // 
            this.tmr_Display.Interval = 1000;
            this.tmr_Display.Tick += new System.EventHandler(this.tmr_Display_Tick);
            // 
            // rbtn_Left
            // 
            this.rbtn_Left.AccessibleDescription = "Left";
            this.rbtn_Left.AutoSize = true;
            this.rbtn_Left.Location = new System.Drawing.Point(100, 19);
            this.rbtn_Left.Name = "rbtn_Left";
            this.rbtn_Left.Size = new System.Drawing.Size(43, 17);
            this.rbtn_Left.TabIndex = 6;
            this.rbtn_Left.Text = "Left";
            this.rbtn_Left.UseVisualStyleBackColor = true;
            this.rbtn_Left.Click += new System.EventHandler(this.rbtn_Left_Click);
            // 
            // rbtn_Right
            // 
            this.rbtn_Right.AccessibleDescription = "Right";
            this.rbtn_Right.AutoSize = true;
            this.rbtn_Right.Location = new System.Drawing.Point(182, 19);
            this.rbtn_Right.Name = "rbtn_Right";
            this.rbtn_Right.Size = new System.Drawing.Size(50, 17);
            this.rbtn_Right.TabIndex = 7;
            this.rbtn_Right.Text = "Right";
            this.rbtn_Right.UseVisualStyleBackColor = true;
            this.rbtn_Right.Click += new System.EventHandler(this.rbtn_Right_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.btn_ServerTx);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbox_ServerTx);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btn_ServerConnect);
            this.groupBox3.Controls.Add(this.tbox_ServerPort);
            this.groupBox3.Controls.Add(this.tbox_ServerIP);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(12, 219);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(506, 114);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Right Comm";
            // 
            // btn_ServerTx
            // 
            this.btn_ServerTx.Location = new System.Drawing.Point(425, 69);
            this.btn_ServerTx.Name = "btn_ServerTx";
            this.btn_ServerTx.Size = new System.Drawing.Size(75, 23);
            this.btn_ServerTx.TabIndex = 1;
            this.btn_ServerTx.Text = "Tx";
            this.btn_ServerTx.UseVisualStyleBackColor = true;
            this.btn_ServerTx.Click += new System.EventHandler(this.btn_ServerTx_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tx";
            // 
            // tbox_ServerTx
            // 
            this.tbox_ServerTx.Location = new System.Drawing.Point(70, 71);
            this.tbox_ServerTx.Name = "tbox_ServerTx";
            this.tbox_ServerTx.Size = new System.Drawing.Size(349, 20);
            this.tbox_ServerTx.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port";
            // 
            // btn_ServerConnect
            // 
            this.btn_ServerConnect.Location = new System.Drawing.Point(166, 17);
            this.btn_ServerConnect.Name = "btn_ServerConnect";
            this.btn_ServerConnect.Size = new System.Drawing.Size(75, 23);
            this.btn_ServerConnect.TabIndex = 1;
            this.btn_ServerConnect.Text = "Connect";
            this.btn_ServerConnect.UseVisualStyleBackColor = true;
            this.btn_ServerConnect.Click += new System.EventHandler(this.btn_ServerConnect_Click);
            // 
            // tbox_ServerPort
            // 
            this.tbox_ServerPort.Location = new System.Drawing.Point(70, 45);
            this.tbox_ServerPort.Name = "tbox_ServerPort";
            this.tbox_ServerPort.Size = new System.Drawing.Size(90, 20);
            this.tbox_ServerPort.TabIndex = 3;
            this.tbox_ServerPort.Text = "9000";
            this.tbox_ServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbox_ServerIP
            // 
            this.tbox_ServerIP.Location = new System.Drawing.Point(70, 19);
            this.tbox_ServerIP.Name = "tbox_ServerIP";
            this.tbox_ServerIP.Size = new System.Drawing.Size(90, 20);
            this.tbox_ServerIP.TabIndex = 2;
            this.tbox_ServerIP.Text = "192.168.0.99";
            this.tbox_ServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "IP Address";
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.rbtn_Standalone);
            this.groupBox4.Controls.Add(this.rbtn_Left);
            this.groupBox4.Controls.Add(this.rbtn_Right);
            this.groupBox4.Location = new System.Drawing.Point(12, 41);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(506, 55);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "System Type";
            // 
            // rbtn_Standalone
            // 
            this.rbtn_Standalone.AccessibleDescription = "Standalone";
            this.rbtn_Standalone.AutoSize = true;
            this.rbtn_Standalone.Checked = true;
            this.rbtn_Standalone.Location = new System.Drawing.Point(6, 19);
            this.rbtn_Standalone.Name = "rbtn_Standalone";
            this.rbtn_Standalone.Size = new System.Drawing.Size(79, 17);
            this.rbtn_Standalone.TabIndex = 7;
            this.rbtn_Standalone.TabStop = true;
            this.rbtn_Standalone.Text = "Standalone";
            this.rbtn_Standalone.UseVisualStyleBackColor = true;
            this.rbtn_Standalone.Click += new System.EventHandler(this.rbtn_Standalone_Click);
            // 
            // frm_OsramSCC_Setup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(537, 606);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lbox_Log);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_OsramSCC_Setup";
            this.Text = "frm_OsramSCC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_OsramSCC_FormClosing);
            this.Load += new System.EventHandler(this.frm_OsramSCC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_ClientTx;
        private System.Windows.Forms.Button btn_ClientTx;
        private System.Windows.Forms.ListBox lbox_Log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_ClientConnect;
        private System.Windows.Forms.TextBox tbox_ClientPort;
        private System.Windows.Forms.TextBox tbox_ClientIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Timer tmr_Display;
        private System.Windows.Forms.RadioButton rbtn_Right;
        private System.Windows.Forms.RadioButton rbtn_Left;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_ServerConnect;
        private System.Windows.Forms.TextBox tbox_ServerPort;
        private System.Windows.Forms.TextBox tbox_ServerIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtn_Standalone;
        private System.Windows.Forms.TextBox tbox_ServerTx;
        private System.Windows.Forms.Button btn_ServerTx;
        private System.Windows.Forms.Label label6;
    }
}