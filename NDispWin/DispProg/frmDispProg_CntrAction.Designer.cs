namespace NDispWin
{
    partial class frm_DispCore_DispProg_CntrAction
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Value = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Action = new System.Windows.Forms.Label();
            this.lbl_Type = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.AccessibleDescription = "Cancel";
            this.btn_Cancel.ForeColor = System.Drawing.Color.Navy;
            this.btn_Cancel.Location = new System.Drawing.Point(119, 100);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(87, 39);
            this.btn_Cancel.TabIndex = 35;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.AccessibleDescription = "OK";
            this.btn_OK.ForeColor = System.Drawing.Color.Navy;
            this.btn_OK.Location = new System.Drawing.Point(27, 100);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(87, 39);
            this.btn_OK.TabIndex = 34;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Value";
            this.label1.Location = new System.Drawing.Point(7, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 23);
            this.label1.TabIndex = 116;
            this.label1.Text = "Value";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Value
            // 
            this.lbl_Value.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_Value.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Value.Location = new System.Drawing.Point(131, 34);
            this.lbl_Value.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Value.Name = "lbl_Value";
            this.lbl_Value.Size = new System.Drawing.Size(75, 23);
            this.lbl_Value.TabIndex = 117;
            this.lbl_Value.Text = "lbl_Value";
            this.lbl_Value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Value.Click += new System.EventHandler(this.lbl_Value_Click);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Action";
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 118;
            this.label2.Text = "Action";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Action
            // 
            this.lbl_Action.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_Action.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Action.Location = new System.Drawing.Point(131, 61);
            this.lbl_Action.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Action.Name = "lbl_Action";
            this.lbl_Action.Size = new System.Drawing.Size(75, 23);
            this.lbl_Action.TabIndex = 119;
            this.lbl_Action.Text = "lbl_Action";
            this.lbl_Action.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Action.Click += new System.EventHandler(this.lbl_Action_Click);
            // 
            // lbl_Type
            // 
            this.lbl_Type.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_Type.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Type.Location = new System.Drawing.Point(131, 7);
            this.lbl_Type.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Type.Name = "lbl_Type";
            this.lbl_Type.Size = new System.Drawing.Size(75, 23);
            this.lbl_Type.TabIndex = 121;
            this.lbl_Type.Text = "lbl_Type";
            this.lbl_Type.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Type.Click += new System.EventHandler(this.lbl_Type_Click);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "Type";
            this.label4.Location = new System.Drawing.Point(7, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 23);
            this.label4.TabIndex = 120;
            this.label4.Text = "Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frm_DispCore_DispProg_CntrAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 241);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Type);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_Action);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_Value);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_DispCore_DispProg_CntrAction";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmDispProg_CntrAct";
            this.Load += new System.EventHandler(this.frmDispProg_CntrAct_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_DispCore_DispProg_CntrAction_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Action;
        private System.Windows.Forms.Label lbl_Type;
        private System.Windows.Forms.Label label4;
    }
}