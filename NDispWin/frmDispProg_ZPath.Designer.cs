
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetPtTL = new System.Windows.Forms.Button();
            this.btnGotoPtTL = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPointBR = new System.Windows.Forms.Label();
            this.btnGotoPtBR = new System.Windows.Forms.Button();
            this.btnSetPtBR = new System.Windows.Forms.Button();
            this.lblStartGap = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblDispGap = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSpeed1 = new System.Windows.Forms.Label();
            this.cbDispense = new System.Windows.Forms.CheckBox();
            this.cbEnableWeight = new System.Windows.Forms.CheckBox();
            this.btnEditModel = new System.Windows.Forms.Button();
            this.l_lbl_ModelNo = new System.Windows.Forms.Label();
            this.lblModelNo = new System.Windows.Forms.Label();
            this.l_lbl_HeadNo = new System.Windows.Forms.Label();
            this.lblHeadNo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCutTailType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCutTailHeight = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCutTailSpeed = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCutTailLength = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lblSpeed2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSpeedF = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStartLength = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblEndLength = new System.Windows.Forms.Label();
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
            this.gbox_Pos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbox_Pos
            // 
            this.gbox_Pos.AccessibleDescription = "Position";
            this.gbox_Pos.AutoSize = true;
            this.gbox_Pos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbox_Pos.Controls.Add(this.lblPointTL);
            this.gbox_Pos.Controls.Add(this.label1);
            this.gbox_Pos.Controls.Add(this.btnSetPtTL);
            this.gbox_Pos.Controls.Add(this.btnGotoPtTL);
            this.gbox_Pos.Controls.Add(this.label2);
            this.gbox_Pos.Controls.Add(this.lblPointBR);
            this.gbox_Pos.Controls.Add(this.btnGotoPtBR);
            this.gbox_Pos.Controls.Add(this.btnSetPtBR);
            this.gbox_Pos.Location = new System.Drawing.Point(8, 63);
            this.gbox_Pos.Name = "gbox_Pos";
            this.gbox_Pos.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gbox_Pos.Size = new System.Drawing.Size(268, 266);
            this.gbox_Pos.TabIndex = 174;
            this.gbox_Pos.TabStop = false;
            this.gbox_Pos.Text = "Position";
            // 
            // lblPointTL
            // 
            this.lblPointTL.BackColor = System.Drawing.SystemColors.Window;
            this.lblPointTL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPointTL.Location = new System.Drawing.Point(5, 47);
            this.lblPointTL.Margin = new System.Windows.Forms.Padding(2);
            this.lblPointTL.Name = "lblPointTL";
            this.lblPointTL.Size = new System.Drawing.Size(120, 23);
            this.lblPointTL.TabIndex = 109;
            this.lblPointTL.Text = "-999.999, -999.999";
            this.lblPointTL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPointTL.Click += new System.EventHandler(this.lblPointTL_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "";
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 23);
            this.label1.TabIndex = 133;
            this.label1.Text = "Point TL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSetPtTL
            // 
            this.btnSetPtTL.AccessibleDescription = "Set";
            this.btnSetPtTL.Location = new System.Drawing.Point(31, 74);
            this.btnSetPtTL.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetPtTL.Name = "btnSetPtTL";
            this.btnSetPtTL.Size = new System.Drawing.Size(40, 30);
            this.btnSetPtTL.TabIndex = 7;
            this.btnSetPtTL.Text = "Set";
            this.btnSetPtTL.UseVisualStyleBackColor = true;
            this.btnSetPtTL.Click += new System.EventHandler(this.btnSetPtTL_Click);
            // 
            // btnGotoPtTL
            // 
            this.btnGotoPtTL.AccessibleDescription = "Goto";
            this.btnGotoPtTL.Location = new System.Drawing.Point(75, 74);
            this.btnGotoPtTL.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoPtTL.Name = "btnGotoPtTL";
            this.btnGotoPtTL.Size = new System.Drawing.Size(50, 30);
            this.btnGotoPtTL.TabIndex = 8;
            this.btnGotoPtTL.Text = "Goto";
            this.btnGotoPtTL.UseVisualStyleBackColor = true;
            this.btnGotoPtTL.Click += new System.EventHandler(this.btnGotoPtTL_Click);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.Location = new System.Drawing.Point(140, 169);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 23);
            this.label2.TabIndex = 133;
            this.label2.Text = "Point BR";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPointBR
            // 
            this.lblPointBR.BackColor = System.Drawing.SystemColors.Window;
            this.lblPointBR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPointBR.Location = new System.Drawing.Point(143, 192);
            this.lblPointBR.Margin = new System.Windows.Forms.Padding(2);
            this.lblPointBR.Name = "lblPointBR";
            this.lblPointBR.Size = new System.Drawing.Size(120, 23);
            this.lblPointBR.TabIndex = 109;
            this.lblPointBR.Text = "-999.999, -999.999";
            this.lblPointBR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPointBR.Click += new System.EventHandler(this.lblPointBR_Click);
            // 
            // btnGotoPtBR
            // 
            this.btnGotoPtBR.AccessibleDescription = "Goto";
            this.btnGotoPtBR.Location = new System.Drawing.Point(213, 219);
            this.btnGotoPtBR.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoPtBR.Name = "btnGotoPtBR";
            this.btnGotoPtBR.Size = new System.Drawing.Size(50, 30);
            this.btnGotoPtBR.TabIndex = 8;
            this.btnGotoPtBR.Text = "Goto";
            this.btnGotoPtBR.UseVisualStyleBackColor = true;
            this.btnGotoPtBR.Click += new System.EventHandler(this.btnGotoPtBR_Click);
            // 
            // btnSetPtBR
            // 
            this.btnSetPtBR.AccessibleDescription = "Set";
            this.btnSetPtBR.Location = new System.Drawing.Point(169, 219);
            this.btnSetPtBR.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetPtBR.Name = "btnSetPtBR";
            this.btnSetPtBR.Size = new System.Drawing.Size(40, 30);
            this.btnSetPtBR.TabIndex = 7;
            this.btnSetPtBR.Text = "Set";
            this.btnSetPtBR.UseVisualStyleBackColor = true;
            this.btnSetPtBR.Click += new System.EventHandler(this.btnSetPtBR_Click);
            // 
            // lblStartGap
            // 
            this.lblStartGap.BackColor = System.Drawing.SystemColors.Window;
            this.lblStartGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStartGap.Location = new System.Drawing.Point(411, 128);
            this.lblStartGap.Margin = new System.Windows.Forms.Padding(2);
            this.lblStartGap.Name = "lblStartGap";
            this.lblStartGap.Size = new System.Drawing.Size(75, 23);
            this.lblStartGap.TabIndex = 161;
            this.lblStartGap.Text = "-999.999";
            this.lblStartGap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStartGap.Click += new System.EventHandler(this.lblStartGap_Click);
            // 
            // label13
            // 
            this.label13.AccessibleDescription = "";
            this.label13.Location = new System.Drawing.Point(281, 128);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 23);
            this.label13.TabIndex = 160;
            this.label13.Text = "Start Gap (mm)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AccessibleDescription = "";
            this.label12.Location = new System.Drawing.Point(281, 63);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 23);
            this.label12.TabIndex = 154;
            this.label12.Text = "Start Length (mm)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDispGap
            // 
            this.lblDispGap.BackColor = System.Drawing.SystemColors.Window;
            this.lblDispGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDispGap.Location = new System.Drawing.Point(411, 155);
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
            this.label14.Location = new System.Drawing.Point(281, 273);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 23);
            this.label14.TabIndex = 156;
            this.label14.Text = "Speed (mm/s)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpeed1
            // 
            this.lblSpeed1.BackColor = System.Drawing.SystemColors.Window;
            this.lblSpeed1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed1.Location = new System.Drawing.Point(411, 273);
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
            // cbEnableWeight
            // 
            this.cbEnableWeight.AutoSize = true;
            this.cbEnableWeight.Location = new System.Drawing.Point(250, 33);
            this.cbEnableWeight.Name = "cbEnableWeight";
            this.cbEnableWeight.Padding = new System.Windows.Forms.Padding(3);
            this.cbEnableWeight.Size = new System.Drawing.Size(96, 24);
            this.cbEnableWeight.TabIndex = 173;
            this.cbEnableWeight.Text = "Use Weight";
            this.cbEnableWeight.UseVisualStyleBackColor = true;
            this.cbEnableWeight.Click += new System.EventHandler(this.cbEnableWeight_Click);
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
            this.l_lbl_HeadNo.Visible = false;
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
            this.lblHeadNo.Visible = false;
            this.lblHeadNo.Click += new System.EventHandler(this.lblHeadNo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.lblCutTailType);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblCutTailHeight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblCutTailSpeed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblCutTailLength);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(8, 544);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox1.Size = new System.Drawing.Size(428, 86);
            this.groupBox1.TabIndex = 165;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CutTail";
            // 
            // lblCutTailType
            // 
            this.lblCutTailType.BackColor = System.Drawing.SystemColors.Window;
            this.lblCutTailType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCutTailType.Location = new System.Drawing.Point(348, 46);
            this.lblCutTailType.Margin = new System.Windows.Forms.Padding(2);
            this.lblCutTailType.Name = "lblCutTailType";
            this.lblCutTailType.Size = new System.Drawing.Size(75, 23);
            this.lblCutTailType.TabIndex = 135;
            this.lblCutTailType.Text = "-999.999";
            this.lblCutTailType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "";
            this.label10.Location = new System.Drawing.Point(218, 46);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 23);
            this.label10.TabIndex = 134;
            this.label10.Text = "Type";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCutTailHeight
            // 
            this.lblCutTailHeight.BackColor = System.Drawing.SystemColors.Window;
            this.lblCutTailHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCutTailHeight.Location = new System.Drawing.Point(348, 19);
            this.lblCutTailHeight.Margin = new System.Windows.Forms.Padding(2);
            this.lblCutTailHeight.Name = "lblCutTailHeight";
            this.lblCutTailHeight.Size = new System.Drawing.Size(75, 23);
            this.lblCutTailHeight.TabIndex = 133;
            this.lblCutTailHeight.Text = "-999.999";
            this.lblCutTailHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "";
            this.label8.Location = new System.Drawing.Point(218, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 23);
            this.label8.TabIndex = 132;
            this.label8.Text = "Height (mm)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCutTailSpeed
            // 
            this.lblCutTailSpeed.BackColor = System.Drawing.SystemColors.Window;
            this.lblCutTailSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCutTailSpeed.Location = new System.Drawing.Point(132, 46);
            this.lblCutTailSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.lblCutTailSpeed.Name = "lblCutTailSpeed";
            this.lblCutTailSpeed.Size = new System.Drawing.Size(75, 23);
            this.lblCutTailSpeed.TabIndex = 131;
            this.lblCutTailSpeed.Text = "-999.999";
            this.lblCutTailSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "";
            this.label6.Location = new System.Drawing.Point(4, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 23);
            this.label6.TabIndex = 130;
            this.label6.Text = "Speed (mm/2)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCutTailLength
            // 
            this.lblCutTailLength.BackColor = System.Drawing.SystemColors.Window;
            this.lblCutTailLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCutTailLength.Location = new System.Drawing.Point(132, 19);
            this.lblCutTailLength.Margin = new System.Windows.Forms.Padding(2);
            this.lblCutTailLength.Name = "lblCutTailLength";
            this.lblCutTailLength.Size = new System.Drawing.Size(75, 23);
            this.lblCutTailLength.TabIndex = 129;
            this.lblCutTailLength.Text = "-999.999";
            this.lblCutTailLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "";
            this.label4.Location = new System.Drawing.Point(4, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 23);
            this.label4.TabIndex = 128;
            this.label4.Text = "Length (mm)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.AccessibleDescription = "Cancel";
            this.btn_Cancel.Location = new System.Drawing.Point(411, 492);
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
            this.btn_OK.Location = new System.Drawing.Point(332, 492);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Padding = new System.Windows.Forms.Padding(3);
            this.btn_OK.Size = new System.Drawing.Size(75, 36);
            this.btn_OK.TabIndex = 163;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lblSpeed2
            // 
            this.lblSpeed2.BackColor = System.Drawing.SystemColors.Window;
            this.lblSpeed2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed2.Location = new System.Drawing.Point(411, 300);
            this.lblSpeed2.Margin = new System.Windows.Forms.Padding(2);
            this.lblSpeed2.Name = "lblSpeed2";
            this.lblSpeed2.Size = new System.Drawing.Size(75, 23);
            this.lblSpeed2.TabIndex = 157;
            this.lblSpeed2.Text = "-999.999";
            this.lblSpeed2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpeed2.Click += new System.EventHandler(this.lblSpeed2_Click);
            // 
            // label11
            // 
            this.label11.AccessibleDescription = "";
            this.label11.Location = new System.Drawing.Point(281, 300);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 23);
            this.label11.TabIndex = 156;
            this.label11.Text = "Speed2 (mm/s)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpeedF
            // 
            this.lblSpeedF.BackColor = System.Drawing.SystemColors.Window;
            this.lblSpeedF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeedF.Location = new System.Drawing.Point(411, 327);
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
            this.label21.Location = new System.Drawing.Point(281, 327);
            this.label21.Margin = new System.Windows.Forms.Padding(2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(126, 23);
            this.label21.TabIndex = 156;
            this.label21.Text = "SpeedF (mm/s)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "";
            this.label7.Location = new System.Drawing.Point(281, 155);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 23);
            this.label7.TabIndex = 160;
            this.label7.Text = "Disp Gap (mm)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartLength
            // 
            this.lblStartLength.BackColor = System.Drawing.SystemColors.Window;
            this.lblStartLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStartLength.Location = new System.Drawing.Point(411, 63);
            this.lblStartLength.Margin = new System.Windows.Forms.Padding(2);
            this.lblStartLength.Name = "lblStartLength";
            this.lblStartLength.Size = new System.Drawing.Size(75, 23);
            this.lblStartLength.TabIndex = 168;
            this.lblStartLength.Text = "-999.999";
            this.lblStartLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStartLength.Click += new System.EventHandler(this.lblStartLength_Click);
            // 
            // label22
            // 
            this.label22.AccessibleDescription = "";
            this.label22.Location = new System.Drawing.Point(281, 90);
            this.label22.Margin = new System.Windows.Forms.Padding(2);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(126, 23);
            this.label22.TabIndex = 154;
            this.label22.Text = "End Length (mm)";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEndLength
            // 
            this.lblEndLength.BackColor = System.Drawing.SystemColors.Window;
            this.lblEndLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEndLength.Location = new System.Drawing.Point(411, 90);
            this.lblEndLength.Margin = new System.Windows.Forms.Padding(2);
            this.lblEndLength.Name = "lblEndLength";
            this.lblEndLength.Size = new System.Drawing.Size(75, 23);
            this.lblEndLength.TabIndex = 168;
            this.lblEndLength.Text = "-999.999";
            this.lblEndLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEndLength.Click += new System.EventHandler(this.lblEndLength_Click);
            // 
            // lblEndGap
            // 
            this.lblEndGap.BackColor = System.Drawing.SystemColors.Window;
            this.lblEndGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEndGap.Location = new System.Drawing.Point(411, 182);
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
            this.label26.Location = new System.Drawing.Point(281, 182);
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
            this.lblAD.Location = new System.Drawing.Point(411, 219);
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
            this.label27.Location = new System.Drawing.Point(281, 219);
            this.label27.Margin = new System.Windows.Forms.Padding(2);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(126, 23);
            this.label27.TabIndex = 181;
            this.label27.Text = "AccelDecel (mm/s3)";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.Location = new System.Drawing.Point(281, 246);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 23);
            this.label3.TabIndex = 182;
            this.label3.Text = "Initial Speed (mm/s)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInitialSpeed
            // 
            this.lblInitialSpeed.BackColor = System.Drawing.SystemColors.Window;
            this.lblInitialSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInitialSpeed.Location = new System.Drawing.Point(411, 246);
            this.lblInitialSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.lblInitialSpeed.Name = "lblInitialSpeed";
            this.lblInitialSpeed.Size = new System.Drawing.Size(75, 23);
            this.lblInitialSpeed.TabIndex = 183;
            this.lblInitialSpeed.Text = "-999.999";
            this.lblInitialSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblInitialSpeed.Click += new System.EventHandler(this.lblInitialSpeed_Click);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "";
            this.label5.Location = new System.Drawing.Point(281, 371);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 23);
            this.label5.TabIndex = 184;
            this.label5.Text = "Down Wait (ms)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "";
            this.label9.Location = new System.Drawing.Point(281, 398);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 23);
            this.label9.TabIndex = 185;
            this.label9.Text = "Post Wait (ms)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDownWait
            // 
            this.lblDownWait.BackColor = System.Drawing.SystemColors.Window;
            this.lblDownWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDownWait.Location = new System.Drawing.Point(411, 371);
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
            this.lblPostWait.Location = new System.Drawing.Point(411, 398);
            this.lblPostWait.Margin = new System.Windows.Forms.Padding(2);
            this.lblPostWait.Name = "lblPostWait";
            this.lblPostWait.Size = new System.Drawing.Size(75, 23);
            this.lblPostWait.TabIndex = 187;
            this.lblPostWait.Text = "-999.999";
            this.lblPostWait.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPostWait.Click += new System.EventHandler(this.lblPostWait_Click);
            // 
            // frmDispProg_ZPath
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(798, 742);
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
            this.Controls.Add(this.cbEnableWeight);
            this.Controls.Add(this.lblEndLength);
            this.Controls.Add(this.lblStartGap);
            this.Controls.Add(this.lblStartLength);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnEditModel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.l_lbl_ModelNo);
            this.Controls.Add(this.lblModelNo);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.l_lbl_HeadNo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblHeadNo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblSpeed2);
            this.Controls.Add(this.lblSpeedF);
            this.Controls.Add(this.lblSpeed1);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmDispProg_ZPath";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmDispProg_ZPath";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDispProg_ZPath_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDispProg_ZPath_FormClosed);
            this.Load += new System.EventHandler(this.frmDispProg_ZPath_Load);
            this.gbox_Pos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbox_Pos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPointTL;
        private System.Windows.Forms.Button btnSetPtTL;
        private System.Windows.Forms.Button btnGotoPtTL;
        private System.Windows.Forms.Label lblStartGap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDispGap;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSpeed1;
        private System.Windows.Forms.CheckBox cbDispense;
        private System.Windows.Forms.CheckBox cbEnableWeight;
        private System.Windows.Forms.Button btnEditModel;
        private System.Windows.Forms.Label l_lbl_ModelNo;
        private System.Windows.Forms.Label lblModelNo;
        private System.Windows.Forms.Label l_lbl_HeadNo;
        private System.Windows.Forms.Label lblHeadNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCutTailType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCutTailHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCutTailSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCutTailLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPointBR;
        private System.Windows.Forms.Button btnSetPtBR;
        private System.Windows.Forms.Button btnGotoPtBR;
        private System.Windows.Forms.Label lblSpeed2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSpeedF;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblStartLength;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblEndLength;
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
    }
}