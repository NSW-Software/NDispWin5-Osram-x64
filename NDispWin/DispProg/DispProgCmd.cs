using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;

namespace NDispWin
{
    using NSW.Net;
    using SpinnakerNET;

    class DispProgCmd
    {
    }

    internal class GroupDisp
    {
        public static bool Execute(DispProg.TLine Line, ERunMode RunMode, double f_origin_x, double f_origin_y, double f_origin_z)
        {
           // if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2) throw new Exception("Group Disp do not support Dual Head Config.");

            string EMsg = Line.Cmd.ToString();

            try
            {
                GDefine.Status = EStatus.Busy;

                bool b_Head2IsValid = false;
                bool b_SyncHead2 = false;
                bool[] bHeadRun = new bool[2] { false, false };
                if (!DispProg.SelectHead(Line, ref bHeadRun, ref b_Head2IsValid, ref b_SyncHead2)) goto _End;

                switch (RunMode)
                {
                    case ERunMode.Normal:
                        {
                            if (GDefine.DispCtrlType[0] == GDefine.EDispCtrlType.HPC3)
                            {
                                bool[] pumpToFill = TFPump.PP4.CheckStrokeToFill(bHeadRun);

                                if (pumpToFill[0] || pumpToFill[1])
                                {
                                    if (!TaskDisp.TaskGotoTPos2(TaskDisp.Needle_Clean_Pos)) goto _Error;
                                    if (!TFPump.PP4.PFill(pumpToFill)) goto _Error;

                                    if (!TaskDisp.TaskCleanNeedle(bHeadRun[0], bHeadRun[1], RunMode == ERunMode.Normal)) goto _Error;
                                    if (!TaskDisp.TaskPurgeNeedle(bHeadRun[0], bHeadRun[1], RunMode == ERunMode.Normal)) goto _Error;
                                }
                            }
                            else
                            if (GDefine.DispCtrlType[0] != GDefine.EDispCtrlType.HPC3) throw new Exception("Only Disp Controller type HPC3 is supported.");
                            if (DispProg.Pump_Type != TaskDisp.EPumpType.PP) throw new Exception("Only Pump Type PP is supported.");
                            break;
                        }
                    case ERunMode.Dry:
                    case ERunMode.Camera:
                        {
                            break;
                        }
                }

                TModelPara Model = new TModelPara(DispProg.ModelList, Line.IPara[0]);
                bool Disp = (Line.IPara[2] > 0);

                #region Move GZ2 Up if invalid
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2 && !b_Head2IsValid)
                {
                    switch (RunMode)
                    {
                        case ERunMode.Normal:
                        case ERunMode.Dry:
                            if (!TaskDisp.TaskMoveGZ2Up()) return false;
                            break;
                    }
                }
                #endregion

                #region assign and translate position
                double dx = f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].X + Line.X[0];
                double dy = f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].Y + Line.Y[0];
                DispProg.TranslatePos(dx, dy, DispProg.rt_Head1RefData, ref dx, ref dy);

                double dx2 = f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex2].X + Line.X[0];
                double dy2 = f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex2].Y + Line.Y[0];
                DispProg.TranslatePos(dx2, dy2, DispProg.rt_Head2RefData, ref dx2, ref dy2);

                dx = dx + DispProg.BiasKernel.X[DispProg.RunTime.Bias_Head_CR[0].X, DispProg.RunTime.Bias_Head_CR[0].Y];
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XZ_YTABLE)
                    dy = dy - DispProg.BiasKernel.Y[DispProg.RunTime.Bias_Head_CR[0].X, DispProg.RunTime.Bias_Head_CR[0].Y];
                else
                    dy = dy + DispProg.BiasKernel.Y[DispProg.RunTime.Bias_Head_CR[0].X, DispProg.RunTime.Bias_Head_CR[0].Y];

                double X1 = dx;
                double Y1 = dy;
                double X2 = dx2;
                double Y2 = dy2;
                #endregion

                X1 = X1 + DispProg.OriginDrawOfst.X;
                Y1 = Y1 + DispProg.OriginDrawOfst.Y;
                X2 = X2 + DispProg.OriginDrawOfst.X;
                Y2 = Y2 + DispProg.OriginDrawOfst.Y;

                double X2_Ofst = X2 - X1;
                double Y2_Ofst = Y2 - Y1;

                TPos2 GXY = new TPos2(X1, Y1);
                TPos2 GX2Y2 = new TPos2(TaskDisp.Head2_DefPos.X, TaskDisp.Head2_DefPos.Y);
                #region Move To Pos
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {
                            if (!b_SyncHead2)
                            {
                                if (bHeadRun[0])//(HeadNo == EHeadNo.Head1)
                                {
                                    GXY.X = GXY.X + TaskDisp.Head_Ofst[0].X;
                                    GXY.Y = GXY.Y + TaskDisp.Head_Ofst[0].Y;
                                }
                                if (bHeadRun[1])//(HeadNo == EHeadNo.Head2)
                                {
                                    GXY.X = GXY.X + TaskDisp.Head_Ofst[1].X;
                                    GXY.Y = GXY.Y + TaskDisp.Head_Ofst[1].Y;
                                }
                            }
                            else
                            {
                                GXY.X = GXY.X + TaskDisp.Head_Ofst[0].X;
                                GXY.Y = GXY.Y + TaskDisp.Head_Ofst[0].Y;

                                GX2Y2.X = GX2Y2.X - TaskDisp.Head2_DefDistX + X2_Ofst + TaskDisp.Head2_XOffset;
                                GX2Y2.Y = GX2Y2.Y + Y2_Ofst + TaskDisp.Head2_YOffset;
                            }
                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }

                if (!TaskGantry.SetMotionParamGXY()) goto _Error;
                if (!TaskGantry.MoveAbsGXY(GXY.X, GXY.Y, false)) goto _Error;
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2)
                {
                    if (bHeadRun[1])
                    {
                        if (!TaskGantry.SetMotionParamGX2Y2()) goto _Error;
                        if (!TaskGantry.MoveAbsGX2Y2(GX2Y2.X, GX2Y2.Y, false)) goto _Error;
                    }
                }
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2)
                    TaskGantry.WaitGX2Y2();
                TaskGantry.WaitGXY();
                #endregion

                double Z1 = 0;
                double Z2 = 0;
                #region Assign Z positions
                double dz = f_origin_z;
                dz = dz + TaskDisp.Head_Ofst[0].Z;
                double ZDiff = (TaskDisp.Head_ZSensor_RefPosZ[1] + TaskDisp.Head_Ofst[1].Z - (TaskDisp.Head_ZSensor_RefPosZ[0] + TaskDisp.Head_Ofst[0].Z));
                double dz2 = dz + ZDiff;

                Z1 = dz;
                Z2 = dz2;
                #endregion
                #region Update Z Offset
                Z1 = Z1 + TaskDisp.Z1Offset;
                Z2 = Z2 + TaskDisp.Z2Offset + TaskDisp.Head2_ZOffset;
                #endregion

                #region If ZPlane Valid, Update Z Values
                double LX1 = GXY.X - TaskDisp.Head_Ofst[0].X;
                double LY1 = GXY.Y - TaskDisp.Head_Ofst[0].Y;
                double LX2 = LX1 + (X2 - X1);
                double LY2 = LY1 + (Y2 - Y1);
                DispProg.UpdateZHeight(b_SyncHead2, LX1, LY1, LX2, LY2, ref Z1, ref Z2);
                #endregion
                #region Move z to DispGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {

                            double sv = Model.DnStartV;
                            double dv = Model.DnSpeed;
                            double ac = Model.DnAccel;
                            if (!TaskGantry.SetMotionParamGZZ2(sv, dv, ac)) goto _Stop;
                            if (!DispProg.MoveZAbs(bHeadRun[0], bHeadRun[1], Z1 + Model.DispGap + Model.RetGap, Z2 + Model.DispGap + Model.RetGap)) return false;


                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }
                #endregion

                #region Weighted - HPC3 Precalculation of dots volumne and line lenght.
                double ttlWeight = Line.DPara[1];
                double vol = ttlWeight / TaskWeight.CurrentCal[0];

                double singleDotVol = 0;//Single dot volume
                double ttlLength = 0;//Total Line Length
                double lineDispSpeed = 0;//Line disp speed

                double ttlppDispPulse = 0;//Total ppPulse incl Start Delay and Lines
                double ttlppLineDispPulse = 0;//Total ppPulse for Lines

                if (ttlWeight > 0)//enabled Weighted
                {
                    if (TaskWeight.CurrentCal[0] <= 0) throw new Exception("Invalid Flowrate. Perform Flowrate cal.");
                    if (ttlWeight <= 0) throw new Exception("Weight value is invalid. Define weight value.");

                    //Calculate dot volumes total length of group lines
                    ttlLength = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        bool breakFor = false;
                        switch (Line.Index[i])
                        {
                            case (int)EGDispCmd.None:
                                breakFor = true;
                                break;
                            case (int)EGDispCmd.DOT:
                                singleDotVol = vol / (i + 1);
                                breakFor = true;
                                break;
                            case (int)EGDispCmd.LINE_START:
                                break;
                            case (int)EGDispCmd.LINE_PASS:
                                {
                                    double lineLength = Math.Sqrt(Math.Pow(Line.X[i], 2) + Math.Pow(Line.Y[i], 2));
                                    ttlLength += lineLength;
                                }
                                break;
                            case (int)EGDispCmd.LINE_END:
                                {
                                    double lineLength = Math.Sqrt(Math.Pow(Line.X[i], 2) + Math.Pow(Line.Y[i], 2));
                                    ttlLength += lineLength;
                                }
                                break;
                        }
                        if (breakFor) break;
                    }
                }
                #endregion

                int t = GDefine.GetTickCount();
                #region Prepare Paths
                CControl2.TAxis[] Axis = new CControl2.TAxis[] { TaskGantry.GXAxis, TaskGantry.GYAxis, TaskGantry.GZAxis, TaskGantry.GZ2Axis, TaskGantry.PAAxis, TaskGantry.PBAxis };
                CommonControl.P1245.PathFree(Axis);
                CommonControl.P1245.SetAccel(Axis, Model.LineAccel);
                bool b_Blend = false;
                #endregion

                double relX = 0; double relY = 0;//***Placeholder for current XY position, used when more than 1 LineStart
                double relGap = Model.RetGap;//***Placeholder for current Z position, used when CutTail
                for (int i = 0; i < 100; i++)
                {
                    bool breakFor = false;
                    switch (Line.Index[i])
                    {
                        case (int)EGDispCmd.None:
                            breakFor = true;
                            break;
                        case (int)EGDispCmd.DOT:
                            {
                                if (i > 0)
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, TaskGantry.GXAxis.Para.FastV, 0, new double[6] { Line.X[i], Line.Y[i], 0, 0, 0, 0 }, null);
                                #region Path Move to Disp Gap
                                switch (RunMode)
                                {
                                    case ERunMode.Normal:
                                    case ERunMode.Dry:
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, Model.DnSpeed, /*Model.DnStartV*/0, new double[6] { 0, 0, -relGap, 0, 0, 0 }, null);
                                        relGap = 0;
                                        break;
                                }
                                if (Model.DnWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.DnWait, 0, null, null);
                                #endregion

                                if (singleDotVol > 0)
                                {
                                    double ppDispPulse = TFPump.PP4.LengthConversion(singleDotVol + DispProg.PP_HeadA_BackSuckVol);
                                    double ppBSuckPulse = TFPump.PP4.LengthConversion(DispProg.PP_HeadA_BackSuckVol);

                                    bool disp = (Line.U[i] > 0);
                                    if (!disp || RunMode == ERunMode.Dry)
                                    {
                                        ppDispPulse = 0;
                                        ppBSuckPulse = 0;
                                    }
                                    if (!bHeadRun[0])
                                    {
                                        ppDispPulse = 0;
                                        ppBSuckPulse = 0;
                                    }
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, -ppDispPulse, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, Model.StartDelay, 0, null, null);//temp
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, 0, 0, ppBSuckPulse, 0 }, null);
                                }
                                if (bHeadRun[0] && RunMode == ERunMode.Normal) DispProg.Stats.DispCount_Inc(0);
                                if (bHeadRun[1] && RunMode == ERunMode.Normal) DispProg.Stats.DispCount_Inc(1);

                                if (Model.PostWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.PostWait, 0, null, null);

                                #region Path Retract and Up
                                switch (RunMode)
                                {
                                    case ERunMode.Normal:
                                    case ERunMode.Dry:
                                        {
                                            if (Model.RetGap > 0)
                                            {
                                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, Model.RetSpeed, 0, new double[6] { 0, 0, Model.RetGap, 0, 0, 0 }, null);
                                                relGap += Model.RetGap;
                                                if (Model.RetWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.RetWait, 0, null, null);
                                            }
                                            if (Line.Index[i] == (int)EGDispCmd.DOT && Model.UpGap > 0)
                                            {
                                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, Model.UpSpeed, 0, new double[6] { 0, 0, Model.UpGap, 0, 0, 0 }, null);
                                                relGap += Model.UpGap;
                                                if (Model.UpWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.UpWait, 0, null, null);
                                            }
                                            break;
                                        }
                                    case ERunMode.Camera:
                                    default:
                                        {
                                            if (Model.RetWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.RetWait, 0, null, null);
                                            if (Line.Index[i] == (int)EGDispCmd.DOT && Model.UpWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.UpWait, 0, null, null);
                                            break;
                                        }
                                }
                                #endregion
                                break;
                            }
                        case (int)EGDispCmd.LINE_START:
                            ttlppDispPulse = TFPump.PP4.LengthConversion(vol + DispProg.PP_HeadA_BackSuckVol);
                            double dispVol = vol + DispProg.PP_HeadA_BackSuckVol;
                            double ttlDispTime = TaskDisp.CalcPPDispTime(dispVol);
                             
                            double startDelayppDispPulse = (((double)Model.StartDelay/1000) / ttlDispTime) * ttlppDispPulse;
                            ttlppLineDispPulse = ttlppDispPulse - startDelayppDispPulse;
 
                            lineDispSpeed = ttlLength / ttlDispTime;//LineSpeed for all lines.
                            
                            if (lineDispSpeed > 100) throw new Exception("Line Speed over 100mm/s. Run Aborted.");

                            if (i > 0)
                            {
                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, TaskGantry.GXAxis.Para.FastV, 0, new double[6] { Line.X[i] - relX, Line.Y[i] - relY, 0, 0, 0, 0 }, null);
                                relX = 0;
                                relY = 0;
                            }

                            switch (RunMode)
                            {
                                case ERunMode.Normal:
                                case ERunMode.Dry:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, Model.DnSpeed, Model.DnStartV, new double[6] { 0, 0, -relGap, 0, 0, 0 }, null);
                                    relGap = 0;
                                    break;
                            }
                            if (Model.DnWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.DnWait, 0, null, null);
                            //Start Delay
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, -startDelayppDispPulse, 0 }, null);


                            break;
                        case (int)EGDispCmd.LINE_PASS:
                            {
                                double lineLength = Math.Sqrt(Math.Pow(Line.X[i], 2) + Math.Pow(Line.Y[i], 2));
                                double ppLineDispPulse = ttlppLineDispPulse * (lineLength / ttlLength);//Calc current line pulse by length ratio

                                if (ttlWeight == 0)
                                {
                                    lineDispSpeed = Model.LineSpeed;
                                    ppLineDispPulse = 0;
                                }

                                bool disp = (Line.U[i] > 0);
                                if (!bHeadRun[0] || !disp || RunMode == ERunMode.Dry || RunMode == ERunMode.Camera)
                                {
                                    ppLineDispPulse = 0;
                                }

                                if (i == 0)
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, lineDispSpeed, 0, new double[6] { Line.X[i], Line.Y[i], 0, 0, -ppLineDispPulse, 0 }, null);
                                else
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, lineDispSpeed, lineDispSpeed, new double[6] { Line.X[i], Line.Y[i], 0, 0, -ppLineDispPulse, 0 }, null);
                                break;
                            }
                        case (int)EGDispCmd.LINE_END:
                            {
                                double ppBSuckPulse = TFPump.PP4.LengthConversion(DispProg.PP_HeadA_BackSuckVol);

                                double lineLength = Math.Sqrt(Math.Pow(Line.X[i], 2) + Math.Pow(Line.Y[i], 2));
                                double ppLineDispPulse = ttlppLineDispPulse * (lineLength / ttlLength);//Calc current line pulse by length ratio

                                if (ttlWeight == 0)
                                {
                                    lineDispSpeed = Model.LineSpeed;
                                    ppLineDispPulse = 0;
                                    ppBSuckPulse = 0;
                                }

                                bool disp = (Line.U[i] > 0);
                                if (!bHeadRun[0] || !disp || RunMode == ERunMode.Dry || RunMode == ERunMode.Camera)
                                {
                                    ppLineDispPulse = 0;
                                    ppBSuckPulse = 0;
                                }

                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, lineDispSpeed, 0, new double[6] { Line.X[i], Line.Y[i], 0, 0, -ppLineDispPulse, 0 }, null);
                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, 0, 0, ppBSuckPulse, 0 }, null);

                                if (bHeadRun[0] && RunMode == ERunMode.Normal) DispProg.Stats.DispCount_Inc(0);
                                if (bHeadRun[1] && RunMode == ERunMode.Normal) DispProg.Stats.DispCount_Inc(1);

                                #region Path CutTail
                                double extRelX = Line.X[i] * Line.DPara[10] / lineLength;
                                double extRelY = Line.Y[i] * Line.DPara[10] / lineLength;
                                double cutTailSpeed = Line.DPara[11];
                                double cutTailSSpeed = Math.Min(Model.LineStartV, cutTailSpeed);
                                double cutTailHeight = Line.DPara[12];
                                ECutTailType cutTailType = ECutTailType.None;
                                try { cutTailType = (ECutTailType)Line.DPara[13]; } catch { };

                                switch (cutTailType)
                                {
                                    case ECutTailType.None:
                                        break;
                                    case ECutTailType.Fwd:
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { extRelX, extRelY, cutTailHeight }, null);
                                        relX = extRelX;
                                        relY = extRelY;
                                        break;
                                    case ECutTailType.Bwd:
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { -extRelX, -extRelY, cutTailHeight }, null);
                                        relX = -extRelX;
                                        relY = -extRelY;
                                        break;
                                    case ECutTailType.SqFwd:
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { 0, 0, cutTailHeight }, null);
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { extRelX, extRelY, 0 }, null);
                                        relX = extRelX;
                                        relY = extRelY;
                                        break;
                                    case ECutTailType.SqBwd:
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { 0, 0, cutTailHeight }, null);
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { -extRelX, -extRelY, 0 }, null);
                                        relX = -extRelX;
                                        relY = -extRelY;
                                        break;
                                    case ECutTailType.Rev:
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { extRelX, extRelY, 0 }, null);
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { -extRelX, -extRelY, cutTailHeight }, null);
                                        break;
                                    case ECutTailType.SqRev:
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { extRelX, extRelY, 0 }, null);
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { 0, 0, cutTailHeight }, null);
                                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DDirect, b_Blend, cutTailSpeed, cutTailSSpeed, new double[3] { -extRelX, -extRelY, 0 }, null);
                                        break;
                                }
                                relGap += cutTailHeight;
                                #endregion
                                #region Path Retract and Up
                                switch (RunMode)
                                {
                                    case ERunMode.Normal:
                                    case ERunMode.Dry:
                                        {
                                            if (Model.RetGap > 0)
                                            {
                                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, Model.RetSpeed, Model.RetStartV, new double[6] { 0, 0, Model.RetGap, 0, 0, 0  }, null);
                                                relGap += Model.RetGap;
                                                if (Model.RetWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.RetWait, 0, null, null);
                                            }
                                            if (Model.UpGap > 0)
                                            {
                                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, b_Blend, Model.UpSpeed, Model.UpStartV, new double[6] { 0, 0, Model.UpGap, 0 ,0,0 }, null);
                                                relGap += Model.UpGap;
                                                if (Model.UpWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.UpWait, 0, null, null);
                                            }
                                            break;
                                        }
                                    case ERunMode.Camera:
                                    default:
                                        {
                                            if (Model.RetWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.RetWait, 0, null, null);
                                            if (Model.UpWait > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, b_Blend, Model.UpWait, 0, null, null);
                                            break;
                                        }
                                }
                                #endregion
                                break;
                            }
                    }
                    if (breakFor) break;
                }

                #region Move Paths
               // CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, TaskGantry.GXAxis.Para.FastV, 0, new double[3] { 0.001, 0.001, 0 }, null);

                uint index = 0, curr = 0, remain = 0;
                CommonControl.P1245.PathInfo(Axis, ref index, ref curr, ref remain);
                if (remain > 0) CommonControl.P1245.PathEnd(Axis);
                CommonControl.P1245.PathMove(Axis);
                while (true)
                {
                    if (!CommonControl.P1245.AxisBusy(Axis)) break;
                }
                if (GDefine.LogLevel == 1) Log.AddToLog("Line Time " + (GDefine.GetTickCount() - t).ToString("f3") + "ms");
                #endregion}
            }
            catch (Exception Ex)
            {
                GDefine.Status = EStatus.ErrorInit;
                TaskDisp.TrigOff(true, true);
                EMsg = EMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(EMsg);
            }
        _End:
            GDefine.Status = EStatus.Ready;
            return true;
        _Stop:
            GDefine.Status = EStatus.Stop;
            return false;
        _Error:
            GDefine.Status = EStatus.ErrorInit;
            return false;
        }
    }

    internal class NetDisp
    {
        public static bool Net_Line(DispProg.TLine Line, ERunMode RunMode, double f_origin_x, double f_origin_y, double f_origin_z)
        {
            GDefine.Status = EStatus.Busy;

            try
            {
                TModelPara Model = new TModelPara(DispProg.ModelList, Line.IPara[0]);
                bool disp = (Line.IPara[2] > 0);

                EVHType vhType = EVHType.Hort;//0=Horizontal, 1=Vertical
                try { vhType = (EVHType)Line.IPara[3]; } catch { };

                ELineType lineType = ELineType.Cont;
                try { lineType = (ELineType)Line.IPara[4]; } catch { };

                double leadLength = Line.DPara[0];
                double lagLength = Line.DPara[0];

                bool[] bHeadRun = new bool[2] { false, false };
                bool bHead2IsValid = false;
                bool bSyncHead2 = false;
                if (!DispProg.SelectHead(Line, ref bHeadRun, ref bHead2IsValid, ref bSyncHead2)) goto _End;

                TLayout layout = new TLayout();
                layout.Copy(DispProg.rt_Layouts[DispProg.rt_LayoutID]);

                Point currentUnitCR = new Point(0, 0);
                layout.UnitNoGetRC(DispProg.RunTime.UIndex, ref currentUnitCR);
                Point currentClusterCR = new Point(0, 0);
                currentClusterCR.X = currentUnitCR.X / layout.UColCount;
                currentClusterCR.Y = currentUnitCR.Y / layout.URowCount;

                if (!DispProg.SetPumpParameters(Model, disp, bHeadRun)) goto _Stop;

                #region Move GZ2 Up if invalid
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2 && !bHead2IsValid)
                {
                    switch (RunMode)
                    {
                        case ERunMode.Normal:
                        case ERunMode.Dry:
                            if (!TaskDisp.TaskMoveGZ2Up()) return false;
                            break;
                    }
                }
                #endregion

                #region assign and xy translate position
                TPos2[] absStart = new TPos2[]
                {
                new TPos2(f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].X + Line.X[0], f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].Y + Line.Y[0]),
                new TPos2(f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex2].X + Line.X[0], f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex2].Y + Line.Y[0])
                };
                DispProg.TranslatePos(absStart[0].X, absStart[0].Y, DispProg.rt_Head1RefData, ref absStart[0].X, ref absStart[0].Y);
                DispProg.TranslatePos(absStart[1].X, absStart[1].Y, DispProg.rt_Head1RefData, ref absStart[1].X, ref absStart[1].Y);
                #endregion

                #region Calculate the End positions
                Point firstCR = new Point(0, 0);
                Point lastCR = new Point(0, 0);
                int lastUnitNo = 0;//Last UnitNo of Hort/Vert line 
                if (vhType == EVHType.Hort)
                {
                    layout.UnitNoGetRC(DispProg.RunTime.UIndex, ref firstCR);
                    lastCR = new Point((currentClusterCR.X * layout.UColCount) + layout.UColCount - 1, firstCR.Y);
                    layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current row.
                }
                else
                {
                    layout.UnitNoGetRC(DispProg.RunTime.UIndex, ref firstCR);
                    lastCR = new Point(firstCR.X, (currentClusterCR.Y * layout.URowCount) + layout.URowCount - 1);
                    layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current col.
                }
                TPos2 absEnd = new TPos2(f_origin_x + DispProg.rt_LayoutRelPos[lastUnitNo].X + Line.X[0], f_origin_y + DispProg.rt_LayoutRelPos[lastUnitNo].Y + Line.Y[0]);
                DispProg.TranslatePos(absEnd.X, absEnd.Y, DispProg.rt_Head1RefData, ref absEnd.X, ref absEnd.Y);
                #endregion

                bool reverse = Line.IPara[5] > 0;
                if (reverse)
                {
                    if ((vhType == EVHType.Hort && firstCR.Y % 2 != 0) || (vhType == EVHType.Vert && firstCR.X % 2 != 0))
                    {
                        TPos2 temp = new TPos2(absStart[0]);
                        absStart[0] = new TPos2(absEnd);
                        absEnd = new TPos2(temp);
                    }
                }

                #region Calculate the start Z positions
                double[] absZ = new double[] { 0, 0 };
                absZ[0] = f_origin_z + TaskDisp.Head_Ofst[0].Z; //Assign Z positions
                absZ[1] = absZ[0] + (TaskDisp.Head_ZSensor_RefPosZ[1] - TaskDisp.Head_ZSensor_RefPosZ[0]); //Update ZPlane if valid Z values
                DispProg.UpdateZHeight(bSyncHead2, absStart[0].X, absStart[0].Y, absStart[1].X, absStart[1].Y, ref absZ[0], ref absZ[1]);
                double[] zRetGapPos = new double[] { Math.Min(absZ[0] + Model.DispGap + Model.RetGap, TaskDisp.ZDefPos), Math.Min(absZ[1] + Model.DispGap + Model.RetGap, TaskDisp.ZDefPos) };

                double[] absEndZ = new double[] { absZ[0], absZ[1] };
                DispProg.UpdateZHeight(bSyncHead2, absEnd.X, absEnd.Y, absEnd.X, absEnd.Y, ref absEndZ[0], ref absEndZ[1]);//Head2 Z follow Head1
                #endregion

                //Calculate the relative line end pos
                TPos2 relLineEndXY = new TPos2(absEnd.X - absStart[0].X, absEnd.Y - absStart[0].Y);
                double relEndZ = absEndZ[0] - absZ[0];

                //Calculate the lead lag relative pos
                double lineLength = Math.Sqrt(Math.Pow(relLineEndXY.X, 2) + Math.Pow(relLineEndXY.Y, 2));
                TPos2 relLeadXY = new TPos2(leadLength / lineLength * relLineEndXY.X, leadLength / lineLength * relLineEndXY.Y);
                TPos2 relLagXY = new TPos2(lagLength / lineLength * relLineEndXY.X, lagLength / lineLength * relLineEndXY.Y);

                //lastAbsXY = new TPos2[] { new TPos2(absXY[0]), new TPos2(absXY[1]) };

                #region Move abs Start Pos + lead length, move head2 to position
                if (!TaskGantry.SetMotionParamGXY()) goto _Error;
                TPos2 GXY = new TPos2(absStart[0].X - relLeadXY.X, absStart[0].Y - relLeadXY.Y);
                if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                {
                    GXY.X += TaskDisp.Head_Ofst[0].X;
                    GXY.Y += TaskDisp.Head_Ofst[0].Y;
                }
                if (!TaskGantry.MoveAbsGXY(GXY.X, GXY.Y, false)) goto _Error;
                TPos2 GX2Y2 = new TPos2(TaskDisp.Head2_DefPos.X, TaskDisp.Head2_DefPos.Y);
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2)
                {
                    if (bHeadRun[1])
                    {
                        GX2Y2.X = GX2Y2.X - TaskDisp.Head2_DefDistX + (absStart[1].X - absStart[0].X) + TaskDisp.Head2_XOffset;
                        GX2Y2.Y = GX2Y2.Y + (absStart[1].Y - absStart[0].Y) + TaskDisp.Head2_YOffset;

                        if (!TaskGantry.SetMotionParamGX2Y2()) goto _Error;
                        if (!TaskGantry.MoveAbsGX2Y2(GX2Y2.X, GX2Y2.Y, false)) goto _Error;
                    }
                    TaskGantry.WaitGX2Y2();
                }
                TaskGantry.WaitGXY();
                #endregion

                //Move to abs RetractGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {
                            if (!TaskGantry.SetMotionParamGZZ2(Model.DnStartV, Model.DnSpeed, Model.DnAccel)) return false;
                            if (!DispProg.MoveZAbs(bHeadRun[0], bHeadRun[1], zRetGapPos[0], zRetGapPos[1])) return false;
                            //return false;
                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }

                double[] GZ = new double[] { Math.Min(absZ[0] + Model.DispGap, TaskDisp.ZDefPos), Math.Min(absZ[1] + Model.DispGap + TaskDisp.Head2_ZOffset, TaskDisp.ZDefPos) };//include H2 ZOffset

                //Move to abs DispGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {

                            double sv = Model.DnStartV;
                            double dv = Model.DnSpeed;
                            double ac = Model.DnAccel;
                            if (!TaskGantry.SetMotionParamGZZ2(sv, dv, ac)) goto _Stop;
                            if (!DispProg.MoveZAbs(bHeadRun[0], bHeadRun[1], GZ[0], GZ[1])) return false;

                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }

                #region Dn Wait
                int t = GDefine.GetTickCount() + (int)Model.DnWait;
                while (GDefine.GetTickCount() < t)
                {
                    if (Model.DnWait > 75) Thread.Sleep(1);
                }
                #endregion

                #region Start Disp and StartDelay
                CControl2.TAxis[] Axis = new CControl2.TAxis[] { TaskGantry.GXAxis, TaskGantry.GYAxis, TaskGantry.GZAxis, TaskGantry.GZ2Axis };
                CommonControl.P1245.PathFree(Axis);
                CControl2.TOutput[] Output = null;
                DispProg.Outputs(bHeadRun, ref Output);
                CommonControl.P1245.PathAddDO(Axis, Output, disp && RunMode == ERunMode.Normal);
                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.StartDelay, 0, null, null);
                #endregion

                double LineSpeed = Model.LineSpeed;
                CommonControl.P1245.SetAccel(Axis, Model.LineAccel);

                if (lineType == ELineType.Cont)
                {
                    if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                    {
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLeadXY.X, relLeadXY.Y, 0, 0 }, null);
                        CommonControl.P1245.PathAddDO(Axis, Output, disp && RunMode == ERunMode.Normal);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLineEndXY.X, relLineEndXY.Y, relEndZ, relEndZ }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLagXY.X, relLagXY.Y, 0, 0 }, null);
                        CommonControl.P1245.PathAddDO(Axis, Output, false);
                    }
                    else
                    {
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLeadXY.X, relLeadXY.Y, 0, 0 }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLineEndXY.X, relLineEndXY.Y, 0, 0 }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLagXY.X, relLagXY.Y, 0, 0 }, null);
                    }
                }
                else//lineType == ELineType.Dash
                {
                    double delayOnDist = Line.DPara[3];
                    double earlyOffDist = Line.DPara[4];
                    double junctionLift = Line.DPara[5];

                    #region Line positions
                    double relXYLength = Math.Sqrt(Math.Pow(relLineEndXY.X, 2) + Math.Pow(relLineEndXY.Y, 2));
                    double unitPitchDist = Math.Sqrt(Math.Pow(layout.URowPX, 2) + Math.Pow(layout.URowPY, 2));

                    //Calc DelayOn rel position
                    TPos2 relDelayOnXY = new TPos2(relLineEndXY.X * delayOnDist / relXYLength, relLineEndXY.Y * delayOnDist / relXYLength);
                    double relDelayOnZ = relEndZ * delayOnDist / relXYLength;

                    //Calc EarlyOff rel position
                    TPos2 relEarlyOffXY = new TPos2(relLineEndXY.X * earlyOffDist / relXYLength, relLineEndXY.Y * earlyOffDist / relXYLength);
                    double relEarlyOffZ = relEndZ * earlyOffDist / relXYLength;

                    //Calc Line rel position
                    double dispDist = unitPitchDist - delayOnDist - earlyOffDist;
                    TPos2 relDispXY = new TPos2(relLineEndXY.X * dispDist / relXYLength, relLineEndXY.Y * dispDist / relXYLength);
                    double dispDistZ = unitPitchDist - delayOnDist - earlyOffDist;
                    double relDispDistZ = relEndZ * dispDist / relXYLength;
                    #endregion

                    //Calc EarlyOffDist in lead line
                    TPos2 relLead2XY = new TPos2(relLeadXY);
                    double lead2Length = leadLength - earlyOffDist;
                    if (lead2Length > 0) relLead2XY = new TPos2(relLeadXY.X * lead2Length / leadLength, relLeadXY.Y * lead2Length / leadLength);

                    //Calc DelayOnDist in lead line
                    TPos2 relLag2XY = new TPos2(relLagXY);
                    double lag2Length = lagLength - delayOnDist;
                    if (lag2Length > 0) relLag2XY = new TPos2(relLagXY.X * lag2Length / lagLength, relLagXY.Y * lag2Length / lagLength);

                    int dashCount = (vhType == EVHType.Hort ? layout.UColCount : layout.URowCount);

                    if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                    {
                        //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLeadXY.X, relLeadXY.Y, junctionLift, junctionLift }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLead2XY.X, relLead2XY.Y, 0, 0 }, null);
                        if (lead2Length > 0)
                        {
                            CommonControl.P1245.PathAddDO(Axis, Output, false);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relEarlyOffXY.X, relEarlyOffXY.Y, junctionLift, junctionLift }, null);
                        }
                    }
                    else
                    {
                        //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLeadXY.X, relLeadXY.Y, 0, 0 }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLead2XY.X, relLead2XY.Y, 0, 0 }, null);
                        if (lead2Length > 0)
                        {
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relEarlyOffXY.X, relEarlyOffXY.Y, 0, 0 }, null);
                        }
                    }

                    for (int r = 0; r < dashCount - 1; r++)
                    {
                        if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                        {
                            CommonControl.P1245.PathAddDO(Axis, Output, false);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relDelayOnXY.X, relDelayOnXY.Y, relDelayOnZ - junctionLift, relDelayOnZ - junctionLift }, null);
                            CommonControl.P1245.PathAddDO(Axis, Output, disp && RunMode == ERunMode.Normal);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relDispXY.X, relDispXY.Y, relDispDistZ, relDispDistZ }, null);
                            CommonControl.P1245.PathAddDO(Axis, Output, false);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relEarlyOffXY.X, relEarlyOffXY.Y, relEarlyOffZ + junctionLift, relEarlyOffZ + junctionLift }, null);
                        }
                        else
                        {
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relDelayOnXY.X, relDelayOnXY.Y, 0, 0 }, null);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relDispXY.X, relDispXY.Y, 0, 0 }, null);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relEarlyOffXY.X, relEarlyOffXY.Y, 0, 0 }, null);
                        }
                    }

                    if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                    {
                        //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLagXY.X, relLagXY.Y, 0, 0 }, null);
                        if (lag2Length > 0)
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relDelayOnXY.X, relDelayOnXY.Y, -junctionLift, -junctionLift }, null);
                        CommonControl.P1245.PathAddDO(Axis, Output, disp && RunMode == ERunMode.Normal);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLag2XY.X, relLag2XY.Y, 0, 0 }, null);
                        CommonControl.P1245.PathAddDO(Axis, Output, false);
                    }
                    else
                    {
                        if (lag2Length > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relDelayOnXY.X, relDelayOnXY.Y, 0, 0 }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLag2XY.X, relLag2XY.Y, 0, 0 }, null);
                    }
                }

                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.PostWait, 0, null, null);

                #region Path CutTail
                double priorLineLength = Math.Sqrt(Math.Pow(relLagXY.X, 2) + Math.Pow(relLagXY.Y, 2));
                if (priorLineLength == 0) goto _SkipCutTail;
                double extRelX = relLagXY.X * Line.DPara[10] / priorLineLength;
                double extRelY = relLagXY.Y * Line.DPara[10] / priorLineLength;
                double cutTailSpeed = Line.DPara[11];
                double cutTailSSpeed = Math.Min(Model.LineStartV, cutTailSpeed);
                double cutTailHeight = (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry) ? Line.DPara[12] : 0;
                ECutTailType cutTailType = ECutTailType.None;
                try { cutTailType = (ECutTailType)Line.DPara[13]; } catch { };

                bool b_Blend = false;

                switch (cutTailType)
                {
                    case ECutTailType.None:
                        break;
                    case ECutTailType.Fwd:
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, cutTailHeight, cutTailHeight }, null);
                        break;
                    case ECutTailType.Bwd:
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, cutTailHeight, cutTailHeight }, null);
                        break;
                    case ECutTailType.SqFwd:
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                        break;
                    case ECutTailType.SqBwd:
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, 0, 0 }, null);
                        break;
                    case ECutTailType.Rev:
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, cutTailHeight, cutTailHeight }, null);
                        break;
                    case ECutTailType.SqRev:
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, 0, 0 }, null);
                        break;
                }
            _SkipCutTail:
                #endregion

                CommonControl.P1245.PathEnd(Axis);
                CommonControl.P1245.PathMove(Axis);

                while (true)
                {
                    if (!CommonControl.P1245.AxisBusy(Axis)) break;
                }

                #region Move ZRetGap, ZUpGap and ZPanelGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {
                            if (Model.RetGap != 0)
                            {
                                #region Move Ret
                                if (!TaskGantry.SetMotionParamGZZ2(Model.RetStartV, Model.RetSpeed, Model.RetAccel)) return false;
                                if (!DispProg.MoveRelZ(bHeadRun[0], bHeadRun[1], Model.RetGap, Model.RetGap)) return false;
                                #endregion
                                #region Ret Wait
                                if (Model.RetWait > 0)
                                {
                                    t = GDefine.GetTickCount() + Model.RetWait;
                                    while (GDefine.GetTickCount() < t)
                                    {
                                        TaskDisp.Thread_CheckIsFilling_Run(bHeadRun[0], bHeadRun[1]);
                                    }
                                }
                                #endregion
                            }
                            if (Model.UpGap != 0)
                            {
                                #region Move Up
                                if (!TaskGantry.SetMotionParamGZZ2(Model.UpStartV, Model.UpSpeed, Model.UpAccel)) return false;
                                if (!DispProg.MoveRelZ(bHeadRun[0], bHeadRun[1], Model.UpGap, Model.UpGap)) return false;
                                #endregion
                                #region Up Wait
                                t = GDefine.GetTickCount() + Model.UpWait;
                                while (GDefine.GetTickCount() < t)
                                {
                                    TaskDisp.Thread_CheckIsFilling_Run(bHeadRun[0], bHeadRun[1]);
                                }
                                #endregion
                            }
                            break;
                        }
                    case ERunMode.Camera:
                        {
                            break;
                        }
                }
                #endregion

                if (DispProg.Options_EnableProcessLog)
                {
                    double gz1 = TaskGantry.EncoderPos(TaskGantry.GZAxis);
                    string str = $"{Line.Cmd}\t";
                    str += $"DispGap={Model.DispGap:f3}\t";
                    str += $"C,R={DispProg.RunTime.Head_CR[0].X},{DispProg.RunTime.Head_CR[0].Y}\t";
                    str += $"X,Y,Z={GXY.X:f3},{GXY.Y:f3},{gz1:f3} XE,YE,ZE ={ GXY.X + relLineEndXY.X:f3},{ GXY.Y + relLineEndXY.Y:f3},{ gz1 + relEndZ:f3}\t";
                    if (DispProg.Head_Operation == TaskDisp.EHeadOperation.Sync)
                    {
                        double gz2 = TaskGantry.EncoderPos(TaskGantry.GZ2Axis);
                        str += $"C2,R2={DispProg.RunTime.Head_CR[1].X},{DispProg.RunTime.Head_CR[1].Y}\t";
                        str += $"X2,Y2,Z2={GX2Y2.X:f3},{GX2Y2.Y:f3},{gz2:f3} X2,Y2,Z2={GX2Y2.X + relLineEndXY.X:f3},{GX2Y2.Y + relLineEndXY.Y:f3},{gz2 + relEndZ:f3}\t";
                        double zdiff = TaskDisp.Head_ZSensor_RefPosZ[1] - TaskDisp.Head_ZSensor_RefPosZ[0];
                        str += $"XA2,YA2,ZA2={GXY.X + (absStart[1].X - absStart[0].X):f3},{GXY.Y + (absStart[1].Y - absStart[0].Y):f3},{gz2 - TaskDisp.Head_ZSensor_RefPosZ[1] - TaskDisp.Head_ZSensor_RefPosZ[0]:f3}\t";
                    }
                    GLog.WriteProcessLog(str);
                }
            }
            catch (Exception Ex)
            {
                GDefine.Status = EStatus.ErrorInit;
                TaskDisp.TrigOff(true, true);
                string eMsg = Line.Cmd.ToString() + (char)13 + Ex.Message.ToString();
                throw new Exception(eMsg);
            }
        _End:
            GDefine.Status = EStatus.Ready;
            return true;
        _Stop:
            GDefine.Status = EStatus.Stop;
            return false;
        _Error:
            GDefine.Status = EStatus.ErrorInit;
            return false;
        }
    }

    internal class MeasTemp
    {
        public static bool Execute(DispProg.TLine Line, ERunMode RunMode, double f_origin_x, double f_origin_y, double f_origin_z)
        {
            int points = Line.IPara[1];
            if (points == 0) return true;

            try
            {
                GDefine.Status = EStatus.Busy;

                if (!TaskDisp.TaskMoveGZZ2Up()) return false;

                switch (RunMode)
                {
                    case ERunMode.Dry:
                    case ERunMode.Normal:
                        TaskVision.LightingOff();
                        break;
                    case ERunMode.Camera:
                        TaskVision.LightingOn(TaskVision.DefLightRGB);
                        break;
                }

                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2)
                {
                    TPos2 GX2Y2 = new TPos2(TaskDisp.Head2_DefPos.X, TaskDisp.Head2_DefPos.Y);

                    if (!TaskGantry.SetMotionParamGX2Y2()) goto _Error;
                    if (!TaskGantry.MoveAbsGX2Y2(GX2Y2.X, GX2Y2.Y, false)) goto _Error;

                    TaskGantry.WaitGX2Y2();
                }

                List<TPos2> pos = new List<TPos2>();
                List<double> temp = new List<double>();
                for (int i = 0; i < points; i++)
                {
                    #region assign and translate position
                    double dx = f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].X + Line.X[i];
                    double dy = f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].Y + Line.Y[i];
                    DispProg.TranslatePos(dx, dy, DispProg.rt_Head1RefData, ref dx, ref dy);

                    TPos2 GXY = new TPos2(dx, dy);
                    #endregion

                    #region Move To Pos
                    switch (RunMode)
                    {
                        case ERunMode.Normal:
                        case ERunMode.Dry:
                            {
                                GXY.X = GXY.X + TaskDisp.TempSensor_Ofst.X;
                                GXY.Y = GXY.Y + TaskDisp.TempSensor_Ofst.Y;
                                break;
                            }
                        case ERunMode.Camera:
                        default:
                            {
                                break;
                            }
                    }

                    if (!TaskGantry.SetMotionParamGXY()) goto _Error;
                    if (!TaskGantry.MoveAbsGXY(GXY.X, GXY.Y, false)) goto _Error;
                    TaskGantry.WaitGXY();
                    #endregion

                    var sw = System.Diagnostics.Stopwatch.StartNew();
                    //int SettleTime = Line.IPara[4];
                    while (sw.ElapsedMilliseconds < TaskLaser.TempSensor_SettleTime) Thread.Sleep(0);

                    double d = 0;
                    TFTempSensor.GetTemp(ref d);

                    pos.Add(GXY);
                    temp.Add(d);
                }

                if (DispProg.Options_EnableProcessLog)
                {
                    string str = $"{Line.Cmd}\t";
                    str += $"Cal\t";
                    str += $"OX,OY={TaskDisp.TempSensor_Ofst.X:f3},{TaskDisp.TempSensor_Ofst.Y:f3}\t";
                    GLog.WriteProcessLog(str);

                    str = $"{Line.Cmd}\t";
                    str += $"C,R={DispProg.RunTime.Head_CR[0].X},{DispProg.RunTime.Head_CR[0].Y}\t";
                    for (int i = 0; i < temp.Count; i++)
                    {
                        str += $"X,Y,T={pos[i].X},{pos[i].Y},{temp[i]:f1}\t";
                    }
                    GLog.WriteProcessLog(str);
                }
            }
            catch (Exception Ex)
            {
                GDefine.Status = EStatus.ErrorInit;
                throw new Exception(MethodBase.GetCurrentMethod().Name.ToString() + '\r' + Ex.Message.ToString());
            }
            GDefine.Status = EStatus.Ready;
            return true;
        _Error:
            GDefine.Status = EStatus.ErrorInit;
            return false;
        }
    }

    internal class ParLines
    {
        public static bool Par_Lines(DispProg.TLine Line, ERunMode RunMode, double f_origin_x, double f_origin_y, double f_origin_z)
        {
            GDefine.Status = EStatus.Busy;

            int profMode = Line.IPara[9];

            try
            {
                TModelPara Model = new TModelPara(DispProg.ModelList, Line.IPara[0]);
                bool disp = (Line.IPara[2] > 0);

                EVHType vhType = EVHType.Hort;//0=Horizontal, 1=Vertical
                try { vhType = (EVHType)Line.IPara[3]; } catch { };

                bool endDispense = Line.IPara[6] > 0;

                bool indFirstLine = Line.IPara[11] > 0;
                bool indLastLine = Line.IPara[12] > 0;

                double startLength = Line.DPara[0];
                double endLength = Line.DPara[1];
                double startGap = Line.DPara[2];
                double endGap = Line.DPara[3];
                double speedAdjust = Line.DPara[4];
                double startOfst = Line.DPara[6];
                double endOfst = Line.DPara[7];
                double startVolume = Line.DPara[8];

                //Profile1 - Para
                    int segCount = (int)Line.DPara[25];
                    double segSize = Line.DPara[26];
                double riseGap = Line.DPara[27];
                double fallGap = Line.DPara[28];
                double maxSpeed = Line.DPara[23];
                double startEndOfst = Line.DPara[9];

                if (profMode == 1)
                {
                    speedAdjust = 0;
                    startLength = 0;
                    endLength = 0;
                    startGap = 0;
                    endGap = 0;
                    startOfst = startEndOfst;
                    endOfst = startEndOfst;
               }

                bool[] bHeadRun = new bool[2] { false, false };
                bool bHead2IsValid = false;
                bool bSyncHead2 = false;
                if (!DispProg.SelectHead(Line, ref bHeadRun, ref bHead2IsValid, ref bSyncHead2)) goto _End;

                switch (RunMode)
                {
                    case ERunMode.Normal:
                        {
                            if (disp)
                            {
                                if (GDefine.DispCtrlType[0] == GDefine.EDispCtrlType.HPC3)
                                {
                                    bool[] pumpToFill = TFPump.PP4.CheckStrokeToFill(bHeadRun);

                                    if (pumpToFill[0] || pumpToFill[1])
                                    {
                                        if (!TaskDisp.TaskGotoTPos2(TaskDisp.Needle_Clean_Pos)) goto _Error;
                                        if (!TFPump.PP4.PFill(pumpToFill)) goto _Error;

                                        if (!TaskDisp.TaskCleanNeedle(bHeadRun[0], bHeadRun[1], RunMode == ERunMode.Normal)) goto _Error;
                                        if (!TaskDisp.TaskPurgeNeedle(bHeadRun[0], bHeadRun[1], RunMode == ERunMode.Normal)) goto _Error;
                                    }
                                }

                                if (!TaskDisp.CtrlWaitReady(bHeadRun[0], bHeadRun[1])) goto _Stop;
                            }
                            break;
                        }
                    case ERunMode.Dry:
                    case ERunMode.Camera:
                        {
                            break;
                        }
                }

                TLayout layout = new TLayout();
                layout.Copy(DispProg.rt_Layouts[DispProg.rt_LayoutID]);

                Point currentUnitCR = new Point(0, 0);
                layout.UnitNoGetRC(DispProg.RunTime.UIndex, ref currentUnitCR);
                Point currentClusterCR = new Point(0, 0);
                currentClusterCR.X = currentUnitCR.X / layout.UColCount;
                currentClusterCR.Y = currentUnitCR.Y / layout.URowCount;

                if (!DispProg.SetPumpParameters(Model, disp, bHeadRun)) goto _Stop;

                #region Move GZ2 Up if invalid
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2 && !bHead2IsValid)
                {
                    switch (RunMode)
                    {
                        case ERunMode.Normal:
                        case ERunMode.Dry:
                            if (!TaskDisp.TaskMoveGZ2Up()) return false;
                            break;
                    }
                }
                #endregion

                #region assign and xy translate position
                TPos2 lineStart = new TPos2(Line.X[0], Line.Y[0]);
                if (vhType == EVHType.Hort)
                {
                    if (indFirstLine && currentUnitCR.Y == 0) lineStart = new TPos2(Line.X[1], Line.Y[1]);
                    if (indLastLine && currentUnitCR.Y == layout.TRowCount - 1) lineStart = new TPos2(Line.X[2], Line.Y[2]);
                }
                else //Vert
                {
                    if (indFirstLine && currentUnitCR.X == 0) lineStart = new TPos2(Line.X[1], Line.Y[1]);
                    if (indLastLine && currentUnitCR.X == layout.TColCount - 1) lineStart = new TPos2(Line.X[2], Line.Y[2]);
                }

                TPos2 absStart = new TPos2(f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].X + lineStart.X, f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].Y + lineStart.Y);
                DispProg.TranslatePos(absStart.X, absStart.Y, DispProg.rt_Head1RefData, ref absStart.X, ref absStart.Y);
                #endregion

                #region Calculate the End positions
                Point firstCR = new Point(0, 0);
                Point lastCR = new Point(0, 0);
                int lastUnitNo = 0;//Last UnitNo of Hort/Vert line 
                if (vhType == EVHType.Hort)
                {
                    layout.UnitNoGetRC(DispProg.RunTime.UIndex, ref firstCR);
                    lastCR = new Point((currentClusterCR.X * layout.UColCount) + layout.UColCount - 1, firstCR.Y);
                    layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current row.
                }
                else
                {
                    layout.UnitNoGetRC(DispProg.RunTime.UIndex, ref firstCR);
                    lastCR = new Point(firstCR.X, (currentClusterCR.Y * layout.URowCount) + layout.URowCount - 1);
                    layout.RCGetUnitNo(ref lastUnitNo, lastCR.X, lastCR.Y);//Get the last unit number of the current col.
                }
                TPos2 absEnd = new TPos2(f_origin_x + DispProg.rt_LayoutRelPos[lastUnitNo].X + lineStart.X, f_origin_y + DispProg.rt_LayoutRelPos[lastUnitNo].Y + lineStart.Y);
                DispProg.TranslatePos(absEnd.X, absEnd.Y, DispProg.rt_Head1RefData, ref absEnd.X, ref absEnd.Y);
                #endregion

                bool reverse = Line.IPara[5] > 0;
                if (reverse)
                {
                    if ((vhType == EVHType.Hort && firstCR.Y % 2 != 0) || (vhType == EVHType.Vert && firstCR.X % 2 != 0))
                    {
                        TPos2 temp = new TPos2(absStart);
                        absStart = new TPos2(absEnd);
                        absEnd = new TPos2(temp);
                    }
                }

                #region Calculate the start Z positions
                double[] absZ = new double[] { 0, 0 };
                absZ[0] = f_origin_z + TaskDisp.Head_Ofst[0].Z; //Assign Z positions
                absZ[1] = absZ[0] + (TaskDisp.Head_ZSensor_RefPosZ[1] - TaskDisp.Head_ZSensor_RefPosZ[0]); //Update ZPlane if valid Z values
                DispProg.UpdateZHeight(bSyncHead2, absStart.X, absStart.Y, absStart.X, absStart.Y, ref absZ[0], ref absZ[1]);
                double[] zRetGapPos = new double[] { Math.Min(absZ[0] + Model.DispGap + Model.RetGap, TaskDisp.ZDefPos), Math.Min(absZ[1] + Model.DispGap + Model.RetGap, TaskDisp.ZDefPos) };

                double[] absEndZ = new double[] { absZ[0], absZ[1] };
                DispProg.UpdateZHeight(bSyncHead2, absEnd.X, absEnd.Y, absEnd.X, absEnd.Y, ref absEndZ[0], ref absEndZ[1]);//Head2 Z follow Head1
                #endregion

                //Calculate the relative line end pos
                TPos2 relLineEndXY = new TPos2(absEnd.X - absStart.X, absEnd.Y - absStart.Y);
                double relEndZ = absEndZ[0] - absZ[0];

                //Calculate the lead lag relative pos
                double lineLength = Math.Sqrt(Math.Pow(relLineEndXY.X, 2) + Math.Pow(relLineEndXY.Y, 2)) - startOfst - endOfst;

                TPos2 relStartOfstXY = new TPos2(startOfst / lineLength * relLineEndXY.X, startOfst / lineLength * relLineEndXY.Y);
                TPos2 relEndOfstXY = new TPos2(endOfst / lineLength * relLineEndXY.X, endOfst / lineLength * relLineEndXY.Y);

                //Calc the Rise and Fall relative pos
                double constLineLength = lineLength - startLength - endLength;
                TPos2 relStartXY = new TPos2(startLength / lineLength * relLineEndXY.X, startLength / lineLength * relLineEndXY.Y);
                TPos2 relEndXY = new TPos2(endLength / lineLength * relLineEndXY.X, endLength / lineLength * relLineEndXY.Y);
                TPos2 relConstXY = new TPos2(constLineLength / lineLength * relLineEndXY.X, constLineLength / lineLength * relLineEndXY.Y);

                #region Move abs Start Pos + lead length, move head2 to position
                if (!TaskGantry.SetMotionParamGXY()) goto _Error;
                TPos2 GXY = new TPos2(absStart.X + relStartOfstXY.X, absStart.Y + relStartOfstXY.Y);
                if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                {
                    GXY.X += TaskDisp.Head_Ofst[0].X;
                    GXY.Y += TaskDisp.Head_Ofst[0].Y;
                }
                if (!TaskGantry.MoveAbsGXY(GXY.X, GXY.Y, false)) goto _Error;
                TPos2 GX2Y2 = new TPos2(TaskDisp.Head2_DefPos.X, TaskDisp.Head2_DefPos.Y);
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2)
                {
                    if (bHeadRun[1])
                    {
                        if (!TaskGantry.SetMotionParamGX2Y2()) goto _Error;
                        if (!TaskGantry.MoveAbsGX2Y2(GX2Y2.X, GX2Y2.Y, false)) goto _Error;
                    }
                    TaskGantry.WaitGX2Y2();
                }
                TaskGantry.WaitGXY();
                #endregion

                //Move to abs RetractGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {
                            if (!TaskGantry.SetMotionParamGZZ2(Model.DnStartV, Model.DnSpeed, Model.DnAccel)) return false;
                            if (!DispProg.MoveZAbs(bHeadRun[0], bHeadRun[1], zRetGapPos[0], zRetGapPos[1])) return false;
                            //return false;
                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }

                double[] GZ = new double[] { Math.Min(absZ[0] + Model.DispGap, TaskDisp.ZDefPos), Math.Min(absZ[1] + Model.DispGap + TaskDisp.Head2_ZOffset, TaskDisp.ZDefPos) };//include H2 ZOffset
                if (startLength > 0)
                    GZ = new double[] { Math.Min(absZ[0] + startGap, TaskDisp.ZDefPos), Math.Min(absZ[1] + TaskDisp.Head2_ZOffset + startGap, TaskDisp.ZDefPos) };//include H2 ZOffset

                //Move to abs DispGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {

                            double sv = Model.DnStartV;
                            double dv = Model.DnSpeed;
                            double ac = Model.DnAccel;
                            if (!TaskGantry.SetMotionParamGZZ2(sv, dv, ac)) goto _Stop;
                            if (!DispProg.MoveZAbs(bHeadRun[0], bHeadRun[1], GZ[0], GZ[1])) return false;

                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }

                #region Dn Wait
                int t = GDefine.GetTickCount() + (int)Model.DnWait;
                while (GDefine.GetTickCount() < t)
                {
                    if (Model.DnWait > 75) Thread.Sleep(1);
                }
                #endregion

                switch (GDefine.DispCtrlType[0])
                {
                    case GDefine.EDispCtrlType.HPC3:
                        {
                            #region Path Prep
                            CControl2.TAxis[] Axis = new CControl2.TAxis[] { TaskGantry.GXAxis, TaskGantry.GYAxis, TaskGantry.GZAxis, TaskGantry.GZ2Axis, TaskGantry.PAAxis, TaskGantry.PBAxis };
                            CommonControl.P1245.PathFree(Axis);
                            CommonControl.P1245.SetAccel(Axis, Model.LineAccel);
                            double LineSpeed = Model.LineSpeed;
                            #endregion

                            int useWeight = Line.IPara[4];
                            switch (useWeight)
                            {
                                case 1://Weight Mode
                                    {
                                        double LineMass = Line.DPara[21];
                                        double FLineMass = Line.DPara[20] > 0 ? Line.DPara[20] : Line.DPara[21];
                                        double LLineMass = Line.DPara[22] > 0 ? Line.DPara[22] : Line.DPara[21];

                                        double targetWeight = 0;
                                        if (vhType == EVHType.Hort)
                                        {
                                            targetWeight = LineMass;
                                            if (currentUnitCR.Y == 0) targetWeight = FLineMass;
                                            if (currentUnitCR.Y == layout.TRowCount - 1) targetWeight = LLineMass;
                                        }
                                        else //Vert
                                        {
                                            targetWeight = LineMass;
                                            if (currentUnitCR.X == 0) targetWeight = FLineMass;
                                            if (currentUnitCR.X == layout.TColCount - 1) targetWeight = LLineMass;
                                        }

                                        double vol = targetWeight / TaskWeight.CurrentCal[0];
                                        double dispVol = vol + DispProg.PP_HeadA_BackSuckVol;
                                        DispProg.PP_HeadA_DispBaseVol = dispVol;
                                        double time = TaskDisp.CalcPPDispTime(dispVol);// + addLineTime;
                                        time = time + (time * speedAdjust / 100);

                                        LineSpeed = lineLength / time;
                                        if (!endDispense)
                                        {
                                            LineSpeed = (lineLength - endLength) / time;
                                        }
                                        Log.AddToEventLog($"DispVol(ul) {vol}, BSuck(ul) {DispProg.PP_HeadA_BackSuckVol}, LineTime {time}, LineSpeed {LineSpeed}");
                                        if (LineSpeed > 150) throw new Exception("Auto Line Speed over 150mm/s. Run Aborted.");
                                        break;
                                    }
                                case 2://Volume Mode
                                    {
                                        double LineVol = Line.DPara[21];
                                        double FLineVol = Line.DPara[20] > 0 ? Line.DPara[20] : Line.DPara[21];
                                        double LLineVol = Line.DPara[22] > 0 ? Line.DPara[22] : Line.DPara[21];

                                        double targetWeight = 0;
                                        if (vhType == EVHType.Hort)
                                        {
                                            targetWeight = LineVol;
                                            if (currentUnitCR.Y == 0) targetWeight = FLineVol;
                                            if (currentUnitCR.Y == layout.TRowCount - 1) targetWeight = LLineVol;
                                        }
                                        else //Vert
                                        {
                                            targetWeight = LineVol;
                                            if (currentUnitCR.X == 0) targetWeight = FLineVol;
                                            if (currentUnitCR.X == layout.TColCount - 1) targetWeight = LLineVol;
                                        }

                                        double vol = targetWeight;// / TaskWeight.CurrentCal[0];
                                        double dispVol = vol + DispProg.PP_HeadA_BackSuckVol;
                                        DispProg.PP_HeadA_DispBaseVol = dispVol;
                                        double time = TaskDisp.CalcPPDispTime(dispVol);// + addLineTime;
                                        time = time + (time * speedAdjust / 100);

                                        LineSpeed = lineLength / time;
                                        if (!endDispense)
                                        {
                                            LineSpeed = (lineLength - endLength) / time;
                                        }
                                        Log.AddToEventLog($"DispVol(ul) {vol}, BSuck(ul) {DispProg.PP_HeadA_BackSuckVol}, LineTime {time}, LineSpeed {LineSpeed}");
                                        if (LineSpeed > 150) throw new Exception("Auto Line Speed over 150mm/s. Run Aborted.");
                                        break;
                                    }
                            }

                            if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                            {
                                switch (profMode)
                                {
                                    case 0:
                                    default:
                                        {
                                            double nettDispVol = DispProg.PP_HeadA_DispBaseVol;// + DispProg.PP_HeadA_BackSuckVol;
                                            double pp = -TFPump.PP4.LengthConversion(nettDispVol);

                                            double startDispVol = (pp - (-startVolume)) * startLength / lineLength;
                                            double fallDispVol = (pp - (-startVolume)) * endLength / lineLength;
                                            double constDispVol = pp - startDispVol - fallDispVol - (-startVolume);
                                            if (!endDispense)
                                            {
                                                startDispVol = (pp - (-startVolume)) * startLength / (lineLength - endLength);
                                                constDispVol = pp - startDispVol - (-startVolume);
                                                fallDispVol = 0;
                                                //dd = startDispVol + constDispVol + (-startVolume);
                                            }
                                            Log.AddToEventLog($"PP Dist {pp:f4} {startDispVol:f4}+{constDispVol:f4}+{fallDispVol:f4}+{-startVolume:f4}");

                                            if (RunMode == ERunMode.Dry)
                                            {
                                                startVolume = 0;
                                                startDispVol = 0;
                                                constDispVol = 0;
                                                fallDispVol = 0;
                                            }

                                            double sLineSpeed = TFPump.PP4.DispSpeed;
                                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, Model.DnWait, 0, null, null);
                                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, sLineSpeed, 0, new double[6] { 0, 0, 0, 0, -startVolume, 0 }, null);
                                            if (startLength > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, LineSpeed, 0, new double[6] { relStartXY.X, relStartXY.Y, Model.DispGap - startGap, 0, startDispVol, 0 }, null);
                                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, LineSpeed, LineSpeed, new double[6] { relConstXY.X + relEndOfstXY.X, relConstXY.Y + relEndOfstXY.Y, relEndZ, 0, constDispVol, 0 }, null);
                                            if (endLength > 0) CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, LineSpeed, 0, new double[6] { relEndXY.X, relEndXY.Y, endGap - Model.DispGap, 0, fallDispVol, 0 }, null);

                                            break;
                                        }
                                    case 1:
                                        {
                                            LineSpeed = Math.Min(LineSpeed, maxSpeed);

                                            double nettDispVol = DispProg.PP_HeadA_DispBaseVol;// + DispProg.PP_HeadA_BackSuckVol;
                                            double dispLen = TFPump.PP4.LengthConversion(nettDispVol);

                                            double segVol = dispLen / (lineLength / segSize);//distributed volume/segment

                                            double[] riseSegVolRatio = new double[10];
                                            Array.Copy(Line.DPara, 50, riseSegVolRatio, 0, 10);
                                            double[] riseSegVol = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                            double relRiseGap = riseGap / segCount;

                                            double[] fallSegVolRatio = new double[10];
                                            Array.Copy(Line.DPara, 60, fallSegVolRatio, 0, 10);
                                            double[] fallSegVol = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                            double relFallGap = fallGap / segCount;

                                            string sRiseSegVol = "";
                                            string sFallSegVol = "";
                                            for (int i = 0; i < segCount; i++)
                                            {
                                                riseSegVol[i] = segVol * riseSegVolRatio[i];
                                                fallSegVol[i] = segVol * fallSegVolRatio[i];
                                                sRiseSegVol += $"{riseSegVol[i]:f3},";
                                                sFallSegVol += $"{fallSegVol[i]:f3},";
                                            }

                                            double constDispVol = dispLen - startVolume - riseSegVol.Sum() - fallSegVol.Sum();

                                            if (RunMode == ERunMode.Dry)
                                            {
                                                startVolume = 0;
                                                constDispVol = 0;

                                                for (int i = 0; i < 10; i++)
                                                {
                                                    riseSegVol[i] = 0;
                                                    fallSegVol[i] = 0;
                                                }
                                            }

                                            double pumpSpeed = TFPump.PP4.DispSpeed;
                                            double[] segRiseSpeed = new double[11];
                                            double[] segFallSpeed = new double[11];
                                            PointD relSegRiseDist = new PointD(relLineEndXY.X*(segSize /lineLength), relLineEndXY.Y * (segSize / lineLength));
                                            PointD relSegFallDist = new PointD(relLineEndXY.X * (segSize / lineLength), relLineEndXY.Y * (segSize / lineLength));

                                            string sSegRiseSpeed = "";
                                            for (int i = 0; i < segCount + 1; i++)
                                            {
                                                double inc = (LineSpeed - pumpSpeed) / segCount;
                                                segRiseSpeed[i] = pumpSpeed + (inc * i);
                                                sSegRiseSpeed += $"{segRiseSpeed[i]:f3},";
                                            }
                                            string sSegFallSpeed = "";
                                            for (int i = 0; i < segCount + 1; i++)
                                            {
                                                double inc = (LineSpeed - pumpSpeed) / segCount;
                                                segFallSpeed[i] = pumpSpeed + (inc * i);
                                                sSegFallSpeed += $"{segFallSpeed[i]:f3},";
                                            }

                                            constLineLength = lineLength - (segSize * (segCount - 1) *2);
                                            relConstXY = new TPos2(constLineLength / lineLength * relLineEndXY.X, constLineLength / lineLength * relLineEndXY.Y);

                                            Log.AddToEventLog($"Profile1,SegCount={(int)segCount}, SegSize={segSize:f3}, PPDist=[StartVol={startVolume}, Rise={sRiseSegVol}, Const={constDispVol:f3}, Fall={sFallSegVol}], Speed=[Rise={sSegRiseSpeed}, Const={LineSpeed:f3}, Fall={sSegFallSpeed}]");

                                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, Model.DnSpeed, 0, new double[6] { 0, 0, -riseGap, 0, 0, 0 }, null);
                                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, Model.DnWait, 0, null, null);
                                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, pumpSpeed, 0, new double[6] { 0, 0, 0, 0, -startVolume, 0 }, null);
                                            for (int i = 0; i < segCount - 1; i++)
                                            {
                                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, segRiseSpeed[i + 1], segRiseSpeed[i], new double[6] { relSegRiseDist.X, relSegRiseDist.Y, relRiseGap, 0, -riseSegVol[i], 0 }, null);
                                            }
                                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, LineSpeed, LineSpeed, new double[6] { relConstXY.X, relConstXY.Y, 0, 0, -constDispVol, 0 }, null);
                                            for (int i = segCount - 1; i > 0; i--)
                                            {
                                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, segFallSpeed[i + 1], segFallSpeed[i], new double[6] { relSegFallDist.X, relSegFallDist.Y, -relFallGap, 0, -fallSegVol[i], 0 }, null);
                                            }
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                double pumpSpeed = TFPump.PP4.DispSpeed;
                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, LineSpeed, pumpSpeed, new double[6] { relStartXY.X, relStartXY.Y, 0, 0, 0, 0 }, null);
                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, LineSpeed, LineSpeed, new double[6] { relConstXY.X + relEndOfstXY.X, relConstXY.Y + relEndOfstXY.Y, 0, 0, 0, 0 }, null);
                                CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, LineSpeed, pumpSpeed, new double[6] { relEndXY.X, relEndXY.Y, 0, 0, 0, 0 }, null);
                            }

                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, Model.PostWait, 0, null, null);

                            #region Path CutTail
                            double cutTailLength = Line.DPara[10];
                            double priorLineLength = lineLength;
                            double extRelX = relLineEndXY.X * cutTailLength / priorLineLength;
                            double extRelY = relLineEndXY.Y * cutTailLength / priorLineLength;

                            double lagLineLength = Math.Sqrt(Math.Pow(relEndXY.X, 2) + Math.Pow(relEndXY.Y, 2));
                            if (lagLineLength > 0)
                            //goto _SkipCutTail;
                            {
                                priorLineLength = lagLineLength;
                                extRelX = relEndXY.X * cutTailLength / priorLineLength;
                                extRelY = relEndXY.Y * cutTailLength / priorLineLength;
                            }

                            double cutTailSpeed = Line.DPara[11];
                            double cutTailSSpeed = Math.Min(Model.LineStartV, cutTailSpeed);
                            double cutTailHeight = (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry) ? Line.DPara[12] : 0;
                            ECutTailType cutTailType = ECutTailType.None;
                            try { cutTailType = (ECutTailType)Line.DPara[13]; } catch { };

                            bool b_Blend = false;

                            switch (cutTailType)
                            {
                                case ECutTailType.None:
                                    break;
                                case ECutTailType.Fwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, cutTailHeight, cutTailHeight }, null);
                                    break;
                                case ECutTailType.Bwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, cutTailHeight, cutTailHeight }, null);
                                    break;
                                case ECutTailType.SqFwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                                    break;
                                case ECutTailType.SqBwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, 0, 0 }, null);
                                    break;
                                case ECutTailType.Rev:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, cutTailHeight, cutTailHeight }, null);
                                    break;
                                case ECutTailType.SqRev:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, 0, 0 }, null);
                                    break;
                            }
                        _SkipCutTail:
                            #endregion

                            CommonControl.P1245.PathEnd(Axis);
                            CommonControl.P1245.PathMove(Axis);

                            while (true)
                            {
                                if (!CommonControl.P1245.AxisBusy(Axis)) break;
                            }
                        }
                        break;
                    default:
                        {
                            throw new Exception("DispCtrl Type not supported.");
                            #region Start Disp and StartDelay
                            CControl2.TAxis[] Axis = new CControl2.TAxis[] { TaskGantry.GXAxis, TaskGantry.GYAxis, TaskGantry.GZAxis, TaskGantry.GZ2Axis };
                            CommonControl.P1245.PathFree(Axis);
                            CControl2.TOutput[] Output = null;
                            DispProg.Outputs(bHeadRun, ref Output);
                            CommonControl.P1245.PathAddDO(Axis, Output, disp && RunMode == ERunMode.Normal);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.StartDelay, 0, null, null);
                            #endregion

                            CommonControl.P1245.SetAccel(Axis, Model.LineAccel);
                            double LineSpeed = Model.LineSpeed;

                            bool useWeight = Line.IPara[4] > 0;
                            if (useWeight)
                            {
                                double LineMass = Line.DPara[21];
                                double FLineMass = Line.DPara[20] > 0 ? Line.DPara[20] : Line.DPara[21];
                                double LLineMass = Line.DPara[22] > 0 ? Line.DPara[22] : Line.DPara[21];

                                double targetWeight = 0;
                                if (vhType == EVHType.Hort)
                                {
                                    targetWeight = LineMass;
                                    if (currentUnitCR.Y == 0) targetWeight = FLineMass;
                                    if (currentUnitCR.Y == layout.TRowCount - 1) targetWeight = LLineMass;
                                }
                                else //Vert
                                {
                                    targetWeight = LineMass;
                                    if (currentUnitCR.X == 0) targetWeight = FLineMass;
                                    if (currentUnitCR.X == layout.TColCount - 1) targetWeight = LLineMass;
                                }

                                double vol = targetWeight / TaskWeight.CurrentCal[0];
                                double dispVol = vol + DispProg.PP_HeadA_BackSuckVol;
                                DispProg.PP_HeadA_DispBaseVol = dispVol;
                                double time = TaskDisp.CalcPPDispTime(dispVol) + speedAdjust;
                                LineSpeed = (lineLength + startLength + endLength) / time;
                                Log.AddToEventLog($"DispVol(ul) {vol}, BSuck(ul) {DispProg.PP_HeadA_BackSuckVol}, LineSpeed {LineSpeed}");
                                if (LineSpeed > 100) throw new Exception("Auto Line Speed over 50mm/s. Run Aborted.");

                                if (!TaskDisp.SetDispVolume(
                                true, false,
                                    DispProg.PP_HeadA_DispBaseVol + DispProg.PP_HeadA_DispVol_Adj + DispProg.rt_Head1VolumeOfst,
                                    DispProg.PP_HeadB_DispBaseVol + DispProg.PP_HeadB_DispVol_Adj + DispProg.rt_Head2VolumeOfst))
                                {
                                    throw new Exception("Set Volume Error");
                                }
                            }

                            if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                            {
                                //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLeadXY.X, relLeadXY.Y, -relRiseHeight, -relRiseHeight }, null);
                                //CommonControl.P1245.PathAddDO(Axis, Output, disp && RunMode == ERunMode.Normal);
                                //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLineEndXY.X, relLineEndXY.Y, relEndZ, relEndZ }, null);
                                //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLagXY.X, relLagXY.Y, relFallHeight, relFallHeight }, null);
                                //CommonControl.P1245.PathAddDO(Axis, Output, false);
                            }
                            else
                            {
                                //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLeadXY.X, relLeadXY.Y, 0, 0 }, null);
                                //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLineEndXY.X, relLineEndXY.Y, 0, 0 }, null);
                                //CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel4DDirect, false, LineSpeed, LineSpeed, new double[4] { relLagXY.X, relLagXY.Y, 0, 0 }, null);
                            }

                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.PostWait, 0, null, null);

                            #region Path CutTail
                            //double priorLineLength = Math.Sqrt(Math.Pow(relLagXY.X, 2) + Math.Pow(relLagXY.Y, 2));
                            //if (priorLineLength == 0) goto _SkipCutTail;
                            double cutTailLength = Line.DPara[10];
                            double priorLineLength = lineLength;
                            double extRelX = relLineEndXY.X * cutTailLength / priorLineLength;
                            double extRelY = relLineEndXY.Y * cutTailLength / priorLineLength;

                            double lagLineLength = Math.Sqrt(Math.Pow(relEndXY.X, 2) + Math.Pow(relEndXY.Y, 2));
                            if (lagLineLength > 0)
                            //goto _SkipCutTail;
                            {
                                priorLineLength = lagLineLength;
                                extRelX = relEndXY.X * cutTailLength / priorLineLength;
                                extRelY = relEndXY.Y * cutTailLength / priorLineLength;
                            }

                            double cutTailSpeed = Line.DPara[11];
                            double cutTailSSpeed = Math.Min(Model.LineStartV, cutTailSpeed);
                            double cutTailHeight = (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry) ? Line.DPara[12] : 0;
                            ECutTailType cutTailType = ECutTailType.None;
                            try { cutTailType = (ECutTailType)Line.DPara[13]; } catch { };

                            bool b_Blend = false;

                            switch (cutTailType)
                            {
                                case ECutTailType.None:
                                    break;
                                case ECutTailType.Fwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, cutTailHeight, cutTailHeight }, null);
                                    break;
                                case ECutTailType.Bwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, cutTailHeight, cutTailHeight }, null);
                                    break;
                                case ECutTailType.SqFwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                                    break;
                                case ECutTailType.SqBwd:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, 0, 0 }, null);
                                    break;
                                case ECutTailType.Rev:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, cutTailHeight, cutTailHeight }, null);
                                    break;
                                case ECutTailType.SqRev:
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { extRelX, extRelY, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { 0, 0, cutTailHeight, cutTailHeight }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel3DLine, b_Blend, cutTailSpeed, cutTailSSpeed, new double[4] { -extRelX, -extRelY, 0, 0 }, null);
                                    break;
                            }
                        _SkipCutTail:
                            #endregion

                            CommonControl.P1245.PathEnd(Axis);
                            CommonControl.P1245.PathMove(Axis);

                            while (true)
                            {
                                if (!CommonControl.P1245.AxisBusy(Axis)) break;
                            }
                        }
                        break;
                }
                #region Move ZRetGap, ZUpGap and ZPanelGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {
                            if (Model.RetGap != 0)
                            {
                                #region Move Ret
                                if (!TaskGantry.SetMotionParamGZZ2(Model.RetStartV, Model.RetSpeed, Model.RetAccel)) return false;
                                if (!DispProg.MoveRelZ(bHeadRun[0], bHeadRun[1], Model.RetGap, Model.RetGap)) return false;
                                #endregion
                                #region Ret Wait
                                if (Model.RetWait > 0)
                                {
                                    t = GDefine.GetTickCount() + Model.RetWait;
                                    while (GDefine.GetTickCount() < t)
                                    {
                                        TaskDisp.Thread_CheckIsFilling_Run(bHeadRun[0], bHeadRun[1]);
                                    }
                                }
                                #endregion
                            }
                            if (Model.UpGap != 0)
                            {
                                #region Move Up
                                if (!TaskGantry.SetMotionParamGZZ2(Model.UpStartV, Model.UpSpeed, Model.UpAccel)) return false;
                                if (!DispProg.MoveRelZ(bHeadRun[0], bHeadRun[1], Model.UpGap, Model.UpGap)) return false;
                                #endregion
                                #region Up Wait
                                t = GDefine.GetTickCount() + Model.UpWait;
                                while (GDefine.GetTickCount() < t)
                                {
                                    TaskDisp.Thread_CheckIsFilling_Run(bHeadRun[0], bHeadRun[1]);
                                }
                                #endregion
                            }
                            break;
                        }
                    case ERunMode.Camera:
                        {
                            break;
                        }
                }
                #endregion

                if (DispProg.Options_EnableProcessLog)
                {
                    double gz1 = TaskGantry.EncoderPos(TaskGantry.GZAxis);
                    string str = $"{Line.Cmd}\t";
                    str += $"DispGap={Model.DispGap:f3}\t";
                    str += $"C,R={DispProg.RunTime.Head_CR[0].X},{DispProg.RunTime.Head_CR[0].Y}\t";
                    str += $"X,Y,Z={GXY.X:f3},{GXY.Y:f3},{gz1:f3} XE,YE,ZE ={ GXY.X + relLineEndXY.X:f3},{ GXY.Y + relLineEndXY.Y:f3},{ gz1 + relEndZ:f3}\t";
                    GLog.WriteProcessLog(str);
                }
            }
            catch (Exception Ex)
            {
                GDefine.Status = EStatus.ErrorInit;
                TaskDisp.TrigOff(true, true);
                string eMsg = Line.Cmd.ToString() + (char)13 + Ex.Message.ToString();
                throw new Exception(eMsg);
            }
        _End:
            GDefine.Status = EStatus.Ready;
            return true;
        _Stop:
            GDefine.Status = EStatus.Stop;
            return false;
        _Error:
            GDefine.Status = EStatus.ErrorInit;
            return false;
        }
    }

    internal class DotsZPath
    {
        public static bool Run(DispProg.TLine Line, ERunMode RunMode, double f_origin_x, double f_origin_y, double f_origin_z)  
        {
            GDefine.Status = EStatus.Busy;

            try
            {
                if (TaskDisp.VolumeOfst_Protocol != TaskDisp.EVolumeOfstProtocol.OSRAM_ICC)
                {
                    double[] newVolume = new double[2] { Line.DPara[18], Line.DPara[19] };
                    TFPump.PP4.DispAmounts = new double[] { newVolume[0], newVolume[1] };
                }

                TModelPara Model = new TModelPara(DispProg.ModelList, Line.IPara[0]);
                bool disp = (Line.IPara[2] > 0);

                double startGap = Line.DPara[2];
                double dispGap = Model.DispGap;
                double endGap = Line.DPara[3];

                double endDelay = Model.EndDelay;

                double accelDecel = Model.LineAccel;
                double initialSpeed = Model.LineStartV;
                double speed = Model.LineSpeed;
                if (Line.DPara[14] <= 0) Line.DPara[14] = 1;
                double speed2 = Model.LineSpeed * Line.DPara[14];
                double speed3 = Model.LineSpeed2;

                //bool tailOff = Line.IPara[4] > 0;
                //bool square = Line.IPara[5] > 0;

                bool[] bHeadRun = new bool[2] { false, false };
                bool bHead2IsValid = false;
                bool bSyncHead2 = false;
                if (!DispProg.SelectHead(Line, ref bHeadRun, ref bHead2IsValid, ref bSyncHead2)) goto _End;

                //Check Stroke to Fill
                switch (RunMode)
                {
                    case ERunMode.Normal:
                        {
                            if (disp)
                            {
                                if (GDefine.DispCtrlType[0] == GDefine.EDispCtrlType.HPC3)
                                {
                                    bool[] pumpToFill = TFPump.PP4.CheckStrokeToFill(bHeadRun);

                                    if (pumpToFill[0] || pumpToFill[1])
                                    {
                                        if (!TaskDisp.TaskGotoTPos2(TaskDisp.Needle_Clean_Pos)) goto _Error;
                                        if (!TFPump.PP4.PFill(pumpToFill)) goto _Error;

                                        if (!TaskDisp.TaskCleanNeedle(bHeadRun[0], bHeadRun[1], RunMode == ERunMode.Normal)) goto _Error;
                                        if (!TaskDisp.TaskPurgeNeedle(bHeadRun[0], bHeadRun[1], RunMode == ERunMode.Normal)) goto _Error;
                                    }
                                }

                                if (!TaskDisp.CtrlWaitReady(bHeadRun[0], bHeadRun[1])) goto _Stop;
                            }
                            break;
                        }
                    case ERunMode.Dry:
                    case ERunMode.Camera:
                        {
                            break;
                        }
                }

                if (!DispProg.SetPumpParameters(Model, disp, bHeadRun)) goto _Stop;

                #region Move GZ2 Up if invalid
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2 && !bHead2IsValid)
                {
                    switch (RunMode)
                    {
                        case ERunMode.Normal:
                        case ERunMode.Dry:
                            if (!TaskDisp.TaskMoveGZ2Up()) return false;
                            break;
                    }
                }
                #endregion

                TPos2 absPt1/*StartTL*/ = new TPos2(Line.X[0], Line.Y[0]);
                PointD p = new PointD(Line.X[1] - Line.X[0], Line.Y[1] - Line.Y[0]);

                #region Assign and xy translate start position
                TPos2 absStart = new TPos2(f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].X + absPt1.X, f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex].Y + absPt1.Y);
                DispProg.TranslatePos(absStart.X, absStart.Y, DispProg.rt_Head1RefData, ref absStart.X, ref absStart.Y);

                TPos2 absStart2 = new TPos2(f_origin_x + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex2].X + absPt1.X, f_origin_y + DispProg.rt_LayoutRelPos[DispProg.RunTime.UIndex2].Y + absPt1.Y);
                DispProg.TranslatePos(absStart2.X, absStart2.Y, DispProg.rt_Head2RefData, ref absStart2.X, ref absStart2.Y);
                #endregion

                #region Calculate the start Z positions
                double[] absZ = new double[] { 0, 0 };
                absZ[0] = f_origin_z + TaskDisp.Head_Ofst[0].Z; //Assign Z positions
                absZ[1] = absZ[0] + (TaskDisp.Head_ZSensor_RefPosZ[1] - TaskDisp.Head_ZSensor_RefPosZ[0]); //Update ZPlane if valid Z values
                DispProg.UpdateZHeight(bSyncHead2, absStart.X, absStart.Y, absStart.X, absStart.Y, ref absZ[0], ref absZ[1]);
                double[] zRetGapPos = new double[] { Math.Min(absZ[0] + Model.DispGap + Model.RetGap, TaskDisp.ZDefPos), Math.Min(absZ[1] + Model.DispGap + Model.RetGap, TaskDisp.ZDefPos) };
                #endregion

                #region Move abs Start Pos + start length, move head2 to position
                if (!TaskGantry.SetMotionParamGXY()) goto _Error;
                TPos2 GXY = new TPos2(absStart.X /*+ relStart.X*/, absStart.Y /*+ relStart.Y*/);
                if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                {
                    GXY.X += TaskDisp.Head_Ofst[0].X;
                    GXY.Y += TaskDisp.Head_Ofst[0].Y;
                }
                if (!TaskGantry.MoveAbsGXY(GXY.X, GXY.Y, false)) goto _Error;
                TPos2 GX2Y2 = new TPos2(TaskDisp.Head2_DefPos.X, TaskDisp.Head2_DefPos.Y);
                if (GDefine.GantryConfig == GDefine.EGantryConfig.XY_ZX2Y2_Z2)
                {
                    if (bHeadRun[1])
                    {
                        GX2Y2.X = GX2Y2.X - TaskDisp.Head2_DefDistX + (absStart2.X - absStart.X) + TaskDisp.Head2_XOffset;
                        GX2Y2.Y = GX2Y2.Y + (absStart2.Y - absStart.Y) + TaskDisp.Head2_YOffset;
                        if (!TaskGantry.SetMotionParamGX2Y2()) goto _Error;
                        if (!TaskGantry.MoveAbsGX2Y2(GX2Y2.X, GX2Y2.Y, false)) goto _Error;
                    }
                    TaskGantry.WaitGX2Y2();
                }
                TaskGantry.WaitGXY();
                #endregion

                #region Move to abs RetractGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {
                            if (!TaskGantry.SetMotionParamGZZ2(Model.DnStartV, Model.DnSpeed, Model.DnAccel)) return false;
                            if (!DispProg.MoveZAbs(bHeadRun[0], bHeadRun[1], zRetGapPos[0], zRetGapPos[1])) return false;
                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }
                double[] GZ = new double[] { Math.Min(absZ[0] + Model.DispGap, TaskDisp.ZDefPos), Math.Min(absZ[1] + Model.DispGap + TaskDisp.Head2_ZOffset, TaskDisp.ZDefPos) };//include H2 ZOffset
                #endregion

                #region Move to abs DispGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {

                            double sv = Model.DnStartV;
                            double dv = Model.DnSpeed;
                            double ac = Model.DnAccel;
                            if (!TaskGantry.SetMotionParamGZZ2(sv, dv, ac)) goto _Stop;
                            double[] relStartZ = new double[2] { 0, 0 };
                            if (!DispProg.MoveZAbs(bHeadRun[0], bHeadRun[1], GZ[0] + relStartZ[0], GZ[1] + relStartZ[1])) return false;

                            break;
                        }
                    case ERunMode.Camera:
                    default:
                        {
                            break;
                        }
                }
                #endregion

                #region Dn Wait
                int t = GDefine.GetTickCount() + (int)Model.DnWait;
                while (GDefine.GetTickCount() < t)
                {
                    if (Model.DnWait > 75) Thread.Sleep(1);
                }
                #endregion

                switch (GDefine.DispCtrlType[0])
                {
                    case GDefine.EDispCtrlType.HPC3:
                        {
                            #region Start Disp and StartDelay
                            CControl2.TAxis[] Axis = new CControl2.TAxis[] { TaskGantry.GXAxis, TaskGantry.GYAxis, TaskGantry.GZAxis, TaskGantry.GZ2Axis, TaskGantry.PAAxis, TaskGantry.PBAxis };
                            CommonControl.P1245.PathFree(Axis);
                            CommonControl.P1245.SetAccel(Axis, accelDecel);
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, Model.DnWait, 0, null, null);

                            #endregion

                            if (RunMode == ERunMode.Normal || RunMode == ERunMode.Dry)
                            {
                                //double[] nettDispVol = new double[2] { DispProg.PP_HeadA_DispBaseVol + DispProg.PP_HeadA_BackSuckVol, DispProg.PP_HeadB_DispBaseVol + DispProg.PP_HeadB_BackSuckVol };
                                //double[] nettDispVol = new double[2] { DispProg.PP_HeadA_DispBaseVol, DispProg.PP_HeadB_DispBaseVol };
                                //double[] nettBsuckVol = new double[2] { DispProg.PP_HeadA_BackSuckVol, DispProg.PP_HeadB_BackSuckVol };
                                double[] dispPulse = new double[2] { -TFPump.PP4.LengthConversion(DispProg.PP_HeadA_DispBaseVol), -TFPump.PP4.LengthConversion(DispProg.PP_HeadB_DispBaseVol) };
                                double[] bsuckPulse = new double[2] { TFPump.PP4.LengthConversion(DispProg.PP_HeadA_BackSuckVol), TFPump.PP4.LengthConversion(DispProg.PP_HeadB_BackSuckVol) };

                                if (!disp || RunMode == ERunMode.Dry)
                                {
                                    dispPulse = new double[2] { 0, 0 };
                                    bsuckPulse = new double[2] { 0, 0 };
                                }

                                double[] relEndZ = new double[2] { endGap - dispGap, endGap - dispGap };

                                if (!bHeadRun[0])
                                {
                                    //relStartZ[0] = 0;
                                    relEndZ[0] = 0;
                                    dispPulse[0] = 0;
                                    bsuckPulse[0] = 0;
                                }

                                if (!bHeadRun[1])
                                {
                                    //relStartZ[1] = 0;
                                    relEndZ[1] = 0;
                                    dispPulse[1] = 0;
                                    bsuckPulse[1] = 0;
                                }

                                double[] ratio = new double[4] { Line.DPara[10] / 100, Line.DPara[11] / 100, Line.DPara[12] / 100, Line.DPara[13] / 100 };

                                //if (square)
                                //{
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, dispPulse[0] * ratio[0], dispPulse[1] * ratio[0] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, 0, 0, bsuckPulse[0] * ratio[0], bsuckPulse[1] * ratio[0] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], 0, 0 }, null);

                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { p.X, 0, 0, 0, 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, dispPulse[0] * ratio[1], dispPulse[1] * ratio[1] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, 0, 0, bsuckPulse[0] * ratio[1], bsuckPulse[1] * ratio[1] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], 0, 0 }, null);

                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed2, 0, new double[6] { 0, p.Y, 0, 0, 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, dispPulse[0] * ratio[2], dispPulse[1] * ratio[2] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, 0, 0, bsuckPulse[0] * ratio[2], bsuckPulse[1] * ratio[2] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], 0, 0 }, null);

                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed3, 0, new double[6] { -p.X, 0, 0, 0, 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, dispPulse[0] * ratio[3], dispPulse[1] * ratio[3] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, 0, 0, bsuckPulse[0] * ratio[3], bsuckPulse[1] * ratio[3] }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], 0, 0 }, null);

                                //    if (tailOff)
                                //    {
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, -p.Y, 0, 0, 0, 0 }, null);
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], 0, 0 }, null);
                                //    }
                                //}
                                //else
                                //{
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, (dispPulse[0] * ratio[0]) - bsuckPulse[0], (dispPulse[1] * ratio[0]) - bsuckPulse[1] }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], bsuckPulse[0], bsuckPulse[1] }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, endDelay, 0, null, null);

                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { p.X, 0, 0, 0, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, (dispPulse[0] * ratio[1]) - bsuckPulse[0], (dispPulse[1] * ratio[1]) - bsuckPulse[1] }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], bsuckPulse[0], bsuckPulse[1]}, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, endDelay, 0, null, null);

                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed2, 0, new double[6] { -p.X, p.Y, 0, 0, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, (dispPulse[0] * ratio[2]) - bsuckPulse[0], (dispPulse[1] * ratio[2]) - bsuckPulse[1] }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], bsuckPulse[0], bsuckPulse[1] }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, endDelay, 0, null, null);

                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed3, 0, new double[6] { p.X, 0, 0, 0, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.DispSpeed, 0, new double[6] { 0, 0, 0, 0, (dispPulse[0] * ratio[3]) - bsuckPulse[0], (dispPulse[1] * ratio[3]) - bsuckPulse[1] }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, TFPump.PP4.BSuckSpeed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], bsuckPulse[0], bsuckPulse[1] }, null);

                                    //if (tailOff)
                                    //{
                                    //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { -p.X, -p.Y, 0, 0, 0, 0 }, null);
                                    //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, -relEndZ[0], -relEndZ[1], 0, 0 }, null);
                                    //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, false, 1, 0, null, null);
                                    //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, 0, relEndZ[0], relEndZ[1], 0, 0 }, null);
                                    //}
                                //}
                            }
                            else
                            {
                                //if (square)
                                //{
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { p.X, 0, 0, 0, 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed2, 0, new double[6] { 0, p.Y, 0, 0, 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed3, 0, new double[6] { -p.X, 0, 0, 0, 0, 0 }, null);
                                //    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                //    if (tailOff)
                                //    {
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { 0, -p.Y, 0, 0, 0, 0 }, null);
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);
                                //    }
                                //}
                                //else
                                //{
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { p.X, 0, 0, 0, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed2, 0, new double[6] { -p.X, p.Y, 0, 0, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed3, 0, new double[6] { p.X, 0, 0, 0, 0, 0 }, null);
                                    CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);

                                //    if (tailOff)
                                //    {
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.Rel6DDirect, false, speed, 0, new double[6] { -p.X, -p.Y, 0, 0, 0, 0 }, null);
                                //        CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.DnWait, 0, null, null);
                                //    }
                                //}
                            }
                            CommonControl.P1245.PathAddCmd(Axis, CControl2.EPath_MoveCmd.GPDELAY, true, Model.PostWait, 0, null, null);

                            CommonControl.P1245.PathEnd(Axis);
                            CommonControl.P1245.PathMove(Axis);

                            while (true)
                            {
                                if (!CommonControl.P1245.AxisBusy(Axis)) break;
                            }
                        }
                        break;
                    default:
                        {
                            throw new Exception("DispCtrl Type not supported.");
                        }
                }
                #region Move ZRetGap, ZUpGap and ZPanelGap
                switch (RunMode)
                {
                    case ERunMode.Normal:
                    case ERunMode.Dry:
                        {
                            if (Model.RetGap != 0)
                            {
                                #region Move Ret
                                if (!TaskGantry.SetMotionParamGZZ2(Model.RetStartV, Model.RetSpeed, Model.RetAccel)) return false;
                                if (!DispProg.MoveRelZ(bHeadRun[0], bHeadRun[1], Model.RetGap, Model.RetGap)) return false;
                                #endregion
                                #region Ret Wait
                                if (Model.RetWait > 0)
                                {
                                    t = GDefine.GetTickCount() + Model.RetWait;
                                    while (GDefine.GetTickCount() < t)
                                    {
                                        TaskDisp.Thread_CheckIsFilling_Run(bHeadRun[0], bHeadRun[1]);
                                    }
                                }
                                #endregion
                            }
                            if (Model.UpGap != 0)
                            {
                                #region Move Up
                                if (!TaskGantry.SetMotionParamGZZ2(Model.UpStartV, Model.UpSpeed, Model.UpAccel)) return false;
                                if (!DispProg.MoveRelZ(bHeadRun[0], bHeadRun[1], Model.UpGap, Model.UpGap)) return false;
                                #endregion
                                #region Up Wait
                                t = GDefine.GetTickCount() + Model.UpWait;
                                while (GDefine.GetTickCount() < t)
                                {
                                    TaskDisp.Thread_CheckIsFilling_Run(bHeadRun[0], bHeadRun[1]);
                                }
                                #endregion
                            }
                            break;
                        }
                    case ERunMode.Camera:
                        {
                            break;
                        }
                }
                #endregion

                if (DispProg.Options_EnableProcessLog)
                {
                    double gx = TaskGantry.EncoderPos(TaskGantry.GXAxis);
                    double gy = TaskGantry.EncoderPos(TaskGantry.GYAxis);
                    double gz1 = TaskGantry.EncoderPos(TaskGantry.GZAxis);
                    string str = $"{Line.Cmd}\t";
                    str += $"DispGap={Model.DispGap:f3}\t";
                    str += $"C,R={DispProg.RunTime.Head_CR[0].X},{DispProg.RunTime.Head_CR[0].Y}\t";
                    str += $"X,Y,Z={GXY.X:f3},{GXY.Y:f3},{gz1:f3} XE,YE,ZE ={gx:f3},{gy:f3},{ gz1:f3}\t";

                    if (DispProg.Head_Operation == TaskDisp.EHeadOperation.Sync)
                    {
                        double gz2 = TaskGantry.EncoderPos(TaskGantry.GZ2Axis);
                        str += $"C2,R2={DispProg.RunTime.Head_CR[1].X},{DispProg.RunTime.Head_CR[1].Y}\t";
                        str += $"X2,Y2,Z2={GX2Y2.X:f3},{GX2Y2.Y:f3},{gz2:f3}\t";
                        double zdiff = TaskDisp.Head_ZSensor_RefPosZ[1] - TaskDisp.Head_ZSensor_RefPosZ[0];
                        double[] G2Ofst = new double[] { absStart2.X - absStart.X, absStart2.Y - absStart.Y };
                        str += $"X2A,Y2A,Z2A={GXY.X + G2Ofst[0]:f3},{GXY.Y + G2Ofst[1]:f3},{gz2 - zdiff:f3}\t";
                    }

                    GLog.WriteProcessLog(str);
                }
            }
            catch (Exception Ex)
            {
                GDefine.Status = EStatus.ErrorInit;
                TaskDisp.TrigOff(true, true);
                string eMsg = Line.Cmd.ToString() + (char)13 + Ex.Message.ToString();
                throw new Exception(eMsg);
            }
        _End:
            GDefine.Status = EStatus.Ready;
            return true;
        _Stop:
            GDefine.Status = EStatus.Stop;
            return false;
        _Error:
            GDefine.Status = EStatus.ErrorInit;
            return false;
        }
    }
}