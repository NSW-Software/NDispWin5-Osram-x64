using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDispWin
{
    internal partial class frmDispProgPPSetWeight : Form
    {
        public DispProg.TLine CmdLine = new DispProg.TLine();
        public int ProgNo = 0;
        public int LineNo = 0;
        public TPos2 SubOrigin = new TPos2(0, 0);

        public frmDispProgPPSetWeight()
        {
            InitializeComponent();
            GControl.LogForm(this);

            TopLevel = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void frmDispProgPPSetWeight_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);

            this.Text = CmdName;
            CmdLine.Copy(DispProg.Script[ProgNo].CmdList.Line[LineNo]);
            UpdateDisplay();
        }
        private void frmDispProgPPSetWeight_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_DispProg2.Done = true;
        }

        private string CmdName
        {
            get
            {
                return LineNo.ToString("d3") + " " + CmdLine.Cmd.ToString();
            }
        }

        string dp4 = "f4";
        private void UpdateDisplay()
        {
            lblWeight1.Text = CmdLine.DPara[0].ToString("f3");
            lblWeight2.Text = CmdLine.DPara[1].ToString("f3");

            lblDensity1.Text = TaskWeight.CurrentCal[0].ToString(dp4);
            lblDensity2.Text = TaskWeight.CurrentCal[1].ToString(dp4);

            lblVolume1.Text = $"{DispProg.PP_HeadA_DispBaseVol - DispProg.PP_HeadA_BackSuckVol:f4}";
            lblVolume2.Text = $"{DispProg.PP_HeadB_DispBaseVol - DispProg.PP_HeadB_BackSuckVol:f4}";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DispProg.Script[ProgNo].CmdList.Line[LineNo].Copy(CmdLine);

            TaskDisp.TaskMoveGZZ2Up();

            Log.OnAction("OK", CmdName);
            Close();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            TaskDisp.TaskMoveGZZ2Up();

            Log.OnAction("Cancel", CmdName);
            Close();
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            TaskDisp.PP_SetWeight(new double[] { CmdLine.DPara[0], CmdLine.DPara[1] }, true);
            UpdateDisplay();
        }

        private void lbl_HeadAWeight_Click(object sender, EventArgs e)
        {
            double w = Math.Round(CmdLine.DPara[0], 3);
            UC.AdjustExec(CmdName + ", Head1 Volume_mg", ref w, -20, 20);
            CmdLine.DPara[0] = w;
            UpdateDisplay();
        }

        private void lbl_HeadBWeight_Click(object sender, EventArgs e)
        {
            double w = Math.Round(CmdLine.DPara[1], 3);
            UC.AdjustExec(CmdName + ", Head1 Volume_mg", ref w, -20, 20);
            CmdLine.DPara[1] = w;
            UpdateDisplay();
        }
    }
}
