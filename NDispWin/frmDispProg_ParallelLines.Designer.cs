
namespace NDispWin
{
    partial class frmDispProg_ParallelLines
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCutTailType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCutTailHeight = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCutTailSpeed = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCutTailLength = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEditModel = new System.Windows.Forms.Button();
            this.l_lbl_ModelNo = new System.Windows.Forms.Label();
            this.lblModelNo = new System.Windows.Forms.Label();
            this.l_lbl_HeadNo = new System.Windows.Forms.Label();
            this.lblHeadNo = new System.Windows.Forms.Label();
            this.lblLineDirection = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFirstLineWeight = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLineWeight = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblLastLineWeight = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbEnableWeight = new System.Windows.Forms.CheckBox();
            this.gbox_Pos = new System.Windows.Forms.GroupBox();
            this.btnEditXY0 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblY0 = new System.Windows.Forms.Label();
            this.lblX0 = new System.Windows.Forms.Label();
            this.btnSetXY0 = new System.Windows.Forms.Button();
            this.btnGotoXY0 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbReverse = new System.Windows.Forms.CheckBox();
            this.lblLeadLength = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblLagLength = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbDispense = new System.Windows.Forms.CheckBox();
            this.gbxWeight = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblRelLeadStartHeight = new System.Windows.Forms.Label();
            this.lblRelLagEndHeight = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblAddLineTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbox_Pos.SuspendLayout();
            this.gbxWeight.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.AccessibleDescription = "Cancel";
            this.btn_Cancel.Location = new System.Drawing.Point(355, 504);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Padding = new System.Windows.Forms.Padding(3);
            this.btn_Cancel.Size = new System.Drawing.Size(75, 36);
            this.btn_Cancel.TabIndex = 131;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.AccessibleDescription = "OK";
            this.btn_OK.Location = new System.Drawing.Point(276, 504);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Padding = new System.Windows.Forms.Padding(3);
            this.btn_OK.Size = new System.Drawing.Size(75, 36);
            this.btn_OK.TabIndex = 130;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(7, 410);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 89);
            this.groupBox1.TabIndex = 132;
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
            this.lblCutTailType.Click += new System.EventHandler(this.lblCutTailType_Click);
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
            this.lblCutTailHeight.Click += new System.EventHandler(this.lblCutTailHeight_Click);
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
            this.lblCutTailSpeed.Click += new System.EventHandler(this.lblCutTailSpeed_Click);
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
            this.lblCutTailLength.Click += new System.EventHandler(this.lblCutTailLength_Click);
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
            // btnEditModel
            // 
            this.btnEditModel.AccessibleDescription = "Edit";
            this.btnEditModel.Location = new System.Drawing.Point(165, 34);
            this.btnEditModel.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditModel.Name = "btnEditModel";
            this.btnEditModel.Size = new System.Drawing.Size(75, 23);
            this.btnEditModel.TabIndex = 140;
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
            this.l_lbl_ModelNo.TabIndex = 135;
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
            this.lblModelNo.TabIndex = 136;
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
            this.l_lbl_HeadNo.TabIndex = 133;
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
            this.lblHeadNo.TabIndex = 134;
            this.lblHeadNo.Text = "lblHeadNo";
            this.lblHeadNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHeadNo.Visible = false;
            this.lblHeadNo.Click += new System.EventHandler(this.lblHeadNo_Click);
            // 
            // lblLineDirection
            // 
            this.lblLineDirection.BackColor = System.Drawing.SystemColors.Window;
            this.lblLineDirection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineDirection.Location = new System.Drawing.Point(117, 73);
            this.lblLineDirection.Margin = new System.Windows.Forms.Padding(2);
            this.lblLineDirection.Name = "lblLineDirection";
            this.lblLineDirection.Padding = new System.Windows.Forms.Padding(3);
            this.lblLineDirection.Size = new System.Drawing.Size(75, 23);
            this.lblLineDirection.TabIndex = 142;
            this.lblLineDirection.Text = "0-Hort";
            this.lblLineDirection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLineDirection.Click += new System.EventHandler(this.lblLineDirection_Click);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.Location = new System.Drawing.Point(7, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(120, 23);
            this.label3.TabIndex = 141;
            this.label3.Text = "Line Direction";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFirstLineWeight
            // 
            this.lblFirstLineWeight.BackColor = System.Drawing.SystemColors.Window;
            this.lblFirstLineWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFirstLineWeight.Location = new System.Drawing.Point(348, 20);
            this.lblFirstLineWeight.Margin = new System.Windows.Forms.Padding(2);
            this.lblFirstLineWeight.Name = "lblFirstLineWeight";
            this.lblFirstLineWeight.Size = new System.Drawing.Size(75, 23);
            this.lblFirstLineWeight.TabIndex = 144;
            this.lblFirstLineWeight.Text = "-999.999";
            this.lblFirstLineWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFirstLineWeight.Click += new System.EventHandler(this.lblFirstLineWeight_Click);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "";
            this.label5.Location = new System.Drawing.Point(218, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 23);
            this.label5.TabIndex = 143;
            this.label5.Text = "First Line (mg)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLineWeight
            // 
            this.lblLineWeight.BackColor = System.Drawing.SystemColors.Window;
            this.lblLineWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineWeight.Location = new System.Drawing.Point(132, 19);
            this.lblLineWeight.Margin = new System.Windows.Forms.Padding(2);
            this.lblLineWeight.Name = "lblLineWeight";
            this.lblLineWeight.Size = new System.Drawing.Size(75, 23);
            this.lblLineWeight.TabIndex = 146;
            this.lblLineWeight.Text = "-999.999";
            this.lblLineWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLineWeight.Click += new System.EventHandler(this.lblLineWeight_Click);
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "";
            this.label7.Location = new System.Drawing.Point(4, 19);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 23);
            this.label7.TabIndex = 145;
            this.label7.Text = "Line Weight (mg)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastLineWeight
            // 
            this.lblLastLineWeight.BackColor = System.Drawing.SystemColors.Window;
            this.lblLastLineWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLastLineWeight.Location = new System.Drawing.Point(348, 47);
            this.lblLastLineWeight.Margin = new System.Windows.Forms.Padding(2);
            this.lblLastLineWeight.Name = "lblLastLineWeight";
            this.lblLastLineWeight.Size = new System.Drawing.Size(75, 23);
            this.lblLastLineWeight.TabIndex = 148;
            this.lblLastLineWeight.Text = "-999.999";
            this.lblLastLineWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLastLineWeight.Click += new System.EventHandler(this.lblLastLineWeight_Click);
            // 
            // label11
            // 
            this.label11.AccessibleDescription = "";
            this.label11.Location = new System.Drawing.Point(218, 47);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 23);
            this.label11.TabIndex = 147;
            this.label11.Text = "Last Line (mg)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbEnableWeight
            // 
            this.cbEnableWeight.AutoSize = true;
            this.cbEnableWeight.Location = new System.Drawing.Point(290, 73);
            this.cbEnableWeight.Name = "cbEnableWeight";
            this.cbEnableWeight.Padding = new System.Windows.Forms.Padding(3);
            this.cbEnableWeight.Size = new System.Drawing.Size(96, 24);
            this.cbEnableWeight.TabIndex = 149;
            this.cbEnableWeight.Text = "Use Weight";
            this.cbEnableWeight.UseVisualStyleBackColor = true;
            this.cbEnableWeight.Click += new System.EventHandler(this.cbEnableWeight_Click);
            // 
            // gbox_Pos
            // 
            this.gbox_Pos.AccessibleDescription = "Position";
            this.gbox_Pos.AutoSize = true;
            this.gbox_Pos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbox_Pos.Controls.Add(this.btnEditXY0);
            this.gbox_Pos.Controls.Add(this.label1);
            this.gbox_Pos.Controls.Add(this.lblY0);
            this.gbox_Pos.Controls.Add(this.lblX0);
            this.gbox_Pos.Controls.Add(this.btnSetXY0);
            this.gbox_Pos.Controls.Add(this.btnGotoXY0);
            this.gbox_Pos.Controls.Add(this.label2);
            this.gbox_Pos.Location = new System.Drawing.Point(7, 103);
            this.gbox_Pos.Margin = new System.Windows.Forms.Padding(2);
            this.gbox_Pos.Name = "gbox_Pos";
            this.gbox_Pos.Size = new System.Drawing.Size(428, 72);
            this.gbox_Pos.TabIndex = 150;
            this.gbox_Pos.TabStop = false;
            this.gbox_Pos.Text = "Start Position";
            this.gbox_Pos.Enter += new System.EventHandler(this.gbox_Pos_Enter);
            // 
            // btnEditXY0
            // 
            this.btnEditXY0.AccessibleDescription = "";
            this.btnEditXY0.Location = new System.Drawing.Point(248, 22);
            this.btnEditXY0.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditXY0.Name = "btnEditXY0";
            this.btnEditXY0.Size = new System.Drawing.Size(27, 30);
            this.btnEditXY0.TabIndex = 130;
            this.btnEditXY0.Text = "...";
            this.btnEditXY0.UseVisualStyleBackColor = true;
            this.btnEditXY0.Click += new System.EventHandler(this.btnEditXY0_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Position";
            this.label1.Location = new System.Drawing.Point(4, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 133;
            this.label1.Text = "Position";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblY0
            // 
            this.lblY0.BackColor = System.Drawing.SystemColors.Window;
            this.lblY0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblY0.Location = new System.Drawing.Point(179, 26);
            this.lblY0.Margin = new System.Windows.Forms.Padding(2);
            this.lblY0.Name = "lblY0";
            this.lblY0.Size = new System.Drawing.Size(65, 23);
            this.lblY0.TabIndex = 110;
            this.lblY0.Text = "-999.999";
            this.lblY0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblY0.Click += new System.EventHandler(this.lblY0_Click);
            // 
            // lblX0
            // 
            this.lblX0.BackColor = System.Drawing.SystemColors.Window;
            this.lblX0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblX0.Location = new System.Drawing.Point(110, 26);
            this.lblX0.Margin = new System.Windows.Forms.Padding(2);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(65, 23);
            this.lblX0.TabIndex = 109;
            this.lblX0.Text = "-999.999";
            this.lblX0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblX0.Click += new System.EventHandler(this.lblX0_Click);
            // 
            // btnSetXY0
            // 
            this.btnSetXY0.AccessibleDescription = "Set";
            this.btnSetXY0.Location = new System.Drawing.Point(279, 22);
            this.btnSetXY0.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetXY0.Name = "btnSetXY0";
            this.btnSetXY0.Size = new System.Drawing.Size(70, 30);
            this.btnSetXY0.TabIndex = 7;
            this.btnSetXY0.Text = "Set";
            this.btnSetXY0.UseVisualStyleBackColor = true;
            this.btnSetXY0.Click += new System.EventHandler(this.btnSetXY0_Click);
            // 
            // btnGotoXY0
            // 
            this.btnGotoXY0.AccessibleDescription = "Goto";
            this.btnGotoXY0.Location = new System.Drawing.Point(353, 22);
            this.btnGotoXY0.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoXY0.Name = "btnGotoXY0";
            this.btnGotoXY0.Size = new System.Drawing.Size(70, 30);
            this.btnGotoXY0.TabIndex = 8;
            this.btnGotoXY0.Text = "Goto";
            this.btnGotoXY0.UseVisualStyleBackColor = true;
            this.btnGotoXY0.Click += new System.EventHandler(this.btnGotoXY0_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(66, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "(mm)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbReverse
            // 
            this.cbReverse.AutoSize = true;
            this.cbReverse.Location = new System.Drawing.Point(211, 73);
            this.cbReverse.Name = "cbReverse";
            this.cbReverse.Padding = new System.Windows.Forms.Padding(3);
            this.cbReverse.Size = new System.Drawing.Size(75, 24);
            this.cbReverse.TabIndex = 153;
            this.cbReverse.Text = "Reverse";
            this.cbReverse.UseVisualStyleBackColor = true;
            this.cbReverse.Click += new System.EventHandler(this.cbReverse_Click);
            // 
            // lblLeadLength
            // 
            this.lblLeadLength.BackColor = System.Drawing.SystemColors.Window;
            this.lblLeadLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLeadLength.Location = new System.Drawing.Point(134, 19);
            this.lblLeadLength.Margin = new System.Windows.Forms.Padding(2);
            this.lblLeadLength.Name = "lblLeadLength";
            this.lblLeadLength.Size = new System.Drawing.Size(75, 23);
            this.lblLeadLength.TabIndex = 155;
            this.lblLeadLength.Text = "-999.999";
            this.lblLeadLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLeadLength.Click += new System.EventHandler(this.lblLeadLength_Click);
            // 
            // label12
            // 
            this.label12.AccessibleDescription = "";
            this.label12.Location = new System.Drawing.Point(4, 19);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 23);
            this.label12.TabIndex = 154;
            this.label12.Text = "Lead Length (mm)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLagLength
            // 
            this.lblLagLength.BackColor = System.Drawing.SystemColors.Window;
            this.lblLagLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLagLength.Location = new System.Drawing.Point(348, 19);
            this.lblLagLength.Margin = new System.Windows.Forms.Padding(2);
            this.lblLagLength.Name = "lblLagLength";
            this.lblLagLength.Size = new System.Drawing.Size(75, 23);
            this.lblLagLength.TabIndex = 157;
            this.lblLagLength.Text = "-999.999";
            this.lblLagLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLagLength.Click += new System.EventHandler(this.lblLagLength_Click);
            // 
            // label14
            // 
            this.label14.AccessibleDescription = "";
            this.label14.Location = new System.Drawing.Point(218, 19);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 23);
            this.label14.TabIndex = 156;
            this.label14.Text = "Lag Length (mm)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDispense
            // 
            this.cbDispense.AutoSize = true;
            this.cbDispense.Location = new System.Drawing.Point(166, 8);
            this.cbDispense.Name = "cbDispense";
            this.cbDispense.Padding = new System.Windows.Forms.Padding(3);
            this.cbDispense.Size = new System.Drawing.Size(80, 24);
            this.cbDispense.TabIndex = 160;
            this.cbDispense.Text = "Dispense";
            this.cbDispense.UseVisualStyleBackColor = true;
            this.cbDispense.Click += new System.EventHandler(this.cbDispense_Click);
            // 
            // gbxWeight
            // 
            this.gbxWeight.AutoSize = true;
            this.gbxWeight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbxWeight.Controls.Add(this.label7);
            this.gbxWeight.Controls.Add(this.lblLineWeight);
            this.gbxWeight.Controls.Add(this.label5);
            this.gbxWeight.Controls.Add(this.lblFirstLineWeight);
            this.gbxWeight.Controls.Add(this.label11);
            this.gbxWeight.Controls.Add(this.lblLastLineWeight);
            this.gbxWeight.Location = new System.Drawing.Point(7, 315);
            this.gbxWeight.Name = "gbxWeight";
            this.gbxWeight.Size = new System.Drawing.Size(428, 90);
            this.gbxWeight.TabIndex = 161;
            this.gbxWeight.TabStop = false;
            this.gbxWeight.Text = "Weight";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.lblRelLeadStartHeight);
            this.groupBox2.Controls.Add(this.lblRelLagEndHeight);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.lblAddLineTime);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lblLeadLength);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblLagLength);
            this.groupBox2.Location = new System.Drawing.Point(7, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 130);
            this.groupBox2.TabIndex = 162;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line Settings";
            // 
            // label15
            // 
            this.label15.AccessibleDescription = "";
            this.label15.Location = new System.Drawing.Point(216, 46);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(133, 37);
            this.label15.TabIndex = 164;
            this.label15.Text = "Rel Lag End Height (mm)";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRelLeadStartHeight
            // 
            this.lblRelLeadStartHeight.BackColor = System.Drawing.SystemColors.Window;
            this.lblRelLeadStartHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRelLeadStartHeight.Location = new System.Drawing.Point(134, 46);
            this.lblRelLeadStartHeight.Margin = new System.Windows.Forms.Padding(2);
            this.lblRelLeadStartHeight.Name = "lblRelLeadStartHeight";
            this.lblRelLeadStartHeight.Size = new System.Drawing.Size(75, 23);
            this.lblRelLeadStartHeight.TabIndex = 161;
            this.lblRelLeadStartHeight.Text = "-999.999";
            this.lblRelLeadStartHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRelLeadStartHeight.Click += new System.EventHandler(this.lblRelLeadStartHeight_Click);
            // 
            // lblRelLagEndHeight
            // 
            this.lblRelLagEndHeight.BackColor = System.Drawing.SystemColors.Window;
            this.lblRelLagEndHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRelLagEndHeight.Location = new System.Drawing.Point(348, 46);
            this.lblRelLagEndHeight.Margin = new System.Windows.Forms.Padding(2);
            this.lblRelLagEndHeight.Name = "lblRelLagEndHeight";
            this.lblRelLagEndHeight.Size = new System.Drawing.Size(75, 23);
            this.lblRelLagEndHeight.TabIndex = 163;
            this.lblRelLagEndHeight.Text = "-999.999";
            this.lblRelLagEndHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRelLagEndHeight.Click += new System.EventHandler(this.lblRelLagEndHeight_Click);
            // 
            // label13
            // 
            this.label13.AccessibleDescription = "";
            this.label13.Location = new System.Drawing.Point(4, 46);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 37);
            this.label13.TabIndex = 160;
            this.label13.Text = "Rel Lead Start Height (mm)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAddLineTime
            // 
            this.lblAddLineTime.BackColor = System.Drawing.SystemColors.Window;
            this.lblAddLineTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAddLineTime.Location = new System.Drawing.Point(132, 87);
            this.lblAddLineTime.Margin = new System.Windows.Forms.Padding(2);
            this.lblAddLineTime.Name = "lblAddLineTime";
            this.lblAddLineTime.Size = new System.Drawing.Size(75, 23);
            this.lblAddLineTime.TabIndex = 159;
            this.lblAddLineTime.Text = "-999.999";
            this.lblAddLineTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAddLineTime.Click += new System.EventHandler(this.lblAddLineTime_Click);
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "Add Line Time (ms)";
            this.label9.Location = new System.Drawing.Point(4, 87);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 23);
            this.label9.TabIndex = 158;
            this.label9.Text = "Add Line Time (ms)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDispProg_ParallelLines
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(454, 584);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbxWeight);
            this.Controls.Add(this.cbDispense);
            this.Controls.Add(this.cbReverse);
            this.Controls.Add(this.gbox_Pos);
            this.Controls.Add(this.cbEnableWeight);
            this.Controls.Add(this.lblLineDirection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEditModel);
            this.Controls.Add(this.l_lbl_ModelNo);
            this.Controls.Add(this.lblModelNo);
            this.Controls.Add(this.l_lbl_HeadNo);
            this.Controls.Add(this.lblHeadNo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDispProg_ParallelLines";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmDispProg_ParallelLines";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDispProg_ParallelLines_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDispProg_ParallelLines_FormClosed);
            this.Load += new System.EventHandler(this.frmDispProg_ParallelLines_Load);
            this.groupBox1.ResumeLayout(false);
            this.gbox_Pos.ResumeLayout(false);
            this.gbxWeight.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCutTailType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCutTailHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCutTailSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCutTailLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEditModel;
        private System.Windows.Forms.Label l_lbl_ModelNo;
        private System.Windows.Forms.Label lblModelNo;
        private System.Windows.Forms.Label l_lbl_HeadNo;
        private System.Windows.Forms.Label lblHeadNo;
        private System.Windows.Forms.Label lblLineDirection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFirstLineWeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLineWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblLastLineWeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbEnableWeight;
        private System.Windows.Forms.GroupBox gbox_Pos;
        private System.Windows.Forms.Button btnEditXY0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.Button btnSetXY0;
        private System.Windows.Forms.Button btnGotoXY0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbReverse;
        private System.Windows.Forms.Label lblLeadLength;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblLagLength;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbDispense;
        private System.Windows.Forms.GroupBox gbxWeight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblAddLineTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblRelLeadStartHeight;
        private System.Windows.Forms.Label lblRelLagEndHeight;
        private System.Windows.Forms.Label label13;
    }
}