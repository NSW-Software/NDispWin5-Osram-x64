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

namespace NDispWin
{
    public partial class frm_DispCore_DispTools : Form
    {
        frm_DispCore_DispSetup_PP frm_PP = new frm_DispCore_DispSetup_PP();
        frm_DispCore_DispSetup_HM frm_HM = new frm_DispCore_DispSetup_HM();
        frm_Setup_PJ frm_PJ = new frm_Setup_PJ();

        public frm_DispCore_DispTools()
        {
            InitializeComponent();
            GControl.LogForm(this);

            StartPosition = FormStartPosition.CenterScreen;

            Label Txt_MaterialTimer = new Label(); Txt_MaterialTimer.AccessibleDescription = "Material Timer"; Txt_MaterialTimer.Visible = false; this.Controls.Add(Txt_MaterialTimer);

            pnl_OsramSCC.Location = new Point(4, 4);
            pnl_OsramSCC.Visible = (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC);

            frm_PP.Visible = false;
            if (DispProg.Pump_Type == TaskDisp.EPumpType.PP || DispProg.Pump_Type == TaskDisp.EPumpType.PP2D)
            {
                switch (GDefine.DispCtrlType[0])
                {
                    case GDefine.EDispCtrlType.HPC3:
                        frmSetupPP_HPC3 frm = new frmSetupPP_HPC3();
                        frm.TopLevel = false;
                        frm.Parent = pnl_DispTool_PumpTool;
                        frm.FormBorderStyle = FormBorderStyle.None;
                        frm.AutoSize = false;
                        frm.Dock = DockStyle.Fill;
                        frm.BringToFront();
                        frm.ToolsOnly();
                        frm.Show();
                        break;
                    default:
                        frm_PP.TopLevel = false;
                        frm_PP.Parent = pnl_DispTool_PumpTool;
                        frm_PP.FormBorderStyle = FormBorderStyle.None;
                        frm_PP.AutoSize = false;
                        frm_PP.Dock = DockStyle.Fill;
                        frm_PP.BringToFront();
                        frm_PP.Show();
                        break;
                }
            }
        }

        private void EnableControls(bool Enable, Button Button1, Button Button2)
        {
            for (int i = 0; i <= Controls.Count - 1; i++)
            {
                if (Controls[i] is Button)
                {
                    (Controls[i] as Button).Enabled = Enable;
                }

                if (Controls[i] is GroupBox || Controls[i] is Panel || Controls[i] is TabControl)
                {
                    for (int j = 0; j <= Controls[i].Controls.Count - 1; j++)
                    {
                        if (Controls[i].Controls[j] is TabPage)
                        {
                            for (int k = 0; k <= Controls[i].Controls[j].Controls.Count - 1; k++)
                            {
                                if (Controls[i].Controls[j].Controls[k] is Button)
                                {
                                    (Controls[i].Controls[j].Controls[k] as Button).Enabled = Enable;
                                }
                            }
                        }

                        if (Controls[i].Controls[j] is Button)
                        {
                            (Controls[i].Controls[j] as Button).Enabled = Enable;
                        }
                        if (Controls[i].Controls[j] is Label)
                        {
                            (Controls[i].Controls[j] as Label).Enabled = Enable;
                        }
                    }
                }
            }
            Button1.Enabled = true;
            Button2.Enabled = true;
        }
        private void EnableControls(bool Enable)
        {
            EnableControls(Enable, btn_Close, btn_Close);
        }
        private void EnableControls()
        {
            EnableControls(true, btn_Close, btn_Close);
        }

        private void UpdateDisplay()
        {
            if (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC)
            {
                #region
                lbl_LotID.Text = "LotID" + (char)13 + TaskDisp.OsramSCC.LotID;
                if (TaskDisp.OsramSCC.Enabled)
                {
                    if (TaskDisp.OsramSCC.LotID.Length > 0)
                    {
                        lbl_LotID.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_LotID.BackColor = this.BackColor;
                    }
                }
                lbl_Program.Text = "Recipe" + (char)13 + GDefine.ProgRecipeName;
                if (TaskDisp.OsramSCC.PreMapNo > 0) lbl_Program.Text = lbl_Program.Text + $"_{TaskDisp.OsramSCC.PreMapNo} ";
                lbl_Program.Text = lbl_Program.Text + (char)13 + DispProg.Target_Weight.ToString("f3") + " (mg)";
                lbl_OsramSCC.Text = "OsramSCC" + (char)13 + TaskDisp.OsramSCC.Client.IPAddress + ":" + TaskDisp.OsramSCC.Client.Port.ToString("d4");
                if (TaskDisp.OsramSCC.Enabled)
                {
                    if (TaskDisp.OsramSCC.Client_Connected)
                    {
                        lbl_OsramSCC.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_OsramSCC.BackColor = Color.Red;
                    }
                }
                else
                {
                    lbl_OsramSCC.Text = "OsramSCC" + (char)13 + " Disabled";
                    lbl_OsramSCC.BackColor = Color.Orange;
                }

                btn_OsramSCC.Text = "OsramSCC" + (char)13 + DispProg.Disp_Weight[0].ToString("f3") + "," + DispProg.Disp_Weight[1].ToString("f3") + " (mg)";
                #endregion
            }

            btn_ForceSingle.Visible = TaskDisp.Option_EnableRunSingleHead && GDefine.HeadConfig == GDefine.EHeadConfig.Dual;
            UI_Utils.SetControlSelected(btn_ForceSingle, TaskDisp.ForceSingle);

            btn_DrawOfstAdjust.Visible = TaskDisp.Option_EnableDrawOfstAdjust;
            lbl_OrignOfst.Visible = TaskDisp.Option_EnableDrawOfstAdjust;
            //lbl_OrignOfst.Text = "(" + DispProg.OriginOfst[(int)DispProg.rt_StationNo].X.ToString("F3") + ", " +
            //                       DispProg.OriginOfst[(int)DispProg.rt_StationNo].Y.ToString("F3") + ", " +
            //                       DispProg.OriginOfst[(int)DispProg.rt_StationNo].Z.ToString("F3") + ")";
            lbl_OrignOfst.Text = "(" + DispProg.OriginDrawOfst.GetString + ")";
            if (DispProg.OriginDrawOfst.X != 0 || DispProg.OriginDrawOfst.Y != 0 || DispProg.OriginDrawOfst.Z != 0)
                lbl_OrignOfst.ForeColor = Color.Orange;
            else
                lbl_OrignOfst.ForeColor = Color.Navy;

            btn_Weight.Visible = ((int)GDefine.WeightStType > 0);

            btn_DispWeight.Visible = (TaskDisp.WeightProgramName[0].Length > 0 && File.Exists(GDefine.ProgPath + "\\" + TaskDisp.WeightProgramName[0] + "." + GDefine.ProgExt));
            btn_DispWeight.Text = TaskDisp.WeightProgramName[0] + " [" + TaskDisp.WeightProgramHead[0].ToString() + "]";
            btn_DispWeight2.Visible = (TaskDisp.WeightProgramName[1].Length > 0 && File.Exists(GDefine.ProgPath + "\\" + TaskDisp.WeightProgramName[1] + "." + GDefine.ProgExt));
            btn_DispWeight2.Text = TaskDisp.WeightProgramName[1] + " [" + TaskDisp.WeightProgramHead[1].ToString() + "]";

            lbl_MaterialTimer.Visible = TaskDisp.Material_EnableTimer;
            lbl_MaterialExpiryDT.Visible = TaskDisp.Material_EnableTimer;
            if (TaskDisp.Material_EnableTimer)
            {
                string s_MaterialTimer = AppLanguage.Func2.GetText(this, "Label", "Material Timer") + (char)13;
                int i_LifeTimer_s = (int)TaskDisp.Material_Life_EndTime.Subtract(DateTime.Now).TotalSeconds;
                if (i_LifeTimer_s > 0)
                {
                    if (i_LifeTimer_s <= 0)
                    {
                        lbl_MaterialTimer.Text = s_MaterialTimer + "Expired";
                    }
                    else
                    {
                        int i_LifeTimer_m = i_LifeTimer_s / 60;
                        int i_LifeTimer_h = i_LifeTimer_m / 60;

                        lbl_MaterialTimer.Text = s_MaterialTimer +
                        i_LifeTimer_h.ToString() + " H " +
                        (i_LifeTimer_m % 60).ToString() + " M " +
                        (i_LifeTimer_s % 60).ToString() + " S";
                    }

                    lbl_MaterialExpiryDT.Text = "Material Expiry" + (char)13 + TaskDisp.Material_Life_EndTime.ToString("yyyy/MM/dd HH:mm");
                    lbl_MaterialExpiryDT.BackColor = this.BackColor;
                }
                else
                {
                    lbl_MaterialTimer.Text = s_MaterialTimer + "0";
                    lbl_MaterialExpiryDT.Text = "Material Expired";
                    lbl_MaterialExpiryDT.BackColor = Color.Red;
                }
            }

            lblValveTimer.Visible = TaskDisp.Valve_EnableTimer;
            lblValveExpiryDT.Visible = TaskDisp.Valve_EnableTimer;
            if (TaskDisp.Valve_EnableTimer)
            {
                string s_MaterialTimer = AppLanguage.Func2.GetText(this, "Label", "Valve Timer") + (char)13;
                int i_LifeTimer_s = (int)TaskDisp.Valve_Life_EndTime.Subtract(DateTime.Now).TotalSeconds;
                if (i_LifeTimer_s > 0)
                {
                    if (i_LifeTimer_s <= 0)
                    {
                        lblValveTimer.Text = s_MaterialTimer + "Expired";
                    }
                    else
                    {
                        int i_LifeTimer_m = i_LifeTimer_s / 60;
                        int i_LifeTimer_h = i_LifeTimer_m / 60;

                        lblValveTimer.Text = s_MaterialTimer +
                        i_LifeTimer_h.ToString() + " H " +
                        (i_LifeTimer_m % 60).ToString() + " M " +
                        (i_LifeTimer_s % 60).ToString() + " S";
                    }

                    lblValveExpiryDT.Text = "Valve Expiry" + (char)13 + TaskDisp.Valve_Life_EndTime.ToString("yyyy/MM/dd HH:mm");
                    lblValveExpiryDT.BackColor = this.BackColor;
                }
                else
                {
                    lblValveTimer.Text = s_MaterialTimer + "0";
                    lblValveExpiryDT.Text = "Valve Expired";
                    lblValveExpiryDT.BackColor = Color.Red;
                }
            }

            lbl_SensMat1Low.Visible = TaskDisp.Option_EnableMaterialLow;
            lbl_SensMat2Low.Visible = TaskDisp.Option_EnableMaterialLow;

            btnMaterialChange.Visible = Material.EnableUnitCounter;

                    btn_StartIdle.Visible = TaskDisp.Option_EnableStartIdle;
            btn_PumpAction.Visible = Pump.Action.Enabled;
            btn_PurgeStage.Visible = (DispProg.PurgeStage.Count > 0);

            //if (DispProg.Pump_Type == TaskDisp.EPumpType.PP || DispProg.Pump_Type == TaskDisp.EPumpType.PP2D)
            //{
            //    frm_PP.TopLevel = false;
            //    frm_PP.Parent = pnl_DispTool_PumpTool;
            //    frm_PP.FormBorderStyle = FormBorderStyle.None;
            //    frm_PP.AutoSize = false;
            //    frm_PP.Dock = DockStyle.Fill;
            //    frm_PP.BringToFront();
            //    frm_PP.Show();
            //}
            //else
            //    frm_PP.Visible = false;

            if (DispProg.Pump_Type == TaskDisp.EPumpType.HM)
            {
                frm_HM.TopLevel = false;
                frm_HM.Parent = pnl_DispTool_PumpTool;
                frm_HM.FormBorderStyle = FormBorderStyle.None;
                frm_HM.AutoSize = false;
                frm_HM.Dock = DockStyle.Fill;
                frm_HM.BringToFront();
                frm_HM.Show();
            }
            else
                frm_HM.Visible = false;

            if (DispProg.Pump_Type == TaskDisp.EPumpType.PJ)
            {
                frm_PJ.TopLevel = false;
                frm_PJ.Parent = pnl_DispTool_PumpTool;
                frm_PJ.FormBorderStyle = FormBorderStyle.None;
                frm_PJ.AutoSize = false;
                frm_PJ.Dock = DockStyle.Fill;
                frm_PJ.BringToFront();
                frm_PJ.Show();
            }
            else
                frm_PJ.Visible = false;

            btnMonCamera.Visible = GDefine.MCameraType[0] == GDefine.ECameraType.MVSGenTL || GDefine.MCameraType[1] == GDefine.ECameraType.MVSGenTL;
        }

        private void frmDispTools_Load(object sender, EventArgs e)
        {
            Text = "Dispense Tools";

            AppLanguage.Func2.UpdateText(this);

            btn_Close.Visible = this.Modal;// AllowClose;

            UpdateDisplay();
        }

        private void frm_DispCore_DispTools_Shown(object sender, EventArgs e)
        {
        }
        private void frm_DispCore_DispTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker)
            {
                try
                {
                    //TaskVision.frmGenImageView.GrabStop();
                    //TaskVision.frmGenImageView.Hide();
                    //TaskVision.frmGenImageView.EnableCamReticles = true;
                }
                catch { }
            }
        }

        private void frmDispTools_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        public void SetEnable(bool Enable)
        {
            EnableControls(Enable);
            pnl_DispTool_PumpTool.Enabled = Enable;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            TaskDisp.TaskMoveGZZ2Up();
            Close();
        }

        bool b_ConnectOnce = true;
        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            if (b_ConnectOnce)
            {
                if (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC)
                {
                    try
                    {
                        //TaskDisp.OsramSCC.Client_Connect(TaskDisp.OsramSCC.Client.IPAddress, TaskDisp.OsramSCC.Client.Port);
                    }
                    catch { };
                }
                b_ConnectOnce = false;
            }

            UpdateDisplay();
        }
        private void tmr_5s_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            try
            {
                GDefine.RefreshInput(lbl_SensMat1Low, TaskGantry.SensMat1Low());
                GDefine.RefreshInput(lbl_SensMat2Low, TaskGantry.SensMat2Low());
            }
            catch { };
        }


        private void lbl_Connection_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TaskDisp.OsramSCC.Client_Connected)
                {
                    TaskDisp.OsramSCC.Client_Connect();
                }
                if (!TaskDisp.OsramSCC.Server_Listening)
                {
                    TaskDisp.OsramSCC.Server_Listen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btn_LotInfo_Click(object sender, EventArgs e)
        {
            frm_OsramSCC_LotInfo frm = new frm_OsramSCC_LotInfo();

            frm.ShowDialog();
        }
        private void lbl_LotID_Click(object sender, EventArgs e)
        {

        }


        private void btn_ForceSingle_Click(object sender, EventArgs e)
        {
            TaskDisp.ForceSingle = !TaskDisp.ForceSingle;
            if (TaskDisp.ForceSingle)
                TaskDisp.Head_Operation = TaskDisp.EHeadOperation.Single;
            else
                TaskDisp.Head_Operation = DispProg.Head_Operation;

            DispProg.rt_PromptedSingleHeadRun = false;

            Event.DISPTOOLS_FORCE_SINGLE.Set("ForceSingle", TaskDisp.ForceSingle.ToString());
            UpdateDisplay();
        }
        private void btn_Origin_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            Event.DISPTOOLS_ORIGIN.Set();

            try
            {
                if (!TaskDisp.TaskMoveGZZ2Up()) return;
                if (!TaskGantry.SetMotionParamGXYX2Y2()) return;

                TPos2 GXY = new TPos2(DispProg.Origin(DispProg.rt_StationNo).X, DispProg.Origin(DispProg.rt_StationNo).Y);
                TPos2 GX2Y2 = new TPos2(TaskDisp.Head2_DefPos.X, TaskDisp.Head2_DefPos.Y);
                GX2Y2.X = GX2Y2.X - TaskDisp.Head2_DefDistX + TaskDisp.Head2_DefDistX;

                if (!TaskDisp.GotoXYPos(GXY, GX2Y2)) return;

                TaskVision.LightingOn(TaskVision.DefLightRGB);

                frm_DispProg_View frm = new frm_DispProg_View();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DispProg.RunMode = ERunMode.Camera;
                    DispProg.OriginBase[(int)DispProg.rt_StationNo].X = TaskGantry.GXPos();
                    DispProg.OriginBase[(int)DispProg.rt_StationNo].Y = TaskGantry.GYPos();
                }
            }
            catch { };
        }
        frm_DispCore_DrawOfstAdjust frm_OriginAdjust = new frm_DispCore_DrawOfstAdjust();
        private void btn_OriginAdjust_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_ORIGIN_ADJUST.Set();

            frm_OriginAdjust.TopLevel = false;
            frm_OriginAdjust.Parent = this;
            frm_OriginAdjust.BringToFront();
            frm_OriginAdjust.Show();
        }
        private void lbl_OriginOfst_Click(object sender, EventArgs e)
        {
            btn_OriginAdjust_Click(sender, e);
        }
        private void lbl_MaterialTimer_Click(object sender, EventArgs e)
        {
            int i = 0;

            if (UC.AdjustExec("Material Life (Minute x " + TaskDisp.Material_Life_Multiplier.ToString() + ")", ref i, 0, 720))
            {
                if (i == 0)
                {
                    TaskDisp.Material_Life_EndTime = DateTime.Now;
                }
                else
                {
                    TaskDisp.Material_Life_EndTime = DateTime.Now.AddSeconds(i * 60 * TaskDisp.Material_Life_Multiplier);
                }
                TaskDisp.Material_LifePreAlert_Time = TaskDisp.Material_Life_EndTime.AddMinutes((double)-TaskDisp.Material_ExpiryPreAlertTime);

                Event.DISPTOOLS_ADJ_MATERIAL_TIMER.Set("Timer", i.ToString());
            }

            UpdateDisplay();
        }
        private void lblValveTimer_Click(object sender, EventArgs e)
        {
            int i = 0;

            if (UC.AdjustExec("Valve Life (Minute x " + TaskDisp.Material_Life_Multiplier.ToString() + ")", ref i, 0, 720))
            {
                if (i == 0)
                {
                    TaskDisp.Valve_Life_EndTime = DateTime.Now;
                }
                else
                {
                    TaskDisp.Valve_Life_EndTime = DateTime.Now.AddSeconds(i * 60 * TaskDisp.Material_Life_Multiplier);
                }
                Event.DISPTOOLS_ADJ_MATERIAL_TIMER.Set("Timer", i.ToString());
            }

            UpdateDisplay();
        }

        private void dtp_ExpiryTime_ValueChanged(object sender, EventArgs e)
        {

        }
        private void dtp_ScanEntry_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string S = dtp_ScanEntry.Text;
                try
                {
                    int YYYY = Convert.ToInt32(S.Remove(4));
                    int MM = Convert.ToInt32(S.Remove(0, 4).Remove(2));
                    int DD = Convert.ToInt32(S.Remove(0, 6).Remove(2));
                    int hh = Convert.ToInt32(S.Remove(0, 8).Remove(2));
                    int mm = 0;
                    if (S.Length > 12) mm = Convert.ToInt32(S.Remove(0, 10).Remove(2));
                    else mm = Convert.ToInt32(S.Remove(0, 10));

                    dtp_ExpiryDate.Value = new DateTime(YYYY, MM, DD);
                    dtp_ExpiryTime.Value = new DateTime(YYYY, MM, DD, hh, mm, 0, DateTimeKind.Unspecified);

                    btn_dtpOK_Click(sender, e);
                }
                catch
                {
                    dtp_ScanEntry.Text = "";
                };

                UpdateDisplay();
            }
        }
        private void dtp_ScanEntry_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void dtp_ScanEntry_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void dtp_ScanEntry_Layout(object sender, LayoutEventArgs e)
        {

        }
        private void dtp_ScanEntry_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void dtp_ExpiryDate_ValueChanged(object sender, EventArgs e)
        {

        }
        private void lbl_MaterialExpiryDT_Click(object sender, EventArgs e)
        {
            gbox_DateTime.Location = lbl_MaterialExpiryDT.Location;
            gbox_DateTime.BringToFront();

            EnableControls(false, btn_Close, btn_dtpOK);
            btn_dtpCancel.Enable(true);

            gbox_DateTime.Visible = true;
            dtp_ScanEntry.Text = "";
            dtp_ScanEntry.Focus();
        }
        private void btn_dtpOK_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_ADJ_MATERIAL_EXP.Set();

            TaskDisp.Material_Life_EndTime = dtp_ExpiryDate.Value.Date.Add(dtp_ExpiryTime.Value.TimeOfDay);
            TaskDisp.Material_LifePreAlert_Time = TaskDisp.Material_Life_EndTime.AddMinutes((double)-TaskDisp.Material_ExpiryPreAlertTime);
            gbox_DateTime.Visible = false;

            EnableControls();
            UpdateDisplay();
        }
        private void btn_dtpCancel_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_ADJ_MATERIAL_EXP_CANCEL.Set();

            gbox_DateTime.Visible = false;

            EnableControls();
            UpdateDisplay();
        }


        private void btn_GotoMMaint_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            Event.DISPTOOLS_GOTO_MACHINE_MAINT_POS.Set();

            DefineSafety.DoorLock = true;
            frm_DispCore_Progress frm = new frm_DispCore_Progress();
            frm.Show();
            TaskDisp.TaskGotoMMaint();
            frm.Close();
            DefineSafety.DoorLock = false;
        }
        private void btn_GotoPMaint_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            Event.DISPTOOLS_GOTO_PUMP_MAINT_POS.Set();

            DefineSafety.DoorLock = true;
            frm_DispCore_Progress frm = new frm_DispCore_Progress();
            frm.Show();
            TaskDisp.TaskGotoPMaint();
            frm.Close();
            DefineSafety.DoorLock = false;
        }
        string LastProgName = "";
        private void StartDispWeight(string ProgramName, EHeadNo HeadNo)
        {
            EnableControls(false, btn_Close, btn_Close);
            frm_DispCore_Progress frm = new frm_DispCore_Progress();
            frm.Show();

            if (!File.Exists(GDefine.ProgPath + "\\" + ProgramName + "." + GDefine.ProgExt)) goto _End;

            if (!TaskGantry.CheckReadyStop()) goto _End;

            LastProgName = GDefine.ProgRecipeName;
            DispProg.Load(GDefine.ProgPath + "\\" + ProgramName, false);
            DispProg.ModelList.SetDispVolumeDefault();

            DispProg.RunMode = ERunMode.Normal;

            if (HeadNo == EHeadNo.Head1) DispProg.b_ForceHead1 = true;
            if (HeadNo == EHeadNo.Head2) DispProg.b_ForceHead2 = true;

            Thread.Sleep(0);
            UpdateDisplay();
            while (DispProg.TR_IsBusy())
            {
                if (frm.Cancel) DispProg.TR_Cancel();

                Thread.Sleep(5);
            }
            Thread.Sleep(2);

            _End:
            frm.Close();
            EnableControls(true, btn_Close, btn_Close);
        }
        private void EndDispWeight()
        {
            DispProg.Load(GDefine.ProgPath + "\\" + LastProgName, false);
        }
        private void btn_DispWeight_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            StartDispWeight(TaskDisp.WeightProgramName[0], TaskDisp.WeightProgramHead[0]);
            EndDispWeight();
            UpdateDisplay();
        }
        private void btn_DispWeight2_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            StartDispWeight(TaskDisp.WeightProgramName[1], TaskDisp.WeightProgramHead[1]);
            EndDispWeight();
            UpdateDisplay();
        }

        private void btn_TeachNeedle_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_TEACH_NEEDLE.Set();

            //Msg MsgBox = new Msg();
            //EMsgRes Res = MsgBox.Show("Continue to Teach Needle - " + TaskDisp.TeachNeedle_Method.ToString(), EMcState.Notice, EMsgBtn.smbOK_Cancel, false);
            //switch (Res)
            //{
            //    case EMsgRes.smrCancel:
            //        {
            //            Event.DISPTOOLS_TEACH_NEEDLE_CANCEL.Set();
            //            return;
            //        }
            //}
            string Msg = "Continue to Teach Needle - " + TaskDisp.TeachNeedle_Method.ToString();
            if (MessageBox.Show(Msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            TaskDisp.DispTool_TeachNeedle();
        }
        private void btn_View_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            Event.DISPTOOLS_VIEW.Set();

            try
            {
                if (!TaskDisp.TaskMoveGZZ2Up()) return;
                if (!TaskGantry.SetMotionParamGXYX2Y2()) return;

                TPos2 GXY = new TPos2(DispProg.Origin(DispProg.rt_StationNo).X, DispProg.Origin(DispProg.rt_StationNo).Y);
                TPos2 GX2Y2 = new TPos2(TaskDisp.Head2_DefPos.X, TaskDisp.Head2_DefPos.Y);
                GX2Y2.X = GX2Y2.X - TaskDisp.Head2_DefDistX + TaskDisp.Head2_DefDistX;

                TaskVision.LightingOn(TaskVision.DefLightRGB);

                frm_DispProg_View frm = new frm_DispProg_View();
                frm.ShowSetBtn = false;
                frm.ShowCamOfstBtn = true;

                frm.ShowDialog();
            }
            catch { };
        }
        private void btn_PurgeStage_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PURGE_STAGE.Set();

            TModelPara Model = new TModelPara(DispProg.ModelList, 0);
            TaskDisp.PurgeStage.Execute(DispProg.PurgeStage.Count);
        }
        private void btn_PumpAction_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            gbox_PumpAction.Location = new Point(342, 142);

            btn_PumpAction1.Text = Pump.Action.ActionGroup[0].Name;
            btn_PumpAction2.Text = Pump.Action.ActionGroup[1].Name;
            btn_PumpAction3.Text = Pump.Action.ActionGroup[2].Name;
            btn_PumpAction4.Text = Pump.Action.ActionGroup[3].Name;
            btn_PumpAction5.Text = Pump.Action.ActionGroup[4].Name;

            btn_PumpAction1.Visible = Pump.Action.ActionGroup[0].Name.Length > 0;
            btn_PumpAction2.Visible = Pump.Action.ActionGroup[1].Name.Length > 0;
            btn_PumpAction3.Visible = Pump.Action.ActionGroup[2].Name.Length > 0;
            btn_PumpAction4.Visible = Pump.Action.ActionGroup[3].Name.Length > 0;
            btn_PumpAction5.Visible = Pump.Action.ActionGroup[4].Name.Length > 0;

            EnableControls(false, btn_Close, btn_PumpActionCancel);

            btn_PumpAction1.Enabled = true;
            btn_PumpAction2.Enabled = true;
            btn_PumpAction3.Enabled = true;
            btn_PumpAction4.Enabled = true;
            btn_PumpAction5.Enabled = true;

            gbox_PumpAction.Visible = true;
        }
        private void btn_PumpAction1_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PUMP_ACTION_1.Set();

            gbox_PumpAction.Visible = false;
            try
            {
                Pump.Action.ActionGroup[0].Execute();
            }
            catch
            { }
            finally
            {
                EnableControls();
            }
        }
        private void btn_PumpAction2_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PUMP_ACTION_2.Set();

            gbox_PumpAction.Visible = false;
            try
            {
                Pump.Action.ActionGroup[1].Execute();
            }
            catch
            { }
            finally
            {
                EnableControls();
            }
        }
        private void btn_PumpAction3_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PUMP_ACTION_3.Set();

            gbox_PumpAction.Visible = false;
            try
            {
                Pump.Action.ActionGroup[2].Execute();
            }
            catch
            { }
            finally
            {
                EnableControls();
            }
        }
        private void btn_PumpAction4_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PUMP_ACTION_4.Set();

            gbox_PumpAction.Visible = false;
            try
            {
                Pump.Action.ActionGroup[3].Execute();
            }
            catch
            { }
            finally
            {
                EnableControls();
            }
        }
        private void btn_PumpAction5_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PUMP_ACTION_5.Set();

            gbox_PumpAction.Visible = false;
            try
            {
                Pump.Action.ActionGroup[4].Execute();
            }
            catch
            { }
            finally
            {
                EnableControls();
            }
        }
        private void btn_PumpActionCancel_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PUMP_ACTION_CANCEL.Set();

            gbox_PumpAction.Visible = false;
            EnableControls();
        }


        private void btn_CleanNeedle_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            frm_DispCore_Progress frm = new frm_DispCore_Progress();
            frm.Show();
            TaskDisp.TaskCleanNeedle(true);
            frm.Close();
        }
        private void btn_PurgeNeedle_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            frm_DispCore_Progress frm = new frm_DispCore_Progress();
            frm.Show();
            TaskDisp.TaskPurgeNeedle(true);
            frm.Close();
        }
        private void btn_CPF_Click(object sender, EventArgs e)
        {
            gbox_CPF.Location = new Point(480, 4);
            gbox_CPF.BringToFront();

            EnableControls(false, btn_Close, btn_CPF_Cancel);

            btn_CPF_Clean.Enabled = true;
            btn_CPF_Purge.Enabled = true;
            btn_CPF_Flush.Enabled = true;

            gbox_CPF.Visible = true;
        }
        private void btn_CPFClean_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            Event.DISPTOOLS_CLEAN.Set();

            DefineSafety.DoorLock = true;

            try
            {
                //frm_DispCore_Progress frm = new frm_DispCore_Progress();
                //frm.Show();
                //TaskDisp.TaskCleanNeedle(true);
                //frm.Close();
                MsgBox.Processing("Task Clean Needle in progress.", () => TaskDisp.TaskCleanNeedle(true));
            }
            catch
            { }
            finally
            {
                TaskDisp.FPressOff();
                DefineSafety.DoorLock = false;
                EnableControls();
            }
            gbox_CPF.Visible = false;
        }
        private void btn_CPF_Purge_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            Event.DISPTOOLS_PURGE.Set();

            DefineSafety.DoorLock = true;

            try
            {
                //frm_DispCore_Progress frm = new frm_DispCore_Progress();
                //frm.Show();
                //TaskDisp.TaskPurgeNeedle(true);
                //frm.Close();
                MsgBox.Processing("Task Purge Needle in progress.", () => TaskDisp.TaskPurgeNeedle(true));
            }
            catch
            { }
            finally
            {
                TaskDisp.FPressOff();
                DefineSafety.DoorLock = false;
                EnableControls();
            }
            gbox_CPF.Visible = false;
        }
        private void btn_CPF_Flush_Click(object sender, EventArgs e)
        {
            if (!TaskGantry.CheckDoorSw()) return;

            Event.DISPTOOLS_FLUSH.Set();

            DefineSafety.DoorLock = true;

            try
            {
                //frm_DispCore_Progress frm = new frm_DispCore_Progress();
                //frm.Show();
                //TaskDisp.TaskFlushNeedle(true);
                //frm.Close();
                MsgBox.Processing("Task Flush Needle in progress.", () => TaskDisp.TaskFlushNeedle(true));
            }
            catch
            { }
            finally
            {
                TaskDisp.FPressOff();
                DefineSafety.DoorLock = false;
                EnableControls();
            }
            gbox_CPF.Visible = false;
        }
        private void btn_CPF_Cancel_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_CLEANPURGE_CANCEL.Set();

            gbox_CPF.Visible = false;
            EnableControls();
        }
        private void btn_StartIdle_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_START_IDLE.Set();

            frm_DispCore_IdlePurge frm = new frm_DispCore_IdlePurge();

            int i_HeadSelect = 0;

            switch (DispProg.Head_Operation)
            {
                case TaskDisp.EHeadOperation.Double:
                case TaskDisp.EHeadOperation.Sync:
                    i_HeadSelect = 3;
                    break;
                case TaskDisp.EHeadOperation.Single:
                    i_HeadSelect = 1;
                    break;
            }
            frm.i_DispSelect = i_HeadSelect;
            frm.AutoStart = true;
            frm.ShowDialog();
        }

        private void btn_Weight_Click(object sender, EventArgs e)
        {
            gbox_Weight.Location = btn_Weight.Location;
            gbox_Weight.BringToFront();

            EnableControls(false, btn_Close, btn_WeightCancel);
            btn_WeightAdjust.Enabled = true;
            btn_WeightCalibrate.Enabled = true;
            btn_WeightMeasure.Enabled = true;

            gbox_Weight.Visible = true;
        }
        private void btn_WeightAdjust_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_WEIGHT_ADJUST.Set();

            gbox_Weight.Visible = false;
            EnableControls();

            frm_DispCore_WeightAdjust frm = new frm_DispCore_WeightAdjust();
            frm.ShowDialog();
        }
        private void btn_WeightCalibrate_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_WEIGHT_CALIBRATE.Set();

            gbox_Weight.Visible = false;
            EnableControls();

            switch (DispProg.Pump_Type)
            {
                case TaskDisp.EPumpType.SP:
                case TaskDisp.EPumpType.TP:
                    frmFlowRateCal frmFR = new frmFlowRateCal();
                    frmFR.ShowDialog();
                    break;
                default:
                    frm_DispCore_WeightCal frm = new frm_DispCore_WeightCal();
                    frm.ShowDialog();
                    break;
            }
        }
        private void btn_WeightMeasure_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_WEIGHT_MEASURE.Set();

            gbox_Weight.Visible = false;
            EnableControls();

            switch (DispProg.Pump_Type)
            {
                case TaskDisp.EPumpType.SP:
                case TaskDisp.EPumpType.TP:
                    frmWeightMeasure frmMeas = new frmWeightMeasure();
                    frmMeas.ShowDialog();
                    break;
                default:
                    frm_DispCore_WeightMeasure frm = new frm_DispCore_WeightMeasure();
                    frm.ShowDialog();
                    break;
            }
        }
        private void btn_WeightCancel_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_WEIGHT_CANCEL.Set();

            gbox_Weight.Visible = false;
            EnableControls();
        }
        frm_Setup_PJ frmPJ = new frm_Setup_PJ();
        frm_Setup_SP frmSP = new frm_Setup_SP();
        private void btn_PumpAdjust_Click(object sender, EventArgs e)
        {
            Event.DISPTOOLS_PUMP_ADJUST.Set();

            switch (DispProg.Pump_Type)
            {
                case TaskDisp.EPumpType.PP:
                case TaskDisp.EPumpType.PP2D:
                case TaskDisp.EPumpType.PPD:
                    {
                        frm_DispTool_VolumeAdjust frm = new frm_DispTool_VolumeAdjust();
                        frm.AdjustUnit = frm_DispTool_VolumeAdjust.EAdjustUnit.ul;
                        frm.SettingMode = false;
                        frm.ShowDialog();
                        break;
                    }
                case TaskDisp.EPumpType.HM:
                    {
                        frm_DispTool_SpeedAdjust frm = new frm_DispTool_SpeedAdjust();
                        frm.SettingMode = false;
                        frm.ShowDialog();
                        break;
                    }
                case TaskDisp.EPumpType.PJ:
                    {
                        if (frmPJ.IsDisposed)
                        {
                            frmPJ.Close();
                            frmPJ = new frm_Setup_PJ();
                        }

                        frmPJ.Visible = true;
                        frmPJ.BringToFront();
                        frmPJ.TopMost = true;
                        break;
                    }
                case TaskDisp.EPumpType.SP:
                    {
                        if (frmSP.IsDisposed)
                        {
                            frmSP.Close();
                            frmSP = new frm_Setup_SP();
                        }
                        frmSP.Visible = true;
                        frmSP.BringToFront();
                        frmSP.TopMost = true;
                        break;
                    }
            }
        }

        private void btnMaterialChange_Click(object sender, EventArgs e)
        {
            if (Material.EnableUnitCounter)
            {
                if (MessageBox.Show("Reset Material Unit Counter?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Log.OnSet("Reset Material Unit Counter A", Material.Unit.Count[0], 0);
                    Material.Unit.Count[0] = 0;
                    Log.OnSet("Reset Material Unit Counter B", Material.Unit.Count[1], 0);
                    Material.Unit.Count[1] = 0;
                    GDefine.SaveDefault();
                }
                UpdateDisplay();
            }
        }

    }
}
