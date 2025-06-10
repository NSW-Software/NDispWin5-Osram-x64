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
        public List<int> VIDs = new List<int>();//related VIDs

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
            //GDefine.sgc2.SendEvent(this.Code + "," + this.Name + "," + paraName + "," + paraValue);
            TFSecsGem.Send($"{nameof(StreamFunc.ERS)},{this.Code},{this.Name},{paraName},{paraValue}");
        }
        public void Set()
        {
            Event.LastEvent = this;

            if (!GDefineN.EnableEventDebugLog && Code == Event.DEBUG_INFO.Code) return;

            if (this.Name.Length == 0) this.Name = this.Name.ToString();

            Log.AddToEventLog(this.Code, this.Name);
            //GDefine.sgc2.SendEvent(this.Code + "," + this.Name);
            TFSecsGem.Send($"{nameof(StreamFunc.ERS)},{this.Code}");
        }

        public static List<string> CEID_List()
        {
            var ceIDlist = typeof(Event).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEEvent))
                .Select(x => (TEEvent)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var para in ceIDlist)
            {
                string vids = para.VIDs == null ? "" : $",VID:{string.Join(",", para.VIDs)}"; 
                list.Add($"{para.Code:d4},{para.Name}{vids}");
            }

            return list;
        }
    }
    class Event
    {
        internal static TEEvent LastEvent = new TEEvent(0, "");

        #region 1000
        public static TEEvent APP_START = new TEEvent(1000, "App StartUp.");
        public static TEEvent APP_CLOSE = new TEEvent(1001, "App Close.");
        public static TEEvent ERROR = new TEEvent(1002, "Error.");

        public static TEEvent CTRL = new TEEvent(1003, "Control.");//log control
        public static TEEvent APP_INFO = new TEEvent(1004, "App Info.");
        public static TEEvent DEBUG_INFO = new TEEvent(1006, "Debug Info.");
        public static TEEvent WARNING = new TEEvent(1007, "Warning.");

        public static TEEvent OP_START_RUN = new TEEvent(1010, "Auto Start Run.");
        public static TEEvent OP_STOP_RUN = new TEEvent(1011, "Auto Stop Run.");
        public static TEEvent OP_FINISH_RUN = new TEEvent(1012, "Auto Run Finished.");
        public static TEEvent OP_INIT_GANTRY_START = new TEEvent(1020, "App Init Gantry Start.");
        public static TEEvent OP_INIT_GANTRY_COMPLETE = new TEEvent(1021, "App Init Gantry Complete.");
        public static TEEvent OP_INIT_CONV_START = new TEEvent(1022, "App Init Conveyor Start.");
        public static TEEvent OP_INIT_CONV_COMPLETE = new TEEvent(1023, "App Init Conveyor Complete.");
        public static TEEvent OP_INIT_LR_LINE_START = new TEEvent(1024, "App Init LR Line Start.");
        public static TEEvent OP_INIT_LR_LINE_COMPLETE = new TEEvent(1025, "App Init LR Line Complete.");
        public static TEEvent OP_MAIN_LOGIN = new TEEvent(1030, "Main Login.");
        public static TEEvent OP_PROMPT_LOGIN = new TEEvent(1031, "Prompt Login.");

        public static TEEvent FAULT = new TEEvent(1050, "Fault.");
        public static TEEvent NOTIFICATION = new TEEvent(1051, "Notification.");
        public static TEEvent CONFIRMATION = new TEEvent(1052, "Confirmation.");
        public static TEEvent CUSTOM1 = new TEEvent(1053, "Custom1.");
        public static TEEvent CUSTOM2 = new TEEvent(1054, "Custom2.");

        public static TEEvent CAMERA_INFO = new TEEvent(1150, "Camera Info.");
        #endregion
        #region 2000
        public static TEEvent TEST_EVENT = new TEEvent(2000, "Test Event.");

        public static TEEvent DISPTOOLS_TEACH_NEEDLE = new TEEvent(2110, "DispTools Teach Needle.");
        public static TEEvent DISPTOOLS_TEACH_NEEDLE_CANCEL = new TEEvent(2111, "DispTools Teach Needle Cancel.");
        public static TEEvent DISPTOOLS_GOTO_PUMP_MAINT_POS = new TEEvent(2112, "DispTools Goto Pump Maint Pos.");
        public static TEEvent DISPTOOLS_GOTO_MACHINE_MAINT_POS = new TEEvent(2113, "DispTools Goto Machine Maint Pos.");
        public static TEEvent DISPTOOLS_CLEAN = new TEEvent(2115, "DispTools Clean.");
        public static TEEvent DISPTOOLS_PURGE = new TEEvent(2116, "DispTools Purge.");
        public static TEEvent DISPTOOLS_FLUSH = new TEEvent(2117, "DispTools Flush.");
        public static TEEvent DISPTOOLS_CLEANPURGE_CANCEL = new TEEvent(2118, "DispTools Clean Purge Cancel.");
        public static TEEvent DISPTOOLS_WEIGHT_ADJUST = new TEEvent(2120, "DispTools Weight Adjust.");
        public static TEEvent DISPTOOLS_WEIGHT_CALIBRATE = new TEEvent(2121, "DispTools Weight Calibrate.");
        public static TEEvent DISPTOOLS_WEIGHT_MEASURE = new TEEvent(2122, "DispTools Weight Measure.");
        public static TEEvent DISPTOOLS_WEIGHT_CANCEL = new TEEvent(2123, "DispTools Weight Cancel.");
        public static TEEvent DISPTOOLS_ADJ_MATERIAL_TIMER = new TEEvent(2125, "DispTools Adjust Material Timer.");
        public static TEEvent DISPTOOLS_ADJ_MATERIAL_EXP = new TEEvent(2126, "DispTools Adjust Material Expiry.");
        public static TEEvent DISPTOOLS_ADJ_MATERIAL_EXP_CANCEL = new TEEvent(2126, "DispTools Adjust Material Expiry Cancel.");
        public static TEEvent DISPTOOLS_FORCE_SINGLE = new TEEvent(2130, "DispTools Force Single.");
        public static TEEvent DISPTOOLS_PUMP_ADJUST = new TEEvent(2132, "DispTools Pump Adjust.");
        public static TEEvent DISPTOOLS_ORIGIN_ADJUST = new TEEvent(2133, "DispTools Origin Adjust.");
        public static TEEvent DISPTOOLS_ORIGIN = new TEEvent(2134, "DispTools Origin.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_1 = new TEEvent(2140, "DispTools Pump Action 1.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_2 = new TEEvent(2141, "DispTools Pump Action 2.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_3 = new TEEvent(2142, "DispTools Pump Action 3.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_4 = new TEEvent(2143, "DispTools Pump Action 4.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_5 = new TEEvent(2144, "DispTools Pump Action 5.");
        public static TEEvent DISPTOOLS_PUMP_ACTION_CANCEL = new TEEvent(2145, "DispTools Pump Action Cancel.");
        public static TEEvent DISPTOOLS_START_IDLE = new TEEvent(2150, "DispTools Start Idle.");
        public static TEEvent DISPTOOLS_PURGE_STAGE = new TEEvent(2151, "DispTools Purge Stage.");
        public static TEEvent DISPTOOLS_VIEW = new TEEvent(2152, "DispTools View.");
        public static TEEvent DISPTOOLS_ADJ_VALVE_TIMER = new TEEvent(2125, "DispTools Adjust Valve Timer.");

        public static TEEvent OP_WEIGHT_ADJUST = new TEEvent(2200, "Weight Adjust.");
        public static TEEvent OP_WEIGHT_CALIBRATION = new TEEvent(2201, "Weight Calibration.", new List<int> { 11010, 11011 });
        public static TEEvent OP_WEIGHT_MEASURE = new TEEvent(2202, "Weight Measure.", new List<int> { 11020, 11021, 11022, 11023 });
        public static TEEvent OP_FLOWRATE1_CALIBRATION = new TEEvent(2203, "Flowrate1 Calibration.", new List<int> { 11030 });
        public static TEEvent OP_FLOWRATE2_CALIBRATION = new TEEvent(2204, "Flowrate2 Calibration.", new List<int> { 11031 });
        public static TEEvent OP_WEIGHT1_MEASURE = new TEEvent(2205, "Weight1 Measure.", new List<int> { 11040, 11041, 11042, 11043 });
        public static TEEvent OP_WEIGHT2_MEASURE = new TEEvent(2206, "Weight2 Measure.", new List<int> { 11040, 11041, 11042, 11043 });
        public static TEEvent OP_IDLE_PURGE_START = new TEEvent(2210, "Start Idle Purge.");
        public static TEEvent OP_IDLE_PURGE_STOP = new TEEvent(2211, "Stop Idle Purge.");
        public static TEEvent OP_SET_WEIGHT = new TEEvent(2212, "Set Weight.");

        public static TEEvent OP_DISP_LOAD_DEVICE = new TEEvent(2215, "Load Device.", new List<int> { 21200, 21201, 21202 });
        public static TEEvent OP_DISP_LOAD_DISP_RECIPE = new TEEvent(2216, "Load Disp Recipe.", new List<int> { 21201 });
        public static TEEvent OP_DISP_LOAD_MHS_RECIPE = new TEEvent(2217, "Load MHS Recipe.", new List<int> { 21202 });
        public static TEEvent OP_DISP_AUTO_LOAD_DEVICE_INVALID = new TEEvent(2218, "Auto Load Device is Invalid.");
        public static TEEvent OP_DISP_AUTO_LOAD_DEVICE_NO_FOUND = new TEEvent(2219, "Auto Load Device not Found.");
        public static TEEvent OP_DISP_AUTO_LOAD_SUCCESSFUL = new TEEvent(2220, "Auto Load Device successful.", new List<int> { 21200, 21201, 21202 });

        public static TEEvent OP_EXT_VISION_OK = new TEEvent(2230, "Ext Vision Inspection OK.");
        public static TEEvent OP_EXT_VISION_NG = new TEEvent(2231, "Ext Vision Inspection NG.");

        public static TEEvent OP_LMDS_TESTER_SEQ = new TEEvent(2290, "Lmds Tester Seq.");
        public static TEEvent OP_CHECK_MENISCUS = new TEEvent(2291, "Check Meniscus.");
        public static TEEvent OP_CHECK_MENISCUS_OOS = new TEEvent(2292, "Check Meniscus OOS.");

        public static TEEvent OP_LOT_START = new TEEvent(2295, "Lot Start.", new List<int> { 21100, 21101, 21102, 21103, 21104, 21105, 21106, 21107, 21108 });
        public static TEEvent OP_LOT_END = new TEEvent(2296, "Lot End.", new List<int> { 21100, 21101, 21102, 21103, 21104, 21105, 21106, 21107, 21108 });

        public static TEEvent CLEAN_NEEDLE = new TEEvent(2300, "Clean Needle.");
        public static TEEvent PURGE_NEEDLE = new TEEvent(2301, "Purge Needle.");
        public static TEEvent FLUSH_NEEDLE = new TEEvent(2301, "Flush Needle.");

        public static TEEvent PROG_UNLOCK_Z = new TEEvent(2400, "Program Mode Unlock Z.");

        public static TEEvent SETUP_BYPASS_TEACH_NEEDLE = new TEEvent(2500, "ByPass Teach Needle.");
        public static TEEvent TEACH_NEEDLE_OFST = new TEEvent(2501, "Teach Needle Offset.");
        public static TEEvent SETUP_EVENT = new TEEvent(2502, "Setup.");
        public static TEEvent SETUP_HEAD1_OFST_UPDATE = new TEEvent(2550, "Head1 Offset Update.");
        public static TEEvent SETUP_HEAD2_OFST_UPDATE = new TEEvent(2551, "Head2 Offset Update.");
        public static TEEvent SETUP_LASER_OFST_UPDATE = new TEEvent(2553, "Laser Offset Update.");
        public static TEEvent SETUP_REFZ_UPDATE = new TEEvent(2554, "RefZ Update.");
        public static TEEvent SETUP_TOUCH_POS_UPDATE = new TEEvent(2555, "TouchPos Update.");
        public static TEEvent SET_FOCUS0_UPDATE = new TEEvent(2560, "Set Focus0.");
        public static TEEvent SET_FOCUS1_UPDATE = new TEEvent(2561, "Set Focus1.");
        public static TEEvent SET_FOCUS2_UPDATE = new TEEvent(2562, "Set Focus2.");
        public static TEEvent SET_FOCUS3_UPDATE = new TEEvent(2563, "Set Focus3.");

        public static TEEvent SETUP_TEMPSENSOR_OFST_UPDATE = new TEEvent(2565, "Temp Sensor Offset Update.");
        public static TEEvent SETUP_TEMPSENSOR_VALUE = new TEEvent(2565, "Temp Sensor Value.");

        public static TEEvent CAL_DEFZPOS_UPDATE = new TEEvent(2600, "Calibrate Def Z Pos Update.");
        public static TEEvent CAL_DEFZPOS_CANCEL = new TEEvent(2601, "Calibrate Def Z Pos Cancel.");
        public static TEEvent CAL_LASER_CAL_VALUE_UPDATE = new TEEvent(2610, "Calibrate Laser Cal Value Update.");
        public static TEEvent CAL_LASER_CAL_VALUE_CANCEL = new TEEvent(2611, "Calibrate Laser Cal Value Cancel.");

        public static TEEvent PUMP1_DISP_VOL_UPDATE = new TEEvent(2700, "Pump1 Disp Volume Update.");
        public static TEEvent PUMP2_DISP_VOL_UPDATE = new TEEvent(2701, "Pump2 Disp Volume Update.");
        //public static TEEvent PUMP1_DISP_BASE_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 0, "Pump1 Disp Base Volume Update");
        //public static TEEvent PUMP2_DISP_BASE_VOL_UPDATE = new TEEvent(DISPCORE_EVENT_PUMP + 1, "Pump2 Disp Base Volume Update");
        //public static TEEvent PUMP1_DISP_BASE_VOL_ADJ = new TEEvent(DISPCORE_EVENT_PUMP + 2, "Pump1 Disp Volume Adjust");
        //public static TEEvent PUMP2_DISP_BASE_VOL_ADJ = new TEEvent(DISPCORE_EVENT_PUMP + 3, "Pump2 Disp Volume Adjust");

        public static TEEvent PUMP1_BACKSUCK_VOL_UPDATE = new TEEvent(2704, "Pump1 Backsuck Volume Update.");
        public static TEEvent PUMP2_BACKSUCK_VOL_UPDATE = new TEEvent(2705, "Pump2 Backsuck Volume Update.");
        public static TEEvent PUMP1_DISP_SPEED_UPDATE = new TEEvent(2706, "Pump1 Disp Speed Update.");
        public static TEEvent PUMP2_DISP_SPEED_UPDATE = new TEEvent(2707, "Pump2 Disp Speed Update.");
        public static TEEvent PUMP1_BACKSUCK_SPEED_UPDATE = new TEEvent(2708, "Pump1 Backsuck Speed Update.");
        public static TEEvent PUMP2_BACKSUCK_SPEED_UPDATE = new TEEvent(2709, "Pump2 Backsuck Speed Update.");
        public static TEEvent PUMP1_DISP_TIME_UPDATE = new TEEvent(2710, "Pump1 Disp Time Update.");
        public static TEEvent PUMP2_DISP_TIME_UPDATE = new TEEvent(2711, "Pump2 Disp Time Update.");
        public static TEEvent PUMP1_BACKSUCK_TIME_UPDATE = new TEEvent(2712, "Pump1 Backsuck Time Update.");
        public static TEEvent PUMP2_BACKSUCK_TIME_UPDATE = new TEEvent(2713, "Pump2 Backsuck Time Update.");

        public static TEEvent READ_ID = new TEEvent(2810, "Read ID.");
        public static TEEvent MANUAL_ID_ENTRY = new TEEvent(2811, "Manual ID Entry.");
        public static TEEvent INPUT_MAP = new TEEvent(2820, "Input Map.");
        public static TEEvent INPUT_MAP_QUERY = new TEEvent(2821, "Input Map Query.");
        public static TEEvent PP_SELECTED = new TEEvent(2830, "PP-Selected.");
        public static TEEvent PPSEND_H2E = new TEEvent(2831, "PPSend H2E.");
        public static TEEvent PPSEND_E2H = new TEEvent(2832, "PPSend E2H.");
        public static TEEvent MAP_REQUEST = new TEEvent(2835, "Map Request.");
        public static TEEvent MAP_DOWNLOADED = new TEEvent(2836, "Map Downloaded.");
        public static TEEvent MAP_UPLOADED = new TEEvent(2837, "Map Upload.");

        public static TEEvent MAP_RECOVER_PROMPT = new TEEvent(2840, "Map Recover Prompt.");
        public static TEEvent MAP_RECOVER_UPLOADED = new TEEvent(2841, "Map Recover Uploaded.");
        public static TEEvent MAP_RECOVER_UPLOAD_CANCEL = new TEEvent(2842, "Map Recover Cancelled.");
        public static TEEvent MAP_RECOVER_DELETED = new TEEvent(2843, "Map Recover Deleted.");

        public static TEEvent SUBSTRATE_START = new TEEvent(2850, "Substrate Start.");
        public static TEEvent SUBSTRATE_END = new TEEvent(2851, "Substrate End.");

        public static TEEvent TERMINAL_MESSAGE_ACK = new TEEvent(2880, "Terminal Message Acknowledge.");

        public static TEEvent OSRAMICC = new TEEvent(2900, "OsramICC.");
        #endregion

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

        public static TEEvent SECSGEM_HOST_REQ_OFFLINE = new TEEvent(5000, "Host request Offline.", new List<int>{11000});
        public static TEEvent SECSGEM_HOST_REQ_ONLINE = new TEEvent(5001, "Host request Online.", new List<int>{11000});
        public static TEEvent SECSGEM_EQ_SET_OFFLINE = new TEEvent(5002, "Equipment set Offline.", new List<int>{11000});
        public static TEEvent SECSGEM_EQ_SET_ONLINE = new TEEvent(5003, "Equipment set Online.", new List<int>{11000});
        public static TEEvent SECSGEM_HOST_REQ_LOCAL = new TEEvent(5004, "Host request Local.", new List<int>{11001});
        public static TEEvent SECSGEM_HOST_REQ_REMOTE = new TEEvent(5005, "Host request Remote.", new List<int>{11001});
        public static TEEvent SECSGEM_EQ_SET_LOCAL = new TEEvent(5006, "Equipment set Local.", new List<int> { 11001 });
        public static TEEvent SECSGEM_EQ_SET_REMOTE = new TEEvent(5007, "Equipment set Remote.", new List<int>{11001});
        public static TEEvent SECSGEM_CONTROL_STATE_CHANGE = new TEEvent(5008, "Control State Change.", new List<int> { 10999, 11003, 11004 });

        public static TEEvent SECSGEM_EQ_PROCESS_CHANGE_STATE = new TEEvent(5010, "Process State Change.", new List<int> { 10999, 11010, 11011 });
        public static TEEvent SECSGEM_EQUIPMENT_CONSTANT_CHANGE = new TEEvent(5020, "Equipment Constant Change.", new List<int> { 10999, 11010, 11011 });
        
        public static TEEvent SECSGEM_PP_CREATE = new TEEvent(5030, "PP Create.", new List<int> { 11051, 11052 });
        public static TEEvent SECSGEM_PP_CHANGE = new TEEvent(5031, "PP Change.", new List<int> { 11050, 11051, 11052, 11053 });//edited
        public static TEEvent SECSGEM_PP_DELETE = new TEEvent(5032, "PP Delete.", new List<int> { 11051 });
        public static TEEvent SECSGEM_PP_SELECTED = new TEEvent(5033, "PP Selected.", new List<int> { 11051, 11052 });
    }

    public class TEVID//SV, DV, EC Represents real-time status variables of equipment.
    {
        public int Code = 0;
        public string Desc = "";
        public double Min = 0;
        public double Max = 0;
        public double Def = 0;
        public string Units = "";
        public TEVID(int code, string desc, double min = 0, double max = 0, double def = 0, string units = "")
        {
            Code = code;
            Desc = desc;
            Min = min;
            Max = max;
            Def = def;
            Units = units;
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
        public static string GetNameFromId(int code)
        {
            var fields = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var value = field.GetValue(null) as TEVID;
                if (value != null && value.Code == code)
                {
                    return field.Name; // Return the field name, e.g., "PPERROR"
                }
            }
            return null;
        }
        public static TEVID GetFieldFromId(int code)
        {
            var fields = typeof(VID).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var value = field.GetValue(null) as TEVID;
                if (value != null && value.Code == code)
                {
                    return value;
                }
            }
            return null;
        }
        public dynamic Value
        {
            get
            {
                string vidName = GetNameFromId(Code);

                switch (vidName)
                {
                    #region 10000
                    case nameof(VID.CLOCK):
                        {
                            return DateTime.Now.ToString("yyMMddhhmmss");
                        }
                    case nameof(VID.ONLINE_OFFLINE):
                        return (int)TFSecsGem.OnlineOffline;
                    case nameof(VID.LOCAL_REMOTE):
                        return (int)TFSecsGem.LocalRemote;
                    case nameof(VID.PREV_LOCAL_REMOTE):
                        return (int)TFSecsGem.PrevLocalRemote;
                    case nameof(VID.PROCESS_STATE):
                        return (int)TFSecsGem.ProcessState;
                    case nameof(VID.PREV_PROCESS_STATE):
                        return (int)TFSecsGem.PrevProcessState;
                    case nameof(VID.CONTROL_STATE):
                        return (int)TFSecsGem.ControlState;
                    case nameof(VID.PREV_CONTROL_STATE):
                        return (int)TFSecsGem.PrevControlState;
                    case nameof(VID.CHANGED_ECID):
                        return TFSecsGem.ChangedECID;
                    case nameof(VID.CHANGED_ECVALUE):
                        return TFSecsGem.ChangedECValue;

                    case nameof(VID.PP_CHANGE_STATUS):
                        return TFSecsGem.PPChangeStatus.ToString();
                    case nameof(VID.PP_CHANGE_NAME):
                        return GDefine.ProgRecipeName;
                    case nameof(VID.PP_FORMAT):
                        return TFSecsGem.PPFormat.ToString();
                    case nameof(VID.PP_EXECNAME):
                        return GDefine.ProgRecipeName;

                    case nameof(VID.TEMPCTRL1SV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(0);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL2SV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(1);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL3SV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(2);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL4SV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.SV(3);
                            }
                        }
                        break;

                    case nameof(VID.PUMP1_WEIGHT_CAL_VALUE):
                        return TaskWeight.CurrentCal[0];
                    case nameof(VID.PUMP2_WEIGHT_CAL_VALUE):
                        return TaskWeight.CurrentCal[1];
                    case nameof(VID.PUMP_WEIGHT_MEAS_MIN):
                        return TaskWeight.list_WM_MeasWeight.DefaultIfEmpty(0).Min(); 
                    case nameof(VID.PUMP_WEIGHT_MEAS_MAX):
                        return TaskWeight.list_WM_MeasWeight.DefaultIfEmpty(0).Max();
                    case nameof(VID.PUMP_WEIGHT_MEAS_AVE):
                        return TaskWeight.list_WM_MeasWeight.DefaultIfEmpty(0).Average();
                    case nameof(VID.PUMP_WEIGHT_MEAS_STDEV):
                        {
                            NSW.Net.Stats Stat = new NSW.Net.Stats();
                            return Stat.StDev(TaskWeight.list_WM_MeasWeight);
                        }
                    case nameof(VID.PUMP1_FLOWRATE_VALUE):
                        return TaskFlowRate.Value[0];
                    case nameof(VID.PUMP2_FLOWRATE_VALUE):
                        return TaskFlowRate.Value[1];
                    case nameof(VID.PUMP_WEIGHT_MEAS2_MIN):
                        return TaskWeightMeas.list_Weight.DefaultIfEmpty(0).Min();
                    case nameof(VID.PUMP_WEIGHT_MEAS2_MAX):
                        return TaskWeightMeas.list_Weight.DefaultIfEmpty(0).Max();
                    case nameof(VID.PUMP_WEIGHT_MEAS2_AVE):
                        return TaskWeightMeas.list_Weight.DefaultIfEmpty(0).Average();
                    case nameof(VID.PUMP_WEIGHT_MEAS2_STDEV):
                        {
                            NSW.Net.Stats Stat = new NSW.Net.Stats();
                            return Stat.StDev(TaskWeightMeas.list_Weight);
                        }                
                    #endregion
                    #region 20000
                    case nameof(VID.TEMPCTRL1PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(0);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL2PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(1);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL3PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(2);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL4PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.PV(3);
                            }
                        }
                        break;
                    case nameof(VID.TARGETWEIGHT1):
                        return DispProg.Disp_Weight[0];
                    case nameof(VID.TARGETWEIGHT2):
                        return DispProg.Disp_Weight[1];
                    case nameof(VID.ENTRY_EMPLOYEEID):
                        return LotInfo2.sOperatorID;
                    case nameof(VID.ENTRY_LOTNUMBER):
                        return LotInfo2.LotNumber;
                    case nameof(VID.ENTRY_L11SERIES):
                        return LotInfo2.Osram.ElevenSeries;
                    case nameof(VID.ENTRY_DASTARTNUMBER):
                        return LotInfo2.Osram.DAStartNumber;
                    case nameof(VID.ENTRY_FIELD5):
                        return LotInfo2.Osram.F5Value;
                    case nameof(VID.ENTRY_FIELD6):
                        return LotInfo2.Osram.F6Value;
                    case nameof(VID.ENTRY_FIELD7):
                        return LotInfo2.Osram.F7Value;
                    case nameof(VID.ENTRY_FIELD8):
                        return LotInfo2.Osram.F8Value;
                    case nameof(VID.DEVICE):
                        return GDefine.DeviceRecipe;
                    case nameof(VID.RECIPE):
                        return GDefine.ProgRecipeName;
                    case nameof(VID.MHSRECIPE):
                        return GDefine.MHSRecipeName;
                    case nameof(VID.OPERATION):
                        return LotInfo2.Osram.Operation;
                    case nameof(VID.SUBSTRATEID):
                        return DispProg.rt_Read_IDs[0, 0];
                    case nameof(VID.MODEL1DISPGAP):
                    case nameof(VID.MODEL2DISPGAP):
                    case nameof(VID.MODEL3DISPGAP):
                    case nameof(VID.MODEL4DISPGAP):
                    case nameof(VID.MODEL5DISPGAP):
                    case nameof(VID.MODEL6DISPGAP):
                    case nameof(VID.MODEL7DISPGAP):
                    case nameof(VID.MODEL8DISPGAP):
                    case nameof(VID.MODEL9DISPGAP):
                    case nameof(VID.MODEL10DISPGAP):
                    case nameof(VID.MODEL11DISPGAP):
                    case nameof(VID.MODEL12DISPGAP):
                    case nameof(VID.MODEL13DISPGAP):
                    case nameof(VID.MODEL14DISPGAP):
                    case nameof(VID.MODEL15DISPGAP):
                    //case nameof(VID.MODEL16DISPGAP):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("DISPGAP", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                return Model.DispGap;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1DNWAIT):
                    case nameof(VID.MODEL2DNWAIT):
                    case nameof(VID.MODEL3DNWAIT):
                    case nameof(VID.MODEL4DNWAIT):
                    case nameof(VID.MODEL5DNWAIT):
                    case nameof(VID.MODEL6DNWAIT):
                    case nameof(VID.MODEL7DNWAIT):
                    case nameof(VID.MODEL8DNWAIT):
                    case nameof(VID.MODEL9DNWAIT):
                    case nameof(VID.MODEL10DNWAIT):
                    case nameof(VID.MODEL11DNWAIT):
                    case nameof(VID.MODEL12DNWAIT):
                    case nameof(VID.MODEL13DNWAIT):
                    case nameof(VID.MODEL14DNWAIT):
                    case nameof(VID.MODEL15DNWAIT):
                    //case nameof(VID.MODEL16DNWAIT):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("DNWAIT", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                return Model.DnWait;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1STARTDELAY):
                    case nameof(VID.MODEL2STARTDELAY):
                    case nameof(VID.MODEL3STARTDELAY):
                    case nameof(VID.MODEL4STARTDELAY):
                    case nameof(VID.MODEL5STARTDELAY):
                    case nameof(VID.MODEL6STARTDELAY):
                    case nameof(VID.MODEL7STARTDELAY):
                    case nameof(VID.MODEL8STARTDELAY):
                    case nameof(VID.MODEL9STARTDELAY):
                    case nameof(VID.MODEL10STARTDELAY):
                    case nameof(VID.MODEL11STARTDELAY):
                    case nameof(VID.MODEL12STARTDELAY):
                    case nameof(VID.MODEL13STARTDELAY):
                    case nameof(VID.MODEL14STARTDELAY):
                    case nameof(VID.MODEL15STARTDELAY):
                    //case nameof(VID.MODEL16STARTDELAY):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("STARTDELAY", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                return Model.StartDelay;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1LINESPEED):
                    case nameof(VID.MODEL2LINESPEED):
                    case nameof(VID.MODEL3LINESPEED):
                    case nameof(VID.MODEL4LINESPEED):
                    case nameof(VID.MODEL5LINESPEED):
                    case nameof(VID.MODEL6LINESPEED):
                    case nameof(VID.MODEL7LINESPEED):
                    case nameof(VID.MODEL8LINESPEED):
                    case nameof(VID.MODEL9LINESPEED):
                    case nameof(VID.MODEL10LINESPEED):
                    case nameof(VID.MODEL11LINESPEED):
                    case nameof(VID.MODEL12LINESPEED):
                    case nameof(VID.MODEL13LINESPEED):
                    case nameof(VID.MODEL14LINESPEED):
                    case nameof(VID.MODEL15LINESPEED):
                    //case nameof(VID.MODEL16LINESPEED):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("LINESPEED", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                return Model.LineSpeed;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1ENDDELAY):
                    case nameof(VID.MODEL2ENDDELAY):
                    case nameof(VID.MODEL3ENDDELAY):
                    case nameof(VID.MODEL4ENDDELAY):
                    case nameof(VID.MODEL5ENDDELAY):
                    case nameof(VID.MODEL6ENDDELAY):
                    case nameof(VID.MODEL7ENDDELAY):
                    case nameof(VID.MODEL8ENDDELAY):
                    case nameof(VID.MODEL9ENDDELAY):
                    case nameof(VID.MODEL10ENDDELAY):
                    case nameof(VID.MODEL11ENDDELAY):
                    case nameof(VID.MODEL12ENDDELAY):
                    case nameof(VID.MODEL13ENDDELAY):
                    case nameof(VID.MODEL14ENDDELAY):
                    case nameof(VID.MODEL15ENDDELAY):
                    //case nameof(VID.MODEL16ENDDELAY):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("ENDDELAY", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                return Model.EndDelay;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1POSTWAIT):
                    case nameof(VID.MODEL2POSTWAIT):
                    case nameof(VID.MODEL3POSTWAIT):
                    case nameof(VID.MODEL4POSTWAIT):
                    case nameof(VID.MODEL5POSTWAIT):
                    case nameof(VID.MODEL6POSTWAIT):
                    case nameof(VID.MODEL7POSTWAIT):
                    case nameof(VID.MODEL8POSTWAIT):
                    case nameof(VID.MODEL9POSTWAIT):
                    case nameof(VID.MODEL10POSTWAIT):
                    case nameof(VID.MODEL11POSTWAIT):
                    case nameof(VID.MODEL12POSTWAIT):
                    case nameof(VID.MODEL13POSTWAIT):
                    case nameof(VID.MODEL14POSTWAIT):
                    case nameof(VID.MODEL15POSTWAIT):
                    //case nameof(VID.MODEL16POSTWAIT):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("POSTWAIT", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                return Model.PostWait;
                            }
                            break;
                        }
                    #endregion
                    #region 30000
                    case nameof(VID.TEMPCTRL1TOL):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.AL1_Dev(0);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL2TOL):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.AL1_Dev(1);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL3TOL):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                return TempCtrl.AL1_Dev(2);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL4TOL):
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
                string vidName = GetNameFromId(Code);

                switch (vidName)
                {
                    #region 20000
                    case nameof(VID.TEMPCTRL1PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[0] = value;
                                TempCtrl.Set(0, (int)DispProg.HeaterSV[0], (int)DispProg.HeaterRange[0]);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL2PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[1] = value;
                                TempCtrl.Set(1, (int)DispProg.HeaterSV[1], (int)DispProg.HeaterRange[1]);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL3PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[2] = value;
                                TempCtrl.Set(2, (int)DispProg.HeaterSV[2], (int)DispProg.HeaterRange[2]);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL4PV):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterSV[3] = value;
                                TempCtrl.Set(3, (int)DispProg.HeaterSV[3], (int)DispProg.HeaterRange[3]);
                            }
                        }
                        break;
                    case nameof(VID.TARGETWEIGHT1):
                        DispProg.Target_Weight = value;
                        DispProg.Disp_Weight[0] = DispProg.Target_Weight;
                        if (DispProg.Disp_Weight[0] > 0) TaskDisp.PP_SetWeight(DispProg.Disp_Weight, true);
                        break;
                    case nameof(VID.TARGETWEIGHT2):
                        DispProg.Target_Weight = value;
                        DispProg.Disp_Weight[1] = DispProg.Target_Weight;
                        if (DispProg.Disp_Weight[1] > 0) TaskDisp.PP_SetWeight(DispProg.Disp_Weight, true);
                        break;
                    case nameof(VID.ENTRY_EMPLOYEEID):
                        LotInfo2.sOperatorID = value;
                        break;
                    case nameof(VID.ENTRY_LOTNUMBER):
                        LotInfo2.LotNumber = value;
                        break;
                    case nameof(VID.ENTRY_L11SERIES):
                        LotInfo2.Osram.ElevenSeries = value;
                        break;
                    case nameof(VID.ENTRY_DASTARTNUMBER):
                        LotInfo2.Osram.DAStartNumber = value;
                        break;
                    case nameof(VID.ENTRY_FIELD5):
                        LotInfo2.Osram.F5Value = value;
                        break;
                    case nameof(VID.ENTRY_FIELD6):
                        LotInfo2.Osram.F6Value = value;
                        break;
                    case nameof(VID.ENTRY_FIELD7):
                        LotInfo2.Osram.F7Value = value;
                        break;
                    case nameof(VID.ENTRY_FIELD8):
                        LotInfo2.Osram.F8Value = value;
                        break;
                    case nameof(VID.DEVICE):
                        GDefine.DeviceRecipe = value;
                        break;
                    case nameof(VID.RECIPE):
                        GDefine.ProgRecipeName = value;
                        break;
                    case nameof(VID.MHSRECIPE):
                        GDefine.MHSRecipeName = value;
                        break;
                    case nameof(VID.MODEL1DISPGAP):
                    case nameof(VID.MODEL2DISPGAP):
                    case nameof(VID.MODEL3DISPGAP):
                    case nameof(VID.MODEL4DISPGAP):
                    case nameof(VID.MODEL5DISPGAP):
                    case nameof(VID.MODEL6DISPGAP):
                    case nameof(VID.MODEL7DISPGAP):
                    case nameof(VID.MODEL8DISPGAP):
                    case nameof(VID.MODEL9DISPGAP):
                    case nameof(VID.MODEL10DISPGAP):
                    case nameof(VID.MODEL11DISPGAP):
                    case nameof(VID.MODEL12DISPGAP):
                    case nameof(VID.MODEL13DISPGAP):
                    case nameof(VID.MODEL14DISPGAP):
                    case nameof(VID.MODEL15DISPGAP):
                    //case nameof(VID.MODEL16DISPGAP):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("DISPGAP", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                Model.DispGap = value;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1DNWAIT):
                    case nameof(VID.MODEL2DNWAIT):
                    case nameof(VID.MODEL3DNWAIT):
                    case nameof(VID.MODEL4DNWAIT):
                    case nameof(VID.MODEL5DNWAIT):
                    case nameof(VID.MODEL6DNWAIT):
                    case nameof(VID.MODEL7DNWAIT):
                    case nameof(VID.MODEL8DNWAIT):
                    case nameof(VID.MODEL9DNWAIT):
                    case nameof(VID.MODEL10DNWAIT):
                    case nameof(VID.MODEL11DNWAIT):
                    case nameof(VID.MODEL12DNWAIT):
                    case nameof(VID.MODEL13DNWAIT):
                    case nameof(VID.MODEL14DNWAIT):
                    case nameof(VID.MODEL15DNWAIT):
                    //case nameof(VID.MODEL16DNWAIT):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("DNWAIT", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                Model.DnWait = (int)value;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1STARTDELAY):
                    case nameof(VID.MODEL2STARTDELAY):
                    case nameof(VID.MODEL3STARTDELAY):
                    case nameof(VID.MODEL4STARTDELAY):
                    case nameof(VID.MODEL5STARTDELAY):
                    case nameof(VID.MODEL6STARTDELAY):
                    case nameof(VID.MODEL7STARTDELAY):
                    case nameof(VID.MODEL8STARTDELAY):
                    case nameof(VID.MODEL9STARTDELAY):
                    case nameof(VID.MODEL10STARTDELAY):
                    case nameof(VID.MODEL11STARTDELAY):
                    case nameof(VID.MODEL12STARTDELAY):
                    case nameof(VID.MODEL13STARTDELAY):
                    case nameof(VID.MODEL14STARTDELAY):
                    case nameof(VID.MODEL15STARTDELAY):
                    //case nameof(VID.MODEL16STARTDELAY):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("STARTDELAY", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                Model.StartDelay = (int)value;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1LINESPEED):
                    case nameof(VID.MODEL2LINESPEED):
                    case nameof(VID.MODEL3LINESPEED):
                    case nameof(VID.MODEL4LINESPEED):
                    case nameof(VID.MODEL5LINESPEED):
                    case nameof(VID.MODEL6LINESPEED):
                    case nameof(VID.MODEL7LINESPEED):
                    case nameof(VID.MODEL8LINESPEED):
                    case nameof(VID.MODEL9LINESPEED):
                    case nameof(VID.MODEL10LINESPEED):
                    case nameof(VID.MODEL11LINESPEED):
                    case nameof(VID.MODEL12LINESPEED):
                    case nameof(VID.MODEL13LINESPEED):
                    case nameof(VID.MODEL14LINESPEED):
                    case nameof(VID.MODEL15LINESPEED):
                    //case nameof(VID.MODEL16LINESPEED):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("LINESPEED", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                Model.LineSpeed = value;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1ENDDELAY):
                    case nameof(VID.MODEL2ENDDELAY):
                    case nameof(VID.MODEL3ENDDELAY):
                    case nameof(VID.MODEL4ENDDELAY):
                    case nameof(VID.MODEL5ENDDELAY):
                    case nameof(VID.MODEL6ENDDELAY):
                    case nameof(VID.MODEL7ENDDELAY):
                    case nameof(VID.MODEL8ENDDELAY):
                    case nameof(VID.MODEL9ENDDELAY):
                    case nameof(VID.MODEL10ENDDELAY):
                    case nameof(VID.MODEL11ENDDELAY):
                    case nameof(VID.MODEL12ENDDELAY):
                    case nameof(VID.MODEL13ENDDELAY):
                    case nameof(VID.MODEL14ENDDELAY):
                    case nameof(VID.MODEL15ENDDELAY):
                    //case nameof(VID.MODEL16ENDDELAY):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("ENDDELAY", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                Model.EndDelay = (int)value;
                            }
                            break;
                        }
                    case nameof(VID.MODEL1POSTWAIT):
                    case nameof(VID.MODEL2POSTWAIT):
                    case nameof(VID.MODEL3POSTWAIT):
                    case nameof(VID.MODEL4POSTWAIT):
                    case nameof(VID.MODEL5POSTWAIT):
                    case nameof(VID.MODEL6POSTWAIT):
                    case nameof(VID.MODEL7POSTWAIT):
                    case nameof(VID.MODEL8POSTWAIT):
                    case nameof(VID.MODEL9POSTWAIT):
                    case nameof(VID.MODEL10POSTWAIT):
                    case nameof(VID.MODEL11POSTWAIT):
                    case nameof(VID.MODEL12POSTWAIT):
                    case nameof(VID.MODEL13POSTWAIT):
                    case nameof(VID.MODEL14POSTWAIT):
                    case nameof(VID.MODEL15POSTWAIT):
                    //case nameof(VID.MODEL16POSTWAIT):
                        {
                            string numberPart = vidName.Replace("MODEL", "").Replace("POSTWAIT", "");
                            if (int.TryParse(numberPart, out int modelNo))
                            {
                                TModelPara Model = new TModelPara(DispProg.ModelList, modelNo);
                                Model.PostWait = (int)value;
                            }
                            break;
                        }
                    #endregion
                    #region 30000
                    case nameof(VID.TEMPCTRL1TOL):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterRange[0] = value;
                                TempCtrl.Set(0, (int)DispProg.HeaterSV[0], (int)DispProg.HeaterRange[0]);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL2TOL):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterRange[1] = value;
                                TempCtrl.Set(1, (int)DispProg.HeaterSV[1], (int)DispProg.HeaterRange[1]);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL3TOL):
                        if (GDefine.TempCtrl_Type == GDefine.ETempCtrl.Autonics_TX_TK)
                        {
                            if (GDefine.TempCtrl_Module[0] > GDefine.ETempCtrlModule.None)
                            {
                                DispProg.HeaterRange[2] = value;
                                TempCtrl.Set(2, (int)DispProg.HeaterSV[2], (int)DispProg.HeaterRange[2]);
                            }
                        }
                        break;
                    case nameof(VID.TEMPCTRL4TOL):
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
        public static TEVID CLOCK = new TEVID(10999, "Clock.");
        public static TEVID ONLINE_OFFLINE = new TEVID(11000, "Online Offline State.");
        public static TEVID LOCAL_REMOTE = new TEVID(11001, "Local Remote State.");
        public static TEVID PREV_LOCAL_REMOTE = new TEVID(11002, "Prev Local Remote State.");
        public static TEVID CONTROL_STATE = new TEVID(11003, "Control State.");
        public static TEVID PREV_CONTROL_STATE = new TEVID(11004, "Previous Control State.");   

        public static TEVID PROCESS_STATE = new TEVID(11010, "Process State.");
        public static TEVID PREV_PROCESS_STATE = new TEVID(11011, "Prev Process State.");

        public static TEVID CHANGED_ECID = new TEVID(11020, "Changed ECID.");
        public static TEVID CHANGED_ECVALUE = new TEVID(11021, "Changed EC Value.");
        public static TEVID PP_CHANGE_STATUS = new TEVID(11050, "PP Change Status.");
        public static TEVID PP_CHANGE_NAME = new TEVID(11051, "PP Change Name.");
        public static TEVID PP_FORMAT = new TEVID(11052, "PP Format.");
        public static TEVID PP_EXECNAME = new TEVID(11053, "PP Exec Name.");

        public static TEVID TEMPCTRL1PV = new TEVID(11101, "Temperature Control PV 1.");
        public static TEVID TEMPCTRL2PV = new TEVID(11102, "Temperature Control PV 2.");
        public static TEVID TEMPCTRL3PV = new TEVID(11103, "Temperature Control PV 3.");
        public static TEVID TEMPCTRL4PV = new TEVID(11104, "Temperature Control PV 4.");

        public static TEVID PUMP1_WEIGHT_CAL_VALUE = new TEVID(11110, "Pump1 Weight Calibration Value.");
        public static TEVID PUMP2_WEIGHT_CAL_VALUE = new TEVID(11111, "Pump2 Weight Calibration Value.");

        public static TEVID PUMP_WEIGHT_MEAS_MIN = new TEVID(11120, "Pump Weight Meas Minimum Value.");
        public static TEVID PUMP_WEIGHT_MEAS_MAX = new TEVID(11121, "Pump Weight Meas Maximum Value.");
        public static TEVID PUMP_WEIGHT_MEAS_AVE = new TEVID(11122, "Pump Weight Meas Average Value.");
        public static TEVID PUMP_WEIGHT_MEAS_STDEV = new TEVID(11123, "Pump Weight Meas Standard Deviation Value.");

        public static TEVID PUMP1_FLOWRATE_VALUE = new TEVID(11130, "Pump1 FlowRate Value.");
        public static TEVID PUMP2_FLOWRATE_VALUE = new TEVID(11131, "Pump2 FlowRate Value.");

        public static TEVID PUMP_WEIGHT_MEAS2_MIN = new TEVID(11140, "Pump Weight Meas2 Minimum Value.");
        public static TEVID PUMP_WEIGHT_MEAS2_MAX = new TEVID(11141, "Pump Weight Meas2 Maximum Value.");
        public static TEVID PUMP_WEIGHT_MEAS2_AVE = new TEVID(11142, "Pump Weight Meas2 Average Value.");
        public static TEVID PUMP_WEIGHT_MEAS2_STDEV = new TEVID(11143, "Pump Weight Meas2 Standard Deviation Value.");      
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
        public static TEVID OPERATION = new TEVID(21108, "Operation.");

        public static TEVID DEVICE = new TEVID(21200, "Device.");
        public static TEVID RECIPE = new TEVID(21201, "Recipe.");
        public static TEVID MHSRECIPE = new TEVID(21202, "MHS Recipe.");

        public static TEVID SUBSTRATEID = new TEVID(21300, "SubstrateID.");
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
        //public static TEVID MODEL16DISPGAP = new TEVID(22015, "Model16 Disp Gap.");

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
        //public static TEVID MODEL16DNWAIT = new TEVID(22115, "Model16 Down Wait.");

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
        //public static TEVID MODEL16STARTDELAY = new TEVID(22215, "Model16 Start Delay.");

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
        //public static TEVID MODEL16LINESPEED = new TEVID(22315, "Model16 Line Speed.");

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
        //public static TEVID MODEL16ENDDELAY = new TEVID(22415, "Model16 End Delay.");

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
        //public static TEVID MODEL16POSTWAIT = new TEVID(22515, "Model16 Post Wait.");
        #endregion
        #endregion

        #region 30000 ECID
        public static TEVID TEMPCTRL1TOL = new TEVID(31001, "Temperature Control Tol 1.", 0, 10, 0, "DegC");
        public static TEVID TEMPCTRL2TOL = new TEVID(31002, "Temperature Control Tol 2.", 0, 10, 0, "DegC");
        public static TEVID TEMPCTRL3TOL = new TEVID(31003, "Temperature Control Tol 3.", 0, 10, 0, "DegC");
        public static TEVID TEMPCTRL4TOL = new TEVID(31004, "Temperature Control Tol 4.", 0, 10, 0, "DegC");
        #endregion
    }

    public class RMCD
    {
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

        public const int PP_SELECT = 0;
        public const int START = 10;
        public const int STOP = 11;
        public const int PAUSE = 12;//Same as STOP, fullfill GTOS
        public const int RESUME = 13;//Same as START, fullfill GTOS
        public const int ABORT = 12;//Cancel Program
        public const int NEWLOT = 20;
    }
}
