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
    public partial class frmVisionFailMsg2 : Form
    {
        public string Message = "";
        public bool ShowAccept = false;
        public bool ShowSkip = true;
        public bool ShowManual = true;

        public frmVisionFailMsg2()
        {
            InitializeComponent();
            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker) this.AutoSize = true;
            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker2)
            {
            }
        } 

        Size s_Form = new Size(0,0);
        Point p_Form = new Point(0, 0);
        private void frmVisionFailMsg2_Load(object sender, EventArgs e)
        {
            btn_Accept.Visible = ShowAccept;
            btn_Skip.Visible = ShowSkip;
            btn_Manual.Visible = ShowManual;
            rtbMessage.Text = Message;
            rtbMessage.Visible = rtbMessage.Text.Length > 0;

            Left = 0;
            Top = 0;

            UpdateDisplay();

            Text = "Vision Fail Message";

            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker)
            {
            }

            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker2)
            {
                //this.WindowState = FormWindowState.Maximized;
                //AutoSize = false;
                //this.FormBorderStyle = FormBorderStyle.Sizable;
                //panel1.Top = 0;
                //panel1.Left = this.Width - panel1.Width;
                //panel1.AutoSize = true;
                //this.TopMost = true;
                //this.BringToFront();

                //TaskVision.frmCamera = new frmCamera();
                //TaskVision.frmCamera.flirCamera = TaskVision.flirCamera2;
                //TaskVision.frmCamera.CamReticles = Reticle.Reticles;
                //TaskVision.frmCamera.FormBorderStyle = FormBorderStyle.None;
                //TaskVision.frmCamera.TopLevel = false;
                //TaskVision.frmCamera.Parent = this;
                //TaskVision.frmCamera.Dock = DockStyle.Fill;
                //TaskVision.frmCamera.SelectCamera(0);
                //TaskVision.frmCamera.Show();

                //TaskVision.frmCamera.ShowCamReticles = true;
                //TaskVision.frmCamera.Grab();
                this.WindowState = FormWindowState.Maximized;
                AutoSize = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                panel1.Top = 0;
                panel1.Left = this.Width - panel1.Width;
                panel1.AutoSize = true;
                this.TopMost = true;
                this.BringToFront();

                TaskVision.frmMVCGenTLCamera = new frmMVCGenTLCamera();
                TaskVision.frmMVCGenTLCamera.CamReticles = Reticle.Reticles;
                TaskVision.frmMVCGenTLCamera.FormBorderStyle = FormBorderStyle.None;
                TaskVision.frmMVCGenTLCamera.TopLevel = false;
                TaskVision.frmMVCGenTLCamera.Parent = this;
                TaskVision.frmMVCGenTLCamera.Dock = DockStyle.Fill;
                TaskVision.frmMVCGenTLCamera.SelectCamera(0);
                TaskVision.frmMVCGenTLCamera.Show();

                TaskVision.frmMVCGenTLCamera.ShowCamReticles = true;
                TaskVision.genTLCamera[0].StartGrab();
            }

            if (GDefine.CameraType[0] == GDefine.ECameraType.MVSGenTL)
            {
                this.WindowState = FormWindowState.Maximized;
                AutoSize = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                panel1.Top = 0;
                panel1.Left = this.Width - panel1.Width;
                panel1.AutoSize = true;
                this.TopMost = true;
                this.BringToFront();

                TaskVision.frmMVCGenTLCamera = new frmMVCGenTLCamera();
                TaskVision.frmMVCGenTLCamera.CamReticles = Reticle.Reticles;
                TaskVision.frmMVCGenTLCamera.FormBorderStyle = FormBorderStyle.None;
                TaskVision.frmMVCGenTLCamera.TopLevel = false;
                TaskVision.frmMVCGenTLCamera.Parent = this;
                TaskVision.frmMVCGenTLCamera.Dock = DockStyle.Fill;
                TaskVision.frmMVCGenTLCamera.SelectCamera(0);
                TaskVision.frmMVCGenTLCamera.Show();

                TaskVision.frmMVCGenTLCamera.ShowCamReticles = true;
                TaskVision.genTLCamera[0].StartGrab();
            }

            TCTwrLight.SetStatus(TwrLight.Error);//IO.SetState(EMcState.Error);
        }
        private void frmVisionFailMsg2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker2) TaskVision.frmMVCGenTLCamera.Close();//TaskVision.frmCamera.Close();
            if (GDefine.CameraType[0] == GDefine.ECameraType.MVSGenTL) TaskVision.frmMVCGenTLCamera.Close();
        }

        enum EJogWindPos { TR, BR, BL, TL };
        EJogWindPos JogWindPos = EJogWindPos.TR;
        private void UpdateDisplay()
        {
            switch (JogWindPos)
            {
                case EJogWindPos.TR:
                    panel1.Left = this.Width - panel1.Width;
                    panel1.Top = 0;
                    break;
                case EJogWindPos.BR:
                    panel1.Left = this.Width - panel1.Width;
                    panel1.Top = this.Height - panel1.Height;
                    break;
                case EJogWindPos.BL:
                    panel1.Left = 0;
                    panel1.Top = this.Height - panel1.Height;
                    break;
                case EJogWindPos.TL:
                    panel1.Left = 0;
                    panel1.Top = 0;
                    break;
            }
        }

        private void btn_AlmClr_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetMute();//IO.SetState(EMcState.Mute);
        }

        private void btn_Accept_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.Yes;
        }

        private void btn_Retry_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.Retry;
        }

        private void btn_Skip_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.Cancel;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Idle);//TCTowerLight.SetStatus(TowerLight.Idle);
            DialogResult = DialogResult.Abort;
        }

        private void btn_Manual_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.OK;
        }


        private void pbox_Image_Click(object sender, EventArgs e)
        {

        }

        private void btn_JogPos_Click(object sender, EventArgs e)
        {
            if (JogWindPos < EJogWindPos.TL)
                JogWindPos++;
            else
                JogWindPos = EJogWindPos.TR;

            UpdateDisplay();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
