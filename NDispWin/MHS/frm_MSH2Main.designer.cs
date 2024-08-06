﻿namespace NDispWin
{
    partial class frmMHS2Main
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
            this.btn_ConvIO = new System.Windows.Forms.Button();
            this.btn_ElevIO = new System.Windows.Forms.Button();
            this.btn_ElevSetup = new System.Windows.Forms.Button();
            this.btn_ElevMotorParam = new System.Windows.Forms.Button();
            this.btn_ConvParam = new System.Windows.Forms.Button();
            this.pnl_Control = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ProSt = new System.Windows.Forms.Button();
            this.btn_PreSt = new System.Windows.Forms.Button();
            this.cbox_ProcessTime = new System.Windows.Forms.CheckBox();
            this.lbl_ProcessTime = new System.Windows.Forms.Label();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_InitElev = new System.Windows.Forms.Button();
            this.btn_InitConv = new System.Windows.Forms.Button();
            this.btn_InitAll = new System.Windows.Forms.Button();
            this.btn_Load = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.tmr_Run = new System.Windows.Forms.Timer(this.components);
            this.lblRightSmema_McReady = new System.Windows.Forms.Label();
            this.lblLeftSmema_BdReady = new System.Windows.Forms.Label();
            this.btnRightSmema_BdReady = new System.Windows.Forms.Button();
            this.btnLeftSmema_McReady = new System.Windows.Forms.Button();
            this.btn_Control = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Config = new System.Windows.Forms.Button();
            this.btn_LULBypass = new System.Windows.Forms.Button();
            this.tmr_05s = new System.Windows.Forms.Timer(this.components);
            this.btn_SaveAs = new System.Windows.Forms.Button();
            this.pnlLeftSmema = new System.Windows.Forms.Panel();
            this.pnlRightSmema = new System.Windows.Forms.Panel();
            this.cbNewSeq = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlLeftSmema.SuspendLayout();
            this.pnlRightSmema.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ConvIO
            // 
            this.btn_ConvIO.AccessibleDescription = "Conv IO";
            this.btn_ConvIO.Location = new System.Drawing.Point(170, 8);
            this.btn_ConvIO.Name = "btn_ConvIO";
            this.btn_ConvIO.Size = new System.Drawing.Size(80, 30);
            this.btn_ConvIO.TabIndex = 0;
            this.btn_ConvIO.Text = "Conv IO";
            this.btn_ConvIO.UseVisualStyleBackColor = true;
            this.btn_ConvIO.Click += new System.EventHandler(this.btn_ConvIO_Click);
            // 
            // btn_ElevIO
            // 
            this.btn_ElevIO.AccessibleDescription = "Elev IO";
            this.btn_ElevIO.Location = new System.Drawing.Point(413, 8);
            this.btn_ElevIO.Name = "btn_ElevIO";
            this.btn_ElevIO.Size = new System.Drawing.Size(80, 30);
            this.btn_ElevIO.TabIndex = 1;
            this.btn_ElevIO.Text = "Elev IO";
            this.btn_ElevIO.UseVisualStyleBackColor = true;
            this.btn_ElevIO.Click += new System.EventHandler(this.btn_ElevIO_Click);
            // 
            // btn_ElevSetup
            // 
            this.btn_ElevSetup.AccessibleDescription = "Elev Setup";
            this.btn_ElevSetup.Location = new System.Drawing.Point(251, 8);
            this.btn_ElevSetup.Name = "btn_ElevSetup";
            this.btn_ElevSetup.Size = new System.Drawing.Size(80, 30);
            this.btn_ElevSetup.TabIndex = 2;
            this.btn_ElevSetup.Text = "Elev Setup";
            this.btn_ElevSetup.UseVisualStyleBackColor = true;
            this.btn_ElevSetup.Click += new System.EventHandler(this.btn_ElevSetup_Click);
            // 
            // btn_ElevMotorParam
            // 
            this.btn_ElevMotorParam.AccessibleDescription = "Motor Para";
            this.btn_ElevMotorParam.Location = new System.Drawing.Point(332, 8);
            this.btn_ElevMotorParam.Name = "btn_ElevMotorParam";
            this.btn_ElevMotorParam.Size = new System.Drawing.Size(80, 30);
            this.btn_ElevMotorParam.TabIndex = 3;
            this.btn_ElevMotorParam.Text = "Motor Para";
            this.btn_ElevMotorParam.UseVisualStyleBackColor = true;
            this.btn_ElevMotorParam.Click += new System.EventHandler(this.btn_ElevMotorParam_Click);
            // 
            // btn_ConvParam
            // 
            this.btn_ConvParam.AccessibleDescription = "Conv Param";
            this.btn_ConvParam.Location = new System.Drawing.Point(89, 8);
            this.btn_ConvParam.Name = "btn_ConvParam";
            this.btn_ConvParam.Size = new System.Drawing.Size(80, 30);
            this.btn_ConvParam.TabIndex = 4;
            this.btn_ConvParam.Text = "Conv Param";
            this.btn_ConvParam.UseVisualStyleBackColor = true;
            this.btn_ConvParam.Click += new System.EventHandler(this.btn_ConvParam_Click);
            // 
            // pnl_Control
            // 
            this.pnl_Control.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_Control.Location = new System.Drawing.Point(8, 44);
            this.pnl_Control.Name = "pnl_Control";
            this.pnl_Control.Size = new System.Drawing.Size(800, 600);
            this.pnl_Control.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.cbNewSeq);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btn_InitElev);
            this.panel1.Controls.Add(this.btn_InitConv);
            this.panel1.Controls.Add(this.btn_InitAll);
            this.panel1.Location = new System.Drawing.Point(814, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 588);
            this.panel1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Dry Run";
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btn_ProSt);
            this.groupBox1.Controls.Add(this.btn_PreSt);
            this.groupBox1.Controls.Add(this.cbox_ProcessTime);
            this.groupBox1.Controls.Add(this.lbl_ProcessTime);
            this.groupBox1.Controls.Add(this.btn_Stop);
            this.groupBox1.Controls.Add(this.btn_Start);
            this.groupBox1.Location = new System.Drawing.Point(3, 417);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 168);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dry Run";
            // 
            // btn_ProSt
            // 
            this.btn_ProSt.Location = new System.Drawing.Point(87, 48);
            this.btn_ProSt.Name = "btn_ProSt";
            this.btn_ProSt.Size = new System.Drawing.Size(75, 23);
            this.btn_ProSt.TabIndex = 360;
            this.btn_ProSt.Text = "Pro St";
            this.btn_ProSt.UseVisualStyleBackColor = true;
            this.btn_ProSt.Click += new System.EventHandler(this.btn_ProSt_Click);
            // 
            // btn_PreSt
            // 
            this.btn_PreSt.Location = new System.Drawing.Point(6, 48);
            this.btn_PreSt.Name = "btn_PreSt";
            this.btn_PreSt.Size = new System.Drawing.Size(75, 23);
            this.btn_PreSt.TabIndex = 359;
            this.btn_PreSt.Text = "Pre St";
            this.btn_PreSt.UseVisualStyleBackColor = true;
            this.btn_PreSt.Click += new System.EventHandler(this.btn_PreSt_Click);
            // 
            // cbox_ProcessTime
            // 
            this.cbox_ProcessTime.AccessibleDescription = "Process Time";
            this.cbox_ProcessTime.AutoSize = true;
            this.cbox_ProcessTime.Checked = true;
            this.cbox_ProcessTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbox_ProcessTime.Location = new System.Drawing.Point(6, 24);
            this.cbox_ProcessTime.Name = "cbox_ProcessTime";
            this.cbox_ProcessTime.Size = new System.Drawing.Size(118, 22);
            this.cbox_ProcessTime.TabIndex = 358;
            this.cbox_ProcessTime.Text = "Process Time";
            this.cbox_ProcessTime.UseVisualStyleBackColor = true;
            this.cbox_ProcessTime.Click += new System.EventHandler(this.cbox_ProcessTime_Click);
            // 
            // lbl_ProcessTime
            // 
            this.lbl_ProcessTime.BackColor = System.Drawing.Color.White;
            this.lbl_ProcessTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_ProcessTime.Location = new System.Drawing.Point(110, 21);
            this.lbl_ProcessTime.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_ProcessTime.Name = "lbl_ProcessTime";
            this.lbl_ProcessTime.Size = new System.Drawing.Size(52, 23);
            this.lbl_ProcessTime.TabIndex = 356;
            this.lbl_ProcessTime.Text = "---";
            this.lbl_ProcessTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_ProcessTime.Click += new System.EventHandler(this.lbl_DryRunProcessTime_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.AccessibleDescription = "Stop";
            this.btn_Stop.Location = new System.Drawing.Point(87, 94);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 40);
            this.btn_Stop.TabIndex = 3;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.AccessibleDescription = "Start";
            this.btn_Start.Location = new System.Drawing.Point(6, 94);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 40);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_InitElev
            // 
            this.btn_InitElev.AccessibleDescription = "Init Elev";
            this.btn_InitElev.Location = new System.Drawing.Point(3, 88);
            this.btn_InitElev.Name = "btn_InitElev";
            this.btn_InitElev.Size = new System.Drawing.Size(168, 30);
            this.btn_InitElev.TabIndex = 4;
            this.btn_InitElev.Text = "Init Elev";
            this.btn_InitElev.UseVisualStyleBackColor = true;
            this.btn_InitElev.Click += new System.EventHandler(this.btn_InitElev_Click);
            // 
            // btn_InitConv
            // 
            this.btn_InitConv.AccessibleDescription = "Init Conv";
            this.btn_InitConv.Location = new System.Drawing.Point(3, 52);
            this.btn_InitConv.Name = "btn_InitConv";
            this.btn_InitConv.Size = new System.Drawing.Size(168, 30);
            this.btn_InitConv.TabIndex = 3;
            this.btn_InitConv.Text = "Init Conv";
            this.btn_InitConv.UseVisualStyleBackColor = true;
            this.btn_InitConv.Click += new System.EventHandler(this.btn_InitConv_Click);
            // 
            // btn_InitAll
            // 
            this.btn_InitAll.AccessibleDescription = "Init All";
            this.btn_InitAll.Location = new System.Drawing.Point(3, 3);
            this.btn_InitAll.Name = "btn_InitAll";
            this.btn_InitAll.Size = new System.Drawing.Size(168, 30);
            this.btn_InitAll.TabIndex = 2;
            this.btn_InitAll.Text = "Init All";
            this.btn_InitAll.UseVisualStyleBackColor = true;
            this.btn_InitAll.Click += new System.EventHandler(this.btn_InitAll_Click);
            // 
            // btn_Load
            // 
            this.btn_Load.AccessibleDescription = "Load";
            this.btn_Load.Location = new System.Drawing.Point(670, 8);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(75, 30);
            this.btn_Load.TabIndex = 10;
            this.btn_Load.Text = "Load";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.AccessibleDescription = "Save";
            this.btn_Save.Location = new System.Drawing.Point(751, 8);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 30);
            this.btn_Save.TabIndex = 11;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // tmr_Run
            // 
            this.tmr_Run.Enabled = true;
            this.tmr_Run.Tick += new System.EventHandler(this.tmr_Run_Tick);
            // 
            // lblRightSmema_McReady
            // 
            this.lblRightSmema_McReady.AccessibleDescription = "";
            this.lblRightSmema_McReady.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRightSmema_McReady.Location = new System.Drawing.Point(66, 0);
            this.lblRightSmema_McReady.Name = "lblRightSmema_McReady";
            this.lblRightSmema_McReady.Size = new System.Drawing.Size(60, 23);
            this.lblRightSmema_McReady.TabIndex = 129;
            this.lblRightSmema_McReady.Text = "Mc Rdy";
            this.lblRightSmema_McReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightSmema_McReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_DL_McReady_MouseDown);
            this.lblRightSmema_McReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_DL_McReady_MouseUp);
            // 
            // lblLeftSmema_BdReady
            // 
            this.lblLeftSmema_BdReady.AccessibleDescription = "";
            this.lblLeftSmema_BdReady.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLeftSmema_BdReady.Location = new System.Drawing.Point(0, 0);
            this.lblLeftSmema_BdReady.Name = "lblLeftSmema_BdReady";
            this.lblLeftSmema_BdReady.Size = new System.Drawing.Size(60, 23);
            this.lblLeftSmema_BdReady.TabIndex = 128;
            this.lblLeftSmema_BdReady.Text = "Bd Rdy";
            this.lblLeftSmema_BdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftSmema_BdReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_UL_DI_BdReady_MouseDown);
            this.lblLeftSmema_BdReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_UL_DI_BdReady_MouseUp);
            // 
            // btnRightSmema_BdReady
            // 
            this.btnRightSmema_BdReady.AccessibleDescription = "";
            this.btnRightSmema_BdReady.Location = new System.Drawing.Point(0, 0);
            this.btnRightSmema_BdReady.Name = "btnRightSmema_BdReady";
            this.btnRightSmema_BdReady.Size = new System.Drawing.Size(60, 23);
            this.btnRightSmema_BdReady.TabIndex = 127;
            this.btnRightSmema_BdReady.Text = "Bd Rdy";
            this.btnRightSmema_BdReady.UseVisualStyleBackColor = true;
            // 
            // btnLeftSmema_McReady
            // 
            this.btnLeftSmema_McReady.AccessibleDescription = "";
            this.btnLeftSmema_McReady.Location = new System.Drawing.Point(66, 0);
            this.btnLeftSmema_McReady.Name = "btnLeftSmema_McReady";
            this.btnLeftSmema_McReady.Size = new System.Drawing.Size(60, 23);
            this.btnLeftSmema_McReady.TabIndex = 126;
            this.btnLeftSmema_McReady.Text = "Mc Rdy";
            this.btnLeftSmema_McReady.UseVisualStyleBackColor = true;
            // 
            // btn_Control
            // 
            this.btn_Control.AccessibleDescription = "Control";
            this.btn_Control.Location = new System.Drawing.Point(8, 8);
            this.btn_Control.Name = "btn_Control";
            this.btn_Control.Size = new System.Drawing.Size(80, 30);
            this.btn_Control.TabIndex = 130;
            this.btn_Control.Text = "Control";
            this.btn_Control.UseVisualStyleBackColor = true;
            this.btn_Control.Click += new System.EventHandler(this.btn_Control_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.AccessibleDescription = "Close";
            this.btn_Close.Location = new System.Drawing.Point(913, 8);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 30);
            this.btn_Close.TabIndex = 131;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Config
            // 
            this.btn_Config.AccessibleDescription = "Config";
            this.btn_Config.Location = new System.Drawing.Point(494, 8);
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(80, 30);
            this.btn_Config.TabIndex = 132;
            this.btn_Config.Text = "Config";
            this.btn_Config.UseVisualStyleBackColor = true;
            this.btn_Config.Click += new System.EventHandler(this.btn_Config_Click);
            // 
            // btn_LULBypass
            // 
            this.btn_LULBypass.AccessibleDescription = "LUL Bypass";
            this.btn_LULBypass.Location = new System.Drawing.Point(275, 647);
            this.btn_LULBypass.Name = "btn_LULBypass";
            this.btn_LULBypass.Size = new System.Drawing.Size(164, 23);
            this.btn_LULBypass.TabIndex = 133;
            this.btn_LULBypass.Text = "LUL Bypass";
            this.btn_LULBypass.UseVisualStyleBackColor = true;
            this.btn_LULBypass.Click += new System.EventHandler(this.btn_LULBypass_Click);
            // 
            // tmr_05s
            // 
            this.tmr_05s.Enabled = true;
            this.tmr_05s.Interval = 500;
            this.tmr_05s.Tick += new System.EventHandler(this.tmr_1s_Tick);
            // 
            // btn_SaveAs
            // 
            this.btn_SaveAs.AccessibleDescription = "SaveAs";
            this.btn_SaveAs.Location = new System.Drawing.Point(832, 8);
            this.btn_SaveAs.Name = "btn_SaveAs";
            this.btn_SaveAs.Size = new System.Drawing.Size(75, 30);
            this.btn_SaveAs.TabIndex = 134;
            this.btn_SaveAs.Text = "SaveAs";
            this.btn_SaveAs.UseVisualStyleBackColor = true;
            this.btn_SaveAs.Click += new System.EventHandler(this.btn_SaveAs_Click);
            // 
            // pnlLeftSmema
            // 
            this.pnlLeftSmema.AutoSize = true;
            this.pnlLeftSmema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLeftSmema.Controls.Add(this.lblLeftSmema_BdReady);
            this.pnlLeftSmema.Controls.Add(this.btnLeftSmema_McReady);
            this.pnlLeftSmema.Location = new System.Drawing.Point(8, 647);
            this.pnlLeftSmema.Name = "pnlLeftSmema";
            this.pnlLeftSmema.Size = new System.Drawing.Size(129, 26);
            this.pnlLeftSmema.TabIndex = 0;
            // 
            // pnlRightSmema
            // 
            this.pnlRightSmema.AutoSize = true;
            this.pnlRightSmema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlRightSmema.Controls.Add(this.btnRightSmema_BdReady);
            this.pnlRightSmema.Controls.Add(this.lblRightSmema_McReady);
            this.pnlRightSmema.Location = new System.Drawing.Point(679, 647);
            this.pnlRightSmema.Name = "pnlRightSmema";
            this.pnlRightSmema.Size = new System.Drawing.Size(129, 26);
            this.pnlRightSmema.TabIndex = 0;
            // 
            // cbNewSeq
            // 
            this.cbNewSeq.AccessibleDescription = "";
            this.cbNewSeq.AutoSize = true;
            this.cbNewSeq.Location = new System.Drawing.Point(9, 124);
            this.cbNewSeq.Name = "cbNewSeq";
            this.cbNewSeq.Size = new System.Drawing.Size(126, 22);
            this.cbNewSeq.TabIndex = 359;
            this.cbNewSeq.Text = "New Sequence";
            this.cbNewSeq.UseVisualStyleBackColor = true;
            this.cbNewSeq.Click += new System.EventHandler(this.cbNewSeq_Click);
            // 
            // frmMHS2Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(999, 678);
            this.Controls.Add(this.pnlRightSmema);
            this.Controls.Add(this.pnlLeftSmema);
            this.Controls.Add(this.btn_SaveAs);
            this.Controls.Add(this.btn_LULBypass);
            this.Controls.Add(this.btn_Config);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Control);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Load);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_Control);
            this.Controls.Add(this.btn_ConvParam);
            this.Controls.Add(this.btn_ElevMotorParam);
            this.Controls.Add(this.btn_ElevSetup);
            this.Controls.Add(this.btn_ElevIO);
            this.Controls.Add(this.btn_ConvIO);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.Name = "frmMHS2Main";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frm_MHS2Main";
            this.Load += new System.EventHandler(this.frm_Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlLeftSmema.ResumeLayout(false);
            this.pnlRightSmema.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ConvIO;
        private System.Windows.Forms.Button btn_ElevIO;
        private System.Windows.Forms.Button btn_ElevSetup;
        private System.Windows.Forms.Button btn_ElevMotorParam;
        private System.Windows.Forms.Button btn_ConvParam;
        private System.Windows.Forms.Panel pnl_Control;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_InitElev;
        private System.Windows.Forms.Button btn_InitConv;
        private System.Windows.Forms.Button btn_InitAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_ProcessTime;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Load;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Timer tmr_Run;
        private System.Windows.Forms.Label lblRightSmema_McReady;
        private System.Windows.Forms.Label lblLeftSmema_BdReady;
        private System.Windows.Forms.Button btnRightSmema_BdReady;
        private System.Windows.Forms.Button btnLeftSmema_McReady;
        private System.Windows.Forms.Button btn_Control;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Config;
        private System.Windows.Forms.CheckBox cbox_ProcessTime;
        private System.Windows.Forms.Button btn_ProSt;
        private System.Windows.Forms.Button btn_PreSt;
        private System.Windows.Forms.Button btn_LULBypass;
        private System.Windows.Forms.Timer tmr_05s;
        private System.Windows.Forms.Button btn_SaveAs;
        private System.Windows.Forms.Panel pnlLeftSmema;
        private System.Windows.Forms.Panel pnlRightSmema;
        private System.Windows.Forms.CheckBox cbNewSeq;
    }
}