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
    partial class frm_DispCore_JogGantryVision : Form
    {
        frmJogControl frmJogControl = new frmJogControl();
        frmMVCGenTLCamera TaskVisionfrmMVCGenTLCamera = new frmMVCGenTLCamera();

        //public frmVisionView PageVision = new frmVisionView();
        //public frmJogGantry PageJog = new frmJogGantry();

        public EForceGantryMode ForceGantryMode = EForceGantryMode.None;

        public bool ShowVision = false;
        public string Inst = "";

        public TReticles Reticles = new TReticles();
        public bool ShowReticles = false;

        public frm_DispCore_JogGantryVision()
        {
            InitializeComponent();
            ShowVision = true;

            frmJogControl.FormBorderStyle = FormBorderStyle.None;
            frmJogControl.TopLevel = false;
            frmJogControl.Parent = splitContainer1.Panel2;
            frmJogControl.Dock = DockStyle.Right;
            frmJogControl.Show();

            if (GDefine.CameraType[0] == GDefine.ECameraType.Spinnaker2)
            {
                this.WindowState = FormWindowState.Maximized;
                this.BringToFront();
            }
            else
            if (GDefine.CameraType[0] == GDefine.ECameraType.MVSGenTL)
            {
                this.WindowState = FormWindowState.Maximized;
                this.BringToFront();

                TaskVisionfrmMVCGenTLCamera = new frmMVCGenTLCamera();
                TaskVisionfrmMVCGenTLCamera.FormBorderStyle = FormBorderStyle.None;
                TaskVisionfrmMVCGenTLCamera.TopLevel = false;
                TaskVisionfrmMVCGenTLCamera.Parent = splitContainer1.Panel1;
                TaskVisionfrmMVCGenTLCamera.Dock = DockStyle.Fill;
                TaskVisionfrmMVCGenTLCamera.Show();
            }
            else
            {
                this.Top = 0;
                this.Left = 0;
                this.Width = 518;
            }
        }

        private void frmJogGantryVision_Load(object sender, EventArgs e)
        {
            AppLanguage.Func2.UpdateText(this);

            //            TopMost = true;

            this.Text = "Jog";
            this.Top = 0;
            this.Left = 0;

            splitContainer1.BringToFront();
            splitContainer1.SplitterDistance = this.ClientSize.Width - frmJogControl.Width;

            TaskVisionfrmMVCGenTLCamera.Reticles = new TReticles(Reticles);
            TaskVisionfrmMVCGenTLCamera.ShowReticles = ShowReticles;

            switch (GDefine.CameraType[0])
            {
                case GDefine.ECameraType.Spinnaker2:
                case GDefine.ECameraType.MVSGenTL:
                    {
                        ////TaskVisionfrmMVCGenTLCamera.CamReticles = new Reticle(Reticles);
                        //TaskVisionfrmMVCGenTLCamera.Reticles = new TReticles(Reticles);
                        //TaskVisionfrmMVCGenTLCamera.ShowReticles = ShowReticles;
                        //TaskVisionfrmMVCGenTLCamera.FormBorderStyle = FormBorderStyle.None;
                        //TaskVisionfrmMVCGenTLCamera.TopLevel = false;
                        //TaskVisionfrmMVCGenTLCamera.Parent = splitContainer1.Panel1;
                        //TaskVisionfrmMVCGenTLCamera.Dock = DockStyle.Fill;
                        //TaskVisionfrmMVCGenTLCamera.Show();

                        ////TaskVisionfrmMVCGenTLCamera.SelectCamera(0);
                        ////TaskVisionfrmMVCGenTLCamera.ShowCamReticles = true;
                        ////TaskVision.genTLCamera[0].StartGrab();
                        ////TaskVisionfrmMVCGenTLCamera.ZoomFit();
                        break;
                    }
            }
            lbl_Inst.Text = Inst;
        }
        private void frm_JogGantryVision_Shown(object sender, EventArgs e)
        {
        }
        private void frm_JogGantryVision_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaskVision.DrawCalStep = 0;
        }
        private void frm_DispCore_JogGantryVision_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (GDefine.CameraType[0])
            {
                case GDefine.ECameraType.Spinnaker2:
                case GDefine.ECameraType.MVSGenTL:
                    {
                        TaskVisionfrmMVCGenTLCamera.Close();
                        break;
                    }
            }
        }
        private void frm_JogGantryVision_Enter(object sender, EventArgs e)
        {

        }
        private void frm_JogGantryVision_Activated(object sender, EventArgs e)
        {
        }
        private void frm_JogGantryVision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TaskDisp.TaskMoveGZZ2Up();

                if (this.Modal)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                    Visible = false;
            }
        }

        public void SelectCamera(int index)
            {
            TaskVisionfrmMVCGenTLCamera.SelectCamera(index);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.Modal)
            {
                DialogResult = DialogResult.OK;
            }
            else
                Visible = false;
        }
        private void btn_Retry_Click(object sender, EventArgs e)
        {
            if (this.Modal)
            {
                DialogResult = DialogResult.Retry;
            }
            else
                Visible = false;
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (this.Modal)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
                Visible = false;
        }
    }
}
