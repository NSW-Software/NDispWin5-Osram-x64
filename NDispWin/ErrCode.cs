using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NDispWin
{
    public class TEMessage//messages promtps including error, warning, notification, confirmation
    {
        public int Code = 0;
        public string Desc = "";//default description
        public string CAct = "";//default corrective action

        //public string ExtDesc = "";//default extended description
        public string DescAlt = "";//alterrnative language description
        public string CActAlt = "";//alternative language corrective action
        public enum EType { Error = 0, Warning = 1, Fault = 2, Notification = 3, Confirmation = 4, Run = 5, Idle = 6, Custom1 = 8, Custom2 = 9 }
        //Error - A deviation of a system from normal operation. (A physical reason, i.e.circuit breaker tripped)
        //Warning - A deviation of system parameter by a certain level. (A physical reason, i.e.high current)
        //Fault - Lasting error or warning condition. (Abnormal system condition)
        //Notification - Unsolicited transmission of management information. (Diagnostic messages, help messages to operator, i.e. "system is ready", "select Auto mode and press Start")
        //Confirmation - Confirm execution of action. ("Close application")
        //Custom1
        //Custom2
        public EType Type = EType.Error;

        public bool Assist = true;
        public TEMessage(TEMessage msg)
        {
            this.Code = msg.Code;
            this.Desc = msg.Desc;
            this.CAct = msg.CAct;
            this.DescAlt = msg.DescAlt;
            this.CActAlt = msg.CActAlt;
            this.Type = msg.Type;
        }
        public TEMessage(int code)
        {
            Code = code;
            Desc = "";
            CAct = "";
            Type = EType.Error;
            Assist = true;
        }
        public TEMessage(int code, string desc, string cAct, EType type, bool assist)
        {
            Code = code;
            Desc = desc;
            CAct = cAct;
            Type = type;
            Assist = assist;
        }
    }
    public class Messages
    {
        public static TEMessage EMO_ACTIVATED = new TEMessage(101, "EMO Activated.", "Clear Machine EMO.", TEMessage.EType.Fault, false);
        public static TEMessage SYSTEM_NOT_READY = new TEMessage(102, "System Not Ready.", "Initialize System.@Check Module Status.", TEMessage.EType.Fault, false);

        #region Operation Error
        public static TEMessage EXIT_SAVE_RECIPE = new TEMessage(105, "Close NDisp3Win?", "Ensure all Parameters are saved.", TEMessage.EType.Confirmation, false);
        public static TEMessage LOAD_NEW_RECIPE = new TEMessage(106, "Load New Recipe?", "Initialization required.", TEMessage.EType.Confirmation, false);
        public static TEMessage EXIT_WHEN_LOT_ACTIVATED = new TEMessage(107, "Lot Is Activated!", "Please End Lot Before Close NDisp3Win", TEMessage.EType.Notification, false);
        public static TEMessage INIT_SYSTEM = new TEMessage(150, "Initialize System?", "", TEMessage.EType.Confirmation, false);
        public static TEMessage INIT_GANTRY = new TEMessage(151, "Initialize Gantry?", "", TEMessage.EType.Confirmation, false);
        public static TEMessage INIT_GANTRY_FAIL = new TEMessage(152, "Init Gantry Failed.", "Check Parameter and Settings.", TEMessage.EType.Error, false);
        public static TEMessage INIT_CONVEYOR = new TEMessage(155, "Initialize Conveyor?", "", TEMessage.EType.Confirmation, false);
        public static TEMessage INIT_CONVEYOR_FAIL = new TEMessage(156, "Init Conveyor Failed.", "Check Parameter and Settings.", TEMessage.EType.Error, false);
        public static TEMessage INIT_LR_LINE = new TEMessage(160, "Initialize LR Line?", "", TEMessage.EType.Confirmation, false);
        public static TEMessage INIT_LR_LINE_FAIL = new TEMessage(161, "Init LR Line Failed.", "Check Parameter and Settings.", TEMessage.EType.Error, false);

        public static TEMessage LOW_AIR_PRESSURE = new TEMessage(210, "Low Air Pressure.", "Check Incoming Air Supply.", TEMessage.EType.Error, false);
        public static TEMessage FRONT_DOOR_OPEN = new TEMessage(220, "Front Door Open.", "Check Front Door is closed.", TEMessage.EType.Error, false);
        public static TEMessage LEFT_ELEV_DOOR_OPEN = new TEMessage(225, "Left Elevator Door Open.", "Check Elevator Door is closed.", TEMessage.EType.Error, false);
        public static TEMessage RIGHT_ELEV_DOOR_OPEN = new TEMessage(226, "Right Elevator Door Open.", "Check Elevator Door is closed.", TEMessage.EType.Error, false);
        public static TEMessage DISP12MODE_WAIT_PRE_TIMEOUT = new TEMessage(250, "Wait Pre TimeOut. Continue Pro only?", "", TEMessage.EType.Error, false);
        public static TEMessage INPUT_IS_STOPPED = new TEMessage(251, "Input is Stopped.@1. OK - Enable Input and Start Run.@2. STOP - Stop Run.@3. CANCEL - Start Run.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage RESET_PERF_INFO = new TEMessage(300, "Reset Performance Information?", "", TEMessage.EType.Confirmation, false);
        public static TEMessage S320_LOAD_PRODUCT = new TEMessage(400, "Load Product to Table", "", TEMessage.EType.Notification, false);
        public static TEMessage S320_UNLOAD_PRODUCT = new TEMessage(401, "Unload Product from Table", "", TEMessage.EType.Notification, false);
        public static TEMessage S320_NEW_DISPENSE = new TEMessage(402, "Clear current dispense status and start new dispense?", "", TEMessage.EType.Confirmation, false);
        public static TEMessage LOTINFO_ISEMPTY = new TEMessage(400, "Lot Info Is Empty, Please Fill All Information in All Textbox.", "", TEMessage.EType.Notification, false);
        public static TEMessage LOT_NOT_ACTIVATED = new TEMessage(401, "Please Start Lot Before Auto Run", "", TEMessage.EType.Notification, false);
        #endregion
        public static TEMessage ZSENSOR_NOT_CONFIG = new TEMessage(2200, "ZSensor is not configured.", "Check ZSensor configuration.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_CONFIG_NOT_SUPPORT = new TEMessage(2201, "Gantry configuration is not supported.", "Check gantry configuration.", TEMessage.EType.Notification, false);
        #region 2300-2700 Motor, Motion
        public static TEMessage INVALID_AXIS = new TEMessage(2350, "Gantry Invalid Axis.", "1. Check Axis Status. @2. Check Axis Name.", TEMessage.EType.Fault, false);
        public static TEMessage AXIS_ERR = new TEMessage(2355, "Gantry Axis Error.", "1. Check Axis Status. @2. Check Limit Sensor.", TEMessage.EType.Fault, false);
        public static TEMessage MOTOR_ALARM = new TEMessage(2356, "Gantry Axis Motor Alarm.", "1. Check Axis Status. @2. Check Motor and Driver.", TEMessage.EType.Fault, false);
        public static TEMessage ABNORMAL_MOTOR_POSITION_ERROR = new TEMessage(2357, "Abnormal Axis Error.", "Check Motor Position.", TEMessage.EType.Error, false);
        public static TEMessage GANTRY_MOVE_PTP_ABS_ERR = new TEMessage(2400, "Gantry Move Ptp Abs Error.", "1. Check Axis Status.@2. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage GANTRY_MOVE_PTP_REL_ERR = new TEMessage(2420, "Gantry Move Ptp Rel Error.", "1. Check Axis Status.@2. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage GANTRY_MOVE_LINE_ABS2_ERR = new TEMessage(2450, "Gantry Move Line Abs2 Error.", "1. Check Gantry Status.@2. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage GANTRY_MOVE_ARC_CENTER_END_ABS_ERR = new TEMessage(2455, "Gantry Move Arc Center Abs Error.", "1. Check Gantry Status.@2. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage GANTRY_NOT_READY = new TEMessage(2600, "Gantry Not Ready.", "1. Check Gantry Status.@2. Initialize Gantry.", TEMessage.EType.Error, false);
        public static TEMessage AXIS_ALARM_CLEAR_TIMEOUT = new TEMessage(2610, "Gantry Alarm Clear TimeOut.", "1. Check Axis Status.@2. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage HOME_TIMEOUT = new TEMessage(2620, "Gantry Axis Home TimeOut.", "1. Check Axis Status.@2. Check Home Sensor.@3. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage SOFTWARE_N_LIMIT = new TEMessage(2650, "Gantry Axis Software N Limit.", "Check Limit Setting.", TEMessage.EType.Error, false);
        public static TEMessage SOFTWARE_P_LIMIT = new TEMessage(2660, "Gantry Axis Software P Limit.", "Check Limit Setting.", TEMessage.EType.Error, false);
        public static TEMessage GZ_MOVE_TO_HOME_SENSOR_FAIL = new TEMessage(2670, "GZ Move to Home Sensor Fail.", "1. Check Axis Home Sensor.@2. Check Axis Assembly.@3. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage GZ2_MOVE_TO_HOME_SENSOR_FAIL = new TEMessage(2671, "GZ Move to Home Sensor Fail.", "1. Check Axis Home Sensor.@2. Check Axis Assembly.@3. Check Motor and Driver.", TEMessage.EType.Error, false);
        public static TEMessage GZ_FOCUS_POS_NOT_SAFE = new TEMessage(2672, "GZ Focus Position Not Safe.", "1. GZ Home Sensor must On at Focus Position.@2. Check Focus Position.@3. Check GZ Home Sensor.", TEMessage.EType.Error, false);
        public static TEMessage GX_TARGET_MORE_THAN_STROKE = new TEMessage(2690, "GX Target More Than Stroke.", "1. Check program.@2. Check soft limit setting.", TEMessage.EType.Error, false);
        public static TEMessage GY_TARGET_MORE_THAN_STROKE = new TEMessage(2691, "GY Target More Than Stroke.", "1. Check program.@2. Check soft limit setting.", TEMessage.EType.Error, false);
        public static TEMessage GX2Y2_COLLISION_POSSIBLE = new TEMessage(2692, "GX2Y2 Possible Collision.", "1. Check program.@2. Check soft limit setting.", TEMessage.EType.Error, false);
        public static TEMessage GXY_ENC_OFFSET = new TEMessage(2693, "GXY Encoder Offset.", "1. Check Motor Driver and Wiring.@2. Check Motion Card to driver connection.", TEMessage.EType.Error, false);
        #endregion
        #region 2800 Operation
        public static TEMessage NEEDLE_ZSENSOR_NOT_ON = new TEMessage(2800, "Needle ZSensor not On.", "Check ZSensor.", TEMessage.EType.Error, false);
        public static TEMessage SEARCH_NEEDLE_ZSENSOR_NOT_FOUND = new TEMessage(2801, "Search Needle ZSensor not Found.@OK - Define Z Pos.@Retry - Retry Search Needle ZSensor.@Cancel - Cancel Search Needle.", "", TEMessage.EType.Error, false);
        public static TEMessage NEEDLE_ZSENSOR_NOT_OFF = new TEMessage(2802, "Needle ZSensor not OFF.", "Check ZSensor.", TEMessage.EType.Error, false);
        public static TEMessage NEEDLE_ZSENSOR_ABNORMAL = new TEMessage(2803, "Needle ZSensor Abnormal Operation.", "1. Clean ZSensor.@2. Check ZSensor.@3. Check Z Axis.", TEMessage.EType.Error, false);
        public static TEMessage DOOR_IS_OPEN = new TEMessage(2805, "Door Is Open. Operation not safe.", "1. Close Door.@2. Check Door Sensor.", TEMessage.EType.Notification, false);
        public static TEMessage ZTOUCH_ECD_NOT_READY = new TEMessage(2806, "Z Touch Encoder not Ready.", "Check Stage Top Plate is installed.", TEMessage.EType.Error, false);
        public static TEMessage ZTOUCH_ECD_DN_COUNT_FAIL = new TEMessage(2807, "Z Touch Encoder Dn Count Fail.", "1. Check Z Touch Sensor.@2. Maintain Z Touch Sensor.@3. Check Z Axis.", TEMessage.EType.Error, false);
        public static TEMessage ZTOUCH_ECD_UP_COUNT_FAIL = new TEMessage(2808, "Z Touch Encoder Up Count Fail.", "1. Check Z Touch Sensor.@2. Maintain Z Touch Sensor.@3. Check Z Axis.", TEMessage.EType.Error, false);
        public static TEMessage ZTOUCH_ECD_ABNORMAL = new TEMessage(2809, "Z Touch Encoder Abnormal.", "1.Clean Touch Plate.@2.Check Z Touch Sensor.", TEMessage.EType.Error, false);
        public static TEMessage MATERIAL_TIMER_EXPIRED = new TEMessage(2810, "Material Timer Expired.@OK - Continue.@Stop - Stop Operation.", "", TEMessage.EType.Notification, false);
        public static TEMessage FRAME_COUNT_EXCEED_SETTING = new TEMessage(2811, "Frame Count Exceed Setting.@OK - Continue.@Stop - Stop Operation.", "1. Check Material.@2. Reset Frame Count.", TEMessage.EType.Notification, false);
        public static TEMessage UNIT_COUNT_EXCEED_SETTING = new TEMessage(2812, "Unit Count Exceed Setting.@OK - Continue.@Stop - Stop Operation.", "1. Check Material.@2. Reset Unit Count.", TEMessage.EType.Notification, false);
        public static TEMessage RUNTIME_EXCEED_SETTING = new TEMessage(2813, "Runtime Exceed Setting.@OK - Continue.@Stop - Stop Operation.", "1. Check Material.@2. Reset Runtime.", TEMessage.EType.Notification, false);
        public static TEMessage MATERIAL_EXPIRY_PREALERT = new TEMessage(2814, "Material Pre-expiry Alert.", "", TEMessage.EType.Notification, false);
        public static TEMessage MOVE_ZAXIS_TO_POSITION = new TEMessage(2815, "Move Z To Position?", "", TEMessage.EType.Confirmation, false);
        public static TEMessage MATERIAL1_LEVEL_LOW = new TEMessage(2816, "Material1 Level Low.@OK - Continue.@Stop - Stop Operation.", "Change or Refill Material.", TEMessage.EType.Notification, false);
        public static TEMessage MATERIAL2_LEVEL_LOW = new TEMessage(2817, "Material2 Level Low.@OK - Continue.@Stop - Stop Operation.", "Change or Refill Material.", TEMessage.EType.Notification, false);
        public static TEMessage TEMPCTRL_OUT_OF_RANGE = new TEMessage(2818, "Temperature Control Out Of Range.", "Check temperature controls.", TEMessage.EType.Error, false);
        public static TEMessage TEMPCTRL_NOT_CONNECTED = new TEMessage(2819, "Temperature Control not connected.", "Check temperature controls.", TEMessage.EType.Error, false);
        public static TEMessage SINGLE_HEAD_RUN_CHECK = new TEMessage(2820, "Single Head Run. Ensure Head B is removed.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage TEACH_NEEDLE_REQUIRED = new TEMessage(2821, "Please perform Teach Needle.", "", TEMessage.EType.Notification, false);
        public static TEMessage CHUCK_VAC_NOT_HIGH = new TEMessage(2822, "Chuck Vacuum Not Detected.@OK - Continue without vacuum.@Stop - Stop.", "1. Check part is properly located.@2. Check Chuck Vacuum switch setting.", TEMessage.EType.Notification, false);
        public static TEMessage FILL_COUNT_EXCEED_LIMIT = new TEMessage(2830, "Fill Count Exceed Limit.@OK - Continue.@Stop - Stop Operation.", "Replace Rotary Rod.", TEMessage.EType.Notification, false);
        public static TEMessage UNIT_COUNT_EXCEED_LIMIT = new TEMessage(2831, "Unit Count Exceed Limit.@OK - Continue.@Stop - Stop Operation.", "Replace Pump Consumables.", TEMessage.EType.Notification, false);
        public static TEMessage MATERIAL1_UNIT_RUN_EXCEEDED = new TEMessage(2832, "Material 1 Unit Run Exceeded Limit.@OK - Continue Dispense.@Stop - Stop Operation.", "Replace Material.", TEMessage.EType.Notification, false);
        public static TEMessage MATERIAL2_UNIT_RUN_EXCEEDED = new TEMessage(2833, "Material 2 Unit Run Exceeded Limit.@OK - Continue Dispense.@Stop - Stop Operation.", "Replace Material.", TEMessage.EType.Notification, false);
        public static TEMessage CALIBRATE_SPEED_TO_TIME_CANCELLED = new TEMessage(2850, "Calibrate Speed To Time Cancelled.", "", TEMessage.EType.Notification, false);
        public static TEMessage CALIBRATE_SPEED_TO_TIME_ERR = new TEMessage(2851, "Calibrate Speed To Time Error.", "1. Check parameter.@2. Check ExMessage for details.", TEMessage.EType.Error, false);
        public static TEMessage CALIBRATE_SPEED_TO_TIME_INVALID_PARA = new TEMessage(2852, "Calibrate Speed To Time Invalid Para.", "1. Check parameter.@2. Check ExMessage for details.", TEMessage.EType.Notification, false);
        public static TEMessage DO_REF_OFFSET_OOS = new TEMessage(2860, "DO_REF Offset Out Off Spec.@OK - Accept.@Stop - Stop.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage DO_REF_ANGLE_OOS = new TEMessage(2861, "DO_REF Angle Out Off Spec.@OK - Accept.@Stop - Stop.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage DO_REF_PT1_PT2_DIST_OOS = new TEMessage(2862, "DO_REF Pt1 and Pt2 Distance Out Off Spec.@OK - Accept.@Stop - Stop.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage DO_HEIGHT_TOLERANCE_OOS = new TEMessage(2865, "DO_HEIGHT Error Tolerance Out Off Spec.@Stop - Stop.@Retry - Retry DO_HEIGHT.@Cancel - Reject Board.", "1. Check Product.@2. Check Error Tol Value.", TEMessage.EType.Confirmation, false);
        public static TEMessage CREATE_MAP_OKYIELD_OOS = new TEMessage(2870, "CREATE_MAP OK Yield is Out Off Spec.@OK - Accept.@Stop - Stop.@Retry - Retry.@Cancel - Reject Board.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage DO_VISION_FAIL = new TEMessage(2880, "DO_VISION Fail.@OK - Manual.@Stop - Stop.@Retry - Retry DO_VISION.@Cancel - Reject Board.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage DISP_IS_BUSY = new TEMessage(2890, "Disp is busy.", "", TEMessage.EType.Notification, false);
        public static TEMessage VALVE_TIMER_EXPIRED = new TEMessage(2891, "Valve Timer Expired.@OK - Continue.@Stop - Stop Operation.", "", TEMessage.EType.Notification, false);
        public static TEMessage VALVE_DENSITY_FLOWRATE_MISMATCH = new TEMessage(2892, "Valve Density and Flowrate Mismatch.@OK - Continue.@Stop - Stop Operation.", "", TEMessage.EType.Notification, false);
        #endregion
        #region 2900 Program
        public static TEMessage PROGRAM_SCRIPT_ERR = new TEMessage(2900, "Progam Script Error.", "1. Check Script parameter.@2. Check ExMessage for details.", TEMessage.EType.Error, false);
        public static TEMessage PROGRAM_CANNOT_DELETE_ACTIVE = new TEMessage(2901, "Cannot delete active Progam.", "Ensure program to delete is not active.", TEMessage.EType.Notification, false);
        public static TEMessage PROGRAM_CONFIRM_DELETE = new TEMessage(2902, "Confirm to delete selected program?. Deleted program cannot be be undone.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage PROGRAM_HEAD_ERROR = new TEMessage(2903, "Program Script Head Assign Error.", "Check Script Head parameter.", TEMessage.EType.Error, false);
        public static TEMessage PROGRAM_DRAW_OFFSET_UPDATE = new TEMessage(2910, "Program Script Update Draw Offset?@OK - Update Offset.@Cancel - Do not Update.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage PROGRAM_ACTIVE_PROGRAM_COMMAND_MODIFICATION = new TEMessage(2911, "Program Script is Active. Cancel program to edit script.", "", TEMessage.EType.Notification, false);
        public static TEMessage VOLUME_OFST_PATH_NOT_FOUND = new TEMessage(2950, "Volume Offset Path Not Found.", "Check program Volume Offset Path. @OK - Continue without Volume Offset. @Stop - Stop.", TEMessage.EType.Error, false);
        public static TEMessage VOLUME_OFST_ERROR = new TEMessage(2951, "Volume Offset Error.", "Check program Volume Offset Setting", TEMessage.EType.Error, false);
        #endregion
        #region 3000 DispCore and Board
        public static TEMessage DISPCORE_NOT_INIT = new TEMessage(3010, "DispCore not Init.", "Pls contact NSW representative.", TEMessage.EType.Fault, false);
        public static TEMessage FRAME_COUNTER_FULL = new TEMessage(3020, "Frame Counter Reach Total Frame Count", "Machine Stop.", TEMessage.EType.Notification, false);
        public static TEMessage GANTRY_OPEN_BOARD1_FAIL = new TEMessage(3100, "Gantry Open Board1 Error.", "1. Check motion board is installed.@2. Check motion board drivers are installed.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_OPEN_BOARD2_FAIL = new TEMessage(3101, "Gantry Open Board2 Error.", "1. Check motion board is installed.@2. Check motion board drivers are installed.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_INIT_BOARD1_FAIL = new TEMessage(3105, "Gantry Init Board1 Error.", "1. Check motion board is installed.@2. Check motion board drivers are installed.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_INIT_BOARD2_FAIL = new TEMessage(3106, "Gantry Init Board2 Error.", "1. Check motion board is installed.@2. Check motion board drivers are installed.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_BOARD1_NOT_OPENED = new TEMessage(3110, "Gantry Board1 Not Opened.", "1. Check motion board is installed.@2. Check motion board drivers are installed.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_BOARD2_NOT_OPENED = new TEMessage(3111, "Gantry Board2 Not Opened.", "1. Check motion board is installed.@2. Check motion board drivers are installed.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_MOTION_EX_ERR = new TEMessage(3150, "Gantry Motion Exception Error.", "1. Check Motor Address.@2.Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Fault, false);
        public static TEMessage GANTRY_IO_EX_ERR = new TEMessage(3151, "Gantry IO Exception Error.", "1. Check IO Address.@2.Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Fault, false);
        #endregion
        #region 3300 Weight
        public static TEMessage WEIGHT_COMM_EX_ERR = new TEMessage(3300, "Weight Comm Exception Error.", "Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Error, false);
        public static TEMessage WEIGHT_NO_HEAD_SELECTED = new TEMessage(3301, "Weight No Head is Selected.", "Select Head to Measure or Calibrate.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_OPEN_ERR = new TEMessage(3310, "Weight Open Error.", "1. Check Weight connection.@2. Check System Config.@3 Check Weight settings.", TEMessage.EType.Error, false);
        public static TEMessage WEIGHT_GETVALUE_ERR = new TEMessage(3320, "Weight Get Value Error.", "Check Weight connection.", TEMessage.EType.Error, false);
        public static TEMessage WEIGHT_INVALID_TARGET = new TEMessage(3330, "Invalid Weight Target.", "Check Weight Target value is correct.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_INVALID_TOLERANCE = new TEMessage(3331, "Invalid Weight Tolerance.", "Check Weight Tolerance value is correct.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_CAL_CANCELLED = new TEMessage(3333, "Weight Calibration Cancelled.", "Check Weight Tolerance value is correct.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_INVALID_SPEC = new TEMessage(3340, "Invalid Weight Spec.", "Check Weight Target spec is correct.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_INVALID_SPEC_LIMIT = new TEMessage(3341, "Invalid Weight Spec Limit.", "Check Weight Spec Limit value is correct.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_CAL_REQUIRED = new TEMessage(3350, "Weight Calibration is not executed.", "Execute Weight Calibration.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_MEAS_REQUIRED = new TEMessage(3351, "Weight Measure is not executed.", "Execute Weight Measure.", TEMessage.EType.Notification, false);
        public static TEMessage WEIGHT_PROG_HEAD_OP_SINGLE_SELECTED = new TEMessage(3360, "Program Head Operation Single selected.", "Pump2 execution prohibited.", TEMessage.EType.Notification, false);
        #endregion
        #region 3400 Laser and Temp Sensor
        public static TEMessage LASER_COMM_EX_ERR = new TEMessage(3400, "Laser Comm Exception Error.", "Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Error, false);
        public static TEMessage LASER_OPEN_ERR = new TEMessage(3410, "Laser Open Error.", "1. Check Laser is connection.@2. Check Laser communication settings.", TEMessage.EType.Error, false);
        public static TEMessage LASER_NOT_CONFIG_ERR = new TEMessage(3415, "Laser Is Not Configured.", "1. Check Laser config.@2. Check Laser settings.", TEMessage.EType.Notification, false);
        public static TEMessage LASER_NOT_OPEN_ERR = new TEMessage(3420, "Laser Not Open Error.", "1. Check Laser connection.@2. Check Laser communication.@3.Check Laser settings.", TEMessage.EType.Error, false);
        public static TEMessage LASER_OUT_OF_RANGE_ERR = new TEMessage(3425, "Laser Out Of Range.", "1. Check Laser is within sensing range.", TEMessage.EType.Error, false);
        public static TEMessage LASER_SEARCH_ERR = new TEMessage(3425, "Laser Search Error.", "Check laser height and position.", TEMessage.EType.Error, false);
        public static TEMessage LASER_NOT_SUPPORTED = new TEMessage(3430, "Laser Not Supported.", "Laser type is not supported.", TEMessage.EType.Notification, false);
        public static TEMessage LASER_OUT_OF_REF_HEIGHT_TOL = new TEMessage(3450, "Out of Ref Height Tol.", "", TEMessage.EType.Notification, false);

        public static TEMessage TEMPSENSOR_OPEN_ERR = new TEMessage(3460, "Temp Sensor Open Error.", "1. Check Temp Sensor connection.@2. Check Temp Sensor settings.", TEMessage.EType.Error, false);
        public static TEMessage TEMPSENSOR_NOT_CONFIG_ERR = new TEMessage(3463, "Temp Sensor is not configured.", "Check Temp Sensor config", TEMessage.EType.Notification, false);
        public static TEMessage TEMPSENSOR_READ_FAIL = new TEMessage(3464, "Temp Sensor read fail", "Check Temp Sensor connection.", TEMessage.EType.Error, false);
        public static TEMessage TEMPSENSOR_SEARCH_FAIL = new TEMessage(3465, "Temp Sensor search fail", "Check Temp Sensor position.", TEMessage.EType.Error, false);
        #endregion
        #region 3500 DispCtrl PressCtrl
        public static TEMessage DISPCTRL_COMM_EX_ERR = new TEMessage(3500, "Disp Controller Comm Exception Error.", "Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_INIT = new TEMessage(3501, "Disp Controller Init?.@OK - Init Disp Controller.@Cancel - Do not Init Disp Controller.", "", TEMessage.EType.Confirmation, false);
        public static TEMessage DISPCTRL_ERR = new TEMessage(3505, "Disp Controller Error.@OK - Continue.@Stop - Stop Operation.", "1. Check Dispense Controller for Error Details.@2. Clear Dispenser Controller Errors.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_OPEN_ERR = new TEMessage(3510, "Disp Controller Open Error.", "1. Check Controller connection.@2. Check Controller settings.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL1_COMM_ERR = new TEMessage(3520, "Disp Controller 1 Comm Error.", "1. Check Controller connection.@2. Check Controller settings.@3. Check Disp Controller communcation cables.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL2_COMM_ERR = new TEMessage(3521, "Disp Controller 2 Comm Error.", "1. Check Controller connection.@2. Check Controller settings.@3. Check Disp Controller communcation cables.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_NOT_READY = new TEMessage(3557, "Disp Controller Not Ready.", "1. Check Controller Condition.@2. Check Controller IO Cables.@3. Check Controller IO settings.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_READY_TIMEOUT = new TEMessage(3562, "Disp Controller Ready TimeOut.", "1. Check Controller Condition.@2. Check Disp Ready TimeOut value.@3. Check Controller IO Cables.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_RESPONSE_TIMEOUT = new TEMessage(3563, "Disp Controller Response TimeOut.", "1. Check Controller Condition.@2. Check Disp Response TimeOut value.@3. Check Controller IO Cables.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_COMPLETE_TIMEOUT = new TEMessage(3679, "Disp Controller Complete TimeOut.", "1. Check Controller Condition.@2. Check Disp Response TimeOut value.@3. Check Controller IO Cables.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_ROTARY_TIMEOUT = new TEMessage(3580, "Disp Controller Rotary TimeOut.", "1. Check Pump Rotary. @2. Check Dispense Controller.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_THREAD_BUSY = new TEMessage(3590, "Disp Controller Thread is Busy.", "1. Check ExMessage.@2. Check Disp Controller.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_THREAD_ERROR = new TEMessage(3591, "Disp Controller Thread Error.", "1. Check ExMessage.@2. Check Disp Controller.", TEMessage.EType.Error, false);
        public static TEMessage DISPCTRL_THREAD_TIMEOUT = new TEMessage(3592, "Disp Controller Thread TimeOut.", "1. Check ExMessage.@2. Check Disp Controller.", TEMessage.EType.Error, false);
        public static TEMessage PRESSCTRL_THREAD_BUSY = new TEMessage(3595, "Pressure Controller Thread is Busy.", "1. Check ExMessage.@2. Check Pressure Controller.", TEMessage.EType.Error, false);
        public static TEMessage PRESSCTRL_THREAD_ERROR = new TEMessage(3596, "Pressure Controller Thread Error.", "1. Check ExMessage.@2. Check Pressure Controller.", TEMessage.EType.Error, false);
        public static TEMessage PRESSCTRL_THREAD_TIMEOUT = new TEMessage(3597, "Pressure Controller Thread TimeOut.", "1. Check ExMessage.@2. Check Pressure Controller.", TEMessage.EType.Error, false);
        #endregion
        #region 3600 Camera
        public static TEMessage CAMERA_COMM_EX_ERR = new TEMessage(3600, "Camera Comm Exception Error.", "Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Error, false);
        public static TEMessage CAMERA_INIT_ERR = new TEMessage(3601, "Camera Init Error.", "Check Vision Drivers.", TEMessage.EType.Error, false);
        public static TEMessage CAMERA_OPEN_ERR = new TEMessage(3610, "Camera Open Error.", "1. Check Camera connection.@2. Check Camera Drivers.", TEMessage.EType.Error, false);
        public static TEMessage CAMERA_NOT_CONFIG_ERR = new TEMessage(3620, "Camera Not Configured.", "1. Check System Config.@2. Check Camera configuration.", TEMessage.EType.Notification, false);
        public static TEMessage CAMERA_GRAB_TIMEOUT = new TEMessage(3621, "Camera Grab TimeOut.", "1. Check Camera settings.@2. Check Camera configuration.", TEMessage.EType.Error, false);
        public static TEMessage MONITORING_CAMERA1_OPEN_ERR = new TEMessage(3630, "MONITORING_CAMERA1_OPEN_ERR", "", TEMessage.EType.Error, false);
        public static TEMessage MONITORING_CAMERA2_OPEN_ERR = new TEMessage(3631, "MONITORING_CAMERA2_OPEN_ERR", "", TEMessage.EType.Error, false);
        public static TEMessage DISP_RECORDER_DISCONNECTED = new TEMessage(3650, "Disp Recorder Disconnected.", "", TEMessage.EType.Error, false);
        public static TEMessage DISP_RECORDER_COMMAND_ERR = new TEMessage(3651, "Disp Recorder Commmand Error.", "", TEMessage.EType.Error, false);
        public static TEMessage DISP_RECORDER_NO_RESPONSE_ERR = new TEMessage(3652, "Disp Recorder No Response Error.", "", TEMessage.EType.Error, false);
        public static TEMessage DISP_RECORDER_WAIT_TIMEOUT = new TEMessage(3653, "Disp Recorder No Response Error.", "", TEMessage.EType.Error, false);
        public static TEMessage TEMPLOGGER_DISCONNECTED = new TEMessage(3660, "TempLogger Disconneted.", "", TEMessage.EType.Error, false);
        public static TEMessage NEEDLE_INSP_NOT_IN_RUN_MODE = new TEMessage(3670, "Needle Insp not in Run Mode.", "", TEMessage.EType.Error, false);
        public static TEMessage NEEDLE_INSP_IS_BUSY = new TEMessage(3671, "Needle Insp IS Busy.", "", TEMessage.EType.Error, false);
        public static TEMessage NEEDLE_INSP_RESPONSE_TIMEOUT = new TEMessage(3672, "Needle Insp Response Timeout.", "", TEMessage.EType.Error, false);
        public static TEMessage NEEDLE_INSP_BUSY_TIMEOUT = new TEMessage(3673, "Needle Insp Busy Timeout.", "", TEMessage.EType.Error, false);
        public static TEMessage NEEDLE_INSP_FAIL = new TEMessage(3674, "Needle Insp Fail.", "", TEMessage.EType.Error, false);
        #endregion
        #region 3700 LedCtrl
        public static TEMessage LEDCTRL_COMM_EX_ERR = new TEMessage(3700, "Led Controller Comm Exception Error.", "Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Error, false);
        public static TEMessage LEDCTRL_UNKNOWN_CTRL_ERR = new TEMessage(3701, "Led Controller Not Recognized.", "Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Error, false);
        public static TEMessage LEDCTRL_OPEN_ERR = new TEMessage(3710, "Led Controller Open Error.", "1. Check Controller connection.@2. Check System Config.@3 Check Controller settings.", TEMessage.EType.Error, false);
        public static TEMessage LEDCTRL_SETVALUE_ERR = new TEMessage(3720, "Led Controller Set Value Error.", "Check Controller connection.", TEMessage.EType.Error, false);
        #endregion
        #region 3800 Other
        public static TEMessage CLEAN_TAPE_CTRL_NOT_READY = new TEMessage(3810, "Clean Tape Ctrl not Ready.", "", TEMessage.EType.Error, false);
        public static TEMessage CLEAN_TAPE_CTRL_READY_TIMEOUT = new TEMessage(3811, "Clean Tape Ctrl Ready Timeout.", "", TEMessage.EType.Error, false);
        public static TEMessage CLEAN_TAPE_CTRL_ALARM = new TEMessage(3815, "Clean Tape Ctrl Alarm.","", TEMessage.EType.Error, false);

        public static TEMessage COPY_FILE_FAIL = new TEMessage(3850, "Copy File Failed.","", TEMessage.EType.Error, false);
        #endregion
        public static TEMessage UNKNOWN_EX_ERR = new TEMessage(3999, "Unknown Exception Error.", "Check ExMessage and feedback to NSW Automation.", TEMessage.EType.Error, false);
        #region 4000 Conv Init
        public static TEMessage CONV_INIT_ACCESS = new TEMessage(4000, "Conveyor Init Confirmation.", "Are You Sure To Init Conveyor?", TEMessage.EType.Confirmation, false);
        public static TEMessage CONV_DRY_RUN_ACCESS = new TEMessage(4005, "Conveyor Dry Run Confirmation.", "Are You Sure to Dry Run The Conveyor?", TEMessage.EType.Confirmation, false);
        public static TEMessage ALL_INIT_ACCESS = new TEMessage(4006, "Conveyor Init ALL Confirmation.", "Are You Sure To Init Conveyor?", TEMessage.EType.Confirmation, false);
        #endregion
        #region 4100 Conv Control
        public static TEMessage CONV_OPEN_BOARD_FAIL = new TEMessage(4100, "Conveyor Open Board Error.", "1. Check ZKA Network Port are connected.", TEMessage.EType.Error, false);
        public static TEMessage CONV_VALUE_OUT_OF_RANGE = new TEMessage(4101, "Conveyor Value Out Of Range Error.", "Invalid Value or Value Out Of Range.", TEMessage.EType.Error, false);
        public static TEMessage CONV_NOT_READY = new TEMessage(4103, "Conveyor Not Ready.", "1. Check conveyor status.@2. Initialize conveyor.", TEMessage.EType.Notification, false);
        public static TEMessage CONV_BELT_RUN_ERROR = new TEMessage(4110, "Conveyor Belt Run Error.", "1. Check parameter of conveyor motor.@2. Check wiring from motor to ZKA connection.", TEMessage.EType.Error, false);
        public static TEMessage CONV_STOPPER_UP_TIMEOUT = new TEMessage(4150, "Conveyor Stopper Up Error.", "1. Check Stopper cylinder air and sensor.@2. Check Stopper cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_STOPPER_DN_TIMEOUT = new TEMessage(4151, "Conveyor Stopper Down Error.", "1. Check Stopper cylinder air and sensor.@2. Check Stopper cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_LIFTER_UP_TIMEOUT = new TEMessage(4152, "Conveyor Lifter Up Error.", "1. Check Lifter cylinder air and sensor.@2. Check Lifter cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_LIFTER_DN_TIMEOUT = new TEMessage(4153, "Conveyor Lifter Down Error.", "1. Check Lifter cylinder air and sensor.@2. Check Lifter cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_PRECISOR_EXT_TIMEOUT = new TEMessage(4154, "Conveyor Precisor Extend Error.", "1. Check Precisor air and sensor.@2. Check Precisor cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_PRECISOR_RET_TIMEOUT = new TEMessage(4155, "Conveyor Precisor Retract Error.", "1. Check Precisor air and sensor.@2. Check Precisor cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_KICKER_EXT_TIMEOUT = new TEMessage(4156, "Conveyor Kicker Extend Error.", "1. Check Kicker air and sensor.@2. Check Kicker cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_KICKER_RET_TIMEOUT = new TEMessage(4157, "Conveyor Kicker Retract Error.", "1. Check Kicker air and sensor.@2. Check Kicker cylinder delay.", TEMessage.EType.Error, false);
        public static TEMessage CONV_VACUUM_ON_TIMEOUT = new TEMessage(4158, "Conveyor Vacuum 1 On Error.", "1. Check Vacuum1 lines and supply.@2. Check Vacuum1 Switch presure value.", TEMessage.EType.Error, false);
        public static TEMessage CONV_VACUUM_OFF_TIMEOUT = new TEMessage(4159, "Conveyor Vacuum 1 Off Error.", "1. Check Vacuum1 lines and supply.@2. Check Vacuum1 Switch presure value.", TEMessage.EType.Error, false);
        public static TEMessage CONV_VACUUM2_ON_TIMEOUT = new TEMessage(4160, "Conveyor Vacuum 2 On Error.", "1. Check Vacuum2 lines and supply.@2. Check Vacuum2 Switch presure value.", TEMessage.EType.Error, false);
        public static TEMessage CONV_VACUUM2_OFF_TIMEOUT = new TEMessage(4161, "Conveyor Vacuum 2 Off Error.", "1. Check Vacuum2 lines and supply.@2. Check Vacuum2 Switch presure value.", TEMessage.EType.Error, false);
        public static TEMessage CONV_VACUUM_LOW = new TEMessage(4162, "Conveyor Vacuum Low.", "1. Check Vacuum lines and supply.@2. Check Vacuum Switch presure value.", TEMessage.EType.Error, false);
        #endregion
        #region 4200 Conv Process
        public static TEMessage CONV_IN_SENSOR_PSNT = new TEMessage(4200, "Conveyor In Sensor Present Error.", "1. Remove part at In Station. @2. Check InPsnt Sensor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_IN_CLEAR_SENSOR_PSNT = new TEMessage(4201, "Conveyor In Clear Sensor Present Error.", "1. Remove part at between Left Elevator and Conveyor. @2. Check In Clear Sensor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_BUF_SENSOR_PSNT = new TEMessage(4205, "Conveyor Buf Sensor Present Error.", "1. Remove part at Buffer. @2. Check Buffer Sensor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_OUT_SENSOR_PSNT = new TEMessage(4208, "Conveyor Out Sensor Present Error.", "1. Remove part at Out Station. @2. Check OutPsnt Sensor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_OUT_CLEAR_SENSOR_PSNT = new TEMessage(4209, "Conveyor Out Clear Sensor Present Error.", "1. Remove part at between Conveyor and Out Elevator. @2. Check Out Clear Sensor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_MANUAL_UNLOAD = new TEMessage(4212, "Conveyor Manual Unload Product.", "Unload Product at Out Station.", TEMessage.EType.Error, false);
        public static TEMessage CONV_SENSOR_PART_PSNT = new TEMessage(4215, "Part Present at Conveyor.", "Clear Part at Conveyor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_SENSOR_PART_MISSING = new TEMessage(4216, "Part Missing at Conveyor.", "Place Part at Conveyor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_UNLOAD_TIMEOUT = new TEMessage(4235, "Conveyor Unload Time Out.", "Check Part jam at Out Station.", TEMessage.EType.Error, false);
        public static TEMessage CONV_MOVE_TIMEOUT = new TEMessage(4236, "Conveyor Move Time Out.", "Check Part jam at Conveyor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_REVERSE_UNLOAD_TIMEOUT = new TEMessage(4237, "Conveyor Reverse Unload Time Out.", "Check Part jam at In Station.", TEMessage.EType.Error, false);
        public static TEMessage CONV_HEATER_OUT_OF_RANGE = new TEMessage(4250, "Conveyor Heater Out of Range Error.", "1. Wait Heater Process Warm Up.@2. Check Heater Controller and Thermocouple.", TEMessage.EType.Error, false);
        #endregion
        #region 4500 Conv Smema
        public static TEMessage CONV_SMEMA_LEFT_BOARD_IN_TIMEOUT = new TEMessage(4500, "Left(UpLine) Smema Load Time Out.", "Check Product at Conveyor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_SMEMA_RIGHT_IN_TIMEOUT = new TEMessage(4501, "Right Smema Unload Time Out.", "Check Product at Conveyor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_SMEMA_RIGHT_REVERSE_WAIT_BOARD_TIMEOUT = new TEMessage(4502, "Right(DownLine) Smema Wait Board Time Out.", "Check Product at Conveyor.", TEMessage.EType.Error, false);
        public static TEMessage CONV_SMEMA_LEFT_BOARD_OUT_TIMEOUT = new TEMessage(4510, "Left Smema2 Board Sendout Time Out.", "Check Product at Conveyor.", TEMessage.EType.Error, false);
        #endregion
        public static TEMessage CONV_EX_ERR = new TEMessage(4998, "Conveyor Exception Error.", "Check ExMessage for details.", TEMessage.EType.Error, false);
        #region 5000 Elev Common
        public static TEMessage ELEV_ALL_INIT_ACCESS = new TEMessage(5000, "Elevator All Init Confirmation.", "Are You Sure To Init All Elevator?", TEMessage.EType.Confirmation, false);
        public static TEMessage ELEV_LEFT_INIT_ACCESS = new TEMessage(5001, "Elevator Left Init Confirmation.", "Are You Sure To Init Left Elevator?", TEMessage.EType.Confirmation, false);
        public static TEMessage ELEV_RIGHT_INIT_ACCESS = new TEMessage(5002, "Elevator Right Init Confirmation.", "Are You Sure To Init Right Elevator?", TEMessage.EType.Confirmation, false);
        public static TEMessage ELEV_DRY_RUN_ACCESS = new TEMessage(5003, "Elevator Dry Run Confirmation.", "Are You Sure to Dry Run The Elevator?", TEMessage.EType.Confirmation, false);
        public static TEMessage ELEV_LEFT_PUSHER_INIT_ACCESS = new TEMessage(5005, "Elevator Left Pusher Init Confirmation.", "Are You Sure To Init Left Pusher Elevator?", TEMessage.EType.Confirmation, false);
        #endregion
        #region 5100 Control
        public static TEMessage ELEV_OPEN_BOARD_FAIL = new TEMessage(5100, "Elevator Open Board Error.", "1. Check ZKA Network Port are connected.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_CONT_SEND_FAIL = new TEMessage(5105, "Elevator Pusher Continuous Send Product Fail.", "Retry - Continue Send.@Stop - Disable Input.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_EXT_TIME_OUT = new TEMessage(5130, "Elevator Pusher Ext Time Out.", "1. Check Pusher Status.@2. Check LZ Pusher Sensors.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_RET_TIME_OUT = new TEMessage(5131, "Elevator Pusher Ret Time Out.", "1. Check Pusher Status.@2. Check LZ Pusher Sensors.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_SENS_HOME_ERROR = new TEMessage(5132, "Elevator Pusher Home Sensor Error.", "Check Pusher Home Sensor Condition.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_SENS_LIMIT_ERROR = new TEMessage(5133, "Elevator Pusher Limit Sensor Error.", "Check Pusher Limit Sensor Condition.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_NOT_HOME_ERROR = new TEMessage(5134, "Elevator Pusher Home Error.", "1. Check Pusher is Home. 2. Check Pusher Home Sensor.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_JAM = new TEMessage(5135, "Elevator Pusher Jam.", "Check Part Jam at Pusher.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_HOME_FUNCTIONAL_ERROR = new TEMessage(5136, "Elevator Home Functional Error.", "Check Pusher Home Sensor Condition.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_PUSHER_ABNORMAL_STATE = new TEMessage(5137, "Elevator Pusher Abnormal State.", "1. Check Pusher Home Sensor faulty.@2. Check Pusher Limit Sensor faulty.", TEMessage.EType.Error, false);
        #endregion
        #region 5200 Motion
        public static TEMessage ELEV_MOVE_RELATIVE_ERR = new TEMessage(5200, "Elevator Motion Move Relative Error.", "1. Check ZKA Network Connection Status.@2. Check Motor Status.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_SET_MOTION_PARAM_ERR = new TEMessage(5201, "Elevator Set Motion Parameter Error.", "1. Check ZKA Network Connection Status.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_AXIS_WAIT_ERR = new TEMessage(5202, "Elevator Axis Wait Error.", "1. Check ZKA Network Connection Status.@2. Check Motor Status", TEMessage.EType.Error, false);
        public static TEMessage ELEV_AXIS_JOG_FAIL = new TEMessage(5203, "Elevator Jog Direction Fail.", "1. Check ZKA Network Connection Status.@2. Check Motor Status", TEMessage.EType.Error, false);
        public static TEMessage ELEV_FORCE_STOP_FAIL = new TEMessage(5204, "Elevator Force Stop Fail.", "1. Check ZKA Network Connection Status.@2. Check Motor Status.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_MOVE_CONST_ERR = new TEMessage(5205, "Elevator Motion Move Constant Error.", "1. Check ZKA Network Connection Status.@2. Check Motor Status.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_MOTOR_ALARM = new TEMessage(5230, "Motor Alarm Error.", "1. Check Axis Status.@2. Check Motor.@3. Check ZKA Network Connection Status.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_HOME_TIME_OUT = new TEMessage(5231, "Elevator Search Home Time Out.", "1. Check Axis Status.@2. Check Home Sensor.@3. Check Motor.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_MOVE_POS_ERROR = new TEMessage(5233, "Elevator Motor Move Position Error.", "1. Check Axis Status.@2. Check Motor.@3. Check ZKA Network Connection Status.", TEMessage.EType.Error, false);
        public static TEMessage ELEV_MAG_MISSING = new TEMessage(5240, "Elevator Magazine is Missing.", "1. Check Magazine Present.@2. Check Magazine Sensor.", TEMessage.EType.Error, false);
        #endregion
        public static TEMessage ELEV_EX_ERR = new TEMessage(5998, "Elevator Exception Error.", "Check ExMessage for details.", TEMessage.EType.Error, false);
    }

    public class TCMessages
    {
        public static List<string> MessageList()
        {
            var msglist = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            List<string> list = new List<string>();
            foreach (var msg in msglist)
            {
                list.Add($"{msg.Code:d4},{(int)msg.Type},{Convert.ToInt32(msg.Assist)},{msg.Desc},{msg.CAct},{msg.DescAlt},{msg.CActAlt}");
            }

            return list;
        }
        public static bool SaveList()
        {
            List<string> list = MessageList();

            FileStream F = new FileStream(GDefine.MsgFile, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter W = new StreamWriter(F);

            try
            {
                W.WriteLine("Code,Type,Assist,Desc,CAct,DescAlt,CActAlt");
                W.WriteLine("Type: Error = 0, Warning = 1, Fault = 2, Notification = 3, Confirmation = 4, Run = 5, Idle = 6, Custom1 = 8, Custom2 = 9");
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
        public static bool LoadList()
        {
            var msglist = typeof(Messages).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(TEMessage))
                .Select(x => (TEMessage)x.GetValue(null)).ToArray();

            if (!File.Exists(GDefine.MsgFile)) return true;

            FileStream F = new FileStream(GDefine.MsgFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Write);
            StreamReader R = new StreamReader(F);

            string FileLine = "";
            try
            {
                FileLine = R.ReadToEnd();
            }
            catch
            {
                return false;
            }
            finally
            {
                R.Close();
            }

            string[] Lines = FileLine.Split(new char[] { (char)10 }, StringSplitOptions.RemoveEmptyEntries);

            for (int Line = 1; Line < Lines.Count(); Line++)
            {
                string[] s = Lines[Line].Split(',');

                //"{msg.Code:d4},{(int)msg.Type},{Assist},{msg.Desc},{msg.CAct},{msg.DescAlt},{msg.CActAlt}"
                int code = 0;
                try
                {
                    s[0] = s[0].Trim();
                    code = Convert.ToInt16(s[0]);
                }
                catch { }

                int type = 0;
                try
                {
                    s[1] = s[1].Trim();
                    type = Convert.ToInt16(s[1]);
                }
                catch { }

                string descAlt = s[5].Trim();
                string cActAlt = s[6].Trim();

                foreach (var msg in msglist)
                {
                    if (msg.Code == code)
                    {
                        msg.Type = (TEMessage.EType)type;
                        msg.DescAlt = descAlt;
                        msg.CActAlt = cActAlt;
                    }
                }
            }

            return true;
        }
    }
}
