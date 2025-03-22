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
    internal partial class frm_DispCore_DispProg_FillPat : Form
    {
        public static DispProg.TLine CmdLine = new DispProg.TLine();
        public int ProgNo = 0;
        public int LineNo = 0;
        public TPos2 SubOrigin = new TPos2(0, 0);
        DispProg.TFillPat FillPat = new DispProg.TFillPat(CmdLine);

        public frm_DispCore_DispProg_FillPat()
        {
            InitializeComponent();
            GControl.LogForm(this);

            TopLevel = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void UpdateDisplay()
        {
            AppLanguage.Func2.UpdateText(this);

            if (CmdLine.ID > Enum.GetNames(typeof(EHeadNo)).Length) CmdLine.ID = 1;

            lbl_HeadNo.Text = CmdLine.ID.ToString();
            lbl_ModelNo.Text = CmdLine.IPara[0].ToString();
            lbl_Dispense.Text = (CmdLine.IPara[2] > 0).ToString();

            lbl_CavityStartX.Text = CmdLine.X[2].ToString("F3");
            lbl_CavityStartY.Text = CmdLine.Y[2].ToString("F3");
            lbl_CavitySizeX.Text = CmdLine.X[3].ToString("F3");
            lbl_CavitySizeY.Text = CmdLine.Y[3].ToString("F3");

            lbl_FillPat.Text = FillPat.Type.ToString();

            lbl_StartX.Text = FillPat.Start.X.ToString("F3");
            lbl_StartY.Text = FillPat.Start.Y.ToString("F3");
            lbl_SizeX.Text = FillPat.Size.X.ToString("F3");
            lbl_SizeY.Text = FillPat.Size.Y.ToString("F3");

            lbl_CenterX.Text = FillPat.Center.X.ToString("f3");
            lbl_CenterY.Text = FillPat.Center.Y.ToString("f3");
            lbl_Section.Text = FillPat.Section.ToString("f0");
            lbl_RatioX.Text = (FillPat.RatioX * 100).ToString("f1");
            lbl_RatioY.Text = (FillPat.RatioY * 100).ToString("f1");
            lbl_PitchX.Text = FillPat.Pitch.X.ToString("f3");
            lbl_PitchY.Text = FillPat.Pitch.Y.ToString("f3");

            lbl_Angle.Text = FillPat.Angle.ToString("f3");

            cbox_Compensate.Checked = CmdLine.IPara[3] > 0;

            pbox_.Refresh();
        }

        private string CmdName
        {
            get
            {
                return LineNo.ToString("d3") + " " + CmdLine.Cmd;
            }
        }

        private void frmDispProg_Arc_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            
            CmdLine.Copy(DispProg.Script[ProgNo].CmdList.Line[LineNo]);
            FillPat = new DispProg.TFillPat(CmdLine);

            if (FillPat.Section == 0) FillPat.Section = 2;

            this.Text = CmdName;

            //copy topmost cavity
            for (int i = LineNo; i < DispProg.Script[0].CmdList.Line.Count(); i--)
            {
                if (DispProg.Script[0].CmdList.Line[i].Cmd != DispProg.ECmd.FILL_PAT) break;

                CmdLine.X[2] = DispProg.Script[0].CmdList.Line[i].X[2];
                CmdLine.Y[2] = DispProg.Script[0].CmdList.Line[i].Y[2];
                CmdLine.X[3] = DispProg.Script[0].CmdList.Line[i].X[3];
                CmdLine.Y[3] = DispProg.Script[0].CmdList.Line[i].Y[3];
            }

            try
            {
                TaskDisp.TaskMoveGZFocus(CmdLine.IPara[21]);
            }
            catch { };

            UpdateDisplay();
        }
        private void frmDispProg_Arc_Activated(object sender, EventArgs e)
        {
        }
        private void frmDispProg_Arc_Shown(object sender, EventArgs e)
        {
        }
        private void frm_DispCore_DispProg_Arc_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_DispProg2.Done = true;
        }

        private void lbl_HeadNo_Click(object sender, EventArgs e)
        {
            int i = Enum.GetNames(typeof(EHeadNo)).Length;
            UC.AdjustExec(CmdName + ", HeadNo", ref CmdLine.ID, 1, i);
            UpdateDisplay();
        }
        private void lbl_ModelNo_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", ModelNo", ref CmdLine.IPara[0], 0, TModelList.MAX_MODEL);
            UpdateDisplay();
        }
        private void lbl_Dispense_Click(object sender, EventArgs e)
        {
            if (CmdLine.IPara[2] > 0) CmdLine.IPara[2] = 0; else CmdLine.IPara[2] = 1;
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
        }
        private void lbl_FillPat_Click(object sender, EventArgs e)
        {
            int i = (int)FillPat.Type;
            UC.AdjustExec(CmdName + ", Type", ref i, FillPat.Type);
            FillPat.Type = (DispProg.EFillPatType)i;
            UpdateDisplay();
        }

        private void btn_GotoStartPos_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + FillPat.Start.X;
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + FillPat.Start.Y;

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lbl_StartX_Click(object sender, EventArgs e)
        {
            //UC.AdjustExec(CmdName + ", Start X", ref FillPat.Start.X, -1000, 1000);
            //UpdateDisplay();

            double X = FillPat.Start.X;
            double OldX = X;
            UC.AdjustExec(CmdName + ", Start X", ref X, -1000, 1000);
            FillPat.Start.X = X;

            if (b_LockCenter)
            {
                double AddSizeX = (OldX - X) * 2;
                FillPat.Size.X = FillPat.Size.X + AddSizeX;
            }
            UpdateDisplay();
        }
        private void lbl_StartY_Click(object sender, EventArgs e)
        {
            //UC.AdjustExec(CmdName + ", Start Y", ref FillPat.Start.Y, -1000, 1000);
            //UpdateDisplay();

            double Y = FillPat.Start.Y;
            double OldY = Y;
            UC.AdjustExec(CmdName + ", Start Y", ref Y, -1000, 1000);
            FillPat.Start.Y = Y;

            if (b_LockCenter)
            {
                double AddSizeY = (OldY - Y) * 2;
                FillPat.Size.Y = FillPat.Size.Y + AddSizeY;
            }
            UpdateDisplay();
        }
        private void btn_SetStartPos_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(FillPat.Start.X, FillPat.Start.Y);
            FillPat.Start.X = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            FillPat.Start.Y = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            NSW.Net.Point2D New = new NSW.Net.Point2D(FillPat.Start.X, FillPat.Start.Y);
            Log.OnSet(CmdName + ", Start XY", Old, New);

            UpdateDisplay();
        }

        private void btn_GotoSizePos_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + FillPat.Start.X;
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + FillPat.Start.Y;

            X += FillPat.Size.X;
            Y += FillPat.Size.Y;

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lbl_SizeX_Click(object sender, EventArgs e)
        {
            double X = FillPat.Size.X;
            double OldX = X;
            UC.AdjustExec(CmdName + ", Size X", ref X, 0, 100);
            FillPat.Size.X = X;

            double Y = FillPat.Size.Y;
            double OldY = Y;
            if (b_LockRatio)
            {
                Y = Y * (X / OldX);
                Log.OnAction("Auto", CmdName + ", Size Y", OldY, Y);
                FillPat.Size.Y = Y;
            }

            if (b_LockCenter)
            {
                double OfstX = (OldX - X)/2;
                double OfstY = (OldY - Y)/2;

                FillPat.Start.X = FillPat.Start.X + OfstX;
                FillPat.Start.Y = FillPat.Start.Y + OfstY;
            }

            FillPat.Size = FillPat.Size;
            UpdateDisplay();
        }
        private void lbl_SizeY_Click(object sender, EventArgs e)
        {
            double Y = FillPat.Size.Y;
            double OldY = Y;
            UC.AdjustExec(CmdName + ", Size Y", ref Y, 0, 100);
            FillPat.Size.Y = Y;

            double X = FillPat.Size.X;
            double OldX = X;
            if (b_LockRatio)
            {
                X = FillPat.Size.X *(Y / OldY);
                Log.OnAction("Auto", CmdName + ", Size X", OldX, X);
                FillPat.Size.X = X;
            }

            if (b_LockCenter)
            {
                double OfstX = (OldX - X) / 2;
                double OfstY = (OldY - Y) / 2;

                FillPat.Start.X = FillPat.Start.X + OfstX;
                FillPat.Start.Y = FillPat.Start.Y + OfstY;
            }

            FillPat.Size = FillPat.Size;
            UpdateDisplay();
        }
        private void btn_SetSizePos_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(FillPat.Size.X, FillPat.Size.Y);
            double XP = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            double YP = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            FillPat.Size.X = XP - FillPat.Start.X;
            FillPat.Size.Y = YP - FillPat.Start.Y;
            NSW.Net.Point2D New = new NSW.Net.Point2D(XP, YP);
            Log.OnSet(CmdName + ", Size XY", Old, New);

            UpdateDisplay();
        }

        private void btn_GotoCenterPos_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + FillPat.Center.X;
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + FillPat.Center.Y;

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lbl_CenterX_Click(object sender, EventArgs e)
        {
            double d = FillPat.Center.X;
            UC.AdjustExec(CmdName + ", Center X", ref d, 0, 100);
            FillPat.Center = new TPos2(d, FillPat.Center.Y);

            UpdateDisplay();
        }

        private void lbl_CenterY_Click(object sender, EventArgs e)
        {
            double d = FillPat.Center.Y;
            UC.AdjustExec(CmdName + ", Center Y", ref d, 0, 100);
            FillPat.Center = new TPos2(FillPat.Center.X, d);

            UpdateDisplay();
        }
        private void btn_SetCenterPos_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(FillPat.Center.X, FillPat.Center.Y);
            double CX = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            double CY = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            NSW.Net.Point2D New = new NSW.Net.Point2D(CX, CY);
            FillPat.Center = new TPos2(CX, CY);
            Log.OnSet(CmdName + ", Center XY", Old, New);

            UpdateDisplay();
        }

        private void lbl_SectionX_Click(object sender, EventArgs e)
        {
            int X = FillPat.Section;
            UC.AdjustExec(CmdName + ", Section", ref X, 2, 100);
            FillPat.Section = X;
            UpdateDisplay();
        }
        private void lbl_RatioX_Click(object sender, EventArgs e)
        {
            double d = FillPat.RatioX * 100;
            UC.AdjustExec(CmdName + ", Ratio X", ref d, 1, 100);
            FillPat.RatioX = d /100;
            UpdateDisplay();
        }
        private void lbl_RatioY_Click(object sender, EventArgs e)
        {
            double d = FillPat.RatioY * 100;
            UC.AdjustExec(CmdName + ", Ratio Y", ref d, 1, 100);
            FillPat.RatioY = d / 100;
            UpdateDisplay();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            FillPat.Update(ref CmdLine);
            DispProg.Script[ProgNo].CmdList.Line[LineNo].Copy(CmdLine);

            for (int i = LineNo; i < DispProg.Script[0].CmdList.Line.Count(); i--)
            {
                if (DispProg.Script[0].CmdList.Line[i].Cmd != DispProg.ECmd.FILL_PAT) break;

                DispProg.Script[0].CmdList.Line[i].X[2] = CmdLine.X[2];
                DispProg.Script[0].CmdList.Line[i].Y[2] = CmdLine.Y[2];
                DispProg.Script[0].CmdList.Line[i].X[3] = CmdLine.X[3];
                DispProg.Script[0].CmdList.Line[i].Y[3] = CmdLine.Y[3];
            }

            Log.OnAction("OK", CmdName); 
            Close();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Log.OnAction("Cancel", CmdName); 
            Close();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //iterate and draw previous patterns
            List<DispProg.TLine> CmdLineList = new List<DispProg.TLine>();//to draw
            try
            {
                for (int i = 0; i < DispProg.Script[0].CmdList.Line.Count(); i++)
                {
                    DispProg.TLine Line = DispProg.Script[0].CmdList.Line[i];
                    if (i != LineNo && Line.Cmd == DispProg.ECmd.FILL_PAT)
                    {
                        CmdLineList.Add(Line);
                    }
                }

                SolidBrush SBrush = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(SBrush, new Rectangle(0, 0, pbox_.Width - 1, pbox_.Height - 1));

                foreach (DispProg.TLine CmdLine in CmdLineList)
                {
                    double Border = 10;
                    double d_Ratio = Math.Min((pbox_.Width - (Border * 2)) / CmdLine.X[3], (pbox_.Height - (Border * 2)) / CmdLine.Y[3]);


                    SBrush = new SolidBrush(this.BackColor);
                    Pen Pen = new Pen(Color.Maroon);

                    double CX = CmdLine.X[2] * d_Ratio;
                    double CY = CmdLine.Y[2] * d_Ratio;
                    double CX2 = CmdLine.X[3] * d_Ratio;
                    double CY2 = CmdLine.Y[3] * d_Ratio;

                    int OfstX = (int)((pbox_.Width - CX2) / 2);
                    int OfstY = (int)((pbox_.Height - CY2) / 2);

                    //e.Graphics.DrawRectangle(Pen, OfstX, OfstY, (float)CX2, (float)CY2);

                    //Pen.Color = Color.LightGreen;

                    DispProg.TFillPat FillPat = new DispProg.TFillPat(CmdLine);
                    
                    List<TPos2> Pos = FillPat.AbsPos;

                    switch (FillPat.Type)
                    {
                        default:
                            for (int i = 0; i < Pos.Count - 1; i++)
                            {
                                double X = Pos[i].X * d_Ratio;
                                double Y = Pos[i].Y * d_Ratio;
                                double X2 = Pos[i + 1].X * d_Ratio;
                                double Y2 = Pos[i + 1].Y * d_Ratio;
                                X -= CX;
                                Y -= CY;
                                X2 -= CX;
                                Y2 -= CY;

                                e.Graphics.DrawLine(Pen, (float)(OfstX + X), (float)(OfstY + Y), (float)(OfstX + X2), (float)(OfstY + Y2));
                            }
                            break;
                        case DispProg.EFillPatType.SpiralCurve:
                        case DispProg.EFillPatType.SpiralCurve3:
                            for (int i = 0; i < Pos.Count - 1; i++)
                            {
                                if (i % 2 == 1) continue;

                                double X = Pos[i].X * d_Ratio;
                                double Y = Pos[i].Y * d_Ratio;
                                double X2 = Pos[i + 1].X * d_Ratio;
                                double Y2 = Pos[i + 1].Y * d_Ratio;
                                double X3 = Pos[i + 2].X * d_Ratio;
                                double Y3 = Pos[i + 2].Y * d_Ratio;
                                X -= CX;
                                Y -= CY;
                                X2 -= CX;
                                Y2 -= CY;
                                X3 -= CX;
                                Y3 -= CY;
                                X += OfstX;
                                Y += OfstY;
                                X2 += OfstX;
                                Y2 += OfstY;
                                X3 += OfstX;
                                Y3 += OfstY;
                                PointF[] Points = new PointF[3] { new PointF((float)X, (float)Y), new PointF((float)X2, (float)Y2), new PointF((float)X3, (float)Y3) };

                                e.Graphics.DrawCurve(Pen, Points);
                            }
                            break;
                        case DispProg.EFillPatType.SpiralCurve2:
                            for (int i = 0; i < Pos.Count - 1; i++)
                            {
                                if (i % 9 >= 8)
                                {
                                    double X = Pos[i].X * d_Ratio;
                                    double Y = Pos[i].Y * d_Ratio;
                                    double X2 = Pos[i + 1].X * d_Ratio;
                                    double Y2 = Pos[i + 1].Y * d_Ratio;
                                    X -= CX;
                                    Y -= CY;
                                    X2 -= CX;
                                    Y2 -= CY;

                                    e.Graphics.DrawLine(Pen, (float)(OfstX + X), (float)(OfstY + Y), (float)(OfstX + X2), (float)(OfstY + Y2));
                                }
                                else
                                {
                                    int Loop = i / 9;
                                    if (Loop % 2 == 0)
                                    {
                                        if (i % 2 == 1) continue;
                                    }
                                    else
                                        if (i % 2 == 0) continue;

                                    double X = Pos[i].X * d_Ratio;
                                    double Y = Pos[i].Y * d_Ratio;
                                    double X2 = Pos[i + 1].X * d_Ratio;
                                    double Y2 = Pos[i + 1].Y * d_Ratio;
                                    double X3 = Pos[i + 2].X * d_Ratio;
                                    double Y3 = Pos[i + 2].Y * d_Ratio;
                                    X -= CX;
                                    Y -= CY;
                                    X2 -= CX;
                                    Y2 -= CY;
                                    X3 -= CX;
                                    Y3 -= CY;
                                    X += OfstX;
                                    Y += OfstY;
                                    X2 += OfstX;
                                    Y2 += OfstY;
                                    X3 += OfstX;
                                    Y3 += OfstY;
                                    PointF[] Points = new PointF[3] { new PointF((float)X, (float)Y), new PointF((float)X2, (float)Y2), new PointF((float)X3, (float)Y3) };

                                    e.Graphics.DrawCurve(Pen, Points);
                                }
                            }
                            break;
                    }
                }
            }
            catch//(Exception Ex)
            {
            };

            //draw current pattern
            try
            {
                double Border = 10;
                double d_Ratio = Math.Min((pbox_.Width - (Border * 2)) / CmdLine.X[3], (pbox_.Height - (Border * 2)) / CmdLine.Y[3]);

                SolidBrush SBrush = new SolidBrush(this.BackColor);

                SBrush = new SolidBrush(this.BackColor);
                Pen Pen = new Pen(Color.Navy);

                double CX = CmdLine.X[2] * d_Ratio;
                double CY = CmdLine.Y[2] * d_Ratio;
                double CX2 = CmdLine.X[3] * d_Ratio;
                double CY2 = CmdLine.Y[3] * d_Ratio;

                int OfstX = (int)((pbox_.Width - CX2) / 2);
                int OfstY = (int)((pbox_.Height - CY2) / 2);

                e.Graphics.DrawRectangle(Pen, OfstX, OfstY, (float)CX2, (float)CY2);

                //Pen.Color = Color.DarkGreen;

                List<TPos2> Pos = FillPat.AbsPos;

                switch (FillPat.Type)
                {
                    default:
                        for (int i = 0; i < Pos.Count - 1; i++)
                        {
                            double X = Pos[i].X * d_Ratio;
                            double Y = Pos[i].Y * d_Ratio;
                            double X2 = Pos[i + 1].X * d_Ratio;
                            double Y2 = Pos[i + 1].Y * d_Ratio;
                            X -= CX;
                            Y -= CY;
                            X2 -= CX;
                            Y2 -= CY;

                            e.Graphics.DrawLine(Pen, (float)(OfstX + X), (float)(OfstY + Y), (float)(OfstX + X2), (float)(OfstY + Y2));
                        }
                        break;
                    case DispProg.EFillPatType.SpiralCurve:
                    case DispProg.EFillPatType.SpiralCurve3:
                        for (int i = 0; i < Pos.Count - 1; i++)
                        {
                            if (i % 2 == 1) continue;

                            double X = Pos[i].X * d_Ratio;
                            double Y = Pos[i].Y * d_Ratio;
                            double X2 = Pos[i + 1].X * d_Ratio;
                            double Y2 = Pos[i + 1].Y * d_Ratio;
                            double X3 = Pos[i + 2].X * d_Ratio;
                            double Y3 = Pos[i + 2].Y * d_Ratio;
                            X -= CX;
                            Y -= CY;
                            X2 -= CX;
                            Y2 -= CY;
                            X3 -= CX;
                            Y3 -= CY;
                            X += OfstX;
                            Y += OfstY;
                            X2 += OfstX;
                            Y2 += OfstY;
                            X3 += OfstX;
                            Y3 += OfstY;
                            PointF[] Points = new PointF[3] { new PointF((float)X, (float)Y), new PointF((float)X2, (float)Y2), new PointF((float)X3, (float)Y3) };

                            e.Graphics.DrawCurve(Pen, Points);
                        }
                        break;
                    case DispProg.EFillPatType.SpiralCurve2:
                        for (int i = 0; i < Pos.Count - 1; i++)
                        {
                            if (i % 9 >= 8)
                            {
                                double X = Pos[i].X * d_Ratio;
                                double Y = Pos[i].Y * d_Ratio;
                                double X2 = Pos[i + 1].X * d_Ratio;
                                double Y2 = Pos[i + 1].Y * d_Ratio;
                                X -= CX;
                                Y -= CY;
                                X2 -= CX;
                                Y2 -= CY;

                                e.Graphics.DrawLine(Pen, (float)(OfstX + X), (float)(OfstY + Y), (float)(OfstX + X2), (float)(OfstY + Y2));
                            }
                            else
                                {
                                    int Loop = i / 9;
                                    if (Loop % 2 == 0)
                                    {
                                        if (i % 2 == 1) continue;
                                    }
                                    else
                                        if (i % 2 == 0) continue;

                                    double X = Pos[i].X * d_Ratio;
                                    double Y = Pos[i].Y * d_Ratio;
                                    double X2 = Pos[i + 1].X * d_Ratio;
                                    double Y2 = Pos[i + 1].Y * d_Ratio;
                                    double X3 = Pos[i + 2].X * d_Ratio;
                                    double Y3 = Pos[i + 2].Y * d_Ratio;
                                    X -= CX;
                                    Y -= CY;
                                    X2 -= CX;
                                    Y2 -= CY;
                                    X3 -= CX;
                                    Y3 -= CY;
                                    X += OfstX;
                                    Y += OfstY;
                                    X2 += OfstX;
                                    Y2 += OfstY;
                                    X3 += OfstX;
                                    Y3 += OfstY;
                                    PointF[] Points = new PointF[3] { new PointF((float)X, (float)Y), new PointF((float)X2, (float)Y2), new PointF((float)X3, (float)Y3) };

                                    e.Graphics.DrawCurve(Pen, Points);
                                }
                        }
                        break;
                }
            }
            catch//(Exception Ex)
            {
            };
        }

        private void btn_CavityStartXY_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[2];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[2];

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        }
        private void lbl_CavityStartX_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Cavity Start X", ref CmdLine.X[2], -1000, 1000);
            UpdateDisplay();
        }
        private void lbl_CavityStartY_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Cavity Start Y", ref CmdLine.Y[2], -1000, 1000);
            UpdateDisplay();
        }
        private void btn_SetCavityStartXY_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[2], CmdLine.Y[2]);
            CmdLine.X[2] = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            CmdLine.Y[2] = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            NSW.Net.Point2D New = new NSW.Net.Point2D(CmdLine.X[2], CmdLine.Y[2]);
            Log.OnSet(CmdName + ", Cavity Start XY", Old, New);

            UpdateDisplay();
        }

        private void btn_CavitySizeXY_Click(object sender, EventArgs e)
        {
            double X = (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X) + CmdLine.X[2];
            double Y = (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y) + CmdLine.Y[2];

            X += CmdLine.X[3];
            Y += CmdLine.Y[3];

            if (!TaskDisp.TaskMoveGZZ2Up()) return;

            if (!TaskGantry.SetMotionParamGXY()) return;
            if (!TaskGantry.MoveAbsGXY(X, Y)) return;
        } 
        private void lbl_CavitySizeX_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Size X", ref CmdLine.X[3], 0, 100);
            UpdateDisplay();
        }
        private void lbl_CavitySizeY_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Size Y", ref CmdLine.Y[3], 0, 100);
            UpdateDisplay();
        }
        private void btn_SetCavitySizeXY_Click(object sender, EventArgs e)
        {
            double X = TaskGantry.GXPos();
            double Y = TaskGantry.GYPos();

            NSW.Net.Point2D Old = new NSW.Net.Point2D(CmdLine.X[3], CmdLine.Y[3]);
            double XP = X - (DispProg.Origin(DispProg.rt_StationNo).X + SubOrigin.X);
            double YP = Y - (DispProg.Origin(DispProg.rt_StationNo).Y + SubOrigin.Y);
            CmdLine.X[3] = XP - CmdLine.X[2];
            CmdLine.Y[3] = YP - CmdLine.Y[2];
            NSW.Net.Point2D New = new NSW.Net.Point2D(XP, YP);
            Log.OnSet(CmdName + ", Cavity Size XY", Old, New);

            UpdateDisplay();
        }

        private void btn_CenterToCavity_Click(object sender, EventArgs e)
        {
            NSW.Net.Point2D Old = new NSW.Net.Point2D(FillPat.Center.X, FillPat.Center.Y);
            double X = CmdLine.X[2] + (CmdLine.X[3] / 2);
            double Y = CmdLine.Y[2] + (CmdLine.Y[3] / 2);
            NSW.Net.Point2D New = new NSW.Net.Point2D(X, Y);
            FillPat.Center = new TPos2(X, Y);
            Log.OnSet(CmdName + ", Set Center to Cavity ", Old, New);

            UpdateDisplay();
        }

        bool b_LockRatio = false;
        private void cbox_LockRatio_Click(object sender, EventArgs e)
        {
            b_LockRatio = cbox_LockRatio.Checked;
        }
        bool b_LockCenter = false;
        private void cbox_LockCenter_Click(object sender, EventArgs e)
        {
            b_LockCenter = cbox_LockCenter.Checked;
        }

        private void lbl_Angle_Click(object sender, EventArgs e)
        {
            double d = FillPat.Angle;
            if (UC.AdjustExec(CmdName + ", Angle", ref d, -180, 180))
                FillPat.Angle = d;
            UpdateDisplay();
        }

        private void cbox_Compensate_Click(object sender, EventArgs e)
        {
            int Old = CmdLine.IPara[3];
            if (Old == 0) CmdLine.IPara[3] = 1; else CmdLine.IPara[3] = 0;
            int New = CmdLine.IPara[3];
            FillPat.Comp = CmdLine.IPara[3] > 0;
            Log.OnSet(CmdName + ", Compensate", Old, New);

            UpdateDisplay();
        }
    }
}
