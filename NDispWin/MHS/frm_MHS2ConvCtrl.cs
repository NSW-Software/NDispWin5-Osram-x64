using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;

namespace NDispWin
{
    public partial class frm_MHS2ConvCtrl : Form
    {
        public frm_MHS2ConvCtrl()
        {
            InitializeComponent();

            UpdateUI();
        }

        private void frm_ConvCtrl_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);
        }

        public void UpdateUI()
        {
            gbox_Buf1.Visible = TaskConv.Buf1.StType == TaskConv.EBufStType.Buffer;
            gbox_Buf2.Visible = TaskConv.Buf2.StType == TaskConv.EBufStType.Buffer;
            gbox_Pre.Visible = TaskConv.Pre.StType > TaskConv.EPreStType.None;
            gbxPos.Visible = TaskConv.Pos.StType > TaskConv.EPosStType.None;

            gboxSmemaLeftIn.Visible = TaskConv.LeftMode == TaskConv.ELeftMode.Smema;
            gboxSmemaRightOut.Visible = TaskConv.RightMode == TaskConv.ERightMode.Smema;
        }

        private void UpdateDisplay()
        {
            string S = "";
            lbl_Note.Text = TaskMHS.CustomMode > 0 ? TaskMHS.CustomMode.ToString() : "*";

            S = (TaskConv.Buf1.rt_StType == TaskConv.Buf1.StType) ? "" : "*";
            S = "Buf1 (" + S + TaskConv.Buf1.rt_StType.ToString() + ")";
            gbox_Buf1.Text = S;

            S = (TaskConv.Buf2.rt_StType == TaskConv.Buf2.StType) ? "" : "*";
            S = "Buf2 (" + S + TaskConv.Buf2.rt_StType.ToString() + ")";
            gbox_Buf2.Text = S;

            S = (TaskConv.Pre.rt_StType == TaskConv.Pre.StType) ? "" : "*";
            S = "Pre (" + S + TaskConv.Pre.rt_StType.ToString() + ")";
            gbox_Pre.Text = S;

            S = (TaskConv.Pro.rt_StType == TaskConv.Pro.StType) ? "" : "*";
            S = "Pro (" + S + TaskConv.Pro.rt_StType.ToString() + ")";
            gbox_Pro.Text = S;

            S = (TaskConv.Pos.rt_StType == TaskConv.Pos.StType) ? "" : "*";
            S = "Pos (" + S + TaskConv.Pos.rt_StType.ToString() + ")";
            gbxPos.Text = S;

            lbl_ConvSt.Text = TaskConv.Status.ToString();
            lbl_ConvSt.BackColor = TaskConv.StatusColor;

            #region Process Station Status
            if (TaskConv.In.SensPsnt)
            {
                lbl_InStatus.Text = TaskConv.EProcessStatus.Psnt.ToString();
                lbl_InStatus.BackColor = Color.Lime;
            }
            else
            {
                lbl_InStatus.Text = TaskConv.EProcessStatus.Empty.ToString();
                lbl_InStatus.BackColor = Color.LightGray;
            }

            lbl_Buf1St.Text = TaskConv.Buf1.Status.ToString();
            lbl_Buf1St.BackColor = TaskConv.Buf1.StatusColor;

            lbl_Buf2St.Text = TaskConv.Buf2.Status.ToString();
            lbl_Buf2St.BackColor = TaskConv.Buf2.StatusColor;

            lbl_PreSt.Text = TaskConv.Pre.Status.ToString();
            if (TaskConv.Pre.Status == TaskConv.EProcessStatus.Heating)
                lbl_PreSt.Text = TaskConv.Pre.Status.ToString() + " (" + Math.Max(0, TaskConv.Pre.HeatRemain_s) + ")";
            lbl_PreSt.BackColor = TaskConv.Pre.StatusColor;

            lbl_ProSt.Text = TaskConv.Pro.Status.ToString();
            if (TaskConv.Pro.Status == TaskConv.EProcessStatus.Heating)
                lbl_ProSt.Text = TaskConv.Pro.Status.ToString() + " (" + Math.Max(0, TaskConv.Pro.HeatRemain_s) + ")";
            lbl_ProSt.BackColor = TaskConv.Pro.StatusColor;

            lbl_PosSt.Text = TaskConv.Pos.Status.ToString();
            if (TaskConv.Pos.Status == TaskConv.EProcessStatus.Heating)
                lbl_PosSt.Text = TaskConv.Pos.Status.ToString() + " (" + Math.Max(0, TaskConv.Pos.HeatRemain_s) + ")";
            lbl_PosSt.BackColor = TaskConv.Pos.StatusColor;

            lbl_OutSt.Text = TaskConv.Out.Status.ToString();
            lbl_OutSt.BackColor = TaskConv.Out.StatusColor;
            #endregion

            Color color = this.BackColor;
            if (TaskConv.StopInput) color = Color.Lime;
            btn_StopInput.BackColor = color;

            color = this.BackColor;
            if (TaskConv.SkipDisp) color = Color.Lime;
            btn_SkipDisp.BackColor = color;

            color = this.BackColor;
            if (TaskConv.DispEndStop) color = Color.Lime;
            btn_DispEndStop.BackColor = color;

            color = this.BackColor;
            if (TaskConv.UnloadStop) color = Color.Lime;
            btn_UnloadStop.BackColor = color;

            if (TaskConv.BoardIsOpen)
            {
                lblLeftSmemaBdReady.BackColor = TaskConv.In.Smema_DI_BdReady ? Color.Lime : this.BackColor;
                lblLeftSmemaMcReady.BackColor = TaskConv.In.Smema_DO_McReady ? Color.Red : this.BackColor;
            }

            if (TaskConv.BoardIsOpen)
            {
                lbl_RightSmemaMcReady.BackColor = TaskConv.Out.Smema_DI_McReady ? Color.Lime : this.BackColor;
                lbl_RightSmemaBdReady.BackColor = TaskConv.Out.Smema_DO_BdReady ? Color.Red : this.BackColor;
            }
        }

        public bool Enable
        {
            set
            {
                if (value)
                {
                    this.Enable(true);
                }
                else
                {
                    this.Enable(false);
                    btn_StopInput.Enable(true);
                    btn_SkipDisp.Enable(true);
                    btn_DispEndStop.Enable(true);
                    btn_UnloadStop.Enable(true);

                    lblLeftSmemaBdReady.Enable(true);
                    lbl_RightSmemaMcReady.Enable(true);

                }
            }
        }

        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            UpdateDisplay();
        }

        private void EnableParent(bool Enable)
        {
            Enabled = Enable;
            try
            {
                if (Parent != null) { Parent.Enabled = Enable; } else return;
                if (Parent.Parent != null) { Parent.Parent.Enabled = Enable; } else return;
                if (Parent.Parent.Parent != null) { Parent.Parent.Parent.Enabled = Enable; } else return;
                if (Parent.Parent.Parent.Parent != null) { Parent.Parent.Parent.Parent.Enabled = Enable; } else return;
                if (Parent.Parent.Parent.Parent.Parent != null) { Parent.Parent.Parent.Parent.Parent.Enabled = Enable; } else return;
                if (Parent.Parent.Parent.Parent.Parent.Parent != null) { Parent.Parent.Parent.Parent.Parent.Parent.Enabled = Enable; }
            }
            catch (Exception ex)
            {
                Event.DEBUG_INFO.Set(MethodBase.GetCurrentMethod().Name.ToString(), ex.Message.ToString());
            }
        }

        private void btn_Return_Click(object sender, EventArgs e)
        {
            EnableParent(false);
            try
            {
                TaskConv.Manual_Return();

                //Cancel Disp if Pro is returned
                bool cancelDisp = true;
                if (TaskConv.Buf1.Status == TaskConv.EProcessStatus.Psnt) cancelDisp = false;
                if (TaskConv.Buf2.Status == TaskConv.EProcessStatus.Psnt) cancelDisp = false;
                if (TaskConv.Pre.Status >= TaskConv.EProcessStatus.Heating) cancelDisp = false;
                if (cancelDisp) DispProg.TR_Cancel();
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
            }
            finally
            {
                EnableParent(true);
            }
        }
        private void btn_LoadBuf1_Click(object sender, EventArgs e)
        {
            EnableParent(false);
            try
            {
                TaskConv.Manual_LoadBuf1();
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
            }
            finally
            {
                EnableParent(true);
            }
        }
        private void btn_LoadBuf2_Click(object sender, EventArgs e)
        {
            EnableParent(false);
            try
            {
                TaskConv.Manual_LoadBuf2();
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
            }
            finally
            {
                EnableParent(true);
            }
        }
        private void btn_LoadPre_Click(object sender, EventArgs e)
        {
            EnableParent(false);
            try
            {
                if (TaskConv.Out.Status == TaskConv.EProcessStatus.Psnt)
                {
                    if (TaskConv.Out.SensPsnt)
                    {
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.CONV_OUT_SENSOR_PSNT);
                    }
                    else
                        TaskConv.Out.Status = TaskConv.EProcessStatus.Empty;
                }

                TaskConv.Manual_LoadPre();
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
            }
            finally
            {
                EnableParent(true);
            }
        }
        private void btn_LoadPro_Click(object sender, EventArgs e)
        {
            EnableParent(false);
            try
            {
                if (TaskConv.Out.Status == TaskConv.EProcessStatus.Psnt)
                {
                    if (TaskConv.Out.SensPsnt)
                    {
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.CONV_OUT_SENSOR_PSNT);
                    }
                    else
                        TaskConv.Out.Status = TaskConv.EProcessStatus.Empty;
                }

                TaskConv.Manual_LoadPro();
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
            }
            finally
            {
                EnableParent(true);
            }
        }
        private void btnLoadPos_Click(object sender, EventArgs e)
        {
            EnableParent(false);
            try
            {
                if (TaskConv.Out.Status == TaskConv.EProcessStatus.Psnt)
                {
                    if (TaskConv.Out.SensPsnt)
                    {
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.CONV_OUT_SENSOR_PSNT);
                    }
                    else
                        TaskConv.Out.Status = TaskConv.EProcessStatus.Empty;
                }

                TaskConv.Manual_LoadPos();
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
            }
            finally
            {
                EnableParent(true);
            }
        }
        private void btn_Unload_Click(object sender, EventArgs e)
        {
            EnableParent(false);
            try
            {
                TaskConv.Manual_Unload();
                DispProg.TR_Cancel();
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
            }
            finally
            {
                EnableParent(true);
            }
        }

        private void btn_StopInput_Click(object sender, EventArgs e)
        {
            TaskConv.StopInput = !TaskConv.StopInput;

            switch (TaskConv.LeftMode)
            {
                case TaskConv.ELeftMode.Smema:
                    TaskConv.In.Smema_DO_McReady = false;
                    break;
            }

            //if (TaskConv.Pro.rt_StType == TaskConv.EProStType.Disp12)
            //{
            //    if (TaskConv.Pro.Status == TaskConv.EProcessStatus.WaitDisp2)
            //    {
            //        TaskConv.Pre.Status = TaskConv.EProcessStatus.WaitNone;
            //        TaskConv.Pro.Status = TaskConv.EProcessStatus.WaitDisp;
            //    }
            //}
        }
        private void btn_SkipDisp_Click(object sender, EventArgs e)
        {
            TaskConv.SkipDisp = !TaskConv.SkipDisp;
            UpdateDisplay();
        }
        private void btn_DispEndStop_Click(object sender, EventArgs e)
        {
            TaskConv.DispEndStop = !TaskConv.DispEndStop;
            UpdateDisplay();
        }
        private void btn_BoardEndStop_Click(object sender, EventArgs e)
        {
            TaskConv.UnloadStop = !TaskConv.UnloadStop;
            UpdateDisplay();
        }

        private void lbl_ConvSt_DoubleClick(object sender, EventArgs e)
        {
            if (NUtils.UserAcc.Active.GroupID == (int)ELevel.NSW)
                TaskConv.Status = TaskConv.EConvStatus.Ready;
            UpdateDisplay();
        }
        private void lbl_PreSt_DoubleClick(object sender, EventArgs e)
        {
            if (NUtils.UserAcc.Active.GroupID == (int)ELevel.NSW)
                if (TaskConv.Pre.Status == TaskConv.EProcessStatus.Psnt)
                    TaskConv.Pre.Status = (TaskConv.EProcessStatus)0;
                else
                    TaskConv.Pre.Status++;
            UpdateDisplay();
        }
        private void lbl_ProSt_DoubleClick(object sender, EventArgs e)
        {
            if (NUtils.UserAcc.Active.GroupID == (int)ELevel.NSW)
                if (TaskConv.Pro.Status == TaskConv.EProcessStatus.Psnt)
                    TaskConv.Pro.Status = (TaskConv.EProcessStatus)0;
                else
                    TaskConv.Pro.Status++;
            UpdateDisplay();
        }


        #region Smema
        private void btn_UL_RecvBoard_Click(object sender, EventArgs e)
        {
            switch (TaskConv.LeftMode)
            {
                case TaskConv.ELeftMode.Smema:
                    {
                        if (TaskConv.Out.Status != TaskConv.EProcessStatus.Empty) return;

                        try
                        {
                            Task.Run(() =>
                            {
                                bool InPsnt = false;
                                if (!TaskConv.In.Smema_DO_McReady)
                                {
                                    TaskConv.In.Smema_DO_McReady = true;
                                }

                                Stopwatch sw = Stopwatch.StartNew();
                                while (!TaskConv.In.Smema_DI_BdReady)
                                {
                                    if (sw.Elapsed.TotalSeconds >= 10)
                                    {
                                        TaskConv.In.Smema_DO_McReady = false;
                                        return;
                                    }
                                    Thread.Sleep(5);
                                }

                                if (TaskConv.In.Smema_DI_BdReady)
                                {
                                    InPsnt = false;
                                    if (!TaskConv.In.UL_WaitBdNotReady())
                                    {
                                        TaskConv.In.Smema_DO_McReady = false;
                                        return;
                                    }
                                    InPsnt = true;
                                }
                                else
                                {
                                    InPsnt = false;
                                }

                                if (InPsnt)
                                {
                                    if (TaskConv.Buf1.StType == TaskConv.EBufStType.Buffer && TaskConv.Buf1.Status == TaskConv.EProcessStatus.Empty)
                                    {
                                        TaskConv.MoveInToBuf1();
                                        return;
                                    }
                                    if (TaskConv.Buf2.StType == TaskConv.EBufStType.Buffer && TaskConv.Buf2.Status == TaskConv.EProcessStatus.Empty)
                                    {
                                        TaskConv.MoveInToBuf2();
                                        return;
                                    }
                                    if (TaskConv.Pre.StType >= TaskConv.EPreStType.None || TaskConv.Pre.Status == TaskConv.EProcessStatus.Empty)
                                    {
                                        TaskConv.MoveInToPre();
                                        return;
                                    }
                                    if (TaskConv.Pro.StType >= TaskConv.EProStType.None || TaskConv.Pro.Status == TaskConv.EProcessStatus.Empty)
                                    {
                                        TaskConv.MoveInToPro();
                                        return;
                                    }
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            Msg MsgBox = new Msg();
                            MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                        }
                        finally
                        {
                        }
                        break;
                    }
            }
        }

        private void btn_DL_SendBoard_Click(object sender, EventArgs e)
        {
            switch (TaskConv.RightMode)
            {
                case TaskConv.ERightMode.Smema:
                    {
                        try
                        {
                            Task.Run(() =>
                            {
                                TaskConv.Out.Smema_DO_BdReady = true;
                                Stopwatch sw = Stopwatch.StartNew();
                                while (!TaskConv.Out.Smema_DI_McReady)
                                {
                                    if (sw.Elapsed.TotalSeconds >= 10)
                                    {
                                        TaskConv.Out.Smema_DO_BdReady = false;
                                        return;
                                    }
                                    Thread.Sleep(5);
                                }

                                if (TaskConv.Out.Smema_DI_McReady)
                                {
                                    TaskConv.Out.Smema_DO_BdReady = true;
                                    if (!TaskConv.Unload_Out())
                                    {
                                        TaskConv.Out.Smema_DO_BdReady = false;
                                        return;
                                    }
                                    TaskConv.Out.Smema_DO_BdReady = false;
                                    if (!TaskConv.Out.DL_WaitMcNotReady()) return;
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            Msg MsgBox = new Msg();
                            MsgBox.Show(Messages.CONV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                        }
                        finally
                        {
                        }
                        break;
                    }
            }
        }
        #endregion

        private void lblLeftSmemaBdReady_MouseDown(object sender, MouseEventArgs e)
        {
            TaskConv.In.Smema_Emulate_DI_BdReady = true;
        }
        private void lblLeftSmemaBdReady_MouseUp(object sender, MouseEventArgs e)
        {
            TaskConv.In.Smema_Emulate_DI_BdReady = false;
        }

        private void lbl_RightSmemaMcReady_MouseDown(object sender, MouseEventArgs e)
        {
            TaskConv.Out.Smema_Emulate_DI_McReady = true;
        }
        private void lbl_RightSmemaMcReady_MouseUp(object sender, MouseEventArgs e)
        {
            TaskConv.Out.Smema_Emulate_DI_McReady = false;
        }
    }
}
