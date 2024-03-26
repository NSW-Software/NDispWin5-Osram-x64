namespace NDispWin
{
    partial class frm_InfoPanel_VolAdjust
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
            this.lbl_LotID = new System.Windows.Forms.Label();
            this.lbl_Program = new System.Windows.Forms.Label();
            this.btn_OsramSCC = new System.Windows.Forms.Button();
            this.lbl_OsramSCC = new System.Windows.Forms.Label();
            this.tmr_Display = new System.Windows.Forms.Timer(this.components);
            this.pnl_OsramSCC = new System.Windows.Forms.Panel();
            this.pnl_OsramSCC.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_LotID
            // 
            this.lbl_LotID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_LotID.Location = new System.Drawing.Point(4, 45);
            this.lbl_LotID.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_LotID.Name = "lbl_LotID";
            this.lbl_LotID.Size = new System.Drawing.Size(120, 35);
            this.lbl_LotID.TabIndex = 75;
            this.lbl_LotID.Text = "lbl_LotID";
            this.lbl_LotID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Program
            // 
            this.lbl_Program.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Program.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Program.Location = new System.Drawing.Point(4, 86);
            this.lbl_Program.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Program.Name = "lbl_Program";
            this.lbl_Program.Size = new System.Drawing.Size(120, 50);
            this.lbl_Program.TabIndex = 74;
            this.lbl_Program.Text = "lbl_Recipe";
            this.lbl_Program.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_OsramSCC
            // 
            this.btn_OsramSCC.Location = new System.Drawing.Point(4, 142);
            this.btn_OsramSCC.Name = "btn_OsramSCC";
            this.btn_OsramSCC.Size = new System.Drawing.Size(120, 40);
            this.btn_OsramSCC.TabIndex = 73;
            this.btn_OsramSCC.Text = "Osram SCC";
            this.btn_OsramSCC.UseVisualStyleBackColor = true;
            this.btn_OsramSCC.Click += new System.EventHandler(this.btn_OsramSCC_Click);
            // 
            // lbl_OsramSCC
            // 
            this.lbl_OsramSCC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_OsramSCC.Location = new System.Drawing.Point(4, 4);
            this.lbl_OsramSCC.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_OsramSCC.Name = "lbl_OsramSCC";
            this.lbl_OsramSCC.Size = new System.Drawing.Size(120, 35);
            this.lbl_OsramSCC.TabIndex = 72;
            this.lbl_OsramSCC.Text = "OsramSCC";
            this.lbl_OsramSCC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_OsramSCC.Click += new System.EventHandler(this.lbl_OsramSCC_Click);
            // 
            // tmr_Display
            // 
            this.tmr_Display.Interval = 500;
            this.tmr_Display.Tick += new System.EventHandler(this.tmr_Display_Tick);
            // 
            // pnl_OsramSCC
            // 
            this.pnl_OsramSCC.AutoSize = true;
            this.pnl_OsramSCC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_OsramSCC.Controls.Add(this.lbl_OsramSCC);
            this.pnl_OsramSCC.Controls.Add(this.lbl_LotID);
            this.pnl_OsramSCC.Controls.Add(this.btn_OsramSCC);
            this.pnl_OsramSCC.Controls.Add(this.lbl_Program);
            this.pnl_OsramSCC.Location = new System.Drawing.Point(55, 27);
            this.pnl_OsramSCC.Name = "pnl_OsramSCC";
            this.pnl_OsramSCC.Size = new System.Drawing.Size(127, 185);
            this.pnl_OsramSCC.TabIndex = 76;
            // 
            // frm_InfoPanel_VolAdjust
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(240, 240);
            this.Controls.Add(this.pnl_OsramSCC);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_InfoPanel_VolAdjust";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "frm_InfoPanel_VolAdjust";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_InfoPanel_VolAdjust_FormClosing);
            this.Load += new System.EventHandler(this.frm_InfoPanel_VolAdjust_Load);
            this.pnl_OsramSCC.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_LotID;
        private System.Windows.Forms.Label lbl_Program;
        private System.Windows.Forms.Button btn_OsramSCC;
        private System.Windows.Forms.Label lbl_OsramSCC;
        private System.Windows.Forms.Timer tmr_Display;
        private System.Windows.Forms.Panel pnl_OsramSCC;
    }
}