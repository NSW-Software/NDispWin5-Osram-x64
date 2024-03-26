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
    public partial class frm_InfoPanel_VolAdjust : Form
    {
        public frm_InfoPanel_VolAdjust()
        {
            InitializeComponent();
            GControl.LogForm(this);
        }

        private void frm_InfoPanel_VolAdjust_Load(object sender, EventArgs e)
        {
            AppLanguage.Func2.UpdateText(this);

            pnl_OsramSCC.Visible = (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC);
            tmr_Display.Enabled = true;
        }

        private void frm_InfoPanel_VolAdjust_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmr_Display.Enabled = false;
        }

        private void UpdateDisplay()
        {
            if (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC)
            {
                lbl_LotID.Text = "LotID" + (char)13 + TaskDisp.OsramSCC.LotID;
                if (TaskDisp.OsramSCC.Enabled)
                {
                    if (TaskDisp.OsramSCC.LotID.Length > 0)
                    {
                        lbl_LotID.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_LotID.BackColor = this.BackColor;
                    }
                }
                lbl_Program.Text = "Recipe" + (char)13 + GDefine.ProgRecipeName;
                if (TaskDisp.OsramSCC.PreMapNo > 0) lbl_Program.Text = lbl_Program.Text + $"_{TaskDisp.OsramSCC.PreMapNo}";
                lbl_Program.Text = lbl_Program.Text + (char)13 + DispProg.Target_Weight.ToString("f3") + " (mg)";
                lbl_OsramSCC.Text = "OsramSCC" + (char)13 + TaskDisp.OsramSCC.Client.IPAddress + ":" + TaskDisp.OsramSCC.Client.Port.ToString("d4");
                if (TaskDisp.OsramSCC.Enabled)
                {
                    if (TaskDisp.OsramSCC.Client_Connected)
                    {
                        lbl_OsramSCC.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_OsramSCC.BackColor = Color.Red;
                    }
                }
                else
                {
                    lbl_OsramSCC.Text = "OsramSCC" + (char)13 + " Disabled";
                    lbl_OsramSCC.BackColor = Color.Orange;
                }

                btn_OsramSCC.Text = "OsramSCC" + (char)13 + DispProg.Disp_Weight[0].ToString("f3") + "," + DispProg.Disp_Weight[1].ToString("f3") + " (mg)";
            }

            lbl_LotID.Visible = (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC);
            lbl_Program.Visible = (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC);
            lbl_OsramSCC.Visible = (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC);
            btn_OsramSCC.Visible = (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC);
        }

        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            UpdateDisplay();
        }

        private void btn_OsramSCC_Click(object sender, EventArgs e)
        {
            frm_OsramSCC_LotInfo frm = new frm_OsramSCC_LotInfo();
            frm.ShowDialog();
        }

        private void lbl_OsramSCC_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TaskDisp.OsramSCC.Client_Connected)
                {
                    TaskDisp.OsramSCC.Client_Connect();
                }
                if (!TaskDisp.OsramSCC.Server_Listening)
                {
                    TaskDisp.OsramSCC.Server_Listen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
