﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace NDispWin
{
    class TaskMHS
    {
        public static void LoadDIO()
        {
            if (!File.Exists(GDefine.MHSDIOAddFile))
            {
                frmMHS2PromptNewDIO frm = new frmMHS2PromptNewDIO();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
            }
            else
            {
                ConvIO.LoadDIOAdd(GDefine.MHSDIOAddFile);
                Conv2IO.LoadDIOAdd(GDefine.MHSDIOAddFile);
                ElevIO.LoadDIOAdd(GDefine.MHSDIOAddFile);
            }
            ConvIO.SaveDIOAdd(GDefine.MHSDIOAddFile);
            Conv2IO.SaveDIOAdd(GDefine.MHSDIOAddFile);
            ElevIO.SaveDIOAdd(GDefine.MHSDIOAddFile);
        }

        /// <summary>
        /// Load Recipe, Setup and IODefine Conv and Elev
        /// </summary>
        /// <param name="RecipeName"></param>
        public static void LoadRecipe()
        {
            string fullFileName = GDefine.MHSRecipePath + "\\" + GDefine.MHSRecipeName + GDefine.MHSRecipeExt;
            if (!File.Exists(fullFileName))
                MessageBox.Show("MHS Recipe not found. Default MHS Recipe values will be generated.");

            Event.OP_DISP_LOAD_MHS_RECIPE.Set("Name", fullFileName);

            TaskConv.LoadRecipe();
            TaskElev.LoadRecipe();
        }
        public static void LoadRecipe(string RecipeName)
        {
            GDefine.MHSRecipeName = RecipeName;
            LoadRecipe();
        }

        public static string RecipePath
        {
            get { return GDefine.MHSRecipePath; }
        }

        /// <summary>
        /// Save Recipe, Setup and IODefine Conv and Elev
        /// </summary>
        /// <param name="RecipeName"></param>   
        public static void SaveRecipe()
        {
            TaskConv.SaveRecipe();
            TaskElev.SaveRecipe();
        }
        public static void SaveRecipe(string RecipeName)
        {
            GDefine.MHSRecipeName = RecipeName;
            SaveRecipe();
        }

        #region Custom Param
        public enum ECustomMode { None, OSRAMSCCSeq }
        public static ECustomMode CustomMode = ECustomMode.None;
        #endregion
    }

    public class TaskConv
    {
        public static bool NewConvSequence = false;

        #region Status
        public enum EConvStatus
        {
            ErrorInit = 0,
            Ready,
            Busy,
            Stop,
            Reset,
        }
        private static Color[] ConvStatusColor = { Color.Red, Color.Lime, Color.Yellow, Color.Orange, Color.Gold };

        public enum EProcessStatus
        {
            NotReady = 0,
            Empty,
            WaitNone,
            Heating,//>Heating frame is psnt
            WaitDisp,
            WaitDisp2,
            InProcess,
            Psnt,
        }
        private static Color[] ProcessStatusColor =
        {
            Color.Fuchsia,
            Color.LightGray,
            Color.DarkOrange,
            Color.Orange,
            Color.Orange,
            Color.Orange,
            Color.Yellow,
            Color.Lime,
        };
        public enum EStation { Buf1, Buf2, Pre, Pro, Pos, Out }
        #endregion
        #region Param
        public enum ELeftMode
        {
            ManualLoad,
            ElevatorZ,
            Smema,
            NoSupport3,//Smema_SmemaRight,
            NoSupport4,//SmemaBiDirection,
        }
        public enum ERightMode
        {
            ManualUnload,
            ProductReturn,
            ElevatorZ,
            Smema,
            NoSupport4,//Smema_SmemaLeft,//uses UL Port to receive
            NoSupport5,//SmemaBiDirection,
        }
        public static ELeftMode LeftMode = new ELeftMode();
        public static ERightMode RightMode = new ERightMode();
        public static bool OutLevelQtyFollowIn = false;

        public enum EConvPara
        {
            MotorReso = 0,
            InvertMotorOn,
            InvertMotorDir,
            InitSpeed,
            RunSpeed_Slow,
            RunSpeed_Fast,
            RunSpeed_SendOut,
            PartLength,
            Width,
            BlowSuckTime,
        }
        public const int CONV_PARA_COUNT = 10;
        #region TConvPara
        public static string[] TConvParaStr =
            {
                "Motor Pulse Resolution",
                "Invert Motor On",
                "Invert Motor Dir",
                "Init Speed (mm/s)",
                "Run Speed Slow (mm/s)",
                "Run Speed Fast (mm/s)",
                "Run Speed Send Out (mm/s)",
                "Part Length (mm)",
                "Conv Width (um)",
                "Blow Suck Time (s)",
            };
        public static int[] TConvParaMin =
            {
                100,//"Motor Pulse Resolution
                0, //"INVERT MOTOR ON OFF
                0, //"INVERT MOTOR DIR",
                1, //"INIT SPEED",
                1, //"RUN SPEED SLOW",
                1, //"RUN SPEED FAST",
                1,//RunSpeed_SendOut,
                50, //"FRAME LENGTH (mm)",
                0, //"Width (mm)",
                0, //"Blow Suck Time (s)",
            };
        public static int[] TConvParaMax =
            {
                12800,//"Motor Pulse Resolution
                1, //"INVERT MOTOR ON OFF
                1, //"INVERT MOTOR DIR",
                400, //"INIT SPEED",
                400, //"RUN SPEED SLOW",
                400, //"RUN SPEED FAST",
                400, //RunSpeed_SendOut,
                200, //"FRAME LENGTH (mm)",
                150000, //"Width (mm)",
                10, //"Blow Suck Time (s)",
            };
        public static int[] TConvParaDef =
            {
                2500,//"Motor Pulse Resolution
                0, //"INVERT MOTOR ON OFF
                1, //"INVERT MOTOR DIR",
                150, //"INIT SPEED",
                80, //"RUN SPEED SLOW",
                250, //"RUN SPEED FAST",
                150, //RunSpeed_SendOut,
                150, //"FRAME LENGTH (mm)",
                0, //"Width (mm)",
                5, //"Blow Suck Time (s)",
            };
        public static string[] TConvParaDesc =
            {
                "Motor Pulse Resolution (hz)", //"MotorPulse Resolution
                "Invert conveyor motor On(NC) Off(NO)", //"INVERT MOTOR ON OFF
                "Invert conveyor motor if it opposite located (0 -> Disable, 1 -> Enabled)", //"INVERT MOTOR DIR",
                "Conveyor initialize speed in (mm/s)", //"INIT SPEED",
                "Conveyor speed from when product detected (mm/s)", //"RUN SPEED SLOW",
                "Conveyor run speed (mm/s)", //"RUN SPEED FAST",
                "Conveyor speed for part send out (mm/s)", //RunSpeed_SendOut,
                "Length of part in (mm)", //"FRAME LENGTH (mm)",
                "Conveyor Width in (um)",
                "Blow Suck Time in (s)",
            };
        #endregion

        public enum EPara
        {
            Stopper_Enable = 0,
            StopperUp_Delay,
            StopperDn_Delay,

            Lifter_Enable,
            LifterUp_Delay,
            LifterDn_Delay,

            Precisor_Type,
            PrecisorExt_Delay,
            PrecisorRet_Delay,

            Vac1_Enable,
            Vac1On_Delay,
            Vac1Off_Delay,

            Vac2_Enable,
            Vac2On_Delay,
            Vac2Off_Delay,

            HeatTime,
            CheckAlarm,

            Load_Delay,

            TimeOut,
        }
        public const int ST_PARA_COUNT = 19;
        #region TPara
        public static string[] TParaStr =// new string[ST_PARA_COUNT]
            {
                "Stopper Enable",
                "Stopper Up Delay (ms)",
                "Stopper Dn Delay (ms)",

                "Lifter Enable",
                "Lifter Up Delay (ms)",
                "Lifter Dn Delay (ms)",

                "Precisor Type",
                "Precisor Ext Delay (ms)",
                "Precisor Ret Delay (ms)",

                "Vacuum 1 Enable",
                "Vacuum 1 On Delay (ms)",
                "Vacuum 1 Off Delay (ms)",

                "Vacuum 2 Enable",
                "Vacuum 2 On Delay (ms)",
                "Vacuum 2 Off Delay (ms)",

                "Heat Time (s)",
                "Heater Check Alarm",

                "Loading Delay (ms)",

                "TimeOut (ms)",
            };
        public static int[] TParaMin =
            {
                0,//"Stopper Enable",
                0,//"Stopper Up Delay (ms)",
                0,//"Stopper Dn Delay (ms)",

                0,//"Lifter Enable",
                0,//"Lifter Up Delay (ms)",
                0,//"Lifter Dn Delay (ms)",

                0,//"Precisor Type",
                0,//"Precisor Ext Delay (ms)",
                0,//"Precisor Ret Delay (ms)",

                0,//"Vacuum 1 Enable",
                0,//"Vacuum 1 On Delay (ms)",
                0,//"Vacuum 1 Off Delay (ms)",

                0,//"Vacuum 2 Enable",
                0,//"Vacuum 2 On Delay (ms)",
                0,//"Vacuum 2 Off Delay (ms)",

                0,//"Heat Time (s)",
                0,//"Heater Check Alarm",

                0,//"Loading Delay (ms)",

                0,//"TimeOut (ms)",
            };
        public static int[] TParaMax =
            {
                2,//1, //"STOPPER ENABLED",
                15000, //"STOPPER UP END DELAY",
                15000, //"STOPPER DN END DELAY",

                1, //"LIFTER ENABLED",
                15000, //"LIFTER UP END DELAY",
                15000, //"LIFTER DN END DELAY",

                2, //"Precisor ENABLED",
                15000, //PrecisorExt_ED,
                15000, //PrecisorRet_SD,

                2, //"VAC 1 ENABLED
                15000, //"VAC 1 END DELAY",
                15000, //VAC 1 TIMEOUT
                
                1, //"VAC 2 ENABLED
                15000, //"VAC 2 END DELAY",
                15000, //"VAC 2 TIMEOUT",

                6000, //"HEAT TIME",
                1, //Check Alarm

                15000, //"LOADING DELAY",
                60000, //"TIMEOUT",
                

                0,//"Not Used",
                15000,//"Stopper Up Delay (ms)",
                15000,//"Stopper Dn Delay (ms)",

                1,//"Lifter Enable",
                15000,//"Lifter Up Delay (ms)",
                15000,//"Lifter Dn Delay (ms)",

                2,//"Precisor Type",
                15000,//"Precisor Ext Delay (ms)",
                15000,//"Precisor Ret Delay (ms)",

                1,//"Vacuum 1 Enable",
                15000,//"Vacuum 1 On Delay (ms)",
                15000,//"Vacuum 1 Off Delay (ms)",

                2,//"Vacuum 2 Enable",
                15000,//"Vacuum 2 On Delay (ms)",
                15000,//"Vacuum 2 Off Delay (ms)",

                15000,//"Heat Time (s)",
                1,//"Heater Check Alarm",

                15000,//"Loading Delay (ms)",

                60000,//"TimeOut (ms)",
            };
        public static int[] TParaDef =
            {
                0, //"STOPPER ENABLED",
                500, //"STOPPER UP END DELAY",
                500, //"STOPPER DN END DELAY",
        
                0, //"LIFTER ENABLED",
                500, //"LIFTER UP END DELAY",
                500, //"LIFTER DN END DELAY",

                0, //"Precisor ENABLED",
                500, //PrecisorExt_ED,
                500, //PrecisorRet_ED,

                0, //VAC 1 ENABLED
                250, //"VAC 1 END DELAY",
                250, //"VAC 1 TIMEOUT",
                
                0, //"VAC 2 ENABLED",
                250, //"VAC 2 END DELAY",
                250, //"VAC 2 TIMEOUT",

                0, //"HEAT TIME",
                0, //Check Alarm

                800, //"LOADING DELAY",
                5000, //"TIMEOUT",
            };
        public static string[] TParaDesc =
            {
                "Stopper Enable (0, 1-> Enable, 2 -> Enable-Down)",
                "Delay after Stopper Up",
                "Delay after Stopper Dn",

                "Lifter Enable (0 -> Disable, 1 -> Enable)",
                "Delay after Lifter Up",
                "Delay after Lifter Dn",

                "Precisor Enable (0 -> Disable, 1 -> Enable, 2 -> Engaged)",
                "Delay after Precisor Up",
                "Delay after Precisor Dn",

                "Vacuum 1 Enable (0 -> Disable, 1 -> Enable, 2 -> Early)",
                "Delay after Vacuum 1 On",
                "Delay after Vacuum 1 Off",

                "Vacuum 2 Enable (0 -> Disable, 1 -> Enable)",
                "Delay after Vacuum 2 On",
                "Delay after Vacuum 2 Off",

                "Heating Time",
                "Enable Check Heater Alarm",

                "Delay after load in position before stop conveyor",

                "Operation Process TimeOut (0 -> Disable)",
            };
        #endregion

        public enum EOutPara
        {
            Delay,

            KickerEnabled,
            KickerExt_Delay,
            KickerRet_Delay,

            TimeOut,
        }
        public const int ST_OUT_PARA_COUNT = 5;
        #region TOutPara
        public static string[] TOutParaStr =
            {
                "Delay (ms)",
                "Kicker Enabled",
                "Kicker Ext Delay (ms)",
                "Kicker Ret Delay (ms)",
                "TimeOut (ms)",
            };
        public static int[] TOutParaMin =
            {
                10,//"Delay (ms)",
                0, //"Kicker Enabled",
                0, //"Kicker Ext Delay (ms)",
                0, //"Kicker Ret Delay (ms)",
                10,//"TimeOut (ms)",
            };
        public static int[] TOutParaMax =
            {
                15000,//"Delay (ms)",
                1, //"Kicker Enabled",
                15000, //"Kicker Ext Delay (ms)",
                15000, //"Kicker Ret Delay (ms)",
                15000,//"TimeOut (ms)",
            };
        public static int[] TOutParaDef =
            {
                250,//"TimeOut (ms)",
                0, //"Kicker Enabled",
                500, //"Kicker Ext Delay (ms)",
                500, //"Kicker Ret Delay (ms)",
                5000,//"TimeOut (ms)",
            };
        public static string[] TOutParaDesc =
            {
                "Minimum delay for Board to clear Out Sensor",
                "Enabled kicker for kick & clear product after send out", //"Kicker Enabled",
                "Delay after Kicker extend", //"Kicker Ext Delay (ms)",
                "Delay after Kicker retract", //"Kicker Ret Delay (ms)",
                "Operation Timeout", //"TimeOut (ms)",
            };
        #endregion

        public class TSetup
        {
            public bool ByPassDispPro;
            public int[] ConvPara = new int[CONV_PARA_COUNT];
            public int[] Pre = new int[ST_PARA_COUNT];
            public int[] Pro = new int[ST_PARA_COUNT];
            public int[] Pos = new int[ST_PARA_COUNT];
            public int[] Out = new int[ST_OUT_PARA_COUNT];

            public int[] Conv2Para = new int[CONV_PARA_COUNT];
            public int[] Pos2 = new int[ST_PARA_COUNT];
            public int[] Out2 = new int[ST_OUT_PARA_COUNT];

            public TSetup()
            {
                for (int k = 0; k < CONV_PARA_COUNT; k++)
                {
                    ConvPara[k] = 0;
                    Conv2Para[k] = 0;
                }

                for (int j = 0; j < ST_PARA_COUNT; j++)
                {
                    Pre[j] = 0;
                    Pro[j] = 0;
                    Pos2[j] = 0;
                }

                //ErrCode ErrCode = new ErrCode();
            }
        }
        public static TSetup Setup = new TSetup();

        public enum EBufStType { None, Buffer };
        public enum EPreStType { None, Buffer };//, Disp, Disp1, Disp12 };
        public enum EProStType { None, Buffer, Disp };//, Disp2, Disp12 };
        public enum EPosStType { None, Buffer };

        public static bool EnableUnloadMsg = false;
        public static bool EnableVacuumPump = false;
        public static bool EnableBlowSuck = false;
        //public static bool EnableInSensLFPsnt = false;
        //public static bool EnableOutSensLFPsnt = false;
        public static int AutoOffVacuumPumpTime = 0;

        public static bool EnableDoorSens = false;
        public static bool EnableDoorLock = false;

        public static bool InMcReadyFollowSensInPsnt = false;//set UL McReady to low once clear InPsnt
        public static bool OutBdReadyWaitMcReady = false;//wait for DL McReady to start handshake, else high immediately.
        #endregion

        //Process Flags
        public static bool StopInput = false;//disable input of new board
        public static bool SkipDisp = false;
        public static bool DispEndStop = false;//after dispense complete, stop
        public static bool UnloadStop = false;//after unload stop

        public static bool OpenBoard()
        {
            return OpenBoard(ConvIO.BoardID, ConvIO.DIOModel);
        }
        public static bool OpenBoard(byte BoardID, ZEC3002.Ctrl.TDIOModel IOModel)
        {
            string CMsg = "TaskConv.OpenBoard " + IOModel.ToString() + " " + BoardID.ToString() + (char)13;

            if (!ZEC3002.Ctrl.OpenBoard(BoardID, IOModel))
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_OPEN_BOARD_FAIL);
                return false;
            }
            return true;
        }
        public static bool BoardIsOpen
        {
            get
            {
                return ZEC3002.Ctrl.BoardOpened(ConvIO.BoardID);
            }
        }
        public static void CloseBoard(int BoardID)
        {
            try
            {
                ZEC3002.Ctrl.CloseBoard(BoardID);
            }
            catch { };
        }

        public static void SaveRecipe(string Filename) 
        {
            NUtils.IniFile IniFile = new NUtils.IniFile(GDefine.MHSRecipePath + "\\" + Filename);

            string S = "Config";
            string Key = "";

            IniFile.WriteInteger(S, "LeftMode", (int)LeftMode);
            IniFile.WriteInteger(S, "RightMode", (int)RightMode);
            IniFile.WriteBool(S, "OutLevelQtyFollowIn", OutLevelQtyFollowIn);

            S = "Setup";
            for (int k = 0; k < CONV_PARA_COUNT; k++)
            {
                Key = "Conv_" + Enum.GetName(typeof(EConvPara), k).ToString();
                IniFile.WriteInteger(S, Key, Setup.ConvPara[k]);
                Key = "Conv2_" + Enum.GetName(typeof(EConvPara), k).ToString();
                IniFile.WriteInteger(S, Key, Setup.Conv2Para[k]);
            }

            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pre_" + Enum.GetName(typeof(EPara), j).ToString();
                IniFile.WriteInteger(S, Key, Setup.Pre[j]);
            }

            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pro_" + Enum.GetName(typeof(EPara), j).ToString();
                IniFile.WriteInteger(S, Key, Setup.Pro[j]);
            }

            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pos_" + Enum.GetName(typeof(EPara), j).ToString();
                IniFile.WriteInteger(S, Key, Setup.Pos[j]);
            }

            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pos2_" + Enum.GetName(typeof(EPara), j).ToString();
                IniFile.WriteInteger(S, Key, Setup.Pos2[j]);
            }

            for (int j = 0; j < ST_OUT_PARA_COUNT; j++)
            {
                Key = "Out_" + Enum.GetName(typeof(EOutPara), j).ToString();
                IniFile.WriteInteger(S, Key, Setup.Out[j]);
                Key = "Out2_" + Enum.GetName(typeof(EOutPara), j).ToString();
                IniFile.WriteInteger(S, Key, Setup.Out2[j]);
            }

            IniFile.WriteInteger(S, "Buf1_StType", (int)Buf1.StType);
            IniFile.WriteInteger(S, "Buf2_StType", (int)Buf2.StType);
            IniFile.WriteInteger(S, "Pre_StType", (int)Pre.StType);
            IniFile.WriteInteger(S, "Pro_StType", (int)Pro.StType);
            IniFile.WriteInteger(S, "Pos_StType", (int)Pos.StType);

            S = "Option";
            IniFile.WriteBool(S, "EnableUnloadMsg", EnableUnloadMsg);
            IniFile.WriteBool(S, "EnableVacuumPump", EnableVacuumPump);
            IniFile.WriteBool(S, "EnableBlowSuck", EnableBlowSuck);
            IniFile.WriteInteger(S, "AutoOffVaccumPumpTime", AutoOffVacuumPumpTime);

            IniFile.WriteBool(S, "InMcReadyFollowSensInPsnt", TaskConv.InMcReadyFollowSensInPsnt);
            IniFile.WriteBool(S, "OutBdReadyWaitMcReady", TaskConv.OutBdReadyWaitMcReady);

            IniFile.WriteBool(S, "NewConvSequence", TaskConv.NewConvSequence);
        }
        public static void SaveRecipe()
        {
            SaveRecipe(GDefine.MHSRecipeName + GDefine.MHSRecipeExt);
        }
        public static void LoadRecipe()
        {
            NUtils.IniFile IniFile = new NUtils.IniFile(GDefine.MHSRecipePath + "\\" + GDefine.MHSRecipeName + GDefine.MHSRecipeExt);

            string S = "Config";
            string Key = "";

            LeftMode = (ELeftMode)IniFile.ReadInteger(S, "LeftMode", 0);
            RightMode = (ERightMode)IniFile.ReadInteger(S, "RightMode", 0);
            OutLevelQtyFollowIn = IniFile.ReadBool(S, "OutLevelQtyFollowIn", false);

            S = "Setup";
            for (int k = 0; k < CONV_PARA_COUNT; k++)
            {
                Thread.Sleep(1);
                Key = "Conv_" + Enum.GetName(typeof(EConvPara), k).ToString();
                Setup.ConvPara[k] = IniFile.ReadInteger(S, Key, TConvParaDef[k]);
                Key = "Conv2_" + Enum.GetName(typeof(EConvPara), k).ToString();
                Setup.Conv2Para[k] = IniFile.ReadInteger(S, Key, TConvParaDef[k]);
            }
            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pre_" + Enum.GetName(typeof(EPara), j).ToString();
                Setup.Pre[j] = IniFile.ReadInteger(S, Key, TParaDef[j]);
            }

            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pro_" + Enum.GetName(typeof(EPara), j).ToString();
                Setup.Pro[j] = IniFile.ReadInteger(S, Key, TParaDef[j]);
            }

            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pos_" + Enum.GetName(typeof(EPara), j).ToString();
                Setup.Pos[j] = IniFile.ReadInteger(S, Key, TParaDef[j]);
            }

            for (int j = 0; j < ST_PARA_COUNT; j++)
            {
                Key = "Pos2_" + Enum.GetName(typeof(EPara), j).ToString();
                Setup.Pos2[j] = IniFile.ReadInteger(S, Key, TParaDef[j]);
            }

            for (int j = 0; j < ST_OUT_PARA_COUNT; j++)
            {
                Key = "Out_" + Enum.GetName(typeof(EOutPara), j).ToString();
                Setup.Out[j] = IniFile.ReadInteger(S, Key, TOutParaDef[j]);
                Key = "Out2_" + Enum.GetName(typeof(EOutPara), j).ToString();
                Setup.Out2[j] = IniFile.ReadInteger(S, Key, TOutParaDef[j]);
            }

            Buf1.StType = (EBufStType)IniFile.ReadInteger(S, "Buf1_StType", 0);
            Buf2.StType = (EBufStType)IniFile.ReadInteger(S, "Buf2_StType", 0);
            Pre.StType = (EPreStType)IniFile.ReadInteger(S, "Pre_StType", (int)EPreStType.Buffer);
            Pro.StType = (EProStType)IniFile.ReadInteger(S, "Pro_StType", (int)EProStType.Disp);
            Pos.StType = (EPosStType)IniFile.ReadInteger(S, "Pos_StType", (int)EProStType.None);

            S = "Option";
            EnableUnloadMsg = IniFile.ReadBool(S, "EnableUnloadMsg", false);
            EnableVacuumPump = IniFile.ReadBool(S, "EnableVacuumPump", false);
            EnableBlowSuck = IniFile.ReadBool(S, "EnableBlowSuck", false);
            AutoOffVacuumPumpTime = IniFile.ReadInteger(S, "AutoOffVaccumPumpTime", 0);

            TaskConv.InMcReadyFollowSensInPsnt = IniFile.ReadBool(S, "InMcReadyFollowSensInPsnt", false);
            TaskConv.OutBdReadyWaitMcReady = IniFile.ReadBool(S, "OutBdReadyWaitMcReady", false);

            TaskConv.NewConvSequence = IniFile.ReadBool(S, "NewConvSequence", true);
        }

        public static void MigrateMHSRecipe()//migrate from MHS and save to MHS2
        {
            string[] Files = Directory.GetFiles("C:\\Program Files\\NSWAutomation\\NDisp3Win\\MHS", "*.hdlr");

            foreach (string file in Files)
            {
                NUtils.IniFile IniFile = new NUtils.IniFile(file);

                string S = "Config";

                LeftMode = (ELeftMode)IniFile.ReadInteger(S, "LMode", 0);
                RightMode = (ERightMode)IniFile.ReadInteger(S, "RMode", 0);

                S = "Setup0";
                Setup.ConvPara[(int)EConvPara.MotorReso] = IniFile.ReadInteger(S, "ConvSpeed-" + "MOTOR PULSE RESOLUTION", TConvParaDef[(int)EConvPara.MotorReso]);
                Setup.ConvPara[(int)EConvPara.InvertMotorOn] = IniFile.ReadInteger(S, "ConvSpeed-" + "INVERT MOTOR ON OFF", TConvParaDef[(int)EConvPara.InvertMotorOn]);
                Setup.ConvPara[(int)EConvPara.InvertMotorDir] = IniFile.ReadInteger(S, "ConvSpeed-" + "INVERT MOTOR DIRECTION", TConvParaDef[(int)EConvPara.InvertMotorDir]);
                Setup.ConvPara[(int)EConvPara.InitSpeed] = IniFile.ReadInteger(S, "ConvSpeed-" + "INIT SPEED (mm/s)", TConvParaDef[(int)EConvPara.InitSpeed]);
                Setup.ConvPara[(int)EConvPara.RunSpeed_Slow] = IniFile.ReadInteger(S, "ConvSpeed-" + "RUN SPEED-SLOW (mm/s)", TConvParaDef[(int)EConvPara.RunSpeed_Slow]);
                Setup.ConvPara[(int)EConvPara.RunSpeed_Fast] = IniFile.ReadInteger(S, "ConvSpeed-" + "RUN SPEED-FAST (mm/s)", TConvParaDef[(int)EConvPara.RunSpeed_Fast]);
                Setup.ConvPara[(int)EConvPara.RunSpeed_SendOut] = IniFile.ReadInteger(S, "ConvSpeed-" + "RUN SPEED_SEND OUT (mm/s)", TConvParaDef[(int)EConvPara.RunSpeed_SendOut]);
                Setup.ConvPara[(int)EConvPara.PartLength] = IniFile.ReadInteger(S, "ConvSpeed-" + "PART LENGTH (mm)", TConvParaDef[(int)EConvPara.PartLength]);

                #region Pre
                Setup.Pre[(int)EPara.Stopper_Enable] = IniFile.ReadInteger(S, "PRE-" + "Stopper Enabled", TParaDef[(int)EPara.Stopper_Enable]);
                Setup.Pre[(int)EPara.StopperUp_Delay] = IniFile.ReadInteger(S, "PRE-" + "Stopper Up End Delay (ms)", TParaDef[(int)EPara.StopperUp_Delay]);
                Setup.Pre[(int)EPara.StopperDn_Delay] = IniFile.ReadInteger(S, "PRE-" + "Stopper Dn End Delay (ms)", TParaDef[(int)EPara.StopperDn_Delay]);
                Setup.Pre[(int)EPara.Lifter_Enable] = IniFile.ReadInteger(S, "PRE-" + "Lifter Enabled", TParaDef[(int)EPara.Lifter_Enable]);
                Setup.Pre[(int)EPara.LifterUp_Delay] = IniFile.ReadInteger(S, "PRE-" + "Lifter Up End Delay (ms)", TParaDef[(int)EPara.LifterUp_Delay]);
                Setup.Pre[(int)EPara.LifterDn_Delay] = IniFile.ReadInteger(S, "PRE-" + "Lifter Dn End Delay (ms)", TParaDef[(int)EPara.LifterDn_Delay]);
                Setup.Pre[(int)EPara.Precisor_Type] = IniFile.ReadInteger(S, "PRE-" + "Precisor Enabled", TParaDef[(int)EPara.Precisor_Type]);
                Setup.Pre[(int)EPara.PrecisorExt_Delay] = IniFile.ReadInteger(S, "PRE-" + "Precisor Ext End Delay (ms)", TParaDef[(int)EPara.PrecisorExt_Delay]);
                Setup.Pre[(int)EPara.PrecisorRet_Delay] = IniFile.ReadInteger(S, "PRE-" + "Precisor Ret End Delay (ms)", TParaDef[(int)EPara.PrecisorRet_Delay]);
                Setup.Pre[(int)EPara.Vac1_Enable] = IniFile.ReadInteger(S, "PRE-" + "Vacuum 1 Enabled", TParaDef[(int)EPara.Vac1_Enable]);
                Setup.Pre[(int)EPara.Vac1On_Delay] = IniFile.ReadInteger(S, "PRE-" + "Vacuum 1 End Delay (ms)", TParaDef[(int)EPara.Vac1On_Delay]);
                Setup.Pre[(int)EPara.Vac1Off_Delay] = IniFile.ReadInteger(S, "PRE-" + "Vacuum 1 TimeOut (ms)", TParaDef[(int)EPara.Vac1Off_Delay]);
                Setup.Pre[(int)EPara.Vac2_Enable] = IniFile.ReadInteger(S, "PRE-" + "Vacuum 1 Enabled", TParaDef[(int)EPara.Vac2_Enable]);
                Setup.Pre[(int)EPara.Vac2On_Delay] = IniFile.ReadInteger(S, "PRE-" + "Vacuum 1 End Delay (ms)", TParaDef[(int)EPara.Vac2On_Delay]);
                Setup.Pre[(int)EPara.Vac2Off_Delay] = IniFile.ReadInteger(S, "PRE-" + "Vacuum 1 TimeOut (ms)", TParaDef[(int)EPara.Vac2Off_Delay]);
                Setup.Pre[(int)EPara.HeatTime] = IniFile.ReadInteger(S, "PRE-" + "Heat Time (s)", TParaDef[(int)EPara.HeatTime]);
                Setup.Pre[(int)EPara.CheckAlarm] = IniFile.ReadInteger(S, "Heater_L CheckAlarm", TParaDef[(int)EPara.CheckAlarm]);
                Setup.Pre[(int)EPara.Load_Delay] = IniFile.ReadInteger(S, "PRE-" + "Loading Delay (ms)", TParaDef[(int)EPara.Load_Delay]);
                Setup.Pre[(int)EPara.TimeOut] = IniFile.ReadInteger(S, "PRE-" + "TimeOut (ms)", TParaDef[(int)EPara.TimeOut]);
                #endregion

                #region Pro
                Setup.Pro[(int)EPara.Stopper_Enable] = IniFile.ReadInteger(S, "PRO-" + "Stopper Enabled", TParaDef[(int)EPara.Stopper_Enable]);
                Setup.Pro[(int)EPara.StopperUp_Delay] = IniFile.ReadInteger(S, "PRO-" + "Stopper Up End Delay (ms)", TParaDef[(int)EPara.StopperUp_Delay]);
                Setup.Pro[(int)EPara.StopperDn_Delay] = IniFile.ReadInteger(S, "PRO-" + "Stopper Dn End Delay (ms)", TParaDef[(int)EPara.StopperDn_Delay]);
                Setup.Pro[(int)EPara.Lifter_Enable] = IniFile.ReadInteger(S, "PRO-" + "Lifter Enabled", TParaDef[(int)EPara.Lifter_Enable]);
                Setup.Pro[(int)EPara.LifterUp_Delay] = IniFile.ReadInteger(S, "PRO-" + "Lifter Up End Delay (ms)", TParaDef[(int)EPara.LifterUp_Delay]);
                Setup.Pro[(int)EPara.LifterDn_Delay] = IniFile.ReadInteger(S, "PRO-" + "Lifter Dn End Delay (ms)", TParaDef[(int)EPara.LifterDn_Delay]);
                Setup.Pro[(int)EPara.Precisor_Type] = IniFile.ReadInteger(S, "PRO-" + "Procisor Enabled", TParaDef[(int)EPara.Precisor_Type]);
                Setup.Pro[(int)EPara.PrecisorExt_Delay] = IniFile.ReadInteger(S, "PRO-" + "Procisor Ext End Delay (ms)", TParaDef[(int)EPara.PrecisorExt_Delay]);
                Setup.Pro[(int)EPara.PrecisorRet_Delay] = IniFile.ReadInteger(S, "PRO-" + "Procisor Ret End Delay (ms)", TParaDef[(int)EPara.PrecisorRet_Delay]);
                Setup.Pro[(int)EPara.Vac1_Enable] = IniFile.ReadInteger(S, "PRO-" + "Vacuum 1 Enabled", TParaDef[(int)EPara.Vac1_Enable]);
                Setup.Pro[(int)EPara.Vac1On_Delay] = IniFile.ReadInteger(S, "PRO-" + "Vacuum 1 End Delay (ms)", TParaDef[(int)EPara.Vac1On_Delay]);
                Setup.Pro[(int)EPara.Vac1Off_Delay] = IniFile.ReadInteger(S, "PRO-" + "Vacuum 1 TimeOut (ms)", TParaDef[(int)EPara.Vac1Off_Delay]);
                Setup.Pro[(int)EPara.Vac2_Enable] = IniFile.ReadInteger(S, "PRO-" + "Vacuum 1 Enabled", TParaDef[(int)EPara.Vac2_Enable]);
                Setup.Pro[(int)EPara.Vac2On_Delay] = IniFile.ReadInteger(S, "PRO-" + "Vacuum 1 End Delay (ms)", TParaDef[(int)EPara.Vac2On_Delay]);
                Setup.Pro[(int)EPara.Vac2Off_Delay] = IniFile.ReadInteger(S, "PRO-" + "Vacuum 1 TimeOut (ms)", TParaDef[(int)EPara.Vac2Off_Delay]);
                Setup.Pro[(int)EPara.HeatTime] = IniFile.ReadInteger(S, "PRO-" + "Heat Time (s)", TParaDef[(int)EPara.HeatTime]);
                Setup.Pro[(int)EPara.CheckAlarm] = IniFile.ReadInteger(S, "Heater_L CheckAlarm", TParaDef[(int)EPara.CheckAlarm]);
                Setup.Pro[(int)EPara.Load_Delay] = IniFile.ReadInteger(S, "PRO-" + "Loading Delay (ms)", TParaDef[(int)EPara.Load_Delay]);
                Setup.Pro[(int)EPara.TimeOut] = IniFile.ReadInteger(S, "PRO-" + "TimeOut (ms)", TParaDef[(int)EPara.TimeOut]);
                #endregion

                #region Out
                Setup.Out[(int)EOutPara.KickerEnabled] = IniFile.ReadInteger(S, "POS-" + "Kicker Enabled", TParaDef[(int)EOutPara.KickerEnabled]);
                Setup.Out[(int)EOutPara.KickerExt_Delay] = IniFile.ReadInteger(S, "POS-" + "Kicker Up End Delay (ms)", TParaDef[(int)EOutPara.KickerExt_Delay]);
                Setup.Out[(int)EOutPara.KickerRet_Delay] = IniFile.ReadInteger(S, "POS-" + "Kicker Dn End Delay (ms)", TParaDef[(int)EOutPara.KickerRet_Delay]);
                Setup.Out[(int)EOutPara.TimeOut] = IniFile.ReadInteger(S, "POS-" + "Unloading Delay (ms)", TParaDef[(int)EOutPara.TimeOut]);
                #endregion

                Buf1.StType = (EBufStType)IniFile.ReadInteger(S, "Buf1StMode", 0);
                Buf2.StType = (EBufStType)IniFile.ReadInteger(S, "Buf2StMode", 0);
                Pre.StType = (EPreStType)IniFile.ReadInteger(S, "PreStMode", (int)EPreStType.Buffer);
                Pro.StType = (EProStType)IniFile.ReadInteger(S, "ProStMode", (int)EProStType.Disp);

                S = "Option";
                EnableUnloadMsg = IniFile.ReadBool(S, "EnableUnloadMsg", false);
                EnableVacuumPump = IniFile.ReadBool(S, "EnableVacuumPump", false);
                EnableBlowSuck = IniFile.ReadBool(S, "EnableBlowSuck", false);
                AutoOffVacuumPumpTime = IniFile.ReadInteger(S, "AutoOffVaccumPumpTime", 0);
                //TaskConv.EnableDoorSens = IniFile.ReadBool("Conv_Option", "EnableDoorSens", false);
                //TaskConv.EnableDoorLock = IniFile.ReadBool("Conv_Option", "EnableDoorLock", false);

                SaveRecipe(Path.GetFileName(file));
            }
        }

        private static bool Delay(int Delay_ms)
        {
            if (Delay_ms <= 0) { return true; }
            int t = Environment.TickCount + Delay_ms;

            while (true)
            {
                if (Environment.TickCount >= t) { break; }
                Thread.Sleep(1);
            }

            return true;
        }

        public static EConvStatus Status = EConvStatus.ErrorInit;
        public static Color StatusColor
        {
            get
            {
                return ConvStatusColor[(int)Status];
            }
        }
        public static bool Ready
        {
            get
            {
                switch (Status)
                {
                    case EConvStatus.Ready:
                    case EConvStatus.Stop:
                        return true;
                    case EConvStatus.ErrorInit:
                    case EConvStatus.Reset:
                    case EConvStatus.Busy:
                    default:
                        return false;
                }
            }
        }
        public static bool CheckReady()
        {
            if (!Ready)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_NOT_READY);
                return false;
            }
            return true;
        }

        public static bool Init()
        {
            string EMsg = "Init";
            bool BeginFlag = false;

            try
            {
                TaskConv.Status = EConvStatus.ErrorInit;

                TaskConv.Buf1.Status = EProcessStatus.NotReady;
                TaskConv.Buf2.Status = EProcessStatus.NotReady;

                TaskConv.Pre.Status = EProcessStatus.NotReady;
                TaskConv.Pro.Status = EProcessStatus.NotReady;
                TaskConv.Pos.Status = EProcessStatus.NotReady;
                TaskConv.Out.Status = EProcessStatus.NotReady;

                TaskConv.Pre._StType = TaskConv.Pre.StType;
                TaskConv.Pro._StType = TaskConv.Pro.StType;

                Thread.Sleep(5);

                if (!BeginFlag)
                {
                    if (!ZEC3002.Ctrl.BoardOpened(ConvIO.BoardID))
                        if (!TaskConv.OpenBoard()) goto _Error;

                    if (!Delay(10)) { goto _Error; }

                    if (ZEC3002.Ctrl.BoardOpened(ElevIO.BoardID))
                        if (!ElevIO.MtrOnOff(ref ElevIO.CWAxis, true)) goto _Error;
                }
                BeginFlag = true;

                #region Reset Flags
                StopInput = false;
                SkipDisp = false;// CompleteProcess = false;
                DispEndStop = false;// .EndBoard = false;
                                    //GDefine.AbortBoard = false;
                ConvRun = false;
                #endregion

                #region Reset Outputs
                In.SvBlowSuck = false;

                if (Buf1.rt_StType == EBufStType.Buffer) Buf1.SvStopperUp = false;
                if (Buf2.rt_StType == EBufStType.Buffer) Buf2.SvStopperUp = false;

                Pre.SvVac = false;
                Pre.SvPrecisorExt = false;
                Pre.SvStopperUp = false;
                Pre.SvLifterUp = false;

                Pro.SvVac = false;
                Pro.SvVac2 = false;
                Pro.SvPrecisorExt = false;
                Pro.SvStopperUp = false;
                Pro.SvLifterUp = false;

                Pro.SvVac = false;
                Pro.SvPrecisorExt = false;
                Pro.SvStopperUp = false;
                Pro.SvLifterUp = false;

                Out.SvKickerExt = false;

                if (
                    (Buf1.rt_StType == EBufStType.Buffer && !Buf1.StopperDn()) ||
                    (Buf2.rt_StType == EBufStType.Buffer && !Buf2.StopperDn()) ||

                    !Pre.VacOff() || !Pre.PrecisorRet() || !Pre.StopperDn() || !Pre.LifterDn()
                    || !Pro.VacOff() || !Pro.PrecisorRet() || !Pro.StopperDn() || !Pro.LifterDn()
                    || !Out.KickerRet()
                    ) goto _Error;

            #endregion
            _Retry:
                #region Check Product
                if (!Out.CheckEmpty()) goto _Error;
                if (!In.CheckEmpty()) goto _Error;
                #endregion

                #region Conv On
                if (!Conv.Fwd_Init()) goto _Error;

                int TOut = Environment.TickCount + 3000;
                while (true)
                {
                    if (Environment.TickCount >= TOut)
                    {
                        if (!Conv.Stop()) goto _Error;
                        break;
                    }
                    if (In.SensPsnt || Out.SensPsnt)
                    {
                        if (!Conv.Stop()) goto _Error;
                        Msg MsgBox = new Msg();
                        string msg = EMsg;
                        goto _Retry;
                    }
                }
                #endregion

                #region Pro Vacuum Pump On
                //VacuumPumpOn(ConvIdx);
                #endregion

                TaskConv.Buf1.Status = EProcessStatus.Empty;
                TaskConv.Buf2.Status = EProcessStatus.Empty;
                TaskConv.Pre.Status = EProcessStatus.Empty;
                TaskConv.Pro.Status = EProcessStatus.Empty;
                TaskConv.Pos.Status = EProcessStatus.Empty;
                TaskConv.Out.Status = EProcessStatus.Empty;

                TaskConv.Pos2.Status = EProcessStatus.Empty;
                TaskConv.Out2.Status = EProcessStatus.Empty;

                NDispWin.DispProg.TR_Cancel();
            }
            catch (Exception Ex)
            {
                EMsg = EMsg + Ex.Message;
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, EMsg, EMsgBtn.smbOK);
            }

            Thread.Sleep(1);

            TaskConv.Status = EConvStatus.Ready;
            return true;

        _Error:
            try
            {
                Conv.Stop();
            }
            catch { };
            return false;
        }

        public static bool SensDoor
        {
            get
            {
                return ZEC3002.Ctrl.GetDI(ref ConvIO.SensDoor);
            }
        }
        public static bool SensDoorLock
        {
            get
            {
                return ZEC3002.Ctrl.GetDI(ref ConvIO.SensDoorLock);
            }
        }

        public static bool SensDoor2
        {
            get
            {
                return ZEC3002.Ctrl.GetDI(ref Conv2IO.SensDoor2);
            }
        }

        public static bool DoorCheck(bool Prompt)
        {
            if (!EnableDoorSens) return true;

            try
            {
                if (!TaskConv.BoardIsOpen) TaskConv.OpenBoard();
            }
            catch { }

            try
            {
                if (!SensDoor && DateTime.Now >= DefineSafety.dtEnable)
                {
                    if (Prompt)
                    {
                        Msg MsgBox = new Msg();
                        EMsgRes Res = MsgBox.Show(Messages.DOOR_IS_OPEN);
                    }
                    return false;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Conv DoorCheck " + Ex.Message.ToString());
            }
            return true;
        }
        public static bool DoorLock
        {
            set
            {
                try
                {
                    if (value)
                        ZEC3002.Ctrl.SetDO(ref ConvIO.DoorLock, ZEC3002.Ctrl.TDOStatus.Hi);
                    else
                        ZEC3002.Ctrl.SetDO(ref ConvIO.DoorLock, ZEC3002.Ctrl.TDOStatus.Lo);
                }
                catch { throw; }
            }
            get
            {
                return ConvIO.DoorLock.Status;
            }
        }

        public static bool LowPressure
        {
            get
            {
                return ZEC3002.Ctrl.GetDI(ref ConvIO.SensMainPressure);
            }
        }
        public static bool BtnStart
        {
            get
            {
                return ZEC3002.Ctrl.GetDI(ref ConvIO.Btn_Start);
            }
        }
        public static bool BtnStop
        {
            get
            {
                return ZEC3002.Ctrl.GetDI(ref ConvIO.Btn_Stop);
            }
        }
        public static bool BtnReset
        {
            get
            {
                return ZEC3002.Ctrl.GetDI(ref ConvIO.Btn_Reset);
            }
        }

        internal static bool VacPump
        {
            set
            {
                ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                if (value)
                    Status = ZEC3002.Ctrl.TDOStatus.Hi;
                ZEC3002.Ctrl.SetDO(ref ConvIO.VacPump, Status);
            }
            get
            {
                return ConvIO.VacPump.Status;
            }
        }

        public class Conv
        {
            private static uint ConvPulseCH = 2;
            private static uint ConvDirCH = 6;
            private static uint PWM_ON = 1000;
            private static uint PWM_OFF = 0;
            private static uint DutyCycle = 500;//x0.5%

            internal static bool MtrEnable
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus status = value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.Conv_MotorEn, status);
                }
                get
                {
                    return (ConvIO.Conv_MotorEn.Status);
                }
            }
            internal static void MtrOn()
            {
                if (Setup.ConvPara[(int)EConvPara.InvertMotorOn] == 0)
                    MtrEnable = true;
                else
                    MtrEnable = false;
            }
            internal static void MtrOff()
            {
                if (Setup.ConvPara[(int)EConvPara.InvertMotorOn] == 0)
                    MtrEnable = false;
                else
                    MtrEnable = true;
            }

            internal static bool ConvRun(int Dist, bool Fwd)
            {
                uint Dir = PWM_ON;
                if (!Fwd) Dir = PWM_OFF;

                if (Setup.ConvPara[(int)EConvPara.InvertMotorDir] == 1)
                {
                    if (Dir == PWM_ON)
                        Dir = PWM_OFF;
                    else
                        Dir = PWM_ON;
                }

                try
                {
                    double distpullyPerRound = 2 * 3.142 * 14;//2xpi(3.14)x pully diameter / 2
                    double pulsePerDistance = Setup.ConvPara[(int)EConvPara.MotorReso] / distpullyPerRound;
                    //Dist = Dist * 100;
                    uint hz = (uint)(Dist * pulsePerDistance);
                    MtrOn();
                    ZEC3002.Ctrl.SetPWMFreq(ConvIO.BoardID, ConvIO.DIOModel, (uint)hz);
                    ZEC3002.Ctrl.SetPWMDutyCycle(ConvIO.BoardID, ConvIO.DIOModel, ConvDirCH, Dir);
                    ZEC3002.Ctrl.SetPWMDutyCycle(ConvIO.BoardID, ConvIO.DIOModel, ConvPulseCH, DutyCycle);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.CONV_BELT_RUN_ERROR, Ex.Message, EMsgBtn.smbOK);
                    return false;
                }

                return true;
            }
            internal static void ConvSpeed(int Speed)
            {
                try
                {
                    Speed = Speed * 100;
                    ZEC3002.Ctrl.SetPWMFreq(ConvIO.BoardID, ConvIO.DIOModel, (uint)Speed);
                }
                catch { }
            }
            internal static bool Fwd_Init()
            {
                int Speed = Setup.ConvPara[(int)EConvPara.InitSpeed];
                ConvRun(Speed, true);
                return true;
            }
            internal static bool Fwd_Fast()
            {
                int Speed = Setup.ConvPara[(int)EConvPara.RunSpeed_Fast];
                ConvRun(Speed, true);
                return true;
            }
            internal static bool Fwd_Slow()
            {
                int Speed = Setup.ConvPara[(int)EConvPara.RunSpeed_Slow];
                ConvRun(Speed, true);

                return true;
            }
            internal static bool Fwd_SendOut()
            {
                int Speed = Setup.ConvPara[(int)EConvPara.RunSpeed_SendOut];
                ConvRun(Speed, true);

                return true;
            }
            internal static bool Rev_Fast()
            {
                int Speed = Setup.ConvPara[(int)EConvPara.RunSpeed_Fast];
                ConvRun(Speed, false);

                return true;
            }
            internal static bool Rev_Slow()
            {
                int Speed = Setup.ConvPara[(int)EConvPara.RunSpeed_Slow];
                ConvRun(Speed, false);

                return true;
            }
            internal static bool Rev_SendOut()
            {
                int Speed = Setup.ConvPara[(int)EConvPara.RunSpeed_SendOut];
                ConvRun(Speed, false);

                return true;
            }
            internal static bool Stop()
            {
                try
                {
                    ZEC3002.Ctrl.SetPWMDutyCycle(ConvIO.BoardID, ConvIO.DIOModel, ConvPulseCH, PWM_OFF);
                }
                catch
                {
                }

                return true;
            }
        }
        public class In
        {
            internal static int TimeOut
            {
                get
                {
                    return Setup.Pre[(int)EPara.TimeOut];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.In_SensPsnt);
                }
            }
            internal static bool SensLFPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.In_SensLFPsnt);
                }
            }
            internal static bool CheckEmpty()
            {
            _Retry:
                if (In.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "In", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            return false;
                    }
                }

            _Retry1:
                if (In.SensLFPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "In LF", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry1;
                        default:
                        case EMsgRes.smrStop:
                            return false;
                    }
                }

                return true;
            }

            internal static bool SvBlowSuck
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value) Status = ZEC3002.Ctrl.TDOStatus.Hi;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.In_SvBlowSuck, Status);
                }
                get
                {
                    return ConvIO.In_SvBlowSuck.Status;
                }
            }
            internal static void BlowSuckTimed()//execute BlowSuck for timer as Task
            {
                if (!EnableBlowSuck) return;

                try
                {
                    Task.Run(() => 
                    {
                        SvBlowSuck = true;
                        Thread.Sleep(Setup.ConvPara[(int)EConvPara.BlowSuckTime] * 1000);
                        SvBlowSuck = false;
                    });//no await
                }
                catch (Exception Ex)
                {
                }
                finally
                {
                }
            }
            //internal static void BlowSuckOn()
            //{
            //    if (!EnableBlowSuck) return;
            //    SvBlowSuck = true;
            //}
            //internal static void BlowSuckOff()
            //{
            //    if (!EnableBlowSuck) return;
            //    SvBlowSuck = false;
            //}

            internal static bool Smema_DI_BdReady
            {
                get
                {
                    if (Smema_Emulate_DI_BdReady) return true;

                        return ZEC3002.Ctrl.GetDI(ref ConvIO.UL_SmemaInBdReady);
                }
            }
            internal static bool Smema_Emulate_DI_BdReady = false;
            internal static bool Smema_DO_McReady
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus status = value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo;
                        ZEC3002.Ctrl.SetDO(ref ConvIO.UL_SmemaOutMcReady, status);
                }
                get
                {
                        return ConvIO.UL_SmemaOutMcReady.Status;
                }
            }

            internal static bool UL_WaitBdNotReady()
            {
            _Retry:
                int TOut = Environment.TickCount + Setup.Pre[(int)EPara.TimeOut];
                while (true)
                {
                    if (TaskConv.In.SensPsnt) break;
                    Thread.Sleep(5);
                    if (Environment.TickCount >= TOut)
                    {
                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SMEMA_LEFT_BOARD_IN_TIMEOUT, "", EMsgBtn.smbRetry_Stop);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _Retry;
                            case EMsgRes.smrStop:
                                {
                                    TaskConv.Status = EConvStatus.Stop;
                                    return false;
                                }
                            default: goto _Error;
                        }
                    }
                }
                return true;
            _Error:
                return false;
            }
        }
        public class Buf1
        {
            public static EBufStType _StType = EBufStType.None;
            public static EBufStType rt_StType = EBufStType.None;
            public static EBufStType StType
            {
                set
                {
                    _StType = value;
                    rt_StType = value;
                }
                get
                {
                    return _StType;
                }
            }
            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Buf1_SensPsnt);
                }
            }
            internal static bool CheckEmpty()
            {
            _Retry:
                if (Buf1.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_BUF_SENSOR_PSNT, "Buf1", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }

                return true;
            }
            internal static bool SensStopperUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Buf1_SensStopperUp);
                }
            }
            internal static bool SvStopperUp
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.Buf1_SvStopperUp, Status);
                }
                get
                {
                    return ConvIO.Buf1_SvStopperUp.Status;
                }
            }
            internal static bool StopperUp()
            {
            _Retry:
                SvStopperUp = true;
                Delay(Setup.Pre[(int)EPara.StopperUp_Delay]);

                if (Buf1.SensStopperUp) return true;

                SvStopperUp = false;
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "BUF1", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }
            internal static bool StopperDn()
            {
            _Retry:
                SvStopperUp = false;
                Delay(Setup.Pre[(int)EPara.StopperDn_Delay]);

                if (!Buf1.SensStopperUp) return true;

                SvStopperUp = false;
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_DN_TIMEOUT, "BUF1", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }
        }
        public class Buf2
        {
            public static EBufStType _StType = EBufStType.None;
            public static EBufStType rt_StType = EBufStType.None;
            public static EBufStType StType
            {
                set
                {
                    _StType = value;
                    rt_StType = value;
                }
                get
                {
                    return _StType;
                }
            }

            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Buf2_SensPsnt);
                }
            }
            internal static bool CheckEmpty()
            {
            _Retry:
                if (Buf2.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_BUF_SENSOR_PSNT, "Buf2", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }

                return true;
            }
            internal static bool SensStopperUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Buf2_SensStopperUp);
                }
            }
            internal static bool SvStopperUp
            {
                set
                {
                    //if (!UseStopper) return;

                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.Buf2_SvStopperUp, Status);
                }
                get
                {
                    return ConvIO.Buf2_SvStopperUp.Status;
                }
            }
            internal static bool StopperUp()
            {
            _Retry:
                SvStopperUp = true;
                Delay(Setup.Pre[(int)EPara.StopperUp_Delay]);

                if (Buf2.SensStopperUp) return true;

                SvStopperUp = false;
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "BUF2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }
            internal static bool StopperDn()
            {
            _Retry:
                SvStopperUp = false;
                Delay(Setup.Pre[(int)EPara.StopperDn_Delay]);

                if (!Buf2.SensStopperUp) return true;

                SvStopperUp = false;
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_DN_TIMEOUT, "BUF2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }
        }
        public class Pre
        {
            public static EPreStType _StType = EPreStType.None;
            public static EPreStType rt_StType = EPreStType.None;
            public static EPreStType StType
            {
                set
                {
                    _StType = value;
                    rt_StType = value;
                }
                get
                {
                    return _StType;
                }
            }

            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static int TimeOut
            {
                get
                {
                    return Setup.Pre[(int)EPara.TimeOut];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_SensPsnt);
                }
            }

            internal static bool UseStopper//not used
            {
                get
                {
                    return true;// (Setup.PRE[(int)EParam.StopperEnabled] > 0);
                }
            }
            internal static bool SensStopperUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_SensStopperUp);
                }
            }
            internal static bool SvStopperUp
            {
                set
                {
                    if (!UseStopper) return;

                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pre_SvStopperUp, Status);
                }
                get
                {
                    return ConvIO.Pre_SvStopperUp.Status;
                }
            }
            internal static bool StopperUp()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = true;
                Delay(Setup.Pre[(int)EPara.StopperUp_Delay]);

                if (Pre.SensStopperUp) return true;

                SvStopperUp = false;
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "PRE", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }
            internal static bool StopperDn()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = false;
                Delay(Setup.Pre[(int)EPara.StopperDn_Delay]);

                if (!Pre.SensStopperUp) return true;

                SvStopperUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "PRE", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }

            internal static bool UsePrecisor
            {
                get
                {
                    return (Setup.Pre[(int)EPara.Precisor_Type] > 0);
                }
            }
            internal static bool SensPrecisorExt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_SensPrecisorExt);
                }
            }
            internal static bool SvPrecisorExt
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pre_SvPrecisorExt, Status);
                }
                get
                {
                    return ConvIO.Pre_SvPrecisorExt.Status;
                }
            }
            internal static bool PrecisorExt()
            {
                if (!UsePrecisor) return true;

                _Retry:
                SvPrecisorExt = true;
                Delay(Setup.Pre[(int)EPara.PrecisorExt_Delay]);

                if (SensPrecisorExt) return true;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_PRECISOR_EXT_TIMEOUT, "PRE", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                SvPrecisorExt = false;
                return false;
            }
            internal static bool PrecisorRet()  
            {
                if (!UsePrecisor) return true;

                _Retry:
                SvPrecisorExt = false;
                Delay(Setup.Pre[(int)EPara.PrecisorRet_Delay]);

                if (!SensPrecisorExt) return true;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_PRECISOR_RET_TIMEOUT, "PRE", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                return false;
            }

            internal static bool Precise()
            {
                if (!UsePrecisor) return true;

                if (Setup.Pre[(int)EPara.Precisor_Type] == 1)
                {
                    if (!PrecisorExt()) return false;
                    Delay_LoadD();
                    Conv.Stop();
                    if (!LifterUp()) return false;
                    if (!PrecisorRet()) return false;
                }
                else
                if (Setup.Pre[(int)EPara.Precisor_Type] == 2)
                {
                    if (!PrecisorExt()) return false;
                    Delay_LoadD();
                    Conv.Stop();
                    if (!LifterUp()) return false;
                }

                return true;
            }

            internal static bool UseLifter
            {
                get
                {
                    return (Setup.Pre[(int)EPara.Lifter_Enable] > 0);
                }
            }
            internal static bool SensLifterUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_SensLifterUp);
                }
            }
            internal static bool SensLifterDn
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_SensLifterDn);
                }
            }
            internal static bool SvLifterUp
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pre_SvLifterUp, Status);
                }
                get
                {
                    return ConvIO.Pre_SvLifterUp.Status;
                }
            }
            internal static bool LifterUp()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = true;
                Delay(Setup.Pre[(int)EPara.LifterUp_Delay]);

                if (SensLifterUp) return true;

                #region
                SvLifterUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_UP_TIMEOUT, "PRE", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            #endregion
            _Error:
                return false;
            }
            internal static bool LifterDn()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = false;
                Delay(Setup.Pre[(int)EPara.LifterDn_Delay]);

                if (SensLifterDn) return true;

                #region
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_DN_TIMEOUT, "PRE", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            #endregion
            _Error:
                return false;
            }

            internal static bool UseHeater
            {
                get
                {
                    return (Setup.Pre[(int)EPara.HeatTime] > 0);
                }
            }
            internal static bool EnableHeaterAlarm
            {
                get
                {
                    return (Setup.Pre[(int)EPara.CheckAlarm] > 0);
                }
            }
            internal static bool HeaterInRange
            {
                get
                {
                    if (!UseHeater) return true;
                    if (!EnableHeaterAlarm) return true;

                    return !ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_HeaterAlarm);
                }
            }
            internal static bool HeaterAlarm
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_HeaterAlarm);
                }
            }
            public static int StartHeatTime;//volatile
            internal static bool HeatStart()//true if start heating
            {
                if (Setup.Pre[(int)EPara.HeatTime] > 0)
                {
                    Event.PREHEAT_START.Set();
                    Pre.StartHeatTime = Environment.TickCount;
                    return true;
                }
                return false;
            }
            internal static bool HeatEnd()//true if head ended
            {
                int TOut = Pre.StartHeatTime + (Setup.Pre[(int)EPara.HeatTime] * 1000);
                if (Environment.TickCount < TOut) return false;
                Event.PREHEAT_END.Set();

                switch (Pre.rt_StType)
                {
                    case EPreStType.None:
                    case EPreStType.Buffer:
                        Pre.Status = TaskConv.EProcessStatus.Psnt; break;
                    //case EPreStType.Disp:
                    //case EPreStType.Disp1:
                    //    if (SkipDisp) { Pre.Status = EProcessStatus.Psnt; break; }
                    //    Pre.Status = TaskConv.EProcessStatus.WaitDisp;
                    //    break;
                    //case EPreStType.Disp12:
                    //    if (SkipDisp) { Pre.Status = EProcessStatus.Psnt; break; }
                    //    Pre.Status = TaskConv.EProcessStatus.WaitDisp2;
                    //    break;
                }
                return true;
            }
            internal static int HeatRemain_s
            {
                get
                {
                    return (Pre.StartHeatTime + (Setup.Pre[(int)EPara.HeatTime] * 1000) - Environment.TickCount) / 1000;
                }
            }

            internal static bool UseVac
            {
                get
                {
                    return (Setup.Pre[(int)EPara.Vac1_Enable] > 0);
                }
            }
            internal static bool SensVac
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pre_VacSw);
                }
            }
            internal static bool SvVac
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pre_SvVac, Status);
                }
                get
                {
                    return ConvIO.Pre_SvVac.Status;
                }
            }
            internal static bool VacOn()
            {
                if (!UseVac) return true;

                int i_Retry = 0;
            _Retry:
                try
                {
                    Pre.SvVac = true;
                    Delay(Setup.Pre[(int)EPara.Vac1On_Delay]);
                    if (Pre.SensVac) return true;

                    if (i_Retry <= 1)
                    {
                        i_Retry++;

                        SvVac = false;
                        Delay(Setup.Pre[(int)EPara.Vac1Off_Delay]);
                        if (!LifterDn()) return false;
                        if (!LifterUp()) return false;
                        goto _Retry;
                    }
                    else
                    {
                        #region
                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_ON_TIMEOUT, "PRE", EMsgBtn.smbRetry_Stop);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry:
                                {
                                    SvVac = false;
                                    Delay(Setup.Pre[(int)EPara.Vac1Off_Delay]);
                                    if (!LifterDn()) return false;
                                    if (!LifterUp()) return false;
                                    goto _Retry;
                                }
                            case EMsgRes.smrStop:
                                {
                                    if (!LifterDn()) return false;
                                    return false;
                                }
                        }

                        #endregion
                    }
                }
                catch { }

                return true;
            }
            internal static bool VacOff()
            {
                if (!UseVac) return true;

                _Retry:
                try
                {
                    Pre.SvVac = false;
                    Delay(Setup.Pre[(int)EPara.Vac1Off_Delay]);
                    if (!Pre.SensVac) return true;

                    #region
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_OFF_TIMEOUT, "PRE", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry:
                            {
                                goto _Retry;
                            }
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }

                    #endregion
                }
                catch { }

                return true;
            }

            internal static bool CheckEmpty()
            {
            _Retry:
                if (Pre.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "PRE", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }

                return true;
            }

            internal static void Delay_LoadD()
            {
                Delay(Setup.Pre[(int)EPara.Load_Delay]);
            }
        }
        public class Pro
        {
            public static EProStType _StType = EProStType.None;
            public static EProStType rt_StType = EProStType.None;
            public static EProStType StType
            {
                set
                {
                    _StType = value;
                    rt_StType = value;
                }
                get
                {
                    return _StType;
                }
            }

            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static int TimeOut
            {
                get
                {
                    return Setup.Pro[(int)EPara.TimeOut];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_SensPsnt);
                }
            }

            internal static bool UseStopper//not used
            {
                get
                {
                    return true;// (Setup.PRO[(int)EParam.StopperEnabled] > 0);
                }
            }
            internal static bool SensStopperUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_SensStopperUp);
                }
            }
            internal static bool SvStopperUp
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pro_SvStopperUp, Status);
                }
                get
                {
                    return ConvIO.Pro_SvStopperUp.Status;
                }
            }
            internal static bool StopperUp()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = true;
                Delay(Setup.Pro[(int)EPara.StopperUp_Delay]);

                if (SensStopperUp) return true;

                SvStopperUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "PRO", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                return false;
            }
            internal static bool StopperDn()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = false;
                Delay(Setup.Pro[(int)EPara.StopperDn_Delay]);

                if (!Pro.SensStopperUp) return true;

                SvStopperUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_DN_TIMEOUT, "PRO", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                return false;
            }

            internal static bool UsePrecisor
            {
                get
                {
                    return (Setup.Pro[(int)EPara.Precisor_Type] > 0);
                }
            }
            internal static bool SensPrecisorExt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_SensPrecisorExt);
                }
            }
            internal static bool SvPrecisorExt
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pro_SvPrecisorExt, Status);
                }
                get
                {
                    return ConvIO.Pro_SvPrecisorExt.Status;
                }
            }
            internal static bool PrecisorExt()
            {
                if (!UsePrecisor) return true;

                _Retry:
                SvPrecisorExt = true;
                Delay(Setup.Pro[(int)EPara.PrecisorExt_Delay]);

                if (Pro.SensPrecisorExt) return true;

                #region
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_PRECISOR_EXT_TIMEOUT, "PRO", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                #endregion
                SvPrecisorExt = false;
                return false;
            }
            internal static bool PrecisorRet()
            {
                if (!UsePrecisor) return true;

                _Retry:
                SvPrecisorExt = false;
                Delay(Setup.Pro[(int)EPara.PrecisorRet_Delay]);

                if (!Pro.SensPrecisorExt) return true;

                #region
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_PRECISOR_RET_TIMEOUT, "PRO", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                #endregion
                return false;
            }

            internal static bool Precise()
            {
                if (!UsePrecisor) return true;

                if (Setup.Pro[(int)EPara.Precisor_Type] == 1)
                {
                    if (!PrecisorExt()) return false;
                    Delay_LoadD();
                    Conv.Stop();
                    if (!LifterUp()) return false;
                    if (!PrecisorRet()) return false;
                }
                else
                if (Setup.Pro[(int)EPara.Precisor_Type] == 2)
                {
                    if (!PrecisorExt()) return false;
                    Delay_LoadD();
                    Conv.Stop();
                    if (!LifterUp()) return false;
                }

                return true;
            }

            internal static bool UseLifter
            {
                get
                {
                    return (Setup.Pro[(int)EPara.Lifter_Enable] > 0);
                }
            }
            internal static bool SensLifterUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_SensLifterUp);
                }
            }
            internal static bool SensLifterDn
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_SensLifterDn);
                }
            }
            internal static bool SvLifterUp
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pro_SvLifterUp, Status);
                }
                get
                {
                    return ConvIO.Pro_SvLifterUp.Status;
                }
            }
            internal static bool LifterUp()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = true;
                Delay(Setup.Pro[(int)EPara.LifterUp_Delay]);

                if (SensLifterUp) return true;

                #region
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_UP_TIMEOUT, "PRO", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                SvLifterUp = false;
                return false;
                #endregion
            }
            internal static bool LifterDn()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = false;
                Delay(Setup.Pro[(int)EPara.LifterDn_Delay]);

                if (SensLifterDn) return true;

                #region
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_DN_TIMEOUT, "PRO", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                #endregion
                return false;
            }

            internal static bool UseHeater
            {
                get
                {
                    return (Setup.Pro[(int)EPara.HeatTime] > 0);
                }
            }
            internal static bool EnableHeaterAlarm
            {
                get
                {
                    return (Setup.Pro[(int)EPara.CheckAlarm] > 0);
                }
            }
            internal static bool HeaterInRange
            {
                get
                {
                    if (!UseHeater) return true;
                    if (!EnableHeaterAlarm) return true;

                    return !ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_HeaterAlarm);
                }
            }
            internal static bool HeaterAlarm
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_HeaterAlarm);
                }
            }
            public static int StartHeatTime;//volatile
            internal static bool HeatStart()//true if start heating
            {
                if (Setup.Pro[(int)EPara.HeatTime] > 0)
                {
                    Event.DISPHEAT_START.Set();
                    Pro.StartHeatTime = Environment.TickCount;
                    return true;
                }
                return false;
            }
            internal static bool HeatEnd()//true if head ended
            {
                int TOut = Pro.StartHeatTime + (Setup.Pro[(int)EPara.HeatTime] * 1000);
                if (Environment.TickCount < TOut) return false;
                Event.DISPHEAT_END.Set();

                switch (Pro.rt_StType)
                {
                    case EProStType.None:
                    case EProStType.Buffer:
                        Pro.Status = TaskConv.EProcessStatus.Psnt; break;
                    case EProStType.Disp:
                    //case EProStType.Disp2:
                        if (SkipDisp) { Pro.Status = EProcessStatus.Psnt; break; }
                        Pro.Status = TaskConv.EProcessStatus.WaitDisp; break;
                    //case EProStType.Disp12:
                    //    if (SkipDisp) { Pro.Status = EProcessStatus.Psnt; break; }
                    //    Pro.Status = TaskConv.EProcessStatus.WaitDisp2; break;
                }

                return true;
            }
            internal static int HeatRemain_s
            {
                get
                {
                    return (Pro.StartHeatTime + (Setup.Pro[(int)EPara.HeatTime] * 1000) - Environment.TickCount) / 1000;
                }
            }

            internal static bool UseVac
            {
                get
                {
                    return (Setup.Pro[(int)EPara.Vac1_Enable] > 0);
                }
            }
            internal static bool UseVac2
            {
                get
                {
                    return (Setup.Pro[(int)EPara.Vac2_Enable] > 0);
                }
            }

            internal static bool SensVac
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_VacSw);
                }
            }
            internal static bool SvVac
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pro_SvVac, Status);
                }
                get
                {
                    return ConvIO.Pro_SvVac.Status;
                }
            }

            internal static bool SensVac2
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pro_VacSw2);
                }
            }
            internal static bool SvVac2
            {
                set
                {
                    //ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    //if (value)
                    //    Status = ZEC3002.Ctrl.TDOStatus.Hi;
                    ZEC3002.Ctrl.TDOStatus Status = value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pro_SvVac2, Status);
                }
                get
                {
                    return ConvIO.Pro_SvVac2.Status;
                }
            }

            internal static bool VacOn()
            {
                if (!UseVac) return true;

                int i_Retry = 0;
            _Retry:
                try
                {
                    SvVac = true;
                    if (UseVac2) SvVac2 = false;

                    Delay(Setup.Pro[(int)EPara.Vac1On_Delay]);
                    if (!Pro.SensVac)
                    //if (false)
                    {
                        if (i_Retry++ > 0)
                        {
                            #region
                            Msg MsgBox = new Msg();
                            EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_ON_TIMEOUT, "PRO", EMsgBtn.smbRetry_Stop);
                            switch (MsgRes)
                            {
                                case EMsgRes.smrRetry:
                                    {
                                        SvVac = false;
                                        Delay(Setup.Pro[(int)EPara.Vac1Off_Delay]);
                                        if (!LifterDn()) return false;

                                        //Early vacuum sequence, on vac before up lifter
                                        if (Setup.Pro[(int)EPara.Vac1_Enable] == 2)
                                        {
                                            SvVac = true;
                                            Delay(Setup.Pro[(int)EPara.Vac1On_Delay]);
                                        }

                                        if (!LifterUp()) return false;
                                        goto _Retry;
                                    }
                                case EMsgRes.smrStop:
                                    {
                                        if (!LifterUp()) return false;
                                        return false;
                                    }
                            }
                            #endregion
                        }
                        else
                        { 

                            //i_Retry++;

                            SvVac = false;
                            Delay(Setup.Pro[(int)EPara.Vac1Off_Delay]);
                            if (!LifterDn()) return false;

                            //Early vacuum sequence, on vac before up lifter
                            if (Setup.Pro[(int)EPara.Vac1_Enable] == 2)
                            {
                                SvVac = true;
                                Delay(Setup.Pro[(int)EPara.Vac1On_Delay]);
                            }

                            if (!LifterUp()) return false;
                            goto _Retry;
                        }
                    }

                    if (UseVac2)
                    {
                        SvVac2 = true;
                        Delay(Setup.Pro[(int)EPara.Vac2On_Delay]);
                        if (!Pro.SensVac2)
                        {
                            if (i_Retry <= 1)
                            {
                                i_Retry++;

                                SvVac = false;
                                SvVac2 = false;
                                Delay(Setup.Pro[(int)EPara.Vac2Off_Delay]);
                                if (!LifterDn()) return false;
                                if (!LifterUp()) return false;
                                goto _Retry;
                            }
                            else
                            {
                                #region
                                Msg MsgBox = new Msg();
                                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM2_ON_TIMEOUT, "PRO", EMsgBtn.smbRetry_Stop);
                                switch (MsgRes)
                                {
                                    case EMsgRes.smrRetry:
                                        {
                                            SvVac = false;
                                            SvVac2 = false;
                                            Delay(Setup.Pro[(int)EPara.Vac2Off_Delay]);
                                            if (!LifterDn()) return false;
                                            if (!LifterUp()) return false;
                                            goto _Retry;
                                        }
                                    case EMsgRes.smrStop:
                                        {
                                            if (!LifterUp()) return false;
                                            return false;
                                        }
                                }
                                #endregion
                            }
                        }
                    }
                }
                catch { }

                return true;
            }
            internal static bool VacOff()
            {
                if (!UseVac) return true;

                _Retry:
                try
                {
                    Pro.SvVac = false;
                    if (UseVac2) Pro.SvVac2 = false;
                    Delay(Setup.Pro[(int)EPara.Vac1Off_Delay]);
                    if (Pro.SensVac)
                    {
                        #region
                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_OFF_TIMEOUT, "PRO", EMsgBtn.smbRetry_Stop);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry:
                                {
                                    goto _Retry;
                                }
                            case EMsgRes.smrStop:
                                {
                                    return false;
                                }
                        }
                        #endregion
                    }

                    if (UseVac2)
                    {
                        Pro.SvVac2 = false;
                        Delay(Setup.Pro[(int)EPara.Vac2Off_Delay]);
                        if (Pro.SensVac2)
                        {
                            #region
                            Msg MsgBox = new Msg();
                            EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM2_OFF_TIMEOUT, "PRO", EMsgBtn.smbRetry_Stop);
                            switch (MsgRes)
                            {
                                case EMsgRes.smrRetry:
                                    {
                                        goto _Retry;
                                    }
                                case EMsgRes.smrStop:
                                    {
                                        return false;
                                    }
                            }
                            #endregion
                        }
                    }
                }
                catch { }

                return true;
            }

            internal static bool CheckEmpty()
            {
            _Retry:
                if (Pro.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "PRO", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }

                return true;
            }

            internal static void Delay_LoadD()
            {
                Delay(Setup.Pro[(int)EPara.Load_Delay]);
            }
        }
        public class Pos
        {
            public static EPosStType _StType = EPosStType.None;
            public static EPosStType rt_StType = EPosStType.None;
            public static EPosStType StType
            {
                set
                {
                    _StType = value;
                    rt_StType = value;
                }
                get
                {
                    return _StType;
                }
            }

            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static int TimeOut
            {
                get
                {
                    return Setup.Pos[(int)EPara.TimeOut];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pos_SensPsnt);
                }
            }

            internal static bool UseStopper//not used
            {
                get
                {
                    return true;
                }
            }
            internal static bool SensStopperUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pos_SensStopperUp);
                }
            }
            internal static bool SvStopperUp
            {
                set
                {
                    if (!UseStopper) return;

                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pos_SvStopperUp, Status);
                }
                get
                {
                    return ConvIO.Pos_SvStopperUp.Status;
                }
            }
            internal static bool StopperUp()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = true;
                Delay(Setup.Pos[(int)EPara.StopperUp_Delay]);

                if (Pos.SensStopperUp) return true;

                SvStopperUp = false;
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "POS", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }
            internal static bool StopperDn()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = false;
                Delay(Setup.Pos[(int)EPara.StopperDn_Delay]);

                if (!Pos.SensStopperUp) return true;

                SvStopperUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "POS", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }

            internal static bool UseLifter
            {
                get
                {
                    return (Setup.Pos[(int)EPara.Lifter_Enable] > 0);
                }
            }
            internal static bool SensLifterUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pos_SensLifterUp);
                }
            }
            internal static bool SensLifterDn
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pos_SensLifterDn);
                }
            }
            internal static bool SvLifterUp
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pos_SvLifterUp, Status);
                }
                get
                {
                    return ConvIO.Pos_SvLifterUp.Status;
                }
            }
            internal static bool LifterUp()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = true;
                Delay(Setup.Pos[(int)EPara.LifterUp_Delay]);

                if (SensLifterUp) return true;

                #region
                SvLifterUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_UP_TIMEOUT, "POS", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            #endregion
            _Error:
                return false;
            }
            internal static bool LifterDn()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = false;
                Delay(Setup.Pos[(int)EPara.LifterDn_Delay]);

                if (SensLifterDn) return true;

                #region
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_DN_TIMEOUT, "POS", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            #endregion
            _Error:
                return false;
            }

            internal static bool UseHeater
            {
                get
                {
                    return (Setup.Pos[(int)EPara.HeatTime] > 0);
                }
            }

            public static int StartHeatTime;//volatile
            internal static bool HeatStart()//true if start heating
            {
                if (Setup.Pos[(int)EPara.HeatTime] > 0)
                {
                    Event.POSHEAT_START.Set();
                    Pos.StartHeatTime = Environment.TickCount;
                    return true;
                }
                return false;
            }
            internal static bool HeatEnd()//true if head ended
            {
                int TOut = Pos.StartHeatTime + (Setup.Pos[(int)EPara.HeatTime] * 1000);
                if (Environment.TickCount < TOut) return false;
                Event.POSHEAT_END.Set();

                switch (Pos.rt_StType)
                {
                    case EPosStType.None:
                    case EPosStType.Buffer:
                        Pos.Status = TaskConv.EProcessStatus.Psnt; break;
                }
                return true;
            }
            internal static int HeatRemain_s
            {
                get
                {
                    return (Pos.StartHeatTime + (Setup.Pos[(int)EPara.HeatTime] * 1000) - Environment.TickCount) / 1000;
                }
            }

            internal static bool UseVac
            {
                get
                {
                    return (Setup.Pos[(int)EPara.Vac1_Enable] > 0);
                }
            }
            internal static bool SensVac
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Pos_VacSw);
                }
            }
            internal static bool SvVac
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Pos_SvVac, Status);
                }
                get
                {
                    return ConvIO.Pos_SvVac.Status;
                }
            }
            internal static bool VacOn()
            {
                if (!UseVac) return true;

                int i_Retry = 0;
            _Retry:
                try
                {
                    Pos.SvVac = true;
                    Delay(Setup.Pos[(int)EPara.Vac1On_Delay]);
                    if (Pos.SensVac) return true;

                    if (i_Retry <= 1)
                    {
                        i_Retry++;

                        SvVac = false;
                        Delay(Setup.Pos[(int)EPara.Vac1Off_Delay]);
                        if (!LifterDn()) return false;
                        if (!LifterUp()) return false;
                        goto _Retry;
                    }
                    else
                    {
                        #region
                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_ON_TIMEOUT, "POS", EMsgBtn.smbRetry_Stop);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry:
                                {
                                    SvVac = false;
                                    Delay(Setup.Pos[(int)EPara.Vac1Off_Delay]);
                                    if (!LifterDn()) return false;
                                    if (!LifterUp()) return false;
                                    goto _Retry;
                                }
                            case EMsgRes.smrStop:
                                {
                                    if (!LifterDn()) return false;
                                    return false;
                                }
                        }

                        #endregion
                    }
                }
                catch { }

                return true;
            }
            internal static bool VacOff()
            {
                if (!UseVac) return true;

                _Retry:
                try
                {
                    Pos.SvVac = false;
                    Delay(Setup.Pos[(int)EPara.Vac1Off_Delay]);
                    if (!Pos.SensVac) return true;

                    #region
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_OFF_TIMEOUT, "POS", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry:
                            {
                                goto _Retry;
                            }
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }

                    #endregion
                }
                catch { }

                return true;
            }

            internal static bool CheckEmpty()
            {
            _Retry:
                if (Pos.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "POS", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }

                return true;
            }

            internal static void Delay_LoadD()
            {
                Delay(Setup.Pos[(int)EPara.Load_Delay]);
            }
        }
        public class Out
        {
            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static int TimeOut
            {
                get
                {
                    return Setup.Out[(int)EOutPara.TimeOut];
                }
            }
            internal static int Delay
            {
                get
                {
                    return Setup.Out[(int)EOutPara.Delay];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Out_SensPsnt);
                }
            }
            internal static bool SensLFPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Out_SensLFPsnt);
                }
            }

            internal static bool UseKicker
            {
                get
                {
                    return (Setup.Out[(int)EOutPara.KickerEnabled] > 0);
                }
            }
            internal static bool SensKickerExt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Out_SensKickerExt);
                }
            }
            internal static bool SensKickerRet
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.Out_SensKickerRet);
                }
            }
            internal static bool SvKickerExt
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref ConvIO.Out_SvKickerExt, Status);
                }
                get
                {
                    return ConvIO.Out_SvKickerExt.Status;
                }
            }
            internal static bool KickerExt()
            {
                if (!UseKicker) return true;

                _Retry:
                SvKickerExt = true;
                Delay(Setup.Out[(int)EOutPara.KickerExt_Delay]);

                if (SensKickerExt) return true;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_KICKER_EXT_TIMEOUT, "OUT", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                SvKickerExt = false;
                return false;
            }
            internal static bool KickerRet()
            {
                if (!UseKicker) return true;

                _Retry:
                SvKickerExt = false;
                Delay(Setup.Out[(int)EOutPara.KickerRet_Delay]);

                if (SensKickerRet) return true;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_KICKER_RET_TIMEOUT, "OUT", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                return false;
            }

            internal static bool CheckEmpty()//check station is empty
            {
                _Retry:
                if (Out.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "OUT", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            return false;
                    }
                }

                if (Out.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "OUT LF", EMsgBtn.smbStop);
                    return false;
                }
                return true;
            }
            internal static bool CheckPsnt()//check part is present
            {
            _Retry:
                if (!Out.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_MISSING, "OUT", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }
                return true;
            }

            internal static bool Smema_DO_BdReady
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus status = value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo;
                    ZEC3002.Ctrl.SetDO(ref ConvIO.DL_SmemaOutBdReady, status);
                }
                get
                {
                    return ConvIO.DL_SmemaOutBdReady.Status;
                }
            }
            internal static bool Smema_DI_McReady
            {
                get
                {
                    if (Smema_Emulate_DI_McReady) return true;
                    return ZEC3002.Ctrl.GetDI(ref ConvIO.DL_SmemaInMcReady);
                }
            }
            internal static bool Smema_Emulate_DI_McReady = false;

            internal static bool DL_WaitMcNotReady()
            {
            _Retry:
                int TOut = Environment.TickCount + Setup.Out[(int)EOutPara.TimeOut];
                while (true)
                {
                    if (!Smema_DI_McReady) break;
                    Thread.Sleep(5);
                    if (Environment.TickCount >= TOut)
                    {
                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SMEMA_RIGHT_IN_TIMEOUT, "", EMsgBtn.smbOK);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _Retry;
                            default: goto _Error;
                        }
                    }
                }

                return true;
            _Error:
                return false;
            }
        }

        public class Conv2//Conveyor for Post Station
        {
            private static uint ConvPulseCH = 2;
            private static uint ConvDirCH = 6;
            private static uint PWM_ON = 1000;
            private static uint PWM_OFF = 0;
            private static uint DutyCycle = 500;//x0.5%

            internal static bool MtrEnable
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus status = value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo;
                    ZEC3002.Ctrl.SetDO(ref Conv2IO.Conv_MotorEn, status);
                }
                get
                {
                    return (Conv2IO.Conv_MotorEn.Status);
                }
            }
            internal static void MtrOn()
            {
                if (Setup.Conv2Para[(int)EConvPara.InvertMotorOn] == 0)
                    MtrEnable = true;
                else
                    MtrEnable = false;
            }
            internal static void MtrOff()
            {
                if (Setup.Conv2Para[(int)EConvPara.InvertMotorOn] == 0)
                    MtrEnable = false;
                else
                    MtrEnable = true;
            }

            internal static bool ConvRun(int Dist, bool Fwd)
            {
                uint Dir = PWM_ON;
                if (!Fwd) Dir = PWM_OFF;

                if (Setup.Conv2Para[(int)EConvPara.InvertMotorDir] == 1)
                {
                    if (Dir == PWM_ON)
                        Dir = PWM_OFF;
                    else
                        Dir = PWM_ON;
                }

                try
                {
                    double distpullyPerRound = 2 * 3.142 * 14;//2xpi(3.14)x pully diameter / 2
                    double pulsePerDistance = Setup.Conv2Para[(int)EConvPara.MotorReso] / distpullyPerRound;
                    //Dist = Dist * 100;
                    uint hz = (uint)(Dist * pulsePerDistance);
                    MtrOn();
                    ZEC3002.Ctrl.SetPWMFreq(Conv2IO.BoardID, Conv2IO.DIOModel, (uint)hz);
                    ZEC3002.Ctrl.SetPWMDutyCycle(Conv2IO.BoardID, Conv2IO.DIOModel, ConvDirCH, Dir);
                    ZEC3002.Ctrl.SetPWMDutyCycle(Conv2IO.BoardID, Conv2IO.DIOModel, ConvPulseCH, DutyCycle);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.CONV_BELT_RUN_ERROR, Ex.Message, EMsgBtn.smbOK);
                    return false;
                }

                return true;
            }
            internal static void ConvSpeed(int Speed)
            {
                try
                {
                    Speed = Speed * 100;
                    ZEC3002.Ctrl.SetPWMFreq(Conv2IO.BoardID, Conv2IO.DIOModel, (uint)Speed);
                }
                catch { }
            }
            internal static bool Fwd_Init()
            {
                int Speed = Setup.Conv2Para[(int)EConvPara.InitSpeed];
                ConvRun(Speed, true);
                return true;
            }
            internal static bool Fwd_Fast()
            {
                int Speed = Setup.Conv2Para[(int)EConvPara.RunSpeed_Fast];
                ConvRun(Speed, true);
                return true;
            }
            internal static bool Fwd_Slow()
            {
                int Speed = Setup.Conv2Para[(int)EConvPara.RunSpeed_Slow];
                ConvRun(Speed, true);

                return true;
            }
            internal static bool Fwd_SendOut()
            {
                int Speed = Setup.Conv2Para[(int)EConvPara.RunSpeed_SendOut];
                ConvRun(Speed, true);

                return true;
            }
            internal static bool Rev_Fast()
            {
                int Speed = Setup.Conv2Para[(int)EConvPara.RunSpeed_Fast];
                ConvRun(Speed, false);

                return true;
            }
            internal static bool Rev_Slow()
            {
                int Speed = Setup.Conv2Para[(int)EConvPara.RunSpeed_Slow];
                ConvRun(Speed, false);

                return true;
            }
            internal static bool Stop()
            {
                try
                {
                    ZEC3002.Ctrl.SetPWMDutyCycle(Conv2IO.BoardID, Conv2IO.DIOModel, ConvPulseCH, PWM_OFF);
                }
                catch
                {
                }

                return true;
            }

            internal static bool Ready
            {
                get
                {
                    return (Out2.Status == EProcessStatus.Empty && Pos2.Status == EProcessStatus.Empty);
                }
            }
        }
        public class In2
        {
            internal static int TimeOut
            {
                get
                {
                    return Setup.Pos2[(int)EPara.TimeOut];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.In_SensPsnt);
                }
            }
            internal static bool CheckEmpty()
            {
            _Retry:
                if (In.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "In2", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            return false;
                    }
                }
                return true;
            }
        }
        public class Pos2
        {
            public static EPosStType _StType = EPosStType.None;
            public static EPosStType rt_StType = EPosStType.None;
            public static EPosStType StType
            {
                set
                {
                    _StType = value;
                    rt_StType = value;
                }
                get
                {
                    return _StType;
                }
            }

            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static int TimeOut
            {
                get
                {
                    return Setup.Pos2[(int)EPara.TimeOut];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Pos_SensPsnt);
                }
            }

            internal static bool UseStopper//not used
            {
                get
                {
                    return true;// (Setup.Pos[(int)EParam.StopperEnabled] > 0);
                }
            }
            internal static bool SensStopperUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Pos_SensStopperUp);
                }
            }
            internal static bool SvStopperUp
            {
                set
                {
                    if (!UseStopper) return;

                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;
                    ZEC3002.Ctrl.SetDO(ref Conv2IO.Pos_SvStopperUp, Status);
                }
                get
                {
                    return Conv2IO.Pos_SvStopperUp.Status;
                }
            }
            internal static bool StopperUp()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = true;
                Delay(Setup.Pos2[(int)EPara.StopperUp_Delay]);

                if (Pos2.SensStopperUp) return true;

                SvStopperUp = false;
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "Pos2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }
            internal static bool StopperDn()
            {
                if (!UseStopper) return true;

                _Retry:
                SvStopperUp = false;
                Delay(Setup.Pos2[(int)EPara.StopperDn_Delay]);

                if (!Pos2.SensStopperUp) return true;

                SvStopperUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_STOPPER_UP_TIMEOUT, "Pos2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            _Error:
                SvStopperUp = false;
                return false;
            }

            internal static bool UseLifter
            {
                get
                {
                    return (Setup.Pos2[(int)EPara.Lifter_Enable] > 0);
                }
            }
            internal static bool SensLifterUp
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Pos_SensLifterUp);
                }
            }
            internal static bool SensLifterDn
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Pos_SensLifterDn);
                }
            }
            internal static bool SvLifterUp
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref Conv2IO.Pos_SvLifterUp, Status);
                }
                get
                {
                    return Conv2IO.Pos_SvLifterUp.Status;
                }
            }
            internal static bool LifterUp()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = true;
                Delay(Setup.Pos2[(int)EPara.LifterUp_Delay]);

                if (SensLifterUp) return true;

                #region
                SvLifterUp = false;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_UP_TIMEOUT, "Pos2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            #endregion
            _Error:
                return false;
            }
            internal static bool LifterDn()
            {
                if (!UseLifter) return true;

                _Retry:
                SvLifterUp = false;
                Delay(Setup.Pos2[(int)EPara.LifterDn_Delay]);

                if (SensLifterDn) return true;

                #region
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_LIFTER_DN_TIMEOUT, "Pos2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                    default: goto _Error;
                }
            #endregion
            _Error:
                return false;
            }

            internal static bool UseHeater
            {
                get
                {
                    return (Setup.Pos2[(int)EPara.HeatTime] > 0);
                }
            }
            internal static bool EnableHeaterAlarm
            {
                get
                {
                    return (Setup.Pos2[(int)EPara.CheckAlarm] > 0);
                }
            }
            internal static bool HeaterInRange
            {
                get
                {
                    if (!UseHeater) return true;
                    if (!EnableHeaterAlarm) return true;

                    return !ZEC3002.Ctrl.GetDI(ref Conv2IO.Pos_HeaterAlarm);
                }
            }
            public static int StartHeatTime;//volatile
            internal static bool HeatStart()//true if start heating
            {
                if (Setup.Pos2[(int)EPara.HeatTime] > 0)
                {
                    Pos2.StartHeatTime = Environment.TickCount;
                    //Pos.Status = TaskConv.EStStatus.Heating;
                    return true;
                }
                return false;
            }
            internal static bool HeatEnd()//true if head ended
            {
                int TOut = Pos2.StartHeatTime + (Setup.Pos2[(int)EPara.HeatTime] * 1000);
                if (Environment.TickCount < TOut) return false;

                switch (Pos2.rt_StType)
                {
                    case EPosStType.None:
                    case EPosStType.Buffer:
                        Pos2.Status = TaskConv.EProcessStatus.Psnt; break;
                }
                return true;
            }
            internal static int HeatRemain_s
            {
                get
                {
                    return (Pos2.StartHeatTime + (Setup.Pos2[(int)EPara.HeatTime] * 1000) - Environment.TickCount) / 1000;
                }
            }

            internal static bool UseVac
            {
                get
                {
                    return (Setup.Pos2[(int)EPara.Vac1_Enable] > 0);
                }
            }
            internal static bool SensVac
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Pos_VacSw);
                }
            }
            internal static bool SvVac
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref Conv2IO.Pos_SvVac, Status);
                }
                get
                {
                    return Conv2IO.Pos_SvVac.Status;
                }
            }
            internal static bool VacOn()
            {
                if (!UseVac) return true;

                _Retry:
                int i_Retry = 0;
                try
                {
                    Pos2.SvVac = true;
                    Delay(Setup.Pos2[(int)EPara.Vac1On_Delay]);
                    if (Pos2.SensVac) return true;

                    if (i_Retry < 1)
                    {
                        i_Retry++;

                        SvVac = false;
                        Delay(Setup.Pos2[(int)EPara.Vac1Off_Delay]);
                        if (!LifterDn()) return false;
                        if (!LifterUp()) return false;
                        goto _Retry;
                    }
                    else
                    {
                        #region
                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_ON_TIMEOUT, "Pos2", EMsgBtn.smbRetry_Stop);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry:
                                {
                                    SvVac = false;
                                    Delay(Setup.Pos2[(int)EPara.Vac1Off_Delay]);
                                    if (!LifterDn()) return false;
                                    if (!LifterUp()) return false;
                                    goto _Retry;
                                }
                            case EMsgRes.smrStop:
                                {
                                    if (!LifterDn()) return false;
                                    return false;
                                }
                        }

                        #endregion
                    }
                }
                catch { }

                return true;
            }
            internal static bool VacOff()
            {
                if (!UseVac) return true;

                _Retry:
                try
                {
                    Pos2.SvVac = false;
                    Delay(Setup.Pos2[(int)EPara.Vac1Off_Delay]);
                    if (!Pos2.SensVac) return true;

                    #region
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_VACUUM_OFF_TIMEOUT, "Pos2", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry:
                            {
                                goto _Retry;
                            }
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }

                    #endregion
                }
                catch { }

                return true;
            }

            internal static bool CheckEmpty()
            {
            _Retry:
                if (Pos2.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "Pos2", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }

                return true;
            }

            internal static void Delay_LoadD()
            {
                Delay(Setup.Pos2[(int)EPara.Load_Delay]);
            }
        }
        public class Out2
        {
            public static EProcessStatus Status = EProcessStatus.Empty;
            public static Color StatusColor
            {
                get
                {
                    return ProcessStatusColor[(int)Status];
                }
            }

            internal static int TimeOut
            {
                get
                {
                    return Setup.Out2[(int)EOutPara.TimeOut];
                }
            }
            internal static int Delay
            {
                get
                {
                    return Setup.Out2[(int)EOutPara.Delay];
                }
            }
            internal static bool SensPsnt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Out_SensPsnt);
                }
            }

            internal static bool UseKicker
            {
                get
                {
                    return (Setup.Out2[(int)EOutPara.KickerEnabled] > 0);
                }
            }
            internal static bool SensKickerExt
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Out_SensKickerExt);
                }
            }
            internal static bool SensKickerRet
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref Conv2IO.Out_SensKickerRet);
                }
            }
            internal static bool SvKickerExt
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus Status = ZEC3002.Ctrl.TDOStatus.Lo;
                    if (value)
                        Status = ZEC3002.Ctrl.TDOStatus.Hi;

                    ZEC3002.Ctrl.SetDO(ref Conv2IO.Out_SvKickerExt, Status);
                }
                get
                {
                    return Conv2IO.Out_SvKickerExt.Status;
                }
            }
            internal static bool KickerExt()
            {
                if (!UseKicker) return true;

                _Retry:
                SvKickerExt = true;
                Delay(Setup.Out2[(int)EOutPara.KickerExt_Delay]);

                if (SensKickerExt) return true;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_KICKER_EXT_TIMEOUT, "Out2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                SvKickerExt = false;
                return false;
            }
            internal static bool KickerRet()
            {
                if (!UseKicker) return true;

                _Retry:
                SvKickerExt = false;
                Delay(Setup.Out2[(int)EOutPara.KickerRet_Delay]);

                if (SensKickerRet) return true;

                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_KICKER_RET_TIMEOUT, "Out2", EMsgBtn.smbOK_Retry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _Retry;
                }
                return false;
            }

            internal static bool CheckEmpty()//check station is empty
            {
            _Retry:
                if (Out2.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "Out2", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }
                return true;
            }
            internal static bool CheckPsnt()//check part is present
            {
            _Retry:
                if (!Out2.SensPsnt)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.CONV_SENSOR_PART_MISSING, "Out2", EMsgBtn.smbRetry_Stop_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default:
                        case EMsgRes.smrStop:
                            {
                                return false;
                            }
                    }
                }
                return true;
            }
        }

        public class TowerLight
        {
            private static uint PWM_ON = 1000;//X0.1%
            private static uint PWM_OFF = 0;//X0.1%

            public static bool TL_Red
            {
                set
                {
                    if (ConvIO.TL_Control == 1)
                    {
                        uint dutyCycle = value ? PWM_ON : PWM_OFF;
                        ZEC3002.Ctrl.SetPWMDutyCycle(ref ConvIO.PWM_TL_Red, dutyCycle);
                    }
                    else
                    {
                        ZEC3002.Ctrl.SetDO(ref ConvIO.TL_Red, value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo);
                    }
                }
                get
                {
                    if (ConvIO.TL_Control == 1)
                        return ConvIO.PWM_TL_Red.DutyCycle == PWM_ON;
                    else
                        return ConvIO.TL_Red.Status;
                }
            }
            public static bool TL_Yellow
            {
                set
                {
                    if (ConvIO.TL_Control == 1)
                    {
                        uint dutyCycle = value ? PWM_ON : PWM_OFF;
                        ZEC3002.Ctrl.SetPWMDutyCycle(ref ConvIO.PWM_TL_Yellow, dutyCycle);
                    }
                    else
                    {
                        ZEC3002.Ctrl.SetDO(ref ConvIO.TL_Yellow, value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo);
                    }
                }
                get
                {
                    if (ConvIO.TL_Control == 1)
                        return ConvIO.PWM_TL_Yellow.DutyCycle == PWM_ON;
                    else
                        return ConvIO.TL_Yellow.Status;
                }
            }
            public static bool TL_Green
            {
                set
                {
                    if (ConvIO.TL_Control == 1)
                    {
                        uint dutyCycle = value ? PWM_ON : PWM_OFF;
                        ZEC3002.Ctrl.SetPWMDutyCycle(ref ConvIO.PWM_TL_Green, dutyCycle);
                    }
                    else
                    {
                        ZEC3002.Ctrl.SetDO(ref ConvIO.TL_Green, value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo);
                    }
                }
                get
                {
                    if (ConvIO.TL_Control == 1)
                        return ConvIO.PWM_TL_Green.DutyCycle == PWM_ON;
                    else
                        return ConvIO.TL_Green.Status;
                }
            }
            public static bool TL_Buzzer
            {
                set
                {
                    if (ConvIO.TL_Control == 1)
                    {
                        uint dutyCycle = value ? PWM_ON : PWM_OFF;
                        ZEC3002.Ctrl.SetPWMDutyCycle(ref ConvIO.PWM_TL_Buzzer, dutyCycle);
                    }
                    else
                    {
                        ZEC3002.Ctrl.SetDO(ref ConvIO.TL_Buzzer, value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo);
                    }
                }
                get
                {
                    if (ConvIO.TL_Control == 1)
                        return ConvIO.PWM_TL_Buzzer.DutyCycle == PWM_ON;
                    else
                        return ConvIO.TL_Buzzer.Status;
                }
            }
        }

        internal static bool CheckHeaterInRange()
        {
            if (!Pre.HeaterInRange)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_HEATER_OUT_OF_RANGE, "PRE", EMsgBtn.smbOK);
                return false;
            }

            if (!Pro.HeaterInRange)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_HEATER_OUT_OF_RANGE, "PRO", EMsgBtn.smbOK);
                return false;
            }

            return true;
        }

        internal static bool PushIn()//push frame into track
        {
            string EMsg = "PushIn";

            try
            {
                #region
                if (!TaskConv.Ready) return true;

                if (!TaskElev.Left.ReadyToSend)
                {
                    if (TaskElev.Left.TaskRunKick == null || TaskElev.Left.TaskRunKick.Status != TaskStatus.Running)
                    TaskElev.Left.RunLevel();
                    Event.DEBUG_INFO.Set("TaskElev.Left.RunLevel", "");
                }

                bool hold = false;
                if (TaskElev.Left.ReadyToSend)
                {
                    if (TaskConv.OutLevelQtyFollowIn && TaskElev.Left.b_MagChanged) return true;

                    if (TaskElev.Left.TaskRunKick == null || TaskElev.Left.TaskRunKick.Status != TaskStatus.Running)
                    {
                        TaskElev.Left.TaskRunKick = Task.Run(() => { TaskElev.Left.RunKick(ref hold); });

                        while (true)
                        {
                            if (TaskElev.Left.TaskRunKick != null && TaskElev.Left.TaskRunKick.Status == TaskStatus.Running) break;
                            Thread.Sleep(10);
                        }; 

                        while (true)
                        {
                            if (!hold && TaskConv.In.SensPsnt) break;
                            if (TaskElev.Left.TaskRunKick.Status != TaskStatus.Running) break;
                            Thread.Sleep(10);
                        }
                    }
                }
                return true;
                #endregion
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveToBuf1()//move frame to Buf1, update Buf1 Status, return false if error
        {
            string EMsg = "MoveToBuf1";

            try
            {
            #region
            _RetryLoad:
                if (!Buf1.StopperUp()) goto _Error;
                Conv.Fwd_Fast();

                int TOut = Environment.TickCount + Pre.TimeOut;
                while (true)
                {
                    if (InMcReadyFollowSensInPsnt && !In.SensPsnt && In.Smema_DO_McReady)
                    {
                        In.Smema_DO_McReady = false;
                    }
                    if (Buf1.SensPsnt)
                    {
                        In.Smema_DO_McReady = false;
                        Conv.Fwd_Slow();
                        Pre.Delay_LoadD();
                        break;
                    }
                    if (Environment.TickCount >= TOut)
                    {
                        #region
                        In.Smema_DO_McReady = false;
                        Conv.Stop();

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_MOVE_TIMEOUT, "BUF1", EMsgBtn.smbRetry_Stop_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _RetryLoad;
                            case EMsgRes.smrStop:
                                {
                                    Status = EConvStatus.Stop; ;
                                    return true;
                                }
                            default: goto _Error;
                        }
                        #endregion
                    }
                }

                Event.BOARD_ARRIVED_BUFFER1.Set();
                Conv.Stop();
                Buf1.Status = TaskConv.EProcessStatus.Psnt;
                return true;
            _Error:
                In.Smema_DO_McReady = false;
                Conv.Stop();
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
                #endregion
            }
            catch (Exception Ex)
            {
                In.Smema_DO_McReady = false;
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveToBuf2()//move frame to Buf2, update Buf2 Status, return false if error
        {
            string EMsg = "MoveToBuf2";

            try
            {
            #region
            _RetryLoad:
                if (!Buf2.StopperUp()) goto _Error;
                Conv.Fwd_Fast();

                int TOut = Environment.TickCount + Pre.TimeOut;
                while (true)
                {
                    if (InMcReadyFollowSensInPsnt && !In.SensPsnt && In.Smema_DO_McReady)
                    {
                        In.Smema_DO_McReady = false;
                    }
                    if (Buf2.SensPsnt)
                    {
                        In.Smema_DO_McReady = false;
                        Conv.Fwd_Slow();
                        Pre.Delay_LoadD();
                        break;
                    }
                    if (Environment.TickCount >= TOut)
                    {
                        #region
                        In.Smema_DO_McReady = false;
                        Conv.Stop();

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_MOVE_TIMEOUT, "BUF2", EMsgBtn.smbRetry_Stop_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _RetryLoad;
                            case EMsgRes.smrStop:
                                {
                                    Status = EConvStatus.Stop;
                                    return true;
                                }
                            default: goto _Error;
                        }
                        #endregion
                    }
                }

                Event.BOARD_ARRIVED_BUFFER2.Set();
                Conv.Stop();
                Buf2.Status = TaskConv.EProcessStatus.Psnt;
                return true;
            _Error:
                In.Smema_DO_McReady = false;
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
                #endregion
            }
            catch (Exception Ex)
            {
                In.Smema_DO_McReady = false;
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveToPre()//move frame to Pre, update Pre Status, return false if error
        {
            string EMsg = "MoveToPre";

            try
            {
            #region
            _RetryLoad:
                if (!Pre.StopperUp()) goto _Error;
                Conv.Fwd_Fast();

                int TOut = Environment.TickCount + Pre.TimeOut;
                while (true)
                {
                    if (InMcReadyFollowSensInPsnt && !In.SensPsnt && In.Smema_DO_McReady)
                    {
                        In.Smema_DO_McReady = false;
                    }
                    if (Pre.SensPsnt)
                    {
                        In.Smema_DO_McReady = false;

                        Conv.Fwd_Slow();
                        Pre.Delay_LoadD();
                        break;
                    }
                    if (Environment.TickCount >= TOut)
                    {
                        #region
                        In.Smema_DO_McReady = false;
                        Conv.Stop();

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_MOVE_TIMEOUT, "PRE", EMsgBtn.smbRetry_Stop_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _RetryLoad;
                            case EMsgRes.smrStop:
                                {
                                    Status = EConvStatus.Stop; ;
                                    return true;
                                }
                            default: goto _Error;
                        }
                        #endregion
                    }
                }

                if (!Pre.Precise()) goto _Error;

                Event.BOARD_ARRIVED_PRE_STATION.Set();
                Conv.Stop();

                if (!Pre.LifterUp()) goto _Error;

                if (!Pre.VacOn()) goto _Error;

                if (Pre.HeatStart())

                    Pre.Status = TaskConv.EProcessStatus.Heating;
                else
                    switch (Pre.rt_StType)
                    {
                        case EPreStType.Buffer:
                            Pre.Status = TaskConv.EProcessStatus.Psnt; break;
                        //case EPreStType.Disp:
                        //case EPreStType.Disp1:
                        //    if (SkipDisp) { Pre.Status = EProcessStatus.Psnt; break; }
                        //    Pre.Status = TaskConv.EProcessStatus.WaitDisp;
                        //    break;
                        //case EPreStType.Disp12:
                        //    if (SkipDisp) { Pre.Status = EProcessStatus.Psnt; break; }
                        //    Pre.Status = TaskConv.EProcessStatus.WaitDisp2;
                        //    break;
                    }

                //if (Pre.rt_StType == EPreStType.Disp1) Pro.Status = TaskConv.EProcessStatus.WaitNone;
                return true;
            #endregion
            _Error:
                Pre.SvPrecisorExt = false;
                In.Smema_DO_McReady = false;
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            }
            catch (Exception Ex)
            {
                In.Smema_DO_McReady = false;
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveToPro()//move frame to Pro, update Pro Status, return false if error
        {
            string EMsg = "MoveToPro";

            try
            {
            #region
            _RetryLoad:
                if (!Pro.StopperUp()) goto _Error;
                Conv.Fwd_Fast();

                int TOut = Environment.TickCount + Pro.TimeOut;
                while (true)
                {
                    if (InMcReadyFollowSensInPsnt && !In.SensPsnt && In.Smema_DO_McReady)
                    {
                        In.Smema_DO_McReady = false;
                    }
                    if (Pro.SensPsnt)
                    {
                        In.Smema_DO_McReady = false;
                        Conv.Fwd_Slow();
                        Pro.Delay_LoadD();
                        break;
                    }
                    if (Environment.TickCount >= TOut)
                    {
                        #region
                        In.Smema_DO_McReady = false;
                        Conv.Stop();

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_MOVE_TIMEOUT, "PRO", EMsgBtn.smbRetry_Stop_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _RetryLoad;
                            case EMsgRes.smrStop:
                                {
                                    Status = EConvStatus.Stop; ;
                                    return true;
                                }
                            default: goto _Error;
                        }
                        #endregion
                    }
                }
                if (!Pro.Precise()) goto _Error;

                Event.BOARD_ARRIVED_DISP_STATION.Set();
                Conv.Stop();

                //Early vacuum sequence, on vac before up lifter
                if (Setup.Pro[(int)EPara.Vac1_Enable] == 2)
                {
                    Pro.SvVac = true;
                    Delay(Setup.Pro[(int)EPara.Vac1On_Delay]);
                }


                if (!Pro.LifterUp()) goto _Error;

                if (!Pro.VacOn()) goto _Error;

                if (Setup.Pro[(int)EPara.Stopper_Enable] == 2)
                {
                    if (!Pro.StopperDn()) goto _Error;
                }

                if (Pro.HeatStart())
                    Pro.Status = TaskConv.EProcessStatus.Heating;
                else
                    switch (Pro.rt_StType)
                    {
                        case EProStType.Buffer:
                            Pro.Status = TaskConv.EProcessStatus.Psnt; break;
                        case EProStType.Disp:
                        //case EProStType.Disp2:
                            if (SkipDisp) { Pro.Status = EProcessStatus.Psnt; break; }
                            Pro.Status = TaskConv.EProcessStatus.WaitDisp;
                            break;
                        //case EProStType.Disp12:
                        //    if (SkipDisp) { Pro.Status = EProcessStatus.Psnt; break; }
                        //    Pro.Status = TaskConv.EProcessStatus.WaitDisp2;
                        //    break;
                    }
                //if (Pro.rt_StType == EProStType.Disp2) Pre.Status = TaskConv.EProcessStatus.WaitNone;
                return true;
            #endregion
            _Error:
                Pro.SvPrecisorExt = false;
                Conv.Stop(); ;
                In.Smema_DO_McReady = false;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                In.Smema_DO_McReady = false;
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveToPos()//move frame to Pos, update Pos Status, return false if error
        {
            string EMsg = "MoveToPos";

            try
            {
            #region
            _RetryLoad:
                if (!Pos.StopperUp()) goto _Error;
                Conv.Fwd_Fast();

                int TOut = Environment.TickCount + Pos.TimeOut;
                while (true)
                {
                    if (InMcReadyFollowSensInPsnt && !In.SensPsnt && In.Smema_DO_McReady)
                    {
                        In.Smema_DO_McReady = false;
                    }
                    if (Pos.SensPsnt)
                    {
                        In.Smema_DO_McReady = false;
                        Conv.Fwd_Slow();
                        Pos.Delay_LoadD();
                        break;
                    }
                    if (Environment.TickCount >= TOut)
                    {
                        #region
                        In.Smema_DO_McReady = false;
                        Conv.Stop();

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_MOVE_TIMEOUT, "POS", EMsgBtn.smbRetry_Stop_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _RetryLoad;
                            case EMsgRes.smrStop:
                                {
                                    Status = EConvStatus.Stop; ;
                                    return true;
                                }
                            default: goto _Error;
                        }
                        #endregion
                    }
                }

                Event.BOARD_ARRIVED_POST_STATION.Set();
                Conv.Stop();

                //Early vacuum sequence, on vac before up lifter
                if (Setup.Pos[(int)EPara.Vac1_Enable] == 2)
                {
                    Pos.SvVac = true;
                    Delay(Setup.Pos[(int)EPara.Vac1On_Delay]);
                }


                if (!Pos.LifterUp()) goto _Error;

                if (!Pos.VacOn()) goto _Error;

                if (Setup.Pos[(int)EPara.Stopper_Enable] == 2)
                {
                    if (!Pos.StopperDn()) goto _Error;
                }

                if (Pos.HeatStart())
                    Pos.Status = TaskConv.EProcessStatus.Heating;
                else
                    Pos.Status = TaskConv.EProcessStatus.Psnt;
                return true;
            #endregion
            _Error:
                Conv.Stop(); ;
                In.Smema_DO_McReady = false;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                In.Smema_DO_McReady = false;
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool MoveRevToIn()//move frame to In, update In Status, return false if error
        {
            string EMsg = "MoveToIn";

            try
            {
            _Retry:
                Conv.Rev_Fast();

                #region Wait In.SensPsnt
                int TOut = Environment.TickCount + Pre.TimeOut;
                while (true)
                {
                    if (In.SensPsnt)
                    {
                        Event.BOARD_REVERSE_ARRIVED_IN_STATION.Set();
                        Conv.Stop();
                        break;
                    }
                    if (Environment.TickCount >= TOut)
                    {
                        Conv.Stop();
                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_MOVE_TIMEOUT, "IN", EMsgBtn.smbOK_Retry_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _Retry;
                            default: goto _Error;
                        }
                    }
                }
                #endregion

                Conv.Stop();

                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveToOut()//move frame to Out, update Out Status, return false if error
        {
            string EMsg = "MoveToOut";

            try
            {
            _Retry:
                if (!Conv.Fwd_Fast()) goto _Error;

                #region Wait Out.SensPsnt
                int TOut = Environment.TickCount + Out.TimeOut;
                while (true)
                {
                    if (Out.SensPsnt)
                    {
                        Event.BOARD_ARRIVED_OUT_STATION.Set();
                        if (!Conv.Stop()) goto _Error;
                        break;
                    }
                    Thread.Sleep(5);
                    if (Environment.TickCount >= TOut)
                    {
                        if (!Conv.Stop()) goto _Error;

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_MOVE_TIMEOUT, "OUT", EMsgBtn.smbRetry_Stop);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry: goto _Retry;
                            case EMsgRes.smrStop:
                                {
                                    Status = EConvStatus.Stop; ;
                                    return true;
                                }
                            default: goto _Error;
                        }
                    }
                }
                #endregion

                Out.Status = TaskConv.EProcessStatus.Psnt;

                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        //internal static bool RevUnload()//reverse unload
        //{
        //    string EMsg = "ReverseUnload";
        //    try
        //    {
        //        Status = EConvStatus.Busy;

        //        Event.BOARD_REVERSE_SEND_OUT_SMEMA.Set();
        //    _Retry:
        //        #region Start SendOut
        //        Conv.Rev_SendOut();
        //        int TOut = Environment.TickCount + Out.TimeOut;

        //        while (true)
        //        {
        //            if (!In.SensPsnt)
        //            {
        //                int TOutDelay = Environment.TickCount + Out.Delay;
        //                while (true)
        //                {
        //                    if (In.SensPsnt) break;
        //                    if (Environment.TickCount >= TOutDelay) break;
        //                }
        //                if (Environment.TickCount >= TOutDelay) break;
        //            }

        //            if (Environment.TickCount >= TOut)
        //            {
        //                Conv.Stop();
        //                if (LeftMode == ELeftMode.Smema) In.Smema2_DO_BdReady = false;

        //                Msg MsgBox = new Msg();
        //                EMsgRes MsgRes = MsgBox.Show(Messages.CONV_REVERSE_UNLOAD_TIMEOUT, "", EMsgBtn.smbRetry_Stop);
        //                switch (MsgRes)
        //                {
        //                    case EMsgRes.smrRetry:
        //                        goto _Retry;
        //                    case EMsgRes.smrStop:
        //                    default:
        //                        goto _Stop;
        //                    //case EMsgRes.smrCancel:
        //                    //    Out.Status = EProcessStatus.Empty;
        //                    //    goto _Stop;
        //                }
        //            }
        //        }

        //        Conv.Stop();
        //        #endregion
        //        Status = EConvStatus.Ready;
        //        return true;
        //    _Stop:
        //        Conv.Stop();
        //        Status = EConvStatus.Stop;
        //        return true;
        //    }
        //    catch (Exception Ex)
        //    {
        //        Conv.Stop();
        //        throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
        //    }
        //}

        internal static bool MoveInToBuf1()
        {
            string EMsg = "LR_MoveInToBuf1";
            try
            {
                if (!Buf1.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;
                if (!MoveToBuf1()) goto _Error;

                if (Status == TaskConv.EConvStatus.Stop) return true;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveInToBuf2()
        {
            string EMsg = "LR_MoveInToBuf2";
            try
            {
                if (!Buf2.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                if (!MoveToBuf2()) goto _Error;
                Buf2.Status = TaskConv.EProcessStatus.Psnt;

                if (Status == TaskConv.EConvStatus.Stop) return true;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveInToPre()
        {
            string EMsg = "LR_MoveInToPre";
            try
            {
                if (!Pre.CheckEmpty()) goto _Stop;

                if (Buf1.rt_StType == EBufStType.Buffer) Buf1.SvStopperUp = false;
                if (Buf2.rt_StType == EBufStType.Buffer) Buf2.SvStopperUp = false;

                Status = EConvStatus.Busy;

                if (!MoveToPre()) goto _Error;

                if (Status == TaskConv.EConvStatus.Stop) return true;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveInToPro()
        {
            string EMsg = "LR_MoveInToPro";
            try
            {
                Out.SvKickerExt = false;

                if (!Pro.CheckEmpty()) goto _Stop;

                if (Buf1.rt_StType == EBufStType.Buffer) Buf1.SvStopperUp = false;
                if (Buf2.rt_StType == EBufStType.Buffer) Buf2.SvStopperUp = false;

                if (!Pre.StopperDn()) goto _Error;

                Status = EConvStatus.Busy;

                if (!MoveToPro()) goto _Error;

                if (Status == TaskConv.EConvStatus.Stop) return true;
                Status = EConvStatus.Ready;

                return true;

            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveInToPos()
        {
            string EMsg = "LR_MoveInToPos";
            try
            {
                Out.SvKickerExt = false;

                if (!Pos.CheckEmpty()) goto _Stop;

                if (Buf1.rt_StType == EBufStType.Buffer) Buf1.SvStopperUp = false;
                if (Buf2.rt_StType == EBufStType.Buffer) Buf2.SvStopperUp = false;

                if (!Pre.StopperDn()) goto _Error;
                if (!Pro.StopperDn()) goto _Error;

                Status = EConvStatus.Busy;

                if (!MoveToPos()) goto _Error;

                if (Status == TaskConv.EConvStatus.Stop) return true;
                Status = EConvStatus.Ready;

                return true;

            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveInToOut()
        {
            string EMsg = "LR_MoveInToOut";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                switch (LeftMode)
                {
                    case ELeftMode.Smema:
                        In.Smema_DO_McReady = false;
                        break;
                }

                Out.SvKickerExt = false;
                Pro.SvPrecisorExt = false;
                Pro.SvVac = false;
                Pro.SvLifterUp = false;
                Pro.SvStopperUp = false;
                Pre.SvPrecisorExt = false;
                Pre.SvVac = false;
                Pre.SvLifterUp = false;
                Pre.SvStopperUp = false;
                Buf2.SvStopperUp = false;
                Buf1.SvStopperUp = false;

                if (!MoveToOut()) goto _Error;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool MoveBuf1ToBuf2()
        {
            string EMsg = "LR_MoveBuf1ToBuf2";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;
                if (!Buf2.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;
                if (!Buf1.StopperDn()) goto _Error;

                if (!MoveToBuf2()) goto _Error;

                Buf1.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;

            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveBuf1ToPre()
        {
            string EMsg = "LR_MoveBuf1ToPre";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;
                if (!Pre.CheckEmpty()) goto _Stop;
                if (!Buf2.CheckEmpty()) goto _Stop;

                if (Buf2.rt_StType == EBufStType.Buffer) Buf2.SvStopperUp = false;

                Status = EConvStatus.Busy;
                if (!Buf1.StopperDn()) goto _Error;

                if (!MoveToPre()) goto _Error;

                Buf1.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;

            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveBuf1ToPro()
        {
            string EMsg = "LR_MoveBuf1ToPro";
            try
            {
                if (!Buf2.CheckEmpty()) goto _Stop;
                if (!Pre.CheckEmpty()) goto _Stop;
                if (!Out.CheckEmpty()) goto _Stop;

                if (Buf2.rt_StType == EBufStType.Buffer) Buf2.SvStopperUp = false;
                if (Pre.rt_StType >= EPreStType.Buffer) Pre.SvStopperUp = false;

                Status = EConvStatus.Busy;

                if (!Buf1.StopperDn()) goto _Error;

                if (!MoveToPro()) goto _Error;
                Buf1.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;

            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveBuf1ToOut()
        {
            string EMsg = "LR_MoveBuf1ToOut";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                switch (LeftMode)
                {
                    case ELeftMode.Smema:
                        In.Smema_DO_McReady = false;
                        break;
                }

                Out.SvKickerExt = false;
                Pro.SvPrecisorExt = false;
                Pro.SvVac = false;
                Pro.SvLifterUp = false;
                Pro.SvStopperUp = false;
                Pre.SvPrecisorExt = false;
                Pre.SvVac = false;
                Pre.SvLifterUp = false;
                Pre.SvStopperUp = false;
                Buf2.SvStopperUp = false;

                if (!Buf1.StopperDn()) goto _Error;

                if (!MoveToOut()) goto _Error;
                Buf1.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveBuf2ToPre()
        {
            string EMsg = "LR_MoveBuf2ToPre";
            try
            {
                if (!Pre.CheckEmpty()) goto _Stop;
                if (!Out.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                if (!Buf2.StopperDn()) return false;

                if (!MoveToPre()) goto _Error;
                Buf2.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveBuf2ToPro()
        {
            string EMsg = "LR_MoveBuf2ToPro";
            try
            {
                if (!Pre.CheckEmpty()) goto _Stop;
                if (!Out.CheckEmpty()) goto _Stop;

                if (Pre.rt_StType >= EPreStType.Buffer) Pre.SvStopperUp = false;

                Status = EConvStatus.Busy;

                if (!Buf2.StopperDn()) return false;

                if (!MoveToPro()) goto _Error;
                Buf2.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveBuf2ToOut()
        {
            string EMsg = "LR_MoveBuf2ToOut";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                switch (LeftMode)
                {
                    case ELeftMode.Smema:
                        In.Smema_DO_McReady = false;
                        break;
                }

                Out.SvKickerExt = false;
                Pro.SvPrecisorExt = false;
                Pro.SvVac = false;
                Pro.SvLifterUp = false;
                Pro.SvStopperUp = false;
                Pre.SvPrecisorExt = false;
                Pre.SvVac = false;
                Pre.SvLifterUp = false;
                Pre.SvStopperUp = false;

                if (!Buf2.StopperDn()) goto _Error;

                if (!MoveToOut()) goto _Error;
                Buf2.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool MovePreToPro()
        {
            string EMsg = "LR_MovePreToPro";
            try
            {
                if (!Pro.CheckEmpty()) goto _Stop;
                if (!Out.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                if (!Pre.PrecisorRet()) goto _Error;
                if (!Pre.VacOff()) goto _Error;
                if (!Pre.LifterDn()) goto _Error;
                if (!Pre.StopperDn()) goto _Error;

                if (!MoveToPro()) goto _Error;
                if (Pre.Status != TaskConv.EProcessStatus.WaitNone) Pre.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MovePreToPos()
        {
            string EMsg = "LR_MovePreToPos";
            try
            {
                if (!Pro.CheckEmpty()) goto _Stop;
                if (!Out.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                if (!Pre.PrecisorRet()) goto _Error;
                if (!Pre.VacOff()) goto _Error;
                if (!Pre.LifterDn()) goto _Error;
                if (!Pre.StopperDn()) goto _Error;

                if (!MoveToPos()) goto _Error;
                if (Pre.Status != TaskConv.EProcessStatus.WaitNone) Pre.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.Stop;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MovePreToOut()
        {
            string EMsg = "LR_LoadPreToOut";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;
                if (!Pro.CheckEmpty()) goto _Stop;
                if (!Pos.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                switch (LeftMode)
                {
                    case ELeftMode.Smema:
                        In.Smema_DO_McReady = false;
                        break;
                }

                Out.SvKickerExt = false;
                Pro.SvPrecisorExt = false;
                Pro.SvVac = false;
                Pro.SvLifterUp = false;
                Pro.SvStopperUp = false;

                if (!Pre.PrecisorRet()) goto _Error;
                if (!Pre.VacOff()) goto _Error;
                if (!Pre.LifterDn()) goto _Error;
                if (!Pre.StopperDn()) goto _Error;

                if (!MoveToOut()) goto _Error;
                Pre.Status = TaskConv.EProcessStatus.Empty;
                if (Pro.Status == EProcessStatus.WaitNone) Pro.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop(); ;
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool MoveProToPos()
        {
            string EMsg = "LR_MoveProToPos";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;
                if (!Pos.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                //switch (LeftMode)
                //{
                //    case ELeftMode.Smema:
                //    case ELeftMode.Smema_SmemaRight:
                //    case ELeftMode.SmemaBiDirection:
                //        In.Smema_DO_McReady = false;
                //        break;
                //}

                //Out.SvKickerExt = false;

                if (!Pro.PrecisorRet()) goto _Error;
                if (!Pro.VacOff()) goto _Error;
                if (!Pro.LifterDn()) goto _Error;
                if (!Pro.StopperDn()) goto _Error;

                if (!MoveToPos()) goto _Error;
                Pro.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop();
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveProToOut()
        {
            string EMsg = "LR_MoveProToOut";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;
                if (!Pos.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                switch (LeftMode)
                {
                    case ELeftMode.Smema:
                        In.Smema_DO_McReady = false;
                        break;
                }

                Out.SvKickerExt = false;

                if (!Pro.PrecisorRet()) goto _Error;
                if (!Pro.VacOff()) goto _Error;
                if (!Pro.LifterDn()) goto _Error;
                if (!Pro.StopperDn()) goto _Error;

                if (!MoveToOut()) goto _Error;
                Pro.Status = TaskConv.EProcessStatus.Empty;
                if (Pre.Status == EProcessStatus.WaitNone) Pre.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop();
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool MovePosToOut()
        {
            string EMsg = "LR_MovePosToOut";
            try
            {
                if (!Out.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                switch (LeftMode)
                {
                    case ELeftMode.Smema:
                        In.Smema_DO_McReady = false;
                        break;
                }

                Out.SvKickerExt = false;

                if (!Pos.VacOff()) goto _Error;
                if (!Pos.LifterDn()) goto _Error;
                if (!Pos.StopperDn()) goto _Error;

                if (!MoveToOut()) goto _Error;
                Pos.Status = TaskConv.EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Error:
                Conv.Stop();
                Status = TaskConv.EConvStatus.ErrorInit;
                return false;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool MoveBuf1ToIn()
        {
            string EMsg = "LR_MoveBuf1ToIn";
            try
            {
                if (!In.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                if (!MoveRevToIn()) return false;
                Buf1.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Stop:
                Conv.Stop();
                return true;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveBuf2ToIn()
        {
            string EMsg = "LR_MoveBuf2ToIn";
            try
            {
                if (!In.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                Buf1.SvStopperUp = false;

                if (!MoveRevToIn()) return false;

                Buf2.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Stop:
                Conv.Stop();
                return true;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MovePreToIn()
        {
            string EMsg = "LR_MovePreToIn";
            try
            {
                if (!In.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                Pre.SvVac = false;
                Pre.SvPrecisorExt = false;
                Pre.SvStopperUp = false;
                Pre.SvLifterUp = false;

                Buf2.SvStopperUp = false;
                Buf1.SvStopperUp = false;

                if (!MoveRevToIn()) return false;
                Pre.Status = EProcessStatus.Empty;
                if (Pro.Status == EProcessStatus.WaitNone) Pro.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Stop:
                Conv.Stop();
                return true;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveProToIn()
        {
            string EMsg = "LR_MoveProToIn";
            try
            {
                if (!Pre.CheckEmpty()) goto _Stop;
                if (!In.CheckEmpty()) goto _Stop;

                Status = EConvStatus.Busy;

                Pro.SvVac = false;
                Pro.SvPrecisorExt = false;
                Pro.SvLifterUp = false;

                Pre.SvVac = false;
                Pre.SvPrecisorExt = false;
                Pre.SvStopperUp = false;
                Pre.SvLifterUp = false;

                Buf2.SvStopperUp = false;
                Buf1.SvStopperUp = false;

                if (!MoveRevToIn()) return false;
                Pre.Status = EProcessStatus.Empty;
                Pro.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Stop:
                Conv.Stop();
                return true;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MoveOutToIn()
        {
            string EMsg = "LR_MoveOutToIn";

            try
            {
                //if (!Pro.CheckEmpty()) goto _Stop;
                if (!Pre.CheckEmpty()) goto _Stop;
                if (!In.CheckEmpty()) goto _Stop;

                Pro.SvPrecisorExt = false;
                Pro.SvVac = false;
                Pro.SvLifterUp = false;
                Pro.SvStopperUp = false;
                Pre.SvPrecisorExt = false;
                Pre.SvVac = false;
                Pre.SvLifterUp = false;
                Pre.SvStopperUp = false;
                Buf2.SvStopperUp = false;
                Buf1.SvStopperUp = false;

                Status = EConvStatus.Busy;

                if (!MoveRevToIn()) return false;
                Out.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool MovePosToIn()
        {
            string EMsg = "LR_MovePosToIn";

            try
            {
                if (!Pro.CheckEmpty()) goto _Stop;
                if (!Pre.CheckEmpty()) goto _Stop;
                if (!In.CheckEmpty()) goto _Stop;

                Pos.SvVac = false;
                Pos.SvLifterUp = false;
                Pos.SvStopperUp = false;

                Pro.SvPrecisorExt = false;
                Pro.SvVac = false;
                Pro.SvLifterUp = false;
                Pro.SvStopperUp = false;

                Pre.SvPrecisorExt = false;
                Pre.SvVac = false;
                Pre.SvLifterUp = false;
                Pre.SvStopperUp = false;

                Buf2.SvStopperUp = false;
                Buf1.SvStopperUp = false;

                Status = EConvStatus.Busy;

                if (!MoveRevToIn()) return false;
                Pos.Status = EProcessStatus.Empty;

                if (Status == TaskConv.EConvStatus.Stop) return false;
                Status = EConvStatus.Ready;
                return true;
            _Stop:
                Conv.Stop(); ;
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool Unload_Out()
        {
            string EMsg = "LR_UnloadOut";
            try
            {
                Status = EConvStatus.Busy;

                if (Out.SensKickerExt) if (!Out.KickerRet()) goto _Error;

                Event.BOARD_SENT_OUT_TO_MAGAZINE.Set();
            _Retry:
                #region Start SendOut
                Conv.Fwd_SendOut();
                int TOut = Environment.TickCount + Out.TimeOut;

                while (true)
                {
                    if (!Out.SensPsnt)
                    {
                        int TOutDelay = Environment.TickCount + Out.Delay;
                        while (true)
                        {
                            if (Out.SensPsnt) break;
                            if (Environment.TickCount >= TOutDelay) break;
                        }
                        if (Environment.TickCount >= TOutDelay) break;
                    }

                    if (Environment.TickCount >= TOut)
                    {
                        Conv.Stop();
                        if (RightMode == ERightMode.Smema) Out.Smema_DO_BdReady = false;

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_UNLOAD_TIMEOUT, "", EMsgBtn.smbRetry_Stop_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry:
                                goto _Retry;
                            case EMsgRes.smrStop:
                            default:
                                goto _Stop;
                            case EMsgRes.smrCancel:
                                Out.Status = EProcessStatus.Empty;
                                goto _Stop;
                        }
                    }
                }

                Conv.Stop();

                if (!Out.KickerExt()) { goto _Error; }
                if (!Out.KickerRet()) { goto _Error; }

                #endregion
                Out.Status = EProcessStatus.Empty;
                Status = EConvStatus.Ready;

                return true;

            _Stop:
                Conv.Stop();
                Status = EConvStatus.Stop;
                return true;
            _Error:
                Conv.Stop();
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }
        internal static bool Unload_Manual()
        {
            if (EnableUnloadMsg)
            {
            _ReCheck:
                Status = EConvStatus.Busy;
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_MANUAL_UNLOAD, "", EMsgBtn.smbOK);
                if (ZEC3002.Ctrl.GetDI(ref ConvIO.Out_SensPsnt)) goto _ReCheck;
                Out.Status = EProcessStatus.Empty;
                Status = EConvStatus.Ready;
            }
            else
                if (!Out.SensPsnt) Out.Status = EProcessStatus.Empty;

            return true;
        }
        internal static bool Unload_C2()
        {
            string EMsg = "Unload_C2";
            try
            {
                Status = EConvStatus.Busy;

                if (Out.SensKickerExt) if (!Out.KickerRet()) goto _Error;
                Event.BOARD_SEND_OUT_CONV2.Set();
            _Retry:
                #region Start SendOut
                Conv.Fwd_Fast();
                Conv2.Fwd_Fast();
                int TOut = Environment.TickCount + Out.TimeOut;

                while (true)
                {
                    if (!Out.SensPsnt)
                    {
                        int TOut2 = Environment.TickCount + 250;
                        while (true)
                        {
                            if (Out.SensPsnt) break;
                            if (Environment.TickCount >= TOut2) break;
                        }
                        if (Environment.TickCount >= TOut2) break;
                    }

                    if (Environment.TickCount >= TOut)
                    {
                        Conv.Stop();
                        Conv2.Stop();

                        Msg MsgBox = new Msg();
                        EMsgRes MsgRes = MsgBox.Show(Messages.CONV_UNLOAD_TIMEOUT, "", EMsgBtn.smbRetry_Stop_Cancel);
                        switch (MsgRes)
                        {
                            case EMsgRes.smrRetry:
                                goto _Retry;
                            case EMsgRes.smrStop:
                            default:
                                goto _Stop;
                            case EMsgRes.smrCancel:
                                Out.Status = EProcessStatus.Empty;
                                goto _Stop;
                        }
                    }
                }

                Conv.Stop();
                Conv2.Stop();

                #endregion
                Out.Status = EProcessStatus.Empty;
                Status = EConvStatus.Ready;

                return true;

            _Stop:
                Conv.Stop();
                Status = EConvStatus.Stop;
                return true;
            _Error:
                Conv.Stop();
                return false;
            }
            catch (Exception Ex)
            {
                Conv.Stop();
                throw new Exception(EMsg + (char)13 + Ex.Message.ToString());
            }
        }

        internal static bool Run_SendOut()
        {
             #region Pos In Send Out
            if (RightMode == ERightMode.ElevatorZ)
            {
                if (!TaskConv.Out.CheckPsnt()) return false;

                #region
                if (TaskElev.Right.Status != TaskElev.EElevStatus.Ready)
                {
                    Status = EConvStatus.Stop;
                    return true;
                }
                if (!TaskElev.Right.CheckMagPsnt())
                {
                    Status = EConvStatus.Stop;
                    return true;
                }
                if (TaskElev.Right.ReadyToReceive)
                {
                    TaskElev.Right.ReadyToReceive = false;
                    #region Send Out
                    TaskElev.Right.TransferBusy = true;

                    if (!Unload_Out()) return false;
                    TaskElev.Right.TransferBusy = false;

                    if (TaskElev.ElevStatus[(int)TaskElev.TElevator.Right] == TaskElev.EElevStatus.Ready)
                    {
                        if (!TaskElev.Right.RunLevelAsyncIsBusy)
                        {
                            if (TaskConv.Out.SensPsnt && !TaskElev.Right.SafeCheck()) return false;
                            TaskElev.Right.RunLevelAsync();
                        }
                    }
                    #endregion
                }
                #endregion
            }
            if (RightMode == ERightMode.ManualUnload)
            {
                Unload_Manual();
            }
            if (RightMode == ERightMode.ProductReturn)
            {
                if (Pre.Status != EProcessStatus.Empty) return true;//goto _End;
                if (Pro.Status != EProcessStatus.Empty) return true;//goto _End;

                Event.BOARD_REVERSE.Set();
                MoveOutToIn();
            }
            if (RightMode == ERightMode.Smema)
            {
                //{2.2.21 - KN
                if (In.Smema_DI_BdReady && In.Smema_DO_McReady) return true;//input transfer is busy; skip unload.
                if (!In.Smema_DI_BdReady) In.Smema_DO_McReady = false;//disable input transfer
                //2.2.21}

                if (!OutBdReadyWaitMcReady) Out.Smema_DO_BdReady = true;

                //if (!Define_Run.OldMode) 
                    Thread.Sleep(50);

                #region Smema Send Out
                if (Out.Smema_DI_McReady)
                {
                    Event.BOARD_SEND_OUT_SMEMA.Set();
                    Out.Smema_DO_BdReady = true;
                    if (!Unload_Out())
                    {
                        Out.Smema_DO_BdReady = false;
                        return false;
                    }
                    Out.Smema_DO_BdReady = false;
                    if (!Out.DL_WaitMcNotReady()) return false;
                }
                #endregion
            }
            #endregion

            return true;
        }
        internal static bool Run_MoveInTo(TaskConv.EStation Station)
        {
            if (StopInput) return true;

            switch (LeftMode)
            {
                case ELeftMode.ElevatorZ:
                    {
                        #region
                        if (In.SensPsnt) goto _ContinueLoad;

                        if (TaskElev.Left.Status == TaskElev.EElevStatus.Ready)
                        {
                            #region
                            if (Status == EConvStatus.Stop) return false;
                            if (!PushIn())
                            {
                                TaskElev.Left.TransferBusy = false;
                                return false;
                            }
                            #endregion
                        }
                        _ContinueLoad:
                        if (In.SensPsnt)
                        {
                            Event.BOARD_PUSH_ARRIVED_IN_STATION.Set();
                            //In.BlowSuckOn();
                            In.BlowSuckTimed();
                            #region Load
                            switch (Station)
                            {
                                case EStation.Buf1:
                                    if (!MoveInToBuf1()) return false;// goto _Stop;
                                    break;
                                case EStation.Buf2:
                                    if (!MoveInToBuf2()) return false;
                                    break;
                                case EStation.Pre:
                                    if (!MoveInToPre()) return false;
                                    break;
                                case EStation.Pro:
                                    if (!MoveInToPro()) return false;
                                    break;
                            }
                            //TaskElev.Left.TransferBusy = false;
                            #endregion
                        }
                        TaskElev.Left.TransferBusy = false;
                        #endregion
                        break;
                    }
                case ELeftMode.ManualLoad:
                    {
                        #region
                        if (In.SensPsnt)
                        {
                            Event.BOARD_MANUAL_ARRIVED_IN_STATION.Set();
                            //In.BlowSuckOn();
                            In.BlowSuckTimed();
                            switch (Station)
                            {
                                case EStation.Buf1:
                                    if (!MoveInToBuf1()) return false;
                                    break;
                                case EStation.Buf2:
                                    if (!MoveInToBuf2()) return false;
                                    break;
                                case EStation.Pre:
                                    if (!MoveInToPre()) return false;
                                    break;
                                case EStation.Pro:
                                    if (!MoveInToPro()) return false;
                                    break;
                            }
                        }
                        #endregion
                        break;
                    }
                case ELeftMode.Smema:
                    #region Smema
                    bool InPsnt = false;
                    In.Smema_DO_McReady = true;
                    if (In.Smema_DI_BdReady)
                    {
                        InPsnt = In.SensPsnt;
                    }

                    if (InPsnt)
                    {
                        Event.BOARD_SMEMA_ARRIVED_IN_STATION.Set();
                        //In.BlowSuckOn();
                        In.BlowSuckTimed();
                        switch (Station)
                        {
                            case EStation.Buf1:
                                if (!MoveInToBuf1()) return false;
                                break;
                            case EStation.Buf2:
                                if (!MoveInToBuf2()) return false;
                                break;
                            case EStation.Pre:
                                if (!MoveInToPre()) return false;
                                break;
                            case EStation.Pro:
                                if (!MoveInToPro()) return false;
                                break;
                        }
                    }
                    #endregion
                    break;
            }

            return true;
        }

        internal static bool Manual_Return()//Return frame at Leftmost station
        {
            if (!TaskConv.CheckReady()) return false;

            Event.MANUAL_RETURN.Set();

            switch (TaskConv.Buf1.Status)
            {
                case TaskConv.EProcessStatus.Psnt:
                    {
                        TaskConv.MoveBuf1ToIn();
                        return true;
                    }
            }

            switch (TaskConv.Buf2.Status)
            {
                case TaskConv.EProcessStatus.Psnt:
                    {
                        TaskConv.MoveBuf2ToIn();
                        return true;
                    }
            }

            if (TaskConv.Pre.Status >= TaskConv.EProcessStatus.Heating)
            {
                if (TaskConv.Pos.rt_StType == EPosStType.Buffer && TaskConv.Pos.Status > EProcessStatus.Heating)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "PRO");
                    return true;
                }
                TaskConv.MovePreToIn();
                return true;
            }

            if (TaskConv.Pro.Status >= TaskConv.EProcessStatus.Heating)
            {
                if (TaskConv.Pos.rt_StType == EPosStType.Buffer && TaskConv.Pos.Status > EProcessStatus.Heating)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.CONV_SENSOR_PART_PSNT, "PRO");
                    return true;
                }

                if (DispProg.rt_Read_IDs[0, 0].Length > 0 && TaskDisp.InputMap_Protocol == TaskDisp.EInputMapProtocol.OSRAM_eMos)
                {
                    DispProg.TOutputMap.Execute("", DispProg.rt_Read_IDs[0, 0], ref DispProg.Map.CurrMap[0], TaskDisp.InputMap_Enabled);
                }
                TaskConv.MoveProToIn();
                return true;
            }

            if (TaskConv.Pos.Status >= TaskConv.EProcessStatus.Heating)
            {
                TaskConv.MovePosToIn();
                return true;
            }

            switch (TaskConv.Out.Status)
            {
                case TaskConv.EProcessStatus.Psnt:

                    if (TaskConv.Pre.Status == TaskConv.EProcessStatus.Empty &&
                        TaskConv.Pro.Status == TaskConv.EProcessStatus.Empty)
                    {
                        TaskConv.MoveOutToIn();
                        return true;
                    }
                    break;
            }

            return true;
        }
        internal static bool Manual_LoadBuf1()
        {
            if (!TaskConv.CheckReady()) return false;

            Event.MANUAL_LOADBUF1.Set();

            if (TaskConv.Buf1.Status == TaskConv.EProcessStatus.Empty)
            {
                if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                {
                    if (TaskConv.In.SensPsnt) goto _ContLoad;

                    if (TaskElev.Left.Status == TaskElev.EElevStatus.Ready)
                    {
                        if (!TaskConv.PushIn()) return false;
                    }
                    TaskElev.Left.TransferBusy = false;
                }
            _ContLoad:
                if (TaskConv.In.SensPsnt)
                {
                    //In.BlowSuckOn();
                    In.BlowSuckTimed();
                    TaskConv.MoveInToBuf1();
                    return true;
                }
            }
            return true;
        }
        internal static bool Manual_LoadBuf2()
        {
            if (!TaskConv.CheckReady()) return false;

            Event.MANUAL_LOADBUF2.Set();

            if (TaskConv.Buf2.Status == TaskConv.EProcessStatus.Empty)
            {
                if (TaskConv.Buf1.SensPsnt)
                {
                    TaskConv.MoveBuf1ToBuf2();
                    return true;
                }

                if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                {
                    if (TaskConv.In.SensPsnt) goto _ContLoad;

                    if (TaskElev.Left.Status == TaskElev.EElevStatus.Ready)
                    {
                        if (!TaskConv.PushIn()) return false;
                    }
                    TaskElev.Left.TransferBusy = false;
                }
            _ContLoad:
                if (TaskConv.In.SensPsnt)
                {
                    //In.BlowSuckOn();
                    In.BlowSuckTimed();
                    TaskConv.MoveInToBuf2();
                    return true;
                }
            }
            return true;
        }
        internal static bool Manual_LoadPre()
        {
            if (!TaskConv.CheckReady()) return false;

            Event.MANUAL_LOADPRE.Set();

            if (TaskConv.Out.SensPsnt) return true;

            if (TaskConv.Pre.Status == TaskConv.EProcessStatus.Empty)
            {
                if (TaskConv.Buf2.SensPsnt)
                {
                    if (TaskConv.In.SensPsnt) return true;
                    TaskConv.MoveBuf2ToPre();
                    return true;
                }

                if (TaskConv.Buf1.SensPsnt)
                {
                    if (TaskConv.In.SensPsnt) return true;
                    TaskConv.MoveBuf1ToPre();
                    return true;
                }

                if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                {
                    if (TaskConv.In.SensPsnt) goto _ContLoad;

                    if (TaskElev.Left.Status == TaskElev.EElevStatus.Ready)
                    {
                        if (!TaskConv.PushIn()) return false;
                        TaskElev.Left.TransferBusy = false;
                    }
                    TaskElev.Left.TransferBusy = false;
                }
            _ContLoad:
                if (TaskConv.In.SensPsnt)
                {
                    //In.BlowSuckOn();
                    In.BlowSuckTimed();
                    TaskConv.MoveInToPre();
                    return true;
                }
                return true;
            }
            return true;
        }
        internal static bool Manual_LoadPro()
        {
            if (!TaskConv.CheckReady()) return false;

            Event.MANUAL_LOADPRO.Set();

            if (TaskConv.Out.SensPsnt) return true;

            if (TaskConv.Pro.Status == TaskConv.EProcessStatus.Empty ||
                TaskConv.Pro.Status == TaskConv.EProcessStatus.WaitNone)
            {
                if (TaskConv.Pre.SensPsnt)
                {
                    if (TaskConv.In.SensPsnt) return true;

                    TaskConv.MovePreToPro();
                    return true;
                }

                if (TaskConv.Buf2.SensPsnt)
                {
                    if (TaskConv.In.SensPsnt) return true;

                    TaskConv.MoveBuf2ToPro();
                    return true;
                }

                if (TaskConv.Buf1.SensPsnt)
                {
                    if (TaskConv.In.SensPsnt) return true;

                    TaskConv.MoveBuf1ToPro();
                    return true;
                }

                if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                {
                    if (TaskConv.In.SensPsnt) goto _ContLoad;

                    if (TaskElev.Left.Status == TaskElev.EElevStatus.Ready)
                    {
                        if (!TaskConv.PushIn()) return false;
                        TaskElev.Left.TransferBusy = false;
                    }
                    TaskElev.Left.TransferBusy = false;
                }
            _ContLoad:
                if (TaskConv.Pre.SensPsnt)
                {
                    In.BlowSuckTimed();
                    TaskConv.MovePreToPro();
                    return true;
                }

                if (TaskConv.In.SensPsnt)
                {
                    In.BlowSuckTimed();
                    TaskConv.MoveInToPro();
                    return true;
                }
            }
            return true;
        }
        internal static bool Manual_LoadPos()
        {
            if (!TaskConv.CheckReady()) return false;

            Event.MANUAL_LOADPOS.Set();

            if (TaskConv.Out.SensPsnt) return true;

            if (TaskConv.Pos.Status == TaskConv.EProcessStatus.Empty ||
                TaskConv.Pos.Status == TaskConv.EProcessStatus.WaitNone)
            {
                if (TaskConv.Pro.SensPsnt)
                {
                    if (TaskConv.In.SensPsnt) return true;

                    TaskConv.MoveProToPos();
                    return true;
                }

                if (TaskConv.Pre.SensPsnt)
                {
                    if (TaskConv.In.SensPsnt) return true;

                    TaskConv.MovePreToPos();
                    return true;
                }

                if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                {
                    if (TaskConv.In.SensPsnt) goto _ContLoad;

                    if (TaskElev.Left.Status == TaskElev.EElevStatus.Ready)
                    {
                        if (!TaskConv.PushIn()) return false;
                    }
                    TaskElev.Left.TransferBusy = false;
                }
            _ContLoad:
                if (TaskConv.Pro.SensPsnt)
                {
                    In.BlowSuckTimed();
                    TaskConv.MoveProToPos();
                    return true;
                }

                if (TaskConv.Pre.SensPsnt)
                {
                    In.BlowSuckTimed();
                    TaskConv.MovePreToPos();
                    return true;
                }

                if (TaskConv.In.SensPsnt)
                {
                    In.BlowSuckTimed();
                    TaskConv.MoveInToPos();
                    return true;
                }
            }
            return true;
        }
        internal static bool Manual_Unload()//Unload frame at Rightmost station
        {
            if (!TaskConv.CheckReady()) return false;

            Event.MANUAL_UNLOAD.Set();

            if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ)
            {
                if (!TaskElev.Right.ReadyToReceive)
                {
                    TaskElev.Right.RunLevel();
                }
            }

            if (!TaskConv.Out.SensPsnt)
            {
                TaskConv.Out.Status = TaskConv.EProcessStatus.Empty;

                if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ && !TaskElev.Right.ReadyToReceive) goto _End;

                if (TaskConv.Pos.Status >= TaskConv.EProcessStatus.Heating)
                {
                    if (TaskConv.In.SensPsnt) goto _End;

                    TaskConv.MovePosToOut();
                    goto _Store;
                }
                if (TaskConv.Pro.Status >= TaskConv.EProcessStatus.Heating)
                {
                    if (TaskConv.In.SensPsnt) goto _End;

                    if (DispProg.rt_Read_IDs[0, 0].Length > 0 && TaskDisp.InputMap_Protocol == TaskDisp.EInputMapProtocol.OSRAM_eMos)
                    {
                        DispProg.TOutputMap.Execute("", DispProg.rt_Read_IDs[0, 0], ref DispProg.Map.CurrMap[0], TaskDisp.InputMap_Enabled);
                    }

                    TaskConv.MoveProToOut();
                    goto _Store;
                }
                if (TaskConv.Pre.Status >= TaskConv.EProcessStatus.Heating)
                {
                    if (TaskConv.In.SensPsnt) goto _End;

                    TaskConv.MovePreToOut();
                    goto _Store;
                }
                if (TaskConv.Buf2.rt_StType == TaskConv.EBufStType.Buffer && TaskConv.Buf2.Status >= TaskConv.EProcessStatus.Heating)
                {
                    if (TaskConv.In.SensPsnt) goto _End;

                    TaskConv.MoveBuf2ToOut();
                    goto _Store;
                }
                if (TaskConv.Buf1.rt_StType == TaskConv.EBufStType.Buffer && TaskConv.Buf1.Status >= TaskConv.EProcessStatus.Heating)
                {
                    if (TaskConv.In.SensPsnt) goto _End;

                    TaskConv.MoveBuf1ToOut();
                    goto _Store;
                }
                if (TaskConv.In.SensPsnt)
                {
                    TaskConv.MoveInToOut();
                    goto _End;
                }
            }
        _Store:
            if (TaskConv.Out.SensPsnt)
            {
                if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ)
                {
                    if (!TaskElev.Right.ReadyToReceive)
                    {
                        TaskElev.Right.RunLevel();
                    }

                    if (TaskElev.Right.ReadyToReceive)
                    {
                        #region Product to Store
                        if (TaskConv.Out.Status == TaskConv.EProcessStatus.Psnt)
                        {
                            #region check magazine is present
                            bool magPsnt = TaskElev.Right.SensMagNoPsnt(TaskElev.Setups[(int)TaskElev.TElevator.Right].PsntMagz);
                            if (!magPsnt) goto _End;
                            #endregion

                            TaskElev.Right.ReadyToReceive = false;
                            TaskElev.Right.TransferBusy = true;
                            TaskConv.Status = TaskConv.EConvStatus.Busy;

                            TaskConv.Unload_Out();

                            TaskElev.Right.TransferBusy = false;
                            TaskConv.Out.Status = TaskConv.EProcessStatus.Empty;
                            TaskConv.Status = TaskConv.EConvStatus.Ready;
                        }
                        #endregion

                        #region Right Elev Move Next Level
                        if (TaskElev.Right.Status != TaskElev.EElevStatus.Ready) goto _End;

                        TaskElev.Right.RunLevelAsync();
                        #endregion
                    }
                }
            }
        _End:
            return true;
        }
        internal static bool Manual_LoadForward()//Load avail frame to Rightmost station
        {
            if (!TaskConv.CheckReady()) return false;

            if (TaskConv.Out.SensPsnt) return true;

            if (TaskConv.Pro.Status == TaskConv.EProcessStatus.Empty) return Manual_LoadPro();
            if (TaskConv.Pre.Status == TaskConv.EProcessStatus.Empty) return Manual_LoadPre();
            if (TaskConv.Buf2.rt_StType == EBufStType.Buffer && TaskConv.Buf2.Status == EProcessStatus.Empty) return Manual_LoadBuf2();
            if (TaskConv.Buf1.rt_StType == EBufStType.Buffer && TaskConv.Buf1.Status == EProcessStatus.Empty) return Manual_LoadBuf1();

            return true;
        }

        public static bool ConvRun = false;
        public static void Run()
        {
            string EMsg = "Run ";

            try
            {
                if (Status != EConvStatus.Ready) goto _End;

                #region Setting Condition Check
                if (TaskConv.OutLevelQtyFollowIn)
                {
                    if (RightMode != ERightMode.ElevatorZ) TaskConv.OutLevelQtyFollowIn = false;
                }
                #endregion

                #region Operation Condition Check
                if (!CheckHeaterInRange()) { goto _Stop; }
                #endregion

                #region Run Right Elev
                if (TaskConv.OutLevelQtyFollowIn)
                {
                    if (TaskElev.Left.b_MagChanged &&
                        Buf1.Status == EProcessStatus.Empty &&
                        Buf2.Status == EProcessStatus.Empty &&
                        Pre.Status == EProcessStatus.Empty &&
                        Pro.Status == EProcessStatus.Empty &&
                        Out.Status == EProcessStatus.Empty)
                    {
                        int PsntMag = TaskElev.Right.Setup.PsntMagz;
                        TaskElev.Right.Setup.PsntLevel = TaskElev.Right.Setup.LevelCount;
                        TaskElev.Right.ReadyToReceive = false;
                    }
                }

                if (RightMode == ERightMode.ElevatorZ)
                {
                    if (TaskElev.ElevStatus[(int)TaskElev.TElevator.Right] == TaskElev.EElevStatus.Ready)
                    {
                        if (!TaskElev.Right.ReadyToReceive)
                        {
                            if (!TaskElev.Right.RunLevelAsyncIsBusy)
                            {
                                if (TaskConv.Out.SensPsnt && !TaskElev.Right.SafeCheck()) goto _Stop;
                                TaskElev.Right.RunLevelAsync();
                            }
                        }
                    }
                }
                #endregion

                if (Out.Status == EProcessStatus.Psnt)
                {
                    if (!Run_SendOut()) goto _Stop;
                    if (UnloadStop)
                    {
                        UnloadStop = false;
                        Status = EConvStatus.Stop;
                        goto _End;
                    }

                    return;
                }

                if (Pre.Status == EProcessStatus.Heating) Pre.HeatEnd();

                if (Pro.Status == EProcessStatus.Heating) Pro.HeatEnd();

                if (Pos.Status == EProcessStatus.Heating) Pos.HeatEnd();

                if (!NewConvSequence)
                {
                    #region//old
                    if (Out.Status == EProcessStatus.Empty)
                    {
                        if (DispEndStop) goto _End;

                        if (Pro.Status == EProcessStatus.Psnt)
                        {
                            #region
                            if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ && !TaskElev.Right.ReadyToReceive) goto _End;
                            if (!MoveProToOut()) goto _End;
                            #endregion
                            goto _End;
                        }

                        //if (/*(TaskConv.Pre.rt_StType == EPreStType.Disp || TaskConv.Pre.rt_StType == EPreStType.Disp12)
                        //    &&*/ Pre.Status == EProcessStatus.Psnt
                        //    && Pro.Status == EProcessStatus.Empty)
                        //{
                        //    if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ && !TaskElev.Right.ReadyToReceive) goto _End;
                        //    if (!MovePreToOut()) goto _End;
                        //    goto _End;
                        //}

                        if (/*Pre.rt_StType == EPreStType.Disp1 && */Pre.Status == EProcessStatus.Empty)
                        {
                            #region
                            if (Pre.rt_StType == EPreStType.None) goto _End;

                            if (TaskConv.Buf2.rt_StType == EBufStType.Buffer && Buf2.Status == EProcessStatus.Psnt)
                            {
                                if (!MoveBuf2ToPre()) goto _End;
                                goto _PreEnd;
                            }

                            if (TaskConv.Buf1.rt_StType == EBufStType.Buffer && Buf1.Status == EProcessStatus.Psnt)
                            {
                                if (!MoveBuf1ToPre()) goto _End;
                                goto _PreEnd;
                            }

                            if (Status == EConvStatus.Stop) goto _End;

                            if (!Run_MoveInTo(EStation.Pre)) goto _Stop;

                            if (TaskMHS.CustomMode == TaskMHS.ECustomMode.OSRAMSCCSeq) StopInput = true;

                            _PreEnd:
                            #endregion
                            goto _End;
                        }

                        if (Pro.Status == EProcessStatus.Empty)
                        {
                            #region
                            if ((//TaskConv.Pre.rt_StType == EPreStType.Disp ||
                                 //TaskConv.Pre.rt_StType == EPreStType.Disp1 ||
                                 TaskConv.Pre.rt_StType == EPreStType.Buffer
                                 ) &&
                                Pre.Status == EProcessStatus.Psnt)
                            {
                                if (!MovePreToPro()) goto _End;
                                goto _ProEnd;
                            }

                            if (TaskConv.Buf2.rt_StType == EBufStType.Buffer && Buf2.Status == EProcessStatus.Psnt)
                            {
                                if (!MoveBuf2ToPro()) goto _End;
                                goto _ProEnd;
                            }

                            if (TaskConv.Buf1.rt_StType == EBufStType.Buffer && Buf1.Status == EProcessStatus.Psnt)
                            {
                                if (!MoveBuf1ToPro()) goto _End;
                                goto _ProEnd;
                            }

                            if (TaskConv.In.SensPsnt)
                            {
                                if (!MoveInToPro()) goto _End;
                                goto _ProEnd;
                            }

                            if (Status == EConvStatus.Stop) goto _End;

                            if (!Run_MoveInTo(EStation.Pro)) goto _Stop;
                            _ProEnd:

                            #endregion
                            goto _End;
                        }

                        if (Pre.rt_StType > EPreStType.None && (Pre.Status == EProcessStatus.Empty))
                        {
                            #region
                            if (Pre.rt_StType == EPreStType.None) goto _End;

                            if (TaskConv.Buf2.rt_StType == EBufStType.Buffer && Buf2.Status == EProcessStatus.Psnt)
                            {
                                if (!MoveBuf2ToPre()) goto _End;
                                goto _PreEnd;
                            }

                            if (TaskConv.Buf1.rt_StType == EBufStType.Buffer && Buf1.Status == EProcessStatus.Psnt)
                            {
                                if (!MoveBuf1ToPre()) goto _End;
                                goto _PreEnd;
                            }

                            if (TaskConv.In.SensPsnt)
                            {
                                if (!MoveInToPre()) goto _End;
                                goto _PreEnd;
                            }

                            if (Status == EConvStatus.Stop) goto _End;
                            if (!Run_MoveInTo(EStation.Pre)) goto _Stop;
                            _PreEnd:;

                            #endregion
                            goto _End;
                        }

                        if (Buf2.rt_StType == EBufStType.Buffer && Buf2.Status == EProcessStatus.Empty)
                        {
                            #region
                            if (Buf1.rt_StType == EBufStType.Buffer && Buf1.Status == EProcessStatus.Psnt)
                            {
                                if (!MoveBuf1ToBuf2()) goto _End;
                                goto _EndBuf2;
                            }

                            if (TaskConv.In.SensPsnt)
                            {
                                if (!MoveInToBuf2()) goto _End;
                                goto _EndBuf2;
                            }

                            if (Status == EConvStatus.Stop) goto _End;
                            if (!Run_MoveInTo(EStation.Buf2)) goto _Stop;
                            _EndBuf2:;
                            #endregion
                            goto _End;
                        }
                        if (Buf1.rt_StType == EBufStType.Buffer && Buf1.Status == EProcessStatus.Empty)
                        {
                            if (TaskConv.In.SensPsnt)
                            {
                                if (!MoveInToBuf1()) goto _End;
                                goto _EndBuf1;
                            }

                            if (Status == EConvStatus.Stop) goto _End;
                            if (!Run_MoveInTo(EStation.Buf1)) goto _Stop;
                            _EndBuf1:;
                            goto _End;
                        }
                    }
                    #endregion
                }
                else//new sequence
                {
                    if (Out.Status == EProcessStatus.Empty)
                    {
                        if (DispEndStop) goto _End;

                        if (Pro.Status == EProcessStatus.Psnt)
                        {
                            if (Pos.rt_StType == EPosStType.Buffer && Pos.Status == EProcessStatus.Empty)
                            {
                                #region
                                if (Pro.Status == EProcessStatus.Psnt)
                                {
                                    if (!MoveProToPos()) goto _End;
                                }

                                if (Status == EConvStatus.Stop) goto _End;
                                #endregion
                            }
                            else
                            {
                                if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ && !TaskElev.Right.ReadyToReceive) goto _End;
                                if (!MoveProToOut()) goto _End;
                            }
                            if (Status == EConvStatus.Stop) goto _End;
                        }

                        if (Pro.Status == EProcessStatus.Empty)
                        {
                            if (Pre.rt_StType == EPreStType.Buffer && Pre.Status == EProcessStatus.Psnt)
                            {
                                if (!MovePreToPro()) goto _End;
                                goto _ProEnd;
                            }

                            if (Pre.rt_StType == EPreStType.None)
                            {
                                if (TaskConv.In.SensPsnt)
                                {
                                    if (!MoveInToPro()) goto _End;
                                    goto _ProEnd;
                                }

                                if (!Run_MoveInTo(EStation.Pro)) goto _Stop;
                            }

                            if (Status == EConvStatus.Stop) goto _End;
                            _ProEnd:;
                        }

                        if (Pre.rt_StType == EPreStType.Buffer && Pre.Status == EProcessStatus.Empty)
                        {
                            if (TaskConv.In.SensPsnt)
                            {
                                if (!MoveInToPre()) goto _End;
                                goto _PreEnd;
                            }

                            if (!Run_MoveInTo(EStation.Pre)) goto _Stop;

                            _PreEnd:
                            if (TaskMHS.CustomMode == TaskMHS.ECustomMode.OSRAMSCCSeq) StopInput = true;
                            if (Status == EConvStatus.Stop) goto _End;
                        }

                        if (Pos.rt_StType == EPosStType.Buffer && Pos.Status == EProcessStatus.Psnt)
                        {
                            if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ && !TaskElev.Right.ReadyToReceive) goto _End;
                            if (!MovePosToOut()) goto _End;
                        }
                    }
                }
            _End:
                #region Update Process Status
                if (Pro.Status == EProcessStatus.Psnt)
                {
                    if (DispEndStop)
                    {
                        DispEndStop = false;
                        Status = EConvStatus.Stop;
                    }
                }

                if (Pre.Status == EProcessStatus.Empty
                    && Pro.Status == EProcessStatus.Empty
                    && Out.Status == EProcessStatus.Empty)
                {
                    if (StopInput)
                    {
                        //GDefine.StopInput = false;
                        //Status = EConvStatus.Stop;
                    }
                    if (TaskConv.LeftMode == ELeftMode.ElevatorZ)
                    {
                        if (TaskElev.Left.WaitMagChange)
                            Status = EConvStatus.Stop;
                    }
                }
                #endregion
                return;

            _Stop:
                In.Smema_DO_McReady = false;
                Out.Smema_DO_BdReady = false;
                Conv.Stop();
                Status = EConvStatus.Stop;
                return;
            }
            catch (Exception Ex)
            {
                EMsg = EMsg + Ex.Message;
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.CONV_EX_ERR, EMsg, EMsgBtn.smbOK);
            }
        }
        public static void Stop()
        {
            In.Smema_DO_McReady = false;
            Out.Smema_DO_BdReady = false;
            StopInput = false;
        }
    }

    public class TaskElev
    {
        static string ClassName = "TaskElev";

        #region Status
        public enum EElevStatus
        {
            ErrorInit = 0,
            Ready,
            Busy,
        }
        public static EElevStatus[] ElevStatus = new EElevStatus[MAX_ELEV_COUNT];
        public static Color[] ElevStatusColor = { Color.Red, Color.Lime, Color.Yellow };
        #endregion
        #region Var Declaration
        public const int MAX_ELEV_COUNT = 2;
        public enum TElevator
        {
            Left = 0,
            Right,
        }
        public const int MAX_MAG_COUNT = 4;
        public enum EMagazine
        {
            Magazine1,
            Magazine2,
            Magazine3,
            Magazine4,
        }
        public enum EDoorSens
        {
            None,
            Enable,
            ForceStop,
        }
        public enum EPusherType
        {
            Disable,
            DCMotor,
            StepMotor,
            StepMotorJam
        }

        public class TSetup
        {
            public int PsntMagz;
            public int PsntLevel;

            public int MagCount = 2;
            public int LevelCount = 2;
            public double LevelPitch = 3;
            public double[] Mag1stLevelPos = new double[MAX_MAG_COUNT + 1] { 0, 0, 0, 0, 0 };

            public int PusherType = 0;
            public int PusherExtDelay = 25;
            public int PusherRetDelay = 25;
            public int PusherTimeout = 5000;
            public int PusherRetry = 3;
            public bool PusherRunConv = false;

            public int EnableDoorSens;
            public bool EnabledMagLoadPos;
            public double MagLoadPos;

            public TSetup()
            {
                //for (int j = 0; j < MAX_MAG_COUNT; j++)
                //{
                //    this.Mag1stLevelPos[j] = 0;
                //}
            }

            public void Save(string FullRecipeName, string Section)
            {
                NUtils.IniFile IniFile = new NUtils.IniFile(FullRecipeName);

                IniFile.WriteInteger(Section, "PusherExtDelay", PusherExtDelay);
                IniFile.WriteInteger(Section, "PusherRetDelay", PusherRetDelay);
                IniFile.WriteInteger(Section, "PusherTimeout", PusherTimeout);
                IniFile.WriteInteger(Section, "PusherRetry", PusherRetry);

                IniFile.WriteInteger(Section, "LevelCount", LevelCount);
                IniFile.WriteDouble(Section, "LevelPitch", LevelPitch);
                IniFile.WriteInteger(Section, "MagCount", MagCount);

                for (int i = 0; i < MAX_MAG_COUNT; i++)
                {
                    IniFile.WriteDouble(Section, "MagPos" + i.ToString(), Mag1stLevelPos[i]);
                }

                IniFile.WriteInteger(Section, "EnableDoorSens", EnableDoorSens);
                IniFile.WriteBool(Section, "EnabledMagLoadPos", EnabledMagLoadPos);
                IniFile.WriteDouble(Section, "MagLoadPos", MagLoadPos);
                IniFile.WriteInteger(Section, "PusherType", PusherType);
                IniFile.WriteBool(Section, "PusherRunConv", PusherRunConv);
            }
            public void Load(string FullRecipeName, string Section)
            {
                NUtils.IniFile IniFile = new NUtils.IniFile(FullRecipeName);

                PusherExtDelay = IniFile.ReadInteger(Section, "PusherExtDelay", 100);
                PusherRetDelay = IniFile.ReadInteger(Section, "PusherRetDelay", 100);
                PusherTimeout = IniFile.ReadInteger(Section, "PusherTimeout", 5000);
                PusherRetry = IniFile.ReadInteger(Section, "PusherRetry", 3);

                LevelCount = IniFile.ReadInteger(Section, "LevelCount", 5);
                LevelPitch = IniFile.ReadDouble(Section, "LevelPitch", 5);
                MagCount = IniFile.ReadInteger(Section, "MagCount", 2);

                for (int i = 0; i < MAX_MAG_COUNT; i++)
                {
                    Mag1stLevelPos[i] = IniFile.ReadDouble(Section, "MagPos" + i.ToString(), 0);
                }

                EnableDoorSens = IniFile.ReadInteger(Section, "EnableDoorSens", 1);
                EnabledMagLoadPos = IniFile.ReadBool(Section, "EnabledMagLoadPos", true);
                MagLoadPos = IniFile.ReadDouble(Section, "MagLoadPos", 0);
                PusherType = IniFile.ReadInteger(Section, "PusherType", 2);
                PusherRunConv = IniFile.ReadBool(Section, "PusherRunConv", false);
            }
        }
        public static TSetup[] Setups = new TSetup[MAX_ELEV_COUNT] { new TSetup(), new TSetup() };
        #endregion

        public static bool OpenBoard()
        {
            return OpenBoard(ElevIO.BoardID);
        }
        public static bool OpenBoard(byte ZkaBoardID)
        {
            string CMsg = "ElevIO.OpenBoard " + (char)13;

            ElevIO.BoardID = ZkaBoardID;

            if (!ZEC3002.Ctrl.OpenBoard(ElevIO.BoardID, ElevIO.DIOModel))
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_OPEN_BOARD_FAIL);
                return false;
            }
            return true;
        }
        public static bool BoardIsOpen
        {
            get
            {
                return ZEC3002.Ctrl.BoardOpened(ElevIO.BoardID);
            }
        }
        public void CloseBoard()
        {
            CloseBoard(ElevIO.BoardID);
        }
        public void CloseBoard(int BoardID)
        {
            ZEC3002.Ctrl.CloseBoard(BoardID);
        }

        public static void SaveRecipe(string Filename)
        {
            ElevIO.SaveMotorPara(GDefine.MHSMotorParaFile);

            string FullFilename = GDefine.MHSRecipePath + "\\" + Filename;
            Setups[(int)TElevator.Left].Save(FullFilename, TElevator.Left.ToString());
            Setups[(int)TElevator.Right].Save(FullFilename, TElevator.Right.ToString());
        }
        public static void SaveRecipe()
        {
            SaveRecipe(GDefine.MHSRecipeName + GDefine.MHSRecipeExt);
        }
        public static void LoadRecipe()
        {
            ElevIO.LoadMotorPara(GDefine.MHSMotorParaFile);

            string FullFilename = GDefine.MHSRecipePath + "\\" + GDefine.MHSRecipeName + GDefine.MHSRecipeExt;
            Setups[(int)TElevator.Left].Load(FullFilename, TElevator.Left.ToString());
            Setups[(int)TElevator.Right].Load(FullFilename, TElevator.Right.ToString());
        }

        public static void JogP(ZEC3002.Ctrl.TAxis Axis)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return; }

            string EMsg = ClassName + ".JogP " + Axis.Name + " - Fail" + (char)13;
            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {
                try
                {
                    if (Axis.AxisID == 1)
                    {
                        if (!Left.PusherHome()) { return; }
                    }
                    if (Axis.AxisID == 3)
                    {
                        //if (!Right.PusherReturn()) { return; }
                    }
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                    return;
                }

                try
                {
                    ZEC3002.Ctrl.ZM3xx_MovePtpRel1(Axis, 1000);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_AXIS_JOG_FAIL, EMsg + Ex.Message, EMsgBtn.smbOK);
                    return;
                }
            }
        }
        public static void JogN(ZEC3002.Ctrl.TAxis Axis)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return; }
            string EMsg = ClassName + ".JogN " + Axis.Name + " - Fail" + (char)13;
            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {
                try
                {
                    if (Axis.AxisID == 1)
                    {
                        if (!Left.PusherHome()) { return; }
                    }
                    if (Axis.AxisID == 3)
                    {
                        //if (!Right.PusherReturn()) { return; }
                    }
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                    return;
                }

                try
                {
                    //ZEC3002.Ctrl.ZM3xx_JogN(Axis);
                    ZEC3002.Ctrl.ZM3xx_MovePtpRel1(Axis, -1000);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_AXIS_JOG_FAIL, EMsg + Ex.Message, EMsgBtn.smbOK);
                    return;
                }
            }
        }
        public static void MoveRel(ZEC3002.Ctrl.TAxis Axis, double Dist)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return; }
            string EMsg = ClassName + ".MoveRel " + Axis.Name + " - Fail" + (char)13;

            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {

                try
                {
                    if (Axis.AxisID == 1)
                    {
                        if (!Left.PusherHome()) { return; }
                    }
                    if (Axis.AxisID == 3)
                    {
                        //if (!Right.PusherReturn()) { return; }
                    }
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                    return;
                }

                try
                {
                    ZEC3002.Ctrl.ZM3xx_MovePtpRel1(Axis, Dist);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_MOVE_RELATIVE_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                    return;
                }
            }
        }
        public static void MoveConst(ZEC3002.Ctrl.TAxis Axis, double Dist, double Speed)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return; }
            string EMsg = ClassName + ".MoveConst " + Axis.Name + " - Fail" + (char)13;

            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {
                try
                {
                    SetMotionParam(ref Axis, 1, Speed, Axis.MotorPara.Accel);
                    ZEC3002.Ctrl.ZM3xx_MovePtpRel1(Axis, Dist);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_MOVE_CONST_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                    return;
                }
            }
        }
        public static double Pos(ZEC3002.Ctrl.TAxis Axis)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return 0; }
            double Pos = 0;
            try
            {
                ZEC3002.Ctrl.ZM3xx_RLPos(Axis, ref Pos);
            }
            catch { }

            return Pos;
        }
        public static double CWPos
        {
            set
            {
                if (!ZEC3002.Ctrl.BoardOpened(ElevIO.CWAxis.BoardID)) return;
                try
                {
                    ZEC3002.Ctrl.ZM3xx_WLPos(ElevIO.CWAxis, value);
                }
                catch { }
            }
            get
            {
                if (!ZEC3002.Ctrl.BoardOpened(ElevIO.CWAxis.BoardID)) return 0;
                double d = 0;

                try
                {
                    ZEC3002.Ctrl.ZM3xx_RLPos(ElevIO.CWAxis, ref d);
                }
                catch { }
                return d;
            }
        }

        public static bool SetMotionParam(ref ZEC3002.Ctrl.TAxis Axis, double StartV, double DriveV, double Accel)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return false; }

            ZEC3002.Ctrl.ZM3xx_UpdateMotorParaRange(ref Axis);
            string EMsg = ClassName + ".SetMotionParam " + Axis.Name + " - Fail" + (char)13;

            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {
                try
                {
                    #region
                    //if ((StartV != Axis.MotorPara.PsntStartV) || (DriveV != Axis.MotorPara.PsntSpeed) || (Accel != Axis.MotorPara.PsntAccel))
                    {
                        //Axis.MotorPara.PsntAccel = Accel;
                        //Axis.MotorPara.PsntStartV = StartV;
                        //Axis.MotorPara.PsntSpeed = DriveV;
                        if (!ZEC3002.Ctrl.ZM3xx_SetMotionParam(ref Axis, StartV, DriveV, Accel)) { return false; }
                    }
                    #endregion
                }
                catch (Exception Ex)
                {
                    string msg = EMsg + (char)13 + Ex.Message;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_SET_MOTION_PARAM_ERR, msg);
                    return false;
                }
            }
            return true;
        }
        public static bool SetMotionParam(ref ZEC3002.Ctrl.TAxis Axis)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return false; }

            ZEC3002.Ctrl.ZM3xx_UpdateMotorParaRange(ref Axis);
            string EMsg = ClassName + ".SetMotionParam " + Axis.Name + " - Fail" + (char)13;

            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {
                try
                {
                    #region
                    if ((Axis.MotorPara.StartV != Axis.MotorPara.PsntStartV) ||
                        (Axis.MotorPara.FastV != Axis.MotorPara.PsntSpeed) ||
                        (Axis.MotorPara.Accel != Axis.MotorPara.PsntAccel))
                    {
                        if (!ZEC3002.Ctrl.ZM3xx_SetMotionParam(ref Axis, (uint)Axis.MotorPara.StartV, (uint)Axis.MotorPara.FastV, (uint)Axis.MotorPara.Accel)) { return false; }
                    }

                    Axis.MotorPara.PsntAccel = Axis.MotorPara.Accel;
                    Axis.MotorPara.PsntStartV = Axis.MotorPara.StartV;
                    Axis.MotorPara.PsntSpeed = Axis.MotorPara.FastV;
                    #endregion
                }
                catch (Exception Ex)
                {
                    string msg = EMsg + (char)13 + Ex.Message;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_SET_MOTION_PARAM_ERR, msg);
                    return false;
                }
            }

            return true;
        }
        public static bool ForceStop(ZEC3002.Ctrl.TAxis Axis)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return false; }

            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {
                try
                {
                    ZEC3002.Ctrl.ZM3xx_ForceStop(Axis);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    return false;
                }
            }
            return true;
        }

        internal static bool CheckLZAlarm()
        {
            if (ElevIO.MtrAlarm(ElevIO.LZAxis))
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_MOTOR_ALARM, "LZ", EMsgBtn.smbOK);
                return true;
            }
            return false;
        }
        internal static bool CheckRZAlarm()
        {
            if (ElevIO.MtrAlarm(ElevIO.RZAxis))
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_MOTOR_ALARM, "RZ", EMsgBtn.smbOK);
                return true;
            }
            return false;
        }
        internal static bool CheckCWAlarm()
        {
            if (ElevIO.MtrAlarm(ElevIO.CWAxis))
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_MOTOR_ALARM, "CW", EMsgBtn.smbOK);
                return true;
            }
            return false;
        }
        private static double d_LZToMovePos = 0;
        private static double d_RZToMovePos = 0;
        public static bool LZMove(double Pos)
        {
            if (!ZEC3002.Ctrl.BoardOpened(ElevIO.LZAxis.BoardID)) { return false; }

            if (CheckLZAlarm()) return false;

            #region LZMove
            try
            {
                d_LZToMovePos = Pos;
                if (!ZEC3002.Ctrl.ZM3xx_MovePtpAbs1(ElevIO.LZAxis, Pos)) { return false; }

                double SpeedRatio = (double)ElevIO.LZAxis.MotorPara.Home.FastV / ElevIO.LZAxis.MotorPara.FastV;
                int TimeOut = (int)(ElevIO.LZAxis.MotorPara.Home.TimeOut * SpeedRatio);
                TimeOut = (int)((double)TimeOut * 1.25);
                if (!LZWait(TimeOut)) { return false; }
            }
            catch (Exception Ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_MOVE_POS_ERROR, "LZ" + Ex.Message.ToString(), EMsgBtn.smbOK);
                return false;
            }
            #endregion

            if (CheckLZAlarm()) return false;
            return true;
        }
        public static bool RZMove(double Pos)
        {
            if (!ZEC3002.Ctrl.BoardOpened(ElevIO.RZAxis.BoardID)) { return false; }

            if (CheckRZAlarm()) return false;

            #region RZ Move
            try
            {
                d_RZToMovePos = Pos;
                if (!ZEC3002.Ctrl.ZM3xx_MovePtpAbs1(ElevIO.RZAxis, Pos)) { return false; }

                double SpeedRatio = (double)ElevIO.RZAxis.MotorPara.Home.FastV / ElevIO.RZAxis.MotorPara.FastV;
                int TimeOut = (int)(ElevIO.RZAxis.MotorPara.Home.TimeOut * SpeedRatio);
                TimeOut = (int)((double)TimeOut * 1.25);
                if (!RZWait(TimeOut)) { return false; }
            }
            catch (Exception Ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_MOVE_POS_ERROR, "RZ" + Ex.Message.ToString(), EMsgBtn.smbOK);
                return false;
            }
            #endregion

            if (CheckRZAlarm()) return false;
            return true;
        }
        public static bool AxisWait(ZEC3002.Ctrl.TAxis Axis, int TimeOut)
        {
            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return false; }
            string EMsg = "[IOCtrl] Board ID" + Axis.BoardID.ToString() + " " + Axis.Name + " - Axis Wait Fail" + (char)13;

            try
            {
                if (ZEC3002.Ctrl.ZM3xx_AxisWait(Axis, TimeOut))
                {
                    #region Check Alarm
                    if (Axis.AxisID == 1)
                    {
                        if (ElevIO.MtrAlarm(ElevIO.LZAxis))//ElevIO.MtrAlarm(ElevIO.LZAxis))
                        {
                            ForceStop(Axis);
                            string msg = EMsg + "LZ - Motor Alarm";
                            Msg MsgBox = new Msg();
                            MsgBox.Show(Messages.ELEV_AXIS_WAIT_ERR, msg);
                            return false;
                        }
                    }
                    if (Axis.AxisID == 3)
                    {
                        if (ElevIO.MtrAlarm(ElevIO.RZAxis))//ElevIO.MtrAlarm(ElevIO.RZAxis))
                        {
                            ForceStop(Axis);
                            string msg = EMsg + "RZ - Motor Alarm";
                            Msg MsgBox = new Msg();
                            MsgBox.Show(Messages.ELEV_AXIS_WAIT_ERR, msg, EMsgBtn.smbOK);
                            return false;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                ForceStop(Axis);
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                return false;
            }

            return true;
        }

        public static bool LZWait(int TimeOut)
        {
            if (!ZEC3002.Ctrl.BoardOpened(ElevIO.LZAxis.BoardID)) { return false; }
            string EMsg = "[IOCtrl] Board ID" + ElevIO.LZAxis.BoardID.ToString() + " " + ElevIO.LZAxis.Name + " - LZWait Fail" + (char)13;

            int i_TimeOut = TimeOut > 0 ? TimeOut : 1000;

            try
            {
                int t = Environment.TickCount + i_TimeOut;
                while (true)
                {
                _Continue:
                    if (!ZEC3002.Ctrl.ZM3xx_AxisBusy(ElevIO.LZAxis)) break;
                    if (Environment.TickCount > t)
                    {
                        ForceStop(ElevIO.LZAxis);
                        string msg = EMsg + "LZ - Motor Alarm";
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.ELEV_AXIS_WAIT_ERR, msg);
                        return false;
                    }

                    if (Setups[(int)TElevator.Left].EnableDoorSens == (int)EDoorSens.ForceStop)
                    {
                        if (!Left.DoorIsClosed(false))
                        {
                            ForceStop(ElevIO.LZAxis);
                            Msg MsgBox = new Msg();
                        _Retry:
                            EMsgRes Res = MsgBox.Show(Messages.LEFT_ELEV_DOOR_OPEN, "", EMsgBtn.smbRetry_Cancel);
                            if (TimeOut > 0 && Res == EMsgRes.smrRetry)
                            {
                                if (!Left.DoorIsClosed(false)) goto _Retry;
                                if (!ZEC3002.Ctrl.ZM3xx_MovePtpAbs1(ElevIO.LZAxis, d_LZToMovePos)) return false;
                                goto _Continue;
                            }
                            ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                            return false;
                        }
                    }

                    if (Left.PusherValid && !Left.SensPusherHome)
                    {
                        ForceStop(ElevIO.LZAxis);
                        Msg MsgBox = new Msg();
                    _Retry:
                        EMsgRes Res = MsgBox.Show(Messages.ELEV_PUSHER_NOT_HOME_ERROR, "", EMsgBtn.smbRetry_Cancel);
                        if (TimeOut > 0 && Res == EMsgRes.smrRetry)
                        {
                            if (!Left.SensPusherHome) goto _Retry;
                            if (!ZEC3002.Ctrl.ZM3xx_MovePtpAbs1(ElevIO.LZAxis, d_LZToMovePos)) return false;
                            goto _Continue;
                        }
                        ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                        return false;
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception Ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                return false;
            }
            return true;
        }
        public static bool RZWait(int TimeOut)
        {
            if (!ZEC3002.Ctrl.BoardOpened(ElevIO.RZAxis.BoardID)) { return false; }
            string EMsg = "[IOCtrl] Board ID" + ElevIO.RZAxis.BoardID.ToString() + " " + ElevIO.RZAxis.Name + " - RZWait Fail" + (char)13;

            int i_TimeOut = TimeOut > 0 ? TimeOut : 1000;

            try
            {
                int t = Environment.TickCount + i_TimeOut;
                while (true)
                {
                _Continue:
                    if (!ZEC3002.Ctrl.ZM3xx_AxisBusy(ElevIO.RZAxis)) break;
                    if (Environment.TickCount > t)
                    {
                        ForceStop(ElevIO.RZAxis);
                        string msg = EMsg + "RZ - Motor Alarm";
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.ELEV_AXIS_WAIT_ERR, msg);
                        return false;
                    }

                    if (Setups[(int)TElevator.Right].EnableDoorSens == (int)EDoorSens.ForceStop)
                    {
                        if (!Right.DoorIsClosed(false))
                        {
                            ForceStop(ElevIO.RZAxis);
                            Msg MsgBox = new Msg();
                        _Retry:
                            EMsgRes Res = MsgBox.Show(Messages.RIGHT_ELEV_DOOR_OPEN, "", EMsgBtn.smbRetry_Cancel);
                            if (TimeOut > 0 && Res == EMsgRes.smrRetry)
                            {
                                if (!Left.DoorIsClosed(false)) goto _Retry;
                                if (!ZEC3002.Ctrl.ZM3xx_MovePtpAbs1(ElevIO.RZAxis, d_RZToMovePos)) return false;
                                goto _Continue;
                            }
                            ElevStatus[(int)TElevator.Right] = EElevStatus.ErrorInit;
                            return false;
                        }
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception Ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                return false;
            }
            return true;
        }
        public static bool SetLPos(ZEC3002.Ctrl.TAxis Axis, int Pos)
        {

            if (!ZEC3002.Ctrl.BoardOpened(Axis.BoardID)) { return false; }
            string EMsg = "[IOCtrl] Board ID" + Axis.BoardID.ToString() + " " + Axis.Name + " - Set LPos Fail" + (char)13;

            if (Axis.DIOModel == ZEC3002.Ctrl.TDIOModel.ZM324)
            {
                try
                {
                    ZEC3002.Ctrl.ZM3xx_WLPos(Axis, Pos);
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, Ex.Message, EMsgBtn.smbOK);
                    return false;
                }
            }
            return true;
        }

        public static double GetLastPos(int ElevIdx, int MagNo)
        {
            double Total = Setups[ElevIdx].LevelPitch * (Setups[ElevIdx].LevelCount - 1);
            double LastPos = Setups[ElevIdx].Mag1stLevelPos[MagNo] + Total;
            return LastPos;
        }

        public static bool CWMove(double Pos)
        {
            if (!ZEC3002.Ctrl.BoardOpened(ElevIO.RZAxis.BoardID)) { return false; }

            if (CheckCWAlarm()) return false;

            #region
            try
            {
                if (!ZEC3002.Ctrl.ZM3xx_MovePtpAbs1(ElevIO.CWAxis, Pos)) { return false; }

                //double SpeedRatio = (double)ElevIO.CWAxis.MotorPara.Home.FastV / ElevIO.CWAxis.MotorPara.FastV;
                //int TimeOut = (int)(ElevIO.CWAxis.MotorPara.Home.TimeOut * SpeedRatio);
                //TimeOut = (int)((double)TimeOut * 1.25);
                //if (!CWWait(TimeOut)) { return false; }

                int t = Environment.TickCount + ElevIO.CWAxis.MotorPara.Home.TimeOut;
                while (true)
                {
                    if (!ZEC3002.Ctrl.ZM3xx_AxisBusy(ElevIO.CWAxis)) break;
                    if (Environment.TickCount > t)
                    {
                        ForceStop(ElevIO.CWAxis);
                        return false;
                    }
                    Thread.Sleep(1);
                }
            }
            catch (Exception Ex)
            {
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.ELEV_MOVE_POS_ERROR, "CW" + Ex.Message.ToString(), EMsgBtn.smbOK);
                return false;
            }
            #endregion

            if (CheckCWAlarm()) return false;
            return true;
        }

        public static bool Init()
        {
            try
            {
                if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                {
                    if (!Left.Init()) { return false; }
                }
                if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ)
                {
                    if (!Right.Init()) { return false; }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return true;
        }
        public static bool Ready
        {
            get
            {
                if (!Left.Ready) { return false; }
                if (!Right.Ready) { return false; }
                return true;
            }
        }

        public static class Left
        {
            public static EElevStatus Status
            {
                get
                {
                    return ElevStatus[(int)TElevator.Left];
                }
                set
                {
                    ElevStatus[(int)TElevator.Left] = value;
                }
            }
            public static TSetup Setup = TaskElev.Setups[(int)TElevator.Left];
            public static bool b_MagChanged = false;

            public static bool Ready
            {
                get
                {
                    if (TaskConv.LeftMode == TaskConv.ELeftMode.ElevatorZ)
                    {
                        return (ElevStatus[(int)TElevator.Left] == EElevStatus.Ready);
                    }
                    return true;
                }
            }
            public static double Pos
            {
                get
                {
                    return TaskElev.Pos(ElevIO.LZAxis);
                }
            }

            public static bool SensLZHome
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ElevIO.SensLZHome);
                }
            }

            public static bool Init()
            {
                string EMsg = "Left Elevator Init" + (char)13;
                try
                {
                    if (!ZEC3002.Ctrl.BoardOpened(ElevIO.BoardID))
                        if (!TaskElev.OpenBoard(ElevIO.BoardID)) goto _Error;

                    SendRetry = 0;
                    ReadyToSend = false;
                    TransferBusy = false;
                    WaitMagChange = false;

                    b_MagChanged = false;

                    Setup.PsntLevel = 0;
                    Setup.PsntMagz = 0;

                    if (!DoorIsClosed(true)) goto _Error;

                    if (!PusherRet()) { goto _Error; }
                    if (!SafetyCheck_ElevMove()) { goto _Error; }
                    #region LZ
                    if (ElevIO.LZAxis.MotorPara.Home.HomeDir == ZEC3002.Ctrl.THomeDir.P)
                    {
                        if (!Home(true)) { goto _Error; }
                    }
                    else
                    {
                        if (!Home(false)) { goto _Error; }
                    }
                    #endregion
                }
                catch (Exception Ex)
                {
                    EMsg = EMsg + Ex.Message;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, EMsg, EMsgBtn.smbOK);
                }

                ElevStatus[(int)TElevator.Left] = EElevStatus.Ready;
                return true;

            _Error:
                ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                return false;
            }
            private static bool Home(bool HomeP)
            {
                string EMsg = "";
                if (HomeP) EMsg = "LZHome_P" + (char)13; else EMsg = "LZHome_N" + (char)13;

                ZEC3002.Ctrl.ZM3xx_UpdateMotorParaRange(ref ElevIO.LZAxis);
                ZEC3002.Ctrl.ZM3xx_UpdateMotorParaRange(ref ElevIO.LPAxis);
                ForceStop(ElevIO.LZAxis);

            _Retry:
                if (!DoorIsClosed(true)) goto _Error;

                ElevIO.MtrOnOff(ref ElevIO.LZAxis, false);
                if (!Delay(300)) { goto _Error; }
                ElevIO.MtrOnOff(ref ElevIO.LZAxis, true);
                if (!Delay(300)) { goto _Error; }

                if (CheckLZAlarm()) goto _Error;

                try
                {
                    #region Search Home
                    int t = Environment.TickCount + ElevIO.LZAxis.MotorPara.Home.TimeOut;
                    if (!Left.SensLZHome)
                    {
                        if (!SetMotionParam(ref ElevIO.LZAxis, 1, ElevIO.LZAxis.MotorPara.Home.FastV, 100)) { goto _Error; }
                        if (HomeP) JogP(ElevIO.LZAxis); else JogN(ElevIO.LZAxis);

                        while (true)
                        {
                            if (Left.SensLZHome)
                            {
                                break;
                            }
                            Thread.Sleep(5);
                            if (CheckLZAlarm()) goto _Error;
                            if (Environment.TickCount >= t)
                            {
                                if (!ForceStop(ElevIO.LZAxis)) { goto _Error; }
                                Msg MsgBox = new Msg();
                                MsgBox.Show(Messages.ELEV_HOME_TIME_OUT, EMsg, EMsgBtn.smbOK);
                                goto _Error;
                            }
                            if (Setups[(int)TElevator.Left].EnableDoorSens == (int)EDoorSens.ForceStop)
                            {
                                if (!Left.DoorIsClosed(false))
                                {
                                    ForceStop(ElevIO.LZAxis);
                                    Msg MsgBox = new Msg();
                                    EMsgRes Res = MsgBox.Show(Messages.LEFT_ELEV_DOOR_OPEN, "", EMsgBtn.smbOK_Retry);
                                    if (Res == EMsgRes.smrRetry) goto _Retry;
                                    ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                                    return false;
                                }
                            }
                            if (PusherValid && !Left.SensPusherHome)
                            {
                                ForceStop(ElevIO.LZAxis);
                                Msg MsgBox = new Msg();
                                EMsgRes Res = MsgBox.Show(Messages.ELEV_PUSHER_NOT_HOME_ERROR, "", EMsgBtn.smbOK_Retry);
                                if (Res == EMsgRes.smrRetry) goto _Retry;
                                ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                                return false;
                            }
                        }
                        if (!ForceStop(ElevIO.LZAxis)) { goto _Error; }
                        if (!LZWait(0)) { return false; }
                        if (!Delay(100)) { goto _Error; }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + ex.Message, EMsgBtn.smbOK);
                    goto _Error;
                }

                try
                {
                    #region Clear Home
                    int t = Environment.TickCount + ElevIO.LZAxis.MotorPara.Home.TimeOut;
                    if (Left.SensLZHome)
                    {
                        if (!SetMotionParam(ref ElevIO.LZAxis, 1, ElevIO.LZAxis.MotorPara.Home.FastV, 100)) { goto _Error; }
                        if (HomeP) JogN(ElevIO.LZAxis); else JogP(ElevIO.LZAxis);

                        while (true)
                        {
                            if (!Left.SensLZHome) { break; }
                            Thread.Sleep(5);
                            if (CheckLZAlarm()) goto _Error;
                            if (Environment.TickCount >= t)
                            {
                                if (!ForceStop(ElevIO.LZAxis)) { goto _Error; }
                                Msg MsgBox = new Msg();
                                MsgBox.Show(Messages.ELEV_HOME_TIME_OUT, EMsg, EMsgBtn.smbOK);
                                goto _Error;
                            }
                            if (Setups[(int)TElevator.Left].EnableDoorSens == (int)EDoorSens.ForceStop)
                            {
                                if (!Left.DoorIsClosed(false))
                                {
                                    ForceStop(ElevIO.LZAxis);
                                    Msg MsgBox = new Msg();
                                    EMsgRes Res = MsgBox.Show(Messages.LEFT_ELEV_DOOR_OPEN, "", EMsgBtn.smbOK);
                                    if (Res == EMsgRes.smrRetry) goto _Retry;
                                    ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                                    return false;
                                }
                            }
                            if (PusherValid && !Left.SensPusherHome)
                            {
                                ForceStop(ElevIO.LZAxis);
                                Msg MsgBox = new Msg();
                                EMsgRes Res = MsgBox.Show(Messages.ELEV_PUSHER_NOT_HOME_ERROR, "", EMsgBtn.smbOK_Retry);
                                if (Res == EMsgRes.smrRetry) goto _Retry;
                                ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                                return false;
                            }
                        }
                        if (!ForceStop(ElevIO.LZAxis)) { goto _Error; }
                        if (!LZWait(0)) { return false; }
                        if (!Delay(100)) { goto _Error; }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message);
                    goto _Error;
                }

                try
                {
                    #region Touch Home
                    int t = Environment.TickCount + ElevIO.LZAxis.MotorPara.Home.TimeOut;
                    if (!Left.SensLZHome)
                    {
                        if (!SetMotionParam(ref ElevIO.LZAxis, 1, 10, 100)) { goto _Error; }
                        if (HomeP) JogP(ElevIO.LZAxis); else JogN(ElevIO.LZAxis);

                        while (true)
                        {
                            if (Left.SensLZHome)
                            {
                                break;
                            }
                            Thread.Sleep(5);
                            if (CheckLZAlarm()) goto _Error;
                            if (Environment.TickCount >= t)
                            {
                                if (!ForceStop(ElevIO.LZAxis)) { goto _Error; }
                                Msg MsgBox = new Msg();
                                MsgBox.Show(Messages.ELEV_HOME_TIME_OUT, EMsg, EMsgBtn.smbOK);
                                goto _Error;
                            }
                        }
                        if (!ForceStop(ElevIO.LZAxis)) { goto _Error; }
                        if (!LZWait(0)) { return false; }
                        if (!Delay(100)) { goto _Error; }
                    }
                    #endregion;
                }
                catch (Exception ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message);
                    goto _Error;
                }

                if (CheckLZAlarm()) goto _Error;

                #region Set Param
                if (!SetLPos(ElevIO.LZAxis, 0)) { goto _Error; }
                if (!SetMotionParam(ref ElevIO.LZAxis)) { goto _Error; }
                #endregion

                return true;

            _Error:
                ForceStop(ElevIO.LZAxis);
                return false;
            }

            private static bool Delay(int msdelay)
            {
                if (msdelay <= 0) { return true; }
                int t = Environment.TickCount + msdelay;

                while (true)
                {
                    if (Environment.TickCount >= t) { break; }
                    Thread.Sleep(0);
                }

                return true;
            }
            private static bool Delay(double msdelay)
            {
                return Delay((int)msdelay);
            }

            public static bool MagDirUp
            {
                get
                {
                    ////int MaxMag = Setup[(int)TElevator.Left].MagCount;
                    ////int Magz1stPos = Setup[(int)TElevator.Left].Magz_1stLevel[0];
                    ////int MagzLastPos = Setup[(int)TElevator.Left].Magz_1stLevel[MaxMag - 1];
                    //return false;// (Magz1stPos > MagzLastPos);

                    //S 2.0.5.4
                    int MaxMag = (int)Setups[(int)TElevator.Left].MagCount;
                    if (MaxMag == 0) MaxMag = 1;
                    double Magz1stPos = Setups[(int)TElevator.Left].Mag1stLevelPos[0];
                    double MagzLastPos = Setups[(int)TElevator.Left].Mag1stLevelPos[MaxMag - 1];
                    return (Magz1stPos > MagzLastPos);
                    //S 2.0.5.4
                }
            }
            public static double MagCount
            {
                get
                {
                    return Setups[(int)TElevator.Left].MagCount;
                }
            }

            public static bool SensPusherHome
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ElevIO.LP_PusherHome);
                }
            }
            public static bool SensPusherLimit
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ElevIO.LP_PusherLmt);
                }
            }
            public static bool SensPusherJam
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ElevIO.LP_PusherJam);
                }
            }
            internal static bool PusherValid
            {
                get
                {
                    return Setups[(int)TElevator.Left].PusherType > 0;
                }
            }

            internal static bool CheckPusherLimit()
            {
                if (!PusherValid) return true;

                _Retry:
                if (!Left.SensPusherLimit)
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_SENS_LIMIT_ERROR, "", EMsgBtn.smbRetry_Cancel);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default: return false;
                    }
                }
                return true;
            }

            internal static bool HomeSensorFuncCheck()//return false if error
            {
            _RetryCheck:
                Left.OutPusherRun = true;
                Left.OutPusherRet = false;
                int t = Environment.TickCount + 1000;
                while (true)
                {
                    if (!Left.SensPusherHome) break;

                    Thread.Sleep(1);
                    if (Environment.TickCount > t)
                    {
                        Left.OutPusherRun = false;
                        Left.OutPusherRet = false;
                        goto _Error;
                    }
                }
                Left.OutPusherRun = true;
                Left.OutPusherRet = true;
                t = Environment.TickCount + 1000;
                while (true)
                {
                    if (Left.SensPusherHome) break;

                    Thread.Sleep(1);
                    if (Environment.TickCount > t)
                    {
                        Left.OutPusherRun = false;
                        Left.OutPusherRet = false;
                        goto _Error;
                    }
                }
                Delay(100);
                Left.OutPusherRun = false;
                Left.OutPusherRet = false;
                return true;

            _Error:
                Msg MsgBox = new Msg();
                EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_HOME_FUNCTIONAL_ERROR, "", EMsgBtn.smbRetry_Cancel);
                switch (MsgRes)
                {
                    case EMsgRes.smrRetry: goto _RetryCheck;
                    default: return false;
                }
            }
            public static bool OutPusherRun
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus status = value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo;
                    ZEC3002.Ctrl.SetDO(ref ElevIO.LP_PusherRun, status);
                }
                get
                {
                    return ElevIO.LP_PusherRun.Status;
                }
            }
            public static bool OutPusherRet
            {
                set
                {
                    ZEC3002.Ctrl.TDOStatus status = value ? ZEC3002.Ctrl.TDOStatus.Hi : ZEC3002.Ctrl.TDOStatus.Lo;
                    ZEC3002.Ctrl.SetDO(ref ElevIO.LP_PusherRev, status);
                }
                get
                {
                    return ElevIO.LP_PusherRev.Status;
                }
            }

            public enum EMethodResult { OK, Stop, Error }
            public static EMethodResult PusherExt(ref bool hold)
            {
                if (!PusherValid) { return EMethodResult.OK; }

                try
                {
                    //if (!CheckPusherHome()) goto _Error;

                    if (Setups[(int)TElevator.Left].PusherType != 3)
                        if (!CheckPusherLimit()) goto _Error;

                    int TOut = (int)Setups[(int)TElevator.Left].PusherTimeout;

                    switch (Setups[(int)TElevator.Left].PusherType)
                    {
                        case 0: break;//none
                        case 1:
                            #region Pusher With DC Motor
                            {
                            _Retry:
                                Left.OutPusherRun = true;
                                Left.OutPusherRet = false;

                                int t = Environment.TickCount + TOut;
                                while (true)
                                {
                                    bool ProductPsnt = TaskConv.In.SensPsnt;
                                    if (ProductPsnt)
                                    {
                                        if (Left.SensPusherHome)
                                        {
                                            Msg MsgBox = new Msg();
                                            EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_ABNORMAL_STATE);
                                            goto _Error;
                                        }

                                        SendRetry = 0;
                                        if (!Delay(Setups[(int)TElevator.Left].PusherExtDelay)) { goto _Error; }
                                        break;
                                    }
                                    if (!Left.SensPusherLimit)
                                    {
                                        if (Left.SensPusherHome)
                                        {
                                            Left.OutPusherRun = false;
                                            Left.OutPusherRet = false;

                                            Msg MsgBox = new Msg();
                                            EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_ABNORMAL_STATE);
                                            goto _Error;
                                        }
                                        break;
                                    }
                                    Thread.Sleep(1);

                                    if (Environment.TickCount >= t)
                                    {
                                        #region handle retract
                                        {
                                            Left.OutPusherRun = true;
                                            Left.OutPusherRet = true;
                                            int t_TimeOut = Environment.TickCount + 3000;
                                            while (!Left.SensPusherHome)
                                            {
                                                if (Environment.TickCount >= t_TimeOut) break;
                                                Thread.Sleep(1);
                                            }
                                            Left.OutPusherRun = false;
                                            Left.OutPusherRet = false;
                                        }
                                        #endregion

                                        Msg MsgBox = new Msg();
                                        EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_EXT_TIME_OUT, "", EMsgBtn.smbRetry_Stop);
                                        switch (MsgRes)
                                        {
                                            case EMsgRes.smrRetry: goto _Retry;
                                            default: goto _Stop;
                                        }
                                    }
                                }

                                Left.OutPusherRun = false;
                                Left.OutPusherRet = false;

                                if (Left.SensPusherHome)
                                {
                                    Msg MsgBox = new Msg();
                                    EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_ABNORMAL_STATE);
                                    goto _Error;
                                }

                                break;
                            }
                        #endregion
                        case 2: //LP_Axis Stepper Motor
                        case 3: //LP_Axis Stepper Motor - With Jam Sensor
                            #region 
                            {
                            _Retry:
                                MoveConst(ElevIO.LPAxis, 1000, ElevIO.LPAxis.MotorPara.SlowV);
                                int t = Environment.TickCount + TOut;
                                while (true)
                                {
                                    bool ProductPsnt = TaskConv.In.SensPsnt;
                                    if (ProductPsnt)
                                    {
                                        if (Left.SensPusherHome)
                                        {
                                            TaskConv.Conv.Stop();
                                            PusherStop();

                                            Msg MsgBox = new Msg();
                                            EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_ABNORMAL_STATE);
                                            goto _Error;
                                        }

                                        SendRetry = 0;
                                        if (!Delay(Setups[(int)TElevator.Left].PusherExtDelay)) { goto _Error; }
                                        break;
                                    }

                                    if (Setups[(int)TElevator.Left].PusherType == 2)
                                    {
                                        if (!Left.SensPusherLimit)
                                        {
                                            if (Left.SensPusherHome)
                                            {
                                                TaskConv.Conv.Stop();
                                                PusherStop();

                                                Msg MsgBox = new Msg();
                                                EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_ABNORMAL_STATE);
                                                goto _Error;
                                            }
                                            break;
                                        }
                                    }
                                    if (Setups[(int)TElevator.Left].PusherType == 3)
                                    {
                                        if (Left.SensPusherLimit) break;

                                        if (Left.SensPusherJam)
                                        {
                                            TaskConv.Conv.Stop();
                                            PusherStop();
                                            #region handle retract
                                            {
                                                MoveConst(ElevIO.LPAxis, -1000, ElevIO.LPAxis.MotorPara.FastV);
                                                int t_TimeOut = Environment.TickCount + 3000;
                                                while (!Left.SensPusherHome)
                                                {
                                                    if (Environment.TickCount >= t_TimeOut) break;
                                                    Thread.Sleep(1);
                                                }
                                                PusherStop();
                                            }
                                            #endregion

                                            hold = true;
                                            //EMsgBtn msgBtn = EMsgBtn.smbRetry_Stop;
                                            //if (TaskConv.In.SensPsnt) msgBtn = EMsgBtn.smbStop;
                                            Msg MsgBox = new Msg();
                                            EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_JAM, "", EMsgBtn.smbRetry_Stop);
                                            hold = false;
                                            switch (MsgRes)
                                            {
                                                case EMsgRes.smrRetry:
                                                    if (TaskConv.In.SensPsnt)
                                                    {
                                                        return EMethodResult.OK;
                                                    }
                                                    else
                                                    {
                                                        //TaskConv.Conv.Fwd_Fast();
                                                        if (Setup.PusherRunConv) TaskConv.Conv.Fwd_Fast();
                                                        goto _Retry;
                                                    }
                                                default: goto _Stop;
                                            }
                                        }
                                    }
                                    Thread.Sleep(5);

                                    if (Environment.TickCount >= t)
                                    {
                                        TaskConv.Conv.Stop();
                                        PusherStop();
                                        #region handle retract
                                        {
                                            MoveConst(ElevIO.LPAxis, -1000, ElevIO.LPAxis.MotorPara.FastV);
                                            int t_TimeOut = Environment.TickCount + 3000;
                                            while (!Left.SensPusherHome)
                                            {
                                                if (Environment.TickCount >= t_TimeOut) break;
                                                Thread.Sleep(1);
                                            }
                                            PusherStop();
                                        }
                                        #endregion
                                        hold = true;
                                        Msg MsgBox = new Msg();
                                        EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_EXT_TIME_OUT, "", EMsgBtn.smbRetry_Stop);
                                        switch (MsgRes)
                                        {
                                            case EMsgRes.smrRetry:
                                                //TaskConv.Conv.Fwd_Fast();
                                                if (Setup.PusherRunConv) TaskConv.Conv.Fwd_Fast();
                                                goto _Retry;
                                            default: goto _Stop;
                                        }
                                    }
                                }
                                PusherStop();
                                break;
                            }
                            #endregion
                    }
                }
                catch (Exception Ex)
                {
                    #region ErrorMsg
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                    #endregion
                }

                return EMethodResult.OK;

            _Stop:
                //return true;
                return EMethodResult.Stop;

            _Error:
                //return false;
                return EMethodResult.Error;
            }
            public static bool PusherRet()
            {
                if (!PusherValid) { return true; }

                string EMsg = "Left PusherRet ";
            _Retry:
                try
                {
                    int TOut = (int)Setups[(int)TElevator.Left].PusherTimeout;

                    switch (Setups[(int)TElevator.Left].PusherType)
                    {
                        case 0: break;//none
                        case 1://Pusher With DC Motor
                            {
                                bool bAbnormalPusher = false;
                                bAbnormalPusher = !Left.SensPusherLimit && Left.SensPusherHome;
                                if (bAbnormalPusher)
                                {
                                    Msg MsgBox = new Msg();
                                    EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_ABNORMAL_STATE);
                                    return false;
                                }

                                Left.OutPusherRun = true;
                                Left.OutPusherRet = true;

                                int t = Environment.TickCount + TOut;
                                while (true)
                                {
                                    if (Left.SensPusherHome) break;

                                    Thread.Sleep(1);
                                    if (Environment.TickCount > t)
                                    {
                                        Left.OutPusherRun = false;
                                        Left.OutPusherRet = false;
                                        Msg MsgBox = new Msg();
                                        EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_RET_TIME_OUT, "", EMsgBtn.smbRetry_Cancel);
                                        switch (MsgRes)
                                        {
                                            case EMsgRes.smrRetry: goto _Retry;
                                            default: goto _Error;
                                        }
                                    }
                                }

                                Left.OutPusherRun = false;
                                Left.OutPusherRet = false;

                                if (!Delay(Setups[(int)TElevator.Left].PusherRetDelay)) { goto _Error; }
                                if (!HomeSensorFuncCheck()) goto _Error;
                                break;
                            }
                        case 2://Type 2 LP_Axis Stepper Motor
                        case 3: //Type 2 LP_Axis Stepper Motor - With Jam Sensor
                            #region
                            {
                                if (Setups[(int)TElevator.Left].PusherType == 2)
                                {
                                    bool bAbnormalPusher = false;
                                    bAbnormalPusher = !Left.SensPusherLimit && Left.SensPusherHome;
                                    if (bAbnormalPusher)
                                    {
                                        Msg MsgBox = new Msg();
                                        EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_ABNORMAL_STATE);
                                        goto _Error;
                                    }
                                }

                                MoveConst(ElevIO.LPAxis, -1000, ElevIO.LPAxis.MotorPara.FastV);
                                int t = Environment.TickCount + TOut;
                                while (true)
                                {
                                    if (Left.SensPusherHome) break;
                                    if (Environment.TickCount > t)
                                    {
                                        Thread.Sleep(5);
                                        PusherStop();
                                        Msg MsgBox = new Msg();
                                        EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_RET_TIME_OUT, "", EMsgBtn.smbRetry_Cancel);
                                        switch (MsgRes)
                                        {
                                            case EMsgRes.smrRetry: goto _Retry;
                                            default: goto _Error;
                                        }
                                    }
                                }
                                Thread.Sleep(10);
                                PusherStop();

                                if (!Delay(Setups[(int)TElevator.Left].PusherRetDelay)) { goto _Error; }
                                if (!SetLPos(ElevIO.LPAxis, 0)) { goto _Error; }
                                break;
                            }
                            #endregion
                    }
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }

                return true;
            _Error:
                return false;
            }
            public static bool PusherStop() // Axis Control
            {
                if (!PusherValid) { return true; }

                if (!ForceStop(ElevIO.LPAxis)) return false;
                return true;
            }
            public static bool PusherHome()
            {
                if (!PusherValid) return true;

                try
                {
                    if (!Left.SensPusherHome) return PusherRet();
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    return false;
                }
                return true;
            }

            public static bool SensMagPsnt(int MagNo)
            {
                switch (MagNo)
                {
                    default:
                    case 1: return ZEC3002.Ctrl.GetDI(ref ElevIO.Left_SensMagPsnt1);
                    case 2: return ZEC3002.Ctrl.GetDI(ref ElevIO.Left_SensMagPsnt2);
                    case 3: return ZEC3002.Ctrl.GetDI(ref ElevIO.Left_SensMagPsnt3);
                    case 4: return ZEC3002.Ctrl.GetDI(ref ElevIO.Left_SensMagPsnt4);
                }
            }
            public static bool SensMagNoPsnt(int MagNo)
            {
                bool SensMagzPsnt = false;
                int LastMagz = (int)Setups[(int)TElevator.Left].MagCount;
                double Magz1stPos = Setups[(int)TElevator.Left].Mag1stLevelPos[0];
                double MagzLastPos = Setups[(int)TElevator.Left].Mag1stLevelPos[LastMagz - 1];
                int MagCount = (int)Setups[(int)TElevator.Left].MagCount;
                try
                {
                    bool DirUp = (Magz1stPos >= MagzLastPos);
                    if (DirUp)
                    {
                        #region Up
                        if (MagNo == 1)
                        {
                            SensMagzPsnt = SensMagPsnt(1);
                            //SensMagzPsnt = ZEC3002.Ctrl.GetDI(ref DI._LEFT_SensMagzPsnt1);
                        }
                        if (MagNo == 2)
                        {
                            SensMagzPsnt = SensMagPsnt(2);
                        }
                        if (MagNo == 3)
                        {
                            SensMagzPsnt = SensMagPsnt(3);
                        }
                        if (MagNo == 4)
                        {
                            SensMagzPsnt = SensMagPsnt(4);
                        }
                        #endregion
                    }
                    else
                    {
                        #region Down
                        switch (MagCount)
                        {
                            case 4: //4 magazine 
                                {
                                    if (MagNo == 1)
                                    {
                                        SensMagzPsnt = SensMagPsnt(4);
                                    }
                                    if (MagNo == 2)
                                    {
                                        SensMagzPsnt = SensMagPsnt(3);
                                    }
                                    if (MagNo == 3)
                                    {
                                        SensMagzPsnt = SensMagPsnt(2);
                                    }
                                    if (MagNo == 4)
                                    {
                                        SensMagzPsnt = SensMagPsnt(1);
                                    }
                                    break;
                                }
                            case 3: //4 magazine 
                                {
                                    if (MagNo == 1)
                                    {
                                        SensMagzPsnt = SensMagPsnt(3);
                                    }
                                    if (MagNo == 2)
                                    {
                                        SensMagzPsnt = SensMagPsnt(2);
                                    }
                                    if (MagNo == 3)
                                    {
                                        SensMagzPsnt = SensMagPsnt(1);
                                    }
                                    break;
                                }
                            case 2: //4 magazine 
                                {
                                    if (MagNo == 1)
                                    {
                                        SensMagzPsnt = SensMagPsnt(2);
                                    }
                                    if (MagNo == 2)
                                    {
                                        SensMagzPsnt = SensMagPsnt(1);
                                    }
                                    break;
                                }
                            case 1: //4 magazine 
                                {
                                    if (MagNo == 1)
                                    {
                                        SensMagzPsnt = SensMagPsnt(1);
                                    }
                                    break;
                                }
                        }
                        #endregion
                    }
                }
                catch { };
                return SensMagzPsnt;
            }
            public static bool SafetyCheck_ElevMove(bool bRetry = true)
            {
                try
                {
                    if (!DoorIsClosed(true)) goto _Error;

                    if (!Left.SensPusherHome)
                    {
                        Msg MsgBox = new Msg();
                        MsgBox.Show(Messages.ELEV_PUSHER_SENS_HOME_ERROR, "ElevMove", EMsgBtn.smbOK);
                        goto _Error;
                    }

                    EMsgBtn msgBtn = bRetry ? EMsgBtn.smbRetry_Cancel : EMsgBtn.smbCancel;
                _Retry:
                    if (TaskConv.In.SensPsnt)
                    {
                        string msg = "Product Present at Conveyor In.";
                        Msg MsgBox = new Msg();
                        EMsgRes Res = MsgBox.Show(Messages.CONV_IN_SENSOR_PSNT, msg, msgBtn);
                        switch (Res)
                        {
                            case EMsgRes.smrRetry:
                                goto _Retry;
                        }
                        goto _Error;
                    }
                _Retry1:
                    if (TaskConv.In.SensLFPsnt)
                    {
                        string msg = "Product Present between Magazine and Conveyor In.";
                        Msg MsgBox = new Msg();
                        EMsgRes Res = MsgBox.Show(Messages.CONV_IN_CLEAR_SENSOR_PSNT, msg, msgBtn);
                        switch (Res)
                        {
                            case EMsgRes.smrRetry:
                                goto _Retry1;
                        }
                        goto _Error;
                    }
                }
                catch (Exception Ex)
                {
                    string msg = Ex.Message.ToString();
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }

                return true;
            _Error:
                return false;
            }

            public static bool SensDoor
            {
                get
                {
                    //bool b = false;
                    //try
                    {
                        if (!ZEC3002.Ctrl.BoardOpened(ConvIO.BoardID)) return false;
                        return ZEC3002.Ctrl.GetDI(ref ElevIO.Left_SensDoor);
                    }
                    //catch (Exception Ex)
                    //{
                    //    throw new Exception("Left DoorSens " + Ex.Message.ToString());
                    //}
                    //return b;
                }
            }
            static DateTime dt_BypassUntil = DateTime.Now;
            public static int BypassDoorCheckTimer
            {
                set
                {
                    dt_BypassUntil = DateTime.Now.AddSeconds(value);
                }
                get
                {
                    TimeSpan ts = dt_BypassUntil.Subtract(DateTime.Now);
                    return (int)ts.TotalSeconds;
                }
            }
            public static bool DoorIsClosed(bool Prompt)
            {
                if (TaskConv.LeftMode != TaskConv.ELeftMode.ElevatorZ) return true;
                if (Setups[(int)TElevator.Left].EnableDoorSens == (int)EDoorSens.None) return true;

                if (BypassDoorCheckTimer > 0) return true;

                _Retry:
                try
                {
                    if (!SensDoor)
                    {
                        if (Prompt)
                        {
                            Msg MsgBox = new Msg();
                            EMsgRes Res = MsgBox.Show(Messages.LEFT_ELEV_DOOR_OPEN, "", EMsgBtn.smbRetry_Cancel);
                            if (Res == EMsgRes.smrRetry) goto _Retry;
                        }
                        return false;
                    }
                }
                catch (Exception Ex)
                {
                    throw new Exception("Left DoorCheck " + Ex.Message.ToString());
                }
                return true;
            }
            public static bool DoorCheck(bool Prompt)//reverse compatibility
            {
                return DoorIsClosed(Prompt);
            }

            public static bool MoveLevel(int MagNo, int LevelNo)
            {
                string EMsg = "LZMoveLevel";

                #region check mag and level limit
                if (MagNo <= 0) MagNo = 1;
                int MaxMag = (int)Setups[(int)TElevator.Left].MagCount;
                if (MagNo > MaxMag) MagNo = MaxMag;

                if (LevelNo <= 0) LevelNo = 1;
                int MaxLevel = (int)Setups[(int)TElevator.Left].LevelCount;
                if (LevelNo > MaxLevel) LevelNo = MaxLevel;
                #endregion

                try
                {
                    if (!SafetyCheck_ElevMove()) { goto _Error; }

                    #region Process
                    double LevelPitch = Setups[(int)TElevator.Left].LevelPitch;
                    double MovePos = Setups[(int)TElevator.Left].Mag1stLevelPos[MagNo - 1] + ((LevelNo - 1) * LevelPitch);

                    ElevStatus[(int)TElevator.Left] = EElevStatus.Busy;
                    if (!SetMotionParam(ref ElevIO.LZAxis)) { goto _Error; }
                    if (!LZMove(MovePos)) { goto _Error; }
                    ElevStatus[(int)TElevator.Left] = EElevStatus.Ready;
                    #endregion

                    Setups[(int)TElevator.Left].PsntMagz = MagNo;
                    Setups[(int)TElevator.Left].PsntLevel = LevelNo;

                    int TMag = (int)Setups[(int)TElevator.Left].MagCount;
                    int TLevel = (int)Setups[(int)TElevator.Left].LevelCount;

                    NUtils.RegistryWR RegRW = new NUtils.RegistryWR("Software");
                    RegRW.WriteKey("MHS\\Elev\\In", "MagNo", MagNo - 1);
                    RegRW.WriteKey("MHS\\Elev\\In", "LevelNo", LevelNo - 1);
                    RegRW.WriteKey("MHS\\Elev\\In", "TMag", TMag);
                    RegRW.WriteKey("MHS\\Elev\\In", "TLevel", TLevel);

                    Left.ReadyToSend = true;
                }
                catch (Exception Ex)
                {
                    ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }
                return true;

            _Error:
                ElevStatus[(int)TElevator.Left] = EElevStatus.ErrorInit;
                return false;
            }
            public static bool UpLevel()
            {
                int MagNo = Setups[(int)TElevator.Left].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Left].PsntLevel;

                double d_Pitch = Setups[(int)TElevator.Left].LevelPitch;

                if (d_Pitch > 0)
                {
                    if (LevelNo >= Setups[0].LevelCount)
                    {
                        if (MagNo <= 1)
                        {
                        }
                        else
                        {
                            MagNo--;
                            LevelNo = 1;
                        }
                    }
                    else
                        LevelNo++;
                }
                else
                {
                    if (LevelNo <= 1)
                    {
                        if (MagNo <= 1)
                        {
                        }
                        else
                        {
                            MagNo--;
                            LevelNo = (int)Setups[0].LevelCount;//1;
                        }
                    }
                    else
                        LevelNo--;
                }

                return MoveLevel(MagNo, LevelNo);
            }
            public static bool DnLevel()
            {
                int MagNo = Setups[(int)TElevator.Left].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Left].PsntLevel;

                double d_Pitch = Setups[(int)TElevator.Left].LevelPitch;

                if (d_Pitch > 0)
                {
                    if (LevelNo <= 1)
                    {
                        if (MagNo >= Setups[0].MagCount)
                        {
                        }
                        else
                        {
                            MagNo++;
                            LevelNo = (int)Setups[0].LevelCount;
                        }
                    }
                    else
                        LevelNo--;
                }
                else
                {
                    if (LevelNo >= Setups[0].LevelCount)
                    {
                        if (MagNo >= Setups[0].MagCount)
                        {
                        }
                        else
                        {
                            MagNo++;
                            LevelNo = 1;
                        }
                    }
                    else
                        LevelNo++;
                }

                return MoveLevel(MagNo, LevelNo);
            }
            public static bool MoveToNextLevel()
            {
                int MagNo = Setups[(int)TElevator.Left].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Left].PsntLevel;

                if (LevelNo >= Setups[0].LevelCount)
                {
                    LevelNo = 1;

                    if (MagNo >= Setups[0].MagCount)
                    {
                        MagNo = 1;
                    }
                    else
                    {
                        MagNo++;
                        if (TaskConv.OutLevelQtyFollowIn) b_MagChanged = true;
                    }
                }
                else
                    LevelNo++;

                return MoveLevel(MagNo, LevelNo);
            }
            public static bool PrevLevel()
            {
                int MagNo = Setups[(int)TElevator.Left].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Left].PsntLevel;

                if (LevelNo <= 1)
                {
                    LevelNo = (int)Setups[0].LevelCount;

                    if (MagNo <= 1)
                    {
                        MagNo = (int)Setups[0].MagCount;
                    }
                    else
                        MagNo--;
                }
                else
                    LevelNo--;

                return MoveLevel(MagNo, LevelNo);
            }

            public static bool MoveToLoadMagPos()
            {
                if (!SafetyCheck_ElevMove()) { goto _Error; }
                double Load_Pos = Setups[(int)TElevator.Left].MagLoadPos;

                ElevStatus[(int)TElevator.Left] = EElevStatus.Busy;
                if (!SetMotionParam(ref ElevIO.LZAxis)) { goto _Error; }
                if (!LZMove(Load_Pos)) { goto _Error; }
                ElevStatus[(int)TElevator.Left] = EElevStatus.Ready;

                Setups[(int)TElevator.Left].PsntLevel = 0;
                Setups[(int)TElevator.Left].PsntMagz = 0;

                if (TaskConv.OutLevelQtyFollowIn) b_MagChanged = true;

                return true;
            _Error:
                return false;
            }

            public static bool WaitMagChange = false;

            public static bool ReadyToSend = false;//product is ready to send
            public static bool TransferBusy = false;//product tranfer busy
            public static int SendRetry = 0;
            public static void RunLevel()//if not ready to kick move next level
            {
                //***Check Mode
                if (TaskConv.LeftMode != TaskConv.ELeftMode.ElevatorZ) goto _End;

                //***Check Status
                if (Status != EElevStatus.Ready) goto _End;
                Status = EElevStatus.Busy;

                if (WaitMagChange) goto _End;

                try
                {
                    if (TransferBusy) goto _End;

                    if (!ReadyToSend)
                    {
                        bool Psnt = TaskConv.In.SensPsnt;
                        if (Psnt) goto _End;

                        #region Handle Last Mag Last Level
                        if ((Setup.PsntLevel == Setup.LevelCount) && Setup.PsntMagz == Setup.MagCount)
                        {
                            WaitMagChange = true;
                            SendRetry = 0;
                            ReadyToSend = false;
                            TransferBusy = false;

                            if (!MoveToLoadMagPos()) { goto _Error; }

                            goto _End;
                        }
                        #endregion

                        #region Handle No Mag and Mag Last Level
                        if (Setup.PsntLevel == Setup.LevelCount || !SensMagNoPsnt(Setup.PsntMagz))
                        {
                        _CheckMag:
                            Setup.PsntMagz++;
                            Setup.PsntLevel = 1;
                            if (Setup.PsntMagz > 0)
                                if (TaskConv.OutLevelQtyFollowIn) b_MagChanged = true;

                            if (Setup.PsntMagz >= Setup.MagCount)
                            {
                                if (!SensMagNoPsnt(Setup.PsntMagz))
                                {
                                    WaitMagChange = true;
                                    SendRetry = 0;
                                    ReadyToSend = false;
                                    TransferBusy = false;

                                    if (!MoveToLoadMagPos()) { goto _Error; }

                                    goto _End;
                                }
                            }
                            if (SensMagNoPsnt(Setup.PsntMagz))
                            {
                                if (!MoveLevel(Setup.PsntMagz, Setup.PsntLevel)) { goto _Error; }
                                if (!SensMagNoPsnt(Setup.PsntMagz))
                                {
                                    ReadyToSend = false;
                                    goto _End;
                                }
                                ReadyToSend = true;
                                TransferBusy = false;
                                goto _End;
                            }
                            goto _CheckMag;
                        }
                        #endregion

                        #region Initial Level and Next Level
                        if (!MoveToNextLevel()) goto _Error;
                        if (!SensMagNoPsnt(Setup.PsntMagz))
                        {
                            ReadyToSend = false;
                            goto _End;
                        }
                        ReadyToSend = true;
                        TransferBusy = false;
                        goto _End;
                        #endregion
                    }
                }
                catch (Exception Ex)
                {
                    Status = EElevStatus.ErrorInit;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }
            _End:
                Status = EElevStatus.Ready;
                return;
            _Error:
                Status = EElevStatus.ErrorInit;
                return;
            }
            public static void RunLevel_BurnRun()//if not ready to kick move next level
            {
                //***Check Mode & Status
                if (TaskConv.LeftMode != TaskConv.ELeftMode.ElevatorZ) goto _End;
                if (Status != EElevStatus.Ready) goto _End;
                try
                {
                    bool Psnt = TaskConv.In.SensPsnt;
                    if (Psnt) goto _End;

                    Status = EElevStatus.Busy;

                    #region Handle Last Mag Last Level
                    if ((Setup.PsntLevel == Setup.LevelCount) && Setup.PsntMagz == Setup.MagCount)
                    {
                        if (!MoveToLoadMagPos()) { goto _Error; }
                        goto _End;
                    }
                    #endregion

                    #region Handle Mag Last Level
                    if (Setup.PsntLevel == Setup.LevelCount)
                    {
                        Setup.PsntMagz++;
                        Setup.PsntLevel = 1;
                        if (Setup.PsntMagz >= Setup.MagCount)
                        {
                            if (!MoveToLoadMagPos()) { goto _Error; }
                            goto _End;
                        }

                        if (!MoveLevel(Setup.PsntMagz, Setup.PsntLevel)) { goto _Error; }
                        goto _End;
                    }
                    #endregion

                    #region Initial Level and Next Level
                    if (!MoveToNextLevel()) goto _Error;
                    goto _End;
                    #endregion
                }
                catch (Exception Ex)
                {
                    Status = EElevStatus.ErrorInit;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }
            _End:
                Status = EElevStatus.Ready;
                return;
            _Error:
                Status = EElevStatus.ErrorInit;
                return;
            }
            public static Task TaskRunKick = null;
            public static void RunKick(ref bool hold)//if ready to kick product, hold to pause subsequent parallel load process.
            {
                //***Check Mode
                if (TaskConv.LeftMode != TaskConv.ELeftMode.ElevatorZ) goto _End;

                if (!SensMagNoPsnt(Setup.PsntMagz))
                {
                    ReadyToSend = false;
                    goto _End;
                }

                //***Check Status
                if (Status != EElevStatus.Ready) goto _End;
                Status = EElevStatus.Busy;

                if (WaitMagChange) goto _End;

                try
                {
                    if (TransferBusy) goto _End;

                    if (ReadyToSend)
                    {
                        #region no magazine
                        if (!SensMagNoPsnt(Setup.PsntMagz))
                        {
                            ReadyToSend = false;
                            goto _End;
                        }
                        #endregion

                        if (!Left.SensPusherHome)
                        {
                            TaskConv.Status = TaskConv.EConvStatus.Stop;
                            Msg MsgBox = new Msg();
                            MsgBox.Show(Messages.ELEV_PUSHER_SENS_HOME_ERROR, "RunKick", EMsgBtn.smbOK);
                            goto _End;
                        }

                        #region push product
                        ReadyToSend = false;
                        TransferBusy = true;

                        if (Setup.PusherRunConv) TaskConv.Conv.Fwd_Fast();

                        switch (PusherExt(ref hold))
                        {
                            case EMethodResult.OK: break;
                            case EMethodResult.Stop:
                                ReadyToSend = true;
                                TransferBusy = false;
                                TaskConv.Status = TaskConv.EConvStatus.Stop;
                                if (!PusherRet()) goto _Error;
                                goto _End;
                            default:
                            case EMethodResult.Error:
                                if (Setup.PusherRunConv) TaskConv.Conv.Stop();
                                goto _Error;
                        }

                        bool ProductPsnt = TaskConv.In.SensPsnt;
                        if (ProductPsnt)
                        {
                            SendRetry = 0;
                        }
                        else
                        {
                            if (Setup.PusherRunConv) TaskConv.Conv.Stop();
                            SendRetry++;
                        }

                        if (!PusherRet()) goto _Error;
                        #endregion

                        #region check no product
                        ProductPsnt = TaskConv.In.SensPsnt;
                        if (!ProductPsnt)
                        {
                            TransferBusy = false;
                            ReadyToSend = false;

                            #region handle SendRetry
                            if (SendRetry >= (Setup.PusherRetry))
                            {
                                Msg MsgBox = new Msg();
                                EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_PUSHER_CONT_SEND_FAIL, "", EMsgBtn.smbRetry_Stop);
                                switch (MsgRes)
                                {
                                    case EMsgRes.smrRetry:
                                        SendRetry = 0;
                                        break;
                                    default:
                                        TaskConv.Status = TaskConv.EConvStatus.Stop;
                                        TaskConv.StopInput = true;
                                        SendRetry = 0;
                                        goto _End;
                                }
                            }
                            #endregion
                        }
                        #endregion
                        goto _End;
                    }
                }
                catch (Exception Ex)
                {
                    Status = EElevStatus.ErrorInit;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }
            _End:
                Status = EElevStatus.Ready;
                return;
            _Error:
                TransferBusy = false;
                Status = EElevStatus.ErrorInit;
                return;
            }
        }
        public static class Right
        {
            public static EElevStatus Status
            {
                get
                {
                    return ElevStatus[(int)TElevator.Right];
                }
                set
                {
                    ElevStatus[(int)TElevator.Right] = value;
                }
            }
            public static TSetup Setup = TaskElev.Setups[(int)TElevator.Right];

            public static bool Ready
            {
                get
                {
                    if (TaskConv.RightMode == TaskConv.ERightMode.ElevatorZ)
                    {
                        return (ElevStatus[(int)TElevator.Right] == EElevStatus.Ready);
                    }

                    return true;
                }
            }
            public static double Pos
            {
                get
                {
                    return TaskElev.Pos(ElevIO.RZAxis);
                }
            }

            public static bool SensRZHome
            {
                get
                {
                    return ZEC3002.Ctrl.GetDI(ref ElevIO.SensRZHome);
                }
            }

            public static bool Init()
            {
                string EMsg = "Right Elevator Init" + (char)13;
                try
                {
                    if (!ZEC3002.Ctrl.BoardOpened(ElevIO.BoardID))
                        if (!TaskElev.OpenBoard(ElevIO.BoardID)) goto _Error;

                    WaitMagChange = false;
                    TransferBusy = false;
                    ReadyToReceive = false;
                    Setup.PsntLevel = 0;
                    Setup.PsntMagz = 0;

                    if (!DoorIsClosed(true)) goto _Error;
                    if (!SafeCheck()) { goto _Error; }

                    #region RZ
                    if (ElevIO.RZAxis.MotorPara.Home.HomeDir == ZEC3002.Ctrl.THomeDir.P)
                    {
                        if (!Home(true)) { goto _Error; }
                    }
                    else
                    {
                        if (!Home(false)) { goto _Error; }
                    }
                    #endregion
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }

                ElevStatus[(int)TElevator.Right] = EElevStatus.Ready;
                return true;

            _Error:
                ElevStatus[(int)TElevator.Right] = EElevStatus.ErrorInit;
                return false;
            }
            private static bool Home(bool HomeP)
            {
                ZEC3002.Ctrl.ZM3xx_UpdateMotorParaRange(ref ElevIO.RZAxis);
                ForceStop(ElevIO.RZAxis);

            _Retry:
                if (!DoorIsClosed(true)) goto _Error;
                //Motor On/Off
                ElevIO.MtrOnOff(ref ElevIO.RZAxis, false);
                if (!Delay(300)) { goto _Error; }
                ElevIO.MtrOnOff(ref ElevIO.RZAxis, true);
                if (!Delay(300)) { goto _Error; }

                if (CheckRZAlarm()) goto _Error;

                try
                {
                    #region Search Home
                    int t = Environment.TickCount + ElevIO.RZAxis.MotorPara.Home.TimeOut;
                    if (!SetMotionParam(ref ElevIO.RZAxis, 1, 10, 100)) { goto _Error; }

                    if (!Right.SensRZHome)
                    {
                        if (!SetMotionParam(ref ElevIO.RZAxis, 1, ElevIO.RZAxis.MotorPara.Home.FastV, 100)) { goto _Error; }
                        if (HomeP) JogP(ElevIO.RZAxis); else JogN(ElevIO.RZAxis);

                        while (true)
                        {
                            if (Right.SensRZHome)
                            {
                                break;
                            }
                            Thread.Sleep(5);
                            if (CheckRZAlarm()) goto _Error;

                            if (Environment.TickCount >= t)
                            {
                                if (!ForceStop(ElevIO.RZAxis)) { goto _Error; }
                                Msg MsgBox = new Msg();
                                MsgBox.Show(Messages.ELEV_HOME_TIME_OUT);
                                goto _Error;
                            }

                            if (Setups[(int)TElevator.Right].EnableDoorSens == (int)EDoorSens.ForceStop)
                            {
                                if (!Right.DoorIsClosed(false))
                                {
                                    ForceStop(ElevIO.RZAxis);
                                    Msg MsgBox = new Msg();
                                    EMsgRes Res = MsgBox.Show(Messages.RIGHT_ELEV_DOOR_OPEN, "", EMsgBtn.smbOK_Retry);
                                    if (Res == EMsgRes.smrRetry) goto _Retry;
                                    ElevStatus[(int)TElevator.Right] = EElevStatus.ErrorInit;
                                    return false;
                                }
                            }
                        }
                        if (!ForceStop(ElevIO.RZAxis)) { goto _Error; }
                        if (!RZWait(0)) { return false; }
                        if (!Delay(100)) { goto _Error; }
                    }
                    #endregion
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }
                try
                {
                    #region Clear Home
                    if (Right.SensRZHome)
                    {
                        int t = Environment.TickCount + ElevIO.RZAxis.MotorPara.Home.TimeOut;
                        if (!SetMotionParam(ref ElevIO.RZAxis, 1, ElevIO.RZAxis.MotorPara.Home.FastV, 100)) { goto _Error; }
                        if (HomeP) JogN(ElevIO.RZAxis); else JogP(ElevIO.RZAxis);

                        while (true)
                        {
                            if (!Right.SensRZHome) { break; }
                            Thread.Sleep(5);
                            if (CheckRZAlarm()) goto _Error;

                            if (Environment.TickCount >= t)
                            {
                                if (!ForceStop(ElevIO.RZAxis)) { goto _Error; }
                                Msg MsgBox = new Msg();
                                MsgBox.Show(Messages.ELEV_HOME_TIME_OUT);
                                goto _Error;
                            }
                            if (Setups[(int)TElevator.Right].EnableDoorSens == (int)EDoorSens.ForceStop)
                            {
                                if (!Right.DoorIsClosed(false))
                                {
                                    ForceStop(ElevIO.RZAxis);
                                    Msg MsgBox = new Msg();
                                    EMsgRes Res = MsgBox.Show(Messages.RIGHT_ELEV_DOOR_OPEN, "", EMsgBtn.smbOK_Retry);
                                    if (Res == EMsgRes.smrRetry) goto _Retry;
                                    ElevStatus[(int)TElevator.Right] = EElevStatus.ErrorInit;
                                    return false;
                                }
                            }
                        }
                        if (!ForceStop(ElevIO.RZAxis)) { goto _Error; }
                        if (!RZWait(0)) { return false; }
                        if (!Delay(100)) { goto _Error; }
                    }
                    #endregion
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }

                try
                {
                    #region Touch Home
                    if (!Right.SensRZHome)
                    {
                        int t = Environment.TickCount + ElevIO.RZAxis.MotorPara.Home.TimeOut;
                        if (!SetMotionParam(ref ElevIO.RZAxis, 1, 10, 100)) { goto _Error; }
                        if (HomeP) JogP(ElevIO.RZAxis); else JogN(ElevIO.RZAxis);

                        while (true)
                        {
                            if (Right.SensRZHome) break;
                            Thread.Sleep(5);
                            if (CheckRZAlarm()) goto _Error;

                            if (Environment.TickCount >= t)
                            {
                                if (!ForceStop(ElevIO.RZAxis)) { goto _Error; }
                                Msg MsgBox = new Msg();
                                MsgBox.Show(Messages.ELEV_HOME_TIME_OUT);
                                goto _Error;
                            }
                        }
                        if (!ForceStop(ElevIO.RZAxis)) { goto _Error; }
                        //if (!AxisWait(ElevIO.RZAxis, 1000)) { goto _Error; }
                        if (!RZWait(0)) { return false; }
                        if (!Delay(100)) { goto _Error; }
                    }

                    if (CheckRZAlarm()) goto _Error;

                    #endregion;
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }

                #region Set Param
                if (!SetLPos(ElevIO.RZAxis, 0)) { goto _Error; }
                if (!SetMotionParam(ref ElevIO.RZAxis)) { goto _Error; }
                #endregion

                return true;

            _Error:
                ForceStop(ElevIO.RZAxis);
                return false;
            }

            private static bool Delay(int msdelay)
            {
                if (msdelay <= 0) { return true; }
                int t = Environment.TickCount + msdelay;

                while (true)
                {
                    if (Environment.TickCount >= t) { break; }
                    Thread.Sleep(0);
                }

                return true;
            }
            private static bool Delay(double msdelay)
            {
                return Delay(msdelay);
            }

            public static bool MagDirUp
            {
                get
                {
                    //S 2.0.5.4
                    //int MaxMag = Setup[(int)TElevator.Right].MagCount;
                    //int Magz1stPos = Setup[(int)TElevator.Right].Magz_1stLevel[0];
                    //int MagzLastPos = Setup[(int)TElevator.Right].Magz_1stLevel[MaxMag - 1];
                    //return false;// (Magz1stPos > MagzLastPos);

                    int MaxMag = (int)Setups[(int)TElevator.Right].MagCount;
                    if (MaxMag == 0) MaxMag = 1;
                    double Magz1stPos = Setups[(int)TElevator.Right].Mag1stLevelPos[0];
                    double MagzLastPos = Setups[(int)TElevator.Right].Mag1stLevelPos[MaxMag - 1];
                    return (Magz1stPos > MagzLastPos);
                    //E 2.0.5.4
                }
            }
            public static int MagCount
            {
                get
                {
                    return (int)Setups[(int)TElevator.Right].MagCount;
                }
            }

            public static bool SensMagPsnt(int MagNo)
            {
                switch (MagNo)
                {
                    default:
                    case 1: return ZEC3002.Ctrl.GetDI(ref ElevIO.Right_SensMagPsnt1);
                    case 2: return ZEC3002.Ctrl.GetDI(ref ElevIO.Right_SensMagPsnt2);
                    case 3: return ZEC3002.Ctrl.GetDI(ref ElevIO.Right_SensMagPsnt3);
                    case 4: return ZEC3002.Ctrl.GetDI(ref ElevIO.Right_SensMagPsnt4);
                }
            }
            public static bool SensMagNoPsnt(int MagNo)
            {
                bool DirUp = false;
                bool SensMagzPsnt = false;
                int LastMagz = Setups[(int)TElevator.Right].MagCount;
                int MagCount = LastMagz;
                double Magz1stPos = Setups[(int)TElevator.Right].Mag1stLevelPos[0];
                double MagzLastPos = Setups[(int)TElevator.Right].Mag1stLevelPos[LastMagz - 1];
                if (Magz1stPos >= MagzLastPos) { DirUp = true; }
                if (Magz1stPos < MagzLastPos) { DirUp = false; }
                if (DirUp)
                {
                    #region Up
                    if (MagNo == 1)
                    {
                        SensMagzPsnt = SensMagPsnt(1);
                    }
                    if (MagNo == 2)
                    {
                        SensMagzPsnt = SensMagPsnt(2);
                    }
                    if (MagNo == 3)
                    {
                        SensMagzPsnt = SensMagPsnt(3);
                    }
                    if (MagNo == 4)
                    {
                        SensMagzPsnt = SensMagPsnt(4);
                    }
                    #endregion
                }
                else
                {
                    #region Down

                    switch (MagCount)
                    {
                        case 4://4magazine
                            {
                                if (MagNo == 1)
                                {
                                    SensMagzPsnt = SensMagPsnt(4);
                                }
                                if (MagNo == 2)
                                {
                                    SensMagzPsnt = SensMagPsnt(3);
                                }
                                if (MagNo == 3)
                                {
                                    SensMagzPsnt = SensMagPsnt(2);
                                }
                                if (MagNo == 4)
                                {
                                    SensMagzPsnt = SensMagPsnt(1);
                                }
                                break;
                            }
                        case 3://3magazine
                            {
                                if (MagNo == 1)
                                {
                                    SensMagzPsnt = SensMagPsnt(3);
                                }
                                if (MagNo == 2)
                                {
                                    SensMagzPsnt = SensMagPsnt(2);
                                }
                                if (MagNo == 3)
                                {
                                    SensMagzPsnt = SensMagPsnt(1);
                                }
                                break;
                            }
                        case 2://2magazine
                            {
                                if (MagNo == 1)
                                {
                                    SensMagzPsnt = SensMagPsnt(2);
                                }
                                if (MagNo == 2)
                                {
                                    SensMagzPsnt = SensMagPsnt(1);
                                }
                                break;
                            }
                        case 1://1magazine
                            {
                                if (MagNo == 1)
                                {
                                    SensMagzPsnt = SensMagPsnt(1);
                                }
                                break;
                            }
                    }
                    #endregion
                }
                return SensMagzPsnt;
            }
            public static bool SafeCheck(bool bRetry = true)
            {
                try
                {
                    if (!DoorIsClosed(true)) goto _Error;

                    //EMsgBtn msgBtn = bRetry ? EMsgBtn.smbRetry_Cancel : EMsgBtn.smbCancel;
                    EMsgBtn msgBtn = EMsgBtn.smbCancel;

                _Retry:
                    if (TaskConv.Out.SensPsnt)
                    {
                        string msg = "Product Present at Conveyor Out.";
                        Msg MsgBox = new Msg();
                        EMsgRes Res = MsgBox.Show(Messages.CONV_OUT_SENSOR_PSNT, msg, msgBtn);
                        switch (Res)
                        {
                            case EMsgRes.smrRetry: goto _Retry;
                        }
                        goto _Error;
                    }
                _Retry1:
                    if (TaskConv.Out.SensLFPsnt)
                    {
                        string msg = "Product Present at Conveyor Out and Magazine.";
                        Msg MsgBox = new Msg();
                        EMsgRes Res = MsgBox.Show(Messages.CONV_OUT_CLEAR_SENSOR_PSNT, msg, msgBtn);
                        switch (Res)
                        {
                            case EMsgRes.smrRetry: goto _Retry1;
                        }
                        goto _Error;
                    }
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + Ex.Message);
                    goto _Error;
                }

                return true;
            _Error:
                return false;
            }

            public static bool SensDoor
            {
                get
                {
                    //bool Status = false;
                    //try
                    {
                        if (!ZEC3002.Ctrl.BoardOpened(ConvIO.BoardID)) return false;

                        return ZEC3002.Ctrl.GetDI(ref ElevIO.Right_SensDoor);
                    }
                    //catch (Exception Ex)
                    //{
                    //    throw new Exception("Right DoorSens " + Ex.Message.ToString());
                    //}
                    //return Status;
                }
            }
            static DateTime dt_BypassUntil = DateTime.Now;
            public static int BypassDoorCheckTimer
            {
                set
                {
                    dt_BypassUntil = DateTime.Now.AddSeconds(value);
                }
                get
                {
                    TimeSpan ts = dt_BypassUntil.Subtract(DateTime.Now);
                    return (int)ts.TotalSeconds;
                }
            }
            public static bool DoorIsClosed(bool Prompt)
            {
                if (TaskConv.RightMode != TaskConv.ERightMode.ElevatorZ) return true;
                if (Setups[(int)TElevator.Right].EnableDoorSens == (int)EDoorSens.None) return true;

                if (BypassDoorCheckTimer > 0) return true;

                _Retry:
                try
                {
                    if (!SensDoor)
                    {
                        if (Prompt)
                        {
                            Msg MsgBox = new Msg();
                            EMsgRes Res = MsgBox.Show(Messages.RIGHT_ELEV_DOOR_OPEN, "", EMsgBtn.smbRetry_Cancel);
                            if (Res == EMsgRes.smrRetry) goto _Retry;
                        }
                        return false;
                    }
                }
                catch (Exception Ex)
                {
                    throw new Exception("Right DoorCheck " + Ex.Message.ToString());
                }
                return true;
            }
            public static bool DoorCheck(bool Prompt)//reverse compatibility
            {
                return DoorIsClosed(Prompt);
            }

            public static bool MoveLevel(int MagNo, int LevelNo)
            {
                string EMsg = "RZMoveLevel";

                #region check mag and level limit
                if (MagNo <= 0) MagNo = 1;

                int MaxMag = Setups[(int)TElevator.Right].MagCount;
                if (MagNo > MaxMag) MagNo = MaxMag;

                if (LevelNo <= 0) LevelNo = 1;
                int MaxLevel = Setups[(int)TElevator.Right].LevelCount;
                if (LevelNo > MaxLevel) LevelNo = MaxLevel;
                #endregion

                try
                {
                    if (!SafeCheck()) { goto _Error; }

                    #region Process
                    double LevelPitch = Setups[(int)TElevator.Right].LevelPitch;
                    double MovePos = Setups[(int)TElevator.Right].Mag1stLevelPos[MagNo - 1] + ((LevelNo - 1) * LevelPitch);

                    ElevStatus[(int)TElevator.Right] = EElevStatus.Busy;
                    if (!SetMotionParam(ref ElevIO.RZAxis)) { goto _Error; }
                    if (!RZMove(MovePos)) { goto _Error; }
                    ElevStatus[(int)TElevator.Right] = EElevStatus.Ready;
                    #endregion

                    Setups[(int)TElevator.Right].PsntMagz = MagNo;
                    Setups[(int)TElevator.Right].PsntLevel = LevelNo;

                    int TMag = Setups[(int)TElevator.Right].MagCount;
                    int TLevel = Setups[(int)TElevator.Right].LevelCount;

                    NUtils.RegistryWR RegRW = new NUtils.RegistryWR("Software");
                    RegRW.WriteKey("MHS\\Elev\\Out", "MagNo", MagNo - 1);
                    RegRW.WriteKey("MHS\\Elev\\Out", "LevelNo", LevelNo - 1);
                    RegRW.WriteKey("MHS\\Elev\\Out", "TMag", TMag);
                    RegRW.WriteKey("MHS\\Elev\\Out", "TLevel", TLevel);
                }
                catch (Exception Ex)
                {
                    ElevStatus[(int)TElevator.Right] = EElevStatus.ErrorInit;
                    EMsg = EMsg + (char)13 + Ex.Message;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, EMsg + Ex.Message, EMsgBtn.smbOK);
                    goto _Error;
                }
                return true;

            _Error:
                ElevStatus[(int)TElevator.Right] = EElevStatus.ErrorInit;
                return false;
            }
            public static bool MoveUpLevel()
            {
                int MagNo = Setups[(int)TElevator.Right].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Right].PsntLevel;

                double d_Pitch = Setups[(int)TElevator.Right].LevelPitch;

                if (d_Pitch > 0)
                {
                    if (LevelNo >= Setups[1].LevelCount)
                    {
                        if (MagNo <= 1)
                        {
                        }
                        else
                        {
                            MagNo--;
                            LevelNo = 1;
                        }
                    }
                    else
                        LevelNo++;
                }
                else
                {
                    if (LevelNo <= 1)
                    {
                        if (MagNo <= 1)
                        {
                        }
                        else
                        {
                            MagNo--;
                            LevelNo = Setups[1].LevelCount;//1;
                        }
                    }
                    else
                        LevelNo--;
                }

                return MoveLevel(MagNo, LevelNo);
            }
            public static bool MoveDnLevel()
            {
                int MagNo = Setups[(int)TElevator.Right].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Right].PsntLevel;

                double d_Pitch = Setups[(int)TElevator.Right].LevelPitch;

                if (d_Pitch > 0)
                {
                    if (LevelNo <= 1)
                    {
                        if (MagNo >= Setups[1].MagCount)
                        {
                        }
                        else
                        {
                            MagNo++;
                            LevelNo = Setups[1].LevelCount;
                        }
                    }
                    else
                        LevelNo--;
                }
                else
                {
                    if (LevelNo >= Setups[1].LevelCount)
                    {
                        if (MagNo >= Setups[1].MagCount)
                        {
                        }
                        else
                        {
                            MagNo++;
                            LevelNo = 1;
                        }
                    }
                    else
                        LevelNo++;
                }

                return MoveLevel(MagNo, LevelNo);
            }
            public static bool MoveNextLevel()
            {
                int MagNo = Setups[(int)TElevator.Right].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Right].PsntLevel;

                if (LevelNo >= Setups[(int)TElevator.Right].LevelCount)
                {
                    LevelNo = 1;

                    if (MagNo >= Setups[(int)TElevator.Right].MagCount)
                    {
                        MagNo = 1;
                    }
                    else
                    {
                        MagNo++;
                        if (TaskConv.OutLevelQtyFollowIn) Left.b_MagChanged = false;
                    }
                }
                else
                    LevelNo++;

                return MoveLevel(MagNo, LevelNo);
            }
            public static bool MovePrevLevel()
            {
                int MagNo = Setups[(int)TElevator.Right].PsntMagz;
                int LevelNo = Setups[(int)TElevator.Right].PsntLevel;

                if (LevelNo <= 1)
                {
                    LevelNo = Setups[(int)TElevator.Right].LevelCount;

                    if (MagNo <= 1)
                    {
                        MagNo = Setups[(int)TElevator.Right].MagCount;
                    }
                    else
                        MagNo--;
                }
                else
                    LevelNo--;

                return MoveLevel(MagNo, LevelNo);
            }
            public static bool MoveToLoadMagPos()
            {
                if (!SafeCheck()) { goto _Error; }

                double Load_Pos = Setups[(int)TElevator.Right].MagLoadPos;

                ElevStatus[(int)TElevator.Right] = EElevStatus.Busy;
                if (!SetMotionParam(ref ElevIO.RZAxis)) { goto _Error; }
                if (!RZMove(Load_Pos)) { goto _Error; }
                ElevStatus[(int)TElevator.Right] = EElevStatus.Ready;

                Setups[(int)TElevator.Right].PsntLevel = 0;
                Setups[(int)TElevator.Right].PsntMagz = 0;

                if (TaskConv.OutLevelQtyFollowIn) Left.b_MagChanged = false;

                return true;
            _Error:
                return false;
            }

            public static bool WaitMagChange = false;

            public static bool ReadyToReceive = false;//ready to receive product
            public static bool TransferBusy = false;//product tranfer busy
            public static void RunLevel()
            {
                int EIdx = (int)TElevator.Right;
                if (TaskConv.RightMode != TaskConv.ERightMode.ElevatorZ) goto _End;

                if (ElevStatus[EIdx] != EElevStatus.Ready) goto _End;

                if (WaitMagChange) goto _End;

                try
                {
                    if (TransferBusy) goto _End;

                    ElevStatus[EIdx] = EElevStatus.Busy;

                    if (!ReadyToReceive)
                    {
                        #region Handle Last Mag Last Level
                        if ((Setups[EIdx].PsntLevel >= Setups[EIdx].LevelCount) && Setups[EIdx].PsntMagz == Setups[EIdx].MagCount)
                        {
                            ReadyToReceive = false;
                            WaitMagChange = true;

                            if (!MoveToLoadMagPos()) { goto _Error; }
                            goto _End;
                        }
                        #endregion

                        #region Handle No Mag and Mag Last Level
                        if (Setups[EIdx].PsntLevel >= Setups[EIdx].LevelCount || !SensMagNoPsnt(Setups[EIdx].PsntMagz))
                        {
                        _CheckMag:
                            Setups[EIdx].PsntMagz++;
                            Setups[EIdx].PsntLevel = 1;

                            if (Setups[EIdx].PsntMagz >= Setups[EIdx].MagCount)
                            {
                                if (!SensMagNoPsnt(Setups[EIdx].PsntMagz))
                                {
                                    ReadyToReceive = false;
                                    WaitMagChange = true;

                                    if (!MoveToLoadMagPos()) { goto _Error; }
                                    goto _End;
                                }

                            }
                            if (SensMagNoPsnt(Setups[EIdx].PsntMagz))
                            {
                                if (!MoveLevel(Setups[EIdx].PsntMagz, Setups[EIdx].PsntLevel)) { goto _Error; }
                                if (TaskConv.OutLevelQtyFollowIn) Left.b_MagChanged = false;
                                ReadyToReceive = true;

                                goto _End;
                            }
                            goto _CheckMag;

                        }
                        #endregion

                        #region Initial Level and Next Level
                        if (!MoveNextLevel()) goto _Error;
                        ReadyToReceive = true;
                        goto _End;
                        #endregion
                    }

                    if (ReadyToReceive)
                    {
                        #region no magazine
                        if (!SensMagNoPsnt(Setups[EIdx].PsntMagz))
                        {
                            Setups[EIdx].PsntMagz++;
                            Setups[EIdx].PsntLevel = 1;

                            if (Setups[EIdx].PsntMagz == Setups[EIdx].MagCount)
                            {
                                if (!SensMagNoPsnt(Setups[EIdx].PsntMagz))
                                {
                                    ReadyToReceive = false;
                                    WaitMagChange = true;

                                    if (!MoveToLoadMagPos()) { goto _Error; }
                                    goto _End;
                                }

                            }
                            if (SensMagNoPsnt(Setups[EIdx].PsntMagz))
                            {
                                if (!MoveLevel(Setups[EIdx].PsntMagz, Setups[EIdx].PsntLevel)) { goto _Error; }
                                if (TaskConv.OutLevelQtyFollowIn) Left.b_MagChanged = false;
                                ReadyToReceive = true;
                                goto _End;
                            }
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    ElevStatus[EIdx] = EElevStatus.ErrorInit;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message);
                    goto _Error;
                }
            _End:
                ElevStatus[EIdx] = EElevStatus.Ready;
                return;
            _Error:
                ElevStatus[EIdx] = EElevStatus.Ready;
            }
            public static void RunLevel_BurnRun()
            {
                if (TaskConv.RightMode != TaskConv.ERightMode.ElevatorZ) goto _End;
                if (Status != EElevStatus.Ready) goto _End;

                try
                {
                    bool Psnt = TaskConv.Out.SensPsnt;
                    if (Psnt) goto _End;

                    Status = EElevStatus.Busy;

                    #region Handle Last Mag Last Level
                    if ((Setup.PsntLevel >= Setup.LevelCount) && Setup.PsntMagz == Setup.MagCount)
                    {
                        if (!MoveToLoadMagPos()) { goto _Error; }
                        goto _End;
                    }
                    #endregion

                    #region Mag Last Level
                    if (Setup.PsntLevel >= Setup.LevelCount)
                    {
                        Setup.PsntMagz++;
                        Setup.PsntLevel = 1;

                        if (Setup.PsntMagz >= Setup.MagCount)
                        {
                            if (!MoveToLoadMagPos()) { goto _Error; }
                            goto _End;
                        }

                        if (!MoveLevel(Setup.PsntMagz, Setup.PsntLevel)) { goto _Error; }
                        goto _End;
                    }
                    #endregion

                    #region Initial Level and Next Level
                    if (!MoveNextLevel()) goto _Error;
                    goto _End;
                    #endregion
                }
                catch (Exception ex)
                {
                    Status = EElevStatus.ErrorInit;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.ELEV_EX_ERR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message);
                    goto _Error;
                }
            _End:
                Status = EElevStatus.Ready;
                return;
            _Error:
                Status = EElevStatus.Ready;
            }
            public static bool CheckMagPsnt()
            {
            _Retry:
                int EIdx = (int)TElevator.Right;
                if (Setups[EIdx].PsntMagz == 0) return true;
                if (!SensMagNoPsnt(Setups[EIdx].PsntMagz))
                {
                    Msg MsgBox = new Msg();
                    EMsgRes MsgRes = MsgBox.Show(Messages.ELEV_MAG_MISSING, "RZ", EMsgBtn.smbRetry_Stop);
                    switch (MsgRes)
                    {
                        case EMsgRes.smrRetry: goto _Retry;
                        default: goto _Stop;
                    }
                }
                ElevStatus[EIdx] = EElevStatus.Ready;
                return true;
            _Stop:
                ElevStatus[EIdx] = EElevStatus.Ready;
                return false;
            }

            static Task taskRunLevel = null;
            public static bool RunLevelAsyncIsBusy
            {
                get 
                {
                    if (taskRunLevel == null) return false;
                    if (taskRunLevel.Status == TaskStatus.Running) return true;
                    return false;
                }
            }
            public static void RunLevelAsync()
            {
                try
                {
                    if (taskRunLevel != null && taskRunLevel.Status == TaskStatus.Running) return;
                    taskRunLevel = Task.Run(() => { RunLevel(); });//no await
                }
                catch (Exception Ex)
                {
                    Msg MsgBox = new Msg();
                    MsgBox.Show("Righ.RunLevelAsync " + Ex.Message.ToString());
                }
                finally
                {
                }
            }
        }
    }
}
