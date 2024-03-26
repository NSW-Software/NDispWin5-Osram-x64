using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDispWin
{
    public class TaskLaser
    {
#pragma warning disable CS0436 // Type conflicts with imported type
        public static CLaser.MEDAQ Sensor = new CLaser.MEDAQ();
#pragma warning restore CS0436 // Type conflicts with imported type

        public static int SettleTime = 0;
        public static int TempSensor_SettleTime = 100;

        public static void LoadSetup()
        {
            NSW.Net.IniFile IniFile = new NSW.Net.IniFile();

            string Filename = GDefine.SetupPath + "\\Laser.Setup.ini";
            IniFile.Create(Filename);

            SettleTime = IniFile.ReadInteger("Laser", "SettleTime", 200);
        }
        public static void SaveSetup()
        {
            NSW.Net.IniFile IniFile = new NSW.Net.IniFile();

            string Filename = GDefine.SetupPath + "\\Laser.Setup.ini";
            IniFile.Create(Filename);

            IniFile.WriteInteger("Laser", "SettleTime", SettleTime);
        }

        public static bool LaserOpened
        {
            get
            {
                switch (GDefine.HSensorType)
                {
                    case GDefine.EHeightSensorType.None:
                    case GDefine.EHeightSensorType.ILD1302:
                    case GDefine.EHeightSensorType.ILD1700:
                    case GDefine.EHeightSensorType.IFD2451:
                    case GDefine.EHeightSensorType.IFD2421:
                    case GDefine.EHeightSensorType.ILD1X20:
                    case GDefine.EHeightSensorType.ILD1750:
                    case GDefine.EHeightSensorType.IFD2422:
                    case GDefine.EHeightSensorType.ILD1900:
                        return Sensor.IsConnected;
                    default:
                        return false;
                }
            }
        }

        public static void OpenLaser()
        {
            string EMsg = "Open Laser";

            try
            {
                string ComPort = "COM" + GDefine.HSensorComport.ToString();

                switch (GDefine.HSensorType)
                {
                    case GDefine.EHeightSensorType.None:
                        break;
                    case GDefine.EHeightSensorType.ILD1302:
                        Sensor.Open(CLaser.MEDAQ.ESensorType.ILD1302, ComPort);
                        break;
                    case GDefine.EHeightSensorType.ILD1X20:
                        Sensor.Open(CLaser.MEDAQ.ESensorType.ILD1X20, ComPort);
                        break;
                    case GDefine.EHeightSensorType.ILD1700:
                        Sensor.Open(CLaser.MEDAQ.ESensorType.ILD1700, ComPort);
                        break;
                    case GDefine.EHeightSensorType.ILD1750:
                        Sensor.Open(CLaser.MEDAQ.ESensorType.ILD1750, ComPort);
                        Sensor.ILD1750.DataOutputRS422 = true;
                        break;
                    case GDefine.EHeightSensorType.IFD2451:
                        if (ComPort.Contains("COM0"))
                            Sensor.Open(CLaser.MEDAQ.ESensorType.IFD2451, GDefine.HSensorIPAddress);
                        else
                            Sensor.Open(CLaser.MEDAQ.ESensorType.IFD2451, ComPort);
                        break;
                    case GDefine.EHeightSensorType.IFD2421:
                        if (ComPort.Contains("COM0"))
                            Sensor.Open(CLaser.MEDAQ.ESensorType.IFD2421, GDefine.HSensorIPAddress);
                        else
                            Sensor.Open(CLaser.MEDAQ.ESensorType.IFD2421, ComPort);
                        break;
                    case GDefine.EHeightSensorType.IFD2422:
                        if (ComPort.Contains("COM0"))
                            Sensor.Open(CLaser.MEDAQ.ESensorType.IFD2422, GDefine.HSensorIPAddress);
                        else
                            Sensor.Open(CLaser.MEDAQ.ESensorType.IFD2422, ComPort);
                        break;
                    case GDefine.EHeightSensorType.ILD1900:
                        if (ComPort.Contains("COM0"))
                            Sensor.Open(CLaser.MEDAQ.ESensorType.ILD1900, GDefine.HSensorIPAddress);
                        else
                            Sensor.Open(CLaser.MEDAQ.ESensorType.ILD1900, ComPort);
                        break;
                }
            }
            catch (Exception Ex)
            {
                EMsg = EMsg + (char)13 + Ex.Message;
                Msg MsgBox = new Msg();
                MsgBox.Show(Messages.LASER_OPEN_ERR, EMsg);
            }
        }
        public static void CloseLaser()
        {
            try
            {
                Sensor.Close();
            }
            catch { }
        }

        public static bool GetHeight(ref double Value, bool PromptError)
        {
            string EMsg = "LaserGetDistance";

            try
            {
                switch (GDefine.HSensorType)
                {
                    default:
                    case GDefine.EHeightSensorType.None:
                        if (PromptError)
                        {
                            Msg MsgBox = new Msg();
                            MsgBox.Show(Messages.LASER_NOT_CONFIG_ERR, EMsg);
                        }
                        return false;
                    case GDefine.EHeightSensorType.ILD1X20:
                    case GDefine.EHeightSensorType.IFD2421:
                    case GDefine.EHeightSensorType.ILD1750:
                    case GDefine.EHeightSensorType.IFD2422:
                    case GDefine.EHeightSensorType.ILD1302:
                    case GDefine.EHeightSensorType.ILD1700:
                    case GDefine.EHeightSensorType.IFD2451:
                    case GDefine.EHeightSensorType.ILD1900:
                        {
                            int retried = 0;
                            _Retry:
                            try
                            {
                                if (!Sensor.Poll_ScaledAbsInv(ref Value))
                                {
                                    if (PromptError)
                                    {
                                        Msg MsgBox = new Msg();
                                        MsgBox.Show(Messages.LASER_OUT_OF_RANGE_ERR);
                                        return false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (retried > 0) throw;

                                Log.AddToLog(EMsg + " " + ex.Message.ToString() + " - Retry");
                                retried++;
                                goto _Retry;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                if (PromptError)
                {
                    EMsg = EMsg + (char)13 + ex.Message;
                    Msg MsgBox = new Msg();
                    MsgBox.Show(Messages.LASER_COMM_EX_ERR, EMsg);
                    return false;
                }
            }
            return true;
        }
        public static bool GetHeight(ref double Value)
        {
            return GetHeight(ref Value, true);
        }

        public static void DataAvail(ref int DataAvail)
        {
            try
            {
                switch (GDefine.HSensorType)
                {
                    case GDefine.EHeightSensorType.ILD1700:
                    case GDefine.EHeightSensorType.ILD1302:
                    case GDefine.EHeightSensorType.IFD2451:
                    case GDefine.EHeightSensorType.ILD1X20:
                    case GDefine.EHeightSensorType.ILD1750:
                    case GDefine.EHeightSensorType.IFD2421:
                    case GDefine.EHeightSensorType.IFD2422:
                    case GDefine.EHeightSensorType.ILD1900:
                        Sensor.DataAvail(ref DataAvail);
                        break;
                }
            }
            catch { throw; }
        }
        public static void TransferData(double[] Data, ref int Count)
        {
            try
            {
                switch (GDefine.HSensorType)
                {
                    case GDefine.EHeightSensorType.ILD1700:
                    case GDefine.EHeightSensorType.ILD1302:
                    case GDefine.EHeightSensorType.IFD2451:
                    case GDefine.EHeightSensorType.ILD1X20:
                    case GDefine.EHeightSensorType.ILD1750:
                    case GDefine.EHeightSensorType.IFD2421:
                    case GDefine.EHeightSensorType.IFD2422:
                    case GDefine.EHeightSensorType.ILD1900:
                        {
                            int[] RawData = new int[Data.Length];
                            double[] ScaleData = new double[Data.Length];
                            Sensor.TransferData(RawData, ScaleData, Data, ref Count);
                        }
                        break;
                }
            }
            catch { throw; }
        }

        public static double SampleRate
        {
            set
            {
                try
                {
                    switch (GDefine.HSensorType)
                    {
                        case GDefine.EHeightSensorType.ILD1700:
                            Sensor.ILD1700.Meas.RateVal = SampleRate;
                            break;
                        case GDefine.EHeightSensorType.ILD1302:
                            //no rate support, default 750Hz
                            break;
                        case GDefine.EHeightSensorType.IFD2451:
                            Sensor.IFD2451.Meas.SampleRateVal = SampleRate;
                            break;
                    }
                }
                catch { throw; }
            }
            get
            {
                try
                {
                    switch (GDefine.HSensorType)
                    {
                        case GDefine.EHeightSensorType.ILD1700:
                            return Sensor.ILD1302.Meas.RateVal;
                        case GDefine.EHeightSensorType.ILD1302:
                            return Sensor.ILD1700.Meas.RateVal;
                        case GDefine.EHeightSensorType.IFD2451:
                            return Sensor.IFD2451.Meas.SampleRateVal;
                        default:
                            return 0;
                    }
                }
                catch { throw; }
            }
        }

        public static bool TrigMode
        {
            set
            {
                try
                {
                    if (value)
                    {
                        switch (GDefine.HSensorType)
                        {
                            case GDefine.EHeightSensorType.ILD1302:
                                Sensor.ILD1302.DataOutput = false;
                                break;
                            case GDefine.EHeightSensorType.ILD1X20:
                            case GDefine.EHeightSensorType.ILD1750:
                            case GDefine.EHeightSensorType.IFD2421:
                            case GDefine.EHeightSensorType.IFD2422:
                                Sensor.ILD1320.Trig.Mode = CLaser.MEDAQ.E_ILD1X20_TrigMode.Software;
                                Sensor.ILD1320.Trig.Count = -1;
                                break;
                            case GDefine.EHeightSensorType.ILD1700:
                                Sensor.ILD1700.DataOutput = false;
                                break;
                            case GDefine.EHeightSensorType.IFD2451:
                                Sensor.IFD2451.Trig.Mode = CLaser.MEDAQ.E_IFD2451_TrigMode.Software;
                                Sensor.IFD2451.Trig.Count = -1;
                                break;
                        }
                    }
                    else
                    {
                        switch (GDefine.HSensorType)
                        {
                            case GDefine.EHeightSensorType.ILD1302:
                                Sensor.ILD1302.DataOutput = true;
                                break;
                            case GDefine.EHeightSensorType.ILD1X20:
                            case GDefine.EHeightSensorType.ILD1750:
                            case GDefine.EHeightSensorType.IFD2421:
                            case GDefine.EHeightSensorType.IFD2422:
                                Sensor.ILD1320.Trig.Mode = CLaser.MEDAQ.E_ILD1X20_TrigMode.None;
                                Sensor.ILD1320.Trig.Count = -1;
                                break;
                            case GDefine.EHeightSensorType.ILD1700:
                                Sensor.ILD1700.DataOutput = true;
                                break;
                            case GDefine.EHeightSensorType.IFD2451:
                                Sensor.IFD2451.Trig.Mode = CLaser.MEDAQ.E_IFD2451_TrigMode.None;
                                Sensor.IFD2451.Trig.Count = -1;
                                break;
                        }
                    }
                }
                catch { throw; }
            }
        }
        public static void SwTrig()
        {
            try
            {
                switch (GDefine.HSensorType)
                {
                    case GDefine.EHeightSensorType.ILD1700:
                        Sensor.ILD1302.DataOutput = true;
                        break;
                    case GDefine.EHeightSensorType.ILD1X20:
                    case GDefine.EHeightSensorType.ILD1750:
                    case GDefine.EHeightSensorType.IFD2421:
                    case GDefine.EHeightSensorType.IFD2422:
                        Sensor.ILD1320.Trig.SwTrig();
                        break;
                    case GDefine.EHeightSensorType.ILD1302:
                        Sensor.ILD1700.DataOutput = true;
                        break;
                    case GDefine.EHeightSensorType.IFD2451:
                        Sensor.IFD2451.Trig.SwTrig();
                        break;
                }
            }
            catch { throw; }
        }
    }
}
