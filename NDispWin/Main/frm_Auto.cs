using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace NDispWin
{
    public partial class frm_Auto : Form
    {
        public frm_Auto()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            string FileName = "";
            string[] Ext = new string[3] { "bmp", "jpg", "png" };
            foreach (string S in Ext)
            {
                FileName = GDefine.AppPath + "\\Logo." + S;
                if (File.Exists(FileName))
                {
                    pbox_Logo.Image = Image.FromFile(FileName);
                    break;
                }
            }

            tmr_Perf.Enabled = true;

            StartUp();
            UpdateTabs();

            AppLanguage.Func2.WriteLangFile(this);

            tsslblDoorLock.Visible = GDefineN.EnableDoorLock;

            tsslMonCamera.Visible = GDefine.MCameraType[0] == GDefine.ECameraType.MVSGenTL || GDefine.MCameraType[1] == GDefine.ECameraType.MVSGenTL;
        }

        private void StartUp()
        {
            DoubleBuffered = true;
            KeyPreview = true;
            string S = $"{Application.ProductName} \r v{Application.ProductVersion}";
            lbl_AppVersion.Text = S;
            lbl_RunTime.Text = GDefineN.UpdateRunTime(true);
        }

        public NDispWin.frm_DispCore_Map frm_Map = new NDispWin.frm_DispCore_Map();
        public NDispWin.frm_DispCore_DispTools frm_DispTool = new NDispWin.frm_DispCore_DispTools();

        public NDispWin.frm_DispCore_Map InfoPanel_Map = new NDispWin.frm_DispCore_Map();
        public NDispWin.frm_InfoPanel_VolAdjust InfoPanel_VolAdjust = new NDispWin.frm_InfoPanel_VolAdjust();

        NDispWin.frm_MHS2ConvCtrl frmConvCtrl = new NDispWin.frm_MHS2ConvCtrl();
        NDispWin.frm_MHS2ElevCtrl frmElevCtrl = new NDispWin.frm_MHS2ElevCtrl();

        private void EnableControl(bool Enable)
        {
            pnl_Right.Enable(Enable);
            btn_Stop.Enable(true);

            bool CEnabled = GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR;
            btn_InitConveyor.Enabled = Enable && CEnabled;

            frmConvCtrl.Enable = Enable;

            btnNewDispense.Enabled = Enable;
            btnUnload.Enabled = Enable;

            #region External UI
            frm_DispTool.SetEnable(Enable);
            #endregion
        }
        private void UpdateTabs()
        {
            frm_Map.TopLevel = false;
            frm_Map.Parent = tpage_Map;
            frm_Map.FormBorderStyle = FormBorderStyle.None;
            frm_Map.Dock = DockStyle.Fill;
            frm_Map.Visible = true;
            frm_Map.BringToFront();

            int i_Top = 40;

            InfoPanel_Map.TopLevel = false;
            InfoPanel_Map.Parent = tpage_RunInfo;
            InfoPanel_Map.ViewOnly = true;
            InfoPanel_Map.FormBorderStyle = FormBorderStyle.None;
            InfoPanel_Map.Visible = true;
            InfoPanel_Map.Top = i_Top;
            InfoPanel_Map.Left = 0;
            InfoPanel_Map.Width = 5 * 120;
            InfoPanel_Map.Height = 240;
            InfoPanel_Map.BringToFront();

            InfoPanel_VolAdjust.TopLevel = false;
            InfoPanel_VolAdjust.Parent = tpage_RunInfo;
            InfoPanel_VolAdjust.FormBorderStyle = FormBorderStyle.None;
            InfoPanel_VolAdjust.Visible = true;
            InfoPanel_VolAdjust.Top = i_Top;
            InfoPanel_VolAdjust.Left = 5 * 120;
            InfoPanel_VolAdjust.BringToFront();
          
            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                pnl_Table.Visible = false;
                pnl_LRLineSt.Visible = false;
                pnl_ConvSt.Visible = false;

                frmConvCtrl.TopLevel = false;
                frmConvCtrl.Parent = tpage_Manual;
                frmConvCtrl.FormBorderStyle = FormBorderStyle.None;
                frmConvCtrl.Dock = DockStyle.Top;

                frmElevCtrl.TopLevel = false;
                frmElevCtrl.Parent = tpage_Manual;
                frmElevCtrl.FormBorderStyle = FormBorderStyle.None;
                frmElevCtrl.Dock = DockStyle.Fill;

                frmConvCtrl.SendToBack();
                pnl_SysStatus.SendToBack();
                frmConvCtrl.Visible = true;
                frmElevCtrl.Visible = true;
            }
            else
            {
                pnl_Table.Visible = true;
                pnl_LRLineSt.Visible = false;
            }

            frm_DispTool.TopLevel = false;
            frm_DispTool.Parent = tpage_DispTools;
            frm_DispTool.FormBorderStyle = FormBorderStyle.None;
            frm_DispTool.Dock = DockStyle.Fill;
            frm_DispTool.Visible = true;

            if (!GDefineN.AutoPageShowImage)
                tabControl.TabPages.Remove(tpageImage);
            else
            {
                imgBoxEmgu.Dock = DockStyle.Fill;
                TaskVision.imgBoxEmgu = imgBoxEmgu;
                imgBoxEmgu.Image = TaskVision.Image;
            }
        }

        private void lbl_SysAutoRun_DoubleClick(object sender, EventArgs e)
        {
            if (NUtils.UserAcc.Active.GroupID == (int)ELevel.NSW)
                if (GDefineN.SystemSt == GDefineN.ESystemSt.Ready)
                    GDefineN.SystemSt = (GDefineN.ESystemSt)0;
                else
                    GDefineN.SystemSt++;
        }

        private void frm_Auto_Load(object sender, EventArgs e)
        {
            this.Text = "Auto";

            AppLanguage.Func2.UpdateText(this);

            tmr_DateTime_100.Enabled = true;
            tmr_TR_Buttons.Enabled = true;
            tmr_1s.Enabled = true;


            if (GDefineN.AutoPageShowImage)
            {
                tpageImage.HorizontalScroll.Visible = false;
                tpageImage.VerticalScroll.Visible = false;
                imgBoxEmgu.HorizontalScrollBar.Visible = false;
                imgBoxEmgu.VerticalScrollBar.Visible = false;
                double XScale = (double)tpageImage.Width / TaskVision.flirCamera2[0].m_ImageEmgu.m_Image.Width;
                double YScale = (double)tpageImage.Height / TaskVision.flirCamera2[0].m_ImageEmgu.m_Image.Height;
                imgBoxEmgu.SetZoomScale(Math.Min(XScale, YScale), new Point(imgBoxEmgu.Width / 2, imgBoxEmgu.Height / 2));
            }

            if (GDefine.MCameraAutoShow)
            {
                if (frmMonCamera != null && frmMonCamera.Visible) return;

                frmMonCamera = new frmMonCamera();
                frmMonCamera.TopLevel = false;
                frmMonCamera.Parent = this;
                frmMonCamera.BringToFront();
                frmMonCamera.TopMost = true;
                frmMonCamera.Show();
            }
        }
        private void frm_Auto_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmr_DateTime_100.Enabled = false;
            tmr_TR_Buttons.Enabled = false;
            tmr_1s.Enabled = false;

            if (frmMonCamera != null && frmMonCamera.Visible)
            {
                frmMonCamera.Close();
            }
        }
        private void frm_Auto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frm_Map != null)
            {
                frm_Map.Close();
                frm_Map.Dispose();
            }
            if (InfoPanel_Map != null)
            {
                InfoPanel_Map.Close();
                InfoPanel_Map.Dispose();
            }

            Event.DEBUG_INFO.Set("Auto Form Close", $"TaskConvStatus={convStatus}, TaskDispStatus={dispStatus}");

            GC.Collect();
        }

        private void frm_Auto_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.Enter:
                    e.Handled = true; break;
            }
        }
        private void frm_Auto_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void frm_Auto_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void UpdateDisplay()
        {
            #region Left Panel
            btn_ContinuePreviousMap.BackColor = Define_Run.ResumeMap ? Color.Orange : btn_ContinuePreviousMap.BackColor = SystemColors.Control;
            btn_LotInfo.Visible = NDispWin.GDefine.EnableLotEntry;
            btn_LotInfo.BackColor = LotInfo2.LotActive ? Color.Lime : this.BackColor;
            btnBurnRun.Visible = NUtils.UserAcc.Active.GroupID >= 4;
            #endregion

            lbl_Recipe.Text = GDefine.DeviceRecipe;
            lbl_MHS.Text = "Handler: " + GDefine.MHSRecipeName;
            lbl_DispProg.Text = "DispProg: " + NDispWin.Intf.Program.Name;

            lbl_UpTime.Text = GDefineN.UpdateUpTime();
            lbl_Assist.Text = GDefineN.UpdateAssist();
            lbl_MTBA.Text = GDefineN.UpdateMTBA();

            double d_TactTime = (double)DispProg.LastTactTime / 1000;
            lbl_TactTime.Text = d_TactTime.ToString("f3") + " s";
            lbl_LastUPH.Text = DispProg.LastUPH.ToString();

            #region Bottom Panel
            int t_ByPass = TaskElev.Left.BypassDoorCheckTimer;
            tsslbl_LULDoor.Text = t_ByPass > 0 ? "LUL Door ByPass " + t_ByPass.ToString() + " s" : "LUL Door Arm";
            tsslbl_LULDoor.BackColor = t_ByPass > 0 ? Color.Orange : this.BackColor;

            //tsslblDoorLock.BackColor = ConvIO.DoorLock.Status ? Color.Red : this.BackColor;
            tsslblDoorLock.BackColor = DefineSafety.DoorLockStatus ? Color.Red : this.BackColor;
            #endregion

            #region Manual Tab
            lbl_GantrySt.Text = GDefine.Status.ToString();
            lbl_GantrySt.BackColor = Define_Gantry.StatusColor(GDefine.Status);

            if (pnl_Table.Visible)
            {
                //btn_RestartFrom1stStation.Visible = GDefine.Table.NumberOfStations >= 2;
                #region
                lbl_TableSt.Text = GDefine.Table.StationStatus.ToString();

                switch (GDefine.Table.StationStatus)
                {
                    case GDefine.Table.TStStatus.stNone:
                    case GDefine.Table.TStStatus.stLoaded:
                        lbl_TableSt.BackColor = Color.Fuchsia;
                        break;
                    case GDefine.Table.TStStatus.stInProcess:
                        lbl_TableSt.BackColor = Color.Yellow;
                        break;
                    case GDefine.Table.TStStatus.stCompleted:
                        lbl_TableSt.BackColor = Color.Lime;
                        break;
                    default:
                        lbl_TableSt.BackColor = SystemColors.ButtonFace;
                        break;
                        #endregion
                }
                #endregion}
            }
        }
        private void UpdateStatus()
        {
            if (GDefineN.SystemSt == GDefineN.ESystemSt.ErrorInit)
            {
                lbl_SysAutoRun.BackColor = Color.Red;
                lbl_SysAutoRun.Text = "System " + GDefineN.SystemSt.ToString();
                return;
            }

            if (GDefine.Status == EStatus.ErrorInit)
            {
                lbl_SysAutoRun.BackColor = Color.Red;
                lbl_SysAutoRun.Text = "Gantry " + GDefine.Status.ToString();
                return;
            }

            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                if (NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.ErrorInit)
                {
                    lbl_SysAutoRun.BackColor = Color.Red;
                    lbl_SysAutoRun.Text = "Conv " + NDispWin.TaskConv.Status.ToString();
                    return;
                }
            }

            lbl_SysAutoRun.BackColor = Color.Yellow;
            lbl_SysAutoRun.Text = "Ready";
        }
        private void UpdateWaitMagStatus()
        {
            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                if (NDispWin.TaskConv.LeftMode == NDispWin.TaskConv.ELeftMode.ElevatorZ)
                {
                    if (NDispWin.TaskElev.Left.WaitMagChange && !Define_Run.b_LeftMagEmptyAlarming)
                    {
                        //IO.SetState(ERYG.DontCare, ERYG.Blink, ERYG.DontCare, EBuzzer.On);
                        TCTwrLight.SetStatus(yel: 2, buz: 1);
                        Define_Run.b_LeftMagEmptyAlarming = true;
                    }
                    if (!NDispWin.TaskElev.Left.WaitMagChange && Define_Run.b_LeftMagEmptyAlarming)
                    {
                        TCTwrLight.SetStatus(TwrLight.Idle);
                        Define_Run.b_LeftMagEmptyAlarming = false;
                    }
                }
                if (NDispWin.TaskConv.RightMode == NDispWin.TaskConv.ERightMode.ElevatorZ)
                {
                    if (NDispWin.TaskElev.Right.WaitMagChange && !Define_Run.b_RightMagEmptyAlarming)
                    {
                        //IO.SetState(ERYG.DontCare, ERYG.Blink, ERYG.DontCare, EBuzzer.On);
                        TCTwrLight.SetStatus(yel: 2, buz: 1);
                        Define_Run.b_RightMagEmptyAlarming = true;
                    }
                    if (!NDispWin.TaskElev.Right.WaitMagChange && Define_Run.b_RightMagEmptyAlarming)
                    {
                        TCTwrLight.SetStatus(TwrLight.Idle);
                        Define_Run.b_RightMagEmptyAlarming = false;
                    }
                }
            }
        }

        private void tmr_DateTime_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            lbl_DateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt");
            lbl_AccessUser.Text = "[" + NUtils.UserAcc.Active.GroupName + "/" + NUtils.UserAcc.Active.UserName + "]";

            UpdateDisplay();
            UpdateStatus();
            UpdateWaitMagStatus();
        }
        private void tmr_TR_Buttons_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            if (Define_Run.PromptButtonFocus) return;

            if (Msg.Showing) return;

            if (GDefineN.BtnStartValid())
            {
                bool bStart;
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    bStart = GDefineN.DI_BtnStart;
                else
                    bStart = TaskGantry.BtnStart();

                if (bStart)
                {
                    if (btn_Start.Enabled)
                    btn_Start_Click(sender, e);
                }
            }

            if (GDefineN.BtnStopValid())
            {
                bool bStop;
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    bStop = GDefineN.DI_BtnStop;
                else
                    bStop = TaskGantry.BtnStop();

                if (!bStop)
                {
                    btn_Stop_Click(sender, e);
                }
            }
        }
        private void tmr_1s_Tick(object sender, EventArgs e)
        {
            int value = DispProg.Idle.Idling ? 1 : 0;
            if (value == 1)
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                {
                    if (!DefineSafety.DoorCheck_Disp(false)) return;
                }

                Define_Run.TR_StopRun();
                DispProg.Idle.RunPurge();
            }

            #region Progam
            lbl_Weight.Text = DispProg.Disp_Weight[0].ToString("f4") + ", " + DispProg.Disp_Weight[1].ToString("f4");
            lbl_Flowrate.Text = TaskWeight.CurrentCal[0].ToString("f4") + ", " + TaskWeight.CurrentCal[1].ToString("f4");
            lbl_HeadShotCount.Text = DispProg.Stats.DispCount[0].ToString() + ", " + DispProg.Stats.DispCount[1].ToString();

            lbl_FrameCount.Text = DispProg.Stats.BoardCount.ToString();
            int t = GDefine.GetTickCount() - DispProg.Stats.StartTime;
            lbl_Elapsed.Text = ((double)t / 60000).ToString("f1");
            #endregion

            #region Pump Info
            double DispA_BaseVol_ul = DispProg.PP_HeadA_DispBaseVol;
            double DispB_BaseVol_ul = DispProg.PP_HeadB_DispBaseVol;
            lbl_DA_DispBase.Text = DispA_BaseVol_ul.ToString(TaskDisp.VolumeDisplayDecimalPoint);
            lbl_DB_DispBase.Text = DispB_BaseVol_ul.ToString(TaskDisp.VolumeDisplayDecimalPoint);

            double DispA_DispAdj_ul = DispProg.PP_HeadA_DispVol_Adj;
            double DispB_DispAdj_ul = DispProg.PP_HeadB_DispVol_Adj;
            lbl_DA_DispAdj.Text = DispA_DispAdj_ul.ToString(TaskDisp.VolumeDisplayDecimalPoint);
            lbl_DB_DispAdj.Text = DispB_DispAdj_ul.ToString(TaskDisp.VolumeDisplayDecimalPoint);

            double DispA_VolOfst = DispProg.rt_Head1VolumeOfst;
            double DispB_VolOfst = DispProg.rt_Head2VolumeOfst;
            lbl_DA_DispOfst.Text = DispA_VolOfst.ToString(TaskDisp.VolumeDisplayDecimalPoint);
            lbl_DB_DispOfst.Text = DispB_VolOfst.ToString(TaskDisp.VolumeDisplayDecimalPoint);

            lbl_DA_BackSuckVol.Text = DispProg.PP_HeadA_BackSuckVol.ToString(TaskDisp.VolumeDisplayDecimalPoint);
            lbl_DB_BackSuckVol.Text = DispProg.PP_HeadB_BackSuckVol.ToString(TaskDisp.VolumeDisplayDecimalPoint);

            lbl_DA_DispTotal.Text = (DispProg.PP_HeadA_DispBaseVol + DispProg.PP_HeadA_DispVol_Adj + DispProg.rt_Head1VolumeOfst - DispProg.PP_HeadA_BackSuckVol).ToString(TaskDisp.VolumeDisplayDecimalPoint);
            lbl_DB_DispTotal.Text = (DispProg.PP_HeadB_DispBaseVol + DispProg.PP_HeadB_DispVol_Adj + DispProg.rt_Head2VolumeOfst - DispProg.PP_HeadB_BackSuckVol).ToString(TaskDisp.VolumeDisplayDecimalPoint);

            lblDADispCount.Text = Utils.GetKK(Maint.Unit.Count[0]) + " / " + Utils.GetKK(Maint.Unit.CountLimit[0]);
            lblDBDispCount.Text = Utils.GetKK(Maint.Unit.Count[1]) + " / " + Utils.GetKK(Maint.Unit.CountLimit[1]);
            #endregion

            if (TaskDisp.Material_ExpiryPreAlertTime > 0)
            {
                if (DateTime.Now > TaskDisp.Material_LifePreAlert_Time && TaskDisp.Material_LifePreAlert_Time != TaskDisp.Material_Life_EndTime)
                {
                    btn_Stop_Click(sender, e);

                    TaskDisp.Material_LifePreAlert_Time = TaskDisp.Material_Life_EndTime;
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes =
                    //MsgBox.Show((int)EErrCode.MATERIAL_EXPIRY_PREALERT, $"Material Expire in {TaskDisp.Material_ExpiryPreAlertTime} minutes.");
                    MsgBox.Show(Messages.MATERIAL_EXPIRY_PREALERT, $"Material Expire in {TaskDisp.Material_ExpiryPreAlertTime} minutes.");
                }
            }
        }
        private void tmr_Perf_Tick(object sender, EventArgs e)
        {
            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                #region DownTime Stopwatch
                #region MHS2
                if (
                    GDefineN.SystemSt == GDefineN.ESystemSt.Ready &&
                    (GDefine.Status == EStatus.ErrorInit || NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.ErrorInit)
                    )
                {
                    lbl_DownTime.Text = GDefineN.UpdateDownTime();
                }
                else
                {
                    if (DLLDefine.TickCountDowntTime.IsRunning) DLLDefine.TickCountDowntTime.Stop();
                }
                #endregion
                #endregion

                #region Idle Time Stopwatch
                if (
                    GDefineN.SystemSt == GDefineN.ESystemSt.Ready &&
                    !DispProg.TR_IsBusy() &&
                    (NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.Ready || NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.Stop) &&
                    !Define_Run.TR_IsRunning
                    )
                {
                    lbl_DownTime.Text = GDefineN.UpdateDownTime();
                }
                else
                {
                    if (DLLDefine.TickCountDowntTime.IsRunning) DLLDefine.TickCountDowntTime.Stop();
                }
                #endregion
            }

            if (GDefine.ConveyorType == GDefine.EConveyorType.TABLE_S320A)
            {
                #region
                if (
                    GDefineN.SystemSt != GDefineN.ESystemSt.Ready &&
                    GDefine.Status == EStatus.ErrorInit
                    )
                {
                    lbl_DownTime.Text = GDefineN.UpdateDownTime();
                }
                else
                {
                    if (DLLDefine.TickCountDowntTime.IsRunning) DLLDefine.TickCountDowntTime.Stop();
                }
                if (GDefineN.SystemSt == GDefineN.ESystemSt.Ready && !DispProg.TR_IsBusy())
                {
                    lbl_IdleTime.Text = GDefineN.UpdateIdleTime();
                }
                else
                {
                    if (DLLDefine.TickCountIdleTime.IsRunning)
                    {
                        DLLDefine.TickCountIdleTime.Stop();
                    }
                }
                #endregion
            }

            if (Define_Run.TR_IsRunning)
            {
                lbl_RunTime.Text = GDefineN.UpdateRunTime(false);
                if (Msg.Showing)
                {
                    lbl_MTTA.Text = GDefineN.UpdateMTTA();
                }
                else
                {
                    if (DLLDefine.TickCountMTTATime.IsRunning)
                    {
                        DLLDefine.TickCountMTTATime.Stop();
                    }
                }
            }
            else
            {
                if (DLLDefine.TickCountMTTATime.IsRunning)
                {
                    DLLDefine.TickCountMTTATime.Stop();
                }
            }
        }

        private void btn_SysInfoReset_Click(object sender, EventArgs e)
        {
            Msg MsgBox = new Msg();
            if (MsgBox.Show(Messages.RESET_PERF_INFO, "", EMsgBtn.smbOK_Cancel) == EMsgRes.smrCancel) return;

            GDefineN.PerformanceReset();
        }
        private void btnResetProgram_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Reset Shots, FrameCount and Runtime?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                DispProg.Stats.Reset();
            }
        }

        #region Status Bar
        private void tsslbl_BuzzerMute_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetMute();//IO.SetState(EMcState.Mute);
        }
        private void tsddbtn_Language_Click(object sender, EventArgs e)
        {
            if (AppLanguage.Func2.SelectedLang == GDefineN.Language1)
                AppLanguage.Func2.SelectedLang = GDefineN.Language2;
            else
                AppLanguage.Func2.SelectedLang = GDefineN.Language1;
            AppLanguage.Func2.UpdateText(this);

            AppLanguage.Func2.UpdateText(frm_Map);

            AppLanguage.Func2.UpdateText(frm_DispTool);

            AppLanguage.Func2.UpdateText(InfoPanel_Map);
            AppLanguage.Func2.UpdateText(InfoPanel_VolAdjust);
        }

        private void tsslbl_LULDoor_Click(object sender, EventArgs e)
        {
            if (NDispWin.TaskElev.Left.BypassDoorCheckTimer > 0)
            {
                NDispWin.TaskElev.Left.BypassDoorCheckTimer = 0;
                NDispWin.TaskElev.Right.BypassDoorCheckTimer = 0;
            }
            else
            {
                NDispWin.TaskElev.Left.BypassDoorCheckTimer = 20;
                NDispWin.TaskElev.Right.BypassDoorCheckTimer = 20;
            }
        }
        private void tsslblDoorLock_Click(object sender, EventArgs e)
        {
            if (!DispProg.TR_IsBusy())
                DefineSafety.DoorLock = false;
        }

        frmMonCamera frmMonCamera = null;
        private void tsslMonCamera_Click(object sender, EventArgs e)
        {
            if (frmMonCamera != null && frmMonCamera.Visible) return;

            frmMonCamera = new frmMonCamera();
            frmMonCamera.TopLevel = false;
            frmMonCamera.Parent = this;
            frmMonCamera.BringToFront();
            frmMonCamera.TopMost = true;
            frmMonCamera.Show();
        }
        #endregion

        #region Manual
        private void btnNewDispense_Click(object sender, EventArgs e)
        {
            if (DispProg.LastLine <= 0) return;

            Msg MsgBox = new Msg();
            EMsgRes MsgRes = MsgBox.Show(Messages.S320_NEW_DISPENSE, msgBtn: EMsgBtn.smbOK_Cancel);
            switch (MsgRes)
            {
                case EMsgRes.smrOK:
                    {
                        DispProg.TR_Cancel();
                    }
                    break;
            }
        }
        private void btnUnload_Click(object sender, EventArgs e)
        {
            switch (GDefine.Table.StationStatus)
            {
                case GDefine.Table.TStStatus.stLoaded:
                case GDefine.Table.TStStatus.stInProcess:
                    GDefine.Table.StationStatus = GDefine.Table.TStStatus.stCompleted;
                    Define_Run.RunDispTable();
                    break;
            }
        }
        #endregion

        #region Functions
        private void btn_ContinuePreviousMap_Click(object sender, EventArgs e)
        {
            Define_Run.ResumeMap = !Define_Run.ResumeMap;
        }
        private void btn_LotInfo_Click(object sender, EventArgs e)
        {
            switch (LotInfo2.Customer)
            {
                case LotInfo2.ECustomer.OsramEMos:
                    {
                        frm_LotEntryOsramEMos frm = new frm_LotEntryOsramEMos();
                        frm.ShowDialog();
                        break;
                    }
                case LotInfo2.ECustomer.OsramICC:
                    {
                        frm_LotEntryOsramICC frm = new frm_LotEntryOsramICC();
                        frm.ShowDialog();
                        break;
                    }
                default:
                    {
                        Msg MsgBox = new Msg();
                        MsgBox.Show("Lot Entry mode not supported. Please contact NSW representative.");
                        break;
                    }
            }
        
            if (LotInfo2.LoadRecipe)
            {
                switch (LotInfo2.Customer)
                {
                    case LotInfo2.ECustomer.OsramEMos:
                        {
                            Enabled = false;

                            GDefineN.PerformanceReset();
                            DispProg.Stats.Reset();

                            btn_LotInfo.Text = "Loading Recipe";
                            btn_LotInfo.ForeColor = Color.Lime;
                            LotInfo2.LoadRecipe = false;

                            string RecipeName = LotInfo2._SProgramRecipe;
                            if (RecipeName.Length == 0)
                            {
                                Event.OP_DISP_AUTO_LOAD_DEVICE_INVALID.Set();
                                Msg MsgBox = new Msg();
                                MsgBox.Show("Auto Load - Invalid Recipe.");
                            }
                            else
                            if (RecipeName.Length >= 0)
                            {
                                string Filename = GDefine.DevicePath + RecipeName + "." + GDefine.DeviceRecipeExt;

                                if (!File.Exists(Filename))
                                {
                                    Event.OP_DISP_AUTO_LOAD_DEVICE_NO_FOUND.Set("Name", Filename);
                                    Msg MsgBox = new Msg();
                                    MsgBox.Show("Auto Load - Recipe not found.");
                                }
                                else
                                {
                                    GDefine.DeviceRecipe = RecipeName;
                                    GDefine.LoadDevice(GDefine.DeviceRecipe);
                                }
                            }

                            if (LotInfo2.MatLife > 0)
                            {
                                TaskDisp.Material_EnableTimer = LotInfo2.MatLife > 0;
                                if (LotInfo2.MatLife > 0)
                                {
                                    TaskDisp.Material_Life_EndTime = DateTime.Now.AddMinutes(LotInfo2.MatLife);
                                    TaskDisp.Material_LifePreAlert_Time = TaskDisp.Material_Life_EndTime.AddMinutes((double)-TaskDisp.Material_ExpiryPreAlertTime);
                                }
                            }

                            btn_LotInfo.Text = "Lot Info";
                            btn_LotInfo.ForeColor = Color.Blue;

                            Enabled = true;
                        }
                        break;
                }
            }

            switch (LotInfo2.LotEvent)
            {
                case LotInfo2.ELotEvent.None: break;
                case LotInfo2.ELotEvent.LotStart:
                    DispProg.Stats.BoardCount = 0;
                    if (TaskDisp.TeachNeedle_PromptOnLotStart) TaskDisp.TeachNeedle_Completed = false;
                    if (TaskWeight.Cal_RequireOnLotStart) TaskWeight.Cal_Status = TaskWeight.EWeightCalStatus.Require;
                    if (TaskWeight.Meas_RequireOnLotStart) TaskWeight.Meas_Status = TaskWeight.EWeightMeasStatus.Require;
                    break;
                case LotInfo2.ELotEvent.LotRestart:
                    break;
                case LotInfo2.ELotEvent.LotEnd:
                    break;
            }
        }
        private void btn_InitConveyor_Click(object sender, EventArgs e)
        {
            Define_Run.TR_StopRun();

            tmr_TR_Buttons.Enabled = false;
            Enabled = false;

            Define_Run.InitConv(true);

            Enabled = true;
            tmr_TR_Buttons.Enabled = true;
        }
        #endregion

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                if (bBurnRun)
                {
                    AutoRun_BurnRun();
                }
                else
                {
                    Define_Run.TR_StartRun();
                    AutoRun();
                }
            }
            else
            {
                AutoRun_ManualLoad();
            }
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                if (!DispProg.SetupMode && TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC)
                {
                    TaskDisp.OsramSCC.SendDMStop();
                }
                Define_Run.TR_StopRun();
            }
            else
            {
                Define_Run.StopDispTable();
            }
        }
        private void btn_Back_Click(object sender, EventArgs e)
        {
            Close();
        }

        TaskStatus convStatus;
        TaskStatus dispStatus;
        private async void AutoRun()
        {
            EnableControl(false);

            var taskGeneral = Task.Run(() =>//Check StopRun conditions
            {
                while (Define_Run.TR_IsRunning)
                {
                    if (!DefineSafety.DoorCheck_All(false))
                    {
                        GDefine.Status = EStatus.Stop;
                        Define_Run.TR_StopRun();
                        DefineSafety.DoorCheck_All(true);
                    }

                    if (GDefineN.LowPressureValid() && !GDefineN.DI_InPressureInRange)
                    {
                        GDefine.Status = EStatus.Stop;
                        Define_Run.TR_StopRun();
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.LOW_AIR_PRESSURE);
                    }

                    if (TaskConv.Pro.Status == TaskConv.EProcessStatus.InProcess &&
                      TaskConv.Pro.UseVac &&
                      !TaskConv.Pro.SensVac)
                    {
                        GDefine.Status = EStatus.Stop;
                        Define_Run.TR_StopRun();
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.CONV_VACUUM_LOW);
                    }

                    if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                    {
                        if (TaskElev.Left.WaitMagChange)
                        {
                            if (TaskConv.Pre.Status == TaskConv.EProcessStatus.Empty &&
                                TaskConv.Pro.Status == TaskConv.EProcessStatus.Empty &&
                                TaskConv.Pos.Status == TaskConv.EProcessStatus.Empty)
                            {
                                GDefine.Status = EStatus.Stop;
                                Define_Run.TR_StopRun();
                                //Send LotEnd Here
                                Msg MsgBox = new Msg();
                                MsgBox.Show(Messages.LOT_COMPLETE_IN_MAGAZINE_EMPTY);
                            }
                        }
                    }

                    Thread.Sleep(1000);
                }
            });

            var taskConv = Task.Run(() =>
            {
                while (Define_Run.TR_IsRunning)
                {
                    try
                    {
                        if (
                            (NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.Stop) ||
                            (NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.ErrorInit) ||
                            (NDispWin.TaskConv.LeftMode == NDispWin.TaskConv.ELeftMode.ElevatorZ && NDispWin.TaskElev.Left.Status == NDispWin.TaskElev.EElevStatus.ErrorInit) ||
                            (NDispWin.TaskConv.RightMode == NDispWin.TaskConv.ERightMode.ElevatorZ && NDispWin.TaskElev.Right.Status == NDispWin.TaskElev.EElevStatus.ErrorInit)
                            )
                        {
                            Define_Run.TR_StopRun();
                            return;
                        }

                        TaskConv.Run();
                        Thread.Sleep(500);
                    }
                    catch
                    {
                        Event.DEBUG_INFO.Set("TaskConv.Run", "Exception");
                        Define_Run.TR_StopRun();
                    }
                }
            });

            var taskDisp = Task.Run(() =>
            {
                while (Define_Run.TR_IsRunning)
                {
                    try
                    {
                        Define_Run.RunDispConv();
                        Thread.Sleep(100);
                    }
                    catch
                    {
                        Event.DEBUG_INFO.Set("TaskDisp.Run", "Exception");
                        Define_Run.TR_StopRun();
                    }
                }
            });

            await Task.Run(() =>
            {
                Task.WaitAll(taskConv, taskDisp);
                convStatus = taskConv.Status;
                dispStatus = taskDisp.Status;
            });

            TaskConv.In.Smema_DO_McReady = false;
            TaskConv.Out.Smema_DO_BdReady = false;

            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                TCTwrLight.SetStatus(TwrLight.Idle);
            }
            DefineSafety.DoorLock = false;

            EnableControl(true);
        }
        private async void AutoRun_ManualLoad()
        {
            if (!DefineSafety.DoorCheck_All(true)) return;

            Define_Run.TableIsRunning = true;

            EnableControl(false);

            TCTwrLight.SetStatus(TwrLight.Run);

            bool keepTLState = false;
            var taskGeneral = Task.Run(() =>//Check StopRun conditions
            {
                while (Define_Run.TableIsRunning)
                {
                    if (!DefineSafety.DoorCheck_All(false))
                    {
                        Define_Run.StopDispTable();
                        keepTLState = true;
                        DefineSafety.DoorCheck_All(true);
                    }
                    Thread.Sleep(1000);
                }
            });

            await Task.Run(() =>
            {
                while (true)// Define_Run.TableIsRunning)//Define_Run.TR_IsRunning)
                {
                    if (!Define_Run.RunDispTable()) return;
                    Thread.Sleep(10);
                }
            });

            TCTwrLight.SetStatus(TwrLight.Idle);

            DefineSafety.DoorLock = false;

            EnableControl(true);
        }

        private async void AutoRun_BurnRun()
        {
            EnableControl(false);

            var taskGeneral = Task.Run(() =>//Check StopRun conditions
            {
                while (Define_Run.TR_IsRunning)
                {
                    if (!DefineSafety.DoorCheck_All(false))
                    {
                        GDefine.Status = EStatus.Stop;
                        Define_Run.TR_StopRun();
                        DefineSafety.DoorCheck_All(true);
                    }
                    Thread.Sleep(1000);
                }
            });

            var taskConv = Task.Run(() =>
            {
                while (Define_Run.TR_IsRunning)
                {
                    try
                    {
                        if (
                            (NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.Stop) ||
                            (NDispWin.TaskConv.Status == NDispWin.TaskConv.EConvStatus.ErrorInit) ||
                            (NDispWin.TaskConv.LeftMode == NDispWin.TaskConv.ELeftMode.ElevatorZ && NDispWin.TaskElev.Left.Status == NDispWin.TaskElev.EElevStatus.ErrorInit) ||
                            (NDispWin.TaskConv.RightMode == NDispWin.TaskConv.ERightMode.ElevatorZ && NDispWin.TaskElev.Right.Status == NDispWin.TaskElev.EElevStatus.ErrorInit)
                            )
                        {
                            Define_Run.TR_StopRun();
                            return;
                        }

                        TaskConv.Run();
                        Thread.Sleep(500);
                    }
                    catch
                    {
                        Event.DEBUG_INFO.Set("TaskConv.BurnRun", "Exception");
                        Define_Run.TR_StopRun();
                    }
                }
            });

            var taskDisp = Task.Run(() =>
            {
                while (Define_Run.TR_IsRunning)
                {
                    try
                    {
                        Define_Run.RunDispConv_BurnRun();
                        Thread.Sleep(100);
                    }
                    catch
                    {
                        Event.DEBUG_INFO.Set("TaskDisp.BurnRun", "Exception");
                        Define_Run.TR_StopRun();
                    }
                }
            });

            await Task.Run(() => Task.WaitAll(taskConv, taskDisp));

            TaskConv.In.Smema_DO_McReady = false;
            TaskConv.Out.Smema_DO_BdReady = false;

            if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
            {
                TCTwrLight.SetStatus(TwrLight.Idle);
            }
            DefineSafety.DoorLock = false;

            EnableControl(true);
        }

        bool bBurnRun = false;
        private void btnBurnRun_Click(object sender, EventArgs e)
        {
            bBurnRun = !bBurnRun;
        }
    }
}
