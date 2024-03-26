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
    public partial class frmMonCamera : Form
    {
        int camCount = 0;
        public frmMonCamera()
        {
            InitializeComponent();

            panel1.Dock = DockStyle.Left;
            panel2.Dock = DockStyle.Fill;
            pbox1.Dock = DockStyle.Fill;
            pbox2.Dock = DockStyle.Fill;

            if (GDefine.MCameraType[0] == GDefine.ECameraType.MVCGenTL) camCount++;
            if (GDefine.MCameraType[1] == GDefine.ECameraType.MVCGenTL) camCount++;
            panel2.Visible = camCount == 2;

            tsslCam1Status.Visible = (GDefine.MCameraType[0] == GDefine.ECameraType.MVCGenTL);
            tsslCam2Status.Visible = (GDefine.MCameraType[1] == GDefine.ECameraType.MVCGenTL);

            if (GDefine.MCameraLocation.Width > 0)
            {
                this.Left = GDefine.MCameraLocation.X;
                this.Top = GDefine.MCameraLocation.Y;
                this.Width = GDefine.MCameraLocation.Width;
                this.Height = GDefine.MCameraLocation.Height;
            }

            if (!(GDefine.MCameraType[0] == GDefine.ECameraType.MVCGenTL || GDefine.MCameraType[1] == GDefine.ECameraType.MVCGenTL))
            {
                GDefine.MCameraAutoShow = false;
                Close();
            }

            AssignCamera();
        }
        private void AssignCamera()
        {
            if (GDefine.MCameraSwapPos)
            {
                pbox1.Parent = panel2;
                pbox2.Parent = panel1;
            }
        }

        private void frmMonitoring_Load(object sender, EventArgs e)
        {
            frmMonitoring_Resize(sender, e);
            Text = "Monitoring Camera";
            try
            {
                TaskMCamera.MCamera[0].RegisterPictureBoxHandle(pbox1);
                TaskMCamera.MCamera[0].StartGrab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            try
            {
                TaskMCamera.MCamera[1].RegisterPictureBoxHandle(pbox2);
                TaskMCamera.MCamera[1].StartGrab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void frmMonitoring_Resize(object sender, EventArgs e)
        {
            switch (camCount)
            {
                case 2: panel1.Width = this.ClientSize.Width / 2; break;
                default: panel1.Width = this.Width; break;
            }
        }
        private void frmMonitoring_FormClosing(object sender, FormClosingEventArgs e)
        {
            GDefine.MCameraLocation.X = this.Left;
            GDefine.MCameraLocation.Y = this.Top;
            GDefine.MCameraLocation.Width = this.Width;
            GDefine.MCameraLocation.Height = this.Height;

            if (TaskMCamera.MCamera[0].IsConnected)
            {
                try
                {
                    TaskMCamera.MCamera[0].StopRecord();
                }
                catch
                {
                }
            }

            if (camCount > 1 && TaskMCamera.MCamera[1].IsConnected)
            {
                try
                {
                    TaskMCamera.MCamera[1].StopRecord();
                }
                catch
                {
                }
            }

            TaskMCamera.MCamera[0].StopGrab();
            TaskMCamera.MCamera[1].StopGrab();
        }
        private void frmMonCamera_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GDefine.MCameraLocation.X = this.Left;
            GDefine.MCameraLocation.Y = this.Top;
            GDefine.MCameraLocation.Width = this.Width;
            GDefine.MCameraLocation.Height = this.Height;

            frmMonCameraSettings frm = new frmMonCameraSettings();
            frm.TopMost = true;
            frm.ShowDialog();

            AssignCamera();
        }

        private void liveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TaskMCamera.MCamera[0].StartGrab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            try
            {
                TaskMCamera.MCamera[1].StartGrab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TaskMCamera.MCamera[0].StopGrab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            try
            {
                TaskMCamera.MCamera[1].StopGrab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TaskMCamera.MCamera[0].IsConnected)
            {
                string filename = "Cam1_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                string fullFilename = GDefine.MonCameraImageDir.FullName + filename;
                try
                {
                    TaskMCamera.MCamera[0].SaveBuffer(fullFilename);
                    tsslStatus.Text = $"Saved: ...{filename}";
                }
                catch (Exception ex)
                {
                    tsslStatus.Text = $"Cam1 save error! {ex.Message.ToString()}";
                }
            }

            if (camCount > 1 && TaskMCamera.MCamera[1].IsConnected)
            {
                string filename = "Cam2_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                string fullFilename = GDefine.MonCameraImageDir.FullName + filename;
                try
                {
                    TaskMCamera.MCamera[0].SaveBuffer(fullFilename);
                    tsslStatus.Text = $"Saved: ...{filename}";
                }
                catch (Exception ex)
                {
                    tsslStatus.Text = $"Cam2 save error! {ex.Message.ToString()}";
                }
            }
        }
        private void startRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TaskMCamera.MCamera[0].IsConnected)
            {
                string filename = "Cam1_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".avi";
                string fullFilename = GDefine.MonCameraVideoDir.FullName + filename;
                try
                {
                    TaskMCamera.MCamera[0].StartRecord(fullFilename);
                    tsslStatus.Text = $"Recording: ...{filename}";
                }
                catch (Exception ex)
                {
                    tsslStatus.Text = $"Cam1 record error! {ex.Message.ToString()}";
                }
            }

            if (camCount > 1 && TaskMCamera.MCamera[1].IsConnected)
            {
                string filename = "Cam2_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".avi";
                string fullFilename = GDefine.MonCameraVideoDir.FullName + filename;
                try
                {
                    TaskMCamera.MCamera[1].StartRecord(fullFilename);
                    tsslStatus.Text = $"Recording: ...{filename}";
                }
                catch (Exception ex)
                {
                    tsslStatus.Text = $"Cam2 record error! {ex.Message.ToString()}";
                }
            }
        }
        private void stopRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TaskMCamera.MCamera[0].IsConnected)
            {
                try
                {
                    TaskMCamera.MCamera[0].StopRecord();
                    tsslStatus.Text = $"Cam1 record end.";
                }
                catch (Exception ex)
                {
                    tsslStatus.Text = $"Cam1 record error! {ex.Message.ToString()}";
                }
            }

            if (camCount > 1 && TaskMCamera.MCamera[1].IsConnected)
            {
                try
                {
                    TaskMCamera.MCamera[1].StopRecord();
                    tsslStatus.Text = $"Cam2 record end.";
                }
                catch (Exception ex)
                {
                    tsslStatus.Text = $"Cam2 record error! {ex.Message.ToString()}";
                }
            }
        }

        private void tmr1s_Tick(object sender, EventArgs e)
        {
            tsslCam1Status.BackColor = TaskMCamera.MCamera[0].IsConnected ? Color.Lime : Color.Red;
            tsslCam2Status.BackColor = TaskMCamera.MCamera[1].IsConnected ? Color.Lime : Color.Red;
        }
    }
}
