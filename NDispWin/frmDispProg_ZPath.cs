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
    internal partial class frmDispProg_ZPath : Form
    {
        public DispProg.TLine CmdLine = new DispProg.TLine();
        public int ProgNo = 0;
        public int LineNo = 0;
        public TPos2 SubOrigin = new TPos2(0, 0);

        public frmDispProg_ZPath()
        {
            InitializeComponent();
            GControl.LogForm(this);
            AutoSize = true;
        }

        private void UpdateDisplay()
        {
            lblHeadNo.Text = CmdLine.ID.ToString();
            lblModelNo.Text = CmdLine.IPara[0].ToString();
        }
        private string CmdName
        {
            get
            {
                return LineNo.ToString("d3") + " " + CmdLine.Cmd;
            }
        }

        private void frmDispProg_ZPath_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);

            CmdLine.Copy(DispProg.Script[ProgNo].CmdList.Line[LineNo]);
            this.Text = CmdName;

            UpdateDisplay();
        }
        private void frmDispProg_ZPath_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void frmDispProg_ZPath_FormClosed(object sender, FormClosedEventArgs e)
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
        private void btnEditModel_Click(object sender, EventArgs e)
        {
            frm_DispCore_DispProg_ModelList frmModelList = new frm_DispCore_DispProg_ModelList();
            frmModelList.ShowDialog();
        }
        private void cbDispense_Click(object sender, EventArgs e)
        {
            CmdLine.IPara[2] = CmdLine.IPara[2] > 0 ? 0 : 1;

            bool enabled = CmdLine.IPara[2] > 0;
            Log.OnSet(CmdName + ", Disp", !enabled, enabled);

            UpdateDisplay();
        }
        private void cbEnableWeight_Click(object sender, EventArgs e)
        {

        }

        private void btnSetPtTL_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.InvTranslate(0, ref X, ref Y);

            CmdLine.X[0] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[0] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            Log.OnSet(CmdName + ", Set Point TL", Old, new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]));

            UpdateDisplay();
        }
        private void btnGotoPtTL_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[0];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[0];
            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lblPointTL_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();
            frm.ParamName = LineNo.ToString() + " Point TL, Start XY";
            frm.ValueX = CmdLine.X[0];
            frm.ValueY = CmdLine.Y[0];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CmdLine.X[0] = frm.ValueX;
                CmdLine.Y[0] = frm.ValueY;
            }

            UpdateDisplay();
        }

        private void btnSetPtBR_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.InvTranslate(0, ref X, ref Y);

            CmdLine.X[1] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[1] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            Log.OnSet(CmdName + ", Set Point BR", Old, new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]));

            UpdateDisplay();
        }
        private void btnGotoPtBR_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[1];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[1];

            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lblPointBR_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();
            frm.ParamName = LineNo.ToString() + " Point BR, End XY";
            frm.ValueX = CmdLine.X[1];
            frm.ValueY = CmdLine.Y[1];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CmdLine.X[1] = frm.ValueX;
                CmdLine.Y[1] = frm.ValueY;
            }

            UpdateDisplay();
        }

        private void lblStartLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartLength", ref CmdLine.DPara[0], 0, 10);
            UpdateDisplay();
        }
        private void lblEndLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", EndLength", ref CmdLine.DPara[1], 0, 10);
            UpdateDisplay();
        }
        private void lblStartGap_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartGap", ref CmdLine.DPara[2], 0, 1);
            UpdateDisplay();
        }
        private void lblDispGap_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
            
            double dispGap = Model.DispGap;
            UC.AdjustExec(CmdName + ", DispGap", ref dispGap, 0, 1);
            Model.DispGap = dispGap;

            UpdateDisplay();
        }
        private void lblEndGap_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", EndGap", ref CmdLine.DPara[3], 0, 1);
            UpdateDisplay();
        }

        private void lblAD_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", AccelDecel", ref CmdLine.DPara[30], 0, 5000);
            UpdateDisplay();
        }
        private void lblInitialSpeed_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Initial Speed", ref CmdLine.DPara[31], 0, 5000);
            UpdateDisplay();
        }
        private void lblSpeed_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Speed", ref CmdLine.DPara[32], 0, 5000);
            UpdateDisplay();
        }
        private void lblSpeed2_Click(object sender, EventArgs e)
        {
        }
        private void lblSpeedF_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", SpeedF", ref CmdLine.DPara[33], 0, 5000);
            UpdateDisplay();
        }

        private void lblDownWait_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            double dnWait = Model.DnWait;
            UC.AdjustExec(CmdName + ", DownWait", ref dnWait, 0, 1);
            Model.DnWait = dnWait;

            UpdateDisplay();
        }

        private void lblPostWait_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            double postWait = Model.DnWait;
            UC.AdjustExec(CmdName + ", PostWait", ref postWait, 0, 1);
            Model.DnWait = postWait;

            UpdateDisplay();
        }

        //PAR_LINES = 461,
        /* Parameters
        ID              nil
        IPara[0..9]     [ModelNo, .1., Disp, VHType, UseWeight, Reverse, EndDisp, .7., .8., .9.]
        IPara[10..19]   [.10., IndFirstLine, IndLastLine, .13., .14., .15., .16., .17., .18., .19.]

        DPara[0..9]     [StartLen, EndLen, StartGap, EndGap, .4., .5., .6., .7., .8., .9.]

        DPara[10..19]   [CutTailLength, Speed, Height, Type, ..]
        DPara[20..29]   [FirstLineMass, LineMass, LastLineMass, ..]
        DPara[30..39]   [AD, InitialSpeed, Speed, SpeedF, ..]
        X[0..99]        [PointTL, ..]
        Y[0..99]        [PointBR, ..]
        */
    }
}
