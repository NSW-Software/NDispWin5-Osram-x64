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
using System.IO;
using System.Threading;
using static NDispWin.SECSGEMConnect2;

namespace NDispWin
{
    public partial class frmSecsGem : Form
    {
        public string Message => TFSecsGem.RxTerminalMessage;
        public frmSecsGem()
        {
            InitializeComponent();

            cbxProcessState.DataSource = Enum.GetNames(typeof(EProcessState1)).ToList();
            cbxProcessState.SelectedText = TFSecsGem.Eq.ProcessState.ToString();
        }
        public EventHandler<EventArgs> UpdateTerminal;
        public bool TriggerUpdateTerminal()
        {
            //Message = msg;
            UpdateTerminal?.Invoke(this, new EventArgs());
            return true;
        }
        private void frmSecsGem_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
            UpdateTerminal += (a, b) =>
            {
                this.Invoke(new Action(() =>
                {
                    rtbRxTerminal.Text = Message;
                }));
            };
            TriggerUpdateTerminal();
        }
        private void UpdateDisplay()
        {
            lblIPPort.Text = $"IPAddress: {TFSecsGem.client.IPAddress}, Port: {TFSecsGem.client.Port}, TimeOut: {TFSecsGem.client.TimeOut}";
            btnConnect.Text = TFSecsGem.client.IsConnected ? "Disconnect" : "Connect";
            btnConnect.BackColor = TFSecsGem.client.IsConnected ? Color.Lime : Color.Red;
            btnOnlineOffline.Text = TFSecsGem.Eq.OnlineOffline == EOnlineOffline.Offline ? "Offline" : "Online";
            btnOnlineOffline.BackColor = TFSecsGem.Eq.OnlineOffline == EOnlineOffline.Offline ? Color.Red : Color.Lime;
            btnLocalRemote.Text = $"{TFSecsGem.Eq.LocalRemote}";
            if(TFSecsGem.E142_Map_On == "1")
            {
                cbE142.CheckState = CheckState.Checked;
            }
            else { cbE142.CheckState = CheckState.Unchecked; }
            if (TFSecsGem.Set_Substrate == "1")
            {
                cbSetSubstrate.CheckState = CheckState.Checked;
            }
            else { cbSetSubstrate.CheckState = CheckState.Unchecked; }

            if (TFSecsGem.Eq.LocalRemote == ELocalRemote.Offline)
            {
                btnLocalRemote.BackColor = Color.Red;
            }
            else
            {
                btnLocalRemote.BackColor = TFSecsGem.Eq.LocalRemote == ELocalRemote.Local ? Color.Yellow : Color.Lime;
            }


            if (TaskConv.TowerLight.TL_Red)
            {
                lblProcessState.Text = "Error";
                lblProcessState.BackColor = Color.Red;
                TFSecsGem.Eq.ProcessState = EProcessState.Error;
            }
            else

            {
                lblProcessState.Text = TFSecsGem.Eq.ProcessState.ToString();
                if (TFSecsGem.Eq.ProcessState == EProcessState.Processing) { lblProcessState.BackColor = Color.Green; }
                else { lblProcessState.BackColor = Color.Yellow; }
            }

            //rtbRxTerminal.Text = TFSecsGem.RxTerminalMessage;
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

        private void btnGenerateIDList_Click(object sender, EventArgs e)
        {
            string fileName = GDefine.RootDir.FullName + $"ID_List_{Application.ProductName}_v{Application.ProductVersion}_{DateTime.Now:yyyyMMddHHmmss}.txt";

            FileStream F = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter W = new StreamWriter(F);

            List<string> list = null;
            try
            {
                list = TEStreamFunc.List();
                W.WriteLine("***StreamFunc, Desc, VIDs***");
                foreach (string s in list)
                {
                    W.WriteLine(s);
                }
                W.WriteLine("");

                list = TEVID.List();
                W.WriteLine("***SVID/DVID/ECID, Desc***");
                W.WriteLine("***SVID 10000~19999, DVID 20000~29999, ECID 30000~39999***");
                foreach (string s in list)
                {
                    W.WriteLine(s);
                }
                W.WriteLine("");

                list = TEMessage.ALID_List();
                W.WriteLine("***ALID, Desc***");
                foreach (string s in list)
                {
                    W.WriteLine(s);
                }
                W.WriteLine("");

                list = TEEvent.CEID_List();
                W.WriteLine("***CEID, Desc, VIDs***");
                foreach (string s in list)
                {
                    W.WriteLine(s);
                }
                W.WriteLine("");

                list = RMCD.List();
                W.WriteLine("***RMCD***");
                foreach (string s in list)
                {
                    W.WriteLine(s);
                }
                W.WriteLine("");

                W.WriteLine("***End of list***");
            }
            catch
            {
            }
            finally
            {
                W.Close();
            }

            MessageBox.Show($"{fileName} was created.");
        }

        private void btnAckTerminalMessage_Click(object sender, EventArgs e)
        {
            TFSecsGem.RxTerminalMessage = "";
            Event.TERMINAL_MESSAGE_ACK.Set();
            TriggerUpdateTerminal();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            rtbInfo.Clear();
            rtbBinCodes.Clear();

            string substrateID = "";
            string binDefStrings = "";
            string binCodeStrings = "";

            if (cbLocal.Checked)
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    InitialDirectory = TaskDisp.OsramICC_LotPath,
                    Title = "Select a XMLFile",
                    Filter = "TXT Files (*.txt)|*.txt|XML Files (*.xml)|*.xml"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        TFSecsGem.rxE142XmlData = File.ReadAllText(ofd.FileName);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else return;

                TFSecsGem.DecodeMap(TFSecsGem.rxE142XmlData, ref substrateID, ref binDefStrings, ref binCodeStrings);

                rtbInfo.Clear();
                rtbInfo.Text = $"{substrateID}\n" + binDefStrings;
                rtbBinCodes.Clear();
                rtbBinCodes.Text = binCodeStrings;
            }
            else
            {
                if (TFSecsGem.E142_Map_On == "0") return;

                TFSecsGem.GAR(tbSubstrateID.Text);
                LotInfo2.sOperatorID = tbBadgeNo.Text;
                Event.MAP_REQUEST.Set();
                int t = Environment.TickCount;
                while (!TFSecsGem.ReceivedXMLMapData)
                {
                    Thread.Sleep(10);
                    if (Environment.TickCount - t >= 30000) return;
                }

                TFSecsGem.DecodeMap(TFSecsGem.rxE142XmlData, ref substrateID, ref binDefStrings, ref binCodeStrings);

                rtbInfo.Clear();
                rtbInfo.Text = $"{substrateID}\n" + binDefStrings;
                rtbBinCodes.Clear();
                rtbBinCodes.Text = binCodeStrings;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (cbLocal.Checked)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    InitialDirectory = TaskDisp.OsramICC_LotPath,
                    Title = "Save as file",
                    Filter = "TXT Files (*.txt)|*.txt|XML Files (*.xml)|*.xml"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string xmlString = "";
                        TFSecsGem.EncodeMap(rtbBinCodes.Text, ref xmlString);
                        File.WriteAllText(sfd.FileName, xmlString);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else return;
            }
            else
            {
                string xmlString = "";
                TFSecsGem.EncodeMap(rtbBinCodes.Text, ref xmlString);
                TFSecsGem.Send($"{nameof(StreamFunc.ERS)},MapData,{xmlString}");
            }

            rtbInfo.Clear();
            rtbBinCodes.Clear();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbSubstrateScanner_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSetSubstrate.Checked)
            {
                TFSecsGem.Set_Substrate = "1";
            }
            else
            {
                TFSecsGem.Set_Substrate = "0";
            }
        }

        private void cbE142_CheckedChanged(object sender, EventArgs e)
        {
            if (cbE142.Checked)
            {
                TFSecsGem.E142_Map_On = "1";
            }
            else
            {
                TFSecsGem.E142_Map_On = "0";
            }
        }
    }
}
