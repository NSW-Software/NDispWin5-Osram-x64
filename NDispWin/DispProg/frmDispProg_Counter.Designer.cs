namespace NDispWin
{
    partial class frm_DispCore_DispProg_Counter
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
            this.lbl_Event = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lbl_CounterID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._lbl_Value = new System.Windows.Forms.Label();
            this.lbl_Value = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Event
            // 
            this.lbl_Event.BackColor = System.Drawing.Color.White;
            this.lbl_Event.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Event.Location = new System.Drawing.Point(137, 38);
            this.lbl_Event.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Event.Name = "lbl_Event";
            this.lbl_Event.Size = new System.Drawing.Size(75, 23);
            this.lbl_Event.TabIndex = 49;
            this.lbl_Event.Text = "1000";
            this.lbl_Event.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Event.Click += new System.EventHandler(this.lbl_Event_Click);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Action";
            this.label2.Location = new System.Drawing.Point(11, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 48;
            this.label2.Text = "Action";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.AccessibleDescription = "Cancel";
            this.btn_Cancel.Location = new System.Drawing.Point(138, 104);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 36);
            this.btn_Cancel.TabIndex = 47;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.AccessibleDescription = "OK";
            this.btn_OK.Location = new System.Drawing.Point(57, 104);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 36);
            this.btn_OK.TabIndex = 46;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lbl_CounterID
            // 
            this.lbl_CounterID.BackColor = System.Drawing.Color.White;
            this.lbl_CounterID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_CounterID.Location = new System.Drawing.Point(137, 11);
            this.lbl_CounterID.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_CounterID.Name = "lbl_CounterID";
            this.lbl_CounterID.Size = new System.Drawing.Size(75, 23);
            this.lbl_CounterID.TabIndex = 52;
            this.lbl_CounterID.Text = "0";
            this.lbl_CounterID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_CounterID.Click += new System.EventHandler(this.lbl_CounterID_Click);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "Counter ID";
            this.label3.Location = new System.Drawing.Point(11, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 23);
            this.label3.TabIndex = 51;
            this.label3.Text = "Counter ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // _lbl_Value
            // 
            this._lbl_Value.AccessibleDescription = "Value";
            this._lbl_Value.Location = new System.Drawing.Point(11, 65);
            this._lbl_Value.Margin = new System.Windows.Forms.Padding(2);
            this._lbl_Value.Name = "_lbl_Value";
            this._lbl_Value.Size = new System.Drawing.Size(120, 23);
            this._lbl_Value.TabIndex = 53;
            this._lbl_Value.Text = "Value";
            this._lbl_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Value
            // 
            this.lbl_Value.Location = new System.Drawing.Point(135, 65);
            this.lbl_Value.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Value.Name = "lbl_Value";
            this.lbl_Value.Size = new System.Drawing.Size(77, 23);
            this.lbl_Value.TabIndex = 54;
            this.lbl_Value.Text = "0";
            this.lbl_Value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_DispCore_DispProg_Counter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(247, 172);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Value);
            this.Controls.Add(this._lbl_Value);
            this.Controls.Add(this.lbl_CounterID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_Event);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_DispCore_DispProg_Counter";
            this.Text = "frmDispProg_Counter";
            this.Load += new System.EventHandler(this.frmDispProg_Counter_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_DispCore_DispProg_Counter_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Event;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label lbl_CounterID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label _lbl_Value;
        private System.Windows.Forms.Label lbl_Value;
    }
}