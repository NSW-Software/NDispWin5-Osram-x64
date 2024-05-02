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
    internal partial class frmVisionSetup : Form
    {
        public frmVisionSetup()
        {
            InitializeComponent();
            GControl.LogForm(this);

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmVisionConfig_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);

            this.Text = "Vision Setup";

            TaskVision.CalMode[2] = TaskVision.ECalMode.Aperture;
 
            UpdateDisplay();
        }
        private void frm_VisionSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
         //   TaskDisp.TaskMoveGZZ2Up();
        }
        private void frmVisionConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
        private void UpdateDisplay()
        {
            lbl_VisionSettleTime.Text = TaskVision.SettleTime.ToString();
            lblCam1DistPerPixX.Text = $"{TaskVision.DistPerPixelX[0]:f6}";
            lblCam1DistPerPixY.Text = $"{TaskVision.DistPerPixelY[0]:f6}";
            lblCam2DistPerPixX.Text = $"{TaskVision.DistPerPixelX[1]:f6}";
            lblCam2DistPerPixY.Text = $"{TaskVision.DistPerPixelY[1]:f6}";
            lblCam3DistPerPixX.Text = $"{TaskVision.DistPerPixelX[2]:f6}";
            lblCam3DistPerPixY.Text = $"{TaskVision.DistPerPixelY[2]:f6}";

            lbl_LaserSettleTime.Text = TaskLaser.SettleTime.ToString();
        }

        private void lbl_VisionSettleTime_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Vision Settle Time (ms)", ref TaskVision.SettleTime, 0, 500);
            UpdateDisplay();
        }

        private void btn_CalCam1_Click(object sender, EventArgs e)
        {
            TaskVision.CalVisionXY(ECamNo.Cam00);
            UpdateDisplay();
        }
        private void btn_CalCam2_Click(object sender, EventArgs e)
        {
            TaskVision.CalVisionXY(ECamNo.Cam01);
            UpdateDisplay();
        }
        private void btn_CalCam3_Click(object sender, EventArgs e)
        {
            TaskVision.CalVisionXY(ECamNo.Cam02);
            UpdateDisplay();
        }

        private void lbl_LaserSettleTime_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Laser Settle Time (ms)", ref TaskLaser.SettleTime, 0, 500);
            UpdateDisplay();
        }

        private void btn_Cam1Setting_Click(object sender, EventArgs e)
        {
            frmCameraSetting frm = new frmCameraSetting();

            frm.CamNo = 0;
            frm.ShowDialog();
        }
        private void btn_Cam2Setting_Click(object sender, EventArgs e)
        {
            frmCameraSetting frm = new frmCameraSetting();

            frm.CamNo = 1;
            frm.ShowDialog();
        }
        private void btn_Cam3Setting_Click(object sender, EventArgs e)
        {
            frmCameraSetting frm = new frmCameraSetting();

            frm.CamNo = 2;
            frm.ShowDialog();
        }

        private void lblCam1DistPerPixX_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Cam1 DistPerPixX", ref TaskVision.DistPerPixelX[0], 0, 1);
            UpdateDisplay();
        }
        private void lblCam1DistPerPixY_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Cam1 DistPerPixY", ref TaskVision.DistPerPixelY[0], 0, 1);
            UpdateDisplay();
        }
        private void lblCam2DistPerPixX_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Cam2 DistPerPixX", ref TaskVision.DistPerPixelX[1], 0, 1);
            UpdateDisplay();
        }
        private void lblCam2DistPerPixY_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Cam2 DistPerPixY", ref TaskVision.DistPerPixelY[1], 0, 1);
            UpdateDisplay();
        }
        private void lblCam3DistPerPixX_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Cam3 DistPerPixX", ref TaskVision.DistPerPixelX[2], 0, 1);
            UpdateDisplay();
        }
        private void lblCam3DistPerPixY_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Vision Setup, Cam3 DistPerPixY", ref TaskVision.DistPerPixelY[2], 0, 1);
            UpdateDisplay();
        }
    }
}
