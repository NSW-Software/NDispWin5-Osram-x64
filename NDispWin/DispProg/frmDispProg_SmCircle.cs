using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NDispWin
{
    internal partial class frm_DispCore_DispProg_SmCircle : Form
    {
        public DispProg.TLine CmdLine = new DispProg.TLine();
        public int ProgNo = 0;
        public int LineNo = 0;
        public TPos2 SubOrigin = new TPos2(0, 0);

        public frm_DispCore_DispProg_SmCircle()
        {
            InitializeComponent();
            GControl.LogForm(this);

            TopLevel = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            //AppLanguage.Func.SetComponent(this);
        }

        static NSW.Net.Point2D StartPt = new NSW.Net.Point2D(0, 0);
        static NSW.Net.Point2D CenterPt = new NSW.Net.Point2D(0, 0);
        static double Radius = 0;
        static double StartA = 0;
        static double EndA = 0;
        static double SweepA = 0;
        static double Dir = 0;
        bool StartPtValid = false;

        private void UpdateDisplayOnce()
        {
            int Line = LineNo;
            while (Line > 0)
            {
                Line--;
                if (DispProg.Script[ProgNo].CmdList.Line[Line].Cmd == DispProg.ECmd.LINE ||
                    DispProg.Script[ProgNo].CmdList.Line[Line].Cmd == DispProg.ECmd.MOVE ||
                    DispProg.Script[ProgNo].CmdList.Line[Line].Cmd == DispProg.ECmd.DOT)
                {
                    StartPt.X = DispProg.Script[ProgNo].CmdList.Line[Line].X[0];
                    StartPt.Y = DispProg.Script[ProgNo].CmdList.Line[Line].Y[0];
                    StartPtValid = true;
                    break;
                }
                if (DispProg.Script[ProgNo].CmdList.Line[Line].Cmd == DispProg.ECmd.ARC)
                {
                    StartPt.X = DispProg.Script[ProgNo].CmdList.Line[Line].X[1];
                    StartPt.Y = DispProg.Script[ProgNo].CmdList.Line[Line].Y[1];
                    StartPtValid = true;
                    break;
                }
            }
        }

        private void UpdateDisplay()
        {
            if (CmdLine.ID > Enum.GetNames(typeof(EHeadNo)).Length) CmdLine.ID = 1;

            lbl_HeadNo.Text = CmdLine.ID.ToString();

            if (CmdLine.IPara[0] > TModelList.MAX_MODEL - 2) CmdLine.IPara[0] = 0;
            lbl_ModelNo.Text = CmdLine.IPara[0].ToString() + ", " + (CmdLine.IPara[0] + 1).ToString() + ", " + (CmdLine.IPara[0] + 2).ToString();

            lbl_Dispense.Text = (CmdLine.IPara[2] > 0).ToString();

            lbl_X1.Text = CmdLine.X[0].ToString("F3");
            lbl_Y1.Text = CmdLine.Y[0].ToString("F3");
            lbl_X2.Text = CmdLine.X[1].ToString("F3");
            lbl_Y2.Text = CmdLine.Y[1].ToString("F3");

            lbl_EndOfst.Text = CmdLine.DPara[10].ToString("f3");

            lbl_Diameter.BackColor = SystemColors.Window;
            if (StartPtValid && LineNo > 0)
            {
                lbl_StartXY.Text = StartPt.X.ToString("F3") + ", " + StartPt.Y.ToString("F3");

                double X2 = CmdLine.X[0];
                double Y2 = CmdLine.Y[0];
                double X3 = CmdLine.X[1];
                double Y3 = CmdLine.Y[1];
                try
                {
                    GDefine.Arc3PGetInfo(StartPt.X, StartPt.Y, X2, Y2, X3, Y3, ref CenterPt.X, ref CenterPt.Y, ref Radius, ref StartA, ref EndA, ref SweepA, ref Dir);
                }
                catch
                {
                    lbl_XC.Text = "invalid";
                    lbl_YC.Text = "invalid";
                    lbl_Dir.Text = "invalid";
                    lbl_Diameter.Text = "invalid";
                    lbl_Length.Text = "invalid";

                    lbl_StartArcLength.Text = "invalid";
                    lbl_StartArc_EndXY.Text = "invalid";
                    lbl_MainArcLength.Text = "invalid";
                    lbl_MainArc_EndXY.Text = "invalid";
                    lbl_OverlapArcLength.Text = "invalid";
                    lbl_OverlapArc_EndXY.Text = "invalid";
                    lbl_EndArcLength.Text = "invalid";
                    lbl_EndArc_EndXY.Text = "invalid";
                    
                    goto _Cont;
                }
                lbl_XC.Text = CenterPt.X.ToString("F3");
                lbl_YC.Text = CenterPt.Y.ToString("F3");
                string s_Dir = "CW";
                //if (GDefine.GantryConfig == GDefine.EGantryConfig.XZ_YTABLE)
                //{
                //    if (Dir < 0) s_Dir = "CCW";
                //}
                //else
                //    if (Dir > 0) s_Dir = "CCW";
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XZ_YTABLE) Dir = -Dir;
                if (Dir < 0) s_Dir = "CCW";
                lbl_Dir.Text = s_Dir;

                lbl_Diameter.Text = (Radius * 2).ToString("F4");

                //if (CmdLine.Cmd == DispProg.ECmd.CIRC)
                lbl_Length.Text = (Radius * 2 * Math.PI).ToString("F4");
                //else
                //    lbl_Length.Text = (Radius * SweepA).ToString("F4");
                //lbl_Angle.Text = (SweepA / Math.PI * 180).ToString("F3");

                /*
                double QuarterLength = (Radius * 2 * Math.PI)/4;

                if (CmdLine.DPara[2] > QuarterLength) CmdLine.DPara[2] = QuarterLength; 
                lbl_StartArcLength.Text = CmdLine.DPara[2].ToString("f3");
                double StartArc_EndX = 0;
                double StartArc_EndY = 0;
                try
                {
                    GDefine.ArcStartCenterLengthGetEndPt(StartPt.X, StartPt.Y, CenterPt.X, CenterPt.Y, Dir < 0, CmdLine.DPara[2], ref StartArc_EndX, ref StartArc_EndY);
                }
                catch { }
                lbl_StartArc_EndXY.Text = StartArc_EndX.ToString("f3") + ", " + StartArc_EndY.ToString("f3");

                lbl_MainArcLength.Text = ((Radius * 2 * Math.PI) - CmdLine.DPara[2]).ToString("f3");
                lbl_MainArc_EndXY.Text = StartPt.X.ToString("f3") + ", " + StartPt.Y.ToString("f3");

                if (CmdLine.DPara[3] > CmdLine.DPara[2]) CmdLine.DPara[3] = CmdLine.DPara[2];
                lbl_OverlapArcLength.Text = CmdLine.DPara[3].ToString("f3");
                double OverlapArc_EndX = 0;
                double OverlapArc_EndY = 0;
                try
                {
                    GDefine.ArcStartCenterLengthGetEndPt(StartPt.X, StartPt.Y, CenterPt.X, CenterPt.Y, Dir < 0, CmdLine.DPara[3], ref OverlapArc_EndX, ref OverlapArc_EndY);
                }
                catch { }
                lbl_OverlapArc_EndXY.Text = OverlapArc_EndX.ToString("f3") + ", " + OverlapArc_EndY.ToString("f3");

                if (CmdLine.DPara[4] > QuarterLength) CmdLine.DPara[4] = QuarterLength;
                lbl_EndArcLength.Text = CmdLine.DPara[4].ToString("f3");
                double EndArc_EndX = 0;
                double EndArc_EndY = 0;
                try
                {
                    GDefine.ArcStartCenterLengthGetEndPt(OverlapArc_EndX, OverlapArc_EndY, CenterPt.X, CenterPt.Y, Dir < 0, CmdLine.DPara[4], ref EndArc_EndX, ref EndArc_EndY);
                }
                catch { }
                lbl_EndArc_EndXY.Text = EndArc_EndX.ToString("f3") + ", " + EndArc_EndY.ToString("f3");
                */

                double StartArc_EndX = 0;
                double StartArc_EndY = 0;
                double MainArc_EndX = 0;
                double MainArc_EndY = 0;
                double OverlapArc_EndX = 0;
                double OverlapArc_EndY = 0;
                double EndArc_EndX = 0;
                double EndArc_EndY = 0;
                double MainArc_Len = 0;

                GDefine.SmCircleGetInfo(StartPt.X, StartPt.Y, CenterPt.X, CenterPt.Y, Dir < 0,
                    ref CmdLine.DPara[2], ref MainArc_Len, ref CmdLine.DPara[3], ref CmdLine.DPara[4],
                    ref StartArc_EndX, ref StartArc_EndY, ref MainArc_EndX, ref MainArc_EndY, 
                    ref OverlapArc_EndX, ref OverlapArc_EndY, ref EndArc_EndX, ref EndArc_EndY);

                lbl_StartArcLength.Text = CmdLine.DPara[2].ToString("f3");
                lbl_StartArc_EndXY.Text = StartArc_EndX.ToString("f3") + ", " + StartArc_EndY.ToString("f3");

                lbl_MainArcLength.Text = ((Radius * 2 * Math.PI) - CmdLine.DPara[2]).ToString("f3");
                lbl_MainArc_EndXY.Text = StartPt.X.ToString("f3") + ", " + StartPt.Y.ToString("f3");

                lbl_OverlapArcLength.Text = CmdLine.DPara[3].ToString("f3");
                lbl_OverlapArc_EndXY.Text = OverlapArc_EndX.ToString("f3") + ", " + OverlapArc_EndY.ToString("f3");

                lbl_EndArcLength.Text = CmdLine.DPara[4].ToString("f3");
                lbl_EndArc_EndXY.Text = EndArc_EndX.ToString("f3") + ", " + EndArc_EndY.ToString("f3");

                lbl_StartArcModel.Text = "M" + CmdLine.IPara[0].ToString();
                lbl_MainArcModel.Text = "M" + (CmdLine.IPara[0] + 1).ToString();
                lbl_OverlapArcModel.Text = "M" + (CmdLine.IPara[0] + 1).ToString();
                lbl_EndArcModel.Text = "M" + (CmdLine.IPara[0] + 2).ToString();

                double d_StartArcTime = CmdLine.DPara[2]/(double)DispProg.ModelList.Model[CmdLine.IPara[0]].Para[(int)TModelList.EModel.LineSpeed];
                lbl_StartArc_Time.Text = d_StartArcTime.ToString("f3");
                double d_MainArcTime = MainArc_Len / (double)DispProg.ModelList.Model[CmdLine.IPara[0] + 1].Para[(int)TModelList.EModel.LineSpeed];
                lbl_MainArc_Time.Text = d_MainArcTime.ToString("f3");
                double d_OverlapArcTime = CmdLine.DPara[3] / (double)DispProg.ModelList.Model[CmdLine.IPara[0] + 1].Para[(int)TModelList.EModel.LineSpeed];
                lbl_OverlapArc_Time.Text = d_OverlapArcTime.ToString("f3");
                double d_EndArcTime = CmdLine.DPara[4] / (double)DispProg.ModelList.Model[CmdLine.IPara[0] + 2].Para[(int)TModelList.EModel.LineSpeed];
                lbl_EndArc_Time.Text = d_EndArcTime.ToString("f3");
            }
            else
            {
                lbl_StartXY.Text = "undetermined";
                lbl_XC.Text = "undetermined";
                lbl_YC.Text = "undetermined";
                lbl_Dir.Text = "undetermined";
                lbl_Diameter.Text = "undetermined";
                lbl_Length.Text = "undetermined";

                lbl_StartArcLength.Text = "undetermined";
                lbl_StartArc_EndXY.Text = "undetermined";
                lbl_MainArcLength.Text = "undetermined";
                lbl_MainArc_EndXY.Text = "undetermined";
                lbl_OverlapArcLength.Text = "undetermined";
                lbl_OverlapArc_EndXY.Text = "undetermined";
                lbl_EndArcLength.Text = "undetermined";
                lbl_EndArc_EndXY.Text = "undetermined";
            }
        _Cont:
            { }
        }

        private string CmdName
        {
            get
            {
                return LineNo.ToString("d3") + " " + CmdLine.Cmd.ToString();
            }
        }

        private void frmDispProg_SmCircle_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);

            CmdLine.Copy(DispProg.Script[ProgNo].CmdList.Line[LineNo]);

            this.Text = CmdName;

            UpdateDisplayOnce();
            UpdateDisplay();

            try
            {
                TaskDisp.TaskMoveGZFocus(CmdLine.IPara[21]);
            }
            catch { };
        }

        private void lbl_HeadNo_Click(object sender, EventArgs e)
        {
            int i = Enum.GetNames(typeof(EHeadNo)).Length;
            UC.AdjustExec(CmdName + ", HeadNo", ref CmdLine.ID, 1, i);
            UpdateDisplay();
        }
        private void lbl_ModelNo_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", ModelNo", ref CmdLine.IPara[0], 0, TModelList.MAX_MODEL - 2);
            UpdateDisplay();
        }
        private void btn_EditModel_Click(object sender, EventArgs e)
        {
            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker)
            {
                //TaskVision.frmGenImageView.TopMost = false;
            }
            frm_DispCore_DispProg_ModelList frmModelList = new frm_DispCore_DispProg_ModelList();
            frmModelList.ShowDialog();

            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker)
            {
                //TaskVision.frmGenImageView.TopMost = true;
            }
            UpdateDisplay();
        }
        private void lbl_Dispense_Click(object sender, EventArgs e)
        {
            if (CmdLine.IPara[2] > 0) CmdLine.IPara[2] = 0; else CmdLine.IPara[2] = 1;
            UpdateDisplay();
        }

        private void btn_SetStartXY_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(StartPt.X, StartPt.Y);
            StartPt.X = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            StartPt.Y = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            NSW.Net.Point2D New = new NSW.Net.Point2D(StartPt.X, StartPt.Y);

            Log.OnSet(CmdName + ", StartXY", Old, New);

            UpdateDisplay();
        }
        private void btn_GotoStartXY_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + StartPt.X;
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + StartPt.Y;

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lbl_X1_Click(object sender, EventArgs e)
        {
            double X = Math.Round(CmdLine.X[0], 3);
            UC.AdjustExec(CmdName + ", X1", ref X, -1000, 1000);
            CmdLine.X[0] = X;
            UpdateDisplay();
        }
        private void lbl_Y1_Click(object sender, EventArgs e)
        {
            double Y = Math.Round(CmdLine.Y[0], 3);
            UC.AdjustExec(CmdName + ", Y1", ref Y, -1000, 1000);
            CmdLine.Y[0] = Y;
            UpdateDisplay();
        }
        private void btn_SetPt1Pos_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);
            CmdLine.X[0] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[0] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            NSW.Net.Point2D New = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);

            Log.OnSet(CmdName + ", Pt1", Old, New);

            UpdateDisplay();
        }

        private void btn_GotoPt1Pos_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[0];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[0];

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lbl_X2_Click(object sender, EventArgs e)
        {
            double X = Math.Round(CmdLine.X[1], 3);
            UC.AdjustExec(CmdName + ", X2", ref X, -1000, 1000);
            CmdLine.X[1] = X;
            UpdateDisplay();
        }
        private void lbl_Y2_Click(object sender, EventArgs e)
        {
            double Y = Math.Round(CmdLine.Y[1], 3);
            UC.AdjustExec(CmdName + ", Y2", ref Y, -1000, 1000);
            CmdLine.Y[1] = Y;
            UpdateDisplay();
        }
        private void btn_SetPt2Pos_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);
            CmdLine.X[1] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[1] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            NSW.Net.Point2D New = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);

            Log.OnSet(CmdName + ", Pt2", Old, New);

            UpdateDisplay();
        }
        private void btn_GotoPt2Pos_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[1];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[1];

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }

        private void TranslateCenter(double NewCenterX, double NewCenterY)
        {
            NSW.Net.Point2D NewCenter = new NSW.Net.Point2D(NewCenterX, NewCenterY);

            //***Convert to Cartesion
            NSW.Net.Point2D Pt_1 = new NSW.Net.Point2D(StartPt.X, StartPt.Y);
            NSW.Net.Point2D Pt_2 = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);
            NSW.Net.Point2D Pt_3 = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);

            //***Translate Position
            Pt_1 = Pt_1.Translate(CenterPt, NewCenter);
            Pt_2 = Pt_2.Translate(CenterPt, NewCenter);
            Pt_3 = Pt_3.Translate(CenterPt, NewCenter);

            StartPt.X = Pt_1.X;
            StartPt.Y = Pt_1.Y;
            CmdLine.X[0] = Pt_2.X;
            CmdLine.Y[0] = Pt_2.Y;
            CmdLine.X[1] = Pt_3.X;
            CmdLine.Y[1] = Pt_3.Y;

            //UpdateDisplay();
        }


        private void lbl_XC_Click(object sender, EventArgs e)
        {
            if (LineNo == 0)
            {
                MessageBox.Show("Set Diameter not valid for first command");
                UpdateDisplay();
                return;
            }

            if (!(DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.LINE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.MOVE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.ARC))
            {
                MessageBox.Show("Set Diameter not valid when previous command not of draw type");
                UpdateDisplay();
                return;
            }

            DialogResult dr = MessageBox.Show("Set Diameter will update previous command position", "", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel) return;

            double X = Math.Round(CenterPt.X, 3);
            UC.AdjustExec(CmdName + ", Center X", ref X, -1000, 1000);

            TranslateCenter(X, CenterPt.Y);

            UpdateDisplay();
        }
        private void lbl_YC_Click(object sender, EventArgs e)
        {
            if (LineNo == 0)
            {
                MessageBox.Show("Set Diameter not valid for first command");
                UpdateDisplay();
                return;
            }

            if (!(DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.LINE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.MOVE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.ARC))
            {
                MessageBox.Show("Set Diameter not valid when previous command not of draw type");
                UpdateDisplay();
                return;
            }

            DialogResult dr = MessageBox.Show("Set Diameter will update previous command position", "", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel) return;

            double Y = Math.Round(CenterPt.Y, 3);
            UC.AdjustExec(CmdName + ", Center Y", ref Y, -1000, 1000);

            TranslateCenter(CenterPt.X, Y);

            UpdateDisplay();

        }
        private void btn_SetCenterPos_Click(object sender, EventArgs e)
        {
            if (LineNo == 0)
            {
                MessageBox.Show("Set Diameter not valid for first command");
                UpdateDisplay();
                return;
            }

            if (!(DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.LINE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.MOVE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.ARC))
            {
                MessageBox.Show("Set Diameter not valid when previous command not of draw type");
                UpdateDisplay();
                return;
            }

            DialogResult dr = MessageBox.Show("Set Diameter will update previous command position", "", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel) return;

            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(CenterPt.X, CenterPt.Y);

            NSW.Net.Point2D NewCenter = new NSW.Net.Point2D(0, 0);
            NewCenter.X = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            NewCenter.Y = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);

            TranslateCenter(NewCenter.X, NewCenter.Y);

            Log.OnSet(CmdName + ", Center", Old, NewCenter);

            UpdateDisplay();
        }
        private void btn_EditCenter_Click(object sender, EventArgs e)
        {
            frm_DispCore_EditXY frm = new frm_DispCore_EditXY();

            frm.ParamName = CmdName + ", Center";
            frm.ValueX = CenterPt.X;
            frm.ValueY = CenterPt.Y;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                TranslateCenter(frm.ValueX, frm.ValueY);
            }
            UpdateDisplay();
        }
        private void btn_CenterXY_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CenterPt.X;
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CenterPt.Y;

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void btn_GotoCenterXY_Click(object sender, EventArgs e)
        {
            //double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CenterPt.X;
            //double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CenterPt.Y;

            //if (!TaskDisp.TaskMoveGZZ2Up()) return;

            //if (!TaskGantry.SetMotionParamGXY()) return;
            //if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lbl_Dir_Click(object sender, EventArgs e)
        {
            double tempX = CmdLine.X[0];
            double tempY = CmdLine.Y[0];

            CmdLine.X[0] = CmdLine.X[1];
            CmdLine.Y[0] = CmdLine.Y[1];

            CmdLine.X[1] = tempX;
            CmdLine.Y[1] = tempY;

            UpdateDisplay();
        }
        private void lbl_Diameter_Click(object sender, EventArgs e)
        {
            double d = Math.Round(Radius * 2, 4);
            UC.AdjustExec(CmdName + ", Diameter (mm)", ref d, 1, 80);
            Radius = d / 2;

            lbl_Diameter.BackColor = Color.Orange;
            lbl_Diameter.Text = d.ToString("f4");
        }

        private void btn_SetDiameterC_Click(object sender, EventArgs e)
        {
            if (LineNo == 0)
            {
                MessageBox.Show("Set Diameter not valid for first command");
                UpdateDisplay();
                return;
            }

            if (!(DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.LINE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.MOVE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.ARC))
            {
                MessageBox.Show("Set Diameter not valid when previous command not of draw type");
                UpdateDisplay();
                return;
            }

            DialogResult dr = MessageBox.Show("Set Diameter will update previous command position", "", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel) return;

            NSW.Net.Point2D Pt_1 = new NSW.Net.Point2D(StartPt.X, StartPt.Y);
            NSW.Net.Point2D Pt_2 = new NSW.Net.Point2D(CmdLine.X[0], CmdLine.Y[0]);
            NSW.Net.Point2D Pt_3 = new NSW.Net.Point2D(CmdLine.X[1], CmdLine.Y[1]);

            NSW.Net.Polar Polar_1 = new NSW.Net.Polar(CenterPt, StartPt);
            NSW.Net.Polar Polar_2 = new NSW.Net.Polar(CenterPt, Pt_2);
            NSW.Net.Polar Polar_3 = new NSW.Net.Polar(CenterPt, Pt_3);

            double Old = Polar_1.R;
            Polar_1.R = Radius;
            Polar_2.R = Radius;
            Polar_3.R = Radius;

            CmdLine.X[0] = Polar_2.Point2D.X + CenterPt.X;
            CmdLine.Y[0] = Polar_2.Point2D.Y + CenterPt.Y;
            CmdLine.X[1] = Polar_3.Point2D.X + CenterPt.X;
            CmdLine.Y[1] = Polar_3.Point2D.Y + CenterPt.Y;

            StartPt.X = Polar_1.Point2D.X + CenterPt.X;
            StartPt.Y = Polar_1.Point2D.Y + CenterPt.Y;

            Log.OnSet(CmdName + ", Diameter", Old, Radius);

            UpdateDisplay();
        }

        private void lbl_EndOfst_Click(object sender, EventArgs e)
        {
            double Limit = Math.Round(Radius / 4, 3);
            UC.AdjustExec(CmdName + ", End Ofst (mm)", ref CmdLine.DPara[10], -Limit, Limit);
            CmdLine.DPara[10] = Math.Round(CmdLine.DPara[10], 3);
            UpdateDisplay();
        }

        private void lbl_StartArcLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Start Arc Length (mm)", ref CmdLine.DPara[2], 0, 200);
            CmdLine.DPara[2] = Math.Round(CmdLine.DPara[2], 3); 
            UpdateDisplay();
        }
        private void lbl_OverlapArcLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Overlap Arc Length (mm)", ref CmdLine.DPara[3], 0, CmdLine.DPara[2]);
            UpdateDisplay();
        }
        private void lbl_EndArcLength_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", End Arc Length (mm)", ref CmdLine.DPara[4], 0, 50);
            UpdateDisplay();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DispProg.Script[ProgNo].CmdList.Line[LineNo].Copy(CmdLine);

            if (DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.LINE ||
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.MOVE)
            {
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].X[0] = StartPt.X;
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Y[0] = StartPt.Y;
            }
            if (DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Cmd == DispProg.ECmd.ARC)
            {
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].X[1] = StartPt.X;
                DispProg.Script[ProgNo].CmdList.Line[LineNo - 1].Y[1] = StartPt.Y;
            }

            //frm_DispProg2.Done = true;
            Log.OnAction("OK", CmdName);
            Close();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //frm_DispProg2.Done = true;
            Log.OnAction("Cancel", CmdName);
            Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void frm_DispCore_DispProg_SmCircle_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_DispProg2.Done = true;
        }
    }
}
