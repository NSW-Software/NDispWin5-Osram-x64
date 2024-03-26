namespace NDispWin
{
    partial class frm_DispCore_DispSetup_Maint
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblMMaintPosXYZ = new System.Windows.Forms.Label();
            this.btn_SetMMaintPos = new System.Windows.Forms.Button();
            this.btn_GotoMMaintPos = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblNMaintPosXYZ = new System.Windows.Forms.Label();
            this.btn_SetPMaintPos = new System.Windows.Forms.Button();
            this.btn_GotoPMaintPos = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.btn_PumpActionSetup = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbox_PumpCounters = new System.Windows.Forms.GroupBox();
            this.btn_UnitCounterBReset = new System.Windows.Forms.Button();
            this.lbl_UnitCounterBStartDateTime = new System.Windows.Forms.Label();
            this.btn_UnitCounterAReset = new System.Windows.Forms.Button();
            this.lbl_UnitCounterAStartDateTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_UnitCounterB = new System.Windows.Forms.Label();
            this.lbl_UnitCounterA = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_FillCounterBLimit = new System.Windows.Forms.Label();
            this.lbl_FillCounterALimit = new System.Windows.Forms.Label();
            this.btn_FillCounterBReset = new System.Windows.Forms.Button();
            this.btn_FillCounterAReset = new System.Windows.Forms.Button();
            this.lbl_FillCounterBStartDateTime = new System.Windows.Forms.Label();
            this.lbl_FillCounterAStartDateTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTrig = new System.Windows.Forms.Button();
            this.btnExecP2NICam = new System.Windows.Forms.Button();
            this.btnExecP1NICam = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblP2NeedleInspCamPos = new System.Windows.Forms.Label();
            this.btnSetP2NICamPos = new System.Windows.Forms.Button();
            this.btnGotoP2NICamPos = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblP1NeedleInspCamPos = new System.Windows.Forms.Label();
            this.btnSetP1NICamPos = new System.Windows.Forms.Button();
            this.btnGotoP1NICamPos = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbox_PumpCounters.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.AccessibleDescription = "Machine Maint Pos";
            this.groupBox6.AutoSize = true;
            this.groupBox6.Controls.Add(this.lblMMaintPosXYZ);
            this.groupBox6.Controls.Add(this.btn_SetMMaintPos);
            this.groupBox6.Controls.Add(this.btn_GotoMMaintPos);
            this.groupBox6.Controls.Add(this.label37);
            this.groupBox6.Location = new System.Drawing.Point(5, 5);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(590, 75);
            this.groupBox6.TabIndex = 173;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Machine Maint Pos";
            // 
            // lblMMaintPosXYZ
            // 
            this.lblMMaintPosXYZ.BackColor = System.Drawing.SystemColors.Control;
            this.lblMMaintPosXYZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMMaintPosXYZ.Location = new System.Drawing.Point(131, 15);
            this.lblMMaintPosXYZ.Margin = new System.Windows.Forms.Padding(2);
            this.lblMMaintPosXYZ.Name = "lblMMaintPosXYZ";
            this.lblMMaintPosXYZ.Size = new System.Drawing.Size(313, 36);
            this.lblMMaintPosXYZ.TabIndex = 157;
            this.lblMMaintPosXYZ.Text = "-999.999,-999.999,-99.999";
            this.lblMMaintPosXYZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_SetMMaintPos
            // 
            this.btn_SetMMaintPos.AccessibleDescription = "Set";
            this.btn_SetMMaintPos.Location = new System.Drawing.Point(447, 15);
            this.btn_SetMMaintPos.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SetMMaintPos.Name = "btn_SetMMaintPos";
            this.btn_SetMMaintPos.Size = new System.Drawing.Size(59, 36);
            this.btn_SetMMaintPos.TabIndex = 147;
            this.btn_SetMMaintPos.Text = "Set";
            this.btn_SetMMaintPos.UseVisualStyleBackColor = true;
            this.btn_SetMMaintPos.Click += new System.EventHandler(this.btn_SetMMaintPos_Click);
            // 
            // btn_GotoMMaintPos
            // 
            this.btn_GotoMMaintPos.AccessibleDescription = "Goto";
            this.btn_GotoMMaintPos.Location = new System.Drawing.Point(510, 15);
            this.btn_GotoMMaintPos.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoMMaintPos.Name = "btn_GotoMMaintPos";
            this.btn_GotoMMaintPos.Size = new System.Drawing.Size(75, 36);
            this.btn_GotoMMaintPos.TabIndex = 146;
            this.btn_GotoMMaintPos.Text = "Goto";
            this.btn_GotoMMaintPos.UseVisualStyleBackColor = true;
            this.btn_GotoMMaintPos.Click += new System.EventHandler(this.btn_GotoMMaintPos_Click);
            // 
            // label37
            // 
            this.label37.AccessibleDescription = "XYZ (mm)";
            this.label37.Location = new System.Drawing.Point(7, 22);
            this.label37.Margin = new System.Windows.Forms.Padding(2);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(120, 23);
            this.label37.TabIndex = 143;
            this.label37.Text = "Position (mm)";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = "Pump Maint Pos";
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.lblNMaintPosXYZ);
            this.groupBox3.Controls.Add(this.btn_SetPMaintPos);
            this.groupBox3.Controls.Add(this.btn_GotoPMaintPos);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Location = new System.Drawing.Point(5, 84);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(590, 75);
            this.groupBox3.TabIndex = 174;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pump Maint Pos";
            // 
            // lblNMaintPosXYZ
            // 
            this.lblNMaintPosXYZ.BackColor = System.Drawing.SystemColors.Control;
            this.lblNMaintPosXYZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNMaintPosXYZ.Location = new System.Drawing.Point(131, 15);
            this.lblNMaintPosXYZ.Margin = new System.Windows.Forms.Padding(2);
            this.lblNMaintPosXYZ.Name = "lblNMaintPosXYZ";
            this.lblNMaintPosXYZ.Size = new System.Drawing.Size(313, 36);
            this.lblNMaintPosXYZ.TabIndex = 157;
            this.lblNMaintPosXYZ.Text = "-999.999,-999.999,-99.999";
            this.lblNMaintPosXYZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_SetPMaintPos
            // 
            this.btn_SetPMaintPos.AccessibleDescription = "Set";
            this.btn_SetPMaintPos.Location = new System.Drawing.Point(447, 15);
            this.btn_SetPMaintPos.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SetPMaintPos.Name = "btn_SetPMaintPos";
            this.btn_SetPMaintPos.Size = new System.Drawing.Size(59, 36);
            this.btn_SetPMaintPos.TabIndex = 147;
            this.btn_SetPMaintPos.Text = "Set";
            this.btn_SetPMaintPos.UseVisualStyleBackColor = true;
            this.btn_SetPMaintPos.Click += new System.EventHandler(this.btn_SetPMaintPos_Click);
            // 
            // btn_GotoPMaintPos
            // 
            this.btn_GotoPMaintPos.AccessibleDescription = "Goto";
            this.btn_GotoPMaintPos.Location = new System.Drawing.Point(510, 15);
            this.btn_GotoPMaintPos.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoPMaintPos.Name = "btn_GotoPMaintPos";
            this.btn_GotoPMaintPos.Size = new System.Drawing.Size(75, 36);
            this.btn_GotoPMaintPos.TabIndex = 146;
            this.btn_GotoPMaintPos.Text = "Goto";
            this.btn_GotoPMaintPos.UseVisualStyleBackColor = true;
            this.btn_GotoPMaintPos.Click += new System.EventHandler(this.btn_GotoPMaintPos_Click);
            // 
            // label17
            // 
            this.label17.AccessibleDescription = "XYZ (mm)";
            this.label17.Location = new System.Drawing.Point(7, 22);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 23);
            this.label17.TabIndex = 143;
            this.label17.Text = "XYZ (mm)";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_PumpActionSetup
            // 
            this.btn_PumpActionSetup.AccessibleDescription = "Setup";
            this.btn_PumpActionSetup.Location = new System.Drawing.Point(6, 21);
            this.btn_PumpActionSetup.Name = "btn_PumpActionSetup";
            this.btn_PumpActionSetup.Size = new System.Drawing.Size(104, 36);
            this.btn_PumpActionSetup.TabIndex = 175;
            this.btn_PumpActionSetup.Text = "Setup";
            this.btn_PumpActionSetup.UseVisualStyleBackColor = true;
            this.btn_PumpActionSetup.Click += new System.EventHandler(this.btn_PumpActionSetup_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Pump Action";
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btn_PumpActionSetup);
            this.groupBox1.Location = new System.Drawing.Point(4, 319);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 82);
            this.groupBox1.TabIndex = 176;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pump Action";
            // 
            // gbox_PumpCounters
            // 
            this.gbox_PumpCounters.AutoSize = true;
            this.gbox_PumpCounters.Controls.Add(this.btn_UnitCounterBReset);
            this.gbox_PumpCounters.Controls.Add(this.lbl_UnitCounterBStartDateTime);
            this.gbox_PumpCounters.Controls.Add(this.btn_UnitCounterAReset);
            this.gbox_PumpCounters.Controls.Add(this.lbl_UnitCounterAStartDateTime);
            this.gbox_PumpCounters.Controls.Add(this.label5);
            this.gbox_PumpCounters.Controls.Add(this.lbl_UnitCounterB);
            this.gbox_PumpCounters.Controls.Add(this.lbl_UnitCounterA);
            this.gbox_PumpCounters.Controls.Add(this.label1);
            this.gbox_PumpCounters.Controls.Add(this.lbl_FillCounterBLimit);
            this.gbox_PumpCounters.Controls.Add(this.lbl_FillCounterALimit);
            this.gbox_PumpCounters.Controls.Add(this.btn_FillCounterBReset);
            this.gbox_PumpCounters.Controls.Add(this.btn_FillCounterAReset);
            this.gbox_PumpCounters.Controls.Add(this.lbl_FillCounterBStartDateTime);
            this.gbox_PumpCounters.Controls.Add(this.lbl_FillCounterAStartDateTime);
            this.gbox_PumpCounters.Controls.Add(this.label4);
            this.gbox_PumpCounters.Controls.Add(this.label2);
            this.gbox_PumpCounters.Location = new System.Drawing.Point(4, 407);
            this.gbox_PumpCounters.Name = "gbox_PumpCounters";
            this.gbox_PumpCounters.Size = new System.Drawing.Size(590, 148);
            this.gbox_PumpCounters.TabIndex = 177;
            this.gbox_PumpCounters.TabStop = false;
            this.gbox_PumpCounters.Text = "Pump Counters";
            // 
            // btn_UnitCounterBReset
            // 
            this.btn_UnitCounterBReset.AccessibleDescription = "Reset";
            this.btn_UnitCounterBReset.Location = new System.Drawing.Point(510, 101);
            this.btn_UnitCounterBReset.Margin = new System.Windows.Forms.Padding(2);
            this.btn_UnitCounterBReset.Name = "btn_UnitCounterBReset";
            this.btn_UnitCounterBReset.Size = new System.Drawing.Size(75, 23);
            this.btn_UnitCounterBReset.TabIndex = 178;
            this.btn_UnitCounterBReset.Text = "Reset";
            this.btn_UnitCounterBReset.UseVisualStyleBackColor = true;
            this.btn_UnitCounterBReset.Click += new System.EventHandler(this.btn_VermesCounterBReset_Click);
            // 
            // lbl_UnitCounterBStartDateTime
            // 
            this.lbl_UnitCounterBStartDateTime.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_UnitCounterBStartDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_UnitCounterBStartDateTime.Location = new System.Drawing.Point(289, 101);
            this.lbl_UnitCounterBStartDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_UnitCounterBStartDateTime.Name = "lbl_UnitCounterBStartDateTime";
            this.lbl_UnitCounterBStartDateTime.Size = new System.Drawing.Size(217, 23);
            this.lbl_UnitCounterBStartDateTime.TabIndex = 177;
            this.lbl_UnitCounterBStartDateTime.Text = "99999";
            this.lbl_UnitCounterBStartDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_UnitCounterAReset
            // 
            this.btn_UnitCounterAReset.AccessibleDescription = "Reset";
            this.btn_UnitCounterAReset.Location = new System.Drawing.Point(510, 74);
            this.btn_UnitCounterAReset.Margin = new System.Windows.Forms.Padding(2);
            this.btn_UnitCounterAReset.Name = "btn_UnitCounterAReset";
            this.btn_UnitCounterAReset.Size = new System.Drawing.Size(75, 23);
            this.btn_UnitCounterAReset.TabIndex = 176;
            this.btn_UnitCounterAReset.Text = "Reset";
            this.btn_UnitCounterAReset.UseVisualStyleBackColor = true;
            this.btn_UnitCounterAReset.Click += new System.EventHandler(this.btn_VermesCounterAReset_Click);
            // 
            // lbl_UnitCounterAStartDateTime
            // 
            this.lbl_UnitCounterAStartDateTime.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_UnitCounterAStartDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_UnitCounterAStartDateTime.Location = new System.Drawing.Point(289, 74);
            this.lbl_UnitCounterAStartDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_UnitCounterAStartDateTime.Name = "lbl_UnitCounterAStartDateTime";
            this.lbl_UnitCounterAStartDateTime.Size = new System.Drawing.Size(217, 23);
            this.lbl_UnitCounterAStartDateTime.TabIndex = 175;
            this.lbl_UnitCounterAStartDateTime.Text = "99999";
            this.lbl_UnitCounterAStartDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "Unit Counter B";
            this.label5.Location = new System.Drawing.Point(7, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 23);
            this.label5.TabIndex = 174;
            this.label5.Text = "Unit Counter B";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_UnitCounterB
            // 
            this.lbl_UnitCounterB.BackColor = System.Drawing.Color.White;
            this.lbl_UnitCounterB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_UnitCounterB.Location = new System.Drawing.Point(131, 101);
            this.lbl_UnitCounterB.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_UnitCounterB.Name = "lbl_UnitCounterB";
            this.lbl_UnitCounterB.Size = new System.Drawing.Size(154, 23);
            this.lbl_UnitCounterB.TabIndex = 173;
            this.lbl_UnitCounterB.Text = "99999";
            this.lbl_UnitCounterB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_UnitCounterB.Click += new System.EventHandler(this.lbl_VermesCounterB_Click);
            // 
            // lbl_UnitCounterA
            // 
            this.lbl_UnitCounterA.BackColor = System.Drawing.Color.White;
            this.lbl_UnitCounterA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_UnitCounterA.Location = new System.Drawing.Point(131, 74);
            this.lbl_UnitCounterA.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_UnitCounterA.Name = "lbl_UnitCounterA";
            this.lbl_UnitCounterA.Size = new System.Drawing.Size(154, 23);
            this.lbl_UnitCounterA.TabIndex = 172;
            this.lbl_UnitCounterA.Text = "99999";
            this.lbl_UnitCounterA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_UnitCounterA.Click += new System.EventHandler(this.lbl_VermesCounterA_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Unit Counter A";
            this.label1.Location = new System.Drawing.Point(7, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 23);
            this.label1.TabIndex = 171;
            this.label1.Text = "Unit Counter A";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_FillCounterBLimit
            // 
            this.lbl_FillCounterBLimit.BackColor = System.Drawing.Color.White;
            this.lbl_FillCounterBLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_FillCounterBLimit.Location = new System.Drawing.Point(130, 47);
            this.lbl_FillCounterBLimit.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_FillCounterBLimit.Name = "lbl_FillCounterBLimit";
            this.lbl_FillCounterBLimit.Size = new System.Drawing.Size(154, 23);
            this.lbl_FillCounterBLimit.TabIndex = 170;
            this.lbl_FillCounterBLimit.Text = "99999";
            this.lbl_FillCounterBLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_FillCounterBLimit.Click += new System.EventHandler(this.lbl_FillCounterBLimit_Click);
            // 
            // lbl_FillCounterALimit
            // 
            this.lbl_FillCounterALimit.BackColor = System.Drawing.Color.White;
            this.lbl_FillCounterALimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_FillCounterALimit.Location = new System.Drawing.Point(130, 20);
            this.lbl_FillCounterALimit.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_FillCounterALimit.Name = "lbl_FillCounterALimit";
            this.lbl_FillCounterALimit.Size = new System.Drawing.Size(155, 23);
            this.lbl_FillCounterALimit.TabIndex = 168;
            this.lbl_FillCounterALimit.Text = "99999 / 99999";
            this.lbl_FillCounterALimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_FillCounterALimit.Click += new System.EventHandler(this.lbl_FillCounterALimit_Click);
            // 
            // btn_FillCounterBReset
            // 
            this.btn_FillCounterBReset.AccessibleDescription = "Reset";
            this.btn_FillCounterBReset.Location = new System.Drawing.Point(510, 47);
            this.btn_FillCounterBReset.Margin = new System.Windows.Forms.Padding(2);
            this.btn_FillCounterBReset.Name = "btn_FillCounterBReset";
            this.btn_FillCounterBReset.Size = new System.Drawing.Size(75, 23);
            this.btn_FillCounterBReset.TabIndex = 166;
            this.btn_FillCounterBReset.Text = "Reset";
            this.btn_FillCounterBReset.UseVisualStyleBackColor = true;
            this.btn_FillCounterBReset.Click += new System.EventHandler(this.btn_FillCounterBReset_Click);
            // 
            // btn_FillCounterAReset
            // 
            this.btn_FillCounterAReset.AccessibleDescription = "Reset";
            this.btn_FillCounterAReset.Location = new System.Drawing.Point(510, 20);
            this.btn_FillCounterAReset.Margin = new System.Windows.Forms.Padding(2);
            this.btn_FillCounterAReset.Name = "btn_FillCounterAReset";
            this.btn_FillCounterAReset.Size = new System.Drawing.Size(75, 23);
            this.btn_FillCounterAReset.TabIndex = 165;
            this.btn_FillCounterAReset.Text = "Reset";
            this.btn_FillCounterAReset.UseVisualStyleBackColor = true;
            this.btn_FillCounterAReset.Click += new System.EventHandler(this.btn_FillCounterAReset_Click);
            // 
            // lbl_FillCounterBStartDateTime
            // 
            this.lbl_FillCounterBStartDateTime.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_FillCounterBStartDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_FillCounterBStartDateTime.Location = new System.Drawing.Point(289, 47);
            this.lbl_FillCounterBStartDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_FillCounterBStartDateTime.Name = "lbl_FillCounterBStartDateTime";
            this.lbl_FillCounterBStartDateTime.Size = new System.Drawing.Size(217, 23);
            this.lbl_FillCounterBStartDateTime.TabIndex = 164;
            this.lbl_FillCounterBStartDateTime.Text = "99999";
            this.lbl_FillCounterBStartDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_FillCounterAStartDateTime
            // 
            this.lbl_FillCounterAStartDateTime.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_FillCounterAStartDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_FillCounterAStartDateTime.Location = new System.Drawing.Point(289, 20);
            this.lbl_FillCounterAStartDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_FillCounterAStartDateTime.Name = "lbl_FillCounterAStartDateTime";
            this.lbl_FillCounterAStartDateTime.Size = new System.Drawing.Size(217, 23);
            this.lbl_FillCounterAStartDateTime.TabIndex = 163;
            this.lbl_FillCounterAStartDateTime.Text = "99999";
            this.lbl_FillCounterAStartDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "Fill Counter B";
            this.label4.Location = new System.Drawing.Point(7, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 23);
            this.label4.TabIndex = 160;
            this.label4.Text = "Fill Counter B";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Fill Counter A";
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 23);
            this.label2.TabIndex = 158;
            this.label2.Text = "Fill Counter A";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Needle Insp Camera Pos";
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.btnTrig);
            this.groupBox2.Controls.Add(this.btnExecP2NICam);
            this.groupBox2.Controls.Add(this.btnExecP1NICam);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblP2NeedleInspCamPos);
            this.groupBox2.Controls.Add(this.btnSetP2NICamPos);
            this.groupBox2.Controls.Add(this.btnGotoP2NICamPos);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblP1NeedleInspCamPos);
            this.groupBox2.Controls.Add(this.btnSetP1NICamPos);
            this.groupBox2.Controls.Add(this.btnGotoP1NICamPos);
            this.groupBox2.Location = new System.Drawing.Point(5, 163);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 151);
            this.groupBox2.TabIndex = 178;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Needle Insp Camera Pos";
            // 
            // btnTrig
            // 
            this.btnTrig.AccessibleDescription = "Trig";
            this.btnTrig.Location = new System.Drawing.Point(510, 95);
            this.btnTrig.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrig.Name = "btnTrig";
            this.btnTrig.Size = new System.Drawing.Size(75, 36);
            this.btnTrig.TabIndex = 173;
            this.btnTrig.Text = "Trig";
            this.btnTrig.UseVisualStyleBackColor = true;
            this.btnTrig.Click += new System.EventHandler(this.btnTrig_Click);
            // 
            // btnExecP2NICam
            // 
            this.btnExecP2NICam.AccessibleDescription = "Exec";
            this.btnExecP2NICam.Location = new System.Drawing.Point(510, 55);
            this.btnExecP2NICam.Margin = new System.Windows.Forms.Padding(2);
            this.btnExecP2NICam.Name = "btnExecP2NICam";
            this.btnExecP2NICam.Size = new System.Drawing.Size(75, 36);
            this.btnExecP2NICam.TabIndex = 172;
            this.btnExecP2NICam.Text = "Exec";
            this.btnExecP2NICam.UseVisualStyleBackColor = true;
            this.btnExecP2NICam.Click += new System.EventHandler(this.btnExecP2NICam_Click);
            // 
            // btnExecP1NICam
            // 
            this.btnExecP1NICam.AccessibleDescription = "Exec";
            this.btnExecP1NICam.Location = new System.Drawing.Point(510, 15);
            this.btnExecP1NICam.Margin = new System.Windows.Forms.Padding(2);
            this.btnExecP1NICam.Name = "btnExecP1NICam";
            this.btnExecP1NICam.Size = new System.Drawing.Size(75, 36);
            this.btnExecP1NICam.TabIndex = 171;
            this.btnExecP1NICam.Text = "Exec";
            this.btnExecP1NICam.UseVisualStyleBackColor = true;
            this.btnExecP1NICam.Click += new System.EventHandler(this.btnExecP1NICam_Click);
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "";
            this.label6.Location = new System.Drawing.Point(5, 62);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 23);
            this.label6.TabIndex = 170;
            this.label6.Text = "Pump 2";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP2NeedleInspCamPos
            // 
            this.lblP2NeedleInspCamPos.BackColor = System.Drawing.SystemColors.Control;
            this.lblP2NeedleInspCamPos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblP2NeedleInspCamPos.Location = new System.Drawing.Point(131, 55);
            this.lblP2NeedleInspCamPos.Margin = new System.Windows.Forms.Padding(2);
            this.lblP2NeedleInspCamPos.Name = "lblP2NeedleInspCamPos";
            this.lblP2NeedleInspCamPos.Size = new System.Drawing.Size(233, 36);
            this.lblP2NeedleInspCamPos.TabIndex = 169;
            this.lblP2NeedleInspCamPos.Text = "-999.999,-999.999,-99.999";
            this.lblP2NeedleInspCamPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSetP2NICamPos
            // 
            this.btnSetP2NICamPos.AccessibleDescription = "Set";
            this.btnSetP2NICamPos.Location = new System.Drawing.Point(368, 55);
            this.btnSetP2NICamPos.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetP2NICamPos.Name = "btnSetP2NICamPos";
            this.btnSetP2NICamPos.Size = new System.Drawing.Size(59, 36);
            this.btnSetP2NICamPos.TabIndex = 168;
            this.btnSetP2NICamPos.Text = "Set";
            this.btnSetP2NICamPos.UseVisualStyleBackColor = true;
            this.btnSetP2NICamPos.Click += new System.EventHandler(this.btnSetP2NICamPos_Click);
            // 
            // btnGotoP2NICamPos
            // 
            this.btnGotoP2NICamPos.AccessibleDescription = "Goto";
            this.btnGotoP2NICamPos.Location = new System.Drawing.Point(431, 55);
            this.btnGotoP2NICamPos.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoP2NICamPos.Name = "btnGotoP2NICamPos";
            this.btnGotoP2NICamPos.Size = new System.Drawing.Size(75, 36);
            this.btnGotoP2NICamPos.TabIndex = 167;
            this.btnGotoP2NICamPos.Text = "Goto";
            this.btnGotoP2NICamPos.UseVisualStyleBackColor = true;
            this.btnGotoP2NICamPos.Click += new System.EventHandler(this.btnGotoP2NICamPos_Click);
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "";
            this.label8.Location = new System.Drawing.Point(5, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 23);
            this.label8.TabIndex = 166;
            this.label8.Text = "Pump 1";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP1NeedleInspCamPos
            // 
            this.lblP1NeedleInspCamPos.BackColor = System.Drawing.SystemColors.Control;
            this.lblP1NeedleInspCamPos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblP1NeedleInspCamPos.Location = new System.Drawing.Point(131, 15);
            this.lblP1NeedleInspCamPos.Margin = new System.Windows.Forms.Padding(2);
            this.lblP1NeedleInspCamPos.Name = "lblP1NeedleInspCamPos";
            this.lblP1NeedleInspCamPos.Size = new System.Drawing.Size(233, 36);
            this.lblP1NeedleInspCamPos.TabIndex = 157;
            this.lblP1NeedleInspCamPos.Text = "-999.999,-999.999,-99.999";
            this.lblP1NeedleInspCamPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSetP1NICamPos
            // 
            this.btnSetP1NICamPos.AccessibleDescription = "Set";
            this.btnSetP1NICamPos.Location = new System.Drawing.Point(368, 15);
            this.btnSetP1NICamPos.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetP1NICamPos.Name = "btnSetP1NICamPos";
            this.btnSetP1NICamPos.Size = new System.Drawing.Size(59, 36);
            this.btnSetP1NICamPos.TabIndex = 147;
            this.btnSetP1NICamPos.Text = "Set";
            this.btnSetP1NICamPos.UseVisualStyleBackColor = true;
            this.btnSetP1NICamPos.Click += new System.EventHandler(this.btnSetP1NICamPos_Click);
            // 
            // btnGotoP1NICamPos
            // 
            this.btnGotoP1NICamPos.AccessibleDescription = "Goto";
            this.btnGotoP1NICamPos.Location = new System.Drawing.Point(431, 15);
            this.btnGotoP1NICamPos.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoP1NICamPos.Name = "btnGotoP1NICamPos";
            this.btnGotoP1NICamPos.Size = new System.Drawing.Size(75, 36);
            this.btnGotoP1NICamPos.TabIndex = 146;
            this.btnGotoP1NICamPos.Text = "Goto";
            this.btnGotoP1NICamPos.UseVisualStyleBackColor = true;
            this.btnGotoP1NICamPos.Click += new System.EventHandler(this.btnGotoP1NICamPos_Click);
            // 
            // frm_DispCore_DispSetup_Maint
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbox_PumpCounters);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox6);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_DispCore_DispSetup_Maint";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frm_DispCore_DispSetup_Maint";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_DispCore_DispSetup_Maint_FormClosing);
            this.Load += new System.EventHandler(this.frm_DispCore_DispSetup_Maint_Load);
            this.Shown += new System.EventHandler(this.frm_DispCore_DispSetup_Maint_Shown);
            this.VisibleChanged += new System.EventHandler(this.frm_DispCore_DispSetup_Maint_VisibleChanged);
            this.groupBox6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbox_PumpCounters.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblMMaintPosXYZ;
        private System.Windows.Forms.Button btn_SetMMaintPos;
        private System.Windows.Forms.Button btn_GotoMMaintPos;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblNMaintPosXYZ;
        private System.Windows.Forms.Button btn_SetPMaintPos;
        private System.Windows.Forms.Button btn_GotoPMaintPos;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btn_PumpActionSetup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbox_PumpCounters;
        private System.Windows.Forms.Button btn_FillCounterBReset;
        private System.Windows.Forms.Button btn_FillCounterAReset;
        private System.Windows.Forms.Label lbl_FillCounterBStartDateTime;
        private System.Windows.Forms.Label lbl_FillCounterAStartDateTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_FillCounterBLimit;
        private System.Windows.Forms.Label lbl_FillCounterALimit;
        private System.Windows.Forms.Button btn_UnitCounterBReset;
        private System.Windows.Forms.Label lbl_UnitCounterBStartDateTime;
        private System.Windows.Forms.Button btn_UnitCounterAReset;
        private System.Windows.Forms.Label lbl_UnitCounterAStartDateTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_UnitCounterB;
        private System.Windows.Forms.Label lbl_UnitCounterA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblP1NeedleInspCamPos;
        private System.Windows.Forms.Button btnSetP1NICamPos;
        private System.Windows.Forms.Button btnGotoP1NICamPos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblP2NeedleInspCamPos;
        private System.Windows.Forms.Button btnSetP2NICamPos;
        private System.Windows.Forms.Button btnGotoP2NICamPos;
        private System.Windows.Forms.Button btnExecP2NICam;
        private System.Windows.Forms.Button btnExecP1NICam;
        private System.Windows.Forms.Button btnTrig;
    }
}