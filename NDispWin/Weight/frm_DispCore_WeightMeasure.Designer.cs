namespace NDispWin
{
    partial class frm_DispCore_WeightMeasure
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
            this.tmr_WeightDisplay = new System.Windows.Forms.Timer(this.components);
            this.tmr_Run = new System.Windows.Forms.Timer(this.components);
            this.ssBottom = new System.Windows.Forms.StatusStrip();
            this.sslMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.vsbar_Zoom = new System.Windows.Forms.VScrollBar();
            this.btn_Ctrl2 = new System.Windows.Forms.Button();
            this.btn_Ctrl1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_StDev = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Ave = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Range = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_Max = new System.Windows.Forms.Label();
            this.lbl_Min = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btn_SaveToFile = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_Spec = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_TolPcnt = new System.Windows.Forms.Label();
            this.btn_ReComputeResult = new System.Windows.Forms.Button();
            this.lbl_Tol = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbl_DotsPerSample = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_WeightSampleCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.btn_Head1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblCurrentNettVolume1 = new System.Windows.Forms.Label();
            this.lblCurrentNettVolume2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_CurrentCalName = new System.Windows.Forms.Label();
            this.lbl_CurrentCal1 = new System.Windows.Forms.Label();
            this.lbl_CurrentCal2 = new System.Windows.Forms.Label();
            this.btn_Zero = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_WeightCurrentValue = new System.Windows.Forms.Label();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.btn_Head2 = new System.Windows.Forms.Button();
            this.lbox_Result = new System.Windows.Forms.ListBox();
            this.btn_Setting = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ssBottom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmr_WeightDisplay
            // 
            this.tmr_WeightDisplay.Enabled = true;
            this.tmr_WeightDisplay.Tick += new System.EventHandler(this.tmr_Display_Tick);
            // 
            // tmr_Run
            // 
            this.tmr_Run.Enabled = true;
            this.tmr_Run.Tick += new System.EventHandler(this.tmr_Run_Tick);
            // 
            // ssBottom
            // 
            this.ssBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslMessage});
            this.ssBottom.Location = new System.Drawing.Point(3, 560);
            this.ssBottom.Name = "ssBottom";
            this.ssBottom.Size = new System.Drawing.Size(846, 22);
            this.ssBottom.TabIndex = 211;
            this.ssBottom.Text = "statusStrip1";
            // 
            // sslMessage
            // 
            this.sslMessage.Name = "sslMessage";
            this.sslMessage.Size = new System.Drawing.Size(12, 17);
            this.sslMessage.Text = "-";
            // 
            // lbl_Status
            // 
            this.lbl_Status.AccessibleDescription = "";
            this.lbl_Status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Status.Location = new System.Drawing.Point(76, 2);
            this.lbl_Status.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(70, 30);
            this.lbl_Status.TabIndex = 224;
            this.lbl_Status.Text = "Status";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vsbar_Zoom
            // 
            this.vsbar_Zoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.vsbar_Zoom.Location = new System.Drawing.Point(679, 0);
            this.vsbar_Zoom.Minimum = 1;
            this.vsbar_Zoom.Name = "vsbar_Zoom";
            this.vsbar_Zoom.Size = new System.Drawing.Size(26, 315);
            this.vsbar_Zoom.TabIndex = 223;
            this.vsbar_Zoom.Value = 50;
            this.vsbar_Zoom.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbar_Zoom_Scroll);
            // 
            // btn_Ctrl2
            // 
            this.btn_Ctrl2.AccessibleDescription = "Ctrl 2";
            this.btn_Ctrl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Ctrl2.Location = new System.Drawing.Point(626, 2);
            this.btn_Ctrl2.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Ctrl2.Name = "btn_Ctrl2";
            this.btn_Ctrl2.Size = new System.Drawing.Size(70, 30);
            this.btn_Ctrl2.TabIndex = 222;
            this.btn_Ctrl2.Text = "Ctrl 2";
            this.btn_Ctrl2.UseVisualStyleBackColor = true;
            this.btn_Ctrl2.Click += new System.EventHandler(this.btn_Ctrl2_Click);
            // 
            // btn_Ctrl1
            // 
            this.btn_Ctrl1.AccessibleDescription = "Ctrl 1";
            this.btn_Ctrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Ctrl1.Location = new System.Drawing.Point(552, 2);
            this.btn_Ctrl1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Ctrl1.Name = "btn_Ctrl1";
            this.btn_Ctrl1.Size = new System.Drawing.Size(70, 30);
            this.btn_Ctrl1.TabIndex = 221;
            this.btn_Ctrl1.Text = "Ctrl 1";
            this.btn_Ctrl1.UseVisualStyleBackColor = true;
            this.btn_Ctrl1.Click += new System.EventHandler(this.btn_Ctrl1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lbl_StDev);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbl_Ave);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lbl_Range);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lbl_Max);
            this.groupBox2.Controls.Add(this.lbl_Min);
            this.groupBox2.Location = new System.Drawing.Point(544, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 178);
            this.groupBox2.TabIndex = 220;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.Location = new System.Drawing.Point(5, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 195;
            this.label2.Text = "Min";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "";
            this.label5.Location = new System.Drawing.Point(5, 39);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 196;
            this.label5.Text = "Max";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_StDev
            // 
            this.lbl_StDev.AccessibleDescription = "";
            this.lbl_StDev.Location = new System.Drawing.Point(69, 96);
            this.lbl_StDev.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_StDev.Name = "lbl_StDev";
            this.lbl_StDev.Size = new System.Drawing.Size(60, 15);
            this.lbl_StDev.TabIndex = 204;
            this.lbl_StDev.Text = "0.00000";
            this.lbl_StDev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "";
            this.label6.Location = new System.Drawing.Point(5, 58);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 197;
            this.label6.Text = "Range";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Ave
            // 
            this.lbl_Ave.AccessibleDescription = "";
            this.lbl_Ave.Location = new System.Drawing.Point(69, 77);
            this.lbl_Ave.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Ave.Name = "lbl_Ave";
            this.lbl_Ave.Size = new System.Drawing.Size(60, 15);
            this.lbl_Ave.TabIndex = 203;
            this.lbl_Ave.Text = "0.00000";
            this.lbl_Ave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "";
            this.label7.Location = new System.Drawing.Point(5, 77);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 198;
            this.label7.Text = "Average";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Range
            // 
            this.lbl_Range.AccessibleDescription = "";
            this.lbl_Range.Location = new System.Drawing.Point(69, 58);
            this.lbl_Range.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Range.Name = "lbl_Range";
            this.lbl_Range.Size = new System.Drawing.Size(60, 15);
            this.lbl_Range.TabIndex = 202;
            this.lbl_Range.Text = "0.00000";
            this.lbl_Range.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "";
            this.label8.Location = new System.Drawing.Point(5, 96);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 15);
            this.label8.TabIndex = 199;
            this.label8.Text = "StDev";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Max
            // 
            this.lbl_Max.AccessibleDescription = "";
            this.lbl_Max.Location = new System.Drawing.Point(69, 39);
            this.lbl_Max.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Max.Name = "lbl_Max";
            this.lbl_Max.Size = new System.Drawing.Size(60, 15);
            this.lbl_Max.TabIndex = 201;
            this.lbl_Max.Text = "0.00000";
            this.lbl_Max.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Min
            // 
            this.lbl_Min.AccessibleDescription = "";
            this.lbl_Min.Location = new System.Drawing.Point(69, 20);
            this.lbl_Min.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Min.Name = "lbl_Min";
            this.lbl_Min.Size = new System.Drawing.Size(60, 15);
            this.lbl_Min.TabIndex = 200;
            this.lbl_Min.Text = "0.00000";
            this.lbl_Min.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Measure";
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.btn_SaveToFile);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lbl_Spec);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.lbl_TolPcnt);
            this.groupBox1.Controls.Add(this.btn_ReComputeResult);
            this.groupBox1.Controls.Add(this.lbl_Tol);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.lbl_DotsPerSample);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lbl_WeightSampleCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(269, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox1.Size = new System.Drawing.Size(269, 178);
            this.groupBox1.TabIndex = 219;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Measure";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(40, 75);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 23);
            this.label19.TabIndex = 191;
            this.label19.Text = "(%,mg)";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_SaveToFile
            // 
            this.btn_SaveToFile.AccessibleDescription = "Save File";
            this.btn_SaveToFile.Location = new System.Drawing.Point(119, 130);
            this.btn_SaveToFile.Name = "btn_SaveToFile";
            this.btn_SaveToFile.Size = new System.Drawing.Size(70, 30);
            this.btn_SaveToFile.TabIndex = 170;
            this.btn_SaveToFile.Text = "Save File";
            this.btn_SaveToFile.UseVisualStyleBackColor = true;
            this.btn_SaveToFile.Click += new System.EventHandler(this.btn_SaveToFile_Click);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(114, 47);
            this.label16.Margin = new System.Windows.Forms.Padding(2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 23);
            this.label16.TabIndex = 190;
            this.label16.Text = "(mg)";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "Spec";
            this.label10.Location = new System.Drawing.Point(5, 48);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 23);
            this.label10.TabIndex = 185;
            this.label10.Text = "Spec";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Spec
            // 
            this.lbl_Spec.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_Spec.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Spec.Location = new System.Drawing.Point(193, 48);
            this.lbl_Spec.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Spec.Name = "lbl_Spec";
            this.lbl_Spec.Size = new System.Drawing.Size(70, 23);
            this.lbl_Spec.TabIndex = 186;
            this.lbl_Spec.Text = "50";
            this.lbl_Spec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Spec.Click += new System.EventHandler(this.lbl_Spec_Click);
            // 
            // label14
            // 
            this.label14.AccessibleDescription = "Tol";
            this.label14.Location = new System.Drawing.Point(5, 75);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 23);
            this.label14.TabIndex = 187;
            this.label14.Text = "Tol";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_TolPcnt
            // 
            this.lbl_TolPcnt.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_TolPcnt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_TolPcnt.Location = new System.Drawing.Point(114, 75);
            this.lbl_TolPcnt.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_TolPcnt.Name = "lbl_TolPcnt";
            this.lbl_TolPcnt.Size = new System.Drawing.Size(70, 23);
            this.lbl_TolPcnt.TabIndex = 188;
            this.lbl_TolPcnt.Text = "2";
            this.lbl_TolPcnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_TolPcnt.Click += new System.EventHandler(this.lbl_TolPcnt_Click);
            // 
            // btn_ReComputeResult
            // 
            this.btn_ReComputeResult.AccessibleDescription = "Compute";
            this.btn_ReComputeResult.Location = new System.Drawing.Point(193, 130);
            this.btn_ReComputeResult.Name = "btn_ReComputeResult";
            this.btn_ReComputeResult.Size = new System.Drawing.Size(70, 30);
            this.btn_ReComputeResult.TabIndex = 172;
            this.btn_ReComputeResult.Text = "Compute";
            this.btn_ReComputeResult.UseVisualStyleBackColor = true;
            this.btn_ReComputeResult.Click += new System.EventHandler(this.btn_ReComputeResult_Click);
            // 
            // lbl_Tol
            // 
            this.lbl_Tol.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_Tol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Tol.Location = new System.Drawing.Point(193, 75);
            this.lbl_Tol.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Tol.Name = "lbl_Tol";
            this.lbl_Tol.Size = new System.Drawing.Size(70, 23);
            this.lbl_Tol.TabIndex = 188;
            this.lbl_Tol.Text = "2";
            this.lbl_Tol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Tol.Click += new System.EventHandler(this.lbl_Tol_Click);
            // 
            // label22
            // 
            this.label22.AccessibleDescription = "";
            this.label22.Location = new System.Drawing.Point(133, 20);
            this.label22.Margin = new System.Windows.Forms.Padding(2);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(51, 23);
            this.label22.TabIndex = 199;
            this.label22.Text = "(count)";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_DotsPerSample
            // 
            this.lbl_DotsPerSample.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_DotsPerSample.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_DotsPerSample.Location = new System.Drawing.Point(193, 21);
            this.lbl_DotsPerSample.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_DotsPerSample.Name = "lbl_DotsPerSample";
            this.lbl_DotsPerSample.Size = new System.Drawing.Size(70, 23);
            this.lbl_DotsPerSample.TabIndex = 196;
            this.lbl_DotsPerSample.Text = "10";
            this.lbl_DotsPerSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.AccessibleDescription = "";
            this.label21.Location = new System.Drawing.Point(130, 102);
            this.label21.Margin = new System.Windows.Forms.Padding(2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 23);
            this.label21.TabIndex = 198;
            this.label21.Text = "(count)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AccessibleDescription = "Dots/Sample";
            this.label11.Location = new System.Drawing.Point(5, 20);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 23);
            this.label11.TabIndex = 195;
            this.label11.Text = "Dots/Sample";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_WeightSampleCount
            // 
            this.lbl_WeightSampleCount.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_WeightSampleCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_WeightSampleCount.Location = new System.Drawing.Point(193, 102);
            this.lbl_WeightSampleCount.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_WeightSampleCount.Name = "lbl_WeightSampleCount";
            this.lbl_WeightSampleCount.Size = new System.Drawing.Size(70, 23);
            this.lbl_WeightSampleCount.TabIndex = 159;
            this.lbl_WeightSampleCount.Text = "10";
            this.lbl_WeightSampleCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_WeightSampleCount.Click += new System.EventHandler(this.lbl_WeightMeasureCount_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Measure Count";
            this.label1.Location = new System.Drawing.Point(5, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 23);
            this.label1.TabIndex = 158;
            this.label1.Text = "Measure Count";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zg1
            // 
            this.zg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zg1.Location = new System.Drawing.Point(0, 0);
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            this.zg1.Size = new System.Drawing.Size(679, 315);
            this.zg1.TabIndex = 218;
            this.zg1.UseExtendedPrintDialog = true;
            // 
            // btn_Head1
            // 
            this.btn_Head1.AccessibleDescription = "Head 1";
            this.btn_Head1.Location = new System.Drawing.Point(150, 2);
            this.btn_Head1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Head1.Name = "btn_Head1";
            this.btn_Head1.Size = new System.Drawing.Size(70, 30);
            this.btn_Head1.TabIndex = 215;
            this.btn_Head1.Text = "Head 1";
            this.btn_Head1.UseVisualStyleBackColor = true;
            this.btn_Head1.Click += new System.EventHandler(this.btn_Head1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.lblCurrentNettVolume1);
            this.groupBox3.Controls.Add(this.lblCurrentNettVolume2);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.lbl_CurrentCalName);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.lbl_CurrentCal1);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.lbl_CurrentCal2);
            this.groupBox3.Controls.Add(this.btn_Zero);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lbl_WeightCurrentValue);
            this.groupBox3.Controls.Add(this.btn_Start);
            this.groupBox3.Controls.Add(this.btn_Continue);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 39);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(705, 206);
            this.groupBox3.TabIndex = 217;
            this.groupBox3.TabStop = false;
            // 
            // label15
            // 
            this.label15.AccessibleDescription = "Nett Volume";
            this.label15.Location = new System.Drawing.Point(5, 128);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 23);
            this.label15.TabIndex = 223;
            this.label15.Text = "Nett Volume";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentNettVolume1
            // 
            this.lblCurrentNettVolume1.BackColor = System.Drawing.SystemColors.Control;
            this.lblCurrentNettVolume1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentNettVolume1.Location = new System.Drawing.Point(108, 128);
            this.lblCurrentNettVolume1.Margin = new System.Windows.Forms.Padding(2);
            this.lblCurrentNettVolume1.Name = "lblCurrentNettVolume1";
            this.lblCurrentNettVolume1.Size = new System.Drawing.Size(70, 23);
            this.lblCurrentNettVolume1.TabIndex = 224;
            this.lblCurrentNettVolume1.Text = "0.0000";
            this.lblCurrentNettVolume1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentNettVolume2
            // 
            this.lblCurrentNettVolume2.BackColor = System.Drawing.SystemColors.Control;
            this.lblCurrentNettVolume2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentNettVolume2.Location = new System.Drawing.Point(182, 128);
            this.lblCurrentNettVolume2.Margin = new System.Windows.Forms.Padding(2);
            this.lblCurrentNettVolume2.Name = "lblCurrentNettVolume2";
            this.lblCurrentNettVolume2.Size = new System.Drawing.Size(70, 23);
            this.lblCurrentNettVolume2.TabIndex = 225;
            this.lblCurrentNettVolume2.Text = "0.0000";
            this.lblCurrentNettVolume2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AccessibleDescription = "Density";
            this.label12.Location = new System.Drawing.Point(108, 74);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 23);
            this.label12.TabIndex = 222;
            this.label12.Text = "Head 1";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "Density";
            this.label9.Location = new System.Drawing.Point(182, 74);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 23);
            this.label9.TabIndex = 221;
            this.label9.Text = "Head 2";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CurrentCalName
            // 
            this.lbl_CurrentCalName.AccessibleDescription = "Density";
            this.lbl_CurrentCalName.Location = new System.Drawing.Point(5, 101);
            this.lbl_CurrentCalName.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_CurrentCalName.Name = "lbl_CurrentCalName";
            this.lbl_CurrentCalName.Size = new System.Drawing.Size(70, 23);
            this.lbl_CurrentCalName.TabIndex = 194;
            this.lbl_CurrentCalName.Text = "Density";
            this.lbl_CurrentCalName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_CurrentCal1
            // 
            this.lbl_CurrentCal1.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_CurrentCal1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_CurrentCal1.Location = new System.Drawing.Point(108, 101);
            this.lbl_CurrentCal1.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_CurrentCal1.Name = "lbl_CurrentCal1";
            this.lbl_CurrentCal1.Size = new System.Drawing.Size(70, 23);
            this.lbl_CurrentCal1.TabIndex = 196;
            this.lbl_CurrentCal1.Text = "0.0000";
            this.lbl_CurrentCal1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_CurrentCal2
            // 
            this.lbl_CurrentCal2.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_CurrentCal2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_CurrentCal2.Location = new System.Drawing.Point(182, 101);
            this.lbl_CurrentCal2.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_CurrentCal2.Name = "lbl_CurrentCal2";
            this.lbl_CurrentCal2.Size = new System.Drawing.Size(70, 23);
            this.lbl_CurrentCal2.TabIndex = 197;
            this.lbl_CurrentCal2.Text = "0.0000";
            this.lbl_CurrentCal2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Zero
            // 
            this.btn_Zero.AccessibleDescription = "Zero";
            this.btn_Zero.Location = new System.Drawing.Point(182, 47);
            this.btn_Zero.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Zero.Name = "btn_Zero";
            this.btn_Zero.Size = new System.Drawing.Size(70, 23);
            this.btn_Zero.TabIndex = 190;
            this.btn_Zero.Text = "Zero";
            this.btn_Zero.UseVisualStyleBackColor = true;
            this.btn_Zero.Click += new System.EventHandler(this.btn_Zero_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(108, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 23);
            this.label3.TabIndex = 189;
            this.label3.Text = "(mg)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "Weight";
            this.label4.Location = new System.Drawing.Point(5, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 23);
            this.label4.TabIndex = 162;
            this.label4.Text = "Weight";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_WeightCurrentValue
            // 
            this.lbl_WeightCurrentValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_WeightCurrentValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_WeightCurrentValue.Location = new System.Drawing.Point(182, 20);
            this.lbl_WeightCurrentValue.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_WeightCurrentValue.Name = "lbl_WeightCurrentValue";
            this.lbl_WeightCurrentValue.Size = new System.Drawing.Size(70, 23);
            this.lbl_WeightCurrentValue.TabIndex = 161;
            this.lbl_WeightCurrentValue.Text = "0.0000";
            this.lbl_WeightCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Start
            // 
            this.btn_Start.AccessibleDescription = "Start";
            this.btn_Start.Location = new System.Drawing.Point(108, 168);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(70, 30);
            this.btn_Start.TabIndex = 168;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Continue
            // 
            this.btn_Continue.AccessibleDescription = "Continue";
            this.btn_Continue.Location = new System.Drawing.Point(182, 168);
            this.btn_Continue.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(70, 30);
            this.btn_Continue.TabIndex = 169;
            this.btn_Continue.Text = "Continue";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Visible = false;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Head2
            // 
            this.btn_Head2.AccessibleDescription = "Head 2";
            this.btn_Head2.Location = new System.Drawing.Point(224, 2);
            this.btn_Head2.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Head2.Name = "btn_Head2";
            this.btn_Head2.Size = new System.Drawing.Size(70, 30);
            this.btn_Head2.TabIndex = 214;
            this.btn_Head2.Text = "Head 2";
            this.btn_Head2.UseVisualStyleBackColor = true;
            this.btn_Head2.Click += new System.EventHandler(this.btn_Head2_Click);
            // 
            // lbox_Result
            // 
            this.lbox_Result.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbox_Result.FormattingEnabled = true;
            this.lbox_Result.ItemHeight = 14;
            this.lbox_Result.Location = new System.Drawing.Point(708, 39);
            this.lbox_Result.Name = "lbox_Result";
            this.lbox_Result.Size = new System.Drawing.Size(141, 521);
            this.lbox_Result.TabIndex = 211;
            // 
            // btn_Setting
            // 
            this.btn_Setting.AccessibleDescription = "Setting";
            this.btn_Setting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Setting.Location = new System.Drawing.Point(700, 2);
            this.btn_Setting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(70, 30);
            this.btn_Setting.TabIndex = 216;
            this.btn_Setting.Text = "Setting";
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // label13
            // 
            this.label13.AccessibleDescription = "Head";
            this.label13.Location = new System.Drawing.Point(2, 2);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 30);
            this.label13.TabIndex = 213;
            this.label13.Text = "Head";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Close
            // 
            this.btn_Close.AccessibleDescription = "Close";
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(774, 2);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(70, 30);
            this.btn_Close.TabIndex = 212;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_Status);
            this.panel2.Controls.Add(this.btn_Ctrl2);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.btn_Ctrl1);
            this.panel2.Controls.Add(this.btn_Head2);
            this.panel2.Controls.Add(this.btn_Head1);
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Controls.Add(this.btn_Setting);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(846, 36);
            this.panel2.TabIndex = 213;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.zg1);
            this.panel3.Controls.Add(this.vsbar_Zoom);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 245);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(705, 315);
            this.panel3.TabIndex = 214;
            // 
            // frm_DispCore_WeightMeasure
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(852, 585);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lbox_Result);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ssBottom);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DispCore_WeightMeasure";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frm_DispCore_WeightMeasure";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_DispCore_WeightMeasure_FormClosing);
            this.Load += new System.EventHandler(this.frmWeightSetup_Load);
            this.ssBottom.ResumeLayout(false);
            this.ssBottom.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmr_WeightDisplay;
        private System.Windows.Forms.Timer tmr_Run;
        private System.Windows.Forms.StatusStrip ssBottom;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.VScrollBar vsbar_Zoom;
        private System.Windows.Forms.Button btn_Ctrl2;
        private System.Windows.Forms.Button btn_Ctrl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_StDev;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Ave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Range;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_Max;
        private System.Windows.Forms.Label lbl_Min;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btn_SaveToFile;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_Spec;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_TolPcnt;
        private System.Windows.Forms.Button btn_ReComputeResult;
        private System.Windows.Forms.Label lbl_Tol;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbl_DotsPerSample;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_WeightSampleCount;
        private System.Windows.Forms.Label label1;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.Button btn_Head1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbl_CurrentCalName;
        private System.Windows.Forms.Label lbl_CurrentCal1;
        private System.Windows.Forms.Label lbl_CurrentCal2;
        private System.Windows.Forms.Button btn_Zero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_WeightCurrentValue;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Continue;
        private System.Windows.Forms.Button btn_Head2;
        private System.Windows.Forms.ListBox lbox_Result;
        private System.Windows.Forms.Button btn_Setting;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.ToolStripStatusLabel sslMessage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblCurrentNettVolume1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCurrentNettVolume2;
    }
}