namespace NDispWin
{
    partial class frm_LotEntryOsramICC
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
            this.tbDAStartNumber = new System.Windows.Forms.TextBox();
            this.lbl_MCNoCaption = new System.Windows.Forms.Label();
            this.tbEmployeeID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb11Series = new System.Windows.Forms.TextBox();
            this.tbLotNumber = new System.Windows.Forms.TextBox();
            this.lbl_LotNumber = new System.Windows.Forms.Label();
            this.lbl_MaterialNr = new System.Windows.Forms.Label();
            this.btn_StartLot = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_EndLot = new System.Windows.Forms.Button();
            this.tbFieldValue5 = new System.Windows.Forms.TextBox();
            this.tbFieldValue6 = new System.Windows.Forms.TextBox();
            this.tbFieldValue7 = new System.Windows.Forms.TextBox();
            this.tbFieldValue8 = new System.Windows.Forms.TextBox();
            this.tbField5Name = new System.Windows.Forms.TextBox();
            this.tbFieldName6 = new System.Windows.Forms.TextBox();
            this.tbFieldName7 = new System.Windows.Forms.TextBox();
            this.tbFieldName8 = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbDAStartNumber
            // 
            this.tbDAStartNumber.ForeColor = System.Drawing.Color.Navy;
            this.tbDAStartNumber.Location = new System.Drawing.Point(161, 106);
            this.tbDAStartNumber.Name = "tbDAStartNumber";
            this.tbDAStartNumber.Size = new System.Drawing.Size(312, 22);
            this.tbDAStartNumber.TabIndex = 3;
            this.tbDAStartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbox_DAStartNumber_KeyDown);
            // 
            // lbl_MCNoCaption
            // 
            this.lbl_MCNoCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MCNoCaption.ForeColor = System.Drawing.Color.Navy;
            this.lbl_MCNoCaption.Location = new System.Drawing.Point(23, 107);
            this.lbl_MCNoCaption.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_MCNoCaption.Name = "lbl_MCNoCaption";
            this.lbl_MCNoCaption.Size = new System.Drawing.Size(132, 22);
            this.lbl_MCNoCaption.TabIndex = 70;
            this.lbl_MCNoCaption.Text = "DA START NUMBER";
            this.lbl_MCNoCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbEmployeeID
            // 
            this.tbEmployeeID.ForeColor = System.Drawing.Color.Navy;
            this.tbEmployeeID.Location = new System.Drawing.Point(161, 22);
            this.tbEmployeeID.Name = "tbEmployeeID";
            this.tbEmployeeID.Size = new System.Drawing.Size(312, 22);
            this.tbEmployeeID.TabIndex = 0;
            this.tbEmployeeID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbox_EmployeeID_KeyDown);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(23, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 22);
            this.label6.TabIndex = 69;
            this.label6.Text = "EMPLOYEE ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb11Series
            // 
            this.tb11Series.ForeColor = System.Drawing.Color.Navy;
            this.tb11Series.Location = new System.Drawing.Point(161, 78);
            this.tb11Series.Name = "tb11Series";
            this.tb11Series.Size = new System.Drawing.Size(312, 22);
            this.tb11Series.TabIndex = 2;
            this.tb11Series.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbox_11Series_KeyDown);
            // 
            // tbLotNumber
            // 
            this.tbLotNumber.ForeColor = System.Drawing.Color.Navy;
            this.tbLotNumber.Location = new System.Drawing.Point(161, 50);
            this.tbLotNumber.Name = "tbLotNumber";
            this.tbLotNumber.Size = new System.Drawing.Size(312, 22);
            this.tbLotNumber.TabIndex = 1;
            this.tbLotNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbox_LotNumber_KeyDown);
            // 
            // lbl_LotNumber
            // 
            this.lbl_LotNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_LotNumber.ForeColor = System.Drawing.Color.Navy;
            this.lbl_LotNumber.Location = new System.Drawing.Point(23, 51);
            this.lbl_LotNumber.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_LotNumber.Name = "lbl_LotNumber";
            this.lbl_LotNumber.Size = new System.Drawing.Size(132, 22);
            this.lbl_LotNumber.TabIndex = 66;
            this.lbl_LotNumber.Text = "LOT NUMBER";
            this.lbl_LotNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MaterialNr
            // 
            this.lbl_MaterialNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MaterialNr.ForeColor = System.Drawing.Color.Navy;
            this.lbl_MaterialNr.Location = new System.Drawing.Point(23, 79);
            this.lbl_MaterialNr.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_MaterialNr.Name = "lbl_MaterialNr";
            this.lbl_MaterialNr.Size = new System.Drawing.Size(132, 22);
            this.lbl_MaterialNr.TabIndex = 68;
            this.lbl_MaterialNr.Text = "11 SERIES - (Recipe)";
            this.lbl_MaterialNr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_StartLot
            // 
            this.btn_StartLot.Location = new System.Drawing.Point(161, 246);
            this.btn_StartLot.Name = "btn_StartLot";
            this.btn_StartLot.Size = new System.Drawing.Size(80, 40);
            this.btn_StartLot.TabIndex = 4;
            this.btn_StartLot.Text = "START LOT";
            this.btn_StartLot.UseVisualStyleBackColor = true;
            this.btn_StartLot.Click += new System.EventHandler(this.btn_StartLot_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(393, 246);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 40);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.Text = "CLOSE";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_EndLot
            // 
            this.btn_EndLot.Location = new System.Drawing.Point(247, 246);
            this.btn_EndLot.Name = "btn_EndLot";
            this.btn_EndLot.Size = new System.Drawing.Size(80, 40);
            this.btn_EndLot.TabIndex = 6;
            this.btn_EndLot.Text = "END LOT";
            this.btn_EndLot.UseVisualStyleBackColor = true;
            this.btn_EndLot.Click += new System.EventHandler(this.btn_EndLot_Click);
            // 
            // tbFieldValue5
            // 
            this.tbFieldValue5.ForeColor = System.Drawing.Color.Navy;
            this.tbFieldValue5.Location = new System.Drawing.Point(161, 134);
            this.tbFieldValue5.Name = "tbFieldValue5";
            this.tbFieldValue5.Size = new System.Drawing.Size(312, 22);
            this.tbFieldValue5.TabIndex = 71;
            this.tbFieldValue5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFieldValue5_KeyDown);
            // 
            // tbFieldValue6
            // 
            this.tbFieldValue6.ForeColor = System.Drawing.Color.Navy;
            this.tbFieldValue6.Location = new System.Drawing.Point(161, 162);
            this.tbFieldValue6.Name = "tbFieldValue6";
            this.tbFieldValue6.Size = new System.Drawing.Size(312, 22);
            this.tbFieldValue6.TabIndex = 73;
            this.tbFieldValue6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFieldValue6_KeyDown);
            // 
            // tbFieldValue7
            // 
            this.tbFieldValue7.ForeColor = System.Drawing.Color.Navy;
            this.tbFieldValue7.Location = new System.Drawing.Point(161, 190);
            this.tbFieldValue7.Name = "tbFieldValue7";
            this.tbFieldValue7.Size = new System.Drawing.Size(312, 22);
            this.tbFieldValue7.TabIndex = 75;
            this.tbFieldValue7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFieldValue7_KeyDown);
            // 
            // tbFieldValue8
            // 
            this.tbFieldValue8.ForeColor = System.Drawing.Color.Navy;
            this.tbFieldValue8.Location = new System.Drawing.Point(161, 218);
            this.tbFieldValue8.Name = "tbFieldValue8";
            this.tbFieldValue8.Size = new System.Drawing.Size(312, 22);
            this.tbFieldValue8.TabIndex = 77;
            this.tbFieldValue8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFieldValue8_KeyDown);
            // 
            // tbField5Name
            // 
            this.tbField5Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbField5Name.ForeColor = System.Drawing.Color.Navy;
            this.tbField5Name.Location = new System.Drawing.Point(23, 135);
            this.tbField5Name.Name = "tbField5Name";
            this.tbField5Name.Size = new System.Drawing.Size(132, 22);
            this.tbField5Name.TabIndex = 79;
            this.tbField5Name.Text = "FIELD NAME 5";
            // 
            // tbFieldName6
            // 
            this.tbFieldName6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFieldName6.ForeColor = System.Drawing.Color.Navy;
            this.tbFieldName6.Location = new System.Drawing.Point(23, 163);
            this.tbFieldName6.Name = "tbFieldName6";
            this.tbFieldName6.Size = new System.Drawing.Size(132, 22);
            this.tbFieldName6.TabIndex = 80;
            // 
            // tbFieldName7
            // 
            this.tbFieldName7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFieldName7.ForeColor = System.Drawing.Color.Navy;
            this.tbFieldName7.Location = new System.Drawing.Point(23, 191);
            this.tbFieldName7.Name = "tbFieldName7";
            this.tbFieldName7.Size = new System.Drawing.Size(132, 22);
            this.tbFieldName7.TabIndex = 81;
            // 
            // tbFieldName8
            // 
            this.tbFieldName8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFieldName8.ForeColor = System.Drawing.Color.Navy;
            this.tbFieldName8.Location = new System.Drawing.Point(23, 219);
            this.tbFieldName8.Name = "tbFieldName8";
            this.tbFieldName8.Size = new System.Drawing.Size(132, 22);
            this.tbFieldName8.TabIndex = 82;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(23, 247);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 40);
            this.btnEdit.TabIndex = 83;
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // frm_LotEntryOsramICC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(556, 317);
            this.ControlBox = false;
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.tbFieldName8);
            this.Controls.Add(this.tbFieldName7);
            this.Controls.Add(this.tbFieldName6);
            this.Controls.Add(this.tbField5Name);
            this.Controls.Add(this.tbFieldValue8);
            this.Controls.Add(this.tbFieldValue7);
            this.Controls.Add(this.tbFieldValue6);
            this.Controls.Add(this.tbFieldValue5);
            this.Controls.Add(this.btn_EndLot);
            this.Controls.Add(this.btn_StartLot);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.tbDAStartNumber);
            this.Controls.Add(this.lbl_MCNoCaption);
            this.Controls.Add(this.tbEmployeeID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb11Series);
            this.Controls.Add(this.tbLotNumber);
            this.Controls.Add(this.lbl_LotNumber);
            this.Controls.Add(this.lbl_MaterialNr);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_LotEntryOsramICC";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_LotEntryOsramEMos";
            this.Load += new System.EventHandler(this.frm_LotEntryOsramType2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDAStartNumber;
        private System.Windows.Forms.Label lbl_MCNoCaption;
        private System.Windows.Forms.TextBox tbEmployeeID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb11Series;
        private System.Windows.Forms.TextBox tbLotNumber;
        private System.Windows.Forms.Label lbl_LotNumber;
        private System.Windows.Forms.Label lbl_MaterialNr;
        private System.Windows.Forms.Button btn_StartLot;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_EndLot;
        private System.Windows.Forms.TextBox tbFieldValue5;
        private System.Windows.Forms.TextBox tbFieldValue6;
        private System.Windows.Forms.TextBox tbFieldValue7;
        private System.Windows.Forms.TextBox tbFieldValue8;
        private System.Windows.Forms.TextBox tbField5Name;
        private System.Windows.Forms.TextBox tbFieldName6;
        private System.Windows.Forms.TextBox tbFieldName7;
        private System.Windows.Forms.TextBox tbFieldName8;
        private System.Windows.Forms.Button btnEdit;
    }
}