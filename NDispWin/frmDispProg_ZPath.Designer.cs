
namespace NDispWin
{
    partial class frmDispProg_ZPath
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
            this.gbox_Pos = new System.Windows.Forms.GroupBox();
            this.lblPointTL = new System.Windows.Forms.Label();
            this.pbZPathDot = new System.Windows.Forms.PictureBox();
            this.btnSetPtTL = new System.Windows.Forms.Button();
            this.btnSetPtBR = new System.Windows.Forms.Button();
            this.btnGotoPtBR = new System.Windows.Forms.Button();
            this.btnGotoPtTL = new System.Windows.Forms.Button();
            this.lblPointBR = new System.Windows.Forms.Label();
            this.lblStartGap = new System.Windows.Forms.Label();
            this.lbkStartGap = new System.Windows.Forms.Label();
            this.lblDispGap = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSpeed1 = new System.Windows.Forms.Label();
            this.cbDispense = new System.Windows.Forms.CheckBox();
            this.btnEditModel = new System.Windows.Forms.Button();
            this.l_lbl_ModelNo = new System.Windows.Forms.Label();
            this.lblModelNo = new System.Windows.Forms.Label();
            this.l_lbl_HeadNo = new System.Windows.Forms.Label();
            this.lblHeadNo = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lblSpeedF = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblEndGap = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblAD = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInitialSpeed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDownWait = new System.Windows.Forms.Label();
            this.lblPostWait = new System.Windows.Forms.Label();
            this.lblHead1DefVolume = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHead2DefVolume = new System.Windows.Forms.Label();
            this.lblRetGap = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDot1Pc = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBackSuck1 = new System.Windows.Forms.Label();
            this.lblBackSuck2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblHead2Volume = new System.Windows.Forms.Label();
            this.lblHead1Volume = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDot4Pc = new System.Windows.Forms.Label();
            this.lblDot3Pc = new System.Windows.Forms.Label();
            this.lblDot2Pc = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeed2Ratio = new System.Windows.Forms.Label();
            this.cbTailOff = new System.Windows.Forms.CheckBox();
            this.cbSquare = new System.Windows.Forms.CheckBox();
            this.gbox_Pos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbZPathDot)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbox_Pos
            // 
            this.gbox_Pos.AccessibleDescription = "Position";
            this.gbox_Pos.AutoSize = true;
            this.gbox_Pos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbox_Pos.Controls.Add(this.lblPointTL);
            this.gbox_Pos.Controls.Add(this.pbZPathDot);
            this.gbox_Pos.Controls.Add(this.btnSetPtTL);
            this.gbox_Pos.Controls.Add(this.btnSetPtBR);
            this.gbox_Pos.Controls.Add(this.btnGotoPtBR);
            this.gbox_Pos.Controls.Add(this.btnGotoPtTL);
            this.gbox_Pos.Controls.Add(this.lblPointBR);
            this.gbox_Pos.Location = new System.Drawing.Point(8, 63);
            this.gbox_Pos.Name = "gbox_Pos";
            this.gbox_Pos.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gbox_Pos.Size = new System.Drawing.Size(267, 312);
            this.gbox_Pos.TabIndex = 174;
            this.gbox_Pos.TabStop = false;
            this.gbox_Pos.Text = "Position";
            // 
            // lblPointTL
            // 
            this.lblPointTL.BackColor = System.Drawing.SystemColors.Window;
            this.lblPointTL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPointTL.Location = new System.Drawing.Point(5, 20);
            this.lblPointTL.Margin = new System.Windows.Forms.Padding(2);
            this.lblPointTL.Name = "lblPointTL";
            this.lblPointTL.Size = new System.Drawing.Size(120, 23);
            this.lblPointTL.TabIndex = 109;
            this.lblPointTL.Text = "-999.999, -999.999";
            this.lblPointTL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPointTL.Click += new System.EventHandler(this.lblPointTL_Click);
            // 
            // pbZPathDot
            // 
            this.pbZPathDot.Image = global::NDispWin.Properties.Resources.ZPathDot2;
            this.pbZPathDot.Location = new System.Drawing.Point(6, 48);
            this.pbZPathDot.Name = "pbZPathDot";
            this.pbZPathDot.Size = new System.Drawing.Size(255, 212);
            this.pbZPathDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbZPathDot.TabIndex = 188;
            this.pbZPathDot.TabStop = false;
            // 
            // btnSetPtTL
            // 
            this.btnSetPtTL.AccessibleDescription = "Set";
            this.btnSetPtTL.Location = new System.Drawing.Point(129, 16);
            this.btnSetPtTL.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetPtTL.Name = "btnSetPtTL";
            this.btnSetPtTL.Size = new System.Drawing.Size(40, 30);
            this.btnSetPtTL.TabIndex = 7;
            this.btnSetPtTL.Text = "Set";
            this.btnSetPtTL.UseVisualStyleBackColor = true;
            this.btnSetPtTL.Click += new System.EventHandler(this.btnSetPtTL_Click);
            // 
            // btnSetPtBR
            // 
            this.btnSetPtBR.AccessibleDescription = "Set";
            this.btnSetPtBR.Location = new System.Drawing.Point(167, 265);
            this.btnSetPtBR.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetPtBR.Name = "btnSetPtBR";
            this.btnSetPtBR.Size = new System.Drawing.Size(40, 30);
            this.btnSetPtBR.TabIndex = 7;
            this.btnSetPtBR.Text = "Set";
            this.btnSetPtBR.UseVisualStyleBackColor = true;
            this.btnSetPtBR.Click += new System.EventHandler(this.btnSetPtBR_Click);
            // 
            // btnGotoPtBR
            // 
            this.btnGotoPtBR.AccessibleDescription = "Goto";
            this.btnGotoPtBR.Location = new System.Drawing.Point(211, 265);
            this.btnGotoPtBR.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoPtBR.Name = "btnGotoPtBR";
            this.btnGotoPtBR.Size = new System.Drawing.Size(50, 30);
            this.btnGotoPtBR.TabIndex = 8;
            this.btnGotoPtBR.Text = "Goto";
            this.btnGotoPtBR.UseVisualStyleBackColor = true;
            this.btnGotoPtBR.Click += new System.EventHandler(this.btnGotoPtBR_Click);
            // 
            // btnGotoPtTL
            // 
            this.btnGotoPtTL.AccessibleDescription = "Goto";
            this.btnGotoPtTL.Location = new System.Drawing.Point(173, 16);
            this.btnGotoPtTL.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoPtTL.Name = "btnGotoPtTL";
            this.btnGotoPtTL.Size = new System.Drawing.Size(50, 30);
            this.btnGotoPtTL.TabIndex = 8;
            this.btnGotoPtTL.Text = "Goto";
            this.btnGotoPtTL.UseVisualStyleBackColor = true;
            this.btnGotoPtTL.Click += new System.EventHandler(this.btnGotoPtTL_Click);
            // 
            // lblPointBR
            // 
            this.lblPointBR.BackColor = System.Drawing.SystemColors.Window;
            this.lblPointBR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPointBR.Location = new System.Drawing.Point(43, 269);
            this.lblPointBR.Margin = new System.Windows.Forms.Padding(2);
            this.lblPointBR.Name = "lblPointBR";
            this.lblPointBR.Size = new System.Drawing.Size(120, 23);
            this.lblPointBR.TabIndex = 109;
            this.lblPointBR.Text = "-999.999, -999.999";
            this.lblPointBR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPointBR.Click += new System.EventHandler(this.lblPointBR_Click);
            // 
            // lblStartGap
            // 
            this.lblStartGap.BackColor = System.Drawing.SystemColors.Window;
            this.lblStartGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStartGap.Location = new System.Drawing.Point(411, 63);
            this.lblStartGap.Margin = new System.Windows.Forms.Padding(2);
            this.lblStartGap.Name = "lblStartGap";
            this.lblStartGap.Size = new System.Drawing.Size(75, 23);
            this.lblStartGap.TabIndex = 161;
            this.lblStartGap.Text = "-999.999";
            this.lblStartGap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStartGap.Click += new System.EventHandler(this.lblStartGap_Click);
            // 
            // lbkStartGap
            // 
            this.lbkStartGap.AccessibleDescription = "";
            this.lbkStartGap.Location = new System.Drawing.Point(281, 63);
            this.lbkStartGap.Margin = new System.Windows.Forms.Padding(2);
            this.lbkStartGap.Name = "lbkStartGap";
            this.lbkStartGap.Size = new System.Drawing.Size(126, 23);
            this.lbkStartGap.TabIndex = 160;
            this.lbkStartGap.Text = "Start Gap (mm)";
            this.lbkStartGap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDispGap
            // 
            this.lblDispGap.BackColor = System.Drawing.SystemColors.Window;
            this.lblDispGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDispGap.Location = new System.Drawing.Point(411, 90);
            this.lblDispGap.Margin = new System.Windows.Forms.Padding(2);
            this.lblDispGap.Name = "lblDispGap";
            this.lblDispGap.Size = new System.Drawing.Size(75, 23);
            this.lblDispGap.TabIndex = 155;
            this.lblDispGap.Text = "-999.999";
            this.lblDispGap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDispGap.Click += new System.EventHandler(this.lblDispGap_Click);
            // 
            // label14
            // 
            this.label14.AccessibleDescription = "";
            this.label14.Location = new System.Drawing.Point(281, 239);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 23);
            this.label14.TabIndex = 156;
            this.label14.Text = "Speed (mm/s)*";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpeed1
            // 
            this.lblSpeed1.BackColor = System.Drawing.SystemColors.Window;
            this.lblSpeed1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed1.Location = new System.Drawing.Point(411, 239);
            this.lblSpeed1.Margin = new System.Windows.Forms.Padding(2);
            this.lblSpeed1.Name = "lblSpeed1";
            this.lblSpeed1.Size = new System.Drawing.Size(75, 23);
            this.lblSpeed1.TabIndex = 157;
            this.lblSpeed1.Text = "-999.999";
            this.lblSpeed1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpeed1.Click += new System.EventHandler(this.lblSpeed_Click);
            // 
            // cbDispense
            // 
            this.cbDispense.AutoSize = true;
            this.cbDispense.Location = new System.Drawing.Point(166, 8);
            this.cbDispense.Name = "cbDispense";
            this.cbDispense.Padding = new System.Windows.Forms.Padding(3);
            this.cbDispense.Size = new System.Drawing.Size(80, 24);
            this.cbDispense.TabIndex = 176;
            this.cbDispense.Text = "Dispense";
            this.cbDispense.UseVisualStyleBackColor = true;
            this.cbDispense.Click += new System.EventHandler(this.cbDispense_Click);
            // 
            // btnEditModel
            // 
            this.btnEditModel.AccessibleDescription = "Edit";
            this.btnEditModel.Location = new System.Drawing.Point(165, 34);
            this.btnEditModel.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditModel.Name = "btnEditModel";
            this.btnEditModel.Size = new System.Drawing.Size(75, 23);
            this.btnEditModel.TabIndex = 170;
            this.btnEditModel.Text = "Edit";
            this.btnEditModel.UseVisualStyleBackColor = true;
            this.btnEditModel.Click += new System.EventHandler(this.btnEditModel_Click);
            // 
            // l_lbl_ModelNo
            // 
            this.l_lbl_ModelNo.AccessibleDescription = "Model No";
            this.l_lbl_ModelNo.Location = new System.Drawing.Point(7, 34);
            this.l_lbl_ModelNo.Margin = new System.Windows.Forms.Padding(2);
            this.l_lbl_ModelNo.Name = "l_lbl_ModelNo";
            this.l_lbl_ModelNo.Padding = new System.Windows.Forms.Padding(3);
            this.l_lbl_ModelNo.Size = new System.Drawing.Size(75, 23);
            this.l_lbl_ModelNo.TabIndex = 168;
            this.l_lbl_ModelNo.Text = "Model No";
            this.l_lbl_ModelNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModelNo
            // 
            this.lblModelNo.BackColor = System.Drawing.SystemColors.Window;
            this.lblModelNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblModelNo.Location = new System.Drawing.Point(86, 34);
            this.lblModelNo.Margin = new System.Windows.Forms.Padding(2);
            this.lblModelNo.Name = "lblModelNo";
            this.lblModelNo.Padding = new System.Windows.Forms.Padding(3);
            this.lblModelNo.Size = new System.Drawing.Size(75, 23);
            this.lblModelNo.TabIndex = 169;
            this.lblModelNo.Text = "lblModelNo";
            this.lblModelNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblModelNo.Click += new System.EventHandler(this.lblModelNo_Click);
            // 
            // l_lbl_HeadNo
            // 
            this.l_lbl_HeadNo.AccessibleDescription = "Head No";
            this.l_lbl_HeadNo.Location = new System.Drawing.Point(7, 7);
            this.l_lbl_HeadNo.Margin = new System.Windows.Forms.Padding(2);
            this.l_lbl_HeadNo.Name = "l_lbl_HeadNo";
            this.l_lbl_HeadNo.Padding = new System.Windows.Forms.Padding(3);
            this.l_lbl_HeadNo.Size = new System.Drawing.Size(75, 23);
            this.l_lbl_HeadNo.TabIndex = 166;
            this.l_lbl_HeadNo.Text = "Head No";
            this.l_lbl_HeadNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeadNo
            // 
            this.lblHeadNo.BackColor = System.Drawing.SystemColors.Window;
            this.lblHeadNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHeadNo.Location = new System.Drawing.Point(86, 7);
            this.lblHeadNo.Margin = new System.Windows.Forms.Padding(2);
            this.lblHeadNo.Name = "lblHeadNo";
            this.lblHeadNo.Padding = new System.Windows.Forms.Padding(3);
            this.lblHeadNo.Size = new System.Drawing.Size(75, 23);
            this.lblHeadNo.TabIndex = 167;
            this.lblHeadNo.Text = "lblHeadNo";
            this.lblHeadNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHeadNo.Click += new System.EventHandler(this.lblHeadNo_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.AccessibleDescription = "Cancel";
            this.btn_Cancel.Location = new System.Drawing.Point(411, 580);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Padding = new System.Windows.Forms.Padding(3);
            this.btn_Cancel.Size = new System.Drawing.Size(75, 36);
            this.btn_Cancel.TabIndex = 164;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.AccessibleDescription = "OK";
            this.btn_OK.Location = new System.Drawing.Point(332, 580);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Padding = new System.Windows.Forms.Padding(3);
            this.btn_OK.Size = new System.Drawing.Size(75, 36);
            this.btn_OK.TabIndex = 163;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lblSpeedF
            // 
            this.lblSpeedF.BackColor = System.Drawing.SystemColors.Window;
            this.lblSpeedF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeedF.Location = new System.Drawing.Point(411, 293);
            this.lblSpeedF.Margin = new System.Windows.Forms.Padding(2);
            this.lblSpeedF.Name = "lblSpeedF";
            this.lblSpeedF.Size = new System.Drawing.Size(75, 23);
            this.lblSpeedF.TabIndex = 157;
            this.lblSpeedF.Text = "-999.999";
            this.lblSpeedF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpeedF.Click += new System.EventHandler(this.lblSpeedF_Click);
            // 
            // label21
            // 
            this.label21.AccessibleDescription = "";
            this.label21.Location = new System.Drawing.Point(281, 293);
            this.label21.Margin = new System.Windows.Forms.Padding(2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(126, 23);
            this.label21.TabIndex = 156;
            this.label21.Text = "Speed3 (mm/s)*";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "";
            this.label7.Location = new System.Drawing.Point(281, 90);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 23);
            this.label7.TabIndex = 160;
            this.label7.Text = "Disp Gap (mm)*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEndGap
            // 
            this.lblEndGap.BackColor = System.Drawing.SystemColors.Window;
            this.lblEndGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEndGap.Location = new System.Drawing.Point(411, 117);
            this.lblEndGap.Margin = new System.Windows.Forms.Padding(2);
            this.lblEndGap.Name = "lblEndGap";
            this.lblEndGap.Size = new System.Drawing.Size(75, 23);
            this.lblEndGap.TabIndex = 178;
            this.lblEndGap.Text = "-999.999";
            this.lblEndGap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEndGap.Click += new System.EventHandler(this.lblEndGap_Click);
            // 
            // label26
            // 
            this.label26.AccessibleDescription = "";
            this.label26.Location = new System.Drawing.Point(281, 117);
            this.label26.Margin = new System.Windows.Forms.Padding(2);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(126, 23);
            this.label26.TabIndex = 179;
            this.label26.Text = "End Gap (mm)";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAD
            // 
            this.lblAD.BackColor = System.Drawing.SystemColors.Window;
            this.lblAD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAD.Location = new System.Drawing.Point(411, 185);
            this.lblAD.Margin = new System.Windows.Forms.Padding(2);
            this.lblAD.Name = "lblAD";
            this.lblAD.Size = new System.Drawing.Size(75, 23);
            this.lblAD.TabIndex = 180;
            this.lblAD.Text = "-999.999";
            this.lblAD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAD.Click += new System.EventHandler(this.lblAD_Click);
            // 
            // label27
            // 
            this.label27.AccessibleDescription = "";
            this.label27.Location = new System.Drawing.Point(281, 185);
            this.label27.Margin = new System.Windows.Forms.Padding(2);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(126, 23);
            this.label27.TabIndex = 181;
            this.label27.Text = "AccelDecel (mm/s3)*";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.Location = new System.Drawing.Point(281, 212);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 23);
            this.label3.TabIndex = 182;
            this.label3.Text = "Initial Speed (mm/s)*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Visible = false;
            // 
            // lblInitialSpeed
            // 
            this.lblInitialSpeed.BackColor = System.Drawing.SystemColors.Window;
            this.lblInitialSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInitialSpeed.Location = new System.Drawing.Point(411, 212);
            this.lblInitialSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.lblInitialSpeed.Name = "lblInitialSpeed";
            this.lblInitialSpeed.Size = new System.Drawing.Size(75, 23);
            this.lblInitialSpeed.TabIndex = 183;
            this.lblInitialSpeed.Text = "-999.999";
            this.lblInitialSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblInitialSpeed.Visible = false;
            this.lblInitialSpeed.Click += new System.EventHandler(this.lblInitialSpeed_Click);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "";
            this.label5.Location = new System.Drawing.Point(280, 335);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 23);
            this.label5.TabIndex = 184;
            this.label5.Text = "Down Wait (ms)*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "";
            this.label9.Location = new System.Drawing.Point(280, 362);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 23);
            this.label9.TabIndex = 185;
            this.label9.Text = "Post Wait (ms)*";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDownWait
            // 
            this.lblDownWait.BackColor = System.Drawing.SystemColors.Window;
            this.lblDownWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDownWait.Location = new System.Drawing.Point(410, 335);
            this.lblDownWait.Margin = new System.Windows.Forms.Padding(2);
            this.lblDownWait.Name = "lblDownWait";
            this.lblDownWait.Size = new System.Drawing.Size(75, 23);
            this.lblDownWait.TabIndex = 186;
            this.lblDownWait.Text = "-999.999";
            this.lblDownWait.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDownWait.Click += new System.EventHandler(this.lblDownWait_Click);
            // 
            // lblPostWait
            // 
            this.lblPostWait.BackColor = System.Drawing.SystemColors.Window;
            this.lblPostWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPostWait.Location = new System.Drawing.Point(410, 362);
            this.lblPostWait.Margin = new System.Windows.Forms.Padding(2);
            this.lblPostWait.Name = "lblPostWait";
            this.lblPostWait.Size = new System.Drawing.Size(75, 23);
            this.lblPostWait.TabIndex = 187;
            this.lblPostWait.Text = "-999.999";
            this.lblPostWait.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPostWait.Click += new System.EventHandler(this.lblPostWait_Click);
            // 
            // lblHead1DefVolume
            // 
            this.lblHead1DefVolume.BackColor = System.Drawing.SystemColors.Window;
            this.lblHead1DefVolume.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHead1DefVolume.Location = new System.Drawing.Point(186, 44);
            this.lblHead1DefVolume.Margin = new System.Windows.Forms.Padding(2);
            this.lblHead1DefVolume.Name = "lblHead1DefVolume";
            this.lblHead1DefVolume.Size = new System.Drawing.Size(75, 23);
            this.lblHead1DefVolume.TabIndex = 189;
            this.lblHead1DefVolume.Text = "-999.999";
            this.lblHead1DefVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHead1DefVolume.Click += new System.EventHandler(this.lblHead1DefVolume_Click);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.Location = new System.Drawing.Point(186, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 23);
            this.label2.TabIndex = 190;
            this.label2.Text = "Head 1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "";
            this.label4.Location = new System.Drawing.Point(106, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 23);
            this.label4.TabIndex = 192;
            this.label4.Text = "Head2";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHead2DefVolume
            // 
            this.lblHead2DefVolume.BackColor = System.Drawing.SystemColors.Window;
            this.lblHead2DefVolume.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHead2DefVolume.Location = new System.Drawing.Point(107, 44);
            this.lblHead2DefVolume.Margin = new System.Windows.Forms.Padding(2);
            this.lblHead2DefVolume.Name = "lblHead2DefVolume";
            this.lblHead2DefVolume.Size = new System.Drawing.Size(75, 23);
            this.lblHead2DefVolume.TabIndex = 191;
            this.lblHead2DefVolume.Text = "-999.999";
            this.lblHead2DefVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHead2DefVolume.Click += new System.EventHandler(this.lblHead2DefVolume_Click);
            // 
            // lblRetGap
            // 
            this.lblRetGap.BackColor = System.Drawing.SystemColors.Window;
            this.lblRetGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRetGap.Location = new System.Drawing.Point(411, 144);
            this.lblRetGap.Margin = new System.Windows.Forms.Padding(2);
            this.lblRetGap.Name = "lblRetGap";
            this.lblRetGap.Size = new System.Drawing.Size(75, 23);
            this.lblRetGap.TabIndex = 193;
            this.lblRetGap.Text = "-999.999";
            this.lblRetGap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRetGap.Click += new System.EventHandler(this.lblRetGap_Click);
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "";
            this.label6.Location = new System.Drawing.Point(281, 144);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 23);
            this.label6.TabIndex = 194;
            this.label6.Text = "Ret Gap (mm)*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDot1Pc
            // 
            this.lblDot1Pc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDot1Pc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDot1Pc.Location = new System.Drawing.Point(394, 20);
            this.lblDot1Pc.Margin = new System.Windows.Forms.Padding(2);
            this.lblDot1Pc.Name = "lblDot1Pc";
            this.lblDot1Pc.Size = new System.Drawing.Size(75, 23);
            this.lblDot1Pc.TabIndex = 197;
            this.lblDot1Pc.Text = "-999.999";
            this.lblDot1Pc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDot1Pc.Click += new System.EventHandler(this.lblDot1Pc_Click);
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "";
            this.label10.Location = new System.Drawing.Point(307, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 23);
            this.label10.TabIndex = 198;
            this.label10.Text = "D1, Dot 1 (%)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lblBackSuck1);
            this.groupBox1.Controls.Add(this.lblBackSuck2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblHead2Volume);
            this.groupBox1.Controls.Add(this.lblHead1Volume);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblDot4Pc);
            this.groupBox1.Controls.Add(this.lblDot3Pc);
            this.groupBox1.Controls.Add(this.lblDot2Pc);
            this.groupBox1.Controls.Add(this.lblDot1Pc);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.lblHead2DefVolume);
            this.groupBox1.Controls.Add(this.lblHead1DefVolume);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(8, 434);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 144);
            this.groupBox1.TabIndex = 199;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volume";
            // 
            // lblBackSuck1
            // 
            this.lblBackSuck1.AccessibleDescription = "";
            this.lblBackSuck1.BackColor = System.Drawing.SystemColors.Window;
            this.lblBackSuck1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBackSuck1.Location = new System.Drawing.Point(185, 98);
            this.lblBackSuck1.Margin = new System.Windows.Forms.Padding(2);
            this.lblBackSuck1.Name = "lblBackSuck1";
            this.lblBackSuck1.Size = new System.Drawing.Size(76, 23);
            this.lblBackSuck1.TabIndex = 211;
            this.lblBackSuck1.Text = "0.000";
            this.lblBackSuck1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBackSuck1.Click += new System.EventHandler(this.lblBackSuck1_Click);
            // 
            // lblBackSuck2
            // 
            this.lblBackSuck2.AccessibleDescription = "";
            this.lblBackSuck2.BackColor = System.Drawing.SystemColors.Window;
            this.lblBackSuck2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBackSuck2.Location = new System.Drawing.Point(106, 98);
            this.lblBackSuck2.Margin = new System.Windows.Forms.Padding(2);
            this.lblBackSuck2.Name = "lblBackSuck2";
            this.lblBackSuck2.Size = new System.Drawing.Size(76, 23);
            this.lblBackSuck2.TabIndex = 210;
            this.lblBackSuck2.Text = "0.000";
            this.lblBackSuck2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBackSuck2.Click += new System.EventHandler(this.lblBackSuck2_Click);
            // 
            // label13
            // 
            this.label13.AccessibleDescription = "";
            this.label13.Location = new System.Drawing.Point(5, 98);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 23);
            this.label13.TabIndex = 209;
            this.label13.Text = "BackSuck (ul)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHead2Volume
            // 
            this.lblHead2Volume.BackColor = System.Drawing.SystemColors.Window;
            this.lblHead2Volume.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHead2Volume.Location = new System.Drawing.Point(107, 71);
            this.lblHead2Volume.Margin = new System.Windows.Forms.Padding(2);
            this.lblHead2Volume.Name = "lblHead2Volume";
            this.lblHead2Volume.Size = new System.Drawing.Size(75, 23);
            this.lblHead2Volume.TabIndex = 208;
            this.lblHead2Volume.Text = "-999.999";
            this.lblHead2Volume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHead2Volume.Click += new System.EventHandler(this.lblHead2Volume_Click);
            // 
            // lblHead1Volume
            // 
            this.lblHead1Volume.BackColor = System.Drawing.SystemColors.Window;
            this.lblHead1Volume.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHead1Volume.Location = new System.Drawing.Point(186, 71);
            this.lblHead1Volume.Margin = new System.Windows.Forms.Padding(2);
            this.lblHead1Volume.Name = "lblHead1Volume";
            this.lblHead1Volume.Size = new System.Drawing.Size(75, 23);
            this.lblHead1Volume.TabIndex = 207;
            this.lblHead1Volume.Text = "-999.999";
            this.lblHead1Volume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHead1Volume.Click += new System.EventHandler(this.lblHead1Volume_Click);
            // 
            // label12
            // 
            this.label12.AccessibleDescription = "";
            this.label12.Location = new System.Drawing.Point(5, 71);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 23);
            this.label12.TabIndex = 206;
            this.label12.Text = "Current Nett (ul)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AccessibleDescription = "";
            this.label11.Location = new System.Drawing.Point(5, 44);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 23);
            this.label11.TabIndex = 205;
            this.label11.Text = "Default Nett (ul)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDot4Pc
            // 
            this.lblDot4Pc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDot4Pc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDot4Pc.Location = new System.Drawing.Point(394, 101);
            this.lblDot4Pc.Margin = new System.Windows.Forms.Padding(2);
            this.lblDot4Pc.Name = "lblDot4Pc";
            this.lblDot4Pc.Size = new System.Drawing.Size(75, 23);
            this.lblDot4Pc.TabIndex = 203;
            this.lblDot4Pc.Text = "-999.999";
            this.lblDot4Pc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDot4Pc.Click += new System.EventHandler(this.lblDot4Pc_Click);
            // 
            // lblDot3Pc
            // 
            this.lblDot3Pc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDot3Pc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDot3Pc.Location = new System.Drawing.Point(394, 74);
            this.lblDot3Pc.Margin = new System.Windows.Forms.Padding(2);
            this.lblDot3Pc.Name = "lblDot3Pc";
            this.lblDot3Pc.Size = new System.Drawing.Size(75, 23);
            this.lblDot3Pc.TabIndex = 201;
            this.lblDot3Pc.Text = "-999.999";
            this.lblDot3Pc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDot3Pc.Click += new System.EventHandler(this.lblDot3Pc_Click);
            // 
            // lblDot2Pc
            // 
            this.lblDot2Pc.BackColor = System.Drawing.SystemColors.Window;
            this.lblDot2Pc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDot2Pc.Location = new System.Drawing.Point(394, 47);
            this.lblDot2Pc.Margin = new System.Windows.Forms.Padding(2);
            this.lblDot2Pc.Name = "lblDot2Pc";
            this.lblDot2Pc.Size = new System.Drawing.Size(75, 23);
            this.lblDot2Pc.TabIndex = 199;
            this.lblDot2Pc.Text = "-999.999";
            this.lblDot2Pc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDot2Pc.Click += new System.EventHandler(this.lblDot2Pc_Click);
            // 
            // label17
            // 
            this.label17.AccessibleDescription = "";
            this.label17.Location = new System.Drawing.Point(307, 101);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 23);
            this.label17.TabIndex = 204;
            this.label17.Text = "D4, Dot 4 (%)";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AccessibleDescription = "";
            this.label15.Location = new System.Drawing.Point(307, 74);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 23);
            this.label15.TabIndex = 202;
            this.label15.Text = "D3, Dot 3 (%)";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "";
            this.label8.Location = new System.Drawing.Point(307, 47);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 23);
            this.label8.TabIndex = 200;
            this.label8.Text = "D2, Dot 2 (%)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "";
            this.label1.Location = new System.Drawing.Point(281, 266);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 23);
            this.label1.TabIndex = 200;
            this.label1.Text = "Speed 2 Ratio";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpeed2Ratio
            // 
            this.lblSpeed2Ratio.BackColor = System.Drawing.SystemColors.Window;
            this.lblSpeed2Ratio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed2Ratio.Location = new System.Drawing.Point(411, 266);
            this.lblSpeed2Ratio.Margin = new System.Windows.Forms.Padding(2);
            this.lblSpeed2Ratio.Name = "lblSpeed2Ratio";
            this.lblSpeed2Ratio.Size = new System.Drawing.Size(75, 23);
            this.lblSpeed2Ratio.TabIndex = 203;
            this.lblSpeed2Ratio.Text = "100";
            this.lblSpeed2Ratio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpeed2Ratio.Click += new System.EventHandler(this.lblSpeed2Ratio_Click);
            // 
            // cbTailOff
            // 
            this.cbTailOff.AutoSize = true;
            this.cbTailOff.Location = new System.Drawing.Point(283, 390);
            this.cbTailOff.Name = "cbTailOff";
            this.cbTailOff.Size = new System.Drawing.Size(65, 18);
            this.cbTailOff.TabIndex = 204;
            this.cbTailOff.Text = "Tail Off";
            this.cbTailOff.UseVisualStyleBackColor = true;
            this.cbTailOff.Click += new System.EventHandler(this.cbTailOff_Click);
            // 
            // cbSquare
            // 
            this.cbSquare.AutoSize = true;
            this.cbSquare.Location = new System.Drawing.Point(283, 410);
            this.cbSquare.Name = "cbSquare";
            this.cbSquare.Size = new System.Drawing.Size(64, 18);
            this.cbSquare.TabIndex = 205;
            this.cbSquare.Text = "Square";
            this.cbSquare.UseVisualStyleBackColor = true;
            this.cbSquare.CheckedChanged += new System.EventHandler(this.cbSquare_CheckedChanged);
            this.cbSquare.Click += new System.EventHandler(this.cbSquare_Click);
            // 
            // frmDispProg_ZPath
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(492, 623);
            this.ControlBox = false;
            this.Controls.Add(this.cbSquare);
            this.Controls.Add(this.cbTailOff);
            this.Controls.Add(this.lblSpeed2Ratio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblRetGap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblDownWait);
            this.Controls.Add(this.lblPostWait);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblInitialSpeed);
            this.Controls.Add(this.lblAD);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.lblEndGap);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lblDispGap);
            this.Controls.Add(this.gbox_Pos);
            this.Controls.Add(this.cbDispense);
            this.Controls.Add(this.lblStartGap);
            this.Controls.Add(this.lbkStartGap);
            this.Controls.Add(this.btnEditModel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.l_lbl_ModelNo);
            this.Controls.Add(this.lblModelNo);
            this.Controls.Add(this.l_lbl_HeadNo);
            this.Controls.Add(this.lblHeadNo);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblSpeedF);
            this.Controls.Add(this.lblSpeed1);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDispProg_ZPath";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmDispProg_ZPath";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDispProg_ZPath_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDispProg_ZPath_FormClosed);
            this.Load += new System.EventHandler(this.frmDispProg_ZPath_Load);
            this.gbox_Pos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbZPathDot)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbox_Pos;
        private System.Windows.Forms.Label lblPointTL;
        private System.Windows.Forms.Button btnSetPtTL;
        private System.Windows.Forms.Button btnGotoPtTL;
        private System.Windows.Forms.Label lblStartGap;
        private System.Windows.Forms.Label lbkStartGap;
        private System.Windows.Forms.Label lblDispGap;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSpeed1;
        private System.Windows.Forms.CheckBox cbDispense;
        private System.Windows.Forms.Button btnEditModel;
        private System.Windows.Forms.Label l_lbl_ModelNo;
        private System.Windows.Forms.Label lblModelNo;
        private System.Windows.Forms.Label l_lbl_HeadNo;
        private System.Windows.Forms.Label lblHeadNo;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label lblPointBR;
        private System.Windows.Forms.Button btnSetPtBR;
        private System.Windows.Forms.Button btnGotoPtBR;
        private System.Windows.Forms.Label lblSpeedF;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblEndGap;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblAD;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInitialSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDownWait;
        private System.Windows.Forms.Label lblPostWait;
        private System.Windows.Forms.Label lblHead1DefVolume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHead2DefVolume;
        private System.Windows.Forms.Label lblRetGap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDot1Pc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblDot4Pc;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblDot3Pc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDot2Pc;
        private System.Windows.Forms.Label lblHead2Volume;
        private System.Windows.Forms.Label lblHead1Volume;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblBackSuck1;
        private System.Windows.Forms.Label lblBackSuck2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeed2Ratio;
        private System.Windows.Forms.PictureBox pbZPathDot;
        private System.Windows.Forms.CheckBox cbTailOff;
        private System.Windows.Forms.CheckBox cbSquare;
    }
}