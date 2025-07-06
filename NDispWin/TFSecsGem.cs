using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.Windows.Forms;
using Automation.BDaq;
using System.Xml.Linq;
using System.Drawing;
using static NDispWin.Intf;

namespace NDispWin
{
    public class TClient2
    {
        public string IPAddress = "127.0.0.1";
        public int Port = 9000;
        public int TimeOut = 30000;//second

        public string RxData = "";
        public string TxData = "";

        private static Mutex mutexRX = new Mutex();
        internal class SocketPacket
        {
            public System.Net.Sockets.Socket m_currentSocket;
            public byte[] dataBuffer = new byte[1];
        }

        private AsyncCallback pfnCallBack;
        private Socket socket;

        public TClient2()
        {
            this.ConnectedEvent += new OnConnected(ConnectedToServerEvent);
            this.DisconnectedEvent += new OnDisconnected(DisconnectedFromServerEvent);

            this.FrameSendEvent += new OnFrameSend(OnFrameSendEvent);
            this.FrameStartReceivedEvent += new OnFrameStartReceived(Server_FrameStartReceivedEvent);
            this.FrameEndReceivedEvent += new OnFrameEndReceived(Server_FrameEndReceivedEvent);
        }

        // Start waiting for data from the TClient
        private void WaitForData(Socket Socket)
        {
            try
            {
                if (pfnCallBack == null)
                {
                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected TClient
                    pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket SocPkt = new SocketPacket();
                SocPkt.m_currentSocket = Socket;
                // Start receiving any data written by the connected TClient asynchronously
                Socket.BeginReceive(SocPkt.dataBuffer, 0, SocPkt.dataBuffer.Length, SocketFlags.None, pfnCallBack, SocPkt);
            }
            catch (SocketException se)
            {
                //MessageBox.Show(se.Message);
                throw;
            }
        }

        public delegate void OnConnected();
        public event OnConnected ConnectedEvent;
        private void ConnectedToServerEvent() { }

        public delegate void OnDisconnected();
        public event OnDisconnected DisconnectedEvent;
        private void DisconnectedFromServerEvent() { }

        public delegate void OnFrameSend();
        public event OnFrameSend FrameSendEvent;
        private void OnFrameSendEvent() { }

        public delegate void OnFrameStartReceived();
        public event OnFrameStartReceived FrameStartReceivedEvent;
        private void Server_FrameStartReceivedEvent() { }
        public delegate void OnFrameEndReceived();
        public event OnFrameEndReceived FrameEndReceivedEvent;
        private void Server_FrameEndReceivedEvent() { }

        private frmSecsGem _frmSecsGem;
        // This the call back function which will be invoked when the socket
        // detects any TClient writing of data on the stream
        private void OnDataReceived(IAsyncResult asyn)
        {
            if (socket != null && !socket.Connected)
            {
                DisconnectedEvent();
                return;
            }

            mutexRX.WaitOne();

            try
            {
                SocketPacket SocPkt = (SocketPacket)asyn.AsyncState;

                // Complete the BeginReceive() asynchronous call by EndReceive() method
                // which will return the number of characters written to the stream 
                // by the TClient
                int iRx = 0;
                iRx = SocPkt.m_currentSocket.EndReceive(asyn);

                if (iRx == 0)
                {
                    Disconnect();
                    DisconnectedEvent();
                }

                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(SocPkt.dataBuffer, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);
                string sData = szData.Remove(charLen);

                if (sData.Contains((char)0x02))
                    FrameStartReceivedEvent();
                RxData = RxData + sData;
                if (RxData.Contains((char)0x03))
                    FrameEndReceivedEvent();

                // Continue the waiting for data on the Socket
                WaitForData(SocPkt.m_currentSocket);
            }
            catch (ObjectDisposedException)
            {
                Disconnect();
                DisconnectedEvent();
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                Disconnect();
                //MessageBox.Show(se.Message);
                DisconnectedEvent();
            }
            finally
            {
                mutexRX.ReleaseMutex();
            }
        }

        public void Connect(string ServerIPAddress, int Port)
        {
            if (ServerIPAddress == "" || Port == 0)
            {
                throw new Exception("IP Address and Port Number are required to connect to the Server\n");
            }
            try
            {
                // Create the socket instance
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Cet the remote IP address
                IPAddress ip = System.Net.IPAddress.Parse(ServerIPAddress);
                //int iPortNo = System.Convert.ToInt16(tbox_Port.Text);
                // Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, Port);
                // Connect to the remote host
                socket.Connect(ipEnd);
                if (socket.Connected)
                {
                    ConnectedEvent();
                    //Wait for data asynchronously 
                    WaitForData(socket);
                }
            }
            catch (SocketException se)
            {
                throw new Exception("Connection failed, is the server running ?\n" + se.Message);
            }
        }
        public void Disconnect()
        {
            if (socket != null)
            {
                try
                {
                    if (socket.Connected)
                    {
                        socket.Shutdown(SocketShutdown.Both); // Graceful shutdown
                    }
                    socket.Close();
                }
                catch (SocketException se)
                {
                    // Log or handle error if needed
                    throw new Exception("Disconnect failed:\n" + se.Message);
                }
                finally
                {
                    socket = null;
                }
            }
        }

        public bool IsConnected
        {
            get
            {
                if (socket == null) return false;
                return socket.Connected;
            }
        }

        public int Send(string Data)
        {
            Object objData = Data;
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
            try
            {
                if (socket != null)
                {
                    if (socket.Connected)
                    {
                        socket.Send(byData);
                        return Data.Length;
                    }
                }
                return -1;
            }
            catch { throw; }
        }
        public int SendFrame(string Data)
        {
            if (Data.Length == 0) return 0;
            TxData = Data;
            Data = (char)0x02 + Data + (char)0x03;

            FrameSendEvent();
            return Send(Data);
        }

        public int BufferFrameCount
        {
            get { return RxData.Split((char)0x03).Length - 1; }
        }
        public int ReceiveFrame(ref string Data)
        {
            mutexRX.WaitOne();
            try
            {
                if (RxData.Length == 0) return 0;
                if (!RxData.Contains((char)0x03)) return 0;

                string Temp = RxData;
                Temp = Temp.Remove(0, 1);
                if (Temp.Length > Temp.IndexOf((char)0x03))
                {
                    Temp = Temp.Remove(RxData.IndexOf((char)0x03) - 1);
                }
                RxData = RxData.Remove(0, RxData.IndexOf((char)0x03) + 1);

                Data = Temp;
            }
            catch { throw; }
            finally { mutexRX.ReleaseMutex(); }
            return Data.Length;
        }
    }

    enum EOnlineOffline
    {
        Online,
        Offline//No communication with host (SECS/GEM not active).
    }
    enum ELocalRemote
    {
        Offline,//Host connected, but Host can't monitor and commnad.
        Local,//Host connected, but local operator has control. Host can monitor but not command.
        Remote//Host has full control. Can send commands, start/stop processes, etc.
    }
    enum EProcessState
    {
        Idle,//Equipment is not processing any jobs.
        Setup,//Preparing for a job (loading recipe, aligning, purging, etc.)
        Processing,//Actively running a job or executing a recipe.
        Paused,//Job was temporarily halted.
        Completed,//Job is finished.
        Aborted,//Error Job was aborted or failed due to an error or alarm.
        Error// Error prompt
    }

    enum EProcessState1
    {
        Idle,//Equipment is not processing any jobs.
        Setup,//Preparing for a job (loading recipe, aligning, purging, etc.)
        Processing,//Actively running a job or executing a recipe.
        Paused,//Job was temporarily halted.
        Completed,//Job is finished.
        Aborted//Error Job was aborted or failed due to an error or alarm.
        
    }

    public enum EControlState
    {
        EquipmentOffline,
        EquipmentLocal,
        EquipmentRemote,
        HostOffline,
        HostOnline
    }

    public enum EPPError
    {
        RecipeNotFound,
        LoadFail,
        EquipmentBusy
    }

    class TEStreamFunc
    {
        public string SF = "";
        public string Desc = "";
        public List<int> VIDs;//related VIDs

        public TEStreamFunc(string sf, string desc, List<int> vids = null)
        {
            SF = sf;
            Desc = desc;
            VIDs = vids;
        }
        public static List<string> List()
        {
            var sSFList = typeof(StreamFunc).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.FieldType == typeof(TEStreamFunc))
            .Select(x => (TEStreamFunc)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var sSF in sSFList)
            {
                string vids = sSF.VIDs == null ? "" : $",VID:{string.Join(",", sSF.VIDs)}";
                list.Add($"{sSF.SF},{sSF.Desc}{vids}");
            }
            return list;
        }
    }
    class StreamFunc
    {
        public static TEStreamFunc S1F0 = new TEStreamFunc("S1F0", "H<->E, Abort Transaction.");
        public static TEStreamFunc SSR = new TEStreamFunc("S1F3", "H->E, Equipment Status Request.", new List<int> { 11000, 11001});//tested
        public static TEStreamFunc SSD = new TEStreamFunc("S1F4", "H<-E, Equipment Status Data.", new List<int> { 11000, 11001 });//tested
        public static TEStreamFunc SVNR = new TEStreamFunc("S1F11", "H->E, Status Variable Namelist Request.");//tested
        public static TEStreamFunc SVNRR = new TEStreamFunc("S1F12", "H<-E, Status Variable Namelist Reply.");//tested
        public static TEStreamFunc ROFL = new TEStreamFunc("S1F15", "H->E, Request Offline.");//tested
        public static TEStreamFunc OFLA = new TEStreamFunc("S1F16", "H<-E, Request Offline Ack.");//tested
        public static TEStreamFunc RONL = new TEStreamFunc("S1F17", "H->E, Request Online.");//tested
        public static TEStreamFunc ONLA = new TEStreamFunc("S1F18", "H<-E, Request Online Ack.");//tested
        public static TEStreamFunc CENR = new TEStreamFunc("S1F23", "H<-E, Collection Event Namelist Request.");//tested
        public static TEStreamFunc CEN = new TEStreamFunc("S1F24", "H->E, Collection Event Namelist.");//tested

        public static TEStreamFunc S2F0 = new TEStreamFunc("S2F0", "H<->E, Abort Transaction.");
        public static TEStreamFunc ECR = new TEStreamFunc("S2F13", "H->E, Equipment Constant Request.");//tested
        public static TEStreamFunc ECD = new TEStreamFunc("S2F14", "H<-E, Equipment Constant Data.");//tested
        public static TEStreamFunc ECS = new TEStreamFunc("S2F15", "H->E, New Equipment Constant Send.");
        public static TEStreamFunc ECA = new TEStreamFunc("S2F16", "H<-E, New Equipment Constant Acknowledge.");
        public static TEStreamFunc TIS = new TEStreamFunc("S2F23", "H->E, Trace Initialize Send.");
        public static TEStreamFunc TIA = new TEStreamFunc("S2F24", "H->E, Trace Initialize Acknowledge.");//tested
        public static TEStreamFunc ECNR = new TEStreamFunc("S2F29", "H->E, Equipment Constant Namelist Request.");//tested
        public static TEStreamFunc ECN = new TEStreamFunc("S2F30", "H<-E, Equipment Constant Namelist.");//tested

        public static TEStreamFunc S5F0 = new TEStreamFunc("S5F0", "H<->E, Abort Transaction.");
        public static TEStreamFunc ARS = new TEStreamFunc("S5F1", "H<-E, Alarm Report Send.");//tested
        public static TEStreamFunc ARA = new TEStreamFunc("S5F2", "H->E, Alarm Report Acknowledge.");//tested
        public static TEStreamFunc EAS = new TEStreamFunc("S5F3", "H->E, Enable/Disable Alarm Send.");//tested
        public static TEStreamFunc EAA = new TEStreamFunc("S5F4", "H<-E, Enable/Disable Alarm Acknowledge.");//tested
        public static TEStreamFunc LAR = new TEStreamFunc("S5F5", "H->E, List Alarm Request.");//tested
        public static TEStreamFunc LAD = new TEStreamFunc("S5F6", "H<-E, List Alarm Data.");//tested
        public static TEStreamFunc LEAR = new TEStreamFunc("S5F7", "H->E, List Enabled Alarm Request.");//tested
        public static TEStreamFunc LEAD = new TEStreamFunc("S5F8", "H<-E, List Enabled Alarm Data.");//tested

        public static TEStreamFunc ERS = new TEStreamFunc("S6F11", "H<-E, Event Report Send.");//tested
        public static TEStreamFunc ERA = new TEStreamFunc("S6F12", "H->E, Event Report Acknowledge.");//tested
        public static TEStreamFunc VID = new TEStreamFunc("S6F3", "H->E, Equipment Status Request.", new List<int> { 11000, 11001 });//tested

        public static TEStreamFunc S7F0 = new TEStreamFunc("S7F0", "H<->E, Abort Transation.");
        public static TEStreamFunc PPI = new TEStreamFunc("S7F1", "H<->E, Process Program Inquire.");//H->E tested
        public static TEStreamFunc PPG = new TEStreamFunc("S7F2", "H<->E, Process Program Grant.");//tested
        public static TEStreamFunc PPS = new TEStreamFunc("S7F3", "H<->E, Process Program Sent.");//H->E tested
        public static TEStreamFunc PPA = new TEStreamFunc("S7F4", "H<->E, Process Program Acknowledge.");//tested
        public static TEStreamFunc PPR = new TEStreamFunc("S7F5", "H<->E, Process Program Request.");//H->E tested
        public static TEStreamFunc PPD = new TEStreamFunc("S7F6", "H<->E, Process Program Data.");//tested
        public static TEStreamFunc DPS = new TEStreamFunc("S7F17", "H->E, Delete Process Program Send.");//tested

        public static TEStreamFunc S10F0 = new TEStreamFunc("S10F0", "H<->E, Abort Transaction.");
        public static TEStreamFunc TRN = new TEStreamFunc("S10F1", "H->E, Terminal Request.");
        public static TEStreamFunc TRA = new TEStreamFunc("S10F2", "H->E, Terminal Request Acknowledge.");
        public static TEStreamFunc VTN = new TEStreamFunc("S10F3", "H->E, Terminal Display,Single.");
        public static TEStreamFunc VTA = new TEStreamFunc("S10F4", "H<-E, Terminal Display,Single Acknowledge.");

        public static TEStreamFunc S14F0 = new TEStreamFunc("S14F0", "H<->E, Abort Transaction.");
        public static TEStreamFunc GAR = new TEStreamFunc("S14F1", "H<->E, Get Attribute Request.");
        public static TEStreamFunc GAD = new TEStreamFunc("S14F2", "H<->E, Get Attribute Data.");
        public static TEStreamFunc SAR = new TEStreamFunc("S14F3", "H<->E, Set Attribute Request.");
        public static TEStreamFunc SAD = new TEStreamFunc("S14F4", "H<->E, Set Attribute Data.");
    }

    class TFSecsGem
    {
        private static int Timeout = 10000;

        public static EOnlineOffline OnlineOffline = EOnlineOffline.Offline;
        public static ELocalRemote LocalRemote = ELocalRemote.Local;
        public static ELocalRemote PrevLocalRemote = ELocalRemote.Local;
        public static EProcessState ProcessState = EProcessState.Idle;
        public static EProcessState PrevProcessState = EProcessState.Idle;
        public static EControlState ControlState = EControlState.EquipmentLocal;
        public static EControlState PrevControlState = EControlState.EquipmentLocal;
        public static EPPError PPError;
        public static string PPChangeStatus = "Success";
        public static string PPFormat = "Unformatted";

        public static string RxTerminalMessage = "";
        public static string TxTerminalMessage = "";

        public static string ChangedECID = "";
        public static string ChangedECValue = "";
        public static string Set_Substrate = "0";
        public static string E142_Map_On = "0";

        public static string Map_Update_Content = "";
        public static TClient2 client = new TClient2();
        public static void Start()
        {
            client.ConnectedEvent -= OnConnectedEvent;
            client.DisconnectedEvent -= OnDisconnectedEvent;
            client.FrameSendEvent -= OnFrameSendEvent;
            client.FrameEndReceivedEvent -= OnFrameEndReceivedEvent;

            client.ConnectedEvent += OnConnectedEvent;
            client.DisconnectedEvent += OnDisconnectedEvent;
            client.FrameSendEvent += OnFrameSendEvent;
            client.FrameEndReceivedEvent += OnFrameEndReceivedEvent;
        }
        public static void Stop()
        {
            client.ConnectedEvent -= OnConnectedEvent;
            client.DisconnectedEvent -= OnDisconnectedEvent;
            client.FrameSendEvent -= OnFrameSendEvent;
            client.FrameEndReceivedEvent -= OnFrameEndReceivedEvent;
        }

        public static void Connect(string ipAddress, int port)
        {
            try
            {
                Log.SecsGem.WriteByMonthDay("Connecting to " + ipAddress + " : " + port.ToString());
                client.Connect(ipAddress, port);
            }
            catch
            {
                Log.SecsGem.WriteByMonthDay("Connect fail.");
            }
        }
        public static void Connect()
        {
            Log.SecsGem.WriteByMonthDay("Connecting");
            Start();
            client.Connect(client.IPAddress, client.Port);
        }
        public static void Disconnect()
        {
            try
            {
                Log.SecsGem.WriteByMonthDay("Disconnecting");
                LocalRemote = ELocalRemote.Local;
                client.Disconnect();
                Stop();
            }
            catch
            {
                Log.SecsGem.WriteByMonthDay("Disconnect fail.");
            }
        }
        public static bool IsConnected
        {
            get
            {
                if (!client.IsConnected) OnlineOffline = EOnlineOffline.Offline;
                return client.IsConnected;
            }
        }

        public static void Send(string outMsg)
        {
            string data = outMsg;
            client.SendFrame(data);
        }

        static ReaderWriterLockSlim slim = new ReaderWriterLockSlim();
        private static void OnConnectedEvent()
        {
            Log.SecsGem.WriteByMonthDay("Connected.");
        }
        private static void OnDisconnectedEvent()
        {
            Log.SecsGem.WriteByMonthDay("Disconnected.");
        }
        private static void OnFrameSendEvent()
        {
            Log.SecsGem.WriteByMonthDay($"< {client.TxData}");
        }

        static List<string> rxPPGData;
        static List<string> rxPPDData;
        public static bool ReceivedXMLMapData = false;
        public static string rxE142XmlData;
        public static Dictionary<string, string> SubstrateStatus = new Dictionary<string, string>();
        public static string SubstrateID;
        private static void OnFrameEndReceivedEvent()
        {
            string rxRawData = "";
            while (client.BufferFrameCount > 0)
            {
                if (client.ReceiveFrame(ref rxRawData) != 0)
                {
                    Log.SecsGem.WriteByMonthDay($"> {rxRawData}");
                }

                string[] rxSplitData = rxRawData.Split(new[] { "," }, StringSplitOptions.None);
                string data0 = rxSplitData[0].ToUpper();
                data0 = data0.Contains("_") ? data0.Split('_')[0] : data0;
                switch (data0)
                {
                    #region S1
                    case nameof(StreamFunc.SSR):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = SSR_GetList(requestList);
                            Send($"{nameof(StreamFunc.SSD)},{string.Join(",", responseList)}");
                            break;
                        }
                    case nameof(StreamFunc.SVNR):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = SVNR_GetList(requestList);
                            Send($"{nameof(StreamFunc.SVNRR)},{string.Join(",", responseList)}");
                            break;
                        }
                    case nameof(StreamFunc.ROFL):
                        {
                            if (!IsConnected) Send(nameof(StreamFunc.S1F0));
                            OnlineOffline = EOnlineOffline.Offline;
                            Event.SECSGEM_EQ_SET_OFFLINE.Set();
                            break;
                        }
                    case nameof(StreamFunc.RONL):
                        {
                            if (!IsConnected) Send(nameof(StreamFunc.S1F0));
                            OnlineOffline = EOnlineOffline.Online;
                            Event.SECSGEM_EQ_SET_ONLINE.Set();
                            break;
                        }
                    case "REOFL":
                        {
                            if (!IsConnected) Send(nameof(StreamFunc.S1F0));
                            LocalRemote = ELocalRemote.Offline;
                            PrevControlState = ControlState;
                            ControlState = EControlState.EquipmentOffline;
                            Event.SECSGEM_CONTROL_STATE_CHANGE.Set();
                            break;
                        }
                    case "REOLL":
                        {
                            if (!IsConnected) Send(nameof(StreamFunc.S1F0));
                            LocalRemote = ELocalRemote.Local;
                            PrevControlState = ControlState;
                            ControlState = EControlState.EquipmentLocal;
                            Event.SECSGEM_CONTROL_STATE_CHANGE.Set();
                            break;
                        }
                    case "REONL":
                        {
                            if (!IsConnected) Send(nameof(StreamFunc.S1F0));
                            LocalRemote = ELocalRemote.Remote;
                            PrevControlState = ControlState;
                            ControlState = EControlState.EquipmentRemote;
                            Event.SECSGEM_CONTROL_STATE_CHANGE.Set();
                            break;
                        }
                    case nameof(StreamFunc.CENR):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = CENR_GetList(requestList);
                            Send($"{nameof(StreamFunc.CEN)},{string.Join(",", responseList)}");
                            break;
                        }
                    #endregion
                    #region S2
                    case nameof(StreamFunc.ECR):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = ECR_GetList(requestList);
                            Send($"{nameof(StreamFunc.ECD)},{string.Join(",", responseList)}");
                            break;
                        }
                    case nameof(StreamFunc.ECNR):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = ECNR_GetList(requestList);
                            Send($"{nameof(StreamFunc.ECN)},{string.Join(",", responseList)}");
                            break;
                        }
                    case nameof(StreamFunc.ECS):
                        {
                            try
                            {
                                Send($"{nameof(StreamFunc.ECA)}");
                                if (rxSplitData[1] != "")
                                {
                                    for (int i = 1; i < rxSplitData.Length; i += 2)
                                    {
                                        TEVID.GetFieldFromId(Convert.ToInt32(rxSplitData[i])).Value = rxSplitData[i + 1];
                                        ChangedECID = rxSplitData[i];
                                        ChangedECValue = rxSplitData[i + 1];
                                        Send($"ERS1,5020,{ChangedECID},{ChangedECValue}");
                                    }
                                }

                            }
                            catch
                            {
                                Send($"{nameof(StreamFunc.ECA)},FAIL.");
                            }
                            break;
                        }
                    case nameof(StreamFunc.TIS):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = SSR_GetList(requestList);
                            Send($"{nameof(StreamFunc.TIA)},{rxSplitData[1]},{string.Join(",", responseList)}");
                            break;
                        }
                    #endregion
                    #region S5
                    case nameof(StreamFunc.ARS):
                        {
                            break;
                        }
                    case nameof(StreamFunc.ARA):
                        {
                            break;
                        }
                    case nameof(StreamFunc.EAS):
                        {
                            rxSplitData = rxSplitData.Concat(Enumerable.Repeat("", 2)).ToArray();//add 2 empty strings

                            var aled = rxSplitData[1];
                            var alid = rxSplitData[2];

                            if (alid == "0")
                            {
                                var msglist = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                                    .Where(x => x.FieldType == typeof(TEMessage))
                                    .Select(x => (TEMessage)x.GetValue(null)).ToArray();

                                foreach (var msg in msglist)
                                {
                                    msg.Enabled = aled == "128" ? true : false;
                                }
                            }
                            else
                            {
                                var msglist = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                                    .Where(x => x.FieldType == typeof(TEMessage))
                                    .Select(x => (TEMessage)x.GetValue(null)).ToArray();

                                foreach (var msg in msglist)
                                {
                                    if (alid == $"{msg.Code}")
                                        msg.Enabled = aled == "128" ? true : false;
                                }
                            }
                            Send($"{nameof(StreamFunc.EAA)}");
                            break;
                        }
                    case nameof(StreamFunc.LAR):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = LAR_GetList(requestList);
                            Send($"{nameof(StreamFunc.LAD)},{string.Join(",", responseList)}");
                            break;
                        }
                    case nameof(StreamFunc.LEAR):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = LEAR_GetList();
                            Send($"{nameof(StreamFunc.LEAD)},{string.Join(",", responseList)}");
                            break;
                        }
                    case "LDA":
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = LDA_GetList();
                            Send($"LDA,{string.Join(",", responseList)}");
                            break;
                        }
                    case "LEA":
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = LEA_GetList();
                            Send($"LEA,{string.Join(",", responseList)}");
                            break;
                        }
                    #endregion

                    #region S6
                    case nameof(StreamFunc.VID):
                        {
                            List<int> requestList = new List<int>();
                            foreach (string d in rxSplitData)
                            {
                                if (int.TryParse(d, out int vid))
                                {
                                    requestList.Add(vid);
                                }
                            }
                            List<string> responseList = VID_GetList(requestList);
                            Send($"{nameof(StreamFunc.VID)},{string.Join(",", responseList)}");
                            break;
                        }
                    #endregion
                    #region S7
                    case nameof(StreamFunc.PPI):
                        Send(nameof(StreamFunc.PPG));
                        break;
                    case nameof(StreamFunc.PPG):
                        rxPPGData = rxSplitData.ToList();
                        break;
                    case nameof(StreamFunc.PPS):
                        if (rxSplitData.Length >= 3)
                        {
                            try
                            {
                                bool isCreated = false;
                                var ppid = rxSplitData[1];
                                var filePath = rxSplitData[2];
                                var file = GDefine.RecipeDir.FullName + ppid + ".xml";
                                string ppbody = "";
                                string fullFilename = GDefine.ProgPath + "\\" + ppid + "." + GDefine.ProgExt;
                                if (!File.Exists(fullFilename))
                                {
                                    isCreated = true;
                                }
                                if (File.Exists(filePath))
                                {
                                    ppbody = File.ReadAllText(filePath);
                                }
                                StringBuilder xmlbuilder = new StringBuilder();
                                for(int i = 0; i< ppbody.Length; i += 8)
                                {
                                    string byteString = ppbody.Substring(i, 8);
                                    char character = (char)Convert.ToByte(byteString,2);
                                    xmlbuilder.Append(character);
                                }
                                string xmlcontent = xmlbuilder.ToString();
                                slim.EnterWriteLock();
                                try
                                {
                                    using (StreamWriter writer = new StreamWriter(file))
                                    {
                                        writer.Write(xmlcontent);
                                        //a.Close();
                                    }
                                }
                                finally
                                {
                                    slim.ExitWriteLock();
                                }

                                if (!DispProg.LoadProgName(ppid))
                                {
                                    Send(nameof(StreamFunc.PPA) + ",LoadProgNameFail");
                                    PPError = EPPError.LoadFail;
                                    Event.SECSGEM_PP_VERIFICATION.Set();
                                }
                                    
                                else
                                { 
                                    Send(nameof(StreamFunc.PPA) + ",Success");
                                    if (isCreated)
                                    {
                                        Event.SECSGEM_PP_CREATE.Set();
                                        isCreated = false;
                                    }
                                    else
                                    {
                                        Event.SECSGEM_PP_CHANGE.Set();
                                    }
                                    
                                }
                                    
                            }
                            catch (Exception ex)
                            {
                                Send(nameof(StreamFunc.PPA) + ",LoadProgNameFail");
                                PPError = EPPError.LoadFail;
                                Event.SECSGEM_PP_VERIFICATION.Set();
                            }
                        }
                        else
                        {
                            Send(nameof(StreamFunc.PPA) + ",LoadProgNameFail");
                            PPError = EPPError.LoadFail;
                            Event.SECSGEM_PP_VERIFICATION.Set();
                        }
                            
                        break;
                    case nameof(StreamFunc.PPR):
                        {
                            var ppid = rxSplitData[1];
                            var fileName = GDefine.RecipeDir.FullName + ppid + ".xml";

                            if (File.Exists(fileName))
                            {
                                string content;
                                using (StreamReader reader = new StreamReader(fileName))
                                {
                                    content = reader.ReadToEnd();
                                }

                                var slist = new List<string> { content };

                                Send(nameof(StreamFunc.PPD) + "," + string.Join(",", slist));
                            }
                            else
                            {
                                Send(nameof(StreamFunc.S7F0)); //Indicating file not found
                            }
                            break;
                        }
                    case nameof(StreamFunc.PPD):
                        rxPPDData = rxSplitData.ToList();
                        break;
                    case nameof(StreamFunc.DPS):
                        {
                            Event.SECSGEM_PP_DELETE.Set();
                        }
                        break;
                    #endregion
                    #region S10
                    case nameof(StreamFunc.VTN):
                        {
                            rxSplitData = rxSplitData.Concat(Enumerable.Repeat("", 1)).ToArray();//add 1 empty strings
                            RxTerminalMessage = rxSplitData[2];
                            bool isFormOpen = Application.OpenForms["frmSecsGem"] != null;
                            if(!isFormOpen){ frm_Main.RunTerminal(); }
                            
                            var frms = Application.OpenForms.OfType<frmSecsGem>().ToArray();
                            if (frms.Length < 1) break;
                            foreach(var frm in frms)
                            {
                                frm.TriggerUpdateTerminal();
                            }
                            

                            break;
                        }
                    #endregion
                    #region S14
                    case nameof(StreamFunc.GAD):
                        if (rxSplitData[1] == "HOSTREJECT") 
                        {
                            Msg MsgBox = new Msg();
                            MsgBox.Show(Messages.E142_HOST_REJECT_MAPDATA_REQUEST);
                        }
                        else
                        {
                            // Expect GAD,SubtrateID, MapData, XmlContent
                            var substrateID = (rxSplitData[1]);// SubstrateID
                            rxE142XmlData = (rxSplitData[2]);// XmlContent
                            ReceivedXMLMapData = true;
                        }
                        
                        break;
                    case nameof(StreamFunc.SAR):
                        if (SubstrateStatus == null)
                        {
                            SubstrateStatus = new Dictionary<string, string>();
                        }
                        SubstrateStatus.Clear();
                        var data = rxSplitData.Skip(3).ToList();
                        foreach (string d in data)
                        {
                            if (!string.IsNullOrWhiteSpace(d))
                                SubstrateStatus[d] = "PENDING";
                        }
                        string SID = string.Join(",", data.Select(item => item?.ToString() ?? ""));
                        if (Set_Substrate == "1")
                        {
                            if (SubstrateStatus.Count == 1)
                            {
                                Send(nameof(StreamFunc.SAD) + ",REJECT.," + rxSplitData[1] + "," + rxSplitData[2] + "," + SID);
                            }
                            else
                            {
                                Send(nameof(StreamFunc.SAD) + ",SUCCESS.," + rxSplitData[1] + "," + rxSplitData[2] + "," + SID);
                                Event.SECSGEM_E142_SUBSTRATE_INFO.Set();
                            }
                        }
                        else
                        {
                            Send(nameof(StreamFunc.SAD) + ",SUCCESS.," + rxSplitData[1] + "," + rxSplitData[2] + "," + SID);
                            Event.SECSGEM_E142_SUBSTRATE_INFO.Set();
                        }
                        break;


                    #endregion

                    #region RMCD
                    case "PP-SELECT":
                        {
                            if (OnlineOffline == EOnlineOffline.Offline) return;

                            //PPID, LOTID, MATERIALNO, OPERATION, EMPLOYEEID
                            rxSplitData = rxSplitData.Concat(Enumerable.Repeat("", 4)).ToArray();//add 4 empty strings

                            string fileName = Path.Combine(GDefine.RecipeDir.FullName + rxSplitData[2] + GDefine.RecipeExt);
                            if (!FileExistsCaseSensitive(fileName))
                            {
                                PPChangeStatus = "Recipe not found.";
                                Send($"{data0},RECIPE NOT FOUND.");
                                PPError = EPPError.RecipeNotFound;
                                Event.SECSGEM_PP_VERIFICATION.Set();
                                break;
                            }

                            bool rdy = GDefine.Status == EStatus.Ready || GDefine.Status == EStatus.Stop || GDefine.Status == EStatus.EndStop;
                            if (!rdy || DispProg.TR_IsBusy())
                            {
                                PPChangeStatus = "Equipment is busy.";
                                Send($"{data0},EQUIPMENT IS BUSY.");

                                PPError = EPPError.EquipmentBusy;
                                Event.SECSGEM_PP_VERIFICATION.Set();
                                break;
                            }

                            if (!DispProg.loadXML2(fileName, true))
                            {
                                PPChangeStatus = "Load fail.";
                                Send($"{data0},LOAD FAIL.");

                                PPError = EPPError.LoadFail;
                                Event.SECSGEM_PP_VERIFICATION.Set();
                                break;
                            }

                            //LOTID, MATERIALNO, OPERATION, EMPLOYEEID
                            LotInfo2.RecipeName = rxSplitData[2];
                            LotInfo2.LotNumber = rxSplitData[4];
                            LotInfo2.Osram.ElevenSeries = rxSplitData[6];
                            LotInfo2.Osram.Operation = rxSplitData[10];
                            LotInfo2.sOperatorID = rxSplitData[8];

                            PPChangeStatus = "Success";
                            Send($"{data0},SUCCESS");
                            Thread.Sleep(100);
                            Event.SECSGEM_PP_SELECTED.Set("FileName", fileName);
                            break;
                        }
                    case "START":
                    case "RESTART":
                        if (OnlineOffline == EOnlineOffline.Offline) { Send($"{data0},OFFLINE."); return; }
                        if (!Define_Run.TR_StartRun()) 
                        {
                            Send($"{data0},SETUPFAIL.");
                        }
                        else
                        {
                            if (!frm_Auto.RunAuto())
                            {
                                Send($"{data0},AUTORUNFAIL.");
                            }
                            else
                            {
                                Send($"{data0},SUCCESS");
                                Event.OP_LOT_START.Set("LotInfo", $"{LotInfo2.sOperatorID},{LotInfo2.LotNumber},{LotInfo2.Osram.ElevenSeries},{LotInfo2.Osram.DAStartNumber}");
                            }
                        }
                        break;
                    case "STOP":
                    case "PAUSE":
                        if (OnlineOffline == EOnlineOffline.Offline) { Send($"{data0},OFFLINE."); return; }
                        Define_Run.TR_StopRun();
                        Send($"{data0},SUCCESS");
                        break;
                    case "ABORT":
                        if (OnlineOffline == EOnlineOffline.Offline) { Send($"{data0},OFFLINE."); return; }
                        Define_Run.TR_StopRun();
                        DispProg.TR_Cancel();
                        Send($"{data0},SUCCESS");
                        break;
                        #endregion
                }
            }
        }

        public class Eq
        {
            public static EOnlineOffline OnlineOffline
            {
                set
                {
                    if (value == EOnlineOffline.Online) Event.SECSGEM_EQ_SET_ONLINE.Set();
                    if (value == EOnlineOffline.Offline) Event.SECSGEM_EQ_SET_OFFLINE.Set();
                    TFSecsGem.OnlineOffline = value;
                    UpdateControlState(value.ToString());
                }
                get
                {
                    return TFSecsGem.OnlineOffline;
                }
            }
            public static ELocalRemote LocalRemote
            {
                set
                {
                    TFSecsGem.PrevLocalRemote = TFSecsGem.LocalRemote;
                    if (value == ELocalRemote.Local) Event.SECSGEM_EQ_SET_LOCAL.Set();
                    if (value == ELocalRemote.Remote) Event.SECSGEM_EQ_SET_REMOTE.Set();
                    TFSecsGem.LocalRemote = value;
                    UpdateControlState(value.ToString());
                }
                get
                {
                    return TFSecsGem.LocalRemote;
                }
            }
            public static EProcessState ProcessState
            {
                set
                {
                    TFSecsGem.PrevProcessState = TFSecsGem.ProcessState;
                    TFSecsGem.ProcessState = value;
                    Event.SECSGEM_EQ_PROCESS_CHANGE_STATE.Set();
                }
                get
                {
                    return TFSecsGem.ProcessState;
                }
            }

            private static void UpdateControlState(string value)
            {
                PrevControlState = ControlState;
                switch (value)
                {
                    case "Local":
                        {
                            ControlState = EControlState.EquipmentLocal;
                        }break;
                    case "Remote":
                        {
                            ControlState = EControlState.EquipmentRemote;
                        }break;

                    case "Offline":
                        {
                            ControlState = EControlState.HostOffline;
                        }break;
                    case "Online":
                        {
                            ControlState = EControlState.HostOnline;
                        }break;
                }


            }

            public static EControlState ControlState
            {
                set
                {
                    TFSecsGem.PrevControlState = TFSecsGem.ControlState;
                    Event.SECSGEM_CONTROL_STATE_CHANGE.Set();
                    TFSecsGem.ControlState = value;
                }
                get
                {
                    return TFSecsGem.ControlState;
                }
            }
            public static EControlState PrevControlState
            {
                set
                {
                    TFSecsGem.PrevControlState = value;
                }
                get
                {
                    return TFSecsGem.PrevControlState;
                }
            }
        }
        public static List<string> SSR_GetList(List<int> requestList)
        {
            var sVIDDict = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(x => x.FieldType == typeof(TEVID))
        .Select(x => (TEVID)x.GetValue(null))
        .Where(s => s.Code > 10000 && s.Code <= 29999)
        .ToDictionary(s => s.Code, s => s); // Dictionary<int, TEVID>

            List<string> list = new List<string>();

            if (requestList.Count == 0)
            {
                foreach (var svid in sVIDDict.Values)
                {
                    list.Add(Convert.ToString(svid.Value));
                }
            }
            else
            {
                foreach (var code in requestList)
                {
                    if (sVIDDict.TryGetValue(code, out var svid))
                    {
                        list.Add(Convert.ToString(svid.Value));
                    }
                }
            }

            return list;
        }
        public static List<string> SVNR_GetList(List<int> requestList)
        {
            var sVIDDict = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null))
                .Where(s => s.Code > 10000 && s.Code <= 29999)
                .ToDictionary(s => s.Code, s => s);

            List<string> list = new List<string>();

            if (requestList.Count == 0)
            {
                foreach (var svid in sVIDDict.Values)
                {
                    string info = $"{svid.Code:d5},{svid.Desc},{""}"; // SVID, SVNAME, UNITS
                    list.Add(info);
                }
            }
            else
            {
                foreach (var code in requestList)
                {
                    if (sVIDDict.TryGetValue(code, out var svid))
                    {
                        string info = $"{svid.Code:d5},{svid.Desc},{""}"; // SVID, SVNAME, UNITS
                        list.Add(info);
                    }
                }
            }
            return list;
        }

        public static List<string> ECR_GetList(List<int> requestList)
        {
            var sVIDDict = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null))
                .Where(s => s.Code > 30000 && s.Code <= 39999)
                .ToDictionary(s => s.Code, s => s);

            List<string> list = new List<string>();

            if (requestList.Count == 0)
            {
                foreach (var svid in sVIDDict.Values)
                {
                    string info = $"{svid.Value}"; // ECValue
                    list.Add(info);
                }
            }
            else
            {
                foreach (var code in requestList)
                {
                    if (sVIDDict.TryGetValue(code, out var svid))
                    {
                        string info = $"{svid.Value}"; // ECValue
                        list.Add(info);
                    }
                }
            }
            return list;
        }
        public static List<string> ECNR_GetList(List<int> requestList)
        {
            var sVIDDict = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null))
                .Where(s => s.Code > 30000 && s.Code <= 39999)
                .ToDictionary(s => s.Code, s => s);

            List<string> list = new List<string>();

            if (requestList.Count == 0)
            {
                foreach (var svid in sVIDDict.Values)
                {
                    string info = $"{svid.Code:d5},{svid.Desc},{svid.Min},{svid.Max},{svid.Def},{svid.Units}"; // ECID, ECNAME, ECMIN, ECMAX, ECDEF, UNITS
                    list.Add(info);
                }
            }
            else
            {
                foreach (var code in requestList)
                {
                    if (sVIDDict.TryGetValue(code, out var svid))
                    {
                        string info = $"{svid.Code:d5},{svid.Desc},{svid.Min},{svid.Max},{svid.Def},{svid.Units}"; // ECID, ECNAME, ECMIN, ECMAX, ECDEF, UNITS
                        list.Add(info);
                    }
                }
            }

            return list;
        }

        public static List<string> CENR_GetList(List<int> requestList)
        {
            var ceIDDict = typeof(Event).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.FieldType == typeof(TEEvent))
            .Select(x => (TEEvent)x.GetValue(null))
            .GroupBy(e => e.Code)                  // Group duplicates
            .Select(g => g.First())                // Take only one per Code
            .ToDictionary(e => e.Code, e => e);

            List<string> list = new List<string>();

            if (requestList.Count == 0)
            {
                foreach (var ce in ceIDDict.Values)
                {
                    string info = $"{ce.Code:d5},{ce.Name},[{string.Join(",", ce.VIDs)}]"; // CEID, CEName, [VID1,VID2,...]
                    list.Add(info);
                }
            }
            else
            {
                foreach (var code in requestList)
                {
                    if (ceIDDict.TryGetValue(code, out var ce))
                    {
                        string info = $"{ce.Code:d5},{ce.Name},[{string.Join(",", ce.VIDs)}]"; // CEID, CEName, [VID1,VID2,...]
                        list.Add(info);
                    }
                }
            }

            return list;
        }

        public static List<string> LAR_GetList(List<int> requestList)
        {
            var almList = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var alm in almList)
            {
                string info = $"{0},{alm.Code},{alm.Desc}";//ALCD,ALID,ADTX
                if (requestList.Count == 0)
                {
                    list.Add(info);
                }
                else
                if (requestList.Contains(alm.Code))
                {
                    list.Add(info);
                }
            }
            return list;
        }
        public static List<string> LEAR_GetList()
        {
            var almList = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var alm in almList)
            {
                string info = $"{0},{alm.Code},{alm.Desc}";//ALCD,ALID,ADTX
                if (alm.Enabled) list.Add(info);
            }
            return list;
        }

        public static List<string> LDA_GetList()
        {
            var almList = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var alm in almList)
            {
                string info = $"{alm.Code}";//ALID
                if (!alm.Enabled) list.Add(info);
            }
            return list;
        }
        public static List<string> LEA_GetList()
        {
            var almList = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var alm in almList)
            {
                string info = $"{alm.Code}";//ALID
                if (alm.Enabled) list.Add(info);
            }
            return list;
        }

        public static void SendAlarm_ARS(TEMessage msg, bool set)
        {
            //set bit8 = 1, clear bit8=0, ignore remaining
            string info = $"{(set?128:0)},{msg.Code},{msg.Desc}";//ALCD,ALID,ADTX
            Send(nameof(StreamFunc.ARS)+$",{info}");
        }

        public static void SendTerminalMsg_TRN(string msg)
        {
            Send(nameof(StreamFunc.TRN) + $",0,{msg}");
        }

        public static void UploadPP_PPI_PPS(string recipeName)
        {
            string ppid = recipeName;
            rxPPGData.Clear();
            Send(nameof(StreamFunc.PPI) + $",{ppid},{ppid.Length}");

            int t = Environment.TickCount + Timeout;
            while (true)//wait PPG
            {
                if (rxPPGData.Count > 1) break;
                Thread.Sleep(0);
                if (Environment.TickCount >= t) return;
            }

            var fileName = GDefine.RecipeDir.FullName + ppid + ".xml";

            if (File.Exists(fileName))
            {
                string content;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    content = reader.ReadToEnd();
                }
                StringBuilder binaryBuilder = new StringBuilder();

                foreach (char c in content)
                {
                    string binarychar = Convert.ToString(c, 2).PadLeft(8, '0');
                    binaryBuilder.Append(binarychar);
                }
                string filepath = @"D:\GemTaro__\READFILE\PPSData.txt";
                string binaryString = binaryBuilder.ToString();
                File.WriteAllText(filepath, binaryString);
                Send(nameof(StreamFunc.PPS) + $",{ppid},{filepath}");
            }
        }
        public static void RequestPP_PPR(string recipeName)
        {
            rxPPDData.Clear();
            Send(nameof(StreamFunc.PPR) + $"{recipeName}");

            int t = Environment.TickCount + Timeout;
            while (true)//wait PPD
            {
                if (rxPPDData.Count >= 3) break;
                Thread.Sleep(0);
                if (Environment.TickCount >= t) return;
            }

            try
            {
                bool isCreated = false;
                var ppid = rxPPDData[1];
                var filePath = rxPPDData[2];
                var file = GDefine.RecipeDir.FullName + ppid + ".xml";
                string ppbody = "";
                string fullFilename = GDefine.ProgPath + "\\" + ppid + "." + GDefine.ProgExt;
                if (!File.Exists(fullFilename))
                {
                    isCreated = true;
                }
                if (File.Exists(filePath))
                {
                    ppbody = File.ReadAllText(filePath);
                }
                StringBuilder xmlbuilder = new StringBuilder();
                for (int i = 0; i < ppbody.Length; i += 8)
                {
                    string byteString = ppbody.Substring(i, 8);
                    char character = (char)Convert.ToByte(byteString, 2);
                    xmlbuilder.Append(character);
                }
                string xmlcontent = xmlbuilder.ToString();
                slim.EnterWriteLock();
                try
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.Write(xmlcontent);
                        //a.Close();
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }

                if (!DispProg.LoadProgName(ppid))
                {
                    Send(nameof(StreamFunc.PPA) + ",LoadProgNameFail");
                    PPError = EPPError.LoadFail;
                    Event.SECSGEM_PP_VERIFICATION.Set();
                }

                else
                {
                    Send(nameof(StreamFunc.PPA) + ",Success");
                    if (isCreated)
                    {
                        Event.SECSGEM_PP_CREATE.Set();
                        isCreated = false;
                    }
                    else
                    {
                        Event.SECSGEM_PP_CHANGE.Set();
                    }

                }
            }
            catch (Exception ex)
            {
                Send(nameof(StreamFunc.PPA) + ",LoadProgNameFail");
                PPError = EPPError.LoadFail;
                Event.SECSGEM_PP_VERIFICATION.Set();
            }
        }

        public static List<string> VID_GetList(List<int> requestList)
        {
            var sVIDDict = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(x => x.FieldType == typeof(TEVID))
        .Select(x => (TEVID)x.GetValue(null))
        .Where(s => s.Code > 10000 && s.Code <= 29999)
        .ToDictionary(s => s.Code, s => s); // Dictionary<int, TEVID>

            List<string> list = new List<string>();

            if (requestList.Count == 0)
            {
                foreach (var svid in sVIDDict.Values)
                {
                    list.Add(Convert.ToString(svid.Value));
                }
            }
            else
            {
                foreach (var code in requestList)
                {
                    if (sVIDDict.TryGetValue(code, out var svid))
                    {
                        list.Add(Convert.ToString(svid.Value));
                    }
                }
            }

            return list;
        }

        private static bool FileExistsCaseSensitive(string fullPath)
        {
            if (!File.Exists(fullPath)) return false;

            string directory = Path.GetDirectoryName(fullPath);
            string targetFile = Path.GetFileName(fullPath);

            return Directory
                .GetFiles(directory)
                .Any(f => Path.GetFileName(f) == targetFile); // case-sensitive match
        }

        public static void GAR(string attribute)
        {
            ReceivedXMLMapData = false;
            Send(nameof(StreamFunc.GAR) + "," + $"{attribute}");// SubstrateID
        }

        static string currentXmlString = "";
        public static bool DecodeMap(string xmlString, ref string substrateID, ref string binCodeDefinitionString, ref string binCodesString)
        {
            currentXmlString = xmlString;

            XNamespace ns = "urn:semi-org:xsd.E142-1.V1005.SubstrateMap";
            XDocument doc = null;

            try
            {
                doc = XDocument.Parse(xmlString);

                var substrateId = doc.Descendants(ns + "Substrate")
                                     .FirstOrDefault()?
                                     .Attribute("SubstrateId")?
                                     .Value;
                substrateID = substrateId;

                var binDefinitions = doc.Descendants(ns + "BinDefinition")
                .Select(x => new
                {
                    Pick = (string)x.Attribute("Pick"),
                    BinDescription = (string)x.Attribute("BinDescription"),
                    BinQuality = (string)x.Attribute("BinQuality"),
                    BinCount = (string)x.Attribute("BinCount"),
                    BinCode = (string)x.Attribute("BinCode")
                })
                .ToList();

                List<string> toProcess = new List<string>();
                foreach (var bin in binDefinitions)
                {
                    binCodeDefinitionString = binCodeDefinitionString + $"{bin.BinQuality}-{bin.BinCode}\n";
                }

                var binCodes = doc.Descendants(ns + "BinCode")
                                  .Select(x => x.Value.Trim())
                                  .Where(x => !string.IsNullOrEmpty(x))
                                  .ToList();

                foreach (var bin in binCodes)
                {
                    binCodesString = binCodesString + $"{bin}\n";
                }

                Point cr = new Point(0, 0);
                cr = new Point(binCodes[0].Length / 4, binCodes.Count);

                string[,] dnLoadMap = new string[400, 400];
                int[,] map = new int[400, 400];

                for (int r = 0; r < cr.Y; r++)
                {
                    for (int c = 0; c < cr.X; c++)
                    {
                        dnLoadMap[c, r] = binCodes[r].Substring(c * 4, 4);
                        switch (dnLoadMap[c, r])
                        {
                            case string s when s.StartsWith("0"):
                            case string t when t.StartsWith("1"):
                            case string u when u.StartsWith("2"):
                            case string v when v.StartsWith("3"):
                            case string w when w.StartsWith("4"):
                                {
                                    map[c, r] = 0;
                                    break;
                                }
                            default:
                                map[c, r] = 210;
                                break;
                        }
                        int unitNo = 0;
                        DispProg.rt_Layouts[0].RCGetUnitNo(ref unitNo, c, r);
                        DispProg.Map.CurrMap[0].Bin[unitNo] = (EMapBin)map[c, r];
                    }
                }
            }
            catch (Exception ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.E142_INVALID_MAPDATA_XML_FORMAT, ex.Message);
                //MessageBox.Show(ex.Message.ToString());
            }
            return true;
        }
        public static bool DecodeMap(string xmlString)
        {
            string substrateID = "";
            string binCodeDefinitions = "";
            string binCodes = "";
            return DecodeMap(xmlString, ref substrateID, ref binCodeDefinitions, ref binCodes);
        }

        public static string EncodeBinCodeStrings()
        {
            int iCol = 0;
            int iRow = 0;
            int[,] map = new int[400, 400];
            for (int i = 0; i < DispProg.rt_Layouts[0].TUCount; i++)
            {
                DispProg.rt_Layouts[0].UnitNoGetRC(i, ref iCol, ref iRow);
                int iColM = DispProg.rt_Layouts[0].TColCount - 1 - iCol;

                try
                {
                    map[iColM, iRow] = (int)DispProg.Map.CurrMap[0].Bin[i];
                }
                catch
                {

                }
            }

            string binCodesStrings = "";
            for (int r = 0; r < iRow; r++)
            {
                for (int c = 0; c < iCol; c++)
                {
                    int bin = map[c, r];
                    string binCode = "AAAA";

                    //if (bin < 100) binCode = "0A0F";//Unprocessed
                    //                                //None = 0, BinNG = 100,
                    //                                //MapOK = 1, MapNG = 101,
                    //                                //RefOK = 2, RefNG = 102,
                    //                                //HeightOK = 3, HeightNG = 103,
                    //                                //UnitMarkOK = 4, UnitMarkNG = 104,
                    //                                //VVIOK = 6, VVING = 106,
                    if (bin >= 100 && bin < 200) binCode = "0000";//Complete = 200
                                                                  //if (bin == (int)EMapBin.VVING/*106*/) binCode = "0F0F";//VVING = 106
                                                                  //if (bin == 200) binCode = "AAAA";//Complete = 200
                                                                  //if (bin == 210) binCode = "0C0F";//InMapNG = 210,
                                                                  //if (bin == 220) binCode = "0AFF";//Bypass = 220,
                                                                  //if (bin == 255) binCode = "0A0F";//PreMapNG = 255

                    binCodesStrings = binCodesStrings + binCode;
                }
                binCodesStrings = binCodesStrings + $"\n";
            }

            return "";
        }

        public static bool EncodeMap(string binCodesString, ref string xmlString)
        {
            XNamespace ns = "urn:semi-org:xsd.E142-1.V1005.SubstrateMap";
            XDocument doc = null;

            try
            {
                doc = XDocument.Parse(currentXmlString);

                var substrate = doc.Descendants(ns + "Substrate").FirstOrDefault();
                if (substrate != null)
                {
                    substrate.SetAttributeValue("SubstrateId", "FO");
                }

                string[] lines = binCodesString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Locate the <BinCodeMap> node
                var binCodes = doc.Descendants(ns + "BinCodeMap").FirstOrDefault();
                // Remove existing BinCode elements
                binCodes.Elements(ns + "BinCode").Remove();

                foreach (string line in lines)
                {
                    // Add new BinCode elements
                    binCodes.Add(new XElement(ns + "BinCode", line));
                }

                xmlString = doc.ToString();
                currentXmlString = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return true;
        }
    }
}

