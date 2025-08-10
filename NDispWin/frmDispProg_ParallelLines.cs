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
            //cbEnableWeight.Checked = CmdLine.IPara[4] > 0;
            s = Enum.GetName(typeof(EAmount), CmdLine.IPara[4]);
            lblAmount.Text = Text = $"{CmdLine.IPara[4]}-" + s;
            cbDispense.Checked = CmdLine.IPara[2] > 0;

            switch (CmdLine.IPara[4])
            {
                case 0: lblUnit.Text = "Not used"; break;
                case 1: lblUnit.Text = "Weight (mg)"; break;
                case 2: lblUnit.Text = "Volume (ul)"; break;
            }

            lblStartLength.Text = $"{CmdLine.DPara[0]:f3}";
            llEndLength.Text = $"{CmdLine.DPara[1]:f3}";

            if (CmdLine.DPara[2] == 0) CmdLine.DPara[2] = 0.1;
            if (CmdLine.DPara[3] == 0) CmdLine.DPara[3] = 0.1;
            lblStartGap.Text = $"{CmdLine.DPara[2]:f3}";
            lblEndGap.Text = $"{CmdLine.DPara[3]:f3}";

            lblSpeedAdjust.Text = $"{CmdLine.DPara[4]:f3}";

            lblStartOfst.Text = $"{CmdLine.DPara[6]:f3}";
            lblEndOfst.Text = $"{CmdLine.DPara[7]:f3}";

            lblStartVolume.Text = $"{CmdLine.DPara[8]:f3}";
            cbEndDisp.Checked = CmdLine.IPara[6] > 0;

            lblXY0.Text = $"{CmdLine.X[0]:f3},{CmdLine.Y[0]:f3}";
            lblXY1.Text = CmdLine.IPara[11] > 0 ? $"{CmdLine.X[1]:f3},{CmdLine.Y[1]:f3}": "";
            lblXY2.Text = CmdLine.IPara[12] > 0 ? $"{CmdLine.X[2]:f3},{CmdLine.Y[2]:f3}": "";
            double[] endPt = new double[] { 0, 0 };
            {
                TLayout layout = new TLayout();
                layout.Copy(DispProg.rt_Layouts[0]);

                //StartCR = (0, 0);
                Point lastCR;// = new Point(0, 0);
                int lastUnitNo = 0;//Last UnitNo of Hort/Vert line 

                EVHType vhType = EVHType.Hort;//0=Horizontal, 1=Vertical
                try { vhType = (EVHType)CmdLine.IPara[3]; } catch { };

                if (vhType == EVHType.Hort)
                {
                    lastCR = new Point(layout.UColCount - 1, 0);
                    layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current row.
                }
                else
                {
                    lastCR = new Point(0, layout.URowCount - 1);
                    layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current col.
                }

                endPt[0] = CmdLine.X[0] + DispProg.rt_LayoutRelPos[lastUnitNo].X;
                endPt[1] = CmdLine.Y[0] + DispProg.rt_LayoutRelPos[lastUnitNo].Y;
            }

            cbFirstLine.Checked = CmdLine.IPara[11] > 0;
            cbLastLine.Checked = CmdLine.IPara[12] > 0;

            //gbxWeight.Visible = CmdLine.IPara[4] > 0;
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
            Log.OnSet(CmdName + ", Disp", !enabled, enabled);

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

        private void lblXY0_Click(object sender, EventArgs e)
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
        private void lblXY1_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();
            frm.ParamName = LineNo.ToString() + " First Line Start XY";
            frm.ValueX = CmdLine.X[1];
            frm.ValueY = CmdLine.Y[1];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CmdLine.X[1] = frm.ValueX;
                CmdLine.Y[1] = frm.ValueY;
            }

            UpdateDisplay();
        }
        private void lblXY2_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();
            frm.ParamName = LineNo.ToString() + " Last Line Start XY";
            frm.ValueX = CmdLine.X[2];
            frm.ValueY = CmdLine.Y[2];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CmdLine.X[2] = frm.ValueX;
                CmdLine.Y[2] = frm.ValueY;
            }

            UpdateDisplay();
        }

        private void btnSetXY0_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.InvTranslate(0, ref X, ref Y);

            CmdLine.X[0] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[0] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            Log.OnSet(CmdName + ", Set Start XY", Old, new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]));

            UpdateDisplay();
        }
        private void btnGotoXY0_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[0];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[0];
            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void btnGotoEndXY_Click(object sender, EventArgs e)
        {
            TLayout layout = new TLayout();
            layout.Copy(DispProg.rt_Layouts[0]);

            //StartCR = (0, 0);
            Point lastCR;// = new Point(0, 0);
            int lastUnitNo = 0;//Last UnitNo of Hort/Vert line 

            EVHType vhType = EVHType.Hort;//0=Horizontal, 1=Vertical
            try { vhType = (EVHType)CmdLine.IPara[3]; } catch { };

            if (vhType == EVHType.Hort)
            {
                lastCR = new Point(layout.UColCount - 1, 0);
                layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current row.
            }
            else
            {
                lastCR = new Point(0, layout.URowCount - 1);
                layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current col.
            }

            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[0] + DispProg.rt_LayoutRelPos[lastUnitNo].X;
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[0] + DispProg.rt_LayoutRelPos[lastUnitNo].Y;

            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void cbFirstLine_Click(object sender, EventArgs e)
        {
            if (CmdLine.IPara[11] > 0)
                CmdLine.IPara[11] = 0;
            else
                CmdLine.IPara[11] = 1;

            bool enabled = CmdLine.IPara[11] > 0;
            Log.OnSet(CmdName + ", Enable Ind First Line", !enabled, enabled);

            UpdateDisplay();
        }
        private void btnSetXY1_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.InvTranslate(0, ref X, ref Y);

            CmdLine.X[1] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[1] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            Log.OnSet(CmdName + ", Set Start XY", Old, new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]));

            UpdateDisplay();
        }
        private void btnGotoXY1_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[1];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[1];

            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void cbLastLine_Click(object sender, EventArgs e)
        {
            if (CmdLine.IPara[12] > 0)
                CmdLine.IPara[12] = 0;
            else
                CmdLine.IPara[12] = 1;

            bool enabled = CmdLine.IPara[12] > 0;
            Log.OnSet(CmdName + ", Enable Ind Last Line", !enabled, enabled);

            UpdateDisplay();
        }
        private void btnSetXY2_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[2], CmdLine.Y[2]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            DispProg.InvTranslate(0, ref X, ref Y);

            CmdLine.X[2] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[2] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            Log.OnSet(CmdName + ", Set Start XY", Old, new NSW.Net.Point2D(CmdLine.X[2], CmdLine.Y[2]));

            UpdateDisplay();
        }
        private void btnGotoXY2_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[2];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[2];

            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }

        private void lblStartLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartLength", ref CmdLine.DPara[0], 0, 20);
            UpdateDisplay();
        }
        private void lblStartGap_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartGap", ref CmdLine.DPara[2], 0, 1);
            UpdateDisplay();
        }
        private void lblStartVolume_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartVolume", ref CmdLine.DPara[8], 0, 1);
            UpdateDisplay();
        }
        private void lblStartOfst_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartOffset", ref CmdLine.DPara[6], -0.5, 0.5);
            UpdateDisplay();
        }

        private void lblEndLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", EndLength", ref CmdLine.DPara[1], 0, 20);
            UpdateDisplay();
        }
        private void lblEndGap_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", EndGap", ref CmdLine.DPara[3], 0, 1);
            UpdateDisplay();
        }
        private void lblEndOfst_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", EndOffset", ref CmdLine.DPara[7], -0.5, 0.5);
            UpdateDisplay();
        }
        private void cbEndDisp_Click(object sender, EventArgs e)
        {
            CmdLine.IPara[6] = CmdLine.IPara[6] > 0 ? 0 : 1;

            bool enabled = CmdLine.IPara[6] > 0;
            Log.OnSet(CmdName + ", EndDispense", !enabled, enabled);

            UpdateDisplay();
        }

        private void lblSpeedAdjust_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", SpeedAdjust", ref CmdLine.DPara[4], -75, 75);
            UpdateDisplay();
        }

        enum EAmount { None, Weight, Volume};
        private void lblAmount_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Amount", ref CmdLine.IPara[4], EAmount.None);
            UpdateDisplay();
        }
    }
}
