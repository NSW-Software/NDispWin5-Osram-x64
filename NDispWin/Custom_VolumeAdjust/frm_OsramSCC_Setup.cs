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
    public partial class frm_OsramSCC_Setup : Form
    {
        //public Osram_SCC OsramSCC;
        //public static Osram_SCC OsramSCC = new Osram_SCC();

        public frm_OsramSCC_Setup()
        {
            InitializeComponent();
            GControl.LogForm(this);

            tmr_Display.Enabled = true;
        }

        private void frm_OsramSCC_Load(object sender, EventArgs e)
        {
            this.Text = "OSRAM SCC Setup";

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            rbtn_Standalone.Checked = TaskDisp.OsramSCC.SystemMode == Osram_SCC.ESystemMode.StandAlone;
            rbtn_Left.Checked = TaskDisp.OsramSCC.SystemMode == Osram_SCC.ESystemMode.Left;
            rbtn_Right.Checked = TaskDisp.OsramSCC.SystemMode == Osram_SCC.ESystemMode.Right;

            tbox_ClientIPAddress.Text = TaskDisp.OsramSCC.Client.IPAddress;
            tbox_ClientPort.Text = TaskDisp.OsramSCC.Client.Port.ToString();

            tbox_ServerPort.Text = TaskDisp.OsramSCC.Server.Port.ToString();
        }

        public void AddLog(string S)
        {
            if (Visible)
            {
                try
                {
                    lbox_Log.Invoke(new EventHandler(delegate
                           {
                               lbox_Log.Items.Insert(0, DateTime.Now.ToLongTimeString() + " " + S);
                               while (lbox_Log.Items.Count > 100)
                               {
                                   lbox_Log.Items.RemoveAt(lbox_Log.Items.Count - 1);
                               }
                               lbox_Log.SelectedIndex = 0;
                           }));
                }
                catch
                {
                    Log.AddToLog("frm_OsramSCC_Setup.AddLog Invoke Exception Error.");
                };
            }
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TaskDisp.OsramSCC.Client_Connected)
                {
                    TaskDisp.OsramSCC.Client_Connect(tbox_ClientIPAddress.Text, Convert.ToInt32(tbox_ClientPort.Text));
                }
                else
                {
                    TaskDisp.OsramSCC.Client_Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateDisplay();
        }

        private void btn_Tx_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.Client_Tx(tbox_ClientTx.Text);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.SaveSetup();
            Visible = false;
        }

        private void frm_OsramSCC_FormClosing(object sender, FormClosingEventArgs e)
        {
            //tmr_Display.Enabled = false;
            e.Cancel = true;
        }

        //bool b_IsWaiting = false;
        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            if (!TaskDisp.OsramSCC.Client_Connected)
                btn_ClientConnect.Text = "Connect";
            else
                btn_ClientConnect.Text = "Disconnect";

            if (!TaskDisp.OsramSCC.Server_Listening)
                btn_ServerConnect.Text = "Connect";
            else
                btn_ServerConnect.Text = "Disconnect";
        }

        private void btn_ServerTx_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.Server_TX(tbox_ServerTx.Text);
        }

        private void btn_ServerConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TaskDisp.OsramSCC.Server_Listening)
                {
                    TaskDisp.OsramSCC.Server.IPAddress = tbox_ServerIP.Text;
                    TaskDisp.OsramSCC.Server.Port = Convert.ToInt32(tbox_ServerPort.Text);
                    TaskDisp.OsramSCC.Server_Listen();
                }
                else
                {
                    TaskDisp.OsramSCC.Server_Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateDisplay();
        }

        private void rbtn_Standalone_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.Client.Disconnect();
            TaskDisp.OsramSCC.Server.CloseSocket();

            TaskDisp.OsramSCC.SystemMode = Osram_SCC.ESystemMode.StandAlone;

            TaskDisp.OsramSCC.ConnectAll();
        }

        private void rbtn_Left_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.Client.Disconnect();
            TaskDisp.OsramSCC.Server.CloseSocket();

            TaskDisp.OsramSCC.SystemMode = Osram_SCC.ESystemMode.Left;

            TaskDisp.OsramSCC.ConnectAll();
        }

        private void rbtn_Right_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.Client.Disconnect();
            TaskDisp.OsramSCC.Server.CloseSocket();

            TaskDisp.OsramSCC.SystemMode = Osram_SCC.ESystemMode.Right;

            TaskDisp.OsramSCC.ConnectAll();
        }
    }
}
