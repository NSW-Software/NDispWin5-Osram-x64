using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace NDispWin
{
    class TFPump
    {
        public static class PP4
        {
            const CControl2.EHomeMode HomeMode = CControl2.EHomeMode.MODE7_AbsSearch;
            const CControl2.EHomeDir HomeDir = CControl2.EHomeDir.P;

            public static double DispSpeed = 1;
            public static double DispAD = 100;
            public static double BSuckSpeed = 1;
            public static double BSuckAD = 100;

            public static double VelL = 0.1;//start velocity for all hardcoded
            public static double AccDec = 100;
            public static double InitSpeed = 2.5;
            public static double CleanSpeed = 5;
            public static double FillSpeed = 2.5;
            public static double AfFillSpeed = 2.5;

            public static double PistonDiameter = 4;//mm
            public static double PistonStroke = 42;//mm
            public static double RemoveAirPos = 5;
            public static double RemoveAirPress = 0.05;
            public static double AfFillDist = -1;

            public static double ProcessAmount = 90;//%
            public static double ProcessTimeOut = 60;//s

            public static double MoveDelay = 0;
            public static double PressOnDelay = 500;//ms
            public static double PressOffDelay = 500;//ms
            public static double VacDuration = 1000;//ms
            public static double RotaryDelay = 1000;//ms

            public static double RemoveAirTime = 1;//s;
            public static double RemoveBubbleTime = 1;//s;
            public static double CleanFillCount = 1;
            public static double ShotCount = 1;

            public static double[] FPress//MPa
            {
                get
                {
                    return new double[] { DispProg.FPress[0], DispProg.FPress[1] };
                }
                set
                {
                    DispProg.FPress[0] = value[0];
                    DispProg.FPress[1] = value[1];
                }
            }
            public static double[] DispAmounts
            {
                get
                {
                    return new double[] { DispProg.PP_HeadA_DispBaseVol, DispProg.PP_HeadB_DispBaseVol };
                }
                set
                {
                    DispProg.PP_HeadA_DispBaseVol = value[0];
                    DispProg.PP_HeadB_DispBaseVol = value[1];
                }
            }
            public static double[] BSuckAmounts
            {
                get
                {
                    return new double[] { DispProg.PP_HeadA_BackSuckVol, DispProg.PP_HeadB_BackSuckVol };
                }
                set
                {
                    DispProg.PP_HeadA_BackSuckVol = value[0];
                    DispProg.PP_HeadB_BackSuckVol = value[1];
                }
            }

            public static double MaxDispAmount
            {
                get
                {
                    return Math.PI * Math.Pow(PistonDiameter / 2, 2) * (PistonStroke * ProcessAmount/100);
                }
            }

            public static bool Ready(bool[] pumpSelect)
            {
                if (pumpSelect[0] && TaskGantry.AxisBusy(TaskGantry.PAAxis)) return false;
                if (pumpSelect[1] && TaskGantry.AxisBusy(TaskGantry.PBAxis)) return false;

                return true;
            }
            public static bool Init(bool[] pumpSelect)
            {
                if (!pumpSelect[0] && !pumpSelect[1]) return true;

                FPressCtrl.SetPress_MPa(FPress);

                //if (pumpSelect[0] && !TaskGantry.MotorOn(TaskGantry.PAAxis, true)) return false;
                //if (pumpSelect[1] && !TaskGantry.MotorOn(TaskGantry.PBAxis, true)) return false;
                //Delay(250);

                if (pumpSelect[0] && !TaskGantry.MotorOn(TaskGantry.PAAxis, false)) return false;
                if (pumpSelect[1] && !TaskGantry.MotorOn(TaskGantry.PBAxis, false)) return false;
                Delay(50);

                if (pumpSelect[0] && TaskGantry.MotorAlarmPrompt(TaskGantry.PAAxis)) return false;
                if (pumpSelect[1] && TaskGantry.MotorAlarmPrompt(TaskGantry.PBAxis)) return false;

                if (pumpSelect[0] && TaskGantry.AxisErrorPrompt(TaskGantry.PAAxis)) return false;
                if (pumpSelect[1] && TaskGantry.AxisErrorPrompt(TaskGantry.PBAxis)) return false;

                if (pumpSelect[0]) TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, InitSpeed, AccDec);
                if (pumpSelect[1]) TaskGantry.SetMotionParamEx(TaskGantry.PBAxis, VelL, InitSpeed, AccDec);

                if (!PRotateToFill(pumpSelect)) return false;
                PPressOn(pumpSelect);

                try
                {
                    if (pumpSelect[0]) CommonControl.P1245.SoftwareLimitEnable(TaskGantry.PAAxis, false);
                    if (pumpSelect[1]) CommonControl.P1245.SoftwareLimitEnable(TaskGantry.PBAxis, false);

                    if (pumpSelect[0]) CommonControl.P1245.Home(TaskGantry.PAAxis, HomeMode, HomeDir);
                    if (pumpSelect[1]) CommonControl.P1245.Home(TaskGantry.PBAxis, HomeMode, HomeDir);

                    if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                    if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                    //if (pumpSelect[0]) CommonControl.P1245.SoftwareLimitEnable(TaskGantry.PAAxis, true);
                    //if (pumpSelect[1]) CommonControl.P1245.SoftwareLimitEnable(TaskGantry.PBAxis, true);
                }
                catch (Exception ex) { throw ex; }

                PPressOff(pumpSelect);
                PReleasePress(pumpSelect);

                if (!PRotateToDisp(pumpSelect)) return false;

                if (pumpSelect[0] && TaskGantry.AxisErrorPrompt(TaskGantry.PAAxis)) return false;
                if (pumpSelect[1] && TaskGantry.AxisErrorPrompt(TaskGantry.PBAxis)) return false;

                return true;

                void Delay(int ms)
                {
                    var sw = Stopwatch.StartNew();
                    while (sw.ElapsedMilliseconds < (long)ms) Thread.Sleep(1);
                }
            }

            public static bool PRotateToDisp(bool[] pumpSelect)
            {
                if (pumpSelect[0])
                {
                    TaskGantry.PASvRotDisp = true;
                    TaskGantry.PASvRotFill = false;
                }
                if (pumpSelect[1])
                {
                    TaskGantry.PBSvRotDisp = true;
                    TaskGantry.PBSvRotFill = false;
                }

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < (long)RotaryDelay) Thread.Sleep(1);

                if (pumpSelect[0] && !TaskGantry.PASensDisp)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.PRESSCTRL_THREAD_TIMEOUT, "PumpA");
                    return false;
                }

                if (pumpSelect[1] && !TaskGantry.PBSensDisp)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.PRESSCTRL_THREAD_TIMEOUT, "PumpB");
                    return false;
                }

                return true;
            }
            public static bool PRotateToFill(bool[] pumpSelect)
            {
                if (pumpSelect[0])
                {
                    TaskGantry.PASvRotDisp = false;
                    TaskGantry.PASvRotFill = true;
                }
                if (pumpSelect[1])
                {
                    TaskGantry.PBSvRotDisp = false;
                    TaskGantry.PBSvRotFill = true;
                }

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < (long)RotaryDelay) Thread.Sleep(1);

                if (pumpSelect[0] && !TaskGantry.PASensFill)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.PP4_ROTATE_Fill_TIMEOUT, "PumpA");
                    return false;
                }

                if (pumpSelect[1] && !TaskGantry.PBSensFill)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.PP4_ROTATE_Fill_TIMEOUT, "PumpB");
                    return false;
                }

                return true;
            }
            public static void PPressOn(bool[] pumpSelect)
            {
                if (pumpSelect[0]) TaskGantry.BPress1 = true;
                if (pumpSelect[1]) TaskGantry.BPress2 = true;
            }
            public static void PPressOff(bool[] pumpSelect)
            {
                if (pumpSelect[0]) TaskGantry.BPress1 = false;
                if (pumpSelect[1]) TaskGantry.BPress2 = false;
            }
            public static void PReleasePress(bool[] pumpSelect)
            {
                if (pumpSelect[0]) TaskGantry.BVac1 = true;
                if (pumpSelect[1]) TaskGantry.BVac2 = true;

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < (long)VacDuration) Thread.Sleep(1);

                if (pumpSelect[0]) TaskGantry.BVac1 = false;
                if (pumpSelect[1]) TaskGantry.BVac2 = false;
            }

            public static bool PRemoveAirCancel = false;
            public static bool PRemoveAir(bool[] pumpSelect)
            {
                PRemoveAirCancel = false;
                
                FPressCtrl.SetPress_MPa(new double[] { RemoveAirPress, RemoveAirPress });

                if (!PRotateToFill(pumpSelect)) return false;
                PPressOn(pumpSelect);

                if (pumpSelect[0]) TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, FillSpeed, AccDec);
                if (pumpSelect[1]) TaskGantry.SetMotionParamEx(TaskGantry.PBAxis, VelL, FillSpeed, AccDec);

                if (pumpSelect[0]) TaskGantry.MovePtpAbs(TaskGantry.PAAxis, RemoveAirPos);
                if (pumpSelect[1]) TaskGantry.MovePtpAbs(TaskGantry.PBAxis, RemoveAirPos);

                if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < (long)(RemoveAirTime*1000))
                {
                    if (PRemoveAirCancel) break;
                    Thread.Sleep(1);
                }

                PPressOff(pumpSelect);
                PReleasePress(pumpSelect);

                if (pumpSelect[0]) TaskGantry.MovePtpAbs(TaskGantry.PAAxis, 0);
                if (pumpSelect[1]) TaskGantry.MovePtpAbs(TaskGantry.PBAxis, 0);

                if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < (long)MoveDelay) Thread.Sleep(1);

                if (!PRotateToDisp(pumpSelect)) return false;

                FPressCtrl.SetPress_MPa(FPress);

                return true;
            }
            public enum EFillState { None, Filling, Filled};
            public static EFillState[] FillState = new EFillState[] { PP4.EFillState.None, PP4.EFillState.None };
            public static bool PFill(bool[] pumpSelect)
            {
                if (!pumpSelect[0] && !pumpSelect[1]) return true;

                if (pumpSelect[0]) FillState[0] = EFillState.Filling;
                if (pumpSelect[1]) FillState[1] = EFillState.Filling;

                FPressCtrl.SetPress_MPa(FPress);

                if (!PRotateToFill(pumpSelect)) return false;
                PPressOn(pumpSelect);

                if (pumpSelect[0]) TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, FillSpeed, AccDec);
                if (pumpSelect[1]) TaskGantry.SetMotionParamEx(TaskGantry.PBAxis, VelL, FillSpeed, AccDec);

                if (pumpSelect[0]) TaskGantry.MovePtpAbs(TaskGantry.PAAxis, 0);
                if (pumpSelect[1]) TaskGantry.MovePtpAbs(TaskGantry.PBAxis, 0);

                if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < (long)MoveDelay) Thread.Sleep(1);

                PPressOff(pumpSelect);
                PReleasePress(pumpSelect);

                if (AfFillDist != 0)
                {
                    if (pumpSelect[0]) TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, AfFillSpeed, AccDec);
                    if (pumpSelect[1]) TaskGantry.SetMotionParamEx(TaskGantry.PBAxis, VelL, AfFillSpeed, AccDec);

                    if (pumpSelect[0]) TaskGantry.MovePtpAbs(TaskGantry.PAAxis, AfFillDist);
                    if (pumpSelect[1]) TaskGantry.MovePtpAbs(TaskGantry.PBAxis, AfFillDist);

                    if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                    if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                    sw = Stopwatch.StartNew();
                    while (sw.ElapsedMilliseconds < (long)MoveDelay) Thread.Sleep(1);
                }

                if (!PRotateToDisp(pumpSelect)) return false;

                if (pumpSelect[0]) FillState[0] = EFillState.Filled;
                if (pumpSelect[1]) FillState[1] = EFillState.Filled;

                Maint.PP.FillCount_Inc(pumpSelect[0], pumpSelect[1]);

                return true;
            }
            public static bool PCleanFill(bool[] pumpSelect)
            {
                FPressCtrl.SetPress_MPa(FPress);

                if (!PRotateToDisp(pumpSelect)) return false;

                if (pumpSelect[0]) TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, CleanSpeed, AccDec);
                if (pumpSelect[1]) TaskGantry.SetMotionParamEx(TaskGantry.PBAxis, VelL, CleanSpeed, AccDec);

                if (pumpSelect[0]) TaskGantry.MovePtpAbs(TaskGantry.PAAxis, -PistonStroke);
                if (pumpSelect[1]) TaskGantry.MovePtpAbs(TaskGantry.PBAxis, -PistonStroke);

                if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < (long)MoveDelay) Thread.Sleep(1);

                if (!PFill(pumpSelect)) return false;

                return true;
            }

            public static double LengthConversion(double volume_ul)
            {
                //1 ul = 1 mm3
                //h=V/(πr²)
                double length = volume_ul / (Math.PI * Math.Pow(PistonDiameter / 2, 2));
                return length;
            }
            public static bool CheckStrokeThenFill(bool[] pumpSelect)
            {
                var dispDist1 = LengthConversion(DispAmounts[0]);
                var dispDist2 = LengthConversion(DispAmounts[1]);

                bool[] pumpSelectLocal = new bool[] { false, false };
                if (pumpSelect[0] && (dispDist1 + Math.Abs(TaskGantry.PAPos)) > PistonStroke * (ProcessAmount / 100)) pumpSelectLocal[0] = true;
                if (pumpSelect[1] && (dispDist2 + Math.Abs(TaskGantry.PBPos)) > PistonStroke * (ProcessAmount / 100)) pumpSelectLocal[1] = true;

                return PFill(pumpSelectLocal);
            }
            public static bool[] CheckStrokeToFill(bool[] pumpSelect)//Return ToFill flag
            {
                var dispDist1 = LengthConversion(DispAmounts[0]);
                var dispDist2 = LengthConversion(DispAmounts[1]);

                bool[] pumpSelectLocal = new bool[] { false, false };
                if (pumpSelect[0] && (dispDist1 + Math.Abs(TaskGantry.PAPos)) > PistonStroke * (ProcessAmount / 100)) pumpSelectLocal[0] = true;
                if (pumpSelect[1] && (dispDist2 + Math.Abs(TaskGantry.PBPos)) > PistonStroke * (ProcessAmount / 100)) pumpSelectLocal[1] = true;

                return pumpSelectLocal;
            }
            public static bool IsFilled(bool[] pumpSelect)
            {
                bool f1 = pumpSelect[0] && FillState[0] > EFillState.None;
                bool f2 = pumpSelect[1] && FillState[1] > EFillState.None;

                return f1 || f2;
            }

            public static bool SingleShot(bool[] pumpSelect)
            {
                if (pumpSelect[0]) FillState[0] = EFillState.None;
                if (pumpSelect[1]) FillState[1] = EFillState.None;

                var dispSpd = DispSpeed;
                var dispAD = DispAD;
                var dispDist1 = LengthConversion(DispAmounts[0]);
                var dispDist2 = LengthConversion(DispAmounts[1]);

                var bsuckSpd = BSuckSpeed;
                var bsuckAD = BSuckAD;
                var bsuckDist1 = LengthConversion(BSuckAmounts[0]);
                var bsuckDist2 = LengthConversion(BSuckAmounts[1]);

                if (pumpSelect[0] || pumpSelect[1]) TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, DispSpeed, DispAD);
                if (pumpSelect[0] || pumpSelect[1]) TaskGantry.MoveLineRel(TaskGantry.PAAxis, TaskGantry.PBAxis, pumpSelect[0] ? - dispDist1: 0, pumpSelect[1] ? -dispDist2 : 0);

                if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                if (BSuckAmounts[0] >= 0 || BSuckAmounts[1] >= 0)
                {
                    if (pumpSelect[0] || pumpSelect[1]) TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, BSuckSpeed, BSuckAD);
                    if (pumpSelect[0] || pumpSelect[1]) TaskGantry.MoveLineRel(TaskGantry.PAAxis, TaskGantry.PBAxis, pumpSelect[0] ? bsuckDist1 : 0, pumpSelect[1] ? bsuckDist2 : 0);

                    if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                    if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);
                }

                return true;
            }
            public static bool ShotStart(bool[] pumpSelect)
            {
                if (pumpSelect[0]) FillState[0] = EFillState.None;
                if (pumpSelect[1]) FillState[1] = EFillState.None;

                if (pumpSelect[0] || pumpSelect[1]) if (!TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, DispSpeed, DispAD)) return false;
                if (pumpSelect[0] || pumpSelect[1]) if (!TaskGantry.MoveLineRel(TaskGantry.PAAxis, TaskGantry.PBAxis, pumpSelect[0] ? -PistonStroke : 0, pumpSelect[1] ? -PistonStroke : 0)) return false;

                return true;
            }
            public static bool ShotStop(bool[] pumpSelect)
            {
                CControl2.TAxis[] Axis = new CControl2.TAxis[] { TaskGantry.PAAxis, TaskGantry.PBAxis };
                CommonControl.P1245.PathStopDec(Axis);

                if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                var bsuckSpd = BSuckSpeed;
                var bsuckAD = BSuckAD;
                var bsuckDist1 = LengthConversion(BSuckAmounts[0]);
                var bsuckDist2 = LengthConversion(BSuckAmounts[1]);

                if (BSuckAmounts[0] >= 0 || BSuckAmounts[1] >= 0)
                {
                    if (pumpSelect[0] || pumpSelect[1]) if (!TaskGantry.SetMotionParamEx(TaskGantry.PAAxis, VelL, BSuckSpeed, BSuckAD)) return false;
                    if (pumpSelect[0] || pumpSelect[1]) if (!TaskGantry.MoveLineRel(TaskGantry.PAAxis, TaskGantry.PBAxis, pumpSelect[0] ? bsuckDist1 : 0, pumpSelect[1] ? bsuckDist2 : 0)) return false;

                    if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                    if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);
                }
                return true;
            }
            public static bool ShotAbort(bool[] pumpSelect)//Stop wo BSuck
            {
                CControl2.TAxis[] Axis = new CControl2.TAxis[] { TaskGantry.GXAxis, TaskGantry.GYAxis, TaskGantry.GZAxis, TaskGantry.GZ2Axis };
                CommonControl.P1245.PathStopDec(Axis);

                if (pumpSelect[0]) TaskGantry.AxisWait(TaskGantry.PAAxis);
                if (pumpSelect[1]) TaskGantry.AxisWait(TaskGantry.PBAxis);

                return true;
            }
        }
    }
}
