using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDispWin
{
    public partial class frmMonCameraSettings : Form
    {
        public frmMonCameraSettings()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
        }

        private void frmMonCameraSettings_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void frmMonCameraSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            GDefine.ProcessVideoPath = tboxProcessImageSaveFolder.Text;

            GDefine.SaveSystemConfig("");
        }

        private void UpdateDisplay()
        {
            lblExposure1.Text = $"{GDefine.MCameraExposure[0]:f1}";
            lblGain1.Text = $"{GDefine.MCameraGain[0]:f1}";
            lblExposure2.Text = $"{GDefine.MCameraExposure[1]:f1}";
            lblGain2.Text = $"{GDefine.MCameraGain[1]:f1}";

            cbReverseX1.Checked = GDefine.MCameraReverseX[0];
            cbReverseX2.Checked = GDefine.MCameraReverseX[1];
            cbReverseY1.Checked = GDefine.MCameraReverseY[0];
            cbReverseY2.Checked = GDefine.MCameraReverseY[1];

            cbAutoShow.Checked = GDefine.MCameraAutoShow;
            cbSwapCamera.Checked = GDefine.MCameraSwapPos;

            tboxProcessImageSaveFolder.Text = GDefine.ProcessVideoPath;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GDefine.MAX_MCAMERA; i++)
            {
                if (!TaskMCamera.MCamera[i].IsConnected) continue;

                TaskMCamera.MCamera[i].StopGrab();
                try
                {
                    TaskMCamera.MCamera[i].Exposure = GDefine.MCameraExposure[i];
                    TaskMCamera.MCamera[i].Gain = GDefine.MCameraGain[i];
                    TaskMCamera.MCamera[i].ReverseX = GDefine.MCameraReverseX[i];
                    TaskMCamera.MCamera[i].ReverseY = GDefine.MCameraReverseY[i];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    TaskMCamera.MCamera[i].StartGrab();
                }
            }

            string msg = "";
            if (!TaskMCamera.MCamera[0].IsConnected)
            {
                msg = msg + "Camera Right ";
            }
            if (!TaskMCamera.MCamera[1].IsConnected)
            {
                msg = msg + (msg.Length > 0 ? ", " : "") + "Camera Left ";
            }

            if (msg.Length > 0)
            {
                MessageBox.Show(msg + " not connected!");
            }
        }

        double minExposure = 10;
        double maxExposure = 20000;
        double minGain = 0;
        double maxGain = 24;
        private void lblExposure1_Click(object sender, EventArgs e)
        {
            double d = GDefine.MCameraExposure[0];
            UC.AdjustExec("Cam Right Exposure Time (us)", ref d, minExposure, maxExposure);
            GDefine.MCameraExposure[0] = d;
            UpdateDisplay();
        }
        private void lblGain1_Click(object sender, EventArgs e)
        {
            double d = GDefine.MCameraGain[0];
            UC.AdjustExec("Cam Right Gain Time (us)", ref d, minGain, maxGain);
            GDefine.MCameraGain[0] = d;
            UpdateDisplay();
        }
        private void cbReverseX1_Click(object sender, EventArgs e)
        {
            GDefine.MCameraReverseX[0] = (sender as CheckBox).Checked;
        }
        private void cbReverseY1_Click(object sender, EventArgs e)
        {
            GDefine.MCameraReverseY[0] = (sender as CheckBox).Checked;
        }

        private void lblExposure2_Click(object sender, EventArgs e)
        {
            double d = GDefine.MCameraExposure[1];
            UC.AdjustExec("Cam Left Exposure Time (us)", ref d, minExposure, maxExposure);
            GDefine.MCameraExposure[1] = d;
            UpdateDisplay();
        }
        private void lblGain2_Click(object sender, EventArgs e)
        {
            double d = GDefine.MCameraGain[1];
            UC.AdjustExec("Cam Left Gain Time (us)", ref d, minGain, maxGain);
            GDefine.MCameraGain[1] = d;
            UpdateDisplay();
        }
        private void cbReverseX2_Click(object sender, EventArgs e)
        {
            GDefine.MCameraReverseX[1] = (sender as CheckBox).Checked;
        }
        private void cbReverseY2_Click(object sender, EventArgs e)
        {
            GDefine.MCameraReverseY[1] = (sender as CheckBox).Checked;
        }

        private void cbAutoShow_Click(object sender, EventArgs e)
        {
            GDefine.MCameraAutoShow = (sender as CheckBox).Checked;
            UpdateDisplay();
        }

        private void cbSwapCamera_Click(object sender, EventArgs e)
        {
            GDefine.MCameraSwapPos = (sender as CheckBox).Checked;
            UpdateDisplay();
        }
    }
}
