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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            AutoSize = true;
        }

        private void UpdateDisplay()
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            lblHeadNo.Text = CmdLine.ID.ToString();
            lblModelNo.Text = CmdLine.IPara[0].ToString();

            cbDispense.Checked = CmdLine.IPara[2] > 0;

            lblReference.Text = $"{CmdLine.A[2]:f3},{CmdLine.B[2]:f3}";

            lblPointTL.Text = $"{CmdLine.X[0]:f3},{CmdLine.Y[0]:f3}";
            lblPointBR.Text = $"{CmdLine.X[1]:f3},{CmdLine.Y[1]:f3}";
            //if (cbMaster.Checked)
            //{
                lblMPointTL.Text = $"M: {CmdLine.A[0]:f3},{CmdLine.B[0]:f3}";
                lblMPointBR.Text = $"M: {CmdLine.A[1]:f3},{CmdLine.B[1]:f3}";
            //}
            lblMasterTol.Text = $"{CmdLine.DPara[4]:f3}";

            //cbxType.SelectedIndex = CmdLine.IPara[3];
            //lbkStartLength.Visible = CmdLine.IPara[3] == 1;
            //lblStartLength.Visible = CmdLine.IPara[3] == 1;
            //lbkEndLength.Visible = CmdLine.IPara[3] == 1;
            //lblEndLength.Visible = CmdLine.IPara[3] == 1;
            lbkStartGap.Visible = CmdLine.IPara[3] == 1;
            lblStartGap.Visible = CmdLine.IPara[3] == 1;

            lblStartGap.Text = $"{CmdLine.DPara[2]}";
            lblDispGap.Text = $"{Model.DispGap}";
            lblEndGap.Text = $"{CmdLine.DPara[3]}";
            lblRetGap.Text = $"{Model.RetGap}";

            lblAD.Text = $"{Model.LineAccel:f1}";
            lblInitialSpeed.Text = $"{Model.LineStartV:f1}";
            lblSpeed1.Text = $"{Model.LineSpeed:f1}";
            lblSpeed2Ratio.Text = $"{CmdLine.DPara[14]}";
            lblSpeedF.Text = $"{Model.LineSpeed2:f1}";

            if (CmdLine.DPara[14] <= 0) CmdLine.DPara[14] = 1;

            lblDownWait.Text = $"{Model.DnWait}";
            lblPostWait.Text = $"{Model.PostWait}";

            //cbTailOff.Checked = CmdLine.IPara[4] > 0;
            //cbSquare.Checked = CmdLine.IPara[5] > 0;

            lblHead1DefVolume.Text = $"{CmdLine.DPara[18]:f3}";
            lblHead2DefVolume.Text = $"{CmdLine.DPara[19]:f3}";
            //lblHead1Volume.Text = $"{(TFPump.PP4.DispAmounts[0] - TFPump.PP4.BSuckAmounts[0]):f3}";
            //lblHead2Volume.Text = $"{(TFPump.PP4.DispAmounts[1] - TFPump.PP4.BSuckAmounts[1]):f3}";
            lblBackSuck1.Text = $"{TFPump.PP4.BSuckAmounts[0]:f3}";
            lblBackSuck2.Text = $"{TFPump.PP4.BSuckAmounts[1]:f3}";

            if (CmdLine.DPara[10] + CmdLine.DPara[11] + CmdLine.DPara[11] + CmdLine.DPara[12] == 0)
            {
                CmdLine.DPara[10] = 25;
                CmdLine.DPara[11] = 25;
                CmdLine.DPara[12] = 25;
                CmdLine.DPara[13] = 25;
            }
            lblDot1Pc.Text = $"{CmdLine.DPara[10]}";
            lblDot2Pc.Text = $"{CmdLine.DPara[11]}";
            lblDot3Pc.Text = $"{CmdLine.DPara[12]}";
            lblDot4Pc.Text = $"{CmdLine.DPara[13]}";
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

        private void btnSetRefXY_Click(object sender, EventArgs e)
        {
            double x = TaskGantry.GXPos();
            double y = TaskGantry.GYPos();
            DispProg.InvTranslate(0, ref x, ref y);

            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.A[2], CmdLine.B[2]);
            CmdLine.A[2] = x - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.B[2] = y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            NSW.Net.Point2D New = new NSW.Net.Point2D(CmdLine.A[2], CmdLine.B[2]);
            Log.OnSet(CmdName + ", Ref XY", Old, New);

            UpdateDisplay();
        }
        private void btnGotoRefXY_Click(object sender, EventArgs e)
        {
            double x = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.A[2];
            double y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.B[2];
            DispProg.Translate(0, ref x, ref y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;
            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(x, y)) return;
        }

        private bool CheckInMasterTol(PointD point, PointD master, double tol)
        {
            //double tol = CmdLine.DPara[4];
            PointD xm = new PointD(master.X - tol, master.X + tol);
            PointD ym = new PointD(master.Y - tol, master.Y + tol);

            if (tol > 0 && (point.X < xm.X || point.X > xm.Y))
            {
                Msg MsgBox = new Msg();
                MsgBox.Show("TL Position X is out of Master Pos Tolerance.");
                return false;
            }
            if (tol > 0 && (point.Y < ym.X || point.Y > ym.Y))
            {
                Msg MsgBox = new Msg();
                MsgBox.Show("TL Position Y is out of Master Pos Tolerance.");
                return false;
            }

            return true;
        }

        private void btnSetPtTL_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D old = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.InvTranslate(0, ref X, ref Y);

            double x = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            double y = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            //double tol = CmdLine.DPara[4];
            //PointD xm = new PointD(CmdLine.A[0] - tol, CmdLine.A[0] + tol);
            //PointD ym = new PointD(CmdLine.B[0] - tol, CmdLine.B[0] + tol);

            if (!cbMaster.Checked)
            {
                if (CheckInMasterTol(new PointD(x, y), new PointD(CmdLine.A[0], CmdLine.B[0]), CmdLine.DPara[4]))
                {
                    CmdLine.X[0] = x;
                    CmdLine.Y[0] = y;
                    Log.OnSet(CmdName + ", Set Point TL", old, new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]));
                }
            }

            if (cbMaster.Checked)
            {
                CmdLine.X[0] = x;
                CmdLine.Y[0] = y;

                old = new NSW.Net.Point2D(CmdLine.A[0], CmdLine.B[0]);

                CmdLine.A[0] = CmdLine.X[0];
                CmdLine.B[0] = CmdLine.Y[0];

                Log.OnSet(CmdName + ", Set Point Master TL", old, new NSW.Net.Point2D(CmdLine.A[0], CmdLine.B[0]));
            }

            UpdateDisplay();
        }
        private void btnGotoPtTL_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[0];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[0];

            if (cbMaster.Checked)
            {
                X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.A[0];
                Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.B[0];
            }

            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lblPointTL_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();

            if (!cbMaster.Checked)
            {
                NSW.Net.Point2D old = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);

                frm.ParamName = LineNo.ToString() + " Point TL, XY";
                frm.ValueX = CmdLine.X[0];
                frm.ValueY = CmdLine.Y[0];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (CheckInMasterTol(new PointD(frm.ValueX, frm.ValueY), new PointD(CmdLine.A[0], CmdLine.B[0]), CmdLine.DPara[4]))
                    {
                        CmdLine.X[0] = frm.ValueX;
                        CmdLine.Y[0] = frm.ValueY;
                        Log.OnSet(CmdName + ", Set Point TL", old, new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]));
                    }
                }
            }
            else
            {
                frm.ParamName = LineNo.ToString() + " Master Point TL, XY";
                frm.ValueX = CmdLine.A[0];
                frm.ValueY = CmdLine.B[0];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CmdLine.X[0] = frm.ValueX;
                    CmdLine.Y[0] = frm.ValueY;

                    CmdLine.A[0] = frm.ValueX;
                    CmdLine.B[0] = frm.ValueY;
                }
            }

            UpdateDisplay();
        }

        private void btnSetPtBR_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D old = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();
            DispProg.InvTranslate(0, ref X, ref Y);

            double x = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            double y = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            //double tol = CmdLine.DPara[4];
            //PointD xm = new PointD(CmdLine.A[1] - tol, CmdLine.A[1] + tol);
            //PointD ym = new PointD(CmdLine.B[1] - tol, CmdLine.B[1] + tol);

            if (!cbMaster.Checked)
            {
                if (CheckInMasterTol(new PointD(x, y), new PointD(CmdLine.A[0], CmdLine.B[0]), CmdLine.DPara[4]))
                {
                    CmdLine.X[1] = x;
                    CmdLine.Y[1] = y;
                    Log.OnSet(CmdName + ", Set Point BR", old, new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]));
                }
            }

            if (cbMaster.Checked)
            {
                CmdLine.X[1] = x;
                CmdLine.Y[1] = y;

                old = new NSW.Net.Point2D(CmdLine.A[1], CmdLine.B[1]);

                CmdLine.A[1] = CmdLine.X[1];
                CmdLine.B[1] = CmdLine.Y[1];

                Log.OnSet(CmdName + ", Set Point Master BR", old, new NSW.Net.Point2D(CmdLine.A[1], CmdLine.B[1]));
            }

            UpdateDisplay();
        }
        private void btnGotoPtBR_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[1];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[1];

            if (cbMaster.Checked)
            {
                X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.A[1];
                Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.B[1];
            }

            DispProg.Translate(0, ref X, ref Y);

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lblPointBR_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();

            if (!cbMaster.Checked)
            {
                NSW.Net.Point2D old = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);

                frm.ParamName = LineNo.ToString() + " Point BR, XY";
                frm.ValueX = CmdLine.X[1];
                frm.ValueY = CmdLine.Y[1];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (CheckInMasterTol(new PointD(frm.ValueX, frm.ValueY), new PointD(CmdLine.A[1], CmdLine.B[1]), CmdLine.DPara[4]))
                    {
                        CmdLine.X[1] = frm.ValueX;
                        CmdLine.Y[1] = frm.ValueY;
                        Log.OnSet(CmdName + ", Set Point BR", old, new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]));
                    }
                }
            }
            else
            {
                frm.ParamName = LineNo.ToString() + " Master Point BR, XY";
                frm.ValueX = CmdLine.A[1];
                frm.ValueY = CmdLine.B[1];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CmdLine.A[1] = frm.ValueX;
                    CmdLine.B[1] = frm.ValueY;
                }
            }
            UpdateDisplay();
        }

        private void cbMaster_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void lblMasterTol_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Master Tol", ref CmdLine.DPara[4], 0, 5);
            UpdateDisplay();
        }

        private void lblStartGap_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", StartGap", ref CmdLine.DPara[2], 0, 5);
            UpdateDisplay();
        }
        private void lblDispGap_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
            
            double dispGap = Model.DispGap;
            UC.AdjustExec(CmdName + ", DispGap", ref dispGap, 0, 5);
            Model.DispGap = dispGap;

            UpdateDisplay();
        }
        private void lblEndGap_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", EndGap", ref CmdLine.DPara[3], 0, 5);
            UpdateDisplay();
        }
        private void lblRetGap_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            double retGap = Model.RetGap;
            UC.AdjustExec(CmdName + ", RetGap", ref retGap, 0, 15);
            Model.RetGap = retGap;

            UpdateDisplay();
        }

        private void lblAD_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            double lineAccel = Model.LineAccel;
            UC.AdjustExec(CmdName + ", LineAccel", ref lineAccel, 0, 5000);
            Model.LineAccel = lineAccel;

            UpdateDisplay();
        }
        private void lblInitialSpeed_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            double lineStartV = Model.LineStartV;
            UC.AdjustExec(CmdName + ", LineStartV", ref lineStartV, 0, 50);
            Model.LineStartV = lineStartV;
            UpdateDisplay();
        }
        private void lblSpeed_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            double lineSpeed = Model.LineSpeed;
            UC.AdjustExec(CmdName + ", LineSpeed", ref lineSpeed, 0, 100);
            Model.LineSpeed = lineSpeed;

            UpdateDisplay();
        }
        private void lblSpeed2Ratio_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Speed2Ratio", ref CmdLine.DPara[14], 0.1, 10);
            UpdateDisplay();
        }
        private void lblSpeedF_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            double lineSpeed2 = Model.LineSpeed2;
            UC.AdjustExec(CmdName + ", LineSpeed2", ref lineSpeed2, 0, 500);
            Model.LineSpeed2 = lineSpeed2;

            UpdateDisplay();
        }

        private void lblDownWait_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            int dnWait = Model.DnWait;
            UC.AdjustExec(CmdName + ", DownWait", ref dnWait, 0, 50000);
            Model.DnWait = dnWait;

            UpdateDisplay();
        }
        private void lblPostWait_Click(object sender, EventArgs e)
        {
            int modelNo = CmdLine.IPara[0];
            TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);

            int postWait = Model.PostWait;
            UC.AdjustExec(CmdName + ", PostWait", ref postWait, 0, 50000);
            Model.PostWait = postWait;

            UpdateDisplay();
        }

        private void lblDot1Pc_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Dot1Pc", ref CmdLine.DPara[10], 0, 100);
            CmdLine.DPara[11] = 100 - CmdLine.DPara[10] - CmdLine.DPara[12] - CmdLine.DPara[13];
            UpdateDisplay();
        }
        private void lblDot2Pc_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Dot2Pc", ref CmdLine.DPara[11], 0, 100);
            CmdLine.DPara[12] = 100 - CmdLine.DPara[10] - CmdLine.DPara[11] - CmdLine.DPara[13];
            UpdateDisplay();
        }
        private void lblDot3Pc_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Dot3Pc", ref CmdLine.DPara[12], 0, 100);
            CmdLine.DPara[13] = 100 - CmdLine.DPara[10] - CmdLine.DPara[11] - CmdLine.DPara[12];
            UpdateDisplay();
        }
        private void lblDot4Pc_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Dot4Pc", ref CmdLine.DPara[13], 0, 100);
            CmdLine.DPara[10] = 100 - CmdLine.DPara[11] - CmdLine.DPara[12] - CmdLine.DPara[13];
            UpdateDisplay();
        }

        private void lblHead1DefVolume_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Head1DefNettVolume", ref CmdLine.DPara[18], 0, 100);
            UpdateDisplay();
        }
        private void lblHead2DefVolume_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Head2DefNettVolume", ref CmdLine.DPara[19], 0, 100);
            UpdateDisplay();
        }
        private void lblBackSuck1_Click(object sender, EventArgs e)
        {
            double d = TFPump.PP4.BSuckAmounts[0];
            if (UC.AdjustExec("PP PB Backsuck Amount", ref d, 0, 1300))
                TFPump.PP4.BSuckAmounts = new double[] { d, TFPump.PP4.BSuckAmounts[1] };
            UpdateDisplay();
        }
        private void lblBackSuck2_Click(object sender, EventArgs e)
        {
            double d = TFPump.PP4.BSuckAmounts[1];
            if (UC.AdjustExec("PP PA Backsuck Amount", ref d, 0, 1300))
                TFPump.PP4.BSuckAmounts = new double[] { TFPump.PP4.BSuckAmounts[1], d };
            UpdateDisplay();
        }
    }
}
