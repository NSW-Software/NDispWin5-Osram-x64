using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace NDispWin
{
    class TEEvent
    {
        public int Code = 0;
        public string Name = "";
        public List<int> VIDs;//related VIDs

        public TEEvent(int code, string name)
        {
            Code = code;
            Name = name;
            
        }
        public TEEvent(int code, string name, List<int> vids = null)
        {
            Code = code;
            Name = name;
            VIDs = vids;
        }
        public void Set(string paraName, string paraValue)
        {
            Event.LastEvent = this;

            if (!GDefineN.EnableEventDebugLog && Code == Event.DEBUG_INFO.Code) return;

            if (this.Name.Length == 0) this.Name = this.Name.ToString();

            Log.AddToEventLog(this.Code, this.Name, paraName + " " + paraValue);
            GDefine.sgc2.SendEvent(this.Code + "," + this.Name + "," + paraName + "," + paraValue);
        }
        public void Set()
        {
            Event.LastEvent = this;

            if (!GDefineN.EnableEventDebugLog && Code == Event.DEBUG_INFO.Code) return;

            if (this.Name.Length == 0) this.Name = this.Name.ToString();

            Log.AddToEventLog(this.Code, this.Name);
            GDefine.sgc2.SendEvent(this.Code + "," + this.Name);
        }

        public static List<string> CEID_List()
        {
            var ceIDlist = typeof(Event).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEEvent))
                .Select(x => (TEEvent)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var para in ceIDlist)
            {
                string vids = para.VIDs == null ? "" : $",VID,{string.Join(",", para.VIDs)}"; 
                list.Add($"{para.Code:d4},{para.Name}{vids}");
            }

            return list;
        }
        public static List<string> ALID_List()
        {
            //var alIDlist = typeof(ErrCode).GetFields(BindingFlags.Public | BindingFlags.Static).ToArray();

            //List<string> list = new List<string>();
            //foreach (var para in alIDlist)
            //{
            //    list.Add($"{para. .GetValue(para):d4},{para.Name}");
            //}

            var msglist = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var msg in msglist)
            {
                list.Add($"{msg.Code:d4},{msg.Desc}");
            }

            return list;
        }
    }

    class Event
    {
        internal static TEEvent LastEvent = new TEEvent(0, "");

        const int APP_EVENT = 1000;

        const int APP_EVENT_OP = APP_EVENT + 0;
        public static TEEvent APP_START = new TEEvent(APP_EVENT_OP + 0, "App StartUp.");
        public static TEEvent APP_CLOSE = new TEEvent(APP_EVENT_OP + 1, "App Close.");
        public static TEEvent ERROR = new TEEvent(APP_EVENT_OP + 2, "Error.");

        public static TEEvent CTRL = new TEEvent(APP_EVENT_OP + 3, "Control.");//log control
        public static TEEvent APP_INFO = new TEEvent(APP_EVENT_OP + 4, "App Info.");
        public static TEEvent DEBUG_INFO = new TEEvent(APP_EVENT_OP + 6, "Debug Info.");
        public static TEEvent WARNING = new TEEvent(APP_EVENT_OP + 7, "Warning.");

        public static TEEvent OP_START_RUN = new TEEvent(APP_EVENT_OP + 10, "Auto Start Run.");
        public static TEEvent OP_STOP_RUN = new TEEvent(APP_EVENT_OP + 11, "Auto Stop Run.");
        public static TEEvent OP_INIT_GANTRY_START = new TEEvent(APP_EVENT_OP + 20, "App Init Gantry Start.");
        public static TEEvent OP_INIT_GANTRY_COMPLETE = new TEEvent(APP_EVENT_OP + 21, "App Init Gantry Complete.");
        public static TEEvent OP_INIT_CONV_START = new TEEvent(APP_EVENT_OP + 22, "App Init Conveyor Start.");
        public static TEEvent OP_INIT_CONV_COMPLETE = new TEEvent(APP_EVENT_OP + 23, "App Init Conveyor Complete.");
        public static TEEvent OP_INIT_LR_LINE_START = new TEEvent(APP_EVENT_OP + 24, "App Init LR Line Start.");
        public static TEEvent OP_INIT_LR_LINE_COMPLETE = new TEEvent(APP_EVENT_OP + 25, "App Init LR Line Complete.");
        public static TEEvent OP_MAIN_LOGIN = new TEEvent(APP_EVENT_OP + 30, "Main Login.");
        public static TEEvent OP_PROMPT_LOGIN = new TEEvent(APP_EVENT_OP + 31, "Prompt Login.");

        public static TEEvent FAULT = new TEEvent(APP_EVENT_OP + 50, "Fault.");
        public static TEEvent NOTIFICATION = new TEEvent(APP_EVENT_OP + 51, "Notification.");
        public static TEEvent CONFIRMATION = new TEEvent(APP_EVENT_OP + 52, "Confirmation.");
        public static TEEvent CUSTOM1 = new TEEvent(APP_EVENT_OP + 53, "Custom1.");
        public static TEEvent CUSTOM2 = new TEEvent(APP_EVENT_OP + 54, "Custom2.");

        const int DEVICE_EVENT = 1100;
        #region Click Event
        public static TEEvent CAMERA_INFO = new TEEvent(DEVICE_EVENT + 50, "Camera Info.");
        #endregion

        const int DISPCORE_EVENT = 2000;
        public static TEEvent TEST_EVENT = new TEEvent(DISPCORE_EVENT, "Test Event.");

        const int DISPCORE_EVENT_DISPTOOLS = 2100;
        #region 
        public static TEEvent DISPTOOLS_TEACH_NEEDLE = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 10, "DispTools Teach Needle.");
        public static TEEvent DISPTOOLS_TEACH_NEEDLE_CANCEL = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 11, "DispTools Teach Needle Cancel.");
        public static TEEvent DISPTOOLS_GOTO_PUMP_MAINT_POS = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 12, "DispTools Goto Pump Maint Pos.");
        public static TEEvent DISPTOOLS_GOTO_MACHINE_MAINT_POS = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 13, "DispTools Goto Machine Maint Pos.");
        public static TEEvent DISPTOOLS_CLEAN = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 15, "DispTools Clean.");
        public static TEEvent DISPTOOLS_PURGE = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 16, "DispTools Purge.");
        public static TEEvent DISPTOOLS_FLUSH = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 17, "DispTools Flush.");
        public static TEEvent DISPTOOLS_CLEANPURGE_CANCEL = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 18, "DispTools Clean Purge Cancel.");
        public static TEEvent DISPTOOLS_WEIGHT_ADJUST = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 20, "DispTools Weight Adjust.");
        public static TEEvent DISPTOOLS_WEIGHT_CALIBRATE = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 21, "DispTools Weight Calibrate.");
        public static TEEvent DISPTOOLS_WEIGHT_MEASURE = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 22, "DispTools Weight Measure.");
        public static TEEvent DISPTOOLS_WEIGHT_CANCEL = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 23, "DispTools Weight Cancel.");
        public static TEEvent DISPTOOLS_ADJ_MATERIAL_TIMER = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 25, "DispTools Adjust Material Timer.");
        public static TEEvent DISPTOOLS_ADJ_MATERIAL_EXP = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 26, "DispTools Adjust Material Expiry.");
        public static TEEvent DISPTOOLS_ADJ_MATERIAL_EXP_CANCEL = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 26, "DispTools Adjust Material Expiry Cancel.");
        public static TEEvent DISPTOOLS_FORCE_SINGLE = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 30, "DispTools Force Single.");
        public static TEEvent DISPTOOLS_PUMP_ADJUST = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 32, "DispTools Pump Adjust.");
        public static TEEvent DISPTOOLS_ORIGIN_ADJUST = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 33, "DispTools Origin Adjust.");
        public static TEEvent DISPTOOLS_ORIGIN = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 34, "DispTools Origin.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_1 = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 40, "DispTools Pump Action 1.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_2 = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 41, "DispTools Pump Action 2.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_3 = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 42, "DispTools Pump Action 3.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_4 = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 43, "DispTools Pump Action 4.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_5 = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 44, "DispTools Pump Action 5.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_CANCEL = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 45, "DispTools Pump Action Cancel.");
        public static TEEvent DISPTOOLS_START_IDLE = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 50, "DispTools Start Idle.");
        public static TEEvent DISPTOOLS_PURGE_STAGE = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 51, "DispTools Purge Stage.");
        public static TEEvent DISPTOOLS_VIEW = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 52, "DispTools View.");
        public static TEEvent DISPTOOLS_ADJ_VALVE_TIMER = new TEEvent(DISPCORE_EVENT_DISPTOOLS + 25, "DispTools Adjust Valve Timer.");
        #endregion

        const int DISPCORE_EVENT_OP = DISPCORE_EVENT + 200;
        public static TEEvent OP_WEIGHT_ADJUST = new TEEvent(DISPCORE_EVENT_OP + 0, "Weight Adjust.");
        public static TEEvent OP_WEIGHT_CALIBRATION = new TEEvent(DISPCORE_EVENT_OP + 1, "Weight Calibration.", new List<int> { 11010, 11011 });
        public static TEEvent OP_WEIGHT_MEASURE = new TEEvent(DISPCORE_EVENT_OP + 2, "Weight Measure.", new List<int> {11020, 11021, 11022, 11023});
        public static TEEvent OP_FLOWRATE1_CALIBRATION = new TEEvent(DISPCORE_EVENT_OP + 3, "Flowrate1 Calibration.", new List<int> {11030});
        public static TEEvent OP_FLOWRATE2_CALIBRATION = new TEEvent(DISPCORE_EVENT_OP + 4, "Flowrate2 Calibration.", new List<int> { 11031 });
        public static TEEvent OP_WEIGHT1_MEASURE = new TEEvent(DISPCORE_EVENT_OP + 5, "Weight1 Measure.", new List<int> { 11040, 11041, 11042, 11043 });
        public static TEEvent OP_WEIGHT2_MEASURE = new TEEvent(DISPCORE_EVENT_OP + 6, "Weight2 Measure.", new List<int> { 11040, 11041, 11042, 11043 });
        public static TEEvent OP_IDLE_PURGE_START = new TEEvent(DISPCORE_EVENT_OP + 10, "Start Idle Purge.");
        public static TEEvent OP_IDLE_PURGE_STOP = new TEEvent(DISPCORE_EVENT_OP + 11, "Stop Idle Purge.");
        public static TEEvent OP_SET_WEIGHT = new TEEvent(DISPCORE_EVENT_OP + 12, "Set Weight.");

        public static TEEvent OP_DISP_LOAD_DEVICE = new TEEvent(DISPCORE_EVENT_OP + 15, "Load Device.", new List<int> { 20100, 20101, 20102 });
        public static TEEvent OP_DISP_LOAD_DISP_RECIPE = new TEEvent(DISPCORE_EVENT_OP + 16, "Load Disp Recipe.", new List<int> { 20101});
        public static TEEvent OP_DISP_LOAD_MHS_RECIPE = new TEEvent(DISPCORE_EVENT_OP + 17, "Load MHS Recipe.", new List<int> { 20102 });
        public static TEEvent OP_DISP_AUTO_LOAD_DEVICE_INVALID = new TEEvent(DISPCORE_EVENT_OP + 17, "Auto Load Device is Invalid.");
        public static TEEvent OP_DISP_AUTO_LOAD_DEVICE_NO_FOUND = new TEEvent(DISPCORE_EVENT_OP + 18, "Auto Load Device not Found.");
        public static TEEvent OP_DISP_AUTO_LOAD_SUCCESSFUL = new TEEvent(DISPCORE_EVENT_OP + 19, "Auto Load Device successful.", new List<int> { 20100, 20101, 20102 });

        public static TEEvent OP_EXT_VISION_OK = new TEEvent(DISPCORE_EVENT_OP + 30, "Ext Vision Inspection OK.");
        public static TEEvent OP_EXT_VISION_NG = new TEEvent(DISPCORE_EVENT_OP + 31, "Ext Vision Inspection NG.");

        public static TEEvent OP_LMDS_TESTER_SEQ = new TEEvent(DISPCORE_EVENT_OP + 90, "Lmds Tester Seq.");
        public static TEEvent OP_CHECK_MENISCUS = new TEEvent(DISPCORE_EVENT_OP + 91, "Check Meniscus.");
        public static TEEvent OP_CHECK_MENISCUS_OOS = new TEEvent(DISPCORE_EVENT_OP + 92, "Check Meniscus OOS.");

        public static TEEvent OP_LOT_START = new TEEvent(DISPCORE_EVENT_OP + 95, "Lot Start.", new List<int> {20000, 20001, 20002, 20003, 20004, 20005, 20006, 20007});
        public static TEEvent OP_LOT_END = new TEEvent(DISPCORE_EVENT_OP + 96, "Lot End.", new List<int> { 20000, 20001, 20002, 20003, 20004, 20005, 20006, 20007 });

        public static TEEvent CLEAN_NEEDLE = new TEEvent(DISPCORE_EVENT_OP + 100, "Clean Needle.");
        public static TEEvent PURGE_NEEDLE = new TEEvent(DISPCORE_EVENT_OP + 101, "Purge Needle.");
        public static TEEvent FLUSH_NEEDLE = new TEEvent(DISPCORE_EVENT_OP + 101, "Flush Needle.");

        const int DISPCORE_EVENT_PROG = DISPCORE_EVENT + 400;
        public static TEEvent PROG_UNLOCK_Z = new TEEvent(DISPCORE_EVENT_PROG + 0, "Program Mode Unlock Z.");

        const int DISPCORE_EVENT_SETUP = DISPCORE_EVENT + 500;
        public static TEEvent SETUP_BYPASS_TEACH_NEEDLE = new TEEvent(DISPCORE_EVENT_SETUP + 0, "ByPass Teach Needle.");
        public static TEEvent TEACH_NEEDLE_OFST = new TEEvent(DISPCORE_EVENT_SETUP + 1, "Teach Needle Offset.");
        public static TEEvent SETUP_EVENT = new TEEvent(DISPCORE_EVENT_SETUP + 2, "Setup.");
        public static TEEvent SETUP_HEAD1_OFST_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 50, "Head1 Offset Update.");
        public static TEEvent SETUP_HEAD2_OFST_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 51, "Head2 Offset Update.");
        public static TEEvent SETUP_LASER_OFST_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 53, "Laser Offset Update.");
        public static TEEvent SETUP_REFZ_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 54, "RefZ Update.");
        public static TEEvent SETUP_TOUCH_POS_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 55, "TouchPos Update.");
        public static TEEvent SET_FOCUS0_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 60, "Set Focus0.");
        public static TEEvent SET_FOCUS1_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 61, "Set Focus1.");
        public static TEEvent SET_FOCUS2_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 62, "Set Focus2.");
        public static TEEvent SET_FOCUS3_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 63, "Set Focus3.");

        public static TEEvent SETUP_TEMPSENSOR_OFST_UPDATE = new TEEvent(DISPCORE_EVENT_SETUP + 65, "Temp Sensor Offset Update.");
        public static TEEvent SETUP_TEMPSENSOR_VALUE = new TEEvent(DISPCORE_EVENT_SETUP + 65, "Temp Sensor Value.");

        const int DISPCORE_EVENT_CALIBRATION = DISPCORE_EVENT + 600;
        public static TEEvent CAL_DEFZPOS_UPDATE = new TEEvent(DISPCORE_EVENT_CALIBRATION + 0, "Calibrate Def Z Pos Update.");
        public static TEEvent CAL_DEFZPOS_CANCEL = new TEEvent(DISPCORE_EVENT_CALIBRATION + 1, "Calibrate Def Z Pos Cancel.");
        public static TEEvent CAL_LASER_CAL_VALUE_UPDATE = new TEEvent(DISPCORE_EVENT_CALIBRATION + 10, "Calibrate Laser Cal Value Update.");
        public static TEEvent CAL_LASER_CAL_VALUE_CANCEL = new TEEvent(DISPCORE_EVENT_CALIBRATION + 11, "Calibrate Laser Cal Value Cancel.");

        const int DISPCORE_EVENT_PUMP = DISPCORE_EVENT + 700;
        public static TEEvent PUMP1_DISP_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 0, "Pump1 Disp Volume Update.");
        public static TEEvent PUMP2_DISP_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 1, "Pump2 Disp Volume Update.");
        //public static TEEvent PUMP1_DISP_BASE_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 0, "Pump1 Disp Base Volume Update");
        //public static TEEvent PUMP2_DISP_BASE_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 1, "Pump2 Disp Base Volume Update");
        //public static TEEvent PUMP1_DISP_BASE_VOL_ADJ = new TEEvent(DISPCORE_EVENT_PUMP + 2, "Pump1 Disp Volume Adjust");
        //public static TEEvent PUMP2_DISP_BASE_VOL_ADJ = new TEEvent(DISPCORE_EVENT_PUMP + 3, "Pump2 Disp Volume Adjust");

        public static TEEvent PUMP1_BACKSUCK_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 4, "Pump1 Backsuck Volume Update.");
        public static TEEvent PUMP2_BACKSUCK_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 5, "Pump2 Backsuck Volume Update.");
        public static TEEvent PUMP1_DISP_SPEED_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 6, "Pump1 Disp Speed Update.");
        public static TEEvent PUMP2_DISP_SPEED_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 7, "Pump2 Disp Speed Update.");
        public static TEEvent PUMP1_BACKSUCK_SPEED_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 8, "Pump1 Backsuck Speed Update.");
        public static TEEvent PUMP2_BACKSUCK_SPEED_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 9, "Pump2 Backsuck Speed Update.");
        public static TEEvent PUMP1_DISP_TIME_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 10, "Pump1 Disp Time Update.");
        public static TEEvent PUMP2_DISP_TIME_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 11, "Pump2 Disp Time Update.");
        public static TEEvent PUMP1_BACKSUCK_TIME_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 12, "Pump1 Backsuck Time Update.");
        public static TEEvent PUMP2_BACKSUCK_TIME_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 13, "Pump2 Backsuck Time Update.");

        const int DISPCORE_EVENT_RUN = DISPCORE_EVENT + 800;
        public static TEEvent READ_ID = new TEEvent(DISPCORE_EVENT_RUN + 10, "Read ID.");
        public static TEEvent MANUAL_ID_ENTRY = new TEEvent(DISPCORE_EVENT_RUN + 11, "Manual ID Entry.");
        public static TEEvent INPUT_MAP = new TEEvent(DISPCORE_EVENT_RUN + 20, "Input Map.");
        public static TEEvent INPUT_MAP_QUERY = new TEEvent(DISPCORE_EVENT_RUN + 21, "Input Map Query.");
        public static TEEvent PP_SELECTED = new TEEvent(DISPCORE_EVENT_RUN + 30, "PP-Selected.");
        public static TEEvent PPSEND_H2E = new TEEvent(DISPCORE_EVENT_RUN + 31, "PPSend H2E.");
        public static TEEvent PPSEND_E2H = new TEEvent(DISPCORE_EVENT_RUN + 32, "PPSend E2H.");
        public static TEEvent MAP_REQUEST = new TEEvent(DISPCORE_EVENT_RUN + 35, "Map Request.");
        public static TEEvent MAP_DOWNLOADED = new TEEvent(DISPCORE_EVENT_RUN + 36, "Map Downloaded.");
        public static TEEvent MAP_UPLOADED = new TEEvent(DISPCORE_EVENT_RUN + 37, "Map Upload.");

        public static TEEvent MAP_RECOVER_PROMPT = new TEEvent(DISPCORE_EVENT_RUN + 40, "Map Recover Prompt.");
        public static TEEvent MAP_RECOVER_UPLOADED = new TEEvent(DISPCORE_EVENT_RUN + 41, "Map Recover Uploaded.");
        public static TEEvent MAP_RECOVER_UPLOAD_CANCEL = new TEEvent(DISPCORE_EVENT_RUN + 42, "Map Recover Cancelled.");
        public static TEEvent MAP_RECOVER_DELETED = new TEEvent(DISPCORE_EVENT_RUN + 43, "Map Recover Deleted.");

        public static TEEvent SUBSTRATE_START = new TEEvent(DISPCORE_EVENT_RUN + 50, "Substrate Start.");
        public static TEEvent SUBSTRATE_END = new TEEvent(DISPCORE_EVENT_RUN + 51, "Substrate End.");

        public static TEEvent TERMINAL_MESSAGE_ACK = new TEEvent(DISPCORE_EVENT_RUN + 80, "Terminal Message Acknowledge.");

        public static TEEvent OSRAMICC = new TEEvent(DISPCORE_EVENT_RUN + 100, "OsramICC.");

        #region MHS_EVENT = 4000
        const int MHS_EVENT = 4000;
        public static TEEvent BOARD_ARRIVED_BUFFER1 = new TEEvent(MHS_EVENT + 100, "Board Arrived Buffer1.");
        public static TEEvent BOARD_ARRIVED_BUFFER2 = new TEEvent(MHS_EVENT + 102, "Board Arrived Buffer2.");
        public static TEEvent BOARD_ARRIVED_PRE_STATION = new TEEvent(MHS_EVENT + 104, "Board Arrived Pre Station.");
        public static TEEvent BOARD_ARRIVED_DISP_STATION = new TEEvent(MHS_EVENT + 106, "Board Arrived Disp Station.");
        public static TEEvent BOARD_ARRIVED_POST_STATION = new TEEvent(MHS_EVENT + 108, "Board Arrived Post Station.");
        public static TEEvent BOARD_ARRIVED_OUT_STATION = new TEEvent(MHS_EVENT + 110, "Board Arrived Out Station.");
        public static TEEvent BOARD_REVERSE_ARRIVED_IN_STATION = new TEEvent(MHS_EVENT + 112, "Board Reverse Arrived In Station.");

        public static TEEvent BOARD_PUSH_ARRIVED_IN_STATION = new TEEvent(MHS_EVENT + 120, "Board Push Arrived In Station.");
        public static TEEvent BOARD_MANUAL_ARRIVED_IN_STATION = new TEEvent(MHS_EVENT + 121, "Board Manual Arrived In Station.");
        public static TEEvent BOARD_SMEMA_ARRIVED_IN_STATION = new TEEvent(MHS_EVENT + 122, "Board SMEMA Arrived In Station.");

        public static TEEvent BOARD_SEND_OUT_CONV2 = new TEEvent(MHS_EVENT + 130, "Board Send Out Conv2.");
        public static TEEvent BOARD_SENT_OUT_TO_MAGAZINE = new TEEvent(MHS_EVENT + 131, "Board Send Out to Magazine.");
        public static TEEvent BOARD_SEND_OUT_SMEMA = new TEEvent(MHS_EVENT + 132, "Board Send Out SMEMA.");
        public static TEEvent BOARD_REVERSE = new TEEvent(MHS_EVENT + 133, "Board Reverse.");
        public static TEEvent BOARD_REVERSE_SEND_OUT_SMEMA = new TEEvent(MHS_EVENT + 134, "Board Reverse Send Out SMEMA.");

        public static TEEvent MANUAL_RETURN = new TEEvent(MHS_EVENT + 200, "Manual Return");
        public static TEEvent MANUAL_LOADBUF1 = new TEEvent(MHS_EVENT + 201, "Manual Load Buffer1");
        public static TEEvent MANUAL_LOADBUF2 = new TEEvent(MHS_EVENT + 202, "Manual Load Buffer2");
        public static TEEvent MANUAL_LOADPRE = new TEEvent(MHS_EVENT + 203, "Manual Load Pre");
        public static TEEvent MANUAL_LOADPRO = new TEEvent(MHS_EVENT + 204, "Manual Load Pro");
        public static TEEvent MANUAL_UNLOAD = new TEEvent(MHS_EVENT + 205, "Manual Unload");
        public static TEEvent MANUAL_LOADPOS = new TEEvent(MHS_EVENT + 206, "Manual Load Pos");

        public static TEEvent PREHEAT_START = new TEEvent(MHS_EVENT + 500, "Preheat Start.");
        public static TEEvent PREHEAT_END = new TEEvent(MHS_EVENT + 501, "Preheat End.");
        public static TEEvent DISPHEAT_START = new TEEvent(MHS_EVENT + 510, "Dispense Heat Start.");
        public static TEEvent DISPHEAT_END = new TEEvent(MHS_EVENT + 511, "Dispense Heat End.");
        public static TEEvent POSHEAT_START = new TEEvent(MHS_EVENT + 512, "Post Heat Start.");
        public static TEEvent POSHEAT_END = new TEEvent(MHS_EVENT + 513, "Post Heat End.");
        #endregion
    }

    public class TEVID//SV, DV, EC Represents real-time status variables of equipment.
    {
        public int Code = 0;
        public string Desc = "";
        public TEVID(int code, string desc)
        {
            Code = code;
            Desc = desc;
        }
        public static List<string> List()
        {
            var sVIDList = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEVID))
                .Select(x => (TEVID)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var sVID in sVIDList)
            {
                list.Add($"{sVID.Code:d4},{sVID.Desc}");
            }
            return list;
        }
        public dynamic Value
        {
            get
            {
                switch (Code)
                {
                    #region 10000
                    case 11001:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(0);
                            }
                        }
                        break;
                    case 11002:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(1);
                            }
                        }
                        break;
                    case 11003:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(2);
                            }
                        }
                        break;
                    case 11004:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(3);
                            }
                        }
                        break;

                    case 11010:
                        return TaskWeight.CurrentCal[0];
                    case 11011:
                        return TaskWeight.CurrentCal[1];
                    case 11020:
                        return TaskWeight.list_WM_MeasWeight.Min();
                    case 11021:
                        return TaskWeight.list_WM_MeasWeight.Max();
                    case 11022:
                        return TaskWeight.list_WM_MeasWeight.Average();
                    case 11023:
                        {
                            NSW.Net.Stats Stat = new NSW.Net.Stats();
                            return Stat.StDev(TaskWeight.list_WM_MeasWeight);
                        }
                    case 11030:
                        return TaskFlowRate.Value[0];
                    case 11031:
                        return TaskFlowRate.Value[1];
                    case 11040:
                        return TaskWeightMeas.list_Weight.Min();
                    case 11041:
                        return TaskWeightMeas.list_Weight.Max();
                    case 11042:
                        return TaskWeightMeas.list_Weight.Average();
                    case 11043:
                        {
                            NSW.Net.Stats Stat = new NSW.Net.Stats();
                            return Stat.StDev(TaskWeightMeas.list_Weight);
                        }
                    #endregion
                    #region 20000
                    case 21000:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(0);
                            }
                        }
                        break;
                    case 21001:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(1);
                            }
                        }
                        break;
                    case 21002:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(2);
                            }
                        }
                        break;
                    case 21003:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(3);
                            }
                        }
                        break;
                    case 21010:
                        return DispProg.Disp_Weight[0];
                    case 21011:
                        return DispProg.Disp_Weight[1];
                    case 21100:
                        return LotInfo2.sOperatorID;
                    case 21101:
                        return LotInfo2.LotNumber;
                    case 21102:
                        return LotInfo2.Osram.ElevenSeries;
                    case 21103:
                        return LotInfo2.Osram.DAStartNumber;
                    case 21104:
                        return LotInfo2.Osram.F5Value;
                    case 20005:
                        return LotInfo2.Osram.F6Value;
                    case 21106:
                        return LotInfo2.Osram.F7Value;
                    case 21107:
                        return LotInfo2.Osram.F8Value;
                    case 21200:
                        return GDefine.DeviceRecipe;
                    case 21201:
                        return GDefine.ProgRecipeName;
                    case 21202:
                        return GDefine.MHSRecipeName;
                    case int n when (n >= 22000 && n < 22016):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22000);
                            return Model.DispGap;
                        }
                    case int n when (n >= 22100 && n < 22116):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22100);
                            return Model.DnWait;
                        }
                    case int n when (n >= 22200 && n < 22216):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22200);
                            return Model.StartDelay;
                        }
                    case int n when (n >= 22300 && n < 22316):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22300);
                            return Model.LineSpeed;
                        }
                    case int n when (n >= 22400 && n < 22416):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22400);
                            return Model.EndDelay;
                        }
                    case int n when (n >= 22500 && n < 22516):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22500);
                            return Model.PostWait;
                        }
                    #endregion
                    #region 30000
                    case 31001:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.AL1_Dev(0);
                            }
                        }
                        break;
                    case 31002:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.AL1_Dev(1);
                            }
                        }
                        break;
                    case 31003:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.AL1_Dev(2);
                            }
                        }
                        break;
                    case 31004:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.AL1_Dev(3);
                            }
                        }
                        break;
                        #endregion
                }
                return 0;
            }
            set
            {
                switch (Code)
                {
                    #region 20000
                    case 21000:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[0] = value;
                                TempCtrl.Set(0, (int)DispProg.HeaterSV[0], (int)DispProg.HeaterRange[0]);
                            }
                        }
                        break;
                    case 21001:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[1] = value;
                                TempCtrl.Set(1, (int)DispProg.HeaterSV[1], (int)DispProg.HeaterRange[1]);
                            }
                        }
                        break;
                    case 21002:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[2] = value;
                                TempCtrl.Set(2, (int)DispProg.HeaterSV[2], (int)DispProg.HeaterRange[2]);
                            }
                        }
                        break;
                    case 21003:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[3] = value;
                                TempCtrl.Set(3, (int)DispProg.HeaterSV[3], (int)DispProg.HeaterRange[3]);
                            }
                        }
                        break;
                    case 21010:
                        DispProg.Target_Weight = value;
                        DispProg.Disp_Weight[0] = DispProg.Target_Weight;
                        if (DispProg.Disp_Weight[0] > 0) TaskDisp.PP_SetWeight(DispProg.Disp_Weight, true);
                        break;
                    case 21011:
                        DispProg.Target_Weight = value;
                        DispProg.Disp_Weight[1] = DispProg.Target_Weight;
                        if (DispProg.Disp_Weight[1] > 0) TaskDisp.PP_SetWeight(DispProg.Disp_Weight, true);
                        break;
                    case 21100:
                        LotInfo2.sOperatorID = value;
                        break;
                    case 21101:
                        LotInfo2.LotNumber = value;
                        break;
                    case 21102:
                        LotInfo2.Osram.ElevenSeries = value;
                        break;
                    case 21103:
                        LotInfo2.Osram.DAStartNumber = value;
                        break;
                    case 21104:
                        LotInfo2.Osram.F5Value = value;
                        break;
                    case 20005:
                        LotInfo2.Osram.F6Value = value;
                        break;
                    case 21106:
                        LotInfo2.Osram.F7Value = value;
                        break;
                    case 21107:
                        LotInfo2.Osram.F8Value = value;
                        break;
                    case 21200:
                        GDefine.DeviceRecipe = value;
                        break;
                    case 21201:
                        GDefine.ProgRecipeName = value;
                        break;
                    case 21202:
                        GDefine.MHSRecipeName = value;
                        break;
                    case int n when (n >= 22000 && n < 22016):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22000);
                            Model.DispGap = value;
                            break;
                        }
                    case int n when (n >= 22100 && n < 22116):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22100);
                            Model.DnWait = (int)value;
                            break;
                        }
                    case int n when (n >= 22200 && n < 22216):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22200);
                            Model.StartDelay = (int)value;
                            break;
                        }
                    case int n when (n >= 22300 && n < 22316):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22300);
                            Model.LineSpeed = value;
                            break;
                        }
                    case int n when (n >= 22400 && n < 22416):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22400);
                            Model.EndDelay = (int)value;
                            break;
                        }
                    case int n when (n >= 22500 && n < 22516):
                        {
                            TModelPara Model = new TModelPara(DispProg.ModelList, n - 22500);
                            Model.PostWait = (int)value;
                            break;
                        }
                    #endregion
                    #region 30000
                    case 31001:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterRange[0] = value;
                                TempCtrl.Set(0, (int)DispProg.HeaterSV[0], (int)DispProg.HeaterRange[0]);
                            }
                        }
                        break;
                    case 31002:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterRange[1] = value;
                                TempCtrl.Set(1, (int)DispProg.HeaterSV[1], (int)DispProg.HeaterRange[1]);
                            }
                        }
                        break;
                    case 31003:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterRange[2] = value;
                                TempCtrl.Set(2, (int)DispProg.HeaterSV[2], (int)DispProg.HeaterRange[2]);
                            }
                        }
                        break;
                    case 31004:
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterRange[3] = value;
                                TempCtrl.Set(3, (int)DispProg.HeaterSV[3], (int)DispProg.HeaterRange[3]);
                            }
                        }
                        break;
                        #endregion
                }
            }

        }
    }
    public class VID
    {
        // Unique ID ranges prevent conflicts between SVID, DVID, ECID, ALID, and CEID.
        // Each category serves a different purpose (status, control, configuration, alarm, event).
        #region 10000 SVID
        public static TEVID TEMPCTRL1PV = new TEVID(11001, "Temperature Control PV 1.");
        public static TEVID TEMPCTRL2PV = new TEVID(11002, "Temperature Control PV 2.");
        public static TEVID TEMPCTRL3PV = new TEVID(11003, "Temperature Control PV 3.");
        public static TEVID TEMPCTRL4PV = new TEVID(11004, "Temperature Control PV 4.");

        public static TEVID PUMP1_WEIGHT_CAL_VALUE = new TEVID(11010, "Pump1 Weight Calibration Value.");
        public static TEVID PUMP2_WEIGHT_CAL_VALUE = new TEVID(11011, "Pump2 Weight Calibration Value.");

        public static TEVID PUMP_WEIGHT_MEAS_MIN = new TEVID(11020, "Pump Weight Meas Minimum Value.");
        public static TEVID PUMP_WEIGHT_MEAS_MAX = new TEVID(11021, "Pump Weight Meas Maximum Value.");
        public static TEVID PUMP_WEIGHT_MEAS_AVE = new TEVID(11022, "Pump Weight Meas Average Value.");
        public static TEVID PUMP_WEIGHT_MEAS_STDEV = new TEVID(11023, "Pump Weight Meas Standard Deviation Value.");

        public static TEVID PUMP1_FLOWRATE_VALUE = new TEVID(11030, "Pump1 FlowRate Value.");
        public static TEVID PUMP2_FLOWRATE_VALUE = new TEVID(11031, "Pump2 FlowRate Value.");

        public static TEVID PUMP_WEIGHT_MEAS2_MIN = new TEVID(11040, "Pump Weight Meas2 Minimum Value.");
        public static TEVID PUMP_WEIGHT_MEAS2_MAX = new TEVID(11041, "Pump Weight Meas2 Maximum Value.");
        public static TEVID PUMP_WEIGHT_MEAS2_AVE = new TEVID(11042, "Pump Weight Meas2 Average Value.");
        public static TEVID PUMP_WEIGHT_MEAS2_STDEV = new TEVID(11043, "Pump Weight Meas2 Standard Deviation Value.");
        #endregion

        #region 20000 DVID
        public static TEVID TEMPCTRL1SV = new TEVID(21000, "Temperature Control SV 1.");
        public static TEVID TEMPCTRL2SV = new TEVID(21001, "Temperature Control SV 2.");
        public static TEVID TEMPCTRL3SV = new TEVID(21002, "Temperature Control SV 3.");
        public static TEVID TEMPCTRL4SV = new TEVID(21003, "Temperature Control SV 4.");

        public static TEVID TARGETWEIGHT1 = new TEVID(21010, "Target Weight 1.");
        public static TEVID TARGETWEIGHT2 = new TEVID(21011, "Target Weight 2.");

        public static TEVID ENTRY_EMPLOYEEID = new TEVID(21100, "Entry Employee ID.");
        public static TEVID ENTRY_LOTNUMBER = new TEVID(21101, "Entry Lot Number.");
        public static TEVID ENTRY_L11SERIES = new TEVID(21102, "Entry 11 Series.");
        public static TEVID ENTRY_DASTARTNUMBER = new TEVID(21103, "Entry DA Start Number.");
        public static TEVID ENTRY_FIELD5 = new TEVID(21104, "Entry Field5.");
        public static TEVID ENTRY_FIELD6 = new TEVID(21105, "Entry Field6.");
        public static TEVID ENTRY_FIELD7 = new TEVID(21106, "Entry Field7.");
        public static TEVID ENTRY_FIELD8 = new TEVID(21107, "Entry Field8.");

        public static TEVID DEVICE = new TEVID(21200, "Device.");
        public static TEVID RECIPE = new TEVID(21201, "Recipe.");
        public static TEVID MHSRECIPE = new TEVID(21202, "MHS Recipe.");
        #region Model
        public static TEVID MODEL1DISPGAP = new TEVID(22000, "Model1 Disp Gap.");
        public static TEVID MODEL2DISPGAP = new TEVID(22001, "Model2 Disp Gap.");
        public static TEVID MODEL3DISPGAP = new TEVID(22002, "Model3 Disp Gap.");
        public static TEVID MODEL4DISPGAP = new TEVID(22003, "Model4 Disp Gap.");
        public static TEVID MODEL5DISPGAP = new TEVID(22004, "Model5 Disp Gap.");
        public static TEVID MODEL6DISPGAP = new TEVID(22005, "Model6 Disp Gap.");
        public static TEVID MODEL7DISPGAP = new TEVID(22006, "Model7 Disp Gap.");
        public static TEVID MODEL8DISPGAP = new TEVID(22007, "Model8 Disp Gap.");
        public static TEVID MODEL9DISPGAP = new TEVID(22008, "Model9 Disp Gap.");
        public static TEVID MODEL10DISPGAP = new TEVID(22009, "Model10 Disp Gap.");
        public static TEVID MODEL11DISPGAP = new TEVID(22010, "Model11 Disp Gap.");
        public static TEVID MODEL12DISPGAP = new TEVID(22011, "Model12 Disp Gap.");
        public static TEVID MODEL13DISPGAP = new TEVID(22012, "Model13 Disp Gap.");
        public static TEVID MODEL14DISPGAP = new TEVID(22013, "Model14 Disp Gap.");
        public static TEVID MODEL15DISPGAP = new TEVID(22014, "Model15 Disp Gap.");
        public static TEVID MODEL16DISPGAP = new TEVID(22015, "Model16 Disp Gap.");

        public static TEVID MODEL1DNWAIT = new TEVID(22100, "Model1 Down Wait.");
        public static TEVID MODEL2DNWAIT = new TEVID(22101, "Model2 Down Wait.");
        public static TEVID MODEL3DNWAIT = new TEVID(22102, "Model3 Down Wait.");
        public static TEVID MODEL4DNWAIT = new TEVID(22103, "Model4 Down Wait.");
        public static TEVID MODEL5DNWAIT = new TEVID(22104, "Model5 Down Wait.");
        public static TEVID MODEL6DNWAIT = new TEVID(22105, "Model6 Down Wait.");
        public static TEVID MODEL7DNWAIT = new TEVID(22106, "Model7 Down Wait.");
        public static TEVID MODEL8DNWAIT = new TEVID(22107, "Model8 Down Wait.");
        public static TEVID MODEL9DNWAIT = new TEVID(22108, "Model9 Down Wait.");
        public static TEVID MODEL10DNWAIT = new TEVID(22109, "Model10 Down Wait.");
        public static TEVID MODEL11DNWAIT = new TEVID(22110, "Model11 Down Wait.");
        public static TEVID MODEL12DNWAIT = new TEVID(22111, "Model12 Down Wait.");
        public static TEVID MODEL13DNWAIT = new TEVID(22112, "Model13 Down Wait.");
        public static TEVID MODEL14DNWAIT = new TEVID(22113, "Model14 Down Wait.");
        public static TEVID MODEL15DNWAIT = new TEVID(22114, "Model15 Down Wait.");
        public static TEVID MODEL16DNWAIT = new TEVID(22115, "Model16 Down Wait.");

        public static TEVID MODEL1STARTDELAY = new TEVID(22200, "Model1 Start Delay.");
        public static TEVID MODEL2STARTDELAY = new TEVID(22201, "Model2 Start Delay.");
        public static TEVID MODEL3STARTDELAY = new TEVID(22202, "Model3 Start Delay.");
        public static TEVID MODEL4STARTDELAY = new TEVID(22203, "Model4 Start Delay.");
        public static TEVID MODEL5STARTDELAY = new TEVID(22204, "Model5 Start Delay.");
        public static TEVID MODEL6STARTDELAY = new TEVID(22205, "Model6 Start Delay.");
        public static TEVID MODEL7STARTDELAY = new TEVID(22206, "Model7 Start Delay.");
        public static TEVID MODEL8STARTDELAY = new TEVID(22207, "Model8 Start Delay.");
        public static TEVID MODEL9STARTDELAY = new TEVID(22208, "Model9 Start Delay.");
        public static TEVID MODEL10STARTDELAY = new TEVID(22209, "Model10 Start Delay.");
        public static TEVID MODEL11STARTDELAY = new TEVID(22210, "Model11 Start Delay.");
        public static TEVID MODEL12STARTDELAY = new TEVID(22211, "Model12 Start Delay.");
        public static TEVID MODEL13STARTDELAY = new TEVID(22212, "Model13 Start Delay.");
        public static TEVID MODEL14STARTDELAY = new TEVID(22213, "Model14 Start Delay.");
        public static TEVID MODEL15STARTDELAY = new TEVID(22214, "Model15 Start Delay.");
        public static TEVID MODEL16STARTDELAY = new TEVID(22215, "Model16 Start Delay.");

        public static TEVID MODEL1LINESPEED = new TEVID(22300, "Model1 Line Speed.");
        public static TEVID MODEL2LINESPEED = new TEVID(22301, "Model2 Line Speed.");
        public static TEVID MODEL3LINESPEED = new TEVID(22302, "Model3 Line Speed.");
        public static TEVID MODEL4LINESPEED = new TEVID(22303, "Model4 Line Speed.");
        public static TEVID MODEL5LINESPEED = new TEVID(22304, "Model5 Line Speed.");
        public static TEVID MODEL6LINESPEED = new TEVID(22305, "Model6 Line Speed.");
        public static TEVID MODEL7LINESPEED = new TEVID(22306, "Model7 Line Speed.");
        public static TEVID MODEL8LINESPEED = new TEVID(22307, "Model8 Line Speed.");
        public static TEVID MODEL9LINESPEED = new TEVID(22308, "Model9 Line Speed.");
        public static TEVID MODEL10LINESPEED = new TEVID(22309, "Model10 Line Speed.");
        public static TEVID MODEL11LINESPEED = new TEVID(22310, "Model11 Line Speed.");
        public static TEVID MODEL12LINESPEED = new TEVID(22311, "Model12 Line Speed.");
        public static TEVID MODEL13LINESPEED = new TEVID(22312, "Model13 Line Speed.");
        public static TEVID MODEL14LINESPEED = new TEVID(22313, "Model14 Line Speed.");
        public static TEVID MODEL15LINESPEED = new TEVID(22314, "Model15 Line Speed.");
        public static TEVID MODEL16LINESPEED = new TEVID(22315, "Model16 Line Speed.");

        public static TEVID MODEL1ENDDELAY = new TEVID(22400, "Model1 End Delay.");
        public static TEVID MODEL2ENDDELAY = new TEVID(22401, "Model2 End Delay.");
        public static TEVID MODEL3ENDDELAY = new TEVID(22402, "Model3 End Delay.");
        public static TEVID MODEL4ENDDELAY = new TEVID(22403, "Model4 End Delay.");
        public static TEVID MODEL5ENDDELAY = new TEVID(22404, "Model5 End Delay.");
        public static TEVID MODEL6ENDDELAY = new TEVID(22405, "Model6 End Delay.");
        public static TEVID MODEL7ENDDELAY = new TEVID(22406, "Model7 End Delay.");
        public static TEVID MODEL8ENDDELAY = new TEVID(22407, "Model8 End Delay.");
        public static TEVID MODEL9ENDDELAY = new TEVID(22408, "Model9 End Delay.");
        public static TEVID MODEL10ENDDELAY = new TEVID(22409, "Model10 End Delay.");
        public static TEVID MODEL11ENDDELAY = new TEVID(22410, "Model11 End Delay.");
        public static TEVID MODEL12ENDDELAY = new TEVID(22411, "Model12 End Delay.");
        public static TEVID MODEL13ENDDELAY = new TEVID(22412, "Model13 End Delay.");
        public static TEVID MODEL14ENDDELAY = new TEVID(22413, "Model14 End Delay.");
        public static TEVID MODEL15ENDDELAY = new TEVID(22414, "Model15 End Delay.");
        public static TEVID MODEL16ENDDELAY = new TEVID(22415, "Model16 End Delay.");

        public static TEVID MODEL1POSTWAIT = new TEVID(22500, "Model1 Post Wait.");
        public static TEVID MODEL2POSTWAIT = new TEVID(22501, "Model2 Post Wait.");
        public static TEVID MODEL3POSTWAIT = new TEVID(22502, "Model3 Post Wait.");
        public static TEVID MODEL4POSTWAIT = new TEVID(22503, "Model4 Post Wait.");
        public static TEVID MODEL5POSTWAIT = new TEVID(22504, "Model5 Post Wait.");
        public static TEVID MODEL6POSTWAIT = new TEVID(22505, "Model6 Post Wait.");
        public static TEVID MODEL7POSTWAIT = new TEVID(22506, "Model7 Post Wait.");
        public static TEVID MODEL8POSTWAIT = new TEVID(22507, "Model8 Post Wait.");
        public static TEVID MODEL9POSTWAIT = new TEVID(22508, "Model9 Post Wait.");
        public static TEVID MODEL10POSTWAIT = new TEVID(22509, "Model10 Post Wait.");
        public static TEVID MODEL11POSTWAIT = new TEVID(22510, "Model11 Post Wait.");
        public static TEVID MODEL12POSTWAIT = new TEVID(22511, "Model12 Post Wait.");
        public static TEVID MODEL13POSTWAIT = new TEVID(22512, "Model13 Post Wait.");
        public static TEVID MODEL14POSTWAIT = new TEVID(22513, "Model14 Post Wait.");
        public static TEVID MODEL15POSTWAIT = new TEVID(22514, "Model15 Post Wait.");
        public static TEVID MODEL16POSTWAIT = new TEVID(22515, "Model16 Post Wait.");
        #endregion
        #endregion

        #region 30000 ECID
        public static TEVID TEMPCTRL1TOL = new TEVID(31001, "Temperature Control Tol 1.");
        public static TEVID TEMPCTRL2TOL = new TEVID(31002, "Temperature Control Tol 2.");
        public static TEVID TEMPCTRL3TOL = new TEVID(31003, "Temperature Control Tol 3.");
        public static TEVID TEMPCTRL4TOL = new TEVID(31004, "Temperature Control Tol 4.");
        #endregion
    }

    public class RMCD
    {
        public const int PP_SELECT = 0;
        public const int START = 10;
        public const int STOP = 11;
        public const int NEWLOT = 20;
        public static List<string> List()
        {
            var rMCDList = typeof(RMCD).GetFields(BindingFlags.Public | BindingFlags.Static).ToArray();

            List<string> list = new List<string>();
            foreach (var rmcd in rMCDList)
            {
                list.Add(rmcd.Name);
            }
            return list;
        }
    }
}
