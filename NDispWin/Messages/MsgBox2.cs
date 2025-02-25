using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Timers;
using System.Reflection;

namespace NDispWin
{
    public class TFTowerLight
    {
        internal static bool Red
        {
            set
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    TaskConv.TowerLight.TL_Red = value;
                else
                    TaskGantry.TLRed = value;
            }
            get
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    return TaskConv.TowerLight.TL_Red;
                else
                    return TaskGantry.TLRed;
            }
        }
        internal static bool Yellow
        {
            set
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    TaskConv.TowerLight.TL_Yellow = value;
                else
                    TaskGantry.TLYellow = value;
            }
            get
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    return TaskConv.TowerLight.TL_Yellow;
                else
                    return TaskGantry.TLYellow;
            }
        }
        internal static bool Green
        {
            set
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    TaskConv.TowerLight.TL_Green = value;
                else
                    TaskGantry.TLGreen = value;
            }
            get
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    return TaskConv.TowerLight.TL_Green;
                else
                    return TaskGantry.TLGreen;
            }
        }
        internal static bool Buzzer
        {
            set
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    TaskConv.TowerLight.TL_Buzzer = value;
                else
                    TaskGantry.TLBuzzer = value;
            }
            get
            {
                if (GDefine.ConveyorType == GDefine.EConveyorType.CONVEYOR)
                    return TaskConv.TowerLight.TL_Buzzer;

                else
                    return TaskGantry.TLBuzzer;
            }
        }
    }

    public enum EMsgBtn
    {
        smbNone = 0,
        smbOK = 0x02,
        smbRetry = 0x04,
        smbStop = 0x08,
        smbCancel = 0x10,
        smbYes = 0x20,
        smbNo = 0x40,
        smbOK_Retry = smbOK | smbRetry,
        smbOK_Stop = smbOK | smbStop,
        smbOK_Cancel = smbOK | smbCancel,
        smbOK_Retry_Stop = smbOK_Retry | smbStop,
        smbOK_Retry_Cancel = smbOK_Retry | smbCancel,
        smbRetry_Stop = smbRetry | smbStop,
        smbRetry_Cancel = smbRetry | smbCancel,
        smbRetry_Stop_Cancel = smbRetry_Stop | smbCancel,
        smbAll = smbOK_Retry_Stop | smbCancel,
        smbStop_Cancel = smbStop | smbCancel,
    }
    public enum EMsgRes { smrNone = 0x00, smrAlmClr = 0x01, smrOK = 0x02, smrRetry = 0x04, smrStop = 0x08, smrCancel = 0x10, smrYes = 0x20, smrNo = 0x40 }
    public enum EMsgIcon { smiInfo, smiWarning, smiQuestion, smiError }

    public class MsgInfo
    {
        //public static bool Init(string AppName)
        //{
        //    MsgBoxGDefine.AppName = AppName;

        //    if (AppName.Length == 0) return false;

        //    if (!Directory.Exists(MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName))
        //        try
        //        {
        //            Directory.CreateDirectory(MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName);
        //        }
        //        catch { };

        //    if (!Directory.Exists(MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName + "\\Image"))
        //        try
        //        {
        //            Directory.CreateDirectory(MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName + "\\Image");
        //        }
        //        catch
        //        { };


        //    MsgBoxGDefine.MsgImagePath = MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName + "\\Image";
        //    MsgBoxGDefine.MsgListFFileName = MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName + "\\MsgBoxList.txt";

        //    return true;
        //}
        //public static string AltMsg
        //{
        //    set
        //    {
        //        string FileName = Path.GetFileNameWithoutExtension(value);

        //        if (FileName.Length == 0) return;

        //        string FullFName = MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName + "\\" + FileName + ".txt";

        //        NUtils.IniFile Inifile = new NUtils.IniFile(FullFName);
        //        Inifile.WriteString("AltMsgList", "Name", FileName);

        //        MsgBoxGDefine.AltMsgListName = FileName;

        //        Load_Alt();
        //    }
        //    get
        //    {
        //        return Path.GetFileNameWithoutExtension(MsgBoxGDefine.AltMsgListName);
        //    }
        //}

        //private static Stopwatch TickCountWatch = new Stopwatch();
        //internal static int GetTickCount()
        //{
        //    if (!TickCountWatch.IsRunning)
        //    {
        //        TickCountWatch.Start();
        //    }

        //    int D = (int)TickCountWatch.ElapsedMilliseconds;
        //    return D;
        //}

        //public class TMsgInfo
        //{
        //    public string Desc;
        //    public string Desc_Alt;
        //    public string CAct;
        //    public string CAct_Alt;

        //    public TMsgInfo()
        //    {
        //        Desc = "";
        //        CAct = "";
        //        Desc_Alt = "";
        //        CAct_Alt = "";
        //    }
        //    public TMsgInfo(string _Desc, string _CAct)
        //    {
        //        Desc = _Desc;
        //        CAct = _CAct;
        //        Desc_Alt = "";
        //        CAct_Alt = "";
        //    }
        //    public void Add(string _Desc, string _CAct)
        //    {
        //        Desc = _Desc;
        //        CAct = _CAct;
        //    }
        //    public TMsgInfo GetInfo()
        //    {
        //        return this;
        //    }
        //}
        //public class TMsgInfoList
        //{
        //    public const int MAX_COUNT = 10000;
        //    public TMsgInfo[] MsgInfo = new TMsgInfo[MAX_COUNT];
        //    public TMsgInfoList()
        //    {
        //        for (int i = 0; i < MAX_COUNT; i++)
        //            MsgInfo[i] = new TMsgInfo();
        //    }
        //    public void Add(int ErrCode, string Desc, string CAct)
        //    {
        //        MsgInfo[ErrCode].Add(Desc, CAct);
        //    }
        //}

        //private static TMsgInfoList _MsgInfoList = new TMsgInfoList();

        ///// <summary>
        ///// Load only Alternate Language by the FileName. Default Language not loaded.
        ///// </summary>
        ///// <param name="FileName">Full FileName</param>
        //internal static void Load_Alt()
        //{
        //    string FileName = MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName + "\\" + MsgBoxGDefine.AltMsgListName + ".txt";

        //    if (File.Exists(FileName))
        //    {
        //        //NSW.Net.IniFile Inifile = new NSW.Net.IniFile();
        //        //Inifile.Create(FileName);
        //        NUtils.IniFile Inifile = new NUtils.IniFile(FileName);
        //        for (int i = 0; i < TMsgInfoList.MAX_COUNT; i++)
        //        {
        //            _MsgInfoList.MsgInfo[i].Desc_Alt = Inifile.ReadString(i.ToString("0000"), "Desc", "");
        //            _MsgInfoList.MsgInfo[i].CAct_Alt = Inifile.ReadString(i.ToString("0000"), "CAct", "");
        //        }
        //    }
        //}

        ///// <summary>
        ///// Save Language file to assinged names
        ///// </summary>
        //public static bool Save()
        //{
        //    string FileName = MsgBoxGDefine.MsgListFFileName;

        //    if (System.Diagnostics.Debugger.IsAttached) return true;

        //        if (FileName.Length == 0) return false;

        //    //NSW.Net.IniFile Inifile = new NSW.Net.IniFile();
        //    //Inifile.Create(FileName);
        //    //if (File.Exists(FileName))
        //    {
        //        NUtils.IniFile Inifile = new NUtils.IniFile(FileName);
        //        MsgBoxGDefine.AltMsgListName = Inifile.ReadString("AltMsgList", "Name", "");
        //        for (int i = 0; i < TMsgInfoList.MAX_COUNT; i++)
        //        {
        //            if (_MsgInfoList.MsgInfo[i].Desc.Length == 0)
        //            {
        //                _MsgInfoList.MsgInfo[i].Desc = Inifile.ReadString(i.ToString("0000"), "Desc", "");
        //                _MsgInfoList.MsgInfo[i].CAct = Inifile.ReadString(i.ToString("0000"), "CAct", "");
        //            }
        //        }

        //        File.Delete(FileName);
        //    }


        //    string FileNameAlt = MsgBoxGDefine.AppPath + "\\MsgBox\\" + MsgBoxGDefine.AppName + "\\" + MsgBoxGDefine.AltMsgListName + ".txt";

        //    if (MsgBoxGDefine.AltMsgListName.Length > 0)
        //    {
        //        //Inifile2.Create(FileNameAlt);
        //        NUtils.IniFile Inifile2 = new NUtils.IniFile(FileNameAlt);
        //        if (File.Exists(FileNameAlt))
        //        {
        //            for (int i = 0; i < TMsgInfoList.MAX_COUNT; i++)
        //            {
        //                _MsgInfoList.MsgInfo[i].Desc_Alt = Inifile2.ReadString(i.ToString("0000"), "Desc", "");
        //                _MsgInfoList.MsgInfo[i].CAct_Alt = Inifile2.ReadString(i.ToString("0000"), "CAct", "");
        //            }

        //            File.Delete(FileNameAlt);
        //        }
        //    }

        //    for (int i = 0; i < TMsgInfoList.MAX_COUNT; i++)
        //    {
        //        if (_MsgInfoList.MsgInfo[i].Desc.Length > 0 || _MsgInfoList.MsgInfo[i].Desc_Alt.Length > 0)
        //        {
        //            //Inifile.Create(FileName);
        //            NUtils.IniFile Inifile = new NUtils.IniFile(FileName);
        //            Inifile.WriteString("AltMsgList", "Name", MsgBoxGDefine.AltMsgListName);
        //            Inifile.WriteString(i.ToString("0000"), "Desc", _MsgInfoList.MsgInfo[i].Desc);
        //            Inifile.WriteString(i.ToString("0000"), "CAct", _MsgInfoList.MsgInfo[i].CAct);

        //            if (MsgBoxGDefine.AltMsgListName.Length > 0)
        //            {
        //                //Inifile2.Create(FileNameAlt);
        //                NUtils.IniFile Inifile2 = new NUtils.IniFile(FileNameAlt);
        //                Inifile2.WriteString(i.ToString("0000"), "Desc", _MsgInfoList.MsgInfo[i].Desc_Alt);
        //                Inifile2.WriteString(i.ToString("0000"), "CAct", _MsgInfoList.MsgInfo[i].CAct_Alt);
        //            }
        //        }
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// Register message by TMsgInfo
        ///// </summary>
        ///// <param name="ErrCode"></param>
        ///// <param name="MsgInfo"></param>
        //public static void Register(int ErrCode, TMsgInfo MsgInfo)
        //{
        //    _MsgInfoList.MsgInfo[ErrCode].Add(MsgInfo.Desc, MsgInfo.CAct);
        //}

        ///// <summary>
        ///// Register message by Msg Info
        ///// </summary>
        ///// <param name="ErrCode"></param>
        ///// <param name="MsgInfo"></param>
        //public static void Register(int ErrCode, string Desc, string CAct)
        //{
        //    _MsgInfoList.MsgInfo[ErrCode].Add(Desc, CAct);
        //}

        ///// <summary>
        ///// Register message by MsgInfo
        ///// </summary>
        ///// <param name="ErrCode"></param>
        ///// <param name="MsgInfo"></param>
        //public static void Register(TMsgInfoList MsgInfoList)
        //{
        //    for (int i = 0; i < TMsgInfoList.MAX_COUNT; i++)
        //    {
        //        if (MsgInfoList.MsgInfo[i].Desc.Length > 0)
        //        {
        //            _MsgInfoList.MsgInfo[i].Add(MsgInfoList.MsgInfo[i].Desc, MsgInfoList.MsgInfo[i].CAct);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Get MsgInfo by ErrCode
        ///// </summary>
        ///// <param name="ErrCode"></param>
        ///// <param name="MsgInfo"></param>
        //public static void GetInfo(int ErrCode, ref TMsgInfo MsgInfo)
        //{
        //    MsgInfo = _MsgInfoList.MsgInfo[ErrCode].GetInfo();

        //    if (MsgInfo.Desc.Length == 0) MsgInfo.Desc = ((EErrCode)ErrCode).ToString();
        //}

        //public static string Desc(int ErrCode)
        //{
        //    TMsgInfo msgInfo = _MsgInfoList.MsgInfo[ErrCode].GetInfo();
        //    return msgInfo.Desc;
        //}

        //public static int MsgInQue = 0;
        //public static int AssistCount = 0;
        //public static bool Showing
        //{
        //    get
        //    {
        //        return (MsgInQue > 0);
        //    }
        //}
    }
    public class Msg
    {
        public static bool enableControlKeyPress = false;

        public static int AssistCount = 0;
        public static int MsgInQue = 0;
        public static bool Showing
        {
            get
            {
                return (MsgInQue > 0);
            }
        }

        public EMsgRes Show(string desc, string exMsg = "", TEMessage.EType type = TEMessage.EType.Error, EMsgBtn msgBtn = EMsgBtn.smbOK)
        {
            //frm_MsgBox frm = new frm_MsgBox();
            //frm.SetErrCode(desc, "", exMsg, McState, msgBtn, false);
            TEMessage msg = new TEMessage(0, desc, "", type, false);
            //frm.SetErrCode(msg, exMsg, msgBtn);
            //frm.ShowDialog();
            //return frm.MsgRes;
            return Show(msg, exMsg, msgBtn);
        }
        public EMsgRes Show(TEMessage msg, string exMsg = "", EMsgBtn msgBtn = EMsgBtn.smbOK)
        {
            switch (msg.Type)
            {
                default:
                case TEMessage.EType.Error:
                    Event.ERROR.Set("nil", msg.Desc + "," + exMsg);
                    break;
                case TEMessage.EType.Fault:
                    Event.FAULT.Set("nil", msg.Desc + "," + exMsg);
                    break;
                case TEMessage.EType.Confirmation:
                    Event.CONFIRMATION.Set("nil", msg.Desc + "," + exMsg);
                    break;
                case TEMessage.EType.Notification:
                    Event.NOTIFICATION.Set("nil", msg.Desc + "," + exMsg);
                    break;
                case TEMessage.EType.Warning:
                    Event.WARNING.Set("nil", msg.Desc + "," + exMsg);
                    break;
                case TEMessage.EType.Custom1:
                    Event.CUSTOM1.Set("nil", msg.Desc + "," + exMsg);
                    break;
                case TEMessage.EType.Custom2:
                    Event.CUSTOM2.Set("nil", msg.Desc + "," + exMsg);
                    break;
            }

            frm_MsgBox frm = new frm_MsgBox();
            //frm.SetErrCode(msg.Code, exMsg, EMcState.Error, msgBtn, false);
            frm.SetErrCode(msg, exMsg, msgBtn);
            frm.ShowDialog();
            return frm.MsgRes;
        }
    }

    //public enum ERYG { Off, Blink, On, DontCare }
    //public enum EBuzzer { Off, On, DontCare }
    //internal struct ETLState
    //{
    //    public ERYG Red;
    //    public ERYG Yel;
    //    public ERYG Grn;
    //    public EBuzzer Buz;
    //    public ETLState(ERYG Red_, ERYG Yel_, ERYG Grn_, EBuzzer Buz_)
    //    {
    //        Red = Red_;
    //        Yel = Yel_;
    //        Grn = Grn_;
    //        Buz = Buz_;
    //    }
    //}

    /// <summary>
    /// None - All Off
    /// Mute - Silent buzzer only
    /// Idle - Machine is in Idle or Ready state
    /// Run - Machine is in Run State
    /// Wait - Machine is in Run State, waiting for part
    /// Notice - System is in Notification State, notify an event
    /// Warning - System is in Warning State, danger or undesired state
    /// Error - Machine is in Error State, error state
    /// </summary>
    //public enum EMcState { None, Mute, Idle, Run, Wait, Notice, Warning, Error, Define, Last }

    public class TETwrLightStatus
    {
        public string Type = "";
        public int Red = 0;
        public int Yel = 0;
        public int Grn = 0;
        public int Buz = 0;
        public TETwrLightStatus(string type, int red, int yel, int grn, int buz)
        {
            this.Type = type;
            this.Red = red;
            this.Yel = yel;
            this.Grn = grn;
            this.Buz = buz;
        }
    }
    public class TwrLight
    {
        //0=Off, 1=On, 2=Blinking, 9= Dont care
        public static TETwrLightStatus Run = new TETwrLightStatus("Run", 0, 0, 1, 0);
        public static TETwrLightStatus Idle = new TETwrLightStatus("Idle", 0, 1, 0, 0);
        public static TETwrLightStatus Error = new TETwrLightStatus("Error", 1, 0, 0, 1);
        public static TETwrLightStatus Warning = new TETwrLightStatus("Warning", 2, 0, 0, 1);
        public static TETwrLightStatus Fault = new TETwrLightStatus("Fault", 1, 0, 0, 1);
        public static TETwrLightStatus Notification = new TETwrLightStatus("Notification",0, 1, 0, 1);
        public static TETwrLightStatus Confirmation = new TETwrLightStatus("Confirmation", 0, 1, 0, 1);
        public static TETwrLightStatus Custom1 = new TETwrLightStatus("Custom1", 1, 0, 0, 1);
        public static TETwrLightStatus Custom2 = new TETwrLightStatus("Custom2", 1, 0, 0, 1);
    }

    public class TCTwrLight
    {
        public static bool SaveStatus()
        {
            var statusList = typeof(TwrLight).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TETwrLightStatus))
                .Select(x => (TETwrLightStatus)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var status in statusList)
            {
                list.Add($"{status.Type},{status.Red},{status.Yel},{status.Grn},{status.Buz}");
            }

            FileStream F = new FileStream(GDefine.TLStatusFile, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter W = new StreamWriter(F);

            try
            {
                W.WriteLine("Type,Red,Yellow,Green,Buzzer");
                W.WriteLine("Assign: 0=Off, 1=On, 2=Blinking, 3=Retain");
                foreach (string s in list)
                {
                    W.WriteLine(s);
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                W.Close();
            }
            return true;
        }
        public static bool LoadStatus()
        {
            var statusList = typeof(TwrLight).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TETwrLightStatus))
                .Select(x => (TETwrLightStatus)x.GetValue(null)).ToArray();

            if (!File.Exists(GDefine.TLStatusFile)) return true;

            FileStream F = new FileStream(GDefine.TLStatusFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Write);
            StreamReader R = new StreamReader(F);

            string FileLine = "";
            try
            {
                FileLine = R.ReadToEnd();
            }
            finally
            {
                R.Close();
            }

            string[] Lines = FileLine.Split(new char[] { (char)10 }, StringSplitOptions.RemoveEmptyEntries);

            for (int Line = 1; Line < Lines.Count(); Line++)
            {
                #region
                string[] s = Lines[Line].Split(',');

                //"Type,Red,Yellow,Green,Buzzer"
                string type = s[0].Trim();
                int red = 0;
                try
                {
                    red = Convert.ToInt16(s[1].Trim());
                }
                catch { }
                int yel = 0;
                try
                {
                    yel = Convert.ToInt16(s[2].Trim());
                }
                catch { }
                int grn = 0;
                try
                {
                    grn = Convert.ToInt16(s[3].Trim());
                }
                catch { }
                int buz = 0;
                try
                {
                    buz = Convert.ToInt16(s[4].Trim());
                }
                catch { }

                foreach (var status in statusList)
                {
                    if (status.Type == type)
                    {
                        status.Red = red;
                        status.Yel = yel;
                        status.Grn = grn;
                        status.Buz = buz;
                    }
                }
                #endregion
            }
            return true;
        }

        private static TETwrLightStatus status = new TETwrLightStatus("local", 0, 0, 0, 0);

        static System.Windows.Forms.Timer timer500 = new System.Windows.Forms.Timer();
        static bool bToggle = false;
        static void timer500_Tick(object sender, EventArgs e)
        {
            try
            {
                bToggle = !bToggle;

                if (status.Red == 2) TFTowerLight.Red = bToggle ? true : false;
                if (status.Yel == 2) TFTowerLight.Yellow = bToggle ? true : false;
                if (status.Grn == 2) TFTowerLight.Green = bToggle ? true : false;
            }
            catch
            {
            }
        }
        public static void SetStatus(int red = 3, int yel = 3, int grn = 3, int buz = 3)
        {
            if (!timer500.Enabled)//init timer
            {
                timer500.Interval = 500;
                timer500.Enabled = true;
                timer500.Tick += new EventHandler(timer500_Tick);
            }

            //update last status if not same status
            if (red != status.Red || yel != status.Yel || grn != status.Grn || buz != status.Buz)
                status = new TETwrLightStatus("", red, yel, grn, buz);

            try
            {
                if (status.Red < 3) TFTowerLight.Red = status.Red > 0;
                if (status.Yel < 3) TFTowerLight.Yellow = status.Yel > 0;
                if (status.Grn < 3) TFTowerLight.Green = status.Grn > 0;
                if (status.Buz < 3) TFTowerLight.Buzzer = status.Buz > 0;
            }
            catch
            {
            }
        }
        public static void SetMute()
        {
            SetStatus(buz: 0);
        }
        public static void SetStatus(TETwrLightStatus status)
        {
            SetStatus(status.Red, status.Yel, status.Grn, status.Buz);
        }
    }

    //public class IO
    //{
        //internal static ETLState[] IntStates = new ETLState[] {
        //new ETLState(ERYG.Off, ERYG.Off, ERYG.Off, EBuzzer.Off),//EMcState.None
        //new ETLState(ERYG.DontCare, ERYG.DontCare, ERYG.DontCare, EBuzzer.Off),//EMcState.Mute
        //new ETLState(ERYG.Off, ERYG.On, ERYG.Off, EBuzzer.Off),//EMcState.Idle
        //new ETLState(ERYG.Off, ERYG.Off, ERYG.On, EBuzzer.Off),//EMcState.Run
        //new ETLState(ERYG.Off, ERYG.Blink, ERYG.On, EBuzzer.Off),//EMcState.Wait
        //new ETLState(ERYG.Off, ERYG.Blink, ERYG.Off, EBuzzer.On),//EMcState.Notice
        //new ETLState(ERYG.Blink, ERYG.Off, ERYG.Off, EBuzzer.On),//EMcState.Warning
        //new ETLState(ERYG.On, ERYG.Off, ERYG.Off, EBuzzer.On),//EMcState.Error
        //new ETLState(ERYG.On, ERYG.On, ERYG.On, EBuzzer.On),//EMcState.Define
        //new ETLState(ERYG.Off, ERYG.Off, ERYG.Off, EBuzzer.Off),//EMcState.Last
        //};

        //static System.Windows.Forms.Timer timer500 = new System.Windows.Forms.Timer();
        //internal static ETLState IntState = new ETLState();
        //public static void SetState(EMcState State)
        //{
        //    NUtils.RegistryWR Reg = new NUtils.RegistryWR("SOFTWARE");
        //    Reg.WriteKey("NSWAUTOMATION_STATE", "DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    Reg.WriteKey("NSWAUTOMATION_STATE", "STATE", State.ToString());

        //    //update last status if not same status
        //    if (!(State == EMcState.Last || State == EMcState.Mute || State == EMcState.Define))
        //    {
        //        if (IO.IntStates[(int)EMcState.Last].Red != IO.IntState.Red ||
        //            IO.IntStates[(int)EMcState.Last].Yel != IO.IntState.Yel ||
        //            IO.IntStates[(int)EMcState.Last].Grn != IO.IntState.Grn ||
        //            IO.IntStates[(int)EMcState.Last].Buz != IO.IntState.Buz)
        //            IO.IntStates[(int)EMcState.Last] = IO.IntState;
        //    }

        //    if (IntStates[(int)State].Red != ERYG.DontCare) IntState.Red = IntStates[(int)State].Red;
        //    if (IntStates[(int)State].Yel != ERYG.DontCare) IntState.Yel = IntStates[(int)State].Yel;
        //    if (IntStates[(int)State].Grn != ERYG.DontCare) IntState.Grn = IntStates[(int)State].Grn;
        //    if (IntStates[(int)State].Buz != EBuzzer.DontCare) IntState.Buz = IntStates[(int)State].Buz;

        //    if (!timer500.Enabled)
        //    {
        //        timer500.Interval = 500;
        //        timer500.Enabled = true;
        //        timer500.Tick += new EventHandler(timer500_Tick);
        //    }

        //    try
        //    {
        //        switch (IntState.Grn)
        //        {
        //            case ERYG.On:
        //                TFTowerLight.Green = true;
        //                break;
        //            case ERYG.Off:
        //                TFTowerLight.Green = false;
        //                break;
        //        }
        //        switch (IntState.Yel)
        //        {
        //            case ERYG.On:
        //                TFTowerLight.Yellow = true;
        //                break;
        //            case ERYG.Off:
        //                TFTowerLight.Yellow = false;
        //                break;
        //        }
        //        switch (IntState.Red)
        //        {
        //            case ERYG.On:
        //                TFTowerLight.Red = true;
        //                break;
        //            case ERYG.Off:
        //                TFTowerLight.Red = false;
        //                break;
        //        }
        //        switch (IntState.Buz)
        //        {
        //            case EBuzzer.On:
        //                TFTowerLight.Buzzer = true;
        //                break;
        //            case EBuzzer.Off:
        //                TFTowerLight.Buzzer = false;
        //                break;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        //public static void SetState(ERYG Red, ERYG Yel, ERYG Grn, EBuzzer Buz)
        //{
        //    IntStates[(int)EMcState.Define].Red = Red;
        //    IntStates[(int)EMcState.Define].Yel = Yel;
        //    IntStates[(int)EMcState.Define].Grn = Grn;
        //    IntStates[(int)EMcState.Define].Buz = Buz;

        //    SetState(EMcState.Define);
        //}
        //static bool b_Toggle = false;
        //static void timer500_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        b_Toggle = !b_Toggle;

        //        if (IntState.Grn == ERYG.Blink) TFTowerLight.Green = b_Toggle ? true : false;
        //        if (IntState.Yel == ERYG.Blink) TFTowerLight.Yellow = b_Toggle ? true : false;
        //        if (IntState.Red == ERYG.Blink) TFTowerLight.Red = b_Toggle ? true : false;
        //    }
        //    catch
        //    {
        //    }
        //}
    //}
}
