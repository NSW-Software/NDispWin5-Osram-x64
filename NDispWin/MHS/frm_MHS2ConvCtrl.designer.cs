﻿namespace NDispWin
{
    partial class frm_MHS2ConvCtrl
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
            this.lbl_OutSt = new System.Windows.Forms.Label();
            this.lbl_ProSt = new System.Windows.Forms.Label();
            this.lbl_ConvSt = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbox_Pro = new System.Windows.Forms.GroupBox();
            this.btn_LoadPro = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_StopInput = new System.Windows.Forms.Button();
            this.btn_SkipDisp = new System.Windows.Forms.Button();
            this.btn_DispEndStop = new System.Windows.Forms.Button();
            this.btn_UnloadStop = new System.Windows.Forms.Button();
            this.btn_LoadPre = new System.Windows.Forms.Button();
            this.btn_Unload = new System.Windows.Forms.Button();
            this.btn_Return = new System.Windows.Forms.Button();
            this.tmr_Display = new System.Windows.Forms.Timer(this.components);
            this.gbox_Pre = new System.Windows.Forms.GroupBox();
            this.lbl_PreSt = new System.Windows.Forms.Label();
            this.btn_LoadBuf2 = new System.Windows.Forms.Button();
            this.btn_LoadBuf1 = new System.Windows.Forms.Button();
            this.gbox_Buf2 = new System.Windows.Forms.GroupBox();
            this.lbl_Buf2St = new System.Windows.Forms.Label();
            this.gbox_Buf1 = new System.Windows.Forms.GroupBox();
            this.lbl_Buf1St = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_InStatus = new System.Windows.Forms.Label();
            this.gbxPos = new System.Windows.Forms.GroupBox();
            this.btnLoadPos = new System.Windows.Forms.Button();
            this.lbl_PosSt = new System.Windows.Forms.Label();
            this.lbl_Note = new System.Windows.Forms.Label();
            this.lbl_RightSmemaMcReady = new System.Windows.Forms.Label();
            this.lblLeftSmemaBdReady = new System.Windows.Forms.Label();
            this.gboxSmemaLeftIn = new System.Windows.Forms.GroupBox();
            this.lblLeftSmemaMcReady = new System.Windows.Forms.Label();
            this.btn_UL_RecvBoard = new System.Windows.Forms.Button();
            this.gboxSmemaRightOut = new System.Windows.Forms.GroupBox();
            this.btn_DL_SendBoard = new System.Windows.Forms.Button();
            this.lbl_RightSmemaBdReady = new System.Windows.Forms.Label();
            this.pnl_PostStation = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lbl_In2St = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbl_Pos2St = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lbl_Out2St = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbox_Pro.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbox_Pre.SuspendLayout();
            this.gbox_Buf2.SuspendLayout();
            this.gbox_Buf1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbxPos.SuspendLayout();
            this.gboxSmemaLeftIn.SuspendLayout();
            this.gboxSmemaRightOut.SuspendLayout();
            this.pnl_PostStation.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_OutSt
            // 
            this.lbl_OutSt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_OutSt.Location = new System.Drawing.Point(6, 17);
            this.lbl_OutSt.Name = "lbl_OutSt";
            this.lbl_OutSt.Size = new System.Drawing.Size(88, 25);
            this.lbl_OutSt.TabIndex = 318;
            this.lbl_OutSt.Text = "Status";
            this.lbl_OutSt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ProSt
            // 
            this.lbl_ProSt.AccessibleDescription = "";
            this.lbl_ProSt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_ProSt.Location = new System.Drawing.Point(6, 17);
            this.lbl_ProSt.Name = "lbl_ProSt";
            this.lbl_ProSt.Size = new System.Drawing.Size(88, 25);
            this.lbl_ProSt.TabIndex = 316;
            this.lbl_ProSt.Text = "Status";
            this.lbl_ProSt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_ProSt.DoubleClick += new System.EventHandler(this.lbl_ProSt_DoubleClick);
            // 
            // lbl_ConvSt
            // 
            this.lbl_ConvSt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_ConvSt.Location = new System.Drawing.Point(6, 18);
            this.lbl_ConvSt.Name = "lbl_ConvSt";
            this.lbl_ConvSt.Size = new System.Drawing.Size(88, 25);
            this.lbl_ConvSt.TabIndex = 315;
            this.lbl_ConvSt.Text = "Status";
            this.lbl_ConvSt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_ConvSt.DoubleClick += new System.EventHandler(this.lbl_ConvSt_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "";
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lbl_OutSt);
            this.groupBox1.Location = new System.Drawing.Point(666, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 64);
            this.groupBox1.TabIndex = 322;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Out";
            // 
            // gbox_Pro
            // 
            this.gbox_Pro.AccessibleDescription = "";
            this.gbox_Pro.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbox_Pro.AutoSize = true;
            this.gbox_Pro.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbox_Pro.Controls.Add(this.lbl_ProSt);
            this.gbox_Pro.Controls.Add(this.btn_LoadPro);
            this.gbox_Pro.Location = new System.Drawing.Point(454, 76);
            this.gbox_Pro.Name = "gbox_Pro";
            this.gbox_Pro.Size = new System.Drawing.Size(100, 110);
            this.gbox_Pro.TabIndex = 323;
            this.gbox_Pro.TabStop = false;
            this.gbox_Pro.Text = "Pro";
            // 
            // btn_LoadPro
            // 
            this.btn_LoadPro.AccessibleDescription = "Load Pro";
            this.btn_LoadPro.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_LoadPro.Location = new System.Drawing.Point(6, 45);
            this.btn_LoadPro.Name = "btn_LoadPro";
            this.btn_LoadPro.Size = new System.Drawing.Size(88, 40);
            this.btn_LoadPro.TabIndex = 329;
            this.btn_LoadPro.Text = "Load Pro";
            this.btn_LoadPro.UseVisualStyleBackColor = true;
            this.btn_LoadPro.Click += new System.EventHandler(this.btn_LoadPro_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.AccessibleDescription = "Conveyor";
            this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox4.AutoSize = true;
            this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(this.lbl_ConvSt);
            this.groupBox4.Location = new System.Drawing.Point(348, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(100, 65);
            this.groupBox4.TabIndex = 324;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Conveyor";
            // 
            // btn_StopInput
            // 
            this.btn_StopInput.AccessibleDescription = "Stop Input";
            this.btn_StopInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_StopInput.Location = new System.Drawing.Point(136, 8);
            this.btn_StopInput.Name = "btn_StopInput";
            this.btn_StopInput.Size = new System.Drawing.Size(100, 40);
            this.btn_StopInput.TabIndex = 325;
            this.btn_StopInput.Text = "Stop Input";
            this.btn_StopInput.UseVisualStyleBackColor = true;
            this.btn_StopInput.Click += new System.EventHandler(this.btn_StopInput_Click);
            // 
            // btn_SkipDisp
            // 
            this.btn_SkipDisp.AccessibleDescription = "Skip Disp";
            this.btn_SkipDisp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_SkipDisp.Location = new System.Drawing.Point(242, 8);
            this.btn_SkipDisp.Name = "btn_SkipDisp";
            this.btn_SkipDisp.Size = new System.Drawing.Size(100, 40);
            this.btn_SkipDisp.TabIndex = 326;
            this.btn_SkipDisp.Text = "Skip Disp";
            this.btn_SkipDisp.UseVisualStyleBackColor = true;
            this.btn_SkipDisp.Click += new System.EventHandler(this.btn_SkipDisp_Click);
            // 
            // btn_DispEndStop
            // 
            this.btn_DispEndStop.AccessibleDescription = "Disp End Stop";
            this.btn_DispEndStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_DispEndStop.Location = new System.Drawing.Point(454, 8);
            this.btn_DispEndStop.Name = "btn_DispEndStop";
            this.btn_DispEndStop.Size = new System.Drawing.Size(100, 40);
            this.btn_DispEndStop.TabIndex = 327;
            this.btn_DispEndStop.Text = "Disp End Stop";
            this.btn_DispEndStop.UseVisualStyleBackColor = true;
            this.btn_DispEndStop.Click += new System.EventHandler(this.btn_DispEndStop_Click);
            // 
            // btn_UnloadStop
            // 
            this.btn_UnloadStop.AccessibleDescription = "Unload Stop";
            this.btn_UnloadStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_UnloadStop.Location = new System.Drawing.Point(560, 8);
            this.btn_UnloadStop.Name = "btn_UnloadStop";
            this.btn_UnloadStop.Size = new System.Drawing.Size(100, 40);
            this.btn_UnloadStop.TabIndex = 328;
            this.btn_UnloadStop.Text = "Unload Stop";
            this.btn_UnloadStop.UseVisualStyleBackColor = true;
            this.btn_UnloadStop.Click += new System.EventHandler(this.btn_BoardEndStop_Click);
            // 
            // btn_LoadPre
            // 
            this.btn_LoadPre.AccessibleDescription = "Load Pre";
            this.btn_LoadPre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_LoadPre.Location = new System.Drawing.Point(6, 46);
            this.btn_LoadPre.Name = "btn_LoadPre";
            this.btn_LoadPre.Size = new System.Drawing.Size(88, 40);
            this.btn_LoadPre.TabIndex = 330;
            this.btn_LoadPre.Text = "Load Pre";
            this.btn_LoadPre.UseVisualStyleBackColor = true;
            this.btn_LoadPre.Click += new System.EventHandler(this.btn_LoadPre_Click);
            // 
            // btn_Unload
            // 
            this.btn_Unload.AccessibleDescription = "Unload";
            this.btn_Unload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_Unload.Location = new System.Drawing.Point(666, 142);
            this.btn_Unload.Name = "btn_Unload";
            this.btn_Unload.Size = new System.Drawing.Size(100, 40);
            this.btn_Unload.TabIndex = 331;
            this.btn_Unload.Text = "Unload";
            this.btn_Unload.UseVisualStyleBackColor = true;
            this.btn_Unload.Click += new System.EventHandler(this.btn_Unload_Click);
            // 
            // btn_Return
            // 
            this.btn_Return.AccessibleDescription = "Return";
            this.btn_Return.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_Return.Location = new System.Drawing.Point(30, 142);
            this.btn_Return.Name = "btn_Return";
            this.btn_Return.Size = new System.Drawing.Size(100, 40);
            this.btn_Return.TabIndex = 332;
            this.btn_Return.Text = "Return";
            this.btn_Return.UseVisualStyleBackColor = true;
            this.btn_Return.Click += new System.EventHandler(this.btn_Return_Click);
            // 
            // tmr_Display
            // 
            this.tmr_Display.Enabled = true;
            this.tmr_Display.Interval = 500;
            this.tmr_Display.Tick += new System.EventHandler(this.tmr_Display_Tick);
            // 
            // gbox_Pre
            // 
            this.gbox_Pre.AccessibleDescription = "";
            this.gbox_Pre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbox_Pre.AutoSize = true;
            this.gbox_Pre.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbox_Pre.Controls.Add(this.lbl_PreSt);
            this.gbox_Pre.Controls.Add(this.btn_LoadPre);
            this.gbox_Pre.Location = new System.Drawing.Point(348, 75);
            this.gbox_Pre.Name = "gbox_Pre";
            this.gbox_Pre.Size = new System.Drawing.Size(100, 111);
            this.gbox_Pre.TabIndex = 334;
            this.gbox_Pre.TabStop = false;
            this.gbox_Pre.Text = "Pre";
            // 
            // lbl_PreSt
            // 
            this.lbl_PreSt.AccessibleDescription = "";
            this.lbl_PreSt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_PreSt.Location = new System.Drawing.Point(6, 18);
            this.lbl_PreSt.Name = "lbl_PreSt";
            this.lbl_PreSt.Size = new System.Drawing.Size(88, 25);
            this.lbl_PreSt.TabIndex = 316;
            this.lbl_PreSt.Text = "Status";
            this.lbl_PreSt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_PreSt.DoubleClick += new System.EventHandler(this.lbl_PreSt_DoubleClick);
            // 
            // btn_LoadBuf2
            // 
            this.btn_LoadBuf2.AccessibleDescription = "Load Buf2";
            this.btn_LoadBuf2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_LoadBuf2.Location = new System.Drawing.Point(6, 45);
            this.btn_LoadBuf2.Name = "btn_LoadBuf2";
            this.btn_LoadBuf2.Size = new System.Drawing.Size(88, 40);
            this.btn_LoadBuf2.TabIndex = 335;
            this.btn_LoadBuf2.Text = "Load Buf2";
            this.btn_LoadBuf2.UseVisualStyleBackColor = true;
            this.btn_LoadBuf2.Click += new System.EventHandler(this.btn_LoadBuf2_Click);
            // 
            // btn_LoadBuf1
            // 
            this.btn_LoadBuf1.AccessibleDescription = "Load Buf1";
            this.btn_LoadBuf1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_LoadBuf1.Location = new System.Drawing.Point(6, 46);
            this.btn_LoadBuf1.Name = "btn_LoadBuf1";
            this.btn_LoadBuf1.Size = new System.Drawing.Size(88, 40);
            this.btn_LoadBuf1.TabIndex = 336;
            this.btn_LoadBuf1.Text = "Load Buf1";
            this.btn_LoadBuf1.UseVisualStyleBackColor = true;
            this.btn_LoadBuf1.Click += new System.EventHandler(this.btn_LoadBuf1_Click);
            // 
            // gbox_Buf2
            // 
            this.gbox_Buf2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbox_Buf2.AutoSize = true;
            this.gbox_Buf2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbox_Buf2.Controls.Add(this.lbl_Buf2St);
            this.gbox_Buf2.Controls.Add(this.btn_LoadBuf2);
            this.gbox_Buf2.Location = new System.Drawing.Point(242, 76);
            this.gbox_Buf2.Name = "gbox_Buf2";
            this.gbox_Buf2.Size = new System.Drawing.Size(100, 110);
            this.gbox_Buf2.TabIndex = 337;
            this.gbox_Buf2.TabStop = false;
            this.gbox_Buf2.Text = "Buf2";
            // 
            // lbl_Buf2St
            // 
            this.lbl_Buf2St.AccessibleDescription = "";
            this.lbl_Buf2St.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Buf2St.Location = new System.Drawing.Point(6, 17);
            this.lbl_Buf2St.Name = "lbl_Buf2St";
            this.lbl_Buf2St.Size = new System.Drawing.Size(88, 25);
            this.lbl_Buf2St.TabIndex = 316;
            this.lbl_Buf2St.Text = "Status";
            this.lbl_Buf2St.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbox_Buf1
            // 
            this.gbox_Buf1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbox_Buf1.AutoSize = true;
            this.gbox_Buf1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbox_Buf1.Controls.Add(this.lbl_Buf1St);
            this.gbox_Buf1.Controls.Add(this.btn_LoadBuf1);
            this.gbox_Buf1.Location = new System.Drawing.Point(136, 75);
            this.gbox_Buf1.Name = "gbox_Buf1";
            this.gbox_Buf1.Size = new System.Drawing.Size(100, 111);
            this.gbox_Buf1.TabIndex = 335;
            this.gbox_Buf1.TabStop = false;
            this.gbox_Buf1.Text = "Buf1";
            // 
            // lbl_Buf1St
            // 
            this.lbl_Buf1St.AccessibleDescription = "";
            this.lbl_Buf1St.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Buf1St.Location = new System.Drawing.Point(6, 18);
            this.lbl_Buf1St.Name = "lbl_Buf1St";
            this.lbl_Buf1St.Size = new System.Drawing.Size(88, 25);
            this.lbl_Buf1St.TabIndex = 316;
            this.lbl_Buf1St.Text = "Status";
            this.lbl_Buf1St.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.lbl_InStatus);
            this.groupBox2.Location = new System.Drawing.Point(30, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 64);
            this.groupBox2.TabIndex = 323;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "In";
            // 
            // lbl_InStatus
            // 
            this.lbl_InStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_InStatus.Location = new System.Drawing.Point(6, 17);
            this.lbl_InStatus.Name = "lbl_InStatus";
            this.lbl_InStatus.Size = new System.Drawing.Size(88, 25);
            this.lbl_InStatus.TabIndex = 318;
            this.lbl_InStatus.Text = "Status";
            this.lbl_InStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxPos
            // 
            this.gbxPos.AccessibleDescription = "";
            this.gbxPos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbxPos.AutoSize = true;
            this.gbxPos.Controls.Add(this.btnLoadPos);
            this.gbxPos.Controls.Add(this.lbl_PosSt);
            this.gbxPos.Location = new System.Drawing.Point(560, 75);
            this.gbxPos.Name = "gbxPos";
            this.gbxPos.Size = new System.Drawing.Size(100, 111);
            this.gbxPos.TabIndex = 338;
            this.gbxPos.TabStop = false;
            this.gbxPos.Text = "Pos";
            // 
            // btnLoadPos
            // 
            this.btnLoadPos.AccessibleDescription = "Load Pos";
            this.btnLoadPos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLoadPos.Location = new System.Drawing.Point(6, 46);
            this.btnLoadPos.Name = "btnLoadPos";
            this.btnLoadPos.Size = new System.Drawing.Size(88, 40);
            this.btnLoadPos.TabIndex = 330;
            this.btnLoadPos.Text = "Load Pos";
            this.btnLoadPos.UseVisualStyleBackColor = true;
            this.btnLoadPos.Click += new System.EventHandler(this.btnLoadPos_Click);
            // 
            // lbl_PosSt
            // 
            this.lbl_PosSt.AccessibleDescription = "";
            this.lbl_PosSt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_PosSt.Location = new System.Drawing.Point(6, 17);
            this.lbl_PosSt.Name = "lbl_PosSt";
            this.lbl_PosSt.Size = new System.Drawing.Size(88, 25);
            this.lbl_PosSt.TabIndex = 316;
            this.lbl_PosSt.Text = "Status";
            this.lbl_PosSt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Note
            // 
            this.lbl_Note.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_Note.Location = new System.Drawing.Point(666, 8);
            this.lbl_Note.Name = "lbl_Note";
            this.lbl_Note.Size = new System.Drawing.Size(100, 18);
            this.lbl_Note.TabIndex = 339;
            this.lbl_Note.Text = "label1";
            this.lbl_Note.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RightSmemaMcReady
            // 
            this.lbl_RightSmemaMcReady.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_RightSmemaMcReady.Location = new System.Drawing.Point(6, 59);
            this.lbl_RightSmemaMcReady.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_RightSmemaMcReady.Name = "lbl_RightSmemaMcReady";
            this.lbl_RightSmemaMcReady.Size = new System.Drawing.Size(88, 25);
            this.lbl_RightSmemaMcReady.TabIndex = 345;
            this.lbl_RightSmemaMcReady.Text = "Mc Rdy";
            this.lbl_RightSmemaMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_RightSmemaMcReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_RightSmemaMcReady_MouseDown);
            this.lbl_RightSmemaMcReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_RightSmemaMcReady_MouseUp);
            // 
            // lblLeftSmemaBdReady
            // 
            this.lblLeftSmemaBdReady.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLeftSmemaBdReady.Location = new System.Drawing.Point(6, 35);
            this.lblLeftSmemaBdReady.Name = "lblLeftSmemaBdReady";
            this.lblLeftSmemaBdReady.Size = new System.Drawing.Size(88, 25);
            this.lblLeftSmemaBdReady.TabIndex = 344;
            this.lblLeftSmemaBdReady.Text = "Bd Rdy";
            this.lblLeftSmemaBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftSmemaBdReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblLeftSmemaBdReady_MouseDown);
            this.lblLeftSmemaBdReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblLeftSmemaBdReady_MouseUp);
            // 
            // gboxSmemaLeftIn
            // 
            this.gboxSmemaLeftIn.AccessibleDescription = "";
            this.gboxSmemaLeftIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gboxSmemaLeftIn.Controls.Add(this.lblLeftSmemaMcReady);
            this.gboxSmemaLeftIn.Controls.Add(this.btn_UL_RecvBoard);
            this.gboxSmemaLeftIn.Controls.Add(this.lblLeftSmemaBdReady);
            this.gboxSmemaLeftIn.Location = new System.Drawing.Point(30, 188);
            this.gboxSmemaLeftIn.Name = "gboxSmemaLeftIn";
            this.gboxSmemaLeftIn.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gboxSmemaLeftIn.Size = new System.Drawing.Size(100, 134);
            this.gboxSmemaLeftIn.TabIndex = 346;
            this.gboxSmemaLeftIn.TabStop = false;
            this.gboxSmemaLeftIn.Text = "Smema Left (Bd In)";
            // 
            // lblLeftSmemaMcReady
            // 
            this.lblLeftSmemaMcReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftSmemaMcReady.Location = new System.Drawing.Point(6, 60);
            this.lblLeftSmemaMcReady.Name = "lblLeftSmemaMcReady";
            this.lblLeftSmemaMcReady.Size = new System.Drawing.Size(88, 25);
            this.lblLeftSmemaMcReady.TabIndex = 345;
            this.lblLeftSmemaMcReady.Text = "Mc Rdy";
            this.lblLeftSmemaMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_UL_RecvBoard
            // 
            this.btn_UL_RecvBoard.Location = new System.Drawing.Point(6, 88);
            this.btn_UL_RecvBoard.Name = "btn_UL_RecvBoard";
            this.btn_UL_RecvBoard.Size = new System.Drawing.Size(88, 28);
            this.btn_UL_RecvBoard.TabIndex = 350;
            this.btn_UL_RecvBoard.Text = "Recv Bd";
            this.btn_UL_RecvBoard.UseVisualStyleBackColor = true;
            this.btn_UL_RecvBoard.Click += new System.EventHandler(this.btn_UL_RecvBoard_Click);
            // 
            // gboxSmemaRightOut
            // 
            this.gboxSmemaRightOut.AccessibleDescription = "";
            this.gboxSmemaRightOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gboxSmemaRightOut.AutoSize = true;
            this.gboxSmemaRightOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gboxSmemaRightOut.Controls.Add(this.btn_DL_SendBoard);
            this.gboxSmemaRightOut.Controls.Add(this.lbl_RightSmemaBdReady);
            this.gboxSmemaRightOut.Controls.Add(this.lbl_RightSmemaMcReady);
            this.gboxSmemaRightOut.Location = new System.Drawing.Point(666, 188);
            this.gboxSmemaRightOut.Name = "gboxSmemaRightOut";
            this.gboxSmemaRightOut.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gboxSmemaRightOut.Size = new System.Drawing.Size(100, 138);
            this.gboxSmemaRightOut.TabIndex = 347;
            this.gboxSmemaRightOut.TabStop = false;
            this.gboxSmemaRightOut.Text = "Smema Right (Bd Out)";
            // 
            // btn_DL_SendBoard
            // 
            this.btn_DL_SendBoard.Location = new System.Drawing.Point(6, 88);
            this.btn_DL_SendBoard.Name = "btn_DL_SendBoard";
            this.btn_DL_SendBoard.Size = new System.Drawing.Size(88, 28);
            this.btn_DL_SendBoard.TabIndex = 351;
            this.btn_DL_SendBoard.Text = "Send Bd";
            this.btn_DL_SendBoard.UseVisualStyleBackColor = true;
            this.btn_DL_SendBoard.Click += new System.EventHandler(this.btn_DL_SendBoard_Click);
            // 
            // lbl_RightSmemaBdReady
            // 
            this.lbl_RightSmemaBdReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_RightSmemaBdReady.Location = new System.Drawing.Point(6, 35);
            this.lbl_RightSmemaBdReady.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_RightSmemaBdReady.Name = "lbl_RightSmemaBdReady";
            this.lbl_RightSmemaBdReady.Size = new System.Drawing.Size(88, 25);
            this.lbl_RightSmemaBdReady.TabIndex = 349;
            this.lbl_RightSmemaBdReady.Text = "Bd Rdy";
            this.lbl_RightSmemaBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_PostStation
            // 
            this.pnl_PostStation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnl_PostStation.Controls.Add(this.groupBox7);
            this.pnl_PostStation.Controls.Add(this.groupBox5);
            this.pnl_PostStation.Controls.Add(this.groupBox6);
            this.pnl_PostStation.Location = new System.Drawing.Point(242, 234);
            this.pnl_PostStation.Name = "pnl_PostStation";
            this.pnl_PostStation.Size = new System.Drawing.Size(312, 118);
            this.pnl_PostStation.TabIndex = 350;
            // 
            // groupBox7
            // 
            this.groupBox7.AutoSize = true;
            this.groupBox7.Controls.Add(this.lbl_In2St);
            this.groupBox7.Location = new System.Drawing.Point(0, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(100, 107);
            this.groupBox7.TabIndex = 342;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "In2";
            // 
            // lbl_In2St
            // 
            this.lbl_In2St.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_In2St.Location = new System.Drawing.Point(6, 18);
            this.lbl_In2St.Name = "lbl_In2St";
            this.lbl_In2St.Size = new System.Drawing.Size(88, 25);
            this.lbl_In2St.TabIndex = 351;
            this.lbl_In2St.Text = "Status";
            this.lbl_In2St.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.AccessibleDescription = "";
            this.groupBox5.AutoSize = true;
            this.groupBox5.Controls.Add(this.lbl_Pos2St);
            this.groupBox5.Location = new System.Drawing.Point(106, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(100, 110);
            this.groupBox5.TabIndex = 341;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Pos2";
            // 
            // lbl_Pos2St
            // 
            this.lbl_Pos2St.AccessibleDescription = "";
            this.lbl_Pos2St.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Pos2St.Location = new System.Drawing.Point(6, 17);
            this.lbl_Pos2St.Name = "lbl_Pos2St";
            this.lbl_Pos2St.Size = new System.Drawing.Size(88, 25);
            this.lbl_Pos2St.TabIndex = 316;
            this.lbl_Pos2St.Text = "Status";
            this.lbl_Pos2St.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.AccessibleDescription = "";
            this.groupBox6.AutoSize = true;
            this.groupBox6.Controls.Add(this.lbl_Out2St);
            this.groupBox6.Location = new System.Drawing.Point(212, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(100, 64);
            this.groupBox6.TabIndex = 339;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Out2";
            // 
            // lbl_Out2St
            // 
            this.lbl_Out2St.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Out2St.Location = new System.Drawing.Point(6, 17);
            this.lbl_Out2St.Name = "lbl_Out2St";
            this.lbl_Out2St.Size = new System.Drawing.Size(88, 25);
            this.lbl_Out2St.TabIndex = 318;
            this.lbl_Out2St.Text = "Status";
            this.lbl_Out2St.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_MHS2ConvCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(796, 351);
            this.Controls.Add(this.pnl_PostStation);
            this.Controls.Add(this.gboxSmemaRightOut);
            this.Controls.Add(this.gboxSmemaLeftIn);
            this.Controls.Add(this.lbl_Note);
            this.Controls.Add(this.gbxPos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbox_Buf1);
            this.Controls.Add(this.gbox_Buf2);
            this.Controls.Add(this.gbox_Pre);
            this.Controls.Add(this.btn_Return);
            this.Controls.Add(this.btn_Unload);
            this.Controls.Add(this.btn_UnloadStop);
            this.Controls.Add(this.btn_DispEndStop);
            this.Controls.Add(this.btn_SkipDisp);
            this.Controls.Add(this.btn_StopInput);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gbox_Pro);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_MHS2ConvCtrl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frm_MHS2ConvCtrl";
            this.Load += new System.EventHandler(this.frm_ConvCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.gbox_Pro.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.gbox_Pre.ResumeLayout(false);
            this.gbox_Buf2.ResumeLayout(false);
            this.gbox_Buf1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbxPos.ResumeLayout(false);
            this.gboxSmemaLeftIn.ResumeLayout(false);
            this.gboxSmemaRightOut.ResumeLayout(false);
            this.pnl_PostStation.ResumeLayout(false);
            this.pnl_PostStation.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_OutSt;
        private System.Windows.Forms.Label lbl_ProSt;
        private System.Windows.Forms.Label lbl_ConvSt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbox_Pro;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_StopInput;
        private System.Windows.Forms.Button btn_SkipDisp;
        private System.Windows.Forms.Button btn_DispEndStop;
        private System.Windows.Forms.Button btn_UnloadStop;
        private System.Windows.Forms.Button btn_LoadPro;
        private System.Windows.Forms.Button btn_LoadPre;
        private System.Windows.Forms.Button btn_Unload;
        private System.Windows.Forms.Button btn_Return;
        private System.Windows.Forms.Timer tmr_Display;
        private System.Windows.Forms.GroupBox gbox_Pre;
        private System.Windows.Forms.Label lbl_PreSt;
        private System.Windows.Forms.Button btn_LoadBuf2;
        private System.Windows.Forms.Button btn_LoadBuf1;
        private System.Windows.Forms.GroupBox gbox_Buf2;
        private System.Windows.Forms.Label lbl_Buf2St;
        private System.Windows.Forms.GroupBox gbox_Buf1;
        private System.Windows.Forms.Label lbl_Buf1St;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_InStatus;
        private System.Windows.Forms.GroupBox gbxPos;
        private System.Windows.Forms.Label lbl_PosSt;
        private System.Windows.Forms.Label lbl_Note;
        private System.Windows.Forms.Label lbl_RightSmemaMcReady;
        private System.Windows.Forms.Label lblLeftSmemaBdReady;
        private System.Windows.Forms.GroupBox gboxSmemaLeftIn;
        private System.Windows.Forms.GroupBox gboxSmemaRightOut;
        private System.Windows.Forms.Label lblLeftSmemaMcReady;
        private System.Windows.Forms.Label lbl_RightSmemaBdReady;
        private System.Windows.Forms.Button btn_UL_RecvBoard;
        private System.Windows.Forms.Button btn_DL_SendBoard;
        private System.Windows.Forms.Panel pnl_PostStation;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lbl_Pos2St;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lbl_Out2St;
        private System.Windows.Forms.Label lbl_In2St;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnLoadPos;
    }
}