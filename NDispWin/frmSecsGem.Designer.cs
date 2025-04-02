
namespace NDispWin
{
    partial class frmSecsGem
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
            this.lblIPPort = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnLocalRemote = new System.Windows.Forms.Button();
            this.tmr1s = new System.Windows.Forms.Timer(this.components);
            this.btnOnlineOffline = new System.Windows.Forms.Button();
            this.cbxProcessState = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUploadPP = new System.Windows.Forms.Button();
            this.btnRequestPP = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbAlarmCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAlarmClear = new System.Windows.Forms.Button();
            this.btnAlarmSet = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbTxTerminal = new System.Windows.Forms.RichTextBox();
            this.btnSendTerminalMessage = new System.Windows.Forms.Button();
            this.rtbRxTerminal = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIPPort
            // 
            this.lblIPPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIPPort.Location = new System.Drawing.Point(14, 10);
            this.lblIPPort.Name = "lblIPPort";
            this.lblIPPort.Size = new System.Drawing.Size(507, 31);
            this.lblIPPort.TabIndex = 56;
            this.lblIPPort.Text = "lblIPPort";
            this.lblIPPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(206, 44);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(101, 32);
            this.btnConnect.TabIndex = 55;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnLocalRemote
            // 
            this.btnLocalRemote.Location = new System.Drawing.Point(420, 44);
            this.btnLocalRemote.Name = "btnLocalRemote";
            this.btnLocalRemote.Size = new System.Drawing.Size(101, 32);
            this.btnLocalRemote.TabIndex = 64;
            this.btnLocalRemote.Text = "Local";
            this.btnLocalRemote.UseVisualStyleBackColor = true;
            this.btnLocalRemote.Click += new System.EventHandler(this.btnLocalRemote_Click);
            // 
            // tmr1s
            // 
            this.tmr1s.Enabled = true;
            this.tmr1s.Interval = 1000;
            this.tmr1s.Tick += new System.EventHandler(this.tmr1s_Tick);
            // 
            // btnOnlineOffline
            // 
            this.btnOnlineOffline.Location = new System.Drawing.Point(313, 44);
            this.btnOnlineOffline.Name = "btnOnlineOffline";
            this.btnOnlineOffline.Size = new System.Drawing.Size(101, 32);
            this.btnOnlineOffline.TabIndex = 65;
            this.btnOnlineOffline.Text = "Local";
            this.btnOnlineOffline.UseVisualStyleBackColor = true;
            this.btnOnlineOffline.Click += new System.EventHandler(this.btnOnlineOffline_Click);
            // 
            // cbxProcessState
            // 
            this.cbxProcessState.FormattingEnabled = true;
            this.cbxProcessState.Location = new System.Drawing.Point(116, 94);
            this.cbxProcessState.Name = "cbxProcessState";
            this.cbxProcessState.Size = new System.Drawing.Size(157, 22);
            this.cbxProcessState.TabIndex = 66;
            this.cbxProcessState.SelectionChangeCommitted += new System.EventHandler(this.cbxProcessState_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 67;
            this.label1.Text = "Process State";
            // 
            // btnUploadPP
            // 
            this.btnUploadPP.Location = new System.Drawing.Point(295, 15);
            this.btnUploadPP.Name = "btnUploadPP";
            this.btnUploadPP.Size = new System.Drawing.Size(101, 32);
            this.btnUploadPP.TabIndex = 68;
            this.btnUploadPP.Text = "Upload PP";
            this.btnUploadPP.UseVisualStyleBackColor = true;
            this.btnUploadPP.Click += new System.EventHandler(this.btnUploadPP_Click);
            // 
            // btnRequestPP
            // 
            this.btnRequestPP.Location = new System.Drawing.Point(402, 15);
            this.btnRequestPP.Name = "btnRequestPP";
            this.btnRequestPP.Size = new System.Drawing.Size(101, 32);
            this.btnRequestPP.TabIndex = 69;
            this.btnRequestPP.Text = "Request PP";
            this.btnRequestPP.UseVisualStyleBackColor = true;
            this.btnRequestPP.Click += new System.EventHandler(this.btnRequestPP_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tbPP);
            this.groupBox1.Controls.Add(this.btnRequestPP);
            this.groupBox1.Controls.Add(this.btnUploadPP);
            this.groupBox1.Location = new System.Drawing.Point(12, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 68);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process Recipe Management";
            // 
            // tbPP
            // 
            this.tbPP.Location = new System.Drawing.Point(6, 21);
            this.tbPP.Name = "tbPP";
            this.tbPP.Size = new System.Drawing.Size(283, 22);
            this.tbPP.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbAlarmCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnAlarmClear);
            this.groupBox2.Controls.Add(this.btnAlarmSet);
            this.groupBox2.Location = new System.Drawing.Point(12, 273);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(509, 78);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Alarm";
            // 
            // tbAlarmCode
            // 
            this.tbAlarmCode.Location = new System.Drawing.Point(47, 27);
            this.tbAlarmCode.Name = "tbAlarmCode";
            this.tbAlarmCode.Size = new System.Drawing.Size(80, 22);
            this.tbAlarmCode.TabIndex = 72;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 71;
            this.label2.Text = "Code";
            // 
            // btnAlarmClear
            // 
            this.btnAlarmClear.Location = new System.Drawing.Point(240, 21);
            this.btnAlarmClear.Name = "btnAlarmClear";
            this.btnAlarmClear.Size = new System.Drawing.Size(101, 32);
            this.btnAlarmClear.TabIndex = 70;
            this.btnAlarmClear.Text = "Clear";
            this.btnAlarmClear.UseVisualStyleBackColor = true;
            this.btnAlarmClear.Click += new System.EventHandler(this.btnAlarmClear_Click);
            // 
            // btnAlarmSet
            // 
            this.btnAlarmSet.Location = new System.Drawing.Point(133, 21);
            this.btnAlarmSet.Name = "btnAlarmSet";
            this.btnAlarmSet.Size = new System.Drawing.Size(101, 32);
            this.btnAlarmSet.TabIndex = 69;
            this.btnAlarmSet.Text = "Set";
            this.btnAlarmSet.UseVisualStyleBackColor = true;
            this.btnAlarmSet.Click += new System.EventHandler(this.btnAlarmSet_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.rtbRxTerminal);
            this.groupBox3.Controls.Add(this.rtbTxTerminal);
            this.groupBox3.Controls.Add(this.btnSendTerminalMessage);
            this.groupBox3.Location = new System.Drawing.Point(12, 357);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(509, 156);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Terminal Message";
            // 
            // rtbTxTerminal
            // 
            this.rtbTxTerminal.Location = new System.Drawing.Point(6, 21);
            this.rtbTxTerminal.Name = "rtbTxTerminal";
            this.rtbTxTerminal.Size = new System.Drawing.Size(390, 54);
            this.rtbTxTerminal.TabIndex = 72;
            this.rtbTxTerminal.Text = "";
            // 
            // btnSendTerminalMessage
            // 
            this.btnSendTerminalMessage.Location = new System.Drawing.Point(402, 21);
            this.btnSendTerminalMessage.Name = "btnSendTerminalMessage";
            this.btnSendTerminalMessage.Size = new System.Drawing.Size(101, 32);
            this.btnSendTerminalMessage.TabIndex = 71;
            this.btnSendTerminalMessage.Text = "Send";
            this.btnSendTerminalMessage.UseVisualStyleBackColor = true;
            this.btnSendTerminalMessage.Click += new System.EventHandler(this.btnSendTerminalMsg_Click);
            // 
            // rtbRxTerminal
            // 
            this.rtbRxTerminal.Location = new System.Drawing.Point(6, 81);
            this.rtbRxTerminal.Name = "rtbRxTerminal";
            this.rtbRxTerminal.Size = new System.Drawing.Size(390, 54);
            this.rtbRxTerminal.TabIndex = 73;
            this.rtbRxTerminal.Text = "";
            // 
            // frmSecsGem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 533);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxProcessState);
            this.Controls.Add(this.btnOnlineOffline);
            this.Controls.Add(this.btnLocalRemote);
            this.Controls.Add(this.lblIPPort);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmSecsGem";
            this.Text = "frmSecsGem";
            this.Load += new System.EventHandler(this.frmSecsGem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIPPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnLocalRemote;
        private System.Windows.Forms.Timer tmr1s;
        private System.Windows.Forms.Button btnOnlineOffline;
        private System.Windows.Forms.ComboBox cbxProcessState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUploadPP;
        private System.Windows.Forms.Button btnRequestPP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbPP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAlarmSet;
        private System.Windows.Forms.Button btnAlarmClear;
        private System.Windows.Forms.TextBox tbAlarmCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbTxTerminal;
        private System.Windows.Forms.Button btnSendTerminalMessage;
        private System.Windows.Forms.RichTextBox rtbRxTerminal;
    }
}