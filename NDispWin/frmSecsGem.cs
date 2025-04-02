using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace NDispWin
{
    public partial class frmSecsGem : Form
    {
        public frmSecsGem()
        {
            InitializeComponent();

            cbxProcessState.DataSource = Enum.GetNames(typeof(EProcessState)).ToList();
            cbxProcessState.SelectedText = TFSecsGem.Eq.ProcessState.ToString();
        }
        private void frmSecsGem_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            lblIPPort.Text = $"IPAddress: {TFSecsGem.client.IPAddress}, Port: {TFSecsGem.client.Port}, TimeOut: {TFSecsGem.client.TimeOut}";
            btnConnect.Text = TFSecsGem.client.IsConnected ? "Disconnect" : "Connect";
            btnConnect.BackColor = TFSecsGem.client.IsConnected ? Color.Lime : Color.Red;
            btnOnlineOffline.Text = TFSecsGem.Eq.OnlineOffline == EOnlineOffline.Offline ? "Offline" : "Online";
            btnOnlineOffline.BackColor = TFSecsGem.Eq.OnlineOffline == EOnlineOffline.Offline ? Color.Red : Color.Lime;
            btnLocalRemote.Text = $"{TFSecsGem.Eq.LocalRemote}";
            btnLocalRemote.BackColor = TFSecsGem.Eq.LocalRemote == ELocalRemote.Local ? Color.Yellow : Color.Lime;

            rtbRxTerminal.Text = TFSecsGem.RxTerminalMessage;
        }

        private void tmr1s_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (TFSecsGem.client.IsConnected)
            {
                TFSecsGem.Disconnect();
                UpdateDisplay();
                return;
            }

            try
            {
                TFSecsGem.Connect(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            UpdateDisplay();
        }

        private void btnOnlineOffline_Click(object sender, EventArgs e)
        {
            TFSecsGem.Eq.OnlineOffline = TFSecsGem.Eq.OnlineOffline == EOnlineOffline.Offline ? EOnlineOffline.Online : EOnlineOffline.Offline;
            UpdateDisplay();
        }
        private void btnLocalRemote_Click(object sender, EventArgs e)
        {
            TFSecsGem.Eq.LocalRemote = TFSecsGem.Eq.LocalRemote == ELocalRemote.Local ? ELocalRemote.Remote : ELocalRemote.Local;
            UpdateDisplay();
        }


        private void cbxProcessState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TFSecsGem.Eq.ProcessState = (EProcessState)cbxProcessState.SelectedIndex;
            UpdateDisplay();
        }

        private void btnUploadPP_Click(object sender, EventArgs e)
        {
            TFSecsGem.UploadPP_PPI_PPS(tbPP.Text);
        }
        private void btnRequestPP_Click(object sender, EventArgs e)
        {
            TFSecsGem.RequestPP_PPR(tbPP.Text);
        }

        private void btnAlarmSet_Click(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(tbAlarmCode.Text);

            var almList = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var alm in almList)
            {
                if (alm.Code == code)
                {
                    TFSecsGem.SendAlarm_ARS(alm, true);
                }
            }
         }
        private void btnAlarmClear_Click(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(tbAlarmCode.Text);

            var almList = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var alm in almList)
            {
                if (alm.Code == code)
                {
                    TFSecsGem.SendAlarm_ARS(alm, false);
                }
            }
        }

        private void btnSendTerminalMsg_Click(object sender, EventArgs e)
        {
            TFSecsGem.SendTerminalMsg_TRN(rtbTxTerminal.Text);
        }
    }
}
