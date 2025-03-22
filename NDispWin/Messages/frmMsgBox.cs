using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace NDispWin
{
    public partial class frm_MsgBox : Form
    {
        //public int ErrCode = -1;
        //MsgInfo.TMsgInfo CurrentMsgInfo = new MsgInfo.TMsgInfo();
        TEMessage msg;
        string ExMsg = "";
        //EMcState McState = EMcState.None;
        EMsgBtn MsgBtn = EMsgBtn.smbNone;
        bool Assist = false;
        public EMsgRes MsgRes = EMsgRes.smrNone;

        public frm_MsgBox()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TopMost = true;
            AutoSize = true;

            pbox_Image.SizeMode = PictureBoxSizeMode.Zoom;

            tmr_BringToFront.Enabled = true;
            tmr_BringToFront.Interval = 5000;
        }

        //public void SetErrCode(int ErrCode, string ExMsg, EMcState McState, EMsgBtn Btn, bool Assist)
        //{
        //    this.ErrCode = ErrCode;
        //    MsgInfo.GetInfo(ErrCode, ref CurrentMsgInfo);

        //    this.ExMsg = ExMsg;
        //    this.McState = McState;
        //    this.MsgBtn = Btn;
        //    this.Assist = Assist;

        //    GDefine.sgc2.SendAlarmSet(ErrCode.ToString() + "," + CurrentMsgInfo.Desc);

        //    NUtils.RegistryWR Reg = new NUtils.RegistryWR("SOFTWARE");
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "ERRCODE", ErrCode.ToString("0000"));
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "DESC", CurrentMsgInfo.Desc);
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "CACT", CurrentMsgInfo.CAct);
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "EXMSG", ExMsg);
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "STATE", McState.ToString());
        //}
        //public void SetErrCode(string Desc, string CAct, string ExMsg, EMcState McState, EMsgBtn Btn, bool Assist)
        //{
        //    CurrentMsgInfo = new MsgInfo.TMsgInfo(Desc, CAct);

        //    this.ExMsg = ExMsg;
        //    this.McState = McState;
        //    this.MsgBtn = Btn;
        //    this.Assist = Assist;

        //    GDefine.sgc2.SendAlarmSet("0" + "," + CurrentMsgInfo.Desc);

        //    NUtils.RegistryWR Reg = new NUtils.RegistryWR("SOFTWARE");
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "ERRCODE", "0");
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "DESC", CurrentMsgInfo.Desc);
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "CACT", CurrentMsgInfo.CAct);
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "EXMSG", ExMsg);
        //    Reg.WriteKey("NSWAUTOMATION_MSG", "STATE", McState.ToString());
        //}
        public void SetErrCode(TEMessage msg, string exMsg, EMsgBtn msgBtn)
        {
            this.msg = new TEMessage(msg);
            this.ExMsg = exMsg;
            this.MsgBtn = msgBtn;

            GDefine.sgc2.SendAlarmSet($"{msg.Code:d4},{msg.Desc}");

            NUtils.RegistryWR Reg = new NUtils.RegistryWR("SOFTWARE");
            Reg.WriteKey("NSWAUTOMATION_MSG", "DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Reg.WriteKey("NSWAUTOMATION_MSG", "ERRCODE", $"{msg.Code:d4}");
            Reg.WriteKey("NSWAUTOMATION_MSG", "DESC", msg.Desc);
            Reg.WriteKey("NSWAUTOMATION_MSG", "CACT", msg.CAct);
            Reg.WriteKey("NSWAUTOMATION_MSG", "EXMSG", exMsg);
            Reg.WriteKey("NSWAUTOMATION_MSG", "TYPE", msg.Type.ToString());
        }

        private void frm_Msg_Load(object sender, EventArgs e)
        {
            AppLanguage.Func2.UpdateText(this);

            UpdateMsg();
        }
        private void frm_Msg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        private void frm_Msg_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmr_BringToFront.Enabled = false;
        }
        private void frm_Msg_Shown(object sender, EventArgs e)
        {
        }
        private void UpdateMsg()
        {
            tsslVersion.Text = Application.ProductName + " v" + Application.ProductVersion;

            //CurrentMsgInfo.Desc = CurrentMsgInfo.Desc.Replace((char)64, (char)13);
            //CurrentMsgInfo.CAct = CurrentMsgInfo.CAct.Replace((char)64, (char)13);
            //CurrentMsgInfo.Desc_Alt = CurrentMsgInfo.Desc_Alt.Replace((char)64, (char)13);
            //CurrentMsgInfo.CAct_Alt = CurrentMsgInfo.CAct_Alt.Replace((char)64, (char)13);

            lbl_ErrCode.Text = $"{msg.Code:D4}";//ErrCode.ToString("D4");

            lbl_Desc.Text = msg.Desc.Replace((char)64, (char)13);//CurrentMsgInfo.Desc;
            lbl_CAct.Text = msg.CAct.Replace((char)64, (char)13);//CurrentMsgInfo.CAct;

            lbl_Desc_Alt.Text = msg.DescAlt.Replace((char)64, (char)13);//CurrentMsgInfo.Desc_Alt;
            lbl_CAct_Alt.Text = msg.CActAlt.Replace((char)64, (char)13);//CurrentMsgInfo.CAct_Alt;

            lbl_ExMessage.Text = ExMsg;

            #region Load Image
            string imgFileName = GDefine.MsgBoxImageDir.FullName + $"{msg.Code:D4}";// ErrCode.ToString("0000");
            try
            {
                bool FileExist = false;
                if (File.Exists(imgFileName + ".jpg")) { pbox_Image.Load(imgFileName + ".jpg"); FileExist = true; }
                else
                    if (File.Exists(imgFileName + ".png")) { pbox_Image.Load(imgFileName + ".png"); FileExist = true; }
                else
                        if (File.Exists(imgFileName + ".bmp")) { pbox_Image.Load(imgFileName + ".bmp"); FileExist = true; }

                pbox_Image.Visible = FileExist;
                tsslDateTime.Text = DateTime.Now.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("HH:mm:ss tt");
            }
            catch
            {
                //lbl_Status.Text = "Load Image Error.";
            }
            #endregion

            #region Button
            btn_AlmClr.Enabled = true;
            btn_OK.Visible = (MsgBtn & EMsgBtn.smbOK) == EMsgBtn.smbOK;
            btnYes.Visible = (MsgBtn & EMsgBtn.smbYes) == EMsgBtn.smbYes;
            btnNo.Visible = (MsgBtn & EMsgBtn.smbNo) == EMsgBtn.smbNo;
            btn_Retry.Visible = (MsgBtn & EMsgBtn.smbRetry) == EMsgBtn.smbRetry;
            btn_Stop.Visible = (MsgBtn & EMsgBtn.smbStop) == EMsgBtn.smbStop;
            btn_Cancel.Visible = (MsgBtn & EMsgBtn.smbCancel) == EMsgBtn.smbCancel;
            #endregion

            //IO.SetState(McState);
            switch (msg.Type)
            {
                case TEMessage.EType.Error: TCTwrLight.SetStatus(TwrLight.Error); break;
                case TEMessage.EType.Warning: TCTwrLight.SetStatus(TwrLight.Warning); break;
                case TEMessage.EType.Fault: TCTwrLight.SetStatus(TwrLight.Fault); break;
                case TEMessage.EType.Notification: TCTwrLight.SetStatus(TwrLight.Notification); break;
                case TEMessage.EType.Confirmation: TCTwrLight.SetStatus(TwrLight.Confirmation); break;
                case TEMessage.EType.Run: TCTwrLight.SetStatus(TwrLight.Run); break;
                case TEMessage.EType.Idle: TCTwrLight.SetStatus(TwrLight.Idle); break;
                case TEMessage.EType.Custom1: TCTwrLight.SetStatus(TwrLight.Custom1); break;
                case TEMessage.EType.Custom2: TCTwrLight.SetStatus(TwrLight.Custom2); break;
            }

            Msg.MsgInQue++;
            if (Assist) Msg.AssistCount++;

            Log.AddToLog($"{msg.Code:D4}\t{msg.Desc}\t{ExMsg}");// ErrCode.ToString("0000") + (char)9 + CurrentMsgInfo.Desc + (char)9 + ExMsg);
        }

        private void AlmClr()
        {
            TCTwrLight.SetMute();//IO.SetState(EMcState.Mute);
        }
        private void OK()
        {
            GDefine.sgc2.SendAlarmClear($"{msg.Code:D4}");//ErrCode.ToString("0000"));
            Log.AddToLog($"{msg.Code:D4}\tOK");//ErrCode.ToString("0000") + (char)9 + "OK");
            TCTwrLight.SetStatus(TwrLight.Idle);//IO.SetState(EMcState.Idle);
            MsgRes = EMsgRes.smrOK;
            Msg.MsgInQue--;
            Close();
        }
        private void Yes()
        {
            GDefine.sgc2.SendAlarmClear($"{msg.Code:D4}");//(ErrCode.ToString("0000"));
            Log.AddToLog($"{msg.Code:D4}\tYes");//(ErrCode.ToString("0000") + (char)9 + "Yes");
            TCTwrLight.SetStatus(TwrLight.Idle);//IO.SetState(EMcState.Idle);
            MsgRes = EMsgRes.smrYes;
            Msg.MsgInQue--;
            Close();
        }
        private void No()
        {
            GDefine.sgc2.SendAlarmClear($"{msg.Code:D4}");//(ErrCode.ToString("0000"));
            Log.AddToLog($"{msg.Code:D4}\tNo");//(ErrCode.ToString("0000") + (char)9 + "No");
            TCTwrLight.SetStatus(TwrLight.Idle);//IO.SetState(EMcState.Idle);
            MsgRes = EMsgRes.smrNo;
            Msg.MsgInQue--;
            Close();
        }
        private void Retry()
        {
            GDefine.sgc2.SendAlarmClear($"{msg.Code:D4}");//(ErrCode.ToString("0000"));
            Log.AddToLog($"{msg.Code:D4}\tRetry");//(ErrCode.ToString("0000") + (char)9 + "Retry");
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            MsgRes = EMsgRes.smrRetry;
            Msg.MsgInQue--;
            Close();
        }
        private void Stop()
        {
            GDefine.sgc2.SendAlarmClear($"{msg.Code:D4}");//(ErrCode.ToString("0000"));
            Log.AddToLog($"{msg.Code:D4}\tStop");//(ErrCode.ToString("0000") + (char)9 + "Stop");
            TCTwrLight.SetStatus(TwrLight.Idle);//IO.SetState(EMcState.Idle);
            MsgRes = EMsgRes.smrStop;
            Msg.MsgInQue--;
            Close();
        }
        private void Cancel()
        {
            GDefine.sgc2.SendAlarmClear($"{msg.Code:D4}");//(ErrCode.ToString("0000"));
            Log.AddToLog($"{msg.Code:D4}\tCancel");//(ErrCode.ToString("0000") + (char)9 + "Cancel");
            TCTwrLight.SetStatus(TwrLight.Idle);//IO.SetState(EMcState.Idle);
            MsgRes = EMsgRes.smrCancel;
            Msg.MsgInQue--;
            Close();
        }

        private void btn_AlmClr_Click(object sender, EventArgs e)
        {
            AlmClr();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (Msg.enableControlKeyPress && !((ModifierKeys & Keys.Control) == Keys.Control)) return;
            Cancel();
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (Msg.enableControlKeyPress && !((ModifierKeys & Keys.Control) == Keys.Control)) return;
            OK();
        }
        private void btn_Retry_Click(object sender, EventArgs e)
        {
            if (Msg.enableControlKeyPress && !((ModifierKeys & Keys.Control) == Keys.Control)) return;
            Retry();
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            if (Msg.enableControlKeyPress && !((ModifierKeys & Keys.Control) == Keys.Control)) return;
            Stop();
        }

        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;
            tsslType.Text = $"Type {msg.Type}";
        }
        private void tmr_BringToFront_Tick(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (Msg.enableControlKeyPress && !((ModifierKeys & Keys.Control) == Keys.Control)) return;
            Yes();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            if (Msg.enableControlKeyPress && !((ModifierKeys & Keys.Control) == Keys.Control)) return;
            No();
        }
    }
}


