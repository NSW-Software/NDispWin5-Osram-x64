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
    internal partial class frmDispProg_ParallelLines : Form
    {
        public DispProg.TLine CmdLine = new DispProg.TLine();
        public int ProgNo = 0;
        public int LineNo = 0;
        public TPos2 SubOrigin = new TPos2(0, 0);

        public frmDispProg_ParallelLines()
        {
            InitializeComponent();
            GControl.LogForm(this);
            AutoSize = true;
        }

        private void UpdateDisplay()
        {
            lblHeadNo.Text = CmdLine.ID.ToString();
            lblModelNo.Text = CmdLine.IPara[0].ToString();

            string s = Enum.GetName(typeof(EVHType), CmdLine.IPara[3]);
            lblLineDirection.Text = $"{CmdLine.IPara[3]}-" + s;
            cbReverse.Checked = CmdLine.IPara[5] > 0;
            cbEnableWeight.Checked = CmdLine.IPara[4] > 0;
            cbDispense.Checked = CmdLine.IPara[2] > 0;

            lblLeadLength.Text = $"{CmdLine.DPara[0]:f3}";
            lblLagLength.Text = $"{CmdLine.DPara[1]:f3}";
            lblRelLeadStartHeight.Text = $"{CmdLine.DPara[2]:f3}";
            lblRelLagEndHeight.Text = $"{CmdLine.DPara[3]:f3}";
            lblStartOfst.Text = $"{CmdLine.DPara[6]:f3}";
            lblEndOfst.Text = $"{CmdLine.DPara[7]:f3}";
            lblAddLineTime.Text = $"{CmdLine.DPara[4]:f3}";

            lblX0.Text = CmdLine.X[0].ToString("F3");
            lblY0.Text = CmdLine.Y[0].ToString("F3");

            gbxWeight.Visible = CmdLine.IPara[4] > 0;
            lblLineWeight.Text = $"{CmdLine.DPara[21]:f3}";
            lblFirstLineWeight.Text = CmdLine.DPara[20] > 0 ? $"{CmdLine.DPara[20]:f3}": $"({CmdLine.DPara[21]:f3})";
            lblLastLineWeight.Text = CmdLine.DPara[22] > 0 ? $"{CmdLine.DPara[22]:f3}" : $"({CmdLine.DPara[21]:f3})";

            lblCutTailLength.Text = CmdLine.DPara[10].ToString("f3");
            lblCutTailSpeed.Text = CmdLine.DPara[11].ToString("f3");
            lblCutTailHeight.Text = CmdLine.DPara[12].ToString("f3");
            lblCutTailType.Text = CmdLine.DPara[13].ToString("f0");
        }

        private string CmdName
        {
            get
            {
                return LineNo.ToString("d3") + " " + CmdLine.Cmd;
            }
        }

        private void frmDispProg_ParallelLines_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);

            CmdLine.Copy(DispProg.Script[ProgNo].CmdList.Line[LineNo]);
            this.Text = CmdName;

            UpdateDisplay();
        }

        private void frmDispProg_ParallelLines_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmDispProg_ParallelLines_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DispProg.Script[ProgNo].CmdList.Line[LineNo].Copy(CmdLine);
            frm_DispProg2.Done = true;
            Log.OnAction("OK", CmdName);
            Close();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            frm_DispProg2.Done = true;
            Log.OnAction("Cancel", CmdName);
            Close();
        }

        private void lblHeadNo_Click(object sender, EventArgs e)
        {
            int i = Enum.GetNames(typeof(EHeadNo)).Length;
            UC.AdjustExec(CmdName + ", HeadNo", ref CmdLine.ID, 1, i);
            UpdateDisplay();
        }

        private void lblModelNo_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Model No", ref CmdLine.IPara[0], 0, TModelList.MAX_MODEL);
            UpdateDisplay();
        }
        private void cbDispense_Click(object sender, EventArgs e)
        {
            CmdLine.IPara[2] = CmdLine.IPara[2] > 0 ? 0 : 1;

            bool enabled = CmdLine.IPara[2] > 0;
            Log.OnSet(CmdName + ", Enable Weight", !enabled, enabled);

            UpdateDisplay();
        }

        private void btnEditModel_Click(object sender, EventArgs e)
        {
            frm_DispCore_DispProg_ModelList frmModelList = new frm_DispCore_DispProg_ModelList();
            frmModelList.ShowDialog();
        }

        private void cbEnableWeight_Click(object sender, EventArgs e)
        {
            if (CmdLine.IPara[4] > 0)
                CmdLine.IPara[4] = 0;
            else
                CmdLine.IPara[4] = 1;

            bool enabled = CmdLine.IPara[3] > 0;
            Log.OnSet(CmdName + ", Enable Weight", !enabled, enabled);

            UpdateDisplay();
        }

        private void lblCutTailLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", CutTail Length (mm)", ref CmdLine.DPara[10], 0, 10);
            UpdateDisplay();
        }

        private void lblCutTailSpeed_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", CutTail Length (mm/s)", ref CmdLine.DPara[11], 0, 100);
            UpdateDisplay();
        }

        private void lblCutTailHeight_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", CutTail Height (mm)", ref CmdLine.DPara[12], 0, 100);
            UpdateDisplay();
        }

        private void lblCutTailType_Click(object sender, EventArgs e)
        {
            int i = (int)CmdLine.DPara[13];
            UC.AdjustExec(CmdName + ", CutTail Type", ref i, ECutTailType.None);
            CmdLine.DPara[13] = i;
            UpdateDisplay();
        }

        private void lblLineDirection_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", LineDirection", ref CmdLine.IPara[3], EVHType.Hort);
            UpdateDisplay();
        }

        private void lblLineWeight_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", LineWeight", ref CmdLine.DPara[21], 0, 100);
            UpdateDisplay();
        }

        private void lblFirstLineWeight_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", FirstLineWeight", ref CmdLine.DPara[20], 0, 100);
            UpdateDisplay();
        }

        private void lblLastLineWeight_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", LastLineWeight", ref CmdLine.DPara[22], 0, 100);
            UpdateDisplay();
        }

        private void gbox_Pos_Enter(object sender, EventArgs e)
        {

        }

        private void lblX0_Click(object sender, EventArgs e)
        {
            double X = Math.Round(CmdLine.X[0], 3);
            UC.AdjustExec(CmdName + ", X1", ref X, -1000, 1000);
            CmdLine.X[0] = X;
            UpdateDisplay();
        }

        private void lblY0_Click(object sender, EventArgs e)
        {
            double Y = Math.Round(CmdLine.Y[0], 3);
            UC.AdjustExec(CmdName + ", Y1", ref Y, -1000, 1000);
            CmdLine.Y[0] = Y;
            UpdateDisplay();
        }

        private void btnEditXY0_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();
            frm.ParamName = LineNo.ToString() + " Start XY";
            frm.ValueX = CmdLine.X[0];
            frm.ValueY = CmdLine.Y[0];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CmdLine.X[0] = frm.ValueX;
                CmdLine.Y[0] = frm.ValueY;
            }

            UpdateDisplay();
        }

        private void btnSetXY0_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.RealTimeOffset(DispProg.ERealTimeOp.Minus, ref X, ref Y);

            CmdLine.X[0] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[0] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            Log.OnSet(CmdName + ", Set Start XY", Old, new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]));

            UpdateDisplay();
        }

        private void btnGotoXY0_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[0];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[0];

            DispProg.RealTimeOffset(DispProg.ERealTimeOp.Add, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }

        private void lblX1_Click(object sender, EventArgs e)
        {
            double X = Math.Round(CmdLine.X[1], 3);
            UC.AdjustExec(CmdName + ", X2", ref X, -1000, 1000);
            CmdLine.X[1] = X;
            UpdateDisplay();
        }

        private void lblY1_Click(object sender, EventArgs e)
        {
            double Y = Math.Round(CmdLine.Y[1], 3);
            UC.AdjustExec(CmdName + ", Y2", ref Y, -1000, 1000);
            CmdLine.Y[1] = Y;
            UpdateDisplay();
        }

        private void btnEditXY1_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();
            frm.ParamName = LineNo.ToString() + " Start XY";
            frm.ValueX = CmdLine.X[1];
            frm.ValueY = CmdLine.Y[1];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CmdLine.X[1] = frm.ValueX;
                CmdLine.Y[1] = frm.ValueY;
            }

            UpdateDisplay();
        }

        private void btnSetXY1_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.RealTimeOffset(DispProg.ERealTimeOp.Minus, ref X, ref Y);

            CmdLine.X[1] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[1] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            Log.OnSet(CmdName + ", Set Start XY", Old, new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]));

            UpdateDisplay();
        }

        private void btnGotoXY1_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[1];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[1];

            DispProg.RealTimeOffset(DispProg.ERealTimeOp.Add, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }

        private void cbReverse_Click(object sender, EventArgs e)
        {
            if (CmdLine.IPara[5] > 0)
                CmdLine.IPara[5] = 0;
            else
                CmdLine.IPara[5] = 1;

            bool enabled = CmdLine.IPara[5] > 0;
            Log.OnSet(CmdName + ", Enable Reverse", !enabled, enabled);

            UpdateDisplay();
        }

        private void lblLeadLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", LeadLength", ref CmdLine.DPara[0], 0, 100);
            UpdateDisplay();
        }
        private void lblLagLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", LagLength", ref CmdLine.DPara[1], 0, 100);
            UpdateDisplay();
        }
        private void lblRelLeadStartHeight_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", RelLeadStartHeight", ref CmdLine.DPara[2], -0.5, 0.5);
            UpdateDisplay();
        }
        private void lblRelLagEndHeight_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", RelLagEndHeight", ref CmdLine.DPara[3], -0.5, 0.5);
            UpdateDisplay();
        }
        private void lblStartOfst_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartOffset", ref CmdLine.DPara[6], -0.5, 0.5);
            UpdateDisplay();
        }
        private void lblEndOfst_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", EndOffset", ref CmdLine.DPara[7], -0.5, 0.5);
            UpdateDisplay();
        }
        private void lblAddLineTime_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", AddLineTime", ref CmdLine.DPara[4], -1000, 1000);
            UpdateDisplay();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
