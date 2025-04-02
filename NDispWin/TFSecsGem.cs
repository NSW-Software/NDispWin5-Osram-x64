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
        Aborted//Error Job was aborted or failed due to an error or alarm.
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

        public static TEStreamFunc ECR = new TEStreamFunc("S2F13", "H->E, Equipment Constant Request.");//tested
        public static TEStreamFunc ECD = new TEStreamFunc("S2F14", "H<-E, Equipment Constant Data.");//tested
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

        public static TEStreamFunc ERS = new TEStreamFunc("S5F1", "H<-E, Alarm Report Send.");//tested
        public static TEStreamFunc ERA = new TEStreamFunc("S5F2", "H->E, Alarm Report Acknowledge.");//tested

        public static TEStreamFunc S7F0 = new TEStreamFunc("S7F0", "H<->E, Abort Transation.");
        public static TEStreamFunc PPI = new TEStreamFunc("S7F1", "H<->E, Process Program Inquire.");//H->E tested
        public static TEStreamFunc PPG = new TEStreamFunc("S7F2", "H<->E, Process Program Grant.");//tested
        public static TEStreamFunc PPS = new TEStreamFunc("S7F3", "H<->E, Process Program Sent.");//H->E tested
        public static TEStreamFunc PPA = new TEStreamFunc("S7F4", "H<->E, Process Program Acknowledge.");//tested
        public static TEStreamFunc PPR = new TEStreamFunc("S7F5", "H<->E, Process Program Request.");//H->E tested
        public static TEStreamFunc PPD = new TEStreamFunc("S7F6", "H<->E, Process Program Data.");//tested

        public static TEStreamFunc S10F0 = new TEStreamFunc("S10F0", "H<->E, Abort Transaction.");
        public static TEStreamFunc TRN = new TEStreamFunc("S10F1", "H<-E, Terminal Request.");
        public static TEStreamFunc TRA = new TEStreamFunc("S10F2", "H->E, Terminal Request Acknowledge.");
        public static TEStreamFunc VTN = new TEStreamFunc("S10F3", "H->E, Terminal Display,Single.");
        public static TEStreamFunc VTA = new TEStreamFunc("S10F4", "H<-E, Terminal Display,Single Acknowledge.");
    }

    class TFSecsGem
    {
        private static int Timeout = 10000;

        public static EOnlineOffline OnlineOffline = EOnlineOffline.Offline;
        public static ELocalRemote LocalRemote = ELocalRemote.Local;
        public static ELocalRemote PrevLocalRemote = ELocalRemote.Local;
        public static EProcessState ProcessState = EProcessState.Idle;
        public static EProcessState PrevProcessState = EProcessState.Idle;
        public static string PPChangeStatus = "";
        public static string PPFormat = "Unformatted";

        public static string RxTerminalMessage = "";
        public static string TxTerminalMessage = "";

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
                            Event.SECSGEM_HOST_REQ_OFFLINE.Set();
                            if (!IsConnected) Send(nameof(StreamFunc.S1F0));
                            OnlineOffline = EOnlineOffline.Offline;
                            Send(nameof(StreamFunc.OFLA));
                            break;
                        }
                    case nameof(StreamFunc.RONL):
                        {
                            Event.SECSGEM_HOST_REQ_ONLINE.Set();
                            if (!IsConnected) Send(nameof(StreamFunc.S1F0));
                            OnlineOffline = EOnlineOffline.Online;
                            Send(nameof(StreamFunc.ONLA));
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

                            if (alid == "")
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
                                    if (alid == $"{msg.Code:d5}")
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
                                var ppid = Path.GetFileNameWithoutExtension(rxSplitData[1]);
                                var ppbody = rxSplitData[2];
                                var file = GDefine.RecipeDir.FullName + ppid + ".xml";

                                slim.EnterWriteLock();
                                try
                                {
                                    using (StreamWriter writer = new StreamWriter(file))
                                    {
                                        writer.Write(ppbody);
                                        //a.Close();
                                    }
                                }
                                finally
                                {
                                    slim.ExitWriteLock();
                                }

                                if (!DispProg.LoadProgName(ppid))
                                    Send(nameof(StreamFunc.S7F0));
                                else
                                    Send(nameof(StreamFunc.PPA));
                            }
                            catch (Exception ex)
                            {
                                Send(nameof(StreamFunc.S7F0));
                            }
                        }
                        else
                            Send(nameof(StreamFunc.S7F0));
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
                    #endregion
                    case nameof(StreamFunc.VTN):
                        {
                            rxSplitData = rxSplitData.Concat(Enumerable.Repeat("", 1)).ToArray();//add 1 empty strings
                            RxTerminalMessage = rxSplitData[1];
                            Send($"{nameof(StreamFunc.VTA)}");
                            break;
                        }

                    #region RMCD
                    case "PP-SELECT":
                        {
                            if (OnlineOffline == EOnlineOffline.Offline) return;

                            //PPID, LOTID, MATERIALNO, OPERATION, EMPLOYEEID
                            rxSplitData = rxSplitData.Concat(Enumerable.Repeat("", 4)).ToArray();//add 4 empty strings

                            string fileName = GDefine.RecipeDir.FullName + rxSplitData[1] + GDefine.RecipeExt;
                            if (!File.Exists(fileName))
                            {
                                PPChangeStatus = "Recipe not found.";
                                break;
                            }

                            bool rdy = GDefine.Status == EStatus.Ready || GDefine.Status == EStatus.Stop || GDefine.Status == EStatus.EndStop;
                            if (!rdy || DispProg.TR_IsBusy())
                            {
                                PPChangeStatus = "Equipment is busy.";
                                break;
                            }

                            if (!DispProg.loadXML2(fileName, true))
                            {
                                PPChangeStatus = "Load fail.";
                                break;
                            }

                            //LOTID, MATERIALNO, OPERATION, EMPLOYEEID
                            LotInfo2.LotNumber = rxSplitData[2];
                            LotInfo2.Osram.ElevenSeries = rxSplitData[3];
                            LotInfo2.Osram.Operation = rxSplitData[4];
                            LotInfo2.sOperatorID = rxSplitData[5];

                            PPChangeStatus = "Success";
                            Send(data0);
                            Event.PP_SELECTED.Set("FileName", fileName);
                            break;
                        }
                    case "START":
                    case "RESTART":
                        if (OnlineOffline == EOnlineOffline.Offline) return;
                        Define_Run.TR_StartRun();
                        Send(data0);
                        break;
                    case "STOP":
                    case "PAUSE":
                        if (OnlineOffline == EOnlineOffline.Offline) return;
                        Define_Run.TR_StopRun();
                        Send(data0);
                        break;
                    case "ABORT":
                        if (OnlineOffline == EOnlineOffline.Offline) return;
                        Define_Run.TR_StopRun();
                        DispProg.TR_Cancel();
                        Send(data0);
                        break;
                        #endregion
                        //    case "DOWNLOAD":
                        //        if (!EnableStripMapE142) break;
                        //        if (data.Length == 1) AddLog("Download data incomplete.");
                        //        string content = rxData;
                        //        content = content.Remove(0, content.IndexOf("<?"));
                        //        UpdateXMLtoLocal(content);
                        //        break;

                        //    case "SERVER_DISCONNECTED":
                        //        Msg MsgBox = new Msg();
                        //        EMsgRes res = MsgBox.Show("Server Disconnected. Pls check Gemtaro status.", "", TEMessage.EType.Notification, EMsgBtn.smbOK);
                        //        break;
                        //    default:
                        //        break;


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
                    Event.SECSGEM_EQ_PROCESS_CHANGE_STATE.Set();
                    TFSecsGem.ProcessState = value;
                }
                get
                {
                    return TFSecsGem.ProcessState;
                }
            }
        }

        public static List<string> SSR_GetList(List<int> requestList)
        {
            var sVIDList = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var sVID in sVIDList)
            {
                if (sVID.Code > 10000 && sVID.Code <= 19999)
                {
                    string info = Convert.ToString(sVID.Value);//SVID Values
                    if (requestList.Count == 0)
                    {
                        list.Add(info);
                    }
                    else
                    if (requestList.Contains(sVID.Code))
                    {
                        list.Add(info);
                    }
                }
            }
            return list;
        }
        public static List<string> SVNR_GetList(List<int> requestList)
        {
            var sVIDList = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var sVID in sVIDList)
            {
                if (sVID.Code > 10000 && sVID.Code <= 19999)
                {
                    string info = $"{sVID.Code:d5},{sVID.Desc},{""}";//SVID,SVNAME,UNITS
                    if (requestList.Count == 0)
                    {
                        list.Add(info);
                    }
                    else
                    if (requestList.Contains(sVID.Code))
                    {
                        list.Add(info);
                    }
                }
            }
            return list;
        }
        public static List<string> ECR_GetList(List<int> requestList)
        {
            var sVIDList = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var sVID in sVIDList)
            {
                if (sVID.Code > 30000 && sVID.Code <= 39999)
                {
                    string info = $"{sVID.Value}";//ECValue
                    if (requestList.Count == 0)
                    {
                        list.Add(info);
                    }
                    else
                    if (requestList.Contains(sVID.Code))
                    {
                        list.Add(info);
                    }
                }
            }
            return list;
        }
        public static List<string> ECNR_GetList(List<int> requestList)
        {
            var sVIDList = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var sVID in sVIDList)
            {
                if (sVID.Code > 30000 && sVID.Code <= 39999)
                {
                    string info = $"{sVID.Code:d5},{sVID.Desc},{sVID.Min},{sVID.Max},{sVID.Def},{sVID.Units}";//ECID,ECNAME,ECMIN,ECMAX,ECDEF,UNITS
                    if (requestList.Count == 0)
                    {
                        list.Add(info);
                    }
                    else
                    if (requestList.Contains(sVID.Code))
                    {
                        list.Add(info);
                    }
                }
            }
            return list;
        }
        public static List<string> CENR_GetList(List<int> requestList)
        {
            var ceIDlist = typeof(Event).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEEvent))
                .Select(x => (TEEvent)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var cEID in ceIDlist)
            {
                string info = $"{cEID.Code:d5},{cEID.Name},[{string.Join(",",cEID.VIDs)}]";//CEID,CEName,[VID1,VID2,..VIDn]
                if (requestList.Count == 0)
                {
                    list.Add(info);
                }
                else
                if (requestList.Contains(cEID.Code))
                {
                    list.Add(info);
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
                string info = $"{0},{alm.Code:d5},{alm.Desc}";//ALCD,ALID,ADTX
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
                string info = $"{0},{alm.Code:d5},{alm.Desc}";//ALCD,ALID,ADTX
                if (alm.Enabled) list.Add(info);
            }
            return list;
        }

        public static void SendAlarm_ARS(TEMessage msg, bool set)
        {
            //set bit8 = 1, clear bit8=0, ignore remaining
            string info = $"{(set?128:0)},{msg.Code:d5},{msg.Desc}";//ALCD,ALID,ADTX
            Send(nameof(StreamFunc.ARS)+$",{info}");
        }

        public static void SendTerminalMsg_TRN(string msg)
        {
            Send(nameof(StreamFunc.TRN) + $",{msg}");
        }

        public static void UploadPP_PPI_PPS(string recipeName)
        {
            rxPPGData.Clear();
            Send(nameof(StreamFunc.PPI));

            int t = Environment.TickCount + Timeout;
            while (true)//wait PPG
            {
                if (rxPPGData.Count > 1) break;
                Thread.Sleep(0);
                if (Environment.TickCount >= t) return;
            }

            string ppid = recipeName;
            var fileName = GDefine.RecipeDir + ppid + ".xml";

            StreamReader a = new StreamReader(fileName);
            var slist = new List<dynamic>()
                                {
                                    fileName,
                                    a.ReadToEnd(),
                                };

            Send(nameof(StreamFunc.PPS) + $",{ppid},{slist}");
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
                var ppid = rxPPDData[1];
                var ppbody = rxPPDData[2];
                var file = GDefine.RecipeDir + ppid + ".xml";

                slim.EnterWriteLock();
                StreamWriter a = new StreamWriter(file);
                a.Write(ppbody as string);
                a.Close();
                slim.ExitWriteLock();

                DispProg.LoadProgName(ppid);

            }
            catch (Exception ex)
            {
            }
        }
    }
}

