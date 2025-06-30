
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
            this.btnAckTerminalMessage = new System.Windows.Forms.Button();
            this.rtbRxTerminal = new System.Windows.Forms.RichTextBox();
            this.rtbTxTerminal = new System.Windows.Forms.RichTextBox();
            this.btnSendTerminalMessage = new System.Windows.Forms.Button();
            this.btnGenerateIDList = new System.Windows.Forms.Button();
            this.lblProcessState = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbE142 = new System.Windows.Forms.CheckBox();
            this.tbBadgeNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSetSubstrate = new System.Windows.Forms.CheckBox();
            this.cbLocal = new System.Windows.Forms.CheckBox();
            this.rtbBinCodes = new System.Windows.Forms.RichTextBox();
            this.tbSubstrateID = new System.Windows.Forms.TextBox();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.btnLocalRemote.Text = "LocalRemote";
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
            this.btnOnlineOffline.Text = "OnlineOffline";
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
            this.groupBox3.Controls.Add(this.btnAckTerminalMessage);
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
            // btnAckTerminalMessage
            // 
            this.btnAckTerminalMessage.Location = new System.Drawing.Point(402, 81);
            this.btnAckTerminalMessage.Name = "btnAckTerminalMessage";
            this.btnAckTerminalMessage.Size = new System.Drawing.Size(101, 32);
            this.btnAckTerminalMessage.TabIndex = 74;
            this.btnAckTerminalMessage.Text = "Acknowledge";
            this.btnAckTerminalMessage.UseVisualStyleBackColor = true;
            this.btnAckTerminalMessage.Click += new System.EventHandler(this.btnAckTerminalMessage_Click);
            // 
            // rtbRxTerminal
            // 
            this.rtbRxTerminal.Location = new System.Drawing.Point(6, 81);
            this.rtbRxTerminal.Name = "rtbRxTerminal";
            this.rtbRxTerminal.Size = new System.Drawing.Size(390, 54);
            this.rtbRxTerminal.TabIndex = 73;
            this.rtbRxTerminal.Text = "";
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
            // btnGenerateIDList
            // 
            this.btnGenerateIDList.Location = new System.Drawing.Point(420, 85);
            this.btnGenerateIDList.Name = "btnGenerateIDList";
            this.btnGenerateIDList.Size = new System.Drawing.Size(101, 39);
            this.btnGenerateIDList.TabIndex = 73;
            this.btnGenerateIDList.Text = "Generate ID and RCMD List";
            this.btnGenerateIDList.UseVisualStyleBackColor = true;
            this.btnGenerateIDList.Click += new System.EventHandler(this.btnGenerateIDList_Click);
            // 
            // lblProcessState
            // 
            this.lblProcessState.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProcessState.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessState.Location = new System.Drawing.Point(116, 119);
            this.lblProcessState.Name = "lblProcessState";
            this.lblProcessState.Size = new System.Drawing.Size(157, 35);
            this.lblProcessState.TabIndex = 75;
            this.lblProcessState.Text = "-";
            this.lblProcessState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbE142);
            this.groupBox4.Controls.Add(this.tbBadgeNo);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cbSetSubstrate);
            this.groupBox4.Controls.Add(this.cbLocal);
            this.groupBox4.Controls.Add(this.rtbBinCodes);
            this.groupBox4.Controls.Add(this.tbSubstrateID);
            this.groupBox4.Controls.Add(this.rtbInfo);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.btnUpload);
            this.groupBox4.Controls.Add(this.btnDownload);
            this.groupBox4.Location = new System.Drawing.Point(527, 199);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(509, 314);
            this.groupBox4.TabIndex = 76;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "E142";
            // 
            // cbE142
            // 
            this.cbE142.AutoSize = true;
            this.cbE142.Location = new System.Drawing.Point(9, 15);
            this.cbE142.Name = "cbE142";
            this.cbE142.Size = new System.Drawing.Size(54, 18);
            this.cbE142.TabIndex = 83;
            this.cbE142.Text = "E142";
            this.cbE142.UseVisualStyleBackColor = true;
            this.cbE142.CheckedChanged += new System.EventHandler(this.cbE142_CheckedChanged);
            // 
            // tbBadgeNo
            // 
            this.tbBadgeNo.Location = new System.Drawing.Point(88, 40);
            this.tbBadgeNo.Name = "tbBadgeNo";
            this.tbBadgeNo.Size = new System.Drawing.Size(128, 22);
            this.tbBadgeNo.TabIndex = 82;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 14);
            this.label4.TabIndex = 81;
            this.label4.Text = "Badge No";
            // 
            // cbSetSubstrate
            // 
            this.cbSetSubstrate.AutoSize = true;
            this.cbSetSubstrate.Location = new System.Drawing.Point(89, 15);
            this.cbSetSubstrate.Name = "cbSetSubstrate";
            this.cbSetSubstrate.Size = new System.Drawing.Size(102, 18);
            this.cbSetSubstrate.TabIndex = 80;
            this.cbSetSubstrate.Text = "Set Substrate";
            this.cbSetSubstrate.UseVisualStyleBackColor = true;
            this.cbSetSubstrate.CheckedChanged += new System.EventHandler(this.cbSubstrateScanner_CheckedChanged);
            // 
            // cbLocal
            // 
            this.cbLocal.AutoSize = true;
            this.cbLocal.Location = new System.Drawing.Point(294, 29);
            this.cbLocal.Name = "cbLocal";
            this.cbLocal.Size = new System.Drawing.Size(53, 18);
            this.cbLocal.TabIndex = 79;
            this.cbLocal.Text = "Local";
            this.cbLocal.UseVisualStyleBackColor = true;
            // 
            // rtbBinCodes
            // 
            this.rtbBinCodes.Location = new System.Drawing.Point(144, 95);
            this.rtbBinCodes.Name = "rtbBinCodes";
            this.rtbBinCodes.Size = new System.Drawing.Size(359, 213);
            this.rtbBinCodes.TabIndex = 78;
            this.rtbBinCodes.Text = "";
            this.rtbBinCodes.WordWrap = false;
            // 
            // tbSubstrateID
            // 
            this.tbSubstrateID.Location = new System.Drawing.Point(88, 68);
            this.tbSubstrateID.Name = "tbSubstrateID";
            this.tbSubstrateID.Size = new System.Drawing.Size(128, 22);
            this.tbSubstrateID.TabIndex = 72;
            // 
            // rtbInfo
            // 
            this.rtbInfo.Location = new System.Drawing.Point(9, 95);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(129, 213);
            this.rtbInfo.TabIndex = 77;
            this.rtbInfo.Text = "";
            this.rtbInfo.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 71;
            this.label3.Text = "Subsrtate ID";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(431, 21);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(72, 32);
            this.btnUpload.TabIndex = 70;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(353, 21);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(72, 32);
            this.btnDownload.TabIndex = 69;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // frmSecsGem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1143, 627);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.lblProcessState);
            this.Controls.Add(this.btnGenerateIDList);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.Button btnGenerateIDList;
        private System.Windows.Forms.Button btnAckTerminalMessage;
        private System.Windows.Forms.Label lblProcessState;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbSubstrateID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.RichTextBox rtbBinCodes;
        private System.Windows.Forms.CheckBox cbLocal;
        private System.Windows.Forms.CheckBox cbSetSubstrate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbE142;
        private System.Windows.Forms.TextBox tbBadgeNo;
    }
}