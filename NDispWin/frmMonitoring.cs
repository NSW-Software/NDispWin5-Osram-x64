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
    public partial class frmMonitoring : Form
    {
        public frmMonitoring()
        {
            InitializeComponent();
        }

        private void frmMonitoring_Load(object sender, EventArgs e)
        {
            TaskMCamera.MCamera[0].RegisterPictureBoxHandle(pbox1);
            TaskMCamera.MCamera[0].StartGrab();

            TaskMCamera.MCamera[1].RegisterPictureBoxHandle(pbox2);
            TaskMCamera.MCamera[1].StartGrab();
        }

        private void frmMonitoring_Resize(object sender, EventArgs e)
        {
            pbox1.Top = 0;
            pbox1.Left = 0;
            pbox1.Width = this.Width / 2;
            pbox1.Height = pbox1.Width * 3/4;

            pbox2.Top = 0;
            pbox2.Left = pbox1.Width;
            pbox2.Width = pbox1.Width;
            pbox2.Height = pbox1.Height;
        }

        private void frmMonitoring_FormClosing(object sender, FormClosingEventArgs e)
        {
            TaskMCamera.MCamera[0].StopGrab();
            TaskMCamera.MCamera[1].StopGrab();
        }
    }
}
