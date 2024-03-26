using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Xml;

namespace Vermes
{
    #region Command list
    /*
    MDS3200_
    Micro Dispenser HV, 4072HVA-M

    * ESR?<LF><CR>
    * IDN?<LF><CR>
    * ESR2?<LF><CR>
    * OPC?<LF><CR>
    
    ADJUST:?<LF><CR>
    ADJUST:START<LF><CR>
    
    HEATER:?<LF><CR>
    HEATER:1:ON/OFF<LF><CR>
    HEATER:230V/110V<LF><CR>
    
    KEY:ENTER<LF><CR>
    KEY:ESCAPE<LF><CR>
    
    HELP<LF><CR>LCD?<LF><CR>
    
    MAINT:STATUS<LF><CR>
    MAINT:MESSAGE:ON/OFF<LF><CR>
    
    SYSTEM:KLOCK:ON/OFF<LF><CR>
    SYSTEM:SHOW:CYCLES<LF><CR>
    SYSTEM:SHOW:VALVEID<LF><CR>
    SYSTEM:SHOW:CONTROLLERID<LF><CR>
    SYSTEM:SHOW:STATUS<LF><CR>
    SYSTEM:SHOW:ACTTEMP<LF><CR>
    SYSTEM:DOSOKDELAY:ON/OFF<LF><CR>
    SYSTEM:SINGLEDOSOK:PULSE/SETUP<LF><CR>
    SYSTEM:PASSWORD:<value><LF><CR>
    SYSTEM:PASSWORD:ON/OFF<LF><CR>
    SYSTEM:PASSWORD:SET:<value><LF><CR>
    SYSTEM:AUXILIARYMODE:ON/OFF<LF><CR>
    
    TEMP:?<LF><CR>
    TEMP:<value><LF><CR>
    
    TRIGGER:SET:?<LF><CR>
    TRIGGER:SET:<values><LF><CR>
    TRIGGER:ASET:?<LF><CR>
    TRIGGER:ASET:<values><LF><CR>
    STRIGGER:SET:<values><LF><CR>
    STRIGGER:ASET:<values><LF><CR>
    
    VALVE:UP<LF><CR>
    VALVE:DOWN<LF><CR>
    VALVE:OPEN<LF><CR>
    VALVE:OPEN:<values><LF><CR>
    VALVE:OPENS0/1/2/3<LF><CR>
    VALVE:AOPEN<LF><CR>
    VALVE:AOPEN:<values><LF><CR>
    VALVE:AOPENS0/1/2/3<LF><CR>
    SVALVE:OPEN<LF><CR>
    SVALVE:OPEN:<values><LF><CR>
    SVALVE:OPENS0/1/2/3<LF><CR>
    SVALVE:AOPEN<LF><CR>
    SVALVE:AOPEN:<values><LF><CR>
    SVALVE:AOPENS0/1/2/3<LF><CR>
    
    WRITE:LCD:<text><LF><CR>
    
    TAPPET:SET:<value><LF><CR>
    TAPPET:CLEAR<LF><CR>
    NOZZLE:SET:<value><LF><CR>
    NOZZLE:CLEAR<LF><CR>
    
    SCENARIO:STATUS<LF><CR>
    SCENARIO:ON/OFF<LF><CR>
    SCENARIO:PLCSTOP:<value>:ON/OFF<LF><CR>
    SCENARIO:SAVE:<value>:<values><LF><CR>
    SCENARIO:READ:<value><LF><CR>
    
    SETUP:SAVE:<value>:<values><LF><CR>
    SETUP:ASAVE:<value>:<values><LF><CR>
    SETUP:READ:<value><LF><CR>
    SETUP:AREAD:<value><LF><CR>
    
    BAUDRATE:0/1/2/3/4<LF><CR>
    GETTD<LF><CR>
    MDC:RESTART<LF><CR>
    */
    #endregion

    public class TEVermesMDS3200
    {
        SerialPort sp = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);

        public enum ECtrlModel { NONE, UNKNOWN, MDS3200A, MDS3200P, MDC3090P };
        public ECtrlModel CtrlModel = ECtrlModel.NONE;


        public TEVermesMDS3200()
        {
            Load("Default");
        }

        frmVermesMSD3200Log frmLog = new frmVermesMSD3200Log();
        public void AddLog(string S)
        {
            if (frmLog.Visible) frmLog.AddLog(S);
        }

        public class ASCII
        {
            public static string GetGlyph(Char C)
            {
                switch (C)
                {
                    #region
                    default: return C.ToString();// "<" + (byte)C + ">";
                    case (char)0: return "<NUL>";
                    case (char)1: return "<SOH>";
                    case (char)2: return "<STX>";
                    case (char)3: return "<ETX>";
                    case (char)4: return "<EOT>";
                    case (char)5: return "<ENQ>";
                    case (char)6: return "<ACK>";
                    case (char)7: return "<BEK>";
                    case (char)8: return "<BS>";
                    case (char)9: return "<HT>";
                    case (char)10: return "<LF>";
                    case (char)11: return "<VT>";
                    case (char)12: return "<FF>";
                    case (char)13: return "<CR>";
                    case (char)14: return "<SO>";
                    case (char)15: return "<SI>";
                    case (char)16: return "<DLE>";
                    case (char)17: return "<DC1>";
                    case (char)18: return "<DC2>";
                    case (char)19: return "<DC3>";
                    case (char)20: return "<DC4>";
                    case (char)21: return "<NAK>";
                    case (char)22: return "<SYN>";
                    case (char)23: return "<ETB>";
                    case (char)24: return "<CAN>";
                    case (char)25: return "<EM>";
                    case (char)26: return "<SUB>";
                    case (char)27: return "<ESC>";
                    case (char)28: return "<FS>";
                    case (char)29: return "<GS>";
                    case (char)30: return "<RS>";
                    case (char)31: return "<US>";
                    case (char)127: return "<DEL>";
                        #endregion
                }
            }
            public static string GetGlyph(string S)
            {
                string str = "";
                foreach (char C in S)
                {
                    str = str + ASCII.GetGlyph(C);
                }

                return str;
            }
        }
        Mutex ComMutex = new Mutex();
        #region Low Level
        public void TX(string tx)
        {
            if (!sp.IsOpen) throw new Exception("Port Is Not Open.");


            ComMutex.WaitOne();
            try
            {
                tx = tx + (char)10 + (char)13;
                sp.Write(tx);
                AddLog("<- " + ASCII.GetGlyph(tx));
            }
            catch (Exception ex)
            {
                AddLog(ex.Message.ToString());
                throw;
            }
            finally
            {
                ComMutex.ReleaseMutex();
            }
        }

        public void RX_Clear()
        {
            try
            {
                sp.DiscardInBuffer();
            }
            catch
            {
                throw;
            }
        }
        public int RX_Buffer
        {
            get
            {
                try
                {
                    return sp.BytesToRead;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void RX_Existing(ref string rx)
        {
            ComMutex.WaitOne();
            try
            {
                rx = sp.ReadExisting();
                AddLog("-> " + ASCII.GetGlyph(rx));
            }
            catch (Exception ex)
            {
                AddLog(ex.Message.ToString());
                throw;
            }
            finally
            {
                ComMutex.ReleaseMutex();
            }
        }
        public void RX_Resp(ref string rx)
        {
            ComMutex.WaitOne();
            try
            {
                sp.ReadTimeout = 50;
                rx = "";
                while (true)
                {
                    rx = rx + sp.ReadExisting();
                    Thread.Sleep(50);
                    if (sp.BytesToRead == 0) break;
                }
                AddLog("-> " + ASCII.GetGlyph(rx));
            }
            catch (Exception ex)
            {
                AddLog(ex.Message.ToString());
                throw;
            }
            finally
            {
                ComMutex.ReleaseMutex();
            }
        }
        public void RX_EOR(ref string rx)
        {
            ComMutex.WaitOne();
            try
            {
                sp.ReadTimeout = 3000;
                string EOR = ((char)13).ToString();
                rx = sp.ReadTo(EOR);
                rx = rx.Remove(rx.Length - 1);
                AddLog("-> " + ASCII.GetGlyph(rx));
            }
            catch (Exception ex)
            {
                AddLog(ex.Message.ToString());
                throw;
            }
            finally
            {
                ComMutex.ReleaseMutex();
            }
        }
        #endregion

        string _AppPath = "c:\\Vermes";
        string _ParamLogPath = "c:\\Vermes";
        string _SetupName = "default";
        int _CtrlID = 0;

        public string AppPath
        {
            set
            {
                _AppPath = value;
            }
            get
            {
                return _AppPath;
            }
        }
        public string ParamLogPath
        {
            set
            {
                _ParamLogPath = value;
            }
        }

        public string SetupName
        {
            get
            {
                return _SetupName;
            }
        }
        public int CtrlID
        {
            get
            {
                return _CtrlID;
            }
            set
            {
                _CtrlID = value;
            }
        }

        public string _DeviceInfo = "";
        internal uint _ComPortNo = 1;
        internal string _ControllerID = "";
        internal string _ValveID = "";
        internal uint _Cycles = 0;

        /// <summary>
        /// Open MDS3200 at ComPort
        /// </summary>
        /// <param name="ComPort"></param>
        /// <returns></returns>
        public bool Open(string ComPort)
        {
            try
            {
                CtrlModel = ECtrlModel.NONE;

                string s_ComPortNo = ComPort.Remove(0, 3);
                _ComPortNo = (uint)Convert.ToInt32(s_ComPortNo);

                sp.PortName = ComPort;
                sp.Open();

                AddLog("Open - " + ComPort);

                //MDC 3200+ Controller always starts with 4072(HVA-X) and MDC 3200A Controllers have a 4062(HVA-X)
                TX("*IDN?");
                string rx = "";
                RX_EOR(ref rx);
                _DeviceInfo = rx;

                if (rx.Contains("4072")) CtrlModel = ECtrlModel.MDS3200P;
                else
                    if (rx.Contains("4062")) CtrlModel = ECtrlModel.MDS3200A;
                else
                    if (rx.Contains("4071")) CtrlModel = ECtrlModel.MDC3090P;
                else
                    CtrlModel = ECtrlModel.UNKNOWN;

                TX("SYSTEM:SHOW:CONTROLLERID");
                RX_EOR(ref rx);
                _ControllerID = rx;

                TX("SYSTEM:SHOW:VALVEID");
                RX_EOR(ref rx);
                _ValveID = rx;

                TX("SYSTEM:SHOW:CYCLES");
                RX_EOR(ref rx);
                _Cycles = 0;
                try
                {
                    _Cycles = Convert.ToUInt32(rx);
                }
                catch { }

                UpdateHeater();

                Set();

                return true;
            }
            catch (Exception Ex)
            {
                sp.Close();
                AddLog("Open - " + Ex.Message.ToString());
                throw Ex;
            }
        }
        public bool IsOpen
        {
            get
            {
                return sp.IsOpen;
            }
        }
        public void Close()
        {
            try
            {
                sp.Close();
                AddLog("Close");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            finally
            {
                CtrlModel = ECtrlModel.NONE;
                _DeviceInfo = "";
            }
        }

        public int Cycles
        {
            get { return (int)_Cycles; }
        }
        public void Refresh()
        {
            try
            {
                TX("SYSTEM:SHOW:CYCLES");
                string rx = "";
                RX_EOR(ref rx);

                try
                {
                    _Cycles = Convert.ToUInt32(rx);
                }
                catch { }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
        }

        public void ShowLog()
        {
            frmLog.TopMost = true;
            frmLog.Show();
        }
        public void ShowControl()
        {
            frmVermesMDS3200 frm = new frmVermesMDS3200();
            frm.MDS3200 = this;
            frm.TopMost = true;

            frm.Show();
        }

        public class DispParam
        {
            public double RI = 0.5;
            public double[] RI_CL = new double[] { 0.01, 300 };
            public double OT = 2.0;
            public double[] OT_CL = new double[] { 0, 3000 };
            public double FA = 0.2;
            public double[] FA_CL = new double[] { 0.01, 300 };
            public uint NL = 80;
            public uint[] NL_CL = new uint[] {1, 100 };
            public uint NP = 1;
            public double DL = 20;
        }
        public DispParam Param = new DispParam();
        public class THeater
        {
            public uint SetTemp = 25;
            public bool On = false;
        }
        public THeater Heater = new THeater();

        public void Save(string SetupName)
        {
            try
            {
                if (!Directory.Exists(_AppPath)) Directory.CreateDirectory(_AppPath);
            }
            catch
            {
                throw;
            }

            _SetupName = SetupName;

            NUtils.IniFile Inifile = new NUtils.IniFile(_AppPath + "\\" + _SetupName + ".ini");

            string s_Section = "Param";
            if (_CtrlID > 0) s_Section = s_Section + _CtrlID.ToString();

            Inifile.WriteDouble(s_Section, "RI", Param.RI);
            Inifile.WriteDouble(s_Section, "OT", Param.OT);
            Inifile.WriteDouble(s_Section, "FA", Param.FA);
            Inifile.WriteInteger(s_Section, "NL", Param.NL);
            Inifile.WriteInteger(s_Section, "NP", Param.NP);
            Inifile.WriteDouble(s_Section, "DL", Param.DL);

            Inifile.WriteInteger(s_Section, "SetTemp", Heater.SetTemp);
            Inifile.WriteBool(s_Section, "HeaterOn", Heater.On);
        }
        public void Load(string SetupName)
        {
            try
            {
                if (!Directory.Exists(_AppPath)) Directory.CreateDirectory(_AppPath);
            }
            catch
            {
                throw;
            }

            _SetupName = SetupName;

            NUtils.IniFile Inifile = new NUtils.IniFile(_AppPath + "\\" + _SetupName + ".ini");

            string s_Section = "Param";
            if (_CtrlID > 0) s_Section = s_Section + _CtrlID.ToString();

            Param.RI = Inifile.ReadDouble(s_Section, "RI", 0.5);
            Param.OT = Inifile.ReadDouble(s_Section, "OT", 2.0);
            Param.FA = Inifile.ReadDouble(s_Section, "FA", 0.2);
            Param.NL = (uint)Inifile.ReadInteger(s_Section, "NL", 80);
            Param.NP = (uint)Inifile.ReadInteger(s_Section, "NP", 1);
            Param.DL = Inifile.ReadDouble(s_Section, "DL", 20);

            Heater.SetTemp = Inifile.ReadInteger(s_Section, "SetTemp", (uint)25);
            Heater.On = Inifile.ReadBool(s_Section, "HeaterOn", false);
        }

        //string s_FileName = "";
        //string s_Root = "";
        //string s_Chapter = "";
        //public void saveXML(string fileName, string root, string chapter)
        //{
        //    s_FileName = fileName;
        //    s_Root = root;
        //    s_Chapter = chapter;

        //    NUtils.XmlFile xmlFile = new NUtils.XmlFile(fileName, root);
        //    try
        //    {
        //        xmlFile.Open();

        //        string s_Section = "Ctrl";
        //        if (_CtrlID > 0) s_Section = s_Section + _CtrlID.ToString();

        //        xmlFile.SetValue(root, chapter, s_Section, "RI", Param.RI);
        //        xmlFile.SetValue(root, chapter, s_Section, "OT", Param.OT);
        //        xmlFile.SetValue(root, chapter, s_Section, "FA", Param.FA);
        //        xmlFile.SetValue(root, chapter, s_Section, "NL", Param.NL);
        //        xmlFile.SetValue(root, chapter, s_Section, "NP", Param.NP);
        //        xmlFile.SetValue(root, chapter, s_Section, "DL", Param.DL);

        //        xmlFile.SetValue(root, chapter, s_Section, "SetTemp", (int)Heater.SetTemp);
        //        xmlFile.SetValue(root, chapter, s_Section, "HeaterOn", Heater.On);
        //    }
        //    finally
        //    {
        //        xmlFile.Save();
        //    }
        //}
        //public void savelastXML()
        //{
        //    if (s_FileName.Length == 0) return;
        //    if (s_Root.Length == 0) return;
        //    if (s_Chapter.Length == 0) return;

        //    saveXML(s_FileName, s_Root, s_Chapter);
        //}
        //public void loadXML(string fileName, string root, string chapter)
        //{
        //    s_FileName = fileName;
        //    s_Root = root;
        //    s_Chapter = chapter;

        //    NUtils.XmlFile xmlFile = new NUtils.XmlFile(fileName, root);
        //    try
        //    {
        //        xmlFile.Open();

        //        string s_Section = "Ctrl";
        //        if (_CtrlID > 0) s_Section = s_Section + _CtrlID.ToString();

        //        Param.RI = xmlFile.GetValue(root, chapter, s_Section, "RI", 0.5);
        //        Param.OT = xmlFile.GetValue(root, chapter, s_Section, "OT", 2.0);
        //        Param.FA = xmlFile.GetValue(root, chapter, s_Section, "FA", 0.2);
        //        Param.NL = (uint)xmlFile.GetValue(root, chapter, s_Section, "NL", 80);
        //        Param.NP = (uint)xmlFile.GetValue(root, chapter, s_Section, "NP", 1);
        //        Param.DL = xmlFile.GetValue(root, chapter, s_Section, "DL", 20);

        //        Heater.SetTemp = (uint)xmlFile.GetValue(root, chapter, s_Section, "SetTemp", (int)25);
        //        Heater.On = xmlFile.GetValue(root, chapter, s_Section, "HeaterOn", false);
        //    }
        //    finally { }
        //}

        public void loadXML(XmlReader reader)
        {
            Param.RI = 0.5;
            Param.OT = 2.0;
            Param.FA = 0.2;
            Param.NL = 80;
            Param.NP = 1;
            Param.DL = 20;
            Heater.SetTemp = 25;
            Heater.On = false;

            try
            {
                while (reader.Read())
                {
                    if (reader.Name == "section" && reader.MoveToContent() == XmlNodeType.EndElement) break;

                    if (reader.Name == "entry" && reader["name"] == "Ctrl")
                    {
                        while (reader.Read())
                        {
                            if (reader.Name == "entry" && reader.MoveToContent() == XmlNodeType.EndElement) break;

                            if (reader.Name == "subentry")
                            {
                                string attName = reader["name"];
                                reader.Read();
                                switch (attName)
                                {
                                    case "RI":
                                        Param.RI = Convert.ToDouble(reader.Value);
                                        break;
                                    case "RI_CL":
                                        {
                                            string s = reader.Value;
                                            string[] d = s.Split(new char[] { ',' });
                                            if (d[0] != null) Param.RI_CL[0] = Convert.ToDouble(d[0]);
                                            if (d[1] != null) Param.RI_CL[1] = Convert.ToDouble(d[1]);
                                        break;
                                }
                                    case "OT":
                                        Param.OT = Convert.ToDouble(reader.Value);
                                        break;
                                    case "OT_CL":
                                        {
                                            string s = reader.Value;
                                            string[] d = s.Split(new char[] { ',' });
                                            if (d[0] != null) Param.OT_CL[0] = Convert.ToDouble(d[0]);
                                            if (d[1] != null) Param.OT_CL[1] = Convert.ToDouble(d[1]);
                                            break;
                                        }
                                    case "FA":
                                        Param.FA = Convert.ToDouble(reader.Value);
                                        break;
                                    case "FA_CL":
                                        {
                                            string s = reader.Value;
                                            string[] d = s.Split(new char[] { ',' });
                                            if (d[0] != null) Param.FA_CL[0] = Convert.ToDouble(d[0]);
                                            if (d[1] != null) Param.FA_CL[1] = Convert.ToDouble(d[1]);
                                            break;
                                        }
                                    case "NL":
                                        Param.NL = Convert.ToUInt32(reader.Value);
                                        break;
                                    case "NL_CL":
                                        {
                                            string s = reader.Value;
                                            string[] d = s.Split(new char[] { ',' });
                                            if (d[0] != null) Param.NL_CL[0] = Convert.ToUInt32(d[0]);
                                            if (d[1] != null) Param.NL_CL[1] = Convert.ToUInt32(d[1]);
                                            break;
                                        }
                                    case "NP":
                                        Param.NP = Convert.ToUInt32(reader.Value);
                                        break;
                                    case "DL":
                                        double.TryParse(reader.Value, out Param.DL);
                                        //Param.DL =  Convert.ToInt32(reader.Value);
                                        break;
                                    case "SetTemp":
                                        try
                                        {
                                            Heater.SetTemp = Convert.ToUInt32(reader.Value);
                                        }
                                        catch { }
                                        break;
                                    case "HeaterOn":
                                        Heater.On = Convert.ToBoolean(reader.Value);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            finally { }
        }
        public void saveXML(XmlWriter writer)
        {
            try
            {
                writer.WriteStartElement("entry");
                writer.WriteAttributeString("name", "Ctrl");

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "RI");
                writer.WriteString(Param.RI.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "RI_CL");
                writer.WriteString($"{ Param.RI_CL[0]},{ Param.RI_CL[1]}");
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "OT");
                writer.WriteString(Param.OT.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "OT_CL");
                writer.WriteString($"{ Param.OT_CL[0]},{ Param.OT_CL[1]}");
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "FA");
                writer.WriteString(Param.FA.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "FA_CL");
                writer.WriteString($"{ Param.FA_CL[0]},{ Param.FA_CL[1]}");
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "NL");
                writer.WriteString(Param.NL.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "NL_CL");
                writer.WriteString($"{ Param.NL_CL[0]},{ Param.NL_CL[1]}");
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "NP");
                writer.WriteString(Param.NP.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "DL");
                writer.WriteString(Param.DL.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "SetTemp");
                writer.WriteString(Heater.SetTemp.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("subentry");
                writer.WriteAttributeString("name", "HeaterOn");
                writer.WriteString(Heater.On.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
            catch { }
        }

        double[] MIN_RI = new double[101] {
            0.01,
            0.01, 0.01, 0.01, 0.02, 0.02, 0.02, 0.03, 0.03, 0.03, 0.03,//01~10 
            0.04, 0.04, 0.04, 0.05, 0.05, 0.05, 0.06, 0.06, 0.06, 0.06,//11~20
            0.07, 0.07, 0.07, 0.08, 0.08, 0.08, 0.09, 0.09, 0.09, 0.09,//21~30
            0.10, 0.10, 0.10, 0.11, 0.11, 0.11, 0.12, 0.12, 0.12, 0.12,//31~40
            0.13, 0.13, 0.13, 0.14, 0.14, 0.14, 0.15, 0.15, 0.15, 0.15,//41~50
            0.16, 0.16, 0.16, 0.17, 0.17, 0.17, 0.18, 0.18, 0.18, 0.18,//51~60
            0.19, 0.19, 0.19, 0.20, 0.20, 0.20, 0.21, 0.21, 0.21, 0.21,//61~70
            0.22, 0.22, 0.22, 0.23, 0.23, 0.23, 0.24, 0.24, 0.24, 0.24,//71~80
            0.25, 0.25, 0.25, 0.26, 0.26, 0.26, 0.27, 0.27, 0.27, 0.27,//81~90
            0.28, 0.28, 0.28, 0.29, 0.29, 0.29, 0.30, 0.30, 0.30, 0.30 };//91~100
        double[] MIN_FA = new double[101] {
            0.01,
            0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01,//01~10
            0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02,//11~20
            0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.03,//21~30
            0.04, 0.04, 0.04, 0.04, 0.04, 0.04, 0.04, 0.04, 0.04, 0.04,//31~40
            0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05,//41~50
            0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06,//51~60 
            0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.07,//61~70
            0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08,//71~80 
            0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09,//81~90 
            0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10 };//91~100
        double[] MIN3090_RI = new double[101] {
            0.01,//0
            0.01, 0.01, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05,//01~10 
            0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10, 0.10,//11~20
            0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15,//21~30
            0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20, 0.20,//31~40
            0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25,//41~50
            0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30,//51~60
            0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35,//61~70
            0.40, 0.40, 0.40, 0.40, 0.40, 0.40, 0.40, 0.40, 0.40, 0.40,//71~80
            0.45, 0.45, 0.45, 0.45, 0.45, 0.45, 0.45, 0.45, 0.45, 0.45,//81~90
            0.50, 0.50, 0.50, 0.50, 0.50, 0.50, 0.50, 0.50, 0.50, 0.50 };//91~100
        double[] MIN3090_FA = new double[101] {
            0.01,
            0.01, 0.01, 0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.03,//01~10
            0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06, 0.06,//11~20
            0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09, 0.09,//21~30
            0.12, 0.12, 0.12, 0.12, 0.12, 0.12, 0.12, 0.12, 0.12, 0.12,//31~40
            0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15,//41~50
            0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18,//51~60 
            0.21, 0.21, 0.21, 0.21, 0.21, 0.21, 0.21, 0.21, 0.21, 0.21,//61~70
            0.24, 0.24, 0.24, 0.24, 0.24, 0.24, 0.24, 0.24, 0.24, 0.24,//71~80 
            0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27,//81~90 
            0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30, 0.30 };//91~100
        public void Set_Tx(ref double Rise_ms, ref double Open_ms, ref double Fall_ms, ref uint NeedleLift_pc, ref uint Pulse, ref double Delay_ms)
        {
            #region Range Check
            NeedleLift_pc = Math.Max(NeedleLift_pc, 1);
            NeedleLift_pc = Math.Min(NeedleLift_pc, 100);

            uint NL = (uint)(NeedleLift_pc);

            switch (CtrlModel)
            {
                case ECtrlModel.MDC3090P:
                    Rise_ms = Math.Max(Rise_ms, MIN3090_RI[NL]);
                    break;
                default:
                    Rise_ms = Math.Max(Rise_ms, MIN_RI[NL]);
                    break;
            }
            Rise_ms = Math.Min(Rise_ms, 300);

            if (NeedleLift_pc > 80)
                Open_ms = Math.Min(Open_ms, 15);
            else
                Open_ms = Math.Min(Open_ms, 3000);
            Open_ms = Math.Max(Open_ms, 0);

            switch (CtrlModel)
            {
                case ECtrlModel.MDC3090P:
                    Fall_ms = Math.Max(Fall_ms, MIN3090_FA[NL]);
                    break;
                default:
                    Fall_ms = Math.Max(Fall_ms, MIN_FA[NL]);
                    break;
            }
            Fall_ms = Math.Min(Fall_ms, 300);

            //Pulse = Math.Max(Pulse, 1);
            Pulse = Math.Min(Pulse, 32000);

            //if (HeaterIsOn)
            //{
            //    Delay_ms = Math.Max(Delay_ms, 2.0);
            //}

            switch (CtrlModel)
            {
                case ECtrlModel.MDS3200P:
                    {
                        if (Heater.On)
                            Delay_ms = Math.Max(Delay_ms, 2);
                        else
                            Delay_ms = Math.Max(Delay_ms, 0.1);
                        break;
                    }
                case ECtrlModel.MDS3200A:
                    {
                        if (Heater.On)
                            Delay_ms = Math.Max(Delay_ms, 4.3);
                        else
                            Delay_ms = Math.Max(Delay_ms, 0);
                        break;
                    }
            }
            Delay_ms = Math.Min(Delay_ms, 1000);
            #endregion

            uint RI = (uint)(Rise_ms * 100);
            uint OT = (uint)(Open_ms * 10);
            uint FA = (uint)(Fall_ms * 100);
            uint NP = (uint)Pulse;
            uint DL = (uint)(Delay_ms * 10);

            Param.RI = Rise_ms;
            Param.OT = Open_ms;
            Param.FA = Fall_ms;
            Param.NL = NeedleLift_pc;
            Param.NP = Pulse;
            Param.DL = Delay_ms;

            try
            {
                TX("TRIGGER:ASET:" + RI.ToString() + "," + OT.ToString() + "," + FA.ToString() + "," + NL.ToString() + "," + NP.ToString() + "," + DL.ToString());
            }
            catch
            {
                throw;
            }
        }
        public void Set_Rx()
        {
            try
            {
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("OK")) return;
                if (rx.Contains("NAK")) throw new Exception("Invalid Command or Value.");
                throw new Exception("Invalid Response.");
            }
            catch
            {
                throw;
            }
        }
        public void Set(ref double Rise_ms, ref double Open_ms, ref double Fall_ms, ref uint NeedleLift_pc, ref uint Pulse, ref double Delay_ms)
        {
            try
            {
                Set_Tx(ref Rise_ms, ref Open_ms, ref Fall_ms, ref NeedleLift_pc, ref Pulse, ref Delay_ms);
                Set_Rx();
            }
            catch
            {
                throw;
            }
        }
        public void Set()
        {
            try
            {
                Set(ref Param.RI, ref Param.OT, ref Param.FA, ref Param.NL, ref Param.NP, ref Param.DL);
            }
            catch
            {
                throw;
            }
        }

        public void ValveUp()
        {
            try
            {
                TX("VALVE:UP");
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("OK")) return;
                if (rx.Contains("NAK")) throw new Exception("Invalid Command or Value.");
                throw new Exception("Invalid Response.");
            }
            catch
            {
                throw;
            }
        }
        public void ValveDown()
        {
            try
            {
                TX("VALVE:DOWN");
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("OK")) return;
                if (rx.Contains("NAK")) throw new Exception("Invalid Command or Value.");
                throw new Exception("Invalid Response.");
            }
            catch
            {
                throw;
            }
        }
        public void ValveOpen()
        {
            try
            {
                TX("VALVE:OPEN");
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("OK")) return;
                if (rx.Contains("NAK")) throw new Exception("Invalid Command or Value.");
                throw new Exception("Invalid Response.");
            }
            catch
            {
                throw;
            }
        }

        public void HeaterOn()
        {
            try
            {
                TX("HEATER:1:ON");
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("OK"))
                {
                    Heater.On = true;
                    return;
                }
                if (rx.Contains("No Heater")) throw new Exception("No Nozzle Heater attached.");
                throw new Exception("Invalid Response.");
            }
            catch
            {
                throw;
            }
        }
        public void HeaterOff()
        {
            try
            {
                TX("HEATER:1:OFF");
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("OK"))
                {
                    Heater.On = false;
                    return;
                }
                throw new Exception("Invalid Response.");
            }
            catch
            {
                throw;
            }
        }

        public bool HeaterIsOn = false;
        public uint HeaterSetTemp = 25;
        public void UpdateHeater()
        {
            try
            {
                switch (CtrlModel)
                {
                    case ECtrlModel.MDS3200A:
                        {
                            TX("TEMP:?");
                            string rx = "";
                            RX_EOR(ref rx);//No Heater

                            HeaterIsOn = !rx.Contains("No Heater");
                            break;
                        }
                    case ECtrlModel.MDS3200P:
                        {
                            TX("TEMP:?");
                            string rx = "";
                            RX_EOR(ref rx);//no Heater

                            HeaterIsOn = !rx.Contains("No Heater");
                            break;
                        }
                }
            }
            catch
            {
                throw;
            }
        }
        public void HeaterSet()
        {
            if (Heater.On)
            {
                HeaterOn();
                Temp(ref Heater.SetTemp);
            }
            else
                HeaterOff();
        }

        public void Temp(ref uint Temp)//deg C
        {
            try
            {
                TX("TEMP:?");
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("No Heater")) throw new Exception("No Nozzle Heater attached.");
                Temp = Convert.ToUInt32(rx);
            }
            catch
            {
                throw;
            }
        }
        public void SetTemp(uint Temp)//deg C
        {
            try
            {
                TX("TEMP:" + Temp.ToString());
                string rx = "";
                RX_EOR(ref rx);

                if (rx.Contains("OK")) return;
                throw new Exception("Invalid Response.");
            }
            catch
            {
                throw;
            }
        }
    }
}




