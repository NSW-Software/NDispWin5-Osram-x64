using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace NDispWin
{
    class TELotInfo
    {
    }


    class LotInfo2
    {
        public enum ECustomer
        {
            None,
            Reserved2,//LUMDisp,
            Reserved3,//LUMConfocal,
            OsramDisp,
            OsramConfocal,
            Reserved6,//NXPDisp,
            OsramEMos,
            Reserved8,//Analog,
            OsramICC
        }
        public static ECustomer Customer = ECustomer.None;

        public enum ELotStatus { None, Activated, Deactivated };
        public static ELotStatus LotStatus = ELotStatus.None;
        public static bool LotActive
        {
            get
            {
                return (LotStatus == ELotStatus.Activated);
            }
            set
            {
                LotStatus = value ? ELotStatus.Activated : ELotStatus.Deactivated;
            }
        }

        public enum ELotEvent { None, LotStart, LotEnd, LotRestart }
        public static ELotEvent LotEvent = ELotEvent.None;

        public static bool LoadRecipe = false;
        public static string _SProgramRecipe = "";

        //General Lot Info
        public static string LotNumber = "";
        public static string RecipeName = "";
        public static string sOperatorID = "";
        public static string sShift = "";
        public static string sStartTime = "";
        public static string sEndTime = "";
        public static string sMachineID = "";
        public static string sCatridgeAID = "";
        public static string sCatridgeBID = "";
        public static string sCatridgeCID = "";
        public static string sCatridgeDID = "";
        public static int MatLife = 0;

        internal class Osram
        {
            public static string ElevenSeries = "";//aka Material Nr
            public static string DAStartNumber = "";
            public static string Operation = "";

            public static string F5Name = "";
            public static string F6Name = "";
            public static string F7Name = "";
            public static string F8Name = "";
            public static string F5Value = "";
            public static string F6Value = "";
            public static string F7Value = "";
            public static string F8Value = "";
            
            public static int LoadRecipeIndex = 0;

            public static string SetupFile = GDefine.AppPath + @"\\Osram_LotEntrySetup.csv";
            public static void SaveSetup()
            {
                string fName = SetupFile;

                if (!Directory.Exists(Path.GetDirectoryName(fName))) Directory.CreateDirectory(Path.GetDirectoryName(fName));

                FileStream F = new FileStream(fName, FileMode.Create, FileAccess.Write, FileShare.Write);
                StreamWriter W = new StreamWriter(F);
                try
                {
                    W.WriteLine("Field5Name" + "," + F5Name);
                    W.WriteLine("Field6Name" + "," + F6Name);
                    W.WriteLine("Field7Name" + "," + F7Name);
                    W.WriteLine("Field8Name" + "," + F8Name);
                    W.WriteLine("RecipeName" + "," + RecipeName);
                    W.WriteLine("LotID" + "," + LotNumber);
                    W.WriteLine("MaterialNumber" + "," + ElevenSeries);
                    W.WriteLine("Operation" + "," + Operation);
                    W.WriteLine("OperatorID" + "," + sOperatorID);
                    foreach (var kvp in TFSecsGem.SubstrateStatus)
                    {
                        W.WriteLine($"SubstrateStatus,{kvp.Key},{kvp.Value}");
                    }
                }
                finally
                {
                    W.Close();
                }
            }
            public static void LoadSetup()
            {
                return;
                string fName = SetupFile;

                if (!Directory.Exists(Path.GetDirectoryName(fName))) Directory.CreateDirectory(Path.GetDirectoryName(fName));

                if (!File.Exists(fName)) return;

                try
                {
                    FileStream F = new FileStream(fName, FileMode.Open, FileAccess.ReadWrite, FileShare.Write);
                    StreamReader R = new StreamReader(F);

                    string S = R.ReadToEnd();
                    R.Close();

                    string[] Lines = S.Split(new char[] { (char)10 }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string l in Lines)
                    {
                        string[] line = l.Split(',');

                        if (line[0].StartsWith("Field5Name"))
                        {
                            F5Name = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("Field6Name"))
                        {
                            F6Name = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("Field7Name"))
                        {
                            F7Name = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("Field8Name"))
                        {
                            F8Name = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("RecipeName"))
                        {
                            RecipeName = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("LotID"))
                        {
                            LotNumber = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("MaterialNumber"))
                        {
                            ElevenSeries = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("Operation"))
                        {
                            Operation = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("OperatorID"))
                        {
                            sOperatorID = line[1].Trim();
                            continue;
                        }
                        if (line[0].StartsWith("SubstrateStatus"))
                        {
                            var key = line[1].Trim();
                            var value = line[2].Trim();
                            TFSecsGem.SubstrateStatus[key] = value;
                        }
                    }
                }
                catch
                { }
            }
        }
    }
}
