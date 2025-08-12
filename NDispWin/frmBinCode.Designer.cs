namespace NDispWin
{
    partial class frmBinCode
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
            this.dgvBinCode = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBinCode)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBinCode
            // 
            this.dgvBinCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBinCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBinCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBinCode.Location = new System.Drawing.Point(0, 0);
            this.dgvBinCode.Name = "dgvBinCode";
            this.dgvBinCode.RowHeadersWidth = 51;
            this.dgvBinCode.RowTemplate.Height = 24;
            this.dgvBinCode.Size = new System.Drawing.Size(399, 450);
            this.dgvBinCode.TabIndex = 0;
            // 
            // frmBinCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 450);
            this.Controls.Add(this.dgvBinCode);
            this.Name = "frmBinCode";
            this.Text = "frmBinCode";
            this.Load += new System.EventHandler(this.frmBinCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBinCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBinCode;
    }
}