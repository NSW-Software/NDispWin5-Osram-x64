namespace NDispWin
{
    partial class frm_DispCore_DispSetup_Custom
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
            this.lbl_VolumeOfstProtocol = new System.Windows.Forms.Label();
            this.tmr_Display = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnl_InputMapPaths_Lmd_EMap = new System.Windows.Forms.Panel();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.tbox_Prefix3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbox_Suffix3 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lbl_InputMap_DataPath3 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tbox_Prefix2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbox_Suffix2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_InputMap_DataPath2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbox_Prefix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbox_Suffix = new System.Windows.Forms.TextBox();
            this.btn_Update = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_InputMap_DataPath = new System.Windows.Forms.Label();
            this.btn_InputMap_CheckDataPath = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInputMapSetup = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_InputMap_Protocol = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlOsramICC = new System.Windows.Forms.Panel();
            this.tbxOsramICCOutputPath = new System.Windows.Forms.TextBox();
            this.tbxOsramICCInputPath = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnInputFileLoad = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnOutputFileTileLookUp = new System.Windows.Forms.Button();
            this.tbxLookUpTileID = new System.Windows.Forms.TextBox();
            this.btnOutputFileLoad = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLocalListEditPass2 = new System.Windows.Forms.Button();
            this.btnLocalListSave = new System.Windows.Forms.Button();
            this.btnLocalListEditPass1 = new System.Windows.Forms.Button();
            this.btnLocalListLoad = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.btnCheckOsramICCPath = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_EditVolumeOfst = new System.Windows.Forms.Button();
            this.btnOsramICCTest = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnl_InputMapPaths_Lmd_EMap.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlOsramICC.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_VolumeOfstProtocol
            // 
            this.lbl_VolumeOfstProtocol.BackColor = System.Drawing.Color.White;
            this.lbl_VolumeOfstProtocol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_VolumeOfstProtocol.Location = new System.Drawing.Point(150, 2);
            this.lbl_VolumeOfstProtocol.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_VolumeOfstProtocol.Name = "lbl_VolumeOfstProtocol";
            this.lbl_VolumeOfstProtocol.Size = new System.Drawing.Size(218, 23);
            this.lbl_VolumeOfstProtocol.TabIndex = 159;
            this.lbl_VolumeOfstProtocol.Text = "-100";
            this.lbl_VolumeOfstProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_VolumeOfstProtocol.Click += new System.EventHandler(this.lbl_Protocol_Click);
            // 
            // tmr_Display
            // 
            this.tmr_Display.Tick += new System.EventHandler(this.tmr_Display_Tick);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Protocol";
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 23);
            this.label2.TabIndex = 202;
            this.label2.Text = "Protocol";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(607, 677);
            this.panel3.TabIndex = 200;
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Input Map";
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.pnl_InputMapPaths_Lmd_EMap);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(605, 258);
            this.groupBox2.TabIndex = 204;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input Map";
            // 
            // pnl_InputMapPaths_Lmd_EMap
            // 
            this.pnl_InputMapPaths_Lmd_EMap.AutoSize = true;
            this.pnl_InputMapPaths_Lmd_EMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.btnClearAll);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label14);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.tbox_Prefix3);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label12);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.tbox_Suffix3);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label15);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.lbl_InputMap_DataPath3);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label17);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.tbox_Prefix2);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label9);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.tbox_Suffix2);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label10);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.lbl_InputMap_DataPath2);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label13);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.tbox_Prefix);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label5);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.tbox_Suffix);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.btn_Update);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label8);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.lbl_InputMap_DataPath);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.btn_InputMap_CheckDataPath);
            this.pnl_InputMapPaths_Lmd_EMap.Controls.Add(this.label11);
            this.pnl_InputMapPaths_Lmd_EMap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_InputMapPaths_Lmd_EMap.Location = new System.Drawing.Point(5, 48);
            this.pnl_InputMapPaths_Lmd_EMap.Name = "pnl_InputMapPaths_Lmd_EMap";
            this.pnl_InputMapPaths_Lmd_EMap.Size = new System.Drawing.Size(595, 205);
            this.pnl_InputMapPaths_Lmd_EMap.TabIndex = 215;
            // 
            // btnClearAll
            // 
            this.btnClearAll.AccessibleDescription = "Clear All";
            this.btnClearAll.Location = new System.Drawing.Point(368, 172);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(70, 30);
            this.btnClearAll.TabIndex = 232;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // label14
            // 
            this.label14.AccessibleDescription = "";
            this.label14.Location = new System.Drawing.Point(2, 176);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(353, 23);
            this.label14.TabIndex = 231;
            this.label14.Text = "Note: Singulated Mode use Data Path2";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbox_Prefix3
            // 
            this.tbox_Prefix3.Location = new System.Drawing.Point(150, 144);
            this.tbox_Prefix3.Name = "tbox_Prefix3";
            this.tbox_Prefix3.Size = new System.Drawing.Size(100, 22);
            this.tbox_Prefix3.TabIndex = 230;
            // 
            // label12
            // 
            this.label12.AccessibleDescription = "Filename";
            this.label12.Location = new System.Drawing.Point(2, 143);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 23);
            this.label12.TabIndex = 229;
            this.label12.Text = "Filename";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbox_Suffix3
            // 
            this.tbox_Suffix3.Location = new System.Drawing.Point(336, 145);
            this.tbox_Suffix3.Name = "tbox_Suffix3";
            this.tbox_Suffix3.Size = new System.Drawing.Size(100, 22);
            this.tbox_Suffix3.TabIndex = 228;
            // 
            // label15
            // 
            this.label15.AccessibleDescription = "";
            this.label15.Location = new System.Drawing.Point(255, 144);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 23);
            this.label15.TabIndex = 227;
            this.label15.Text = "+ Read ID +";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_InputMap_DataPath3
            // 
            this.lbl_InputMap_DataPath3.BackColor = System.Drawing.Color.White;
            this.lbl_InputMap_DataPath3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_InputMap_DataPath3.Location = new System.Drawing.Point(150, 116);
            this.lbl_InputMap_DataPath3.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_InputMap_DataPath3.Name = "lbl_InputMap_DataPath3";
            this.lbl_InputMap_DataPath3.Size = new System.Drawing.Size(440, 23);
            this.lbl_InputMap_DataPath3.TabIndex = 225;
            this.lbl_InputMap_DataPath3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_InputMap_DataPath3.Click += new System.EventHandler(this.lbl_InputMap_DataPath3_Click);
            // 
            // label17
            // 
            this.label17.AccessibleDescription = "Data Path3";
            this.label17.Location = new System.Drawing.Point(2, 116);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(144, 23);
            this.label17.TabIndex = 226;
            this.label17.Text = "Data Path3";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbox_Prefix2
            // 
            this.tbox_Prefix2.Location = new System.Drawing.Point(150, 88);
            this.tbox_Prefix2.Name = "tbox_Prefix2";
            this.tbox_Prefix2.Size = new System.Drawing.Size(100, 22);
            this.tbox_Prefix2.TabIndex = 224;
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "Filename";
            this.label9.Location = new System.Drawing.Point(2, 87);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 23);
            this.label9.TabIndex = 223;
            this.label9.Text = "Filename";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbox_Suffix2
            // 
            this.tbox_Suffix2.Location = new System.Drawing.Point(336, 89);
            this.tbox_Suffix2.Name = "tbox_Suffix2";
            this.tbox_Suffix2.Size = new System.Drawing.Size(100, 22);
            this.tbox_Suffix2.TabIndex = 222;
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "";
            this.label10.Location = new System.Drawing.Point(255, 88);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 221;
            this.label10.Text = "+ Read ID +";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_InputMap_DataPath2
            // 
            this.lbl_InputMap_DataPath2.BackColor = System.Drawing.Color.White;
            this.lbl_InputMap_DataPath2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_InputMap_DataPath2.Location = new System.Drawing.Point(150, 60);
            this.lbl_InputMap_DataPath2.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_InputMap_DataPath2.Name = "lbl_InputMap_DataPath2";
            this.lbl_InputMap_DataPath2.Size = new System.Drawing.Size(440, 23);
            this.lbl_InputMap_DataPath2.TabIndex = 219;
            this.lbl_InputMap_DataPath2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_InputMap_DataPath2.Click += new System.EventHandler(this.lbl_InputMap_DataPath2_Click);
            // 
            // label13
            // 
            this.label13.AccessibleDescription = "";
            this.label13.Location = new System.Drawing.Point(2, 60);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(144, 23);
            this.label13.TabIndex = 220;
            this.label13.Text = "Data Path2";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbox_Prefix
            // 
            this.tbox_Prefix.Location = new System.Drawing.Point(150, 33);
            this.tbox_Prefix.Name = "tbox_Prefix";
            this.tbox_Prefix.Size = new System.Drawing.Size(100, 22);
            this.tbox_Prefix.TabIndex = 218;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "Filename";
            this.label5.Location = new System.Drawing.Point(2, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 23);
            this.label5.TabIndex = 217;
            this.label5.Text = "Filename";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbox_Suffix
            // 
            this.tbox_Suffix.Location = new System.Drawing.Point(336, 34);
            this.tbox_Suffix.Name = "tbox_Suffix";
            this.tbox_Suffix.Size = new System.Drawing.Size(100, 22);
            this.tbox_Suffix.TabIndex = 216;
            // 
            // btn_Update
            // 
            this.btn_Update.AccessibleDescription = "Update";
            this.btn_Update.Location = new System.Drawing.Point(444, 172);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(70, 30);
            this.btn_Update.TabIndex = 215;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "";
            this.label8.Location = new System.Drawing.Point(255, 33);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 214;
            this.label8.Text = "+ Read ID +";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_InputMap_DataPath
            // 
            this.lbl_InputMap_DataPath.BackColor = System.Drawing.Color.White;
            this.lbl_InputMap_DataPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_InputMap_DataPath.Location = new System.Drawing.Point(150, 5);
            this.lbl_InputMap_DataPath.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_InputMap_DataPath.Name = "lbl_InputMap_DataPath";
            this.lbl_InputMap_DataPath.Size = new System.Drawing.Size(440, 23);
            this.lbl_InputMap_DataPath.TabIndex = 209;
            this.lbl_InputMap_DataPath.Text = "-100";
            this.lbl_InputMap_DataPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_InputMap_DataPath.Click += new System.EventHandler(this.lbl_InputMap_DataPath_Click);
            // 
            // btn_InputMap_CheckDataPath
            // 
            this.btn_InputMap_CheckDataPath.AccessibleDescription = "Check";
            this.btn_InputMap_CheckDataPath.Location = new System.Drawing.Point(520, 172);
            this.btn_InputMap_CheckDataPath.Name = "btn_InputMap_CheckDataPath";
            this.btn_InputMap_CheckDataPath.Size = new System.Drawing.Size(70, 30);
            this.btn_InputMap_CheckDataPath.TabIndex = 210;
            this.btn_InputMap_CheckDataPath.Text = "Check";
            this.btn_InputMap_CheckDataPath.UseVisualStyleBackColor = true;
            this.btn_InputMap_CheckDataPath.Click += new System.EventHandler(this.btn_InputMap_CheckDataPath_Click);
            // 
            // label11
            // 
            this.label11.AccessibleDescription = "Data Path";
            this.label11.Location = new System.Drawing.Point(2, 5);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 23);
            this.label11.TabIndex = 211;
            this.label11.Text = "Data Path";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnInputMapSetup);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lbl_InputMap_Protocol);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 28);
            this.panel1.TabIndex = 216;
            // 
            // btnInputMapSetup
            // 
            this.btnInputMapSetup.Location = new System.Drawing.Point(373, 2);
            this.btnInputMapSetup.Name = "btnInputMapSetup";
            this.btnInputMapSetup.Size = new System.Drawing.Size(75, 23);
            this.btnInputMapSetup.TabIndex = 209;
            this.btnInputMapSetup.Text = "Setup";
            this.btnInputMapSetup.UseVisualStyleBackColor = true;
            this.btnInputMapSetup.Click += new System.EventHandler(this.btnInputMapSetup_Click);
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "Protocol";
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 23);
            this.label7.TabIndex = 208;
            this.label7.Text = "Protocol";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_InputMap_Protocol
            // 
            this.lbl_InputMap_Protocol.BackColor = System.Drawing.Color.White;
            this.lbl_InputMap_Protocol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_InputMap_Protocol.Location = new System.Drawing.Point(150, 2);
            this.lbl_InputMap_Protocol.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_InputMap_Protocol.Name = "lbl_InputMap_Protocol";
            this.lbl_InputMap_Protocol.Size = new System.Drawing.Size(218, 23);
            this.lbl_InputMap_Protocol.TabIndex = 207;
            this.lbl_InputMap_Protocol.Text = "-100";
            this.lbl_InputMap_Protocol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_InputMap_Protocol.Click += new System.EventHandler(this.lbl_InputMap_Protocol_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Volume Offset";
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.pnlOsramICC);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(605, 233);
            this.groupBox1.TabIndex = 203;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volume Offset";
            // 
            // pnlOsramICC
            // 
            this.pnlOsramICC.AutoSize = true;
            this.pnlOsramICC.Controls.Add(this.btnOsramICCTest);
            this.pnlOsramICC.Controls.Add(this.tbxOsramICCOutputPath);
            this.pnlOsramICC.Controls.Add(this.tbxOsramICCInputPath);
            this.pnlOsramICC.Controls.Add(this.groupBox5);
            this.pnlOsramICC.Controls.Add(this.groupBox4);
            this.pnlOsramICC.Controls.Add(this.groupBox3);
            this.pnlOsramICC.Controls.Add(this.label18);
            this.pnlOsramICC.Controls.Add(this.btnCheckOsramICCPath);
            this.pnlOsramICC.Controls.Add(this.label21);
            this.pnlOsramICC.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOsramICC.Location = new System.Drawing.Point(5, 49);
            this.pnlOsramICC.Name = "pnlOsramICC";
            this.pnlOsramICC.Size = new System.Drawing.Size(595, 179);
            this.pnlOsramICC.TabIndex = 209;
            // 
            // tbxOsramICCOutputPath
            // 
            this.tbxOsramICCOutputPath.Location = new System.Drawing.Point(150, 33);
            this.tbxOsramICCOutputPath.Name = "tbxOsramICCOutputPath";
            this.tbxOsramICCOutputPath.Size = new System.Drawing.Size(440, 22);
            this.tbxOsramICCOutputPath.TabIndex = 214;
            // 
            // tbxOsramICCInputPath
            // 
            this.tbxOsramICCInputPath.Location = new System.Drawing.Point(150, 6);
            this.tbxOsramICCInputPath.Name = "tbxOsramICCInputPath";
            this.tbxOsramICCInputPath.Size = new System.Drawing.Size(440, 22);
            this.tbxOsramICCInputPath.TabIndex = 213;
            // 
            // groupBox5
            // 
            this.groupBox5.AutoSize = true;
            this.groupBox5.Controls.Add(this.btnInputFileLoad);
            this.groupBox5.Location = new System.Drawing.Point(5, 68);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.groupBox5.Size = new System.Drawing.Size(82, 108);
            this.groupBox5.TabIndex = 211;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Input File";
            // 
            // btnInputFileLoad
            // 
            this.btnInputFileLoad.AccessibleDescription = "Load";
            this.btnInputFileLoad.Location = new System.Drawing.Point(6, 24);
            this.btnInputFileLoad.Name = "btnInputFileLoad";
            this.btnInputFileLoad.Size = new System.Drawing.Size(70, 30);
            this.btnInputFileLoad.TabIndex = 205;
            this.btnInputFileLoad.Text = "Load";
            this.btnInputFileLoad.UseVisualStyleBackColor = true;
            this.btnInputFileLoad.Click += new System.EventHandler(this.btnInputFileLoad_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.btnOutputFileTileLookUp);
            this.groupBox4.Controls.Add(this.tbxLookUpTileID);
            this.groupBox4.Controls.Add(this.btnOutputFileLoad);
            this.groupBox4.Location = new System.Drawing.Point(93, 68);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.groupBox4.Size = new System.Drawing.Size(158, 108);
            this.groupBox4.TabIndex = 210;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output File";
            // 
            // btnOutputFileTileLookUp
            // 
            this.btnOutputFileTileLookUp.AccessibleDescription = "LookUp";
            this.btnOutputFileTileLookUp.Location = new System.Drawing.Point(82, 60);
            this.btnOutputFileTileLookUp.Name = "btnOutputFileTileLookUp";
            this.btnOutputFileTileLookUp.Size = new System.Drawing.Size(70, 30);
            this.btnOutputFileTileLookUp.TabIndex = 209;
            this.btnOutputFileTileLookUp.Text = "LookUp";
            this.btnOutputFileTileLookUp.UseVisualStyleBackColor = true;
            this.btnOutputFileTileLookUp.Click += new System.EventHandler(this.btnOutputFileTileLookUp_Click);
            // 
            // tbxLookUpTileID
            // 
            this.tbxLookUpTileID.Location = new System.Drawing.Point(6, 65);
            this.tbxLookUpTileID.Name = "tbxLookUpTileID";
            this.tbxLookUpTileID.Size = new System.Drawing.Size(70, 22);
            this.tbxLookUpTileID.TabIndex = 212;
            // 
            // btnOutputFileLoad
            // 
            this.btnOutputFileLoad.AccessibleDescription = "Load";
            this.btnOutputFileLoad.Location = new System.Drawing.Point(82, 24);
            this.btnOutputFileLoad.Name = "btnOutputFileLoad";
            this.btnOutputFileLoad.Size = new System.Drawing.Size(70, 30);
            this.btnOutputFileLoad.TabIndex = 205;
            this.btnOutputFileLoad.Text = "Load";
            this.btnOutputFileLoad.UseVisualStyleBackColor = true;
            this.btnOutputFileLoad.Click += new System.EventHandler(this.btnOutputFileLoad_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.btnLocalListEditPass2);
            this.groupBox3.Controls.Add(this.btnLocalListSave);
            this.groupBox3.Controls.Add(this.btnLocalListEditPass1);
            this.groupBox3.Controls.Add(this.btnLocalListLoad);
            this.groupBox3.Location = new System.Drawing.Point(257, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.groupBox3.Size = new System.Drawing.Size(158, 108);
            this.groupBox3.TabIndex = 209;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Local PanelID List";
            // 
            // btnLocalListEditPass2
            // 
            this.btnLocalListEditPass2.AccessibleDescription = "Edit Pass2";
            this.btnLocalListEditPass2.Location = new System.Drawing.Point(82, 60);
            this.btnLocalListEditPass2.Name = "btnLocalListEditPass2";
            this.btnLocalListEditPass2.Size = new System.Drawing.Size(70, 30);
            this.btnLocalListEditPass2.TabIndex = 210;
            this.btnLocalListEditPass2.Text = "Edit Pass2";
            this.btnLocalListEditPass2.UseVisualStyleBackColor = true;
            this.btnLocalListEditPass2.Click += new System.EventHandler(this.btnEditPass2_Click);
            // 
            // btnLocalListSave
            // 
            this.btnLocalListSave.AccessibleDescription = "Save";
            this.btnLocalListSave.Location = new System.Drawing.Point(6, 60);
            this.btnLocalListSave.Name = "btnLocalListSave";
            this.btnLocalListSave.Size = new System.Drawing.Size(70, 30);
            this.btnLocalListSave.TabIndex = 207;
            this.btnLocalListSave.Text = "Save";
            this.btnLocalListSave.UseVisualStyleBackColor = true;
            this.btnLocalListSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLocalListEditPass1
            // 
            this.btnLocalListEditPass1.AccessibleDescription = "Edit Pass1";
            this.btnLocalListEditPass1.Location = new System.Drawing.Point(82, 24);
            this.btnLocalListEditPass1.Name = "btnLocalListEditPass1";
            this.btnLocalListEditPass1.Size = new System.Drawing.Size(70, 30);
            this.btnLocalListEditPass1.TabIndex = 209;
            this.btnLocalListEditPass1.Text = "Edit Pass1";
            this.btnLocalListEditPass1.UseVisualStyleBackColor = true;
            this.btnLocalListEditPass1.Click += new System.EventHandler(this.btnEditPass1_Click);
            // 
            // btnLocalListLoad
            // 
            this.btnLocalListLoad.AccessibleDescription = "Load";
            this.btnLocalListLoad.Location = new System.Drawing.Point(6, 24);
            this.btnLocalListLoad.Name = "btnLocalListLoad";
            this.btnLocalListLoad.Size = new System.Drawing.Size(70, 30);
            this.btnLocalListLoad.TabIndex = 205;
            this.btnLocalListLoad.Text = "Load";
            this.btnLocalListLoad.UseVisualStyleBackColor = true;
            this.btnLocalListLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label18
            // 
            this.label18.AccessibleDescription = "Output Path";
            this.label18.Location = new System.Drawing.Point(2, 32);
            this.label18.Margin = new System.Windows.Forms.Padding(2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(144, 23);
            this.label18.TabIndex = 162;
            this.label18.Text = "Output Path";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCheckOsramICCPath
            // 
            this.btnCheckOsramICCPath.AccessibleDescription = "Check";
            this.btnCheckOsramICCPath.Location = new System.Drawing.Point(520, 60);
            this.btnCheckOsramICCPath.Name = "btnCheckOsramICCPath";
            this.btnCheckOsramICCPath.Size = new System.Drawing.Size(70, 30);
            this.btnCheckOsramICCPath.TabIndex = 199;
            this.btnCheckOsramICCPath.Text = "Check";
            this.btnCheckOsramICCPath.UseVisualStyleBackColor = true;
            this.btnCheckOsramICCPath.Click += new System.EventHandler(this.btnCheckOsramICCPath_Click);
            // 
            // label21
            // 
            this.label21.AccessibleDescription = "Input Path";
            this.label21.Location = new System.Drawing.Point(2, 5);
            this.label21.Margin = new System.Windows.Forms.Padding(2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(144, 23);
            this.label21.TabIndex = 203;
            this.label21.Text = "Input Path";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbl_VolumeOfstProtocol);
            this.panel2.Controls.Add(this.btn_EditVolumeOfst);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 29);
            this.panel2.TabIndex = 207;
            // 
            // btn_EditVolumeOfst
            // 
            this.btn_EditVolumeOfst.Location = new System.Drawing.Point(373, 3);
            this.btn_EditVolumeOfst.Name = "btn_EditVolumeOfst";
            this.btn_EditVolumeOfst.Size = new System.Drawing.Size(75, 23);
            this.btn_EditVolumeOfst.TabIndex = 203;
            this.btn_EditVolumeOfst.Text = "Edit";
            this.btn_EditVolumeOfst.UseVisualStyleBackColor = true;
            this.btn_EditVolumeOfst.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // btnOsramICCTest
            // 
            this.btnOsramICCTest.AccessibleDescription = "Test";
            this.btnOsramICCTest.Location = new System.Drawing.Point(421, 128);
            this.btnOsramICCTest.Name = "btnOsramICCTest";
            this.btnOsramICCTest.Size = new System.Drawing.Size(70, 30);
            this.btnOsramICCTest.TabIndex = 215;
            this.btnOsramICCTest.Text = "Test";
            this.btnOsramICCTest.UseVisualStyleBackColor = true;
            this.btnOsramICCTest.Click += new System.EventHandler(this.btnOsramICCTest_Click);
            // 
            // frm_DispCore_DispSetup_Custom
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(613, 683);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_DispCore_DispSetup_Custom";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_DispCore_DispSetup_Custom";
            this.Load += new System.EventHandler(this.frm_DispCore_DispSetup_HeadCal_Load);
            this.VisibleChanged += new System.EventHandler(this.frm_DispCore_DispSetup_HeadCal_VisibleChanged);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnl_InputMapPaths_Lmd_EMap.ResumeLayout(false);
            this.pnl_InputMapPaths_Lmd_EMap.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlOsramICC.ResumeLayout(false);
            this.pnlOsramICC.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_VolumeOfstProtocol;
        private System.Windows.Forms.Timer tmr_Display;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_EditVolumeOfst;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_InputMap_DataPath;
        private System.Windows.Forms.Button btn_InputMap_CheckDataPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnl_InputMapPaths_Lmd_EMap;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TextBox tbox_Suffix;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_InputMap_Protocol;
        private System.Windows.Forms.TextBox tbox_Prefix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbox_Prefix3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbox_Suffix3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbl_InputMap_DataPath3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbox_Prefix2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbox_Suffix2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_InputMap_DataPath2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnInputMapSetup;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Panel pnlOsramICC;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnCheckOsramICCPath;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnLocalListSave;
        private System.Windows.Forms.Button btnLocalListLoad;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLocalListEditPass2;
        private System.Windows.Forms.Button btnLocalListEditPass1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnInputFileLoad;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnOutputFileTileLookUp;
        private System.Windows.Forms.Button btnOutputFileLoad;
        private System.Windows.Forms.TextBox tbxOsramICCOutputPath;
        private System.Windows.Forms.TextBox tbxOsramICCInputPath;
        private System.Windows.Forms.TextBox tbxLookUpTileID;
        private System.Windows.Forms.Button btnOsramICCTest;
    }
}