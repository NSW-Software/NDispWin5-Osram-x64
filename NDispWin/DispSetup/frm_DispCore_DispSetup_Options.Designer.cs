﻿namespace NDispWin
{
    partial class frm_DispCore_DispSetup_Options
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
            this.gbox_Operation = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOption_DrawOfstAdjustRate = new System.Windows.Forms.Label();
            this.lbl_XYShortDist = new System.Windows.Forms.Label();
            this.cbox_EnableDualMaterial = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_OriginAdjustLimitXY = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbox_EnableMaterialLow = new System.Windows.Forms.CheckBox();
            this.lbl_XYPeakSpeedRatio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbox_EnableChuckVac = new System.Windows.Forms.CheckBox();
            this.lbl_OriginAdjustLimitZ = new System.Windows.Forms.Label();
            this.cbox_EnableRunSingleHead = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbox_PromptRunSingleHead = new System.Windows.Forms.CheckBox();
            this.cbox_EnableDrawOfstAdjust = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_IdlePurgeTimer = new System.Windows.Forms.Label();
            this.cbox_EnableStartIdle = new System.Windows.Forms.CheckBox();
            this.cbox_EnableScriptCheck = new System.Windows.Forms.CheckBox();
            this.Program = new System.Windows.Forms.GroupBox();
            this.cbox_EnableRealTimeFineTune = new System.Windows.Forms.CheckBox();
            this.cbox_EnableScriptCheckUnitMode = new System.Windows.Forms.CheckBox();
            this.lbl_Option_VolumeDisplayDecimalPlace = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbox_EnableHPCManualCtrls = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbox_CopyLogToServer = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbox_LogServerPath = new System.Windows.Forms.TextBox();
            this.btn_Idle = new System.Windows.Forms.Button();
            this.lbl_IdlePurgePostVacTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbl_IdlePurgeInterval = new System.Windows.Forms.Label();
            this.lbl_IdlePurgeDuration = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxIdlePosition = new System.Windows.Forms.ComboBox();
            this.gbox_Operation.SuspendLayout();
            this.Program.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbox_Operation
            // 
            this.gbox_Operation.AccessibleDescription = "Operation";
            this.gbox_Operation.AutoSize = true;
            this.gbox_Operation.Controls.Add(this.label14);
            this.gbox_Operation.Controls.Add(this.cbxIdlePosition);
            this.gbox_Operation.Controls.Add(this.lbl_IdlePurgeTimer);
            this.gbox_Operation.Controls.Add(this.lbl_IdlePurgePostVacTime);
            this.gbox_Operation.Controls.Add(this.label10);
            this.gbox_Operation.Controls.Add(this.label12);
            this.gbox_Operation.Controls.Add(this.label17);
            this.gbox_Operation.Controls.Add(this.lbl_IdlePurgeInterval);
            this.gbox_Operation.Controls.Add(this.lbl_IdlePurgeDuration);
            this.gbox_Operation.Controls.Add(this.btn_Idle);
            this.gbox_Operation.Controls.Add(this.label7);
            this.gbox_Operation.Controls.Add(this.lblOption_DrawOfstAdjustRate);
            this.gbox_Operation.Controls.Add(this.lbl_XYShortDist);
            this.gbox_Operation.Controls.Add(this.cbox_EnableDualMaterial);
            this.gbox_Operation.Controls.Add(this.label8);
            this.gbox_Operation.Controls.Add(this.lbl_OriginAdjustLimitXY);
            this.gbox_Operation.Controls.Add(this.label11);
            this.gbox_Operation.Controls.Add(this.cbox_EnableMaterialLow);
            this.gbox_Operation.Controls.Add(this.lbl_XYPeakSpeedRatio);
            this.gbox_Operation.Controls.Add(this.label1);
            this.gbox_Operation.Controls.Add(this.label9);
            this.gbox_Operation.Controls.Add(this.cbox_EnableChuckVac);
            this.gbox_Operation.Controls.Add(this.lbl_OriginAdjustLimitZ);
            this.gbox_Operation.Controls.Add(this.cbox_EnableRunSingleHead);
            this.gbox_Operation.Controls.Add(this.label13);
            this.gbox_Operation.Controls.Add(this.cbox_PromptRunSingleHead);
            this.gbox_Operation.Controls.Add(this.cbox_EnableDrawOfstAdjust);
            this.gbox_Operation.Controls.Add(this.label5);
            this.gbox_Operation.Controls.Add(this.cbox_EnableStartIdle);
            this.gbox_Operation.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbox_Operation.Location = new System.Drawing.Point(3, 18);
            this.gbox_Operation.Name = "gbox_Operation";
            this.gbox_Operation.Size = new System.Drawing.Size(588, 291);
            this.gbox_Operation.TabIndex = 0;
            this.gbox_Operation.TabStop = false;
            this.gbox_Operation.Text = "Operation";
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "Draw Ofst Adjust Rate";
            this.label7.Location = new System.Drawing.Point(184, 119);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 23);
            this.label7.TabIndex = 195;
            this.label7.Text = "Draw Ofst Adjust Rate";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOption_DrawOfstAdjustRate
            // 
            this.lblOption_DrawOfstAdjustRate.BackColor = System.Drawing.SystemColors.Window;
            this.lblOption_DrawOfstAdjustRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOption_DrawOfstAdjustRate.Location = new System.Drawing.Point(447, 119);
            this.lblOption_DrawOfstAdjustRate.Margin = new System.Windows.Forms.Padding(2);
            this.lblOption_DrawOfstAdjustRate.Name = "lblOption_DrawOfstAdjustRate";
            this.lblOption_DrawOfstAdjustRate.Size = new System.Drawing.Size(75, 23);
            this.lblOption_DrawOfstAdjustRate.TabIndex = 196;
            this.lblOption_DrawOfstAdjustRate.Text = "999";
            this.lblOption_DrawOfstAdjustRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOption_DrawOfstAdjustRate.Click += new System.EventHandler(this.lblOption_DrawOfstAdjustRate_Click);
            // 
            // lbl_XYShortDist
            // 
            this.lbl_XYShortDist.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_XYShortDist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_XYShortDist.Location = new System.Drawing.Point(447, 248);
            this.lbl_XYShortDist.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_XYShortDist.Name = "lbl_XYShortDist";
            this.lbl_XYShortDist.Size = new System.Drawing.Size(75, 23);
            this.lbl_XYShortDist.TabIndex = 194;
            this.lbl_XYShortDist.Text = "0";
            this.lbl_XYShortDist.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_XYShortDist.Click += new System.EventHandler(this.lbl_XYShortDist_Click);
            // 
            // cbox_EnableDualMaterial
            // 
            this.cbox_EnableDualMaterial.AccessibleDescription = "Enable Dual Material";
            this.cbox_EnableDualMaterial.AutoSize = true;
            this.cbox_EnableDualMaterial.Location = new System.Drawing.Point(406, 21);
            this.cbox_EnableDualMaterial.Name = "cbox_EnableDualMaterial";
            this.cbox_EnableDualMaterial.Size = new System.Drawing.Size(134, 18);
            this.cbox_EnableDualMaterial.TabIndex = 176;
            this.cbox_EnableDualMaterial.Text = "Enable Dual Material";
            this.cbox_EnableDualMaterial.UseVisualStyleBackColor = true;
            this.cbox_EnableDualMaterial.Visible = false;
            this.cbox_EnableDualMaterial.Click += new System.EventHandler(this.cbox_EnableDualMaterial_Click);
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "XY Short Dist Peak Speed Ratio, Dist";
            this.label8.Location = new System.Drawing.Point(5, 248);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(243, 23);
            this.label8.TabIndex = 190;
            this.label8.Text = "XY Short Dist Peak Speed Ratio, Dist";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lbl_OriginAdjustLimitXY
            // 
            this.lbl_OriginAdjustLimitXY.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_OriginAdjustLimitXY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_OriginAdjustLimitXY.Location = new System.Drawing.Point(368, 92);
            this.lbl_OriginAdjustLimitXY.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_OriginAdjustLimitXY.Name = "lbl_OriginAdjustLimitXY";
            this.lbl_OriginAdjustLimitXY.Size = new System.Drawing.Size(75, 23);
            this.lbl_OriginAdjustLimitXY.TabIndex = 173;
            this.lbl_OriginAdjustLimitXY.Text = "999";
            this.lbl_OriginAdjustLimitXY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_OriginAdjustLimitXY.Click += new System.EventHandler(this.lbl_DrawOfstAdjustLimitXY_Click);
            // 
            // label11
            // 
            this.label11.AccessibleDescription = "";
            this.label11.Location = new System.Drawing.Point(396, 248);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 23);
            this.label11.TabIndex = 192;
            this.label11.Text = "(mm)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbox_EnableMaterialLow
            // 
            this.cbox_EnableMaterialLow.AccessibleDescription = "Enable Material Low";
            this.cbox_EnableMaterialLow.AutoSize = true;
            this.cbox_EnableMaterialLow.Location = new System.Drawing.Point(266, 21);
            this.cbox_EnableMaterialLow.Name = "cbox_EnableMaterialLow";
            this.cbox_EnableMaterialLow.Size = new System.Drawing.Size(134, 18);
            this.cbox_EnableMaterialLow.TabIndex = 2;
            this.cbox_EnableMaterialLow.Text = "Enable Material Low";
            this.cbox_EnableMaterialLow.UseVisualStyleBackColor = true;
            this.cbox_EnableMaterialLow.Visible = false;
            this.cbox_EnableMaterialLow.Click += new System.EventHandler(this.cbox_EnableMaterialLow_Click);
            // 
            // lbl_XYPeakSpeedRatio
            // 
            this.lbl_XYPeakSpeedRatio.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_XYPeakSpeedRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_XYPeakSpeedRatio.Location = new System.Drawing.Point(303, 248);
            this.lbl_XYPeakSpeedRatio.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_XYPeakSpeedRatio.Name = "lbl_XYPeakSpeedRatio";
            this.lbl_XYPeakSpeedRatio.Size = new System.Drawing.Size(75, 23);
            this.lbl_XYPeakSpeedRatio.TabIndex = 191;
            this.lbl_XYPeakSpeedRatio.Text = "1";
            this.lbl_XYPeakSpeedRatio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_XYPeakSpeedRatio.Click += new System.EventHandler(this.lbl_XYPeakSpeedRatio_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Draw Ofst Adjust Limit";
            this.label1.Location = new System.Drawing.Point(184, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 23);
            this.label1.TabIndex = 172;
            this.label1.Text = "Draw Ofst Adjust Limit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "";
            this.label9.Location = new System.Drawing.Point(252, 248);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 23);
            this.label9.TabIndex = 192;
            this.label9.Text = "(0..1)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbox_EnableChuckVac
            // 
            this.cbox_EnableChuckVac.AccessibleDescription = "Enable Chuck Vac";
            this.cbox_EnableChuckVac.AutoSize = true;
            this.cbox_EnableChuckVac.Location = new System.Drawing.Point(266, 45);
            this.cbox_EnableChuckVac.Name = "cbox_EnableChuckVac";
            this.cbox_EnableChuckVac.Size = new System.Drawing.Size(123, 18);
            this.cbox_EnableChuckVac.TabIndex = 3;
            this.cbox_EnableChuckVac.Text = "Enable Chuck Vac";
            this.cbox_EnableChuckVac.UseVisualStyleBackColor = true;
            this.cbox_EnableChuckVac.Click += new System.EventHandler(this.cbox_EnableChuckVac_Click);
            // 
            // lbl_OriginAdjustLimitZ
            // 
            this.lbl_OriginAdjustLimitZ.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_OriginAdjustLimitZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_OriginAdjustLimitZ.Location = new System.Drawing.Point(447, 92);
            this.lbl_OriginAdjustLimitZ.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_OriginAdjustLimitZ.Name = "lbl_OriginAdjustLimitZ";
            this.lbl_OriginAdjustLimitZ.Size = new System.Drawing.Size(75, 23);
            this.lbl_OriginAdjustLimitZ.TabIndex = 174;
            this.lbl_OriginAdjustLimitZ.Text = "999";
            this.lbl_OriginAdjustLimitZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_OriginAdjustLimitZ.Click += new System.EventHandler(this.lbl_DrawOfstAdjustLimitZ_Click);
            // 
            // cbox_EnableRunSingleHead
            // 
            this.cbox_EnableRunSingleHead.AccessibleDescription = "Enable Run Single Head";
            this.cbox_EnableRunSingleHead.AutoSize = true;
            this.cbox_EnableRunSingleHead.Location = new System.Drawing.Point(6, 21);
            this.cbox_EnableRunSingleHead.Name = "cbox_EnableRunSingleHead";
            this.cbox_EnableRunSingleHead.Size = new System.Drawing.Size(155, 18);
            this.cbox_EnableRunSingleHead.TabIndex = 1;
            this.cbox_EnableRunSingleHead.Text = "Enable Run Single Head";
            this.cbox_EnableRunSingleHead.UseVisualStyleBackColor = true;
            this.cbox_EnableRunSingleHead.Click += new System.EventHandler(this.cbox_EnableRunSingleHead_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(289, 92);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 23);
            this.label13.TabIndex = 175;
            this.label13.Text = "XY, Z (mm)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbox_PromptRunSingleHead
            // 
            this.cbox_PromptRunSingleHead.AccessibleDescription = "Prompt Run Single Head";
            this.cbox_PromptRunSingleHead.AutoSize = true;
            this.cbox_PromptRunSingleHead.Location = new System.Drawing.Point(6, 45);
            this.cbox_PromptRunSingleHead.Name = "cbox_PromptRunSingleHead";
            this.cbox_PromptRunSingleHead.Size = new System.Drawing.Size(159, 18);
            this.cbox_PromptRunSingleHead.TabIndex = 0;
            this.cbox_PromptRunSingleHead.Text = "Prompt Run Single Head";
            this.cbox_PromptRunSingleHead.UseVisualStyleBackColor = true;
            this.cbox_PromptRunSingleHead.Click += new System.EventHandler(this.cbox_PromptRunSingleHead_Click);
            // 
            // cbox_EnableDrawOfstAdjust
            // 
            this.cbox_EnableDrawOfstAdjust.AccessibleDescription = "Enable Draw Ofst Adjust";
            this.cbox_EnableDrawOfstAdjust.AutoSize = true;
            this.cbox_EnableDrawOfstAdjust.Location = new System.Drawing.Point(6, 95);
            this.cbox_EnableDrawOfstAdjust.Name = "cbox_EnableDrawOfstAdjust";
            this.cbox_EnableDrawOfstAdjust.Size = new System.Drawing.Size(160, 18);
            this.cbox_EnableDrawOfstAdjust.TabIndex = 2;
            this.cbox_EnableDrawOfstAdjust.Text = "Enable Draw Ofst Adjust";
            this.cbox_EnableDrawOfstAdjust.UseVisualStyleBackColor = true;
            this.cbox_EnableDrawOfstAdjust.Click += new System.EventHandler(this.cbox_EnableDrawOfstAdjust_Click);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "Idle Purge Timer";
            this.label5.Location = new System.Drawing.Point(188, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 23);
            this.label5.TabIndex = 186;
            this.label5.Text = "Time To Idle (s)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_IdlePurgeTimer
            // 
            this.lbl_IdlePurgeTimer.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_IdlePurgeTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_IdlePurgeTimer.Location = new System.Drawing.Point(293, 154);
            this.lbl_IdlePurgeTimer.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_IdlePurgeTimer.Name = "lbl_IdlePurgeTimer";
            this.lbl_IdlePurgeTimer.Size = new System.Drawing.Size(40, 23);
            this.lbl_IdlePurgeTimer.TabIndex = 187;
            this.lbl_IdlePurgeTimer.Text = "3";
            this.lbl_IdlePurgeTimer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_IdlePurgeTimer.Click += new System.EventHandler(this.label3_Click);
            // 
            // cbox_EnableStartIdle
            // 
            this.cbox_EnableStartIdle.AccessibleDescription = "Enable Start Idle";
            this.cbox_EnableStartIdle.AutoSize = true;
            this.cbox_EnableStartIdle.Location = new System.Drawing.Point(6, 159);
            this.cbox_EnableStartIdle.Name = "cbox_EnableStartIdle";
            this.cbox_EnableStartIdle.Size = new System.Drawing.Size(117, 18);
            this.cbox_EnableStartIdle.TabIndex = 176;
            this.cbox_EnableStartIdle.Text = "Enable Start Idle";
            this.cbox_EnableStartIdle.UseVisualStyleBackColor = true;
            this.cbox_EnableStartIdle.Click += new System.EventHandler(this.cbox_EnableStartIdle_Click);
            // 
            // cbox_EnableScriptCheck
            // 
            this.cbox_EnableScriptCheck.AccessibleDescription = "Enable Script Check";
            this.cbox_EnableScriptCheck.AutoSize = true;
            this.cbox_EnableScriptCheck.Location = new System.Drawing.Point(6, 21);
            this.cbox_EnableScriptCheck.Name = "cbox_EnableScriptCheck";
            this.cbox_EnableScriptCheck.Size = new System.Drawing.Size(134, 18);
            this.cbox_EnableScriptCheck.TabIndex = 177;
            this.cbox_EnableScriptCheck.Text = "Enable Script Check";
            this.cbox_EnableScriptCheck.UseVisualStyleBackColor = true;
            this.cbox_EnableScriptCheck.CheckedChanged += new System.EventHandler(this.cbox_EnableScriptCheck_CheckedChanged);
            this.cbox_EnableScriptCheck.Click += new System.EventHandler(this.cbox_EnableScriptCheck_Click);
            // 
            // Program
            // 
            this.Program.AutoSize = true;
            this.Program.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Program.Controls.Add(this.cbox_EnableRealTimeFineTune);
            this.Program.Controls.Add(this.cbox_EnableScriptCheckUnitMode);
            this.Program.Controls.Add(this.cbox_EnableScriptCheck);
            this.Program.Dock = System.Windows.Forms.DockStyle.Top;
            this.Program.Location = new System.Drawing.Point(3, 309);
            this.Program.Name = "Program";
            this.Program.Size = new System.Drawing.Size(588, 84);
            this.Program.TabIndex = 178;
            this.Program.TabStop = false;
            this.Program.Text = "Program";
            // 
            // cbox_EnableRealTimeFineTune
            // 
            this.cbox_EnableRealTimeFineTune.AccessibleDescription = "Enable Real Time Fine Tune";
            this.cbox_EnableRealTimeFineTune.AutoSize = true;
            this.cbox_EnableRealTimeFineTune.Location = new System.Drawing.Point(266, 21);
            this.cbox_EnableRealTimeFineTune.Name = "cbox_EnableRealTimeFineTune";
            this.cbox_EnableRealTimeFineTune.Size = new System.Drawing.Size(178, 18);
            this.cbox_EnableRealTimeFineTune.TabIndex = 179;
            this.cbox_EnableRealTimeFineTune.Text = "Enable Real Time Fine Tune";
            this.cbox_EnableRealTimeFineTune.UseVisualStyleBackColor = true;
            this.cbox_EnableRealTimeFineTune.Click += new System.EventHandler(this.cbox_EnableRealTimeFineTune_Click);
            // 
            // cbox_EnableScriptCheckUnitMode
            // 
            this.cbox_EnableScriptCheckUnitMode.AccessibleDescription = "Enable Script Check Unit Mode";
            this.cbox_EnableScriptCheckUnitMode.AutoSize = true;
            this.cbox_EnableScriptCheckUnitMode.Location = new System.Drawing.Point(6, 45);
            this.cbox_EnableScriptCheckUnitMode.Name = "cbox_EnableScriptCheckUnitMode";
            this.cbox_EnableScriptCheckUnitMode.Size = new System.Drawing.Size(194, 18);
            this.cbox_EnableScriptCheckUnitMode.TabIndex = 178;
            this.cbox_EnableScriptCheckUnitMode.Text = "Enable Script Check Unit Mode";
            this.cbox_EnableScriptCheckUnitMode.UseVisualStyleBackColor = true;
            this.cbox_EnableScriptCheckUnitMode.Click += new System.EventHandler(this.cbox_EnableScriptCheckUnitMode_Click);
            // 
            // lbl_Option_VolumeDisplayDecimalPlace
            // 
            this.lbl_Option_VolumeDisplayDecimalPlace.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_Option_VolumeDisplayDecimalPlace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Option_VolumeDisplayDecimalPlace.Location = new System.Drawing.Point(268, 20);
            this.lbl_Option_VolumeDisplayDecimalPlace.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Option_VolumeDisplayDecimalPlace.Name = "lbl_Option_VolumeDisplayDecimalPlace";
            this.lbl_Option_VolumeDisplayDecimalPlace.Size = new System.Drawing.Size(75, 23);
            this.lbl_Option_VolumeDisplayDecimalPlace.TabIndex = 185;
            this.lbl_Option_VolumeDisplayDecimalPlace.Text = "3";
            this.lbl_Option_VolumeDisplayDecimalPlace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Option_VolumeDisplayDecimalPlace.Click += new System.EventHandler(this.lbl_Option_VolumeDisplayDecimalPlace_Click);
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "Volume Display Decimal Place";
            this.label6.Location = new System.Drawing.Point(5, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 23);
            this.label6.TabIndex = 184;
            this.label6.Text = "Volume Display Decimal Place";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbox_EnableHPCManualCtrls
            // 
            this.cbox_EnableHPCManualCtrls.AccessibleDescription = "Enable HPC Manual Controls";
            this.cbox_EnableHPCManualCtrls.AutoSize = true;
            this.cbox_EnableHPCManualCtrls.Location = new System.Drawing.Point(6, 48);
            this.cbox_EnableHPCManualCtrls.Name = "cbox_EnableHPCManualCtrls";
            this.cbox_EnableHPCManualCtrls.Size = new System.Drawing.Size(177, 18);
            this.cbox_EnableHPCManualCtrls.TabIndex = 189;
            this.cbox_EnableHPCManualCtrls.Text = "Enable HPC Manual Controls";
            this.cbox_EnableHPCManualCtrls.UseVisualStyleBackColor = true;
            this.cbox_EnableHPCManualCtrls.Click += new System.EventHandler(this.cbox_EnableHPCManualCtrls_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_Option_VolumeDisplayDecimalPlace);
            this.groupBox1.Controls.Add(this.cbox_EnableHPCManualCtrls);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 393);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 87);
            this.groupBox1.TabIndex = 193;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display and Controls";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.Program);
            this.groupBox2.Controls.Add(this.gbox_Operation);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(594, 594);
            this.groupBox2.TabIndex = 194;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = "Log and File Output";
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.cbox_CopyLogToServer);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.tbox_LogServerPath);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 480);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(588, 88);
            this.groupBox3.TabIndex = 232;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log and File Output";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // cbox_CopyLogToServer
            // 
            this.cbox_CopyLogToServer.AccessibleDescription = "Copy Log To Server";
            this.cbox_CopyLogToServer.AutoSize = true;
            this.cbox_CopyLogToServer.Location = new System.Drawing.Point(6, 21);
            this.cbox_CopyLogToServer.Name = "cbox_CopyLogToServer";
            this.cbox_CopyLogToServer.Size = new System.Drawing.Size(135, 18);
            this.cbox_CopyLogToServer.TabIndex = 232;
            this.cbox_CopyLogToServer.Text = "Copy Log To Server";
            this.cbox_CopyLogToServer.UseVisualStyleBackColor = true;
            this.cbox_CopyLogToServer.Click += new System.EventHandler(this.cbox_CopyLogToServer_Click);
            // 
            // label18
            // 
            this.label18.AccessibleDescription = "Server Path";
            this.label18.Location = new System.Drawing.Point(5, 44);
            this.label18.Margin = new System.Windows.Forms.Padding(2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(132, 23);
            this.label18.TabIndex = 230;
            this.label18.Text = "Server Path";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbox_LogServerPath
            // 
            this.tbox_LogServerPath.Location = new System.Drawing.Point(142, 45);
            this.tbox_LogServerPath.Name = "tbox_LogServerPath";
            this.tbox_LogServerPath.Size = new System.Drawing.Size(440, 22);
            this.tbox_LogServerPath.TabIndex = 231;
            this.tbox_LogServerPath.TextChanged += new System.EventHandler(this.tbox_LogServerPath_TextChanged);
            this.tbox_LogServerPath.Validated += new System.EventHandler(this.tbox_LogServerPath_Validated);
            // 
            // btn_Idle
            // 
            this.btn_Idle.AccessibleDescription = "Idle";
            this.btn_Idle.Location = new System.Drawing.Point(21, 197);
            this.btn_Idle.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Idle.Name = "btn_Idle";
            this.btn_Idle.Size = new System.Drawing.Size(70, 35);
            this.btn_Idle.TabIndex = 206;
            this.btn_Idle.Text = "Idle";
            this.btn_Idle.UseVisualStyleBackColor = true;
            this.btn_Idle.Click += new System.EventHandler(this.btn_Idle_Click);
            // 
            // lbl_IdlePurgePostVacTime
            // 
            this.lbl_IdlePurgePostVacTime.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_IdlePurgePostVacTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_IdlePurgePostVacTime.Location = new System.Drawing.Point(506, 208);
            this.lbl_IdlePurgePostVacTime.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_IdlePurgePostVacTime.Name = "lbl_IdlePurgePostVacTime";
            this.lbl_IdlePurgePostVacTime.Size = new System.Drawing.Size(75, 23);
            this.lbl_IdlePurgePostVacTime.TabIndex = 212;
            this.lbl_IdlePurgePostVacTime.Text = "999";
            this.lbl_IdlePurgePostVacTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_IdlePurgePostVacTime.Click += new System.EventHandler(this.lbl_IdlePurgePostVacTime_Click);
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "Post Vac Time (ms)";
            this.label10.Location = new System.Drawing.Point(349, 208);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 23);
            this.label10.TabIndex = 211;
            this.label10.Text = "Post Vac Time (ms)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AccessibleDescription = "Purge Interval (s)";
            this.label12.Location = new System.Drawing.Point(349, 154);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 23);
            this.label12.TabIndex = 210;
            this.label12.Text = "Purge Interval (s)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AccessibleDescription = "Purge Duration (ms)";
            this.label17.Location = new System.Drawing.Point(349, 181);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(150, 23);
            this.label17.TabIndex = 209;
            this.label17.Text = "Purge Duration (ms)";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_IdlePurgeInterval
            // 
            this.lbl_IdlePurgeInterval.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_IdlePurgeInterval.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_IdlePurgeInterval.Location = new System.Drawing.Point(507, 154);
            this.lbl_IdlePurgeInterval.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_IdlePurgeInterval.Name = "lbl_IdlePurgeInterval";
            this.lbl_IdlePurgeInterval.Size = new System.Drawing.Size(75, 23);
            this.lbl_IdlePurgeInterval.TabIndex = 208;
            this.lbl_IdlePurgeInterval.Text = "999";
            this.lbl_IdlePurgeInterval.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_IdlePurgeInterval.Click += new System.EventHandler(this.lbl_IdlePurgeInterval_Click);
            // 
            // lbl_IdlePurgeDuration
            // 
            this.lbl_IdlePurgeDuration.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_IdlePurgeDuration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_IdlePurgeDuration.Location = new System.Drawing.Point(506, 181);
            this.lbl_IdlePurgeDuration.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_IdlePurgeDuration.Name = "lbl_IdlePurgeDuration";
            this.lbl_IdlePurgeDuration.Size = new System.Drawing.Size(75, 23);
            this.lbl_IdlePurgeDuration.TabIndex = 207;
            this.lbl_IdlePurgeDuration.Text = "999";
            this.lbl_IdlePurgeDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_IdlePurgeDuration.Click += new System.EventHandler(this.lbl_IdlePurgeDuration_Click);
            // 
            // label14
            // 
            this.label14.AccessibleDescription = "Idle Position";
            this.label14.Location = new System.Drawing.Point(163, 209);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 23);
            this.label14.TabIndex = 214;
            this.label14.Text = "Idle Position";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxIdlePosition
            // 
            this.cbxIdlePosition.FormattingEnabled = true;
            this.cbxIdlePosition.Location = new System.Drawing.Point(249, 209);
            this.cbxIdlePosition.Name = "cbxIdlePosition";
            this.cbxIdlePosition.Size = new System.Drawing.Size(95, 22);
            this.cbxIdlePosition.TabIndex = 213;
            this.cbxIdlePosition.SelectionChangeCommitted += new System.EventHandler(this.cbxIdlePosition_SelectionChangeCommitted);
            // 
            // frm_DispCore_DispSetup_Options
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_DispCore_DispSetup_Options";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frm_DispCore_DispSetup_Options";
            this.Load += new System.EventHandler(this.frm_DispCore_DispSetup_Options_Load);
            this.Shown += new System.EventHandler(this.frm_DispCore_DispSetup_Options_Shown);
            this.VisibleChanged += new System.EventHandler(this.frm_DispCore_DispSetup_Options_VisibleChanged);
            this.gbox_Operation.ResumeLayout(false);
            this.gbox_Operation.PerformLayout();
            this.Program.ResumeLayout(false);
            this.Program.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbox_Operation;
        private System.Windows.Forms.CheckBox cbox_PromptRunSingleHead;
        private System.Windows.Forms.CheckBox cbox_EnableRunSingleHead;
        private System.Windows.Forms.CheckBox cbox_EnableDrawOfstAdjust;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbl_OriginAdjustLimitZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_OriginAdjustLimitXY;
        private System.Windows.Forms.CheckBox cbox_EnableMaterialLow;
        private System.Windows.Forms.CheckBox cbox_EnableStartIdle;
        private System.Windows.Forms.CheckBox cbox_EnableChuckVac;
        private System.Windows.Forms.CheckBox cbox_EnableDualMaterial;
        private System.Windows.Forms.CheckBox cbox_EnableScriptCheck;
        private System.Windows.Forms.GroupBox Program;
        private System.Windows.Forms.CheckBox cbox_EnableScriptCheckUnitMode;
        private System.Windows.Forms.CheckBox cbox_EnableRealTimeFineTune;
        private System.Windows.Forms.Label lbl_Option_VolumeDisplayDecimalPlace;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_IdlePurgeTimer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbox_EnableHPCManualCtrls;
        private System.Windows.Forms.Label lbl_XYPeakSpeedRatio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_XYShortDist;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbox_LogServerPath;
        private System.Windows.Forms.CheckBox cbox_CopyLogToServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOption_DrawOfstAdjustRate;
        private System.Windows.Forms.Button btn_Idle;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxIdlePosition;
        private System.Windows.Forms.Label lbl_IdlePurgePostVacTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_IdlePurgeInterval;
        private System.Windows.Forms.Label lbl_IdlePurgeDuration;
    }
}