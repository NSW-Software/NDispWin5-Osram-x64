using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using SocketV1;
using System.Xml;
using System.Xml.Linq;
using Emgu.CV;
using System.Runtime.InteropServices;

namespace NDispWin
{
    public class Osram_SCC
    {
        public enum ESystemMode { StandAlone, Left, Right };//Station No 1..2
        public ESystemMode SystemMode = ESystemMode.StandAlone;

        public Server Server = new Server();
        public Client Client = new Client();
        frm_OsramSCC_Setup frm = new frm_OsramSCC_Setup();

        bool _Enabled = true;

        public Osram_SCC()
        {
            //frm.OsramSCC = this;

            Client.ConnectedEvent += new Client.OnConnected(Client_ConnectedEvent);
            Client.DisconnectedEvent += new Client.OnDisconnected(Client_DisconnectedEvent);
            Client.FrameEndReceivedEvent += new Client.OnFrameEndReceived(Client_FrameEndReceivedEvent);

            Server.ClientConnectedEvent += new Server.OnClientConnected(Server_ClientConnectedEvent);
            Server.ClientDisconnectedEvent += new Server.OnClientDisconnected(Server_ClientDisconnectedEvent);
            Server.FrameEndReceivedEvent += new Server.OnFrameEndReceived(Server_FrameEndReceivedEvent);
        }

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        internal int StationNo
        {
            get
            {
                switch (SystemMode)
                {
                    default:
                    case ESystemMode.StandAlone:
                    case ESystemMode.Left:
                        return 1;
                    case ESystemMode.Right:
                        return 2;
                }
            }
        }

        public void SaveSetup()
        {
            NSW.Net.IniFile IniFile = new NSW.Net.IniFile();

            string Filename = GDefine.SetupPath + "\\OsramSCC.ini";
            IniFile.Create(Filename);

            IniFile.WriteInteger("System", "Mode", (int)SystemMode);

            IniFile.WriteString("Client", "IPAddress", Client.IPAddress);
            IniFile.WriteInteger("Client", "Port", Client.Port);

            IniFile.WriteString("Server", "IPAddress", Server.IPAddress);
            IniFile.WriteInteger("Server", "Port", Server.Port);

            SaveDefault();
        }
        public void LoadSetup()
        {
            NSW.Net.IniFile IniFile = new NSW.Net.IniFile();

            string Filename = GDefine.SetupPath + "\\OsramSCC.ini";
            IniFile.Create(Filename);

            SystemMode = (ESystemMode)IniFile.ReadInteger("System", "Mode", 0);

            Client.IPAddress = IniFile.ReadString("Client", "IPAddress", "127.0.0.1");
            Client.Port = IniFile.ReadInteger("Client", "Port", 1118);

            Server.IPAddress = IniFile.ReadString("Server", "IPAddress", "127.0.0.1");
            Server.Port = IniFile.ReadInteger("Server", "Port", 1119);

            LoadDefault();
        }

        public void SaveDefault()
        {
            NSW.Net.IniFile IniFile = new NSW.Net.IniFile();

            string Filename = GDefine.SetupPath + "\\OsramSCC.ini";
            IniFile.Create(Filename);

            IniFile.WriteString("LotInfo", "LotID", LotID);
        }
        public void LoadDefault()
        {
            NSW.Net.IniFile IniFile = new NSW.Net.IniFile();

            string Filename = GDefine.SetupPath + "\\OsramSCC.ini";
            IniFile.Create(Filename);

            LotID = IniFile.ReadString("LotInfo", "LotID", "");
        }

        public void ShowWindow()
        {
            if (frm == null)
            {
                frm = new frm_OsramSCC_Setup();
            }

            frm.ShowDialog();
        }

        //Lot
        public const string VC_NEW_LOT = "DMNL";//DMNL;LotID;11Series;DAStart;EmpID;Recipe_1
        public const string VC_CHANGE_RECIPE = "DMNR";//DMNR;Recipe

        public const string DM_ACK = "DMACK";
        public const string VC_ACK = "DMACK";
        public const string DM_REQ_RECIPE = "DMREQR";
        public const string DM_ERROR = "DMERR";//DMERR;0;Error;1 (ErrCode;ErrDesc)
        public const string DM_LAuNCH_PROG = "DMLPRG";
        public const string DM_END_LOT = "DMEND";
        public const string VC_END_LOT_ACK = "DMVCACK";

        //Disp Para
        public const string VC_REQ_PARA_INFO = "DMRVP";//DMRVP;1;1 (ParaOpt 1=FlowRate(mg),2=Press(psi),3=OpenTime(ms); StationNo)
        public const string DM_RESP_PARA = "DMDVP";//DMDVP;3.0;3.0;1; (Para_0..Para_n; StationNo)
        public const string DM_REQ_CHANGE_PARA = "DMSVP";//DMSVP;3.0;3.0;1; (NewPara_0..NewPara_n; StationNo)

        //Alert
        public const string VC_ALERT_ON = "DMALRT";//DMALRT;1 0=OFF,1=ON
        public const string DM_ALERT_ACK = "DMALRTC";

        //Machine Status - uniDirection
        public const string DM_RUN = "DMRUN";//DMRUN;1 machine no
        public const string DM_STOP = "DMSTOP";//DMSTOP;1 machine no
        public const string DM_MC_ERROR = "DMMERR";//DMERR;0;Error;1 (ErrCode;ErrDesc;StationNo)
        public const string DM_MC_WARNING = "DMMWRN";//DMMWRN;0;Error;1 (WarnCode;WarnDesc;StationNo)

        public const string DM_PANEL_COMPLETE = "DMDISC";//DMDISC;1;0 (StationNo; PanelID)
        public const string VC_PANEL_COMPLETE_ACK = "DMVDISC";//DMVDISC;1 (StationNo)
        public const string DM_PANEL_REACH = "DMRCH";//DMRCH;1;0 (StationNo; PanelNo)
        public const string VC_PANEL_REACH_ACK = "DMVCRCH";//DMVCRCH;1 (StationNo)

        //Disp Setting
        public const string VC_REQ_SETTING = "DMRDP";
        public const string DM_RESP_SETTING = "DMDP";//DMDP;3.0;3.0 (Para_0..Para_n, return all head in machine)
        public const string VC_NEW_SETTING = "DMSDP";//DMSDP;3.1;3.1;1 (Para_0..Para_n;StationNo)
        public const string DM_SETTING_DONE = "DMPSC";//DMPSC;1 (Para_0..Para_n;StationNo)
       
        public bool b_EndLotAck = false;
        public void SendEndLot()
        {
            DialogResult dr = MessageBox.Show("Confirm End Lot " + LotID + "?", "End Lot", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;

            Client_Tx(DM_END_LOT);

            if (SystemMode == ESystemMode.Left)
            {
                if (!Server.ClientConnected)
                {
                    TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Right not Connected.");
                    return;
                }
                Server_TX(DM_END_LOT);
            }
            //if (SystemMode == ESystemMode.StandAlone) Client_Tx(DM_ACK);
            //if (SystemMode == ESystemMode.Right)
            //{
            //    Client_Tx(DM_END_LOT);
            //}
            
            b_EndLotAck = false;
            int t = GDefine.GetTickCount() + 5000;
            while (!b_EndLotAck)
            {
                if (GDefine.GetTickCount() >= t)
                {
                    MessageBox.Show("End Lot SCC Response Timeout. Lot Not End " + LotID);
                    return;
                }
                //Application.DoEvents();
                Thread.Sleep(50);
            }
            ClearLotInfo();
            SaveDefault();
        }
        public void ForceEndLot()
        {
            ClearLotInfo();
            SaveDefault();
        }

        public void SendDMRun()
        {
            Client_Tx(DM_RUN + ";" + StationNo.ToString());
        }
        public void SendDMStop()
        {
            Client_Tx(DM_STOP + ";" + StationNo.ToString());
        }
        public void SendMCError(int StationNo, int ErrorCode, string ErrorDesc)
        {
            Client_Tx(DM_MC_ERROR + ";" + StationNo.ToString() + ";" + ErrorCode.ToString() + ";" + ErrorDesc);
        }
        public void SendMCWarning(int ErrorCode, string ErrorDesc)
        {
            Client_Tx(DM_MC_WARNING + ";" + StationNo.ToString() + ";" + ErrorCode.ToString() + ";" + ErrorDesc);
        }
        public void SendPnlReach(int PanelNo)
        {
            Client_Tx(DM_PANEL_REACH + ";" + StationNo.ToString() + ";" + PanelNo.ToString());
        }
        public void SendPnlComplete(string PanelID)
        {
            if (PanelID == "") PanelID = "0";
            Client_Tx(DM_PANEL_COMPLETE + ";" + StationNo.ToString() + ";" + PanelID);
        }

        public void ClearLotInfo()
        {
            LotID = "";
            Series = "";
            DAStart = "";
            EmpID = "";
            Recipe = "";
            //PreMap = "";
            PreMapNo = 0;

            SaveDefault();
        }

        public string LotID = "";
        public string Series = "";
        public string DAStart = "";
        public string EmpID = "";
        public string Recipe = "";
        //public string PreMap = "";
        public int PreMapNo = 0;

        #region Client - Standalone or Right
        public void Client_Connect(string IPAddress, int Port)
        {
            Client.Connect(IPAddress, Port);
        }
        public void Client_Connect()
        {
            try
            {
                Client.Connect(Client.IPAddress, Client.Port);
            }
            catch { };
        }
        public void Client_Disconnect()
        {
            Client.Disconnect();
        }
        public bool Client_Connected
        {
            get
            {
                try
                {
                    return Client.Connected;
                }
                catch
                {
                    return false;
                }
            }
        }

        private void Client_ConnectedEvent()
        {
            string Text = "";
            switch (SystemMode)
            {
                case ESystemMode.StandAlone:
                case ESystemMode.Left:
                    Text = "SCC Connected";
                    break;
                case ESystemMode.Right:
                    Text = "Left Connected ";
                    break;
            }
            Log.OsramSCC.WriteByMonthDay(Text + Client.IPAddress + ":" + Client.Port);
            frm.AddLog(Text + " " + Client.IPAddress + ":" + Client.Port);
        }
        private void Client_DisconnectedEvent()
        {
            string Text = "";
            switch (SystemMode)
            {
                case ESystemMode.StandAlone:
                case ESystemMode.Left:
                    Text = "SCC Disconnected";
                    break;
                case ESystemMode.Right:
                    Text = "Left Disconnected ";
                    break;
            }
            Log.OsramSCC.WriteByMonthDay(Text);
            frm.AddLog(Text);
        }
        private void Client_FrameEndReceivedEvent()
        {
            if (!_Enabled) return;

            string RxData = "";
            while (Client.RxBufFrameCount > 0)
            {
                if (Client.RxFrame(ref RxData) > 0)
                {
                    string S = "SCC";
                    if (SystemMode == ESystemMode.Right) S = "Left";

                    Log.OsramSCC.WriteByMonthDay(S + " >: " + RxData);
                    frm.AddLog(S + " >: " + RxData);
                }
                if (RxData.StartsWith(VC_NEW_LOT))
                #region
                {
                    PreMapNo = 0;

                    if (SystemMode == ESystemMode.Left)
                    {
                        if (!Server.ClientConnected)
                        {
                            TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Right not Connected.");
                            return;
                        }
                        Server_TX(RxData);
                    }

                    if (LotID.Length > 0)
                    {
                        TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Lot already started.");
                        return;
                    }

                    #region Decoding
                    string s_LotID = "";
                    string s_Series = "";
                    string s_DAStart = "";
                    string s_EmpID = "";
                    string s_Recipe = "";
                    string s_PreMap = "";
                    int i_PreMap = 0;
                    try
                    {
                        #region Decode
                        string[] line = RxData.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        s_LotID = line[1];
                        s_Series = line[2];
                        s_DAStart = line[3];
                        s_EmpID = line[4];
                        s_Recipe = line[5];

                        string[] recipe = s_Recipe.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                        if (recipe.Count() > 1)
                        {
                            s_Recipe = "";
                            for (int i = 0; i < recipe.Count() - 1; i++)
                            {
                                s_Recipe = s_Recipe + recipe[i];
                            }
                            s_PreMap = recipe[recipe.Count() - 1];
                            i_PreMap = Convert.ToInt16(s_PreMap);
                        }
                        else
                            if (recipe.Count() == 1)
                            {
                            }
                            else
                            {
                                TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Decode Recipe Error.");
                                return;
                            }
                        #endregion
                    }
                    catch
                    {
                        TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Decode Para Error.");
                        return;
                    }
                    #endregion

                    #region Load Program, PreMap
                    if (DispProg.TR_IsBusy())
                    {
                        //MsgInfo.TMsgInfo MsgInfor = new MsgInfo.TMsgInfo();
                        //MsgInfo.GetInfo((int)EErrCode.DISP_IS_BUSY, ref MsgInfor);
                        //TaskDisp.OsramSCC.SendMCError(StationNo, (int)EErrCode.DISP_IS_BUSY, MsgInfor.Desc);
                        TaskDisp.OsramSCC.SendMCError(StationNo, Messages.DISP_IS_BUSY.Code, Messages.DISP_IS_BUSY.Desc);
                        return;
                    }

                    string fullFileName = GDefine.ProgPath + "\\" + s_Recipe + "." + GDefine.ProgExt;
                    if (TaskDisp.EnableRecipeFile) fullFileName = GDefine.RecipeDir.FullName + s_Recipe + GDefine.RecipeExt;

                    if (!File.Exists(fullFileName))
                    {
                        TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Recipe Not Found.");
                        return;
                    }

                    if (i_PreMap > 0)
                    {
                        if (!DispProg.LoadProgName(Path.GetFileNameWithoutExtension(fullFileName)))
                        {
                            TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Load Recipe Error.");
                        }
                        DispProg.UsePreMap(i_PreMap);
                        //DispProg.CurrMapMask(DispProg.Map.ActivePreMap.Bin);
                    }
                    #endregion

                    LotID = s_LotID;
                    Series = s_Series;
                    DAStart = s_DAStart;
                    EmpID = s_EmpID;
                    Recipe = s_Recipe;
                    //PreMap = s_PreMap;
                    PreMapNo = i_PreMap;

                    if (SystemMode == ESystemMode.StandAlone) Client_Tx(DM_ACK);
                    if (SystemMode == ESystemMode.Right) Client_Tx(VC_NEW_LOT);

                    SaveDefault();

                    return;
                }
                #endregion
                if (RxData.StartsWith(VC_CHANGE_RECIPE))
                #region
                {
                    if (SystemMode == ESystemMode.Left)
                    {
                        if (!Server.ClientConnected)
                        {
                            TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Right not Connected.");
                            return;
                        }
                        Server_TX(RxData);
                    }

                    #region Decoding
                    string s_Recipe = "";
                    string s_PreMap = "";
                    int i_PreMap = 0;
                    try
                    {
                        string[] line = RxData.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        s_Recipe = line[1];

                        string[] recipe = s_Recipe.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                        if (recipe.Count() > 1)
                        {
                            s_Recipe = "";
                            for (int i = 0; i < recipe.Count() - 1; i++)
                            {
                                s_Recipe = s_Recipe + recipe[i];
                            }
                            s_PreMap = recipe[recipe.Count() - 1];
                            i_PreMap = Convert.ToInt16(s_PreMap);
                        }
                        else
                            if (recipe.Count() == 1)
                        {
                        }
                        else
                        {
                            TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Decode Recipe Error.");
                            return;
                        }
                    }
                    catch
                    {
                        TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Decode Para Error.");
                        return;
                    }
                    #endregion

                    #region Load Program, PreMap
                    if (DispProg.TR_IsBusy())
                    {
                        //MsgInfo.TMsgInfo MsgInfor = new MsgInfo.TMsgInfo();
                        //MsgInfo.GetInfo((int)EErrCode.DISP_IS_BUSY, ref MsgInfor);
                        TaskDisp.OsramSCC.SendMCError(StationNo, Messages.DISP_IS_BUSY.Code, Messages.DISP_IS_BUSY.Desc);
                        return;
                    }

                    if (i_PreMap > 0)
                    {
                        //DispProg.ClearRTDispData();
                        DispProg.UsePreMap(i_PreMap);
                        //DispProg.CurrMapMask(DispProg.Map.ActivePreMap.Bin);
                    }
                    #endregion

                    Recipe = s_Recipe;
                    PreMapNo = i_PreMap;

                    //Client_Tx(DM_ACK);
                    if (SystemMode == ESystemMode.StandAlone) Client_Tx(DM_ACK);
                    if (SystemMode == ESystemMode.Right) Client_Tx(VC_CHANGE_RECIPE);
                }
                #endregion
                //if (RxData.StartsWith(uC_REQ_PARA_INFO))
                #region
                //{
                //    string[] line = RxData.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                //    string Para = line[1];

                //    if (Para == "1")
                //    {
                //        //return weight
                //        double Disp1 = DispProg.Set_Weight[0];
                //        double Disp2 = DispProg.Set_Weight[1];

                //        Client_Tx(DM_RESP_PARA + ";" + Disp1.ToString("f3") + ";" + Disp1.ToString("f3"));
                //    }
                //    else
                //        TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Invalid Para.");

                //    return;
                //}
                #endregion
                if (RxData.StartsWith(VC_ALERT_ON))
                #region
                {
                    switch (SystemMode)
                    {
                        case ESystemMode.Left:
                        //if (!Server.ClientConnected)
                        //{
                        //    TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Right not Connected.");
                        //    return;
                        //}
                        //Server.Tx(RxData);
                        //break;
                        case ESystemMode.StandAlone:
                            Client_Tx(DM_ALERT_ACK);
                            break;
                        case ESystemMode.Right:
                            //no action  
                            //Client_Tx(DM_ALERT_ACK);
                            break;
                    }
                }
                #endregion
                if (RxData.StartsWith(VC_REQ_SETTING))
                #region
                {
                    //return weight
                    if (SystemMode == ESystemMode.Left)
                    {
                        if (!Server.ClientConnected)
                        {
                            TaskDisp.OsramSCC.SendMCError(StationNo, 0, "Right not Connected.");
                            return;
                        }
                        Server_TX(RxData);
                    }

                    if (SystemMode == ESystemMode.StandAlone)
                    {
                        double Weight1 = DispProg.Disp_Weight[0];
                        double Weight2 = DispProg.Disp_Weight[1];
                        //Client_Tx(DM_RESP_SETTING + ";" + Weight1.ToString("f3") + ";" + Weight2.ToString("f3"));
                        //Client_Tx(DM_RESP_SETTING + ";" + Weight1.ToString("f3") + ";" + Weight2.ToString("f3") + ";" + Weight1.ToString("f3") + ";" + Weight2.ToString("f3"));
                        Client_Tx(DM_RESP_SETTING + ";" + Weight1.ToString("f3") + ";" + Weight2.ToString("f3"));
                    }
                    if (SystemMode == ESystemMode.Right)
                    {
                        double Weight1 = DispProg.Disp_Weight[0];
                        double Weight2 = DispProg.Disp_Weight[1];
                        Client_Tx(DM_RESP_SETTING + ";" + Weight1.ToString("f3") + ";" + Weight2.ToString("f3"));
                    }
                }
                #endregion
                if (RxData.StartsWith(VC_NEW_SETTING))
                #region
                {
                    double[] W = new double[2] { 0, 0 };
                    int i_StationNo = 0;
                    try
                    {
                        string[] line = RxData.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        string Para1 = line[1];
                        string Para2 = line[2];
                        string Para = line[line.Count() - 1];

                        W[0] = Convert.ToDouble(Para1);
                        W[1] = Convert.ToDouble(Para2);
                        i_StationNo = Convert.ToInt32(Para); 
                    }
                    catch
                    {
                        TaskDisp.OsramSCC.SendMCError(i_StationNo, 0, "Decode Para Error.");
                        return;
                    }

                    if (i_StationNo == 1)
                    {
                        #region
                        switch (SystemMode)
                        {
                            case ESystemMode.StandAlone://update weight
                            case ESystemMode.Left://update weight
                                try
                                {
                                    TaskDisp.PP_SetWeight(W, true);
                                }
                                catch (Exception Ex)
                                {
                                    TaskDisp.OsramSCC.SendMCError(i_StationNo, 0, Ex.Message.ToString());
                                    return;
                                }
                                Client_Tx(DM_SETTING_DONE + ";" + StationNo);
                                break;
                        }
                        #endregion
                    }
                    if (i_StationNo == 2)
                    {
                        #region
                        switch (SystemMode)
                        {
                            case ESystemMode.Left://send to right
                                if (!Server.ClientConnected)
                                {
                                    TaskDisp.OsramSCC.SendMCError(i_StationNo, 0, "Right not Connected.");
                                    return;
                                }
                                Server_TX(RxData);
                                break;
                            case ESystemMode.Right://update weight
                                try
                                {
                                    TaskDisp.PP_SetWeight(W, true);
                                }
                                catch (Exception Ex)
                                {
                                    TaskDisp.OsramSCC.SendMCError(i_StationNo, 0, Ex.Message.ToString());
                                    return;
                                }
                                Client_Tx(DM_SETTING_DONE + ";" + StationNo);
                                break;
                        }
                        #endregion
                    }
                }
                #endregion

                //Response
                //if (RxData.StartsWith(uC_ACK))
                //{
                //    b_LaunchProgAck = true;
                //}
                if (RxData.StartsWith(VC_END_LOT_ACK))
                {
                    b_EndLotAck = true;
                    return;
                }

                if (RxData.StartsWith(DM_END_LOT))
                {
                    if (SystemMode == ESystemMode.Right) ForceEndLot();
                    return;
                }

                if (RxData.StartsWith(VC_PANEL_REACH_ACK) || RxData.StartsWith(VC_PANEL_COMPLETE_ACK))
                {
                    string[] line = RxData.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (line[line.Count() - 1] == "2")
                        if (SystemMode == ESystemMode.Left) Server_TX(RxData);
                    return;
                }
            }
        }
        public void Client_Tx(string Tx)
        {
            if (!_Enabled) return;

            try
            {
                if (Client.TxFrame(Tx) >= 0)
                {
                    string S = "SCC";
                    if (SystemMode == ESystemMode.Right) S = "Left";
                    Log.OsramSCC.WriteByMonthDay(S + " <: " + Tx);
                    frm.AddLog(S + " <: " + Tx);
                }
            }
            catch (Exception ex)
            {
                frm.AddLog("Client Exception Err " + ex.Message.ToString());
            }
        }
        #endregion

        #region Server - Left Only
        internal void Server_Listen()
        {
            if (!_Enabled) return;
            if (SystemMode != ESystemMode.Left) return;

            Server.ListenSocket(Server.IPAddress, Server.Port);

            if (Server_Listening)
            {
                string Text = "Left Listening";
                Log.OsramSCC.WriteByMonthDay(Text);
                frm.AddLog(Text);
            }
        }
        internal void Server_Stop()
        {
            if (!_Enabled) return;
            if (SystemMode != ESystemMode.Left) return;

            Server.CloseSocket();

            string Text = "Left Closed";
            Log.OsramSCC.WriteByMonthDay(Text);
            frm.AddLog(Text);
        }
        internal bool Server_Listening
        {
            get
            {
                return Server.Listening;
            }
        }

        internal void Server_ClientConnectedEvent()
        {
            string Text = "Right Connected";
            Log.OsramSCC.WriteByMonthDay(Text);
            frm.AddLog(Text);
        }
        internal void Server_ClientDisconnectedEvent()
        {
            string Text = "Right Disconnected";
            Log.OsramSCC.WriteByMonthDay(Text);
            frm.AddLog(Text);
        }
        internal void Server_FrameEndReceivedEvent()
        {
            if (!_Enabled) return;
            if (SystemMode != ESystemMode.Left) return;

            string RxData = "";
            while (Server.RxBufFrameCount > 0)
            {
                if (Server.RxFrame(ref RxData) > 0)
                {
                    Log.OsramSCC.WriteByMonthDay("Right >: " + RxData);
                    frm.AddLog("Right >: " + RxData);
                }

                if (RxData.StartsWith(VC_NEW_LOT))
                #region
                {
                    Client_Tx(DM_ACK);
                    return;
                }
                #endregion

                if (RxData.StartsWith(VC_CHANGE_RECIPE))
                #region
                {
                    Client_Tx(DM_ACK);
                    return;
                }
                #endregion

                if (RxData.StartsWith(DM_ALERT_ACK))
                #region
                {
                    Client_Tx(DM_ALERT_ACK);
                    return;
                }
                #endregion

                if (RxData.StartsWith(DM_RESP_SETTING))
                #region
                {
                    string Rx = RxData.Replace(DM_RESP_SETTING, "");

                    double Weight1 = DispProg.Disp_Weight[0];
                    double Weight2 = DispProg.Disp_Weight[1];
                    Client_Tx(DM_RESP_SETTING + ";" + Weight1.ToString("f3") + ";" + Weight2.ToString("f3") + Rx);
                    return;
                }
                #endregion

                //Echo Server Rx
                Client_Tx(RxData);
            }
        }
        internal void Server_TX(string Tx)
        {
            if (!_Enabled) return;
            if (SystemMode != ESystemMode.Left) return;

            try
            {
                if (Server.TxFrame(Tx) >= 0)
                {
                    Log.OsramSCC.WriteByMonthDay("Right <: " + Tx);
                    frm.AddLog("Right <: " + Tx);
                }
            }
            catch (Exception ex)
            {
                frm.AddLog("Server Exception Err " + ex.Message.ToString());
            }
        }
        #endregion
        internal void ConnectAll()
        {
            //try
            //{
                Client_Connect();
            //}
            //catch { };

            if (SystemMode == ESystemMode.Left)
            {
                Server_Listen();
            }
        }
    }

    public class OsramICC
    {
        public static bool ReadInputFile(string filename, ref double volume)
        {

            try
            {
                XNamespace ns = "http://www.osram-os.com/steering/config";
                XDocument doc = XDocument.Load(filename);

                var dispenserSetting = doc
                    .Root
                    .Element(ns + "TargetSettings")
                    ?.Attribute("InitialDispenserSetting")
                    ?.Value;

                volume = Convert.ToDouble(dispenserSetting);

                if (dispenserSetting != null)
                {
                    Event.OSRAMICC.Set("Decode Input File.", $"InitialDispenserSetting. {dispenserSetting:f3}");
                }
                else
                {
                    Event.OSRAMICC.Set("Decode Input File.", "InitialDispenserSetting not found.");
                }
            }
            catch (Exception ex)
            {
                Event.OSRAMICC.Set("Read Input File.", $"Read  Error. {ex.Message.ToString()}");
                return false;
            }

            return true;
        }

        public class TOutputDataRow
        {
            public string PanelID { get; set; }
            public double NewVolume1 { get; set; }
            public double NewVolume2 { get; set; }

            public TOutputDataRow(string panel, double dispenser1, double dispenser2)
            {
                PanelID = panel;
                NewVolume1 = dispenser1;
                NewVolume2 = dispenser2;
            }

            public override string ToString()
            {
                return $"{PanelID}: {NewVolume1}, {NewVolume2}";
            }
        }
        public static Dictionary<string, TOutputDataRow> OutputPanelLookup = new Dictionary<string, TOutputDataRow>();
        public static bool ReadOutputFile(string filename)
        {
            OutputPanelLookup.Clear();

            try
            {
                string data = File.ReadAllText(filename);

                string[] lines = data.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 1; i < lines.Length; i++) // skip header
                {
                    string[] parts = lines[i].Split(';');
                    string panelID = parts[0];
                    double newVolume1 = double.Parse(parts[1]);
                    double newVolume2 = double.Parse(parts[2]);

                    // If the panel already exists, update it with the latest (replicated panel gets overwritten)
                    OutputPanelLookup[panelID] = new TOutputDataRow(panelID, newVolume1, newVolume2);
                }
            }
            catch (Exception ex)
            {
                Event.OSRAMICC.Set("Read Output File.", $"Read  Error. {ex.Message}");
                return false;
            }

            return true;
        }
        public static bool PanelLookup(string panelID, ref double[] newVolume)
        {
            if (!OutputPanelLookup.TryGetValue(panelID, out TOutputDataRow result))
            {
                return false;//not found
            }

            newVolume = new double[] { result.NewVolume1, result.NewVolume2 };

            return true;
        }

        public class TPassPanels
        {
            string fileName;
            public List<string> PanelIDs = new List<string>();

            public TPassPanels(string filename)
            {
                this.fileName = filename;
            }

            public bool WriteFile()
            {
                try
                {
                    string list = "";
                    try
                    {
                        list = string.Join("\r", PanelIDs.Take(100));
                    }
                    catch { return false; }
                    File.WriteAllText(fileName, list);
                }
                catch
                {
                    return false;
                }
                return true;
            }
            public bool ReadFile()
            {
                if (!File.Exists(fileName))
                {
                    PanelIDs = new List<string>();
                    return true;
                }

                try
                {
                    string list = File.ReadAllText(fileName);
                    PanelIDs = list.Split(new[] { ',', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
        public static string Pass1PanelListFile = GDefine.AppPath + "\\Pass1.txt";
        public static string Pass2PanelListFile = GDefine.AppPath + "\\Pass2.txt";
        public static TPassPanels Pass1 = new TPassPanels(Pass1PanelListFile);
        public static TPassPanels Pass2 = new TPassPanels(Pass2PanelListFile);

        public static bool Execute(string tileID, string lotNo, string e11series, string dAStartNo, ref double[] volume)
        {
            //Check TileID completed Pass 2 -> Prompt error
            if (!Pass2.ReadFile())
            {
                Msg MsgBox = new Msg();
                EMsgRes Resp = MsgBox.Show($"Pass2 Load Local fail.");
                return false;
            }

            if (Pass2.PanelIDs.Contains(tileID))
            {
                Msg MsgBox = new Msg();
                EMsgRes Resp = MsgBox.Show($"TileID: {tileID} has completed Pass 2.");
                return false;
            }

            //Check TileID completed Pass 1 -> Run Pass 2
            if (!Pass1.ReadFile())
            {
                Msg MsgBox = new Msg();
                EMsgRes Resp = MsgBox.Show($"Pass1 Load Local fail.");
                return false;
            }

            if (Pass1.PanelIDs.Contains(tileID))
            {
                string outputFile = $"{TaskDisp.OsramICC_OutputPath}\\{lotNo}.txt";

                if (!File.Exists(outputFile))
                {
                    Msg MsgBox = new Msg();
                    EMsgRes Resp = MsgBox.Show($"Output File: {outputFile} is not found.");
                    return false;
                }
                
                if (!ReadOutputFile(outputFile))
                {
                    Msg MsgBox = new Msg();
                    EMsgRes Resp = MsgBox.Show($"Read Output File: {lotNo}.txt fail.");
                    return false;
                }

                if (!PanelLookup(tileID, ref volume))
                {
                    Msg MsgBox = new Msg();
                    EMsgRes Resp = MsgBox.Show($"TileID: {tileID} is not found in Output File: {lotNo}.txt.");
                    return false;
                }

                Event.OSRAMICC.Set($"TileID: {tileID}", "Run Pass2");
                Pass2.PanelIDs.Insert(0, tileID);
                Pass2.WriteFile();
                DispProg.UsePreMap(2);
                DispProg.CurrMapMask(DispProg.Map.ActivePreMap.Bin);

                Event.OSRAMICC.Set($"TileID: {tileID}", "Select Premap2");

                return true;
            }

            //Run Pass1
            {
                string inputFile = $"{TaskDisp.OsramICC_InputPath}\\{e11series}_{dAStartNo}__.xml";

                if (!File.Exists(inputFile))
                {
                    Msg MsgBox = new Msg();
                    EMsgRes Resp = MsgBox.Show($"Input File: {inputFile} is not found.");
                    return false;
                }

                double vol = 0;
                if (!ReadInputFile(inputFile, ref vol))
                {
                    Msg MsgBox = new Msg();
                    EMsgRes Resp = MsgBox.Show($"Read Input File: {inputFile} fail.");
                    return false;
                }
                volume = new double[2] { vol, vol };

                Event.OSRAMICC.Set($"TileID: {tileID}", "Run Pass1");
                Pass1.PanelIDs.Insert(0, tileID);
                Pass1.WriteFile();
                DispProg.UsePreMap(1);
                DispProg.CurrMapMask(DispProg.Map.ActivePreMap.Bin);

                Event.OSRAMICC.Set($"TileID: {tileID}", "Select Premap1");

                return true;
            }

            return true;
        }
    }
}
