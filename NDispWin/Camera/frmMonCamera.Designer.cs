
namespace NDispWin
{
    partial class frmMonCamera
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
            this.pbox2 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.liveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCam1Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCam2Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmr1s = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbox2
            // 
            this.pbox2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pbox2.Location = new System.Drawing.Point(3, 3);
            this.pbox2.Name = "pbox2";
            this.pbox2.Size = new System.Drawing.Size(100, 50);
            this.pbox2.TabIndex = 0;
            this.pbox2.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liveToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem3,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveImageToolStripMenuItem,
            this.toolStripMenuItem1,
            this.startRecordToolStripMenuItem,
            this.stopRecordToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(538, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // liveToolStripMenuItem
            // 
            this.liveToolStripMenuItem.Name = "liveToolStripMenuItem";
            this.liveToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.liveToolStripMenuItem.Text = "Live";
            this.liveToolStripMenuItem.Click += new System.EventHandler(this.liveToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem3.Size = new System.Drawing.Size(14, 20);
            this.toolStripMenuItem3.Text = "|";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem2.Size = new System.Drawing.Size(14, 20);
            this.toolStripMenuItem2.Text = "|";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem1.Size = new System.Drawing.Size(14, 20);
            this.toolStripMenuItem1.Text = "|";
            // 
            // startRecordToolStripMenuItem
            // 
            this.startRecordToolStripMenuItem.Name = "startRecordToolStripMenuItem";
            this.startRecordToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.startRecordToolStripMenuItem.Text = "Start Rec";
            this.startRecordToolStripMenuItem.Click += new System.EventHandler(this.startRecordToolStripMenuItem_Click);
            // 
            // stopRecordToolStripMenuItem
            // 
            this.stopRecordToolStripMenuItem.Name = "stopRecordToolStripMenuItem";
            this.stopRecordToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.stopRecordToolStripMenuItem.Text = "Stop Rec";
            this.stopRecordToolStripMenuItem.Click += new System.EventHandler(this.stopRecordToolStripMenuItem_Click);
            // 
            // pbox1
            // 
            this.pbox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbox1.Location = new System.Drawing.Point(3, 3);
            this.pbox1.Name = "pbox1";
            this.pbox1.Size = new System.Drawing.Size(100, 50);
            this.pbox1.TabIndex = 2;
            this.pbox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCam1Status,
            this.tsslCam2Status,
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 234);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(538, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCam1Status
            // 
            this.tsslCam1Status.Name = "tsslCam1Status";
            this.tsslCam1Status.Size = new System.Drawing.Size(38, 17);
            this.tsslCam1Status.Text = "Cam1";
            // 
            // tsslCam2Status
            // 
            this.tsslCam2Status.Name = "tsslCam2Status";
            this.tsslCam2Status.Size = new System.Drawing.Size(38, 17);
            this.tsslCam2Status.Text = "Cam2";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(12, 17);
            this.tsslStatus.Text = "-";
            // 
            // tmr1s
            // 
            this.tmr1s.Enabled = true;
            this.tmr1s.Interval = 1000;
            this.tmr1s.Tick += new System.EventHandler(this.tmr1s_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbox1);
            this.panel1.Location = new System.Drawing.Point(12, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pbox2);
            this.panel2.Location = new System.Drawing.Point(218, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 5;
            // 
            // frmMonCamera
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(538, 256);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMonCamera";
            this.Text = "frmMonitoring";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMonitoring_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMonCamera_FormClosed);
            this.Load += new System.EventHandler(this.frmMonitoring_Load);
            this.Resize += new System.EventHandler(this.frmMonitoring_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PictureBox pbox1;
        private System.Windows.Forms.ToolStripMenuItem liveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tsslCam1Status;
        private System.Windows.Forms.ToolStripStatusLabel tsslCam2Status;
        private System.Windows.Forms.Timer tmr1s;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}