namespace NDispWin
{
    partial class frmSECSGEMConnect2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSECSGEMConnect2));
            this.label3 = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxIPAddress = new System.Windows.Forms.TextBox();
            this.rtbxOutMsg = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpManual = new System.Windows.Forms.TabPage();
            this.rtbxRecipeFilename = new System.Windows.Forms.RichTextBox();
            this.btnSelectRecipe = new System.Windows.Forms.Button();
            this.btnAlarmReset = new System.Windows.Forms.Button();
            this.tbxEvent = new System.Windows.Forms.TextBox();
            this.tbxAlarm = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEvent = new System.Windows.Forms.Button();
            this.btnAlarmSet = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPPSend = new System.Windows.Forms.Button();
            this.tpStripMap = new System.Windows.Forms.TabPage();
            this.cbFUseFile = new System.Windows.Forms.CheckBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpMap = new System.Windows.Forms.TabPage();
            this.lbxDownloadedMap = new System.Windows.Forms.ListBox();
            this.tpInternal = new System.Windows.Forms.TabPage();
            this.rtbxInternalMap = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxStripID = new System.Windows.Forms.TextBox();
            this.btnClearMap = new System.Windows.Forms.Button();
            this.btnUploadMap = new System.Windows.Forms.Button();
            this.btnDownloadMap = new System.Windows.Forms.Button();
            this.tpSetting = new System.Windows.Forms.TabPage();
            this.tbxTimeOut = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbUseFreshMap = new System.Windows.Forms.CheckBox();
            this.cbEnableSECSGEMConnect2 = new System.Windows.Forms.CheckBox();
            this.cbEnableE142 = new System.Windows.Forms.CheckBox();
            this.cbEnableRMS = new System.Windows.Forms.CheckBox();
            this.cbEnableEvent = new System.Windows.Forms.CheckBox();
            this.cbEnableAlarm = new System.Windows.Forms.CheckBox();
            this.lblIPPort = new System.Windows.Forms.Label();
            this.tmr500ms = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpManual.SuspendLayout();
            this.tpStripMap.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tpMap.SuspendLayout();
            this.tpInternal.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tpSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 43;
            this.label3.Text = "Host Port";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(166, 76);
            this.tbxPort.Margin = new System.Windows.Forms.Padding(2);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(174, 22);
            this.tbxPort.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 14);
            this.label2.TabIndex = 41;
            this.label2.Text = "Host IP Address";
            // 
            // tbxIPAddress
            // 
            this.tbxIPAddress.Location = new System.Drawing.Point(166, 51);
            this.tbxIPAddress.Margin = new System.Windows.Forms.Padding(2);
            this.tbxIPAddress.Name = "tbxIPAddress";
            this.tbxIPAddress.Size = new System.Drawing.Size(174, 22);
            this.tbxIPAddress.TabIndex = 40;
            // 
            // rtbxOutMsg
            // 
            this.rtbxOutMsg.Location = new System.Drawing.Point(6, 20);
            this.rtbxOutMsg.Margin = new System.Windows.Forms.Padding(2);
            this.rtbxOutMsg.Name = "rtbxOutMsg";
            this.rtbxOutMsg.Size = new System.Drawing.Size(303, 42);
            this.rtbxOutMsg.TabIndex = 44;
            this.rtbxOutMsg.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(311, 21);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(87, 25);
            this.btnSend.TabIndex = 45;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(435, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(87, 30);
            this.btnConnect.TabIndex = 47;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.rtbxOutMsg);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.richTextBox2);
            this.groupBox1.Location = new System.Drawing.Point(484, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 200);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Message";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(6, 66);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(398, 114);
            this.richTextBox2.TabIndex = 51;
            this.richTextBox2.Text = "Message List\n\nALARM {SET/CLEAR},[ALID],[ALTX]\nEVENT,[CEID],[CEIDTX],[DV value]\nDO" +
    "WNLOAD,[downloaded xml]\nUPLOAD,[xml to upload]\nPPSEND,[full recipe name]";
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.lbxLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 458);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(903, 214);
            this.groupBox3.TabIndex = 50;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message Log";
            // 
            // lbxLog
            // 
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.ItemHeight = 14;
            this.lbxLog.Location = new System.Drawing.Point(6, 21);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(891, 172);
            this.lbxLog.TabIndex = 52;
            this.lbxLog.DataSourceChanged += new System.EventHandler(this.lbxLog_DataSourceChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpManual);
            this.tabControl1.Controls.Add(this.tpStripMap);
            this.tabControl1.Controls.Add(this.tpSetting);
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 30);
            this.tabControl1.Location = new System.Drawing.Point(12, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(907, 413);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 52;
            // 
            // tpManual
            // 
            this.tpManual.Controls.Add(this.rtbxRecipeFilename);
            this.tpManual.Controls.Add(this.btnSelectRecipe);
            this.tpManual.Controls.Add(this.btnAlarmReset);
            this.tpManual.Controls.Add(this.tbxEvent);
            this.tpManual.Controls.Add(this.tbxAlarm);
            this.tpManual.Controls.Add(this.label7);
            this.tpManual.Controls.Add(this.label6);
            this.tpManual.Controls.Add(this.btnEvent);
            this.tpManual.Controls.Add(this.btnAlarmSet);
            this.tpManual.Controls.Add(this.label5);
            this.tpManual.Controls.Add(this.btnPPSend);
            this.tpManual.Location = new System.Drawing.Point(4, 34);
            this.tpManual.Name = "tpManual";
            this.tpManual.Padding = new System.Windows.Forms.Padding(3);
            this.tpManual.Size = new System.Drawing.Size(899, 375);
            this.tpManual.TabIndex = 2;
            this.tpManual.Text = "Manual";
            // 
            // rtbxRecipeFilename
            // 
            this.rtbxRecipeFilename.Location = new System.Drawing.Point(207, 35);
            this.rtbxRecipeFilename.Name = "rtbxRecipeFilename";
            this.rtbxRecipeFilename.Size = new System.Drawing.Size(330, 39);
            this.rtbxRecipeFilename.TabIndex = 59;
            this.rtbxRecipeFilename.Text = "";
            // 
            // btnSelectRecipe
            // 
            this.btnSelectRecipe.Location = new System.Drawing.Point(543, 43);
            this.btnSelectRecipe.Name = "btnSelectRecipe";
            this.btnSelectRecipe.Size = new System.Drawing.Size(29, 23);
            this.btnSelectRecipe.TabIndex = 57;
            this.btnSelectRecipe.Text = "...";
            this.btnSelectRecipe.UseVisualStyleBackColor = true;
            this.btnSelectRecipe.Click += new System.EventHandler(this.btnSelectRecipe_Click);
            // 
            // btnAlarmReset
            // 
            this.btnAlarmReset.Location = new System.Drawing.Point(113, 95);
            this.btnAlarmReset.Name = "btnAlarmReset";
            this.btnAlarmReset.Size = new System.Drawing.Size(75, 39);
            this.btnAlarmReset.TabIndex = 56;
            this.btnAlarmReset.Text = "Alarm Reset";
            this.btnAlarmReset.UseVisualStyleBackColor = true;
            this.btnAlarmReset.Click += new System.EventHandler(this.btnAlarmReset_Click);
            // 
            // tbxEvent
            // 
            this.tbxEvent.Location = new System.Drawing.Point(322, 162);
            this.tbxEvent.Name = "tbxEvent";
            this.tbxEvent.Size = new System.Drawing.Size(276, 22);
            this.tbxEvent.TabIndex = 55;
            this.tbxEvent.Text = "0001,TEST CE TEXT,ParaName,ParaValue";
            // 
            // tbxAlarm
            // 
            this.tbxAlarm.Location = new System.Drawing.Point(296, 104);
            this.tbxAlarm.Name = "tbxAlarm";
            this.tbxAlarm.Size = new System.Drawing.Size(276, 22);
            this.tbxAlarm.TabIndex = 55;
            this.tbxAlarm.Text = "0001,TEST ALARM TEXT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(113, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 14);
            this.label7.TabIndex = 54;
            this.label7.Text = "[CEID],[CE Desc],[Name],[DV Value]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 14);
            this.label6.TabIndex = 54;
            this.label6.Text = "[ALID],[ALTX]";
            // 
            // btnEvent
            // 
            this.btnEvent.Location = new System.Drawing.Point(32, 158);
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.Size = new System.Drawing.Size(75, 39);
            this.btnEvent.TabIndex = 53;
            this.btnEvent.Text = "Event";
            this.btnEvent.UseVisualStyleBackColor = true;
            this.btnEvent.Click += new System.EventHandler(this.btnEvent_Click);
            // 
            // btnAlarmSet
            // 
            this.btnAlarmSet.Location = new System.Drawing.Point(32, 95);
            this.btnAlarmSet.Name = "btnAlarmSet";
            this.btnAlarmSet.Size = new System.Drawing.Size(75, 39);
            this.btnAlarmSet.TabIndex = 53;
            this.btnAlarmSet.Text = "Alarm Set";
            this.btnAlarmSet.UseVisualStyleBackColor = true;
            this.btnAlarmSet.Click += new System.EventHandler(this.btnAlarmSet_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 14);
            this.label5.TabIndex = 51;
            this.label5.Text = "[Recipe Name]";
            // 
            // btnPPSend
            // 
            this.btnPPSend.Location = new System.Drawing.Point(32, 35);
            this.btnPPSend.Name = "btnPPSend";
            this.btnPPSend.Size = new System.Drawing.Size(75, 39);
            this.btnPPSend.TabIndex = 50;
            this.btnPPSend.Text = "PPSend";
            this.btnPPSend.UseVisualStyleBackColor = true;
            this.btnPPSend.Click += new System.EventHandler(this.btnPPSend_Click);
            // 
            // tpStripMap
            // 
            this.tpStripMap.Controls.Add(this.cbFUseFile);
            this.tpStripMap.Controls.Add(this.tabControl2);
            this.tpStripMap.Controls.Add(this.label1);
            this.tpStripMap.Controls.Add(this.tbxStripID);
            this.tpStripMap.Controls.Add(this.btnClearMap);
            this.tpStripMap.Controls.Add(this.btnUploadMap);
            this.tpStripMap.Controls.Add(this.btnDownloadMap);
            this.tpStripMap.Location = new System.Drawing.Point(4, 34);
            this.tpStripMap.Name = "tpStripMap";
            this.tpStripMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpStripMap.Size = new System.Drawing.Size(899, 375);
            this.tpStripMap.TabIndex = 1;
            this.tpStripMap.Text = "Strip Map E142";
            // 
            // cbFUseFile
            // 
            this.cbFUseFile.AutoSize = true;
            this.cbFUseFile.Location = new System.Drawing.Point(273, 15);
            this.cbFUseFile.Name = "cbFUseFile";
            this.cbFUseFile.Size = new System.Drawing.Size(67, 18);
            this.cbFUseFile.TabIndex = 53;
            this.cbFUseFile.Text = "Use File";
            this.cbFUseFile.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpMap);
            this.tabControl2.Controls.Add(this.tpInternal);
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Location = new System.Drawing.Point(6, 54);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(887, 314);
            this.tabControl2.TabIndex = 6;
            // 
            // tpMap
            // 
            this.tpMap.Controls.Add(this.lbxDownloadedMap);
            this.tpMap.Location = new System.Drawing.Point(4, 23);
            this.tpMap.Name = "tpMap";
            this.tpMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpMap.Size = new System.Drawing.Size(879, 287);
            this.tpMap.TabIndex = 0;
            this.tpMap.Text = "Downloaded Map";
            // 
            // lbxDownloadedMap
            // 
            this.lbxDownloadedMap.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxDownloadedMap.FormattingEnabled = true;
            this.lbxDownloadedMap.HorizontalScrollbar = true;
            this.lbxDownloadedMap.ItemHeight = 15;
            this.lbxDownloadedMap.Location = new System.Drawing.Point(6, 6);
            this.lbxDownloadedMap.Name = "lbxDownloadedMap";
            this.lbxDownloadedMap.Size = new System.Drawing.Size(867, 259);
            this.lbxDownloadedMap.TabIndex = 6;
            // 
            // tpInternal
            // 
            this.tpInternal.Controls.Add(this.rtbxInternalMap);
            this.tpInternal.Location = new System.Drawing.Point(4, 22);
            this.tpInternal.Name = "tpInternal";
            this.tpInternal.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternal.Size = new System.Drawing.Size(879, 288);
            this.tpInternal.TabIndex = 1;
            this.tpInternal.Text = "Internal Map";
            // 
            // rtbxInternalMap
            // 
            this.rtbxInternalMap.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbxInternalMap.Location = new System.Drawing.Point(6, 6);
            this.rtbxInternalMap.Name = "rtbxInternalMap";
            this.rtbxInternalMap.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.rtbxInternalMap.Size = new System.Drawing.Size(867, 274);
            this.rtbxInternalMap.TabIndex = 8;
            this.rtbxInternalMap.Text = "";
            this.rtbxInternalMap.WordWrap = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(879, 288);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Bincode Definition";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.richTextBox1.Location = new System.Drawing.Point(5, 5);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(869, 274);
            this.richTextBox1.TabIndex = 52;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Strip ID";
            // 
            // tbxStripID
            // 
            this.tbxStripID.Location = new System.Drawing.Point(80, 13);
            this.tbxStripID.Name = "tbxStripID";
            this.tbxStripID.Size = new System.Drawing.Size(187, 22);
            this.tbxStripID.TabIndex = 3;
            // 
            // btnClearMap
            // 
            this.btnClearMap.Location = new System.Drawing.Point(517, 12);
            this.btnClearMap.Name = "btnClearMap";
            this.btnClearMap.Size = new System.Drawing.Size(75, 23);
            this.btnClearMap.TabIndex = 2;
            this.btnClearMap.Text = "Clear Map";
            this.btnClearMap.UseVisualStyleBackColor = true;
            this.btnClearMap.Click += new System.EventHandler(this.btnClearMap_Click);
            // 
            // btnUploadMap
            // 
            this.btnUploadMap.Location = new System.Drawing.Point(436, 12);
            this.btnUploadMap.Name = "btnUploadMap";
            this.btnUploadMap.Size = new System.Drawing.Size(75, 23);
            this.btnUploadMap.TabIndex = 1;
            this.btnUploadMap.Text = "Upload Map";
            this.btnUploadMap.UseVisualStyleBackColor = true;
            this.btnUploadMap.Click += new System.EventHandler(this.btnUploadMap_Click);
            // 
            // btnDownloadMap
            // 
            this.btnDownloadMap.Location = new System.Drawing.Point(355, 12);
            this.btnDownloadMap.Name = "btnDownloadMap";
            this.btnDownloadMap.Size = new System.Drawing.Size(75, 23);
            this.btnDownloadMap.TabIndex = 0;
            this.btnDownloadMap.Text = "DownloadMap";
            this.btnDownloadMap.UseVisualStyleBackColor = true;
            this.btnDownloadMap.Click += new System.EventHandler(this.btnDownloadMap_Click);
            // 
            // tpSetting
            // 
            this.tpSetting.Controls.Add(this.tbxTimeOut);
            this.tpSetting.Controls.Add(this.label4);
            this.tpSetting.Controls.Add(this.cbUseFreshMap);
            this.tpSetting.Controls.Add(this.cbEnableSECSGEMConnect2);
            this.tpSetting.Controls.Add(this.cbEnableE142);
            this.tpSetting.Controls.Add(this.cbEnableRMS);
            this.tpSetting.Controls.Add(this.cbEnableEvent);
            this.tpSetting.Controls.Add(this.cbEnableAlarm);
            this.tpSetting.Controls.Add(this.label2);
            this.tpSetting.Controls.Add(this.tbxIPAddress);
            this.tpSetting.Controls.Add(this.tbxPort);
            this.tpSetting.Controls.Add(this.label3);
            this.tpSetting.Controls.Add(this.groupBox1);
            this.tpSetting.Location = new System.Drawing.Point(4, 34);
            this.tpSetting.Name = "tpSetting";
            this.tpSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpSetting.Size = new System.Drawing.Size(899, 375);
            this.tpSetting.TabIndex = 0;
            this.tpSetting.Text = "Setting";
            // 
            // tbxTimeOut
            // 
            this.tbxTimeOut.Location = new System.Drawing.Point(166, 104);
            this.tbxTimeOut.Margin = new System.Windows.Forms.Padding(2);
            this.tbxTimeOut.Name = "tbxTimeOut";
            this.tbxTimeOut.Size = new System.Drawing.Size(174, 22);
            this.tbxTimeOut.TabIndex = 57;
            this.tbxTimeOut.TextChanged += new System.EventHandler(this.tbxTimeOut_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 14);
            this.label4.TabIndex = 56;
            this.label4.Text = "TimeOut (ms)";
            // 
            // cbUseFreshMap
            // 
            this.cbUseFreshMap.AutoSize = true;
            this.cbUseFreshMap.Location = new System.Drawing.Point(43, 236);
            this.cbUseFreshMap.Name = "cbUseFreshMap";
            this.cbUseFreshMap.Size = new System.Drawing.Size(305, 18);
            this.cbUseFreshMap.TabIndex = 54;
            this.cbUseFreshMap.Text = "Use Fresh Map (Downloaded map will not be used)";
            this.cbUseFreshMap.UseVisualStyleBackColor = true;
            this.cbUseFreshMap.Click += new System.EventHandler(this.cbUseFreshMap_Click);
            // 
            // cbEnableSECSGEMConnect2
            // 
            this.cbEnableSECSGEMConnect2.AutoSize = true;
            this.cbEnableSECSGEMConnect2.Location = new System.Drawing.Point(27, 17);
            this.cbEnableSECSGEMConnect2.Name = "cbEnableSECSGEMConnect2";
            this.cbEnableSECSGEMConnect2.Size = new System.Drawing.Size(171, 18);
            this.cbEnableSECSGEMConnect2.TabIndex = 53;
            this.cbEnableSECSGEMConnect2.Text = "Enable SECSGEMConnect2";
            this.cbEnableSECSGEMConnect2.UseVisualStyleBackColor = true;
            this.cbEnableSECSGEMConnect2.Click += new System.EventHandler(this.cbEnableSECSGEMConnect2_Click);
            // 
            // cbEnableE142
            // 
            this.cbEnableE142.AutoSize = true;
            this.cbEnableE142.Location = new System.Drawing.Point(27, 212);
            this.cbEnableE142.Name = "cbEnableE142";
            this.cbEnableE142.Size = new System.Drawing.Size(278, 18);
            this.cbEnableE142.TabIndex = 52;
            this.cbEnableE142.Text = "Enable StripMap E142 (Download and Upload)";
            this.cbEnableE142.UseVisualStyleBackColor = true;
            this.cbEnableE142.Click += new System.EventHandler(this.cbEnableE142_Click);
            // 
            // cbEnableRMS
            // 
            this.cbEnableRMS.AutoSize = true;
            this.cbEnableRMS.Location = new System.Drawing.Point(27, 188);
            this.cbEnableRMS.Name = "cbEnableRMS";
            this.cbEnableRMS.Size = new System.Drawing.Size(241, 18);
            this.cbEnableRMS.TabIndex = 51;
            this.cbEnableRMS.Text = "Enable RMS (S7F3, Remote PPSELECT)";
            this.cbEnableRMS.UseVisualStyleBackColor = true;
            this.cbEnableRMS.Click += new System.EventHandler(this.cbEnableRMS_Click);
            // 
            // cbEnableEvent
            // 
            this.cbEnableEvent.AutoSize = true;
            this.cbEnableEvent.Location = new System.Drawing.Point(27, 164);
            this.cbEnableEvent.Name = "cbEnableEvent";
            this.cbEnableEvent.Size = new System.Drawing.Size(98, 18);
            this.cbEnableEvent.TabIndex = 50;
            this.cbEnableEvent.Text = "Enable Event";
            this.cbEnableEvent.UseVisualStyleBackColor = true;
            this.cbEnableEvent.Click += new System.EventHandler(this.cbEnableEvent_Click);
            // 
            // cbEnableAlarm
            // 
            this.cbEnableAlarm.AutoSize = true;
            this.cbEnableAlarm.Location = new System.Drawing.Point(27, 140);
            this.cbEnableAlarm.Name = "cbEnableAlarm";
            this.cbEnableAlarm.Size = new System.Drawing.Size(96, 18);
            this.cbEnableAlarm.TabIndex = 49;
            this.cbEnableAlarm.Text = "Enable Alarm";
            this.cbEnableAlarm.UseVisualStyleBackColor = true;
            this.cbEnableAlarm.Click += new System.EventHandler(this.cbEnableAlarm_Click);
            // 
            // lblIPPort
            // 
            this.lblIPPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIPPort.Location = new System.Drawing.Point(12, 9);
            this.lblIPPort.Name = "lblIPPort";
            this.lblIPPort.Size = new System.Drawing.Size(417, 29);
            this.lblIPPort.TabIndex = 54;
            this.lblIPPort.Text = "lblIPPort";
            this.lblIPPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmr500ms
            // 
            this.tmr500ms.Enabled = true;
            this.tmr500ms.Interval = 500;
            this.tmr500ms.Tick += new System.EventHandler(this.tmr500ms_Tick);
            // 
            // frmSECSGEMConnect2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(927, 705);
            this.Controls.Add(this.lblIPPort);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSECSGEMConnect2";
            this.Text = "frmSECSGEMConnect2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSECSGEMConnect2_FormClosed);
            this.Load += new System.EventHandler(this.frmSECSGEMConnect2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpManual.ResumeLayout(false);
            this.tpManual.PerformLayout();
            this.tpStripMap.ResumeLayout(false);
            this.tpStripMap.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tpMap.ResumeLayout(false);
            this.tpInternal.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tpSetting.ResumeLayout(false);
            this.tpSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxIPAddress;
        private System.Windows.Forms.RichTextBox rtbxOutMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ListBox lbxLog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSetting;
        private System.Windows.Forms.TabPage tpStripMap;
        private System.Windows.Forms.Button btnClearMap;
        private System.Windows.Forms.Button btnUploadMap;
        private System.Windows.Forms.Button btnDownloadMap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxStripID;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpMap;
        private System.Windows.Forms.TabPage tpInternal;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox cbFUseFile;
        private System.Windows.Forms.ListBox lbxDownloadedMap;
        private System.Windows.Forms.RichTextBox rtbxInternalMap;
        private System.Windows.Forms.TabPage tpManual;
        private System.Windows.Forms.Button btnSelectRecipe;
        private System.Windows.Forms.Button btnAlarmReset;
        private System.Windows.Forms.TextBox tbxEvent;
        private System.Windows.Forms.TextBox tbxAlarm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnEvent;
        private System.Windows.Forms.Button btnAlarmSet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPPSend;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblIPPort;
        private System.Windows.Forms.Timer tmr500ms;
        private System.Windows.Forms.RichTextBox rtbxRecipeFilename;
        private System.Windows.Forms.CheckBox cbEnableE142;
        private System.Windows.Forms.CheckBox cbEnableRMS;
        private System.Windows.Forms.CheckBox cbEnableEvent;
        private System.Windows.Forms.CheckBox cbEnableAlarm;
        private System.Windows.Forms.CheckBox cbEnableSECSGEMConnect2;
        private System.Windows.Forms.CheckBox cbUseFreshMap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxTimeOut;
    }
}