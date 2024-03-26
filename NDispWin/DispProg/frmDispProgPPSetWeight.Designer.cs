
namespace NDispWin
{
    partial class frmDispProgPPSetWeight
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
            this.btn_CopyW = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblWeight2 = new System.Windows.Forms.Label();
            this.lblWeight1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDensity1 = new System.Windows.Forms.Label();
            this.lblDensity2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblVolume1 = new System.Windows.Forms.Label();
            this.lblVolume2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_CopyW
            // 
            this.btn_CopyW.AccessibleDescription = "Copy";
            this.btn_CopyW.Location = new System.Drawing.Point(298, 26);
            this.btn_CopyW.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CopyW.Name = "btn_CopyW";
            this.btn_CopyW.Size = new System.Drawing.Size(75, 36);
            this.btn_CopyW.TabIndex = 142;
            this.btn_CopyW.Text = "Copy";
            this.btn_CopyW.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "Head B";
            this.label7.Location = new System.Drawing.Point(219, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 23);
            this.label7.TabIndex = 138;
            this.label7.Text = "Head B";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "Weight";
            this.label9.Location = new System.Drawing.Point(7, 33);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 23);
            this.label9.TabIndex = 136;
            this.label9.Text = "Weight";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "";
            this.label6.Location = new System.Drawing.Point(86, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 23);
            this.label6.TabIndex = 140;
            this.label6.Text = "(mg)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWeight2
            // 
            this.lblWeight2.BackColor = System.Drawing.SystemColors.Window;
            this.lblWeight2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWeight2.Location = new System.Drawing.Point(219, 33);
            this.lblWeight2.Margin = new System.Windows.Forms.Padding(2);
            this.lblWeight2.Name = "lblWeight2";
            this.lblWeight2.Size = new System.Drawing.Size(75, 23);
            this.lblWeight2.TabIndex = 139;
            this.lblWeight2.Text = "label2";
            this.lblWeight2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWeight2.Click += new System.EventHandler(this.lbl_HeadBWeight_Click);
            // 
            // lblWeight1
            // 
            this.lblWeight1.BackColor = System.Drawing.SystemColors.Window;
            this.lblWeight1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWeight1.Location = new System.Drawing.Point(140, 33);
            this.lblWeight1.Margin = new System.Windows.Forms.Padding(2);
            this.lblWeight1.Name = "lblWeight1";
            this.lblWeight1.Size = new System.Drawing.Size(75, 23);
            this.lblWeight1.TabIndex = 137;
            this.lblWeight1.Text = "label11";
            this.lblWeight1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWeight1.Click += new System.EventHandler(this.lbl_HeadAWeight_Click);
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.btn_Execute);
            this.panel3.Controls.Add(this.btn_Cancel);
            this.panel3.Controls.Add(this.btn_OK);
            this.panel3.Location = new System.Drawing.Point(8, 151);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(437, 50);
            this.panel3.TabIndex = 138;
            // 
            // btn_Execute
            // 
            this.btn_Execute.AccessibleDescription = "Execute";
            this.btn_Execute.Location = new System.Drawing.Point(7, 7);
            this.btn_Execute.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(75, 36);
            this.btn_Execute.TabIndex = 117;
            this.btn_Execute.Text = "Execute";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.AccessibleDescription = "Cancel";
            this.btn_Cancel.Location = new System.Drawing.Point(355, 7);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 36);
            this.btn_Cancel.TabIndex = 101;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.AccessibleDescription = "OK";
            this.btn_OK.Location = new System.Drawing.Point(276, 7);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 36);
            this.btn_OK.TabIndex = 100;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Head A";
            this.label2.Location = new System.Drawing.Point(140, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 23);
            this.label2.TabIndex = 144;
            this.label2.Text = "Head A";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDensity1
            // 
            this.lblDensity1.BackColor = System.Drawing.SystemColors.Window;
            this.lblDensity1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDensity1.Enabled = false;
            this.lblDensity1.Location = new System.Drawing.Point(140, 78);
            this.lblDensity1.Margin = new System.Windows.Forms.Padding(2);
            this.lblDensity1.Name = "lblDensity1";
            this.lblDensity1.Size = new System.Drawing.Size(75, 23);
            this.lblDensity1.TabIndex = 145;
            this.lblDensity1.Text = "label11";
            this.lblDensity1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDensity2
            // 
            this.lblDensity2.BackColor = System.Drawing.SystemColors.Window;
            this.lblDensity2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDensity2.Enabled = false;
            this.lblDensity2.Location = new System.Drawing.Point(219, 78);
            this.lblDensity2.Margin = new System.Windows.Forms.Padding(2);
            this.lblDensity2.Name = "lblDensity2";
            this.lblDensity2.Size = new System.Drawing.Size(75, 23);
            this.lblDensity2.TabIndex = 147;
            this.lblDensity2.Text = "label2";
            this.lblDensity2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "";
            this.label10.Location = new System.Drawing.Point(86, 78);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 23);
            this.label10.TabIndex = 148;
            this.label10.Text = "(mg/ul)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Weight";
            this.label1.Location = new System.Drawing.Point(7, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 149;
            this.label1.Text = "Density";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "Volume";
            this.label3.Location = new System.Drawing.Point(7, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 23);
            this.label3.TabIndex = 153;
            this.label3.Text = "Volume";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVolume1
            // 
            this.lblVolume1.BackColor = System.Drawing.SystemColors.Window;
            this.lblVolume1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVolume1.Enabled = false;
            this.lblVolume1.Location = new System.Drawing.Point(140, 105);
            this.lblVolume1.Margin = new System.Windows.Forms.Padding(2);
            this.lblVolume1.Name = "lblVolume1";
            this.lblVolume1.Size = new System.Drawing.Size(75, 23);
            this.lblVolume1.TabIndex = 150;
            this.lblVolume1.Text = "label11";
            this.lblVolume1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVolume2
            // 
            this.lblVolume2.BackColor = System.Drawing.SystemColors.Window;
            this.lblVolume2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVolume2.Enabled = false;
            this.lblVolume2.Location = new System.Drawing.Point(219, 105);
            this.lblVolume2.Margin = new System.Windows.Forms.Padding(2);
            this.lblVolume2.Name = "lblVolume2";
            this.lblVolume2.Size = new System.Drawing.Size(75, 23);
            this.lblVolume2.TabIndex = 151;
            this.lblVolume2.Text = "label2";
            this.lblVolume2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "";
            this.label8.Location = new System.Drawing.Point(86, 105);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 23);
            this.label8.TabIndex = 152;
            this.label8.Text = "(ul)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmDispProgPPSetWeight
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(458, 216);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVolume1);
            this.Controls.Add(this.lblVolume2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDensity1);
            this.Controls.Add(this.lblDensity2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btn_CopyW);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblWeight1);
            this.Controls.Add(this.lblWeight2);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDispProgPPSetWeight";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmDispProgPPSetWeight";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDispProgPPSetWeight_FormClosed);
            this.Load += new System.EventHandler(this.frmDispProgPPSetWeight_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_CopyW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblWeight2;
        private System.Windows.Forms.Label lblWeight1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDensity1;
        private System.Windows.Forms.Label lblDensity2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblVolume1;
        private System.Windows.Forms.Label lblVolume2;
        private System.Windows.Forms.Label label8;
    }
}