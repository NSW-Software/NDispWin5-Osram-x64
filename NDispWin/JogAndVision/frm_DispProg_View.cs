using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NDispWin
{
    public partial class frm_DispProg_View : Form
    {
        frmJogControl frmJogControl = new frmJogControl();
        public bool ShowSetBtn = true;
        public bool ShowCamOfstBtn = false;

        public frm_DispProg_View()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker)
            {
                JogWindPos = EJogWindPos.TL;
                pbox_Image.Visible = false;

                WindowState = FormWindowState.Normal;
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }

            this.Text = "Image View";

            UpdateDisplay();
        }

        private object _lock = new object();
        private volatile bool _unsubscribed = false;

        Size s_Form = new Size(0, 0);
        Point p_Form = new Point(0, 0);
        private void frm_DispProg_View_Load(object sender, EventArgs e)
        {
            frmJogControl.TopLevel = false;
            frmJogControl.Parent = this;
            frmJogControl.FormBorderStyle = FormBorderStyle.None;
            frmJogControl.BringToFront();
            frmJogControl.Show();


            btn_CamOfst.Visible = ShowCamOfstBtn;
            btn_Set.Visible = ShowSetBtn;
            btn_Confirm.Visible = false;
            btn_Close.Text = "Close";

            tmr_Debug.Enabled = true;
            tmr_Debug.Interval = 10000;

            BringToFront();

            UpdateDisplay();

            if (GDefine.CameraType[0] == GDefine.ECameraType.Basler)
            {
                Close();
            }

            if (GDefine.CameraType[0] == GDefine.ECameraType.PtGrey)
            {
                try
                {
                    TaskVision.PtGrey_CamLive(0);
                }
                catch { };

                TaskVision.fe.GrabbedEvent += new TaskVision.OnGrabbedEventHandler(GrabbedEvent);
                TaskVision.CameraRun = true;
            }

            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker)
            {
                this.BringToFront();
                this.TopMost = true;
                this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width; ;
                this.Top = 0;
            }

            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker2)
            {
                //JogWindPos = EJogWindPos.TR;
                //UpdateDisplay();

                //TaskVision.frmCamera = new frmCamera();
                //TaskVision.frmCamera.flirCamera = TaskVision.flirCamera2;
                //TaskVision.frmCamera.CamReticles = Reticle.Reticles;
                //TaskVision.frmCamera.FormBorderStyle = FormBorderStyle.None;
                //TaskVision.frmCamera.TopLevel = false;
                //TaskVision.frmCamera.Parent = pbox_Image;
                //TaskVision.frmCamera.Dock = DockStyle.Fill;
                //TaskVision.frmCamera.SelectCamera(0);
                //TaskVision.frmCamera.Show();

                //TaskVision.frmCamera.ShowCamReticles = true;
                //TaskVision.frmCamera.Grab();
                JogWindPos = EJogWindPos.TR;
                UpdateDisplay();

                TaskVision.frmMVCGenTLCamera = new frmMVCGenTLCamera();
                TaskVision.frmMVCGenTLCamera.CamReticles = Reticle.Reticles;
                TaskVision.frmMVCGenTLCamera.FormBorderStyle = FormBorderStyle.None;
                TaskVision.frmMVCGenTLCamera.TopLevel = false;
                TaskVision.frmMVCGenTLCamera.Parent = pbox_Image;
                TaskVision.frmMVCGenTLCamera.Dock = DockStyle.Fill;
                TaskVision.frmMVCGenTLCamera.Show();

                TaskVision.genTLCamera[0].StartGrab();
                TaskVision.frmMVCGenTLCamera.SelectCamera(0);
                TaskVision.frmMVCGenTLCamera.ShowCamReticles = true;
            }
            if (GDefine.CameraType[0] == GDefine.ECameraType.MVCGenTL)
            {
                JogWindPos = EJogWindPos.TR;
                UpdateDisplay();

                TaskVision.frmMVCGenTLCamera = new frmMVCGenTLCamera();
                TaskVision.frmMVCGenTLCamera.CamReticles = Reticle.Reticles;
                TaskVision.frmMVCGenTLCamera.FormBorderStyle = FormBorderStyle.None;
                TaskVision.frmMVCGenTLCamera.TopLevel = false;
                TaskVision.frmMVCGenTLCamera.Parent = pbox_Image;
                TaskVision.frmMVCGenTLCamera.Dock = DockStyle.Fill;
                TaskVision.frmMVCGenTLCamera.Show();

                TaskVision.genTLCamera[0].StartGrab();
                TaskVision.frmMVCGenTLCamera.SelectCamera(0);
                TaskVision.frmMVCGenTLCamera.ShowCamReticles = true;
            }
        }
        private void frm_DispProg_View_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmJogControl.Close();

            if (GDefine.CameraType[0] == GDefine.ECameraType.PtGrey)
            {
                try
                {
                    TaskVision.PtGrey_CamStop();
                }
                catch { };

                lock (_lock)
                {
                    _unsubscribed = true;
                    TaskVision.fe.GrabbedEvent -= new TaskVision.OnGrabbedEventHandler(GrabbedEvent);
                }
            }
            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker2) TaskVision.frmMVCGenTLCamera.Close();//TaskVision.frmCamera.Close();
            if (GDefine.CameraType[0] == GDefine.ECameraType.MVCGenTL) TaskVision.frmMVCGenTLCamera.Close();
        }
        private void frm_DispProg_View_FormClosed(object sender, FormClosedEventArgs e)
        {
            tmr_Debug.Enabled = false;

            GC.Collect();
        }

        enum EJogWindPos { TR, BR, BL, TL };
        EJogWindPos JogWindPos = EJogWindPos.TR;
        private void UpdateDisplay()
        {
            switch (JogWindPos)
            {
                case EJogWindPos.TR:
                    pbox_Image.Left = 0;
                    pbox_Image.Top = pnl_Top.Top + pnl_Top.Height;
                    //uctrl_JogGantry1.Left = this.ClientSize.Width - uctrl_JogGantry1.Width;
                    //uctrl_JogGantry1.Top = pnl_Top.Top + pnl_Top.Height;
                    frmJogControl.Left = this.ClientSize.Width - frmJogControl.Width;
                    frmJogControl.Top = pnl_Top.Top + pnl_Top.Height;
                    break;
                case EJogWindPos.BR:
                    pbox_Image.Left = 0;
                    pbox_Image.Top = pnl_Top.Top + pnl_Top.Height;
                    //uctrl_JogGantry1.Left = this.ClientSize.Width - uctrl_JogGantry1.Width;
                    //uctrl_JogGantry1.Top = this.ClientSize.Height - uctrl_JogGantry1.Height;
                    frmJogControl.Left = this.ClientSize.Width - frmJogControl.Width;
                    frmJogControl.Top = this.ClientSize.Height - frmJogControl.Height;
                    break;
                case EJogWindPos.BL:
                    pbox_Image.Left = this.ClientSize.Width - pbox_Image.Width;
                    pbox_Image.Top = pnl_Top.Top + pnl_Top.Height;
                    //uctrl_JogGantry1.Left = 0;
                    //uctrl_JogGantry1.Top = this.ClientSize.Height - uctrl_JogGantry1.Height;
                    frmJogControl.Left = 0;
                    frmJogControl.Top = this.ClientSize.Height - frmJogControl.Height;
                    break;
                case EJogWindPos.TL:
                    pbox_Image.Left = this.ClientSize.Width - pbox_Image.Width;
                    pbox_Image.Top = pnl_Top.Top + pnl_Top.Height;
                    //uctrl_JogGantry1.Left = 0;
                    //uctrl_JogGantry1.Top = pnl_Top.Top + pnl_Top.Height;
                    frmJogControl.Left = 0;
                    frmJogControl.Top = pnl_Top.Top + pnl_Top.Height;
                    break;
            }
        }

        private void btn_JogPos_Click(object sender, EventArgs e)
        {
            if (JogWindPos < EJogWindPos.TL)
                JogWindPos++;
            else
                JogWindPos = EJogWindPos.TR;
            UpdateDisplay();
        }

        private void GrabbedEvent(object sender, EventArgs e)
        {
            if (!Visible) return;

            if (!TaskVision.CameraRun) return;

            //if (!TaskVision.PGCamera[0].IsConnected) return;

            //lock (_lock)
            //{
            //    if (_unsubscribed) return;

            //    using (Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> ImageG = new Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>(TaskVision.PGCamera[0].Image()))
            //    {
            //        try
            //        {
            //            if (ImageG == null) return;

            //            using (Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> ImageC = ImageG.Convert<Emgu.CV.Structure.Bgr, byte>())
            //            {
            //                TaskVision.ImageDrawReticle(ImageG, ImageC);
            //                pbox_Image.Image = ImageC.ToBitmap();
            //                pbox_Image.Invalidate();
            //            }
            //        }
            //        catch (Exception Ex)
            //        {
            //            Log.AddToLog("frm_DispProg_View.GrabbedEvent, " + Ex.Message.ToString() + ".");
            //            GC.Collect();
            //        }
            //        finally
            //        {
            //        }
            //    }
            //    Thread.Sleep(5);
            //}
        }

        private void btn_SetOrigin_Click(object sender, EventArgs e)
        {
            btn_Confirm.Visible = true;
            btn_Close.Text = "Cancel";
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            TCTwrLight.SetStatus(TwrLight.Idle);
        }
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            btn_Confirm.Visible = false;
            DialogResult = DialogResult.OK;
            TCTwrLight.SetStatus(TwrLight.Idle);//IO.SetState(EMcState.Last);
        }

        private void tmr_Debug_Tick(object sender, EventArgs e)
        {
        }

        private void btn_CamOfst_Click(object sender, EventArgs e)
        {
            TaskDisp.TaskToggleCamOffset();
        }

        private void pbox_Image_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
        }
    }
}
