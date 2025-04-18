﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NDispWin
{
    public partial class frm_DispCore_EditXY : Form
    {
        public string ParamName = "";
        public double ValueX = 0;
        public double ValueY = 0;
        public double OfstX = 0;
        public double OfstY = 0;
        public double AdjustRate = 0.005;

        public frm_DispCore_EditXY()
        {
            InitializeComponent();
            GControl.LogForm(this);

            UpdateDisplay();
        }

        private void frm_DispCore_EditXY_Load(object sender, EventArgs e)
        {
            this.Text = ParamName;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lblValueX.Text = $"{ValueX + OfstX:f3}";
            lblValueY.Text = $"{ValueY + OfstY:f3}";
            lbl_OfstX.Text = OfstX.ToString("f3");
            lbl_OfstY.Text = OfstY.ToString("f3");
            lbl_AdjustRate.Text = AdjustRate.ToString("f3");
        }

        private void lbl_OfstX_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(ParamName + ", Offset X", ref OfstX, -1, 1);
            UpdateDisplay();
        }

        private void lbl_OfstY_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(ParamName + ", Offset Y", ref OfstY, -1, 1);
            UpdateDisplay();
        }

        private void btn_XP_Click(object sender, EventArgs e)
        {
            OfstX = OfstX + AdjustRate;
            UpdateDisplay();
        }

        private void btn_XM_Click(object sender, EventArgs e)
        {
            OfstX = OfstX - AdjustRate;
            UpdateDisplay();
        }

        private void btn_YP_Click(object sender, EventArgs e)
        {
            OfstY = OfstY + AdjustRate;
            UpdateDisplay();
        }

        private void btn_YM_Click(object sender, EventArgs e)
        {
            OfstY = OfstY - AdjustRate;
            UpdateDisplay();
        }

        private void lbl_AdjustRate_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(ParamName + ", Adjust Rate", ref AdjustRate, -0.1, 0.1);
            UpdateDisplay();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            ValueX = ValueX + OfstX;
            ValueY = ValueY + OfstY;

            DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lblValueX_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(ParamName + ", ValueX", ref ValueX, -999, 999);
            UpdateDisplay();
        }

        private void lblValueY_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(ParamName + ", ValueY", ref ValueY, -999, 999);
            UpdateDisplay();
        }
    }
}
