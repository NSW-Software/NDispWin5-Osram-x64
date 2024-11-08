﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using SocketV1;

namespace NDispWin
{
    public class AOT_HeightCloseLoop
    {
        private static string WorkPath = "";
        public static string Path
        {
            set
            {
                WorkPath = value;
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);
            }
            get
            {
                return WorkPath;
            }
        }

        public static bool CheckPath()
        {
            if (!Directory.Exists(WorkPath))
            {
                throw new Exception("Path was found.");
            }

            return true;
        }

        /// <summary>
        /// Return the number of *.csv files. 
        /// Return -1 if current path is not valid. 
        /// </summary>
        /// <returns>Number of *.csv files</returns>
        public static int Count()
        {
            if (!Directory.Exists(WorkPath)) return -1;

            string[] files = Directory.GetFiles(WorkPath, "*.csv");

            return files.Count();
        }

        /// <summary>
        /// Decode all *.csv in current path and return the params of latest file.
        /// </summary>
        /// <param name="FileTag"></param>
        /// <param name="Eq_ID"></param>
        /// <param name="Ofst1"></param>
        /// <param name="Ofst2"></param>
        /// <returns></returns>
        public static bool DecodeFile(ref string FileTag, ref string Eq_ID, ref double Ofst1, ref double Ofst2)
        {
            string[] files = Directory.GetFiles(WorkPath, "*.csv");

            if (files.Count() == 0)
            {
                return true;
            }

            int i_LastFileIdx = 0;
            UInt64 i_LastFileTag = 0;
            for (int i = 0; i < files.Count(); i++)// each (string f in files)
            {
                string FileName = files[i];
                string[] s_line = FileName.Split(new char[] { '_' });
                FileTag = s_line[3];

                try
                {
                    //FileTag = FileTag.Remove(0, 4);
                    UInt64 i_FileTag = Convert.ToUInt64(FileTag);
                    if (i_FileTag > i_LastFileTag)
                    {
                        i_LastFileTag = i_FileTag;
                        i_LastFileIdx = i;
                    }
                }
                catch
                {
                    throw new Exception("Decode File Tag Error.");
                }
            }

            //string FileName = files[0];
            //string[] s_line = FileName.Split(new char[] { '_' });
            //FileTag = s_line[3]; 
            List<string[]> data = new List<string[]>();//data[row][col]
            char[] delimiters = new char[] { ',' };
            using (StreamReader reader = new StreamReader(files[i_LastFileIdx]))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    string[] l = line.Split(delimiters);
                    data.Add(l);
                }
            }

            Eq_ID = data[3][5];
            double D1 = 0;
            try
            {
                D1 = Convert.ToDouble(data[2][10]);
            }
            catch
            {
                HandleError();
                throw new Exception("Decode Offset 1 Error.");
            }
            Ofst1 = D1;

            double D2 = 0;
            try
            {
                D2 = Convert.ToDouble(data[3][10]);
            }
            catch
            {
                HandleError();
                throw new Exception("Decode Offset 2 Error.");
            }
            Ofst2 = D2;

            string WorkPath_updated = WorkPath + @"\updated";

            if (!Directory.Exists(WorkPath_updated))
                try
                {
                    Directory.CreateDirectory(WorkPath_updated);
                }
                catch { throw; }

            string Date = DateTime.Now.Date.ToString("yyyyMMdd");
            string Time = DateTime.Now.ToString("HH:mm:ss");
            string MM = DateTime.Now.Month.ToString();
            if (MM.Length == 1) { MM = "0" + MM; }
            string YYYY = DateTime.Now.Year.ToString();
            string DD = DateTime.Now.Day.ToString();
            if (DD.Length == 1) { DD = "0" + DD; }

            string WorkPath_Month = WorkPath_updated + @"\" + YYYY + MM + @"\";
            if (!Directory.Exists(WorkPath_Month))
                try
                { Directory.CreateDirectory(WorkPath_Month); }
                catch { throw; }

            string SourceFName = files[i_LastFileIdx];
            string DestFName = WorkPath_Month + "\\" + System.IO.Path.GetFileName(SourceFName);

            try
            {
                File.Copy(SourceFName, DestFName, true);
            }
            catch { throw; }

            return true;
        }
        public static void HandleError()
        {
            if (!Directory.Exists(WorkPath)) return;

            string WorkPath_Error = WorkPath + @"\Error";

            if (!Directory.Exists(WorkPath_Error))
                try
                {
                    Directory.CreateDirectory(WorkPath_Error);
                }
                catch
                {
                    throw new Exception("Create Error Path Error.");
                }

            string[] files = Directory.GetFiles(WorkPath);

            try
            {
                foreach (string file in files)
                {
                    string FNameExt = System.IO.Path.GetFileName(file);
                    string FName = System.IO.Path.GetFileNameWithoutExtension(file);
                    string Ext = System.IO.Path.GetExtension(file);

                    if (File.Exists(WorkPath_Error + @"\" + FNameExt))
                    {
                        int Idx = 1;
                        while (File.Exists(WorkPath_Error + @"\" + FName + "(" + Idx.ToString() + ")" + Ext))
                        {
                            Idx++;
                        }
                        File.Move(WorkPath_Error + @"\" + FNameExt, WorkPath_Error + @"\" + FName + "(" + Idx.ToString() + ")" + Ext);
                    }

                    File.Move(file, WorkPath_Error + @"\" + FNameExt);
                }
            }
            catch
            {
                throw new Exception("Delete file Error.");
            }
        }
        public static void PurgeFiles()
        {
            if (!Directory.Exists(WorkPath)) return;

            string WorkPath_Purge = WorkPath + @"\Purge";

            if (!Directory.Exists(WorkPath_Purge))
                try
                {
                    Directory.CreateDirectory(WorkPath_Purge);
                }
                catch
                {
                    throw new Exception("Create Purge Path Error.");
                }

            string[] files = Directory.GetFiles(WorkPath);

            try
            {
                foreach (string file in files)
                {
                    string FNameExt = System.IO.Path.GetFileName(file);
                    string FName = System.IO.Path.GetFileNameWithoutExtension(file);
                    string Ext = System.IO.Path.GetExtension(file);

                    if (File.Exists(WorkPath_Purge + @"\" + FNameExt))
                    {
                        int Idx = 1;
                        while (File.Exists(WorkPath_Purge + @"\" + FName + "(" + Idx.ToString() + ")" + Ext))
                        {
                            Idx++;
                        }
                        File.Move(WorkPath_Purge + @"\" + FNameExt, WorkPath_Purge + @"\" + FName + "(" + Idx.ToString() + ")" + Ext);
                    }

                    File.Move(file, WorkPath_Purge + @"\" + FNameExt);
                }
            }
            catch
            {
                throw new Exception("Delete file Error.");
            }
        }
        public static void DeleteFiles()
        {
            if (!Directory.Exists(WorkPath)) return;

            try
            {
                string[] files = Directory.GetFiles(WorkPath);

                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            catch
            {
                throw new Exception("Delete file Error.");
            }
        }
    }
    public class AOT_FrontTestCloseLoop
    {
        static string Local_Path = "";
        static string Local_LogPath = "";
        static string Local_DataPath = "";
        static string DataFile_Path = "";
        static string CompFile_Path = "";
        static string EqID = "EqID";
        static string DTFile = Local_Path + "\\DTFile.txt";//date time path

        /// <summary>
        /// Get/Set Local Path.
        /// </summary>
        public static string LocalPath
        {
            set
            {
                Local_Path = value;
                DTFile = Local_Path + "\\DTFile.txt";
                //if (!Directory.Exists(Local_Path))
                //    Directory.CreateDirectory(Local_Path);
                #region Create Parent Log Path
                if (!Directory.Exists(Local_Path))
                    try
                    {
                        Directory.CreateDirectory(Local_Path);
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception("Create Local_Path Error. " + Ex.Message.ToString());
                    }

                Local_LogPath = Local_Path + "\\Log";
                if (!Directory.Exists(Local_LogPath))
                    try
                    { Directory.CreateDirectory(Local_LogPath); }
                    catch (Exception Ex)
                    {
                        throw new Exception("Create Local_LogPath Error. " + Ex.Message.ToString());
                    }

                Local_DataPath = Local_Path + "\\Data";
                if (!Directory.Exists(Local_DataPath))
                    try
                    { Directory.CreateDirectory(Local_DataPath); }
                    catch (Exception Ex)
                    {
                        throw new Exception("Create Local_DataPath Error. " + Ex.Message.ToString());
                    }
                #endregion
            }
            get
            {
                return Local_Path;
            }
        }
        /// <summary>
        /// Get/Set Data File Path.
        /// </summary>
        public static string DataFilePath
        {
            set
            {
                DataFile_Path = value;
                if (!Directory.Exists(DataFile_Path))
                    Directory.CreateDirectory(DataFile_Path);
            }
            get
            {
                return DataFile_Path;
            }
        }
        /// <summary>
        /// Get/Set Compensation File Path.
        /// </summary>
        public static string CompFilePath
        {
            set
            {
                CompFile_Path = value;
                if (!Directory.Exists(CompFile_Path))
                    Directory.CreateDirectory(CompFile_Path);
            }
            get
            {
                return CompFile_Path;
            }
        }
        public static string EquipmentID
        {
            set
            {
                EqID = value;
            }
            get
            {
                return EqID;
            }
        }

        /// <summary>
        /// Add S to log.
        /// </summary>
        /// <param name="S">Log string</param>
        public static void AddToLog(string S)
        {
            if (!Directory.Exists(Local_LogPath)) return;

            DateTime LogDT = DateTime.Now;
            int Y = LogDT.Year;
            int M = LogDT.Month;
            int D = LogDT.Day;
            int H = LogDT.Hour;
            int m = LogDT.Minute;
            int s = LogDT.Second;
            int n = LogDT.Millisecond;

            DateTime T1 = new DateTime(Y, M, D, 8, 0, 0);
            DateTime T2 = new DateTime(Y, M, D, 20, 0, 0);

            if (LogDT <= T1)
            {
                LogDT = T1;
            }
            else

                if (LogDT > T1 && LogDT <= T2)
                {
                    LogDT = T2;
                }
                else //LogDt > T2
                {
                    LogDT = T1.AddDays(1);
                }

            string LogFile = Local_LogPath + "\\" + LogDT.ToString("yyyyMMddHHmm") + ".log";

            string DT = DateTime.Now.ToString("yyyyMMddHHmmss");

            string s_Log = DT + (char)9 + S;
            try
            {
                FileStream F = new FileStream(LogFile, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter W = new StreamWriter(F);
                W.WriteLine(s_Log);
                W.Close();
                F.Close();
            }
            catch { throw; }
        }

        public static void AddLogSummary(string LogFileName)
        {
            if (!Directory.Exists(Local_LogPath)) return;

            string LogSummary = Local_LogPath + "\\" + DateTime.Now.ToString("yyyyMM") + ".log";

            #region Create Header
            if (!File.Exists(LogSummary))
            {
                //***Write Header
                try
                {
                    FileStream F = new FileStream(LogSummary, FileMode.Append, FileAccess.Write, FileShare.Write);
                    StreamWriter W = new StreamWriter(F);
                    string s_Log = "DateTime           " + (char)9 + "update" + (char)9 + "Cancel" + (char)9 + "Extra" + (char)9 + "Invalid" + (char)9 + "Error";
                    W.WriteLine(s_Log);
                    W.Close();
                    F.Close();
                }
                catch { throw; }
            }
            #endregion

            int i_update = 0;
            int i_Cancel = 0;
            int i_Extra = 0;
            int i_Invalid = 0;
            int i_Error = 0;

            #region Decode Log
            if (File.Exists(LogFileName))
            {
                List<string[]> la_data = new List<string[]>();
                char[] delimiters = new char[] { (char)9 };
                using (StreamReader reader = new StreamReader(LogFileName))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        string[] l = line.Split(delimiters);
                        la_data.Add(l);
                    }
                }

                for (int i = 0; i < la_data.Count; i++)
                {
                    if (la_data[i][1].StartsWith("update")) i_update++;
                    if (la_data[i][1].StartsWith("Cancel")) i_Cancel++;
                    if (la_data[i][1].StartsWith("Extra")) i_Extra++;
                    if (la_data[i][1].StartsWith("Invalid")) i_Invalid++;
                    if (la_data[i][1].StartsWith("Error")) i_Error++;
                }
            }
            #endregion

            #region Write File
            try
            {
                FileStream F = new FileStream(LogSummary, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter W = new StreamWriter(F);
                string s_Log = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + (char)9 +
                    i_Cancel.ToString() + (char)9 +
                    i_update.ToString() + (char)9 +
                    i_Extra.ToString() + (char)9 +
                    i_Invalid.ToString() + (char)9 +
                    i_Error.ToString();
                W.WriteLine(s_Log);
                W.Close();
                F.Close();
            }
            catch { throw; }
            #endregion
        }
        public static void AddLogSummaryByShift()
        {
            if (!Directory.Exists(Local_LogPath)) return;

            string LogSummary = Local_LogPath + "\\" + DateTime.Now.ToString("yyyyMM") + ".log";

            DateTime LastWrite = DateTime.Now.AddHours(-12);
            if (File.Exists(LogSummary))
            {
                LastWrite = File.GetLastWriteTime(LogSummary);
            }

            int Y = LastWrite.Year;
            int M = LastWrite.Month;
            int D = LastWrite.Day;
            int H = LastWrite.Hour;
            int m = LastWrite.Minute;
            int s = LastWrite.Second;
            int n = LastWrite.Millisecond;

            DateTime T1 = new DateTime(Y, M, D, 8, 0, 0);
            DateTime T2 = new DateTime(Y, M, D, 20, 0, 0);

            DateTime NextWriteTime = DateTime.Now;
            if (LastWrite <= T1)
            {
                NextWriteTime = T1;
            }
            else
                if (LastWrite > T1 && LastWrite <= T2)
                {
                    NextWriteTime = T2;
                }
                else //LastWrite > T2
                {
                    NextWriteTime = T1.AddDays(1);
                }

            if (DateTime.Now > NextWriteTime)
            {
                string LogToSummary = Local_LogPath + "\\" + NextWriteTime.ToString("yyyyMMddHHmm") + ".log";
                AddLogSummary(LogToSummary);
            }
        }

        /// <summary>
        /// Return the number of *.csv files. 
        /// Return -1 if current path is not valid. 
        /// </summary>
        /// <returns>Number of *.csv files</returns>
        public static int Count()
        {
            if (!Directory.Exists(DataFile_Path)) return -1;

            string[] files = Directory.GetFiles(DataFile_Path, "*.csv");

            return files.Count();
        }
        /// <summary>
        /// Clear DataFilePath. Copy all files to Month folder.
        /// </summary>
        /// <param name="FilesPurged">No of files purged</param>
        /// <returns></returns>
        public static bool PurgeDataFiles(ref int FilesPurged)
        {
            try
            {
                if (!Directory.Exists(Local_DataPath)) throw new Exception("Local_DataPath not found.");

                string[] files = Directory.GetFiles(Local_DataPath, "*.*");

                if (files.Count() == 0)
                {
                    //no data file
                    return true;
                }

                #region Create Month Path
                string YearMonth = DateTime.Now.Date.ToString("yyyyMM");
                string YearMonthPath = Local_DataPath + "\\" + YearMonth;
                if (!Directory.Exists(YearMonthPath))
                {
                    try
                    {
                        Directory.CreateDirectory(YearMonthPath);
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception("Create " + YearMonthPath + " error." + Ex.Message.ToString());
                    }
                }
                #endregion

                #region Purge Files
                FilesPurged = 0;
                foreach (string s in files)
                {
                    string Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_Purged" + Path.GetExtension(s);
                    int Index = 0;
                    while (File.Exists(Dest))
                    {
                        Index++;
                        Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_Purged(" + Index.ToString() + ")" + Path.GetExtension(s);
                    }
                    File.Move(s, Dest);
                    FilesPurged++;
                    AddToLog("Purged File. " + s + ". ");
                }
                #endregion

                return true;
            }
            catch (Exception Ex)
            {
                string S = "Error - Purge DataFiles. " + Ex.Message.ToString();
                AddToLog(S);
                throw new Exception(S);
            }
        }

        public static bool CopyCurrentDataFiles()
        {
            try
            {
                if (!Directory.Exists(Local_DataPath)) throw new Exception("Local_DataPath not found.");

                #region ClearLocalDataPath - Delete all files
                string[] local_files = Directory.GetFiles(Local_DataPath, "*.csv");
                foreach (string s in local_files)
                {
                    File.Delete(s);
                }
                #endregion

                if (!Directory.Exists(DataFile_Path)) throw new Exception("DataFilePath not found.");
                string[] files = Directory.GetFiles(DataFile_Path, "*.csv");

                if (files.Count() == 0)
                {
                    //no data file
                    return false;
                }


                DateTime dt_Last = File.GetLastWriteTime(DTFile);
                foreach (string s in files)
                {
                    if (s.Contains(EqID))
                    {
                        DateTime dt_Modified = File.GetLastWriteTime(s);

                        if (dt_Modified > dt_Last)
                        {
                            string Dest = Local_DataPath + "\\" + Path.GetFileName(s);
                            File.Copy(s, Dest);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return true;
        }

        static double SpecX = 0;
        static double SpecY = 0;
        static double MedianN1 = 0;
        static double MedianN2 = 0;
        static double TargetY = 0;
        static double Diff1 = 0;
        static double Diff2 = 0;
        static double LastOfst1 = 0;
        static double LastOfst2 = 0;
        static string LastTestFile = "";
        static DateTime LastupdateTime;

        /// <summary>
        /// Find and Decode Test File
        /// Files will be copied to Month folder.
        /// </summary>
        /// <param name="DBFilename"></param>
        /// <param name="MedianN1"></param>
        /// <param name="MedianN2"></param>
        /// <returns>True - file decoded. False - no file decoded.</returns>
        public static bool DecodeDataFile(ref double Ofst1, ref double Ofst2)
        {
            string s_LatestFile = "";
            try
            {
                CopyCurrentDataFiles();

                //if (!Directory.Exists(DataFile_Path)) throw new Exception("DataFilePath not found.");

                string[] files = Directory.GetFiles(Local_DataPath, "*.*");

                if (files.Count() == 0)
                {
                    //no data file
                    return false;
                }

                #region Create Month Path
                string YearMonth = DateTime.Now.Date.ToString("yyyyMM");
                string YearMonthPath = Local_DataPath + "\\" + YearMonth;
                if (!Directory.Exists(YearMonthPath))
                {
                    try
                    {
                        Directory.CreateDirectory(YearMonthPath);
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception("Create " + YearMonthPath + " error." + Ex.Message.ToString());
                    }
                }
                #endregion

                #region Filter Files and find latest file
                UInt64 i_LatestFileTag = 0;
                foreach (string s in files)
                {
                    //***invalid ext
                    if (Path.GetExtension(s) != ".csv")
                    {
                        string Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_InvalidExt" + Path.GetExtension(s);
                        int Index = 0;
                        while (File.Exists(Dest))
                        {
                            Index++;
                            Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_InvalidExt(" + Index.ToString() + ")" + Path.GetExtension(s);
                        }
                        File.Move(s, Dest);
                        AddToLog("Invalid FileExt. " + s);
                        continue;
                    }

                    if (!Path.GetFileName(s).StartsWith("PT"))
                    {
                        string Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_InvalidFileName" + Path.GetExtension(s);
                        int Index = 0;
                        while (File.Exists(Dest))
                        {
                            Index++;
                            Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_InvalidFileName(" + Index.ToString() + ")" + Path.GetExtension(s);
                        }
                        File.Move(s, Dest);
                        AddToLog("Invalid FileName. " + s);
                        continue;
                    }

                    //PT-001-  RuN-647282221-00-GC-044-LOGO98101-20160411123443
                    //<TestID>-RuN-<RunID-00>-  <EqID>-<OpId>-   <FileTag>
                    string FileName = Path.GetFileNameWithoutExtension(s);
                    string[] s_line = FileName.Split(new char[] { '-' });
                    string FileTag = s_line[s_line.Count() - 1];

                    try
                    {
                        UInt64 i_FileTag = Convert.ToUInt64(FileTag);
                        if (i_FileTag > i_LatestFileTag)
                        {
                            i_LatestFileTag = i_FileTag;
                            s_LatestFile = s;
                        }
                    }
                    catch
                    {
                        throw new Exception("Decode File Tag Error. ");
                    }
                }
                #endregion

                #region Purge Extra Files
                files = Directory.GetFiles(Local_DataPath, "*.*");
                foreach (string s in files)
                {
                    if (s != s_LatestFile)
                    {
                        string Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_Extra" + Path.GetExtension(s);
                        int Index = 0;
                        while (File.Exists(Dest))
                        {
                            Index++;
                            Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s) + "_Extra(" + Index.ToString() + ")" + Path.GetExtension(s);
                        }
                        File.Move(s, Dest);
                        AddToLog("Extra File. " + s);
                    }
                }
                #endregion

                List<string> l_Line = new List<string>();
                string DBFilename = "";

                #region Decode File
                try
                {
                    AddToLog("Decoding DataFile " + s_LatestFile);
                    //***Decode File
                    //???q?W??:	???????~???????????q									
                    //?????W??:	CG3WW3001-BA12-059B									
                    //?????W??:	PT-001-RuN-647282246-00-GC-046-LOGO98101-20160411123139.csv									
                    //FrontTestBox:	0.287	0.2787	0.293	0.2887	0.298	0.2777	0.293	0.2697		
                    //ChangBlockBox:	0.2852	0.277	0.2948	0.2924	0.2998	0.2814	0.2912	0.266		
                    //?e??????:	2016/4/11 ?u?? 12:31:40									
                    //????:	uF1	    IR	    LOP1	WLP1	WLD1	X1	    Y1	    ST1	    INT1    NeedleNo
                    //1_1	2.809	0	    610	    447.8	471.7	0.2943	0.2855	100	    3728	1
                    //1_2	7.999	0	    3.852	448.1	471.2	0.2921	0.2818	100	    3831	1
                    //1_3	8.001	0.001	1.552	447.5	469.8	0.2907	0.2781	100	    3865	1
                    //1_4	7.999	0	    0.455	446.3	467.2	0.2864	0.2685	100	    3858	2
                    //1_5	7.999	0.022	1.796	446.1	466.9	0.2858	0.2672	98	    3846	2
                    //1_6	7.999	0	    0.823	447.3	470.7	0.2932	0.2826	100	    3851	2
                    //..
                    //..
                    //1_4   7.999	0	    0.369	448.5	470.1	0.2909	0.2786	100	    3854	1
                    //1_48	7.999	0	    1.001	448.6	470.6	0.2915	0.2801	100	    3898	1
                    //?????p??????:	2.821	0.001	0.084	448.3	466.2	0.275	0.2506	100	3854	
                    //?????j??????:	8.001	0.002	630	449	471.9	0.2958	0.2874	100	3907	
                    //???d??????:	5.18	0.001	629.916	0.7	5.7	0.021	0.037	0	53	
                    //??????????????????:	7.853	0.001	13.437	448.66	469.93	0.2896	0.2766	100	3880.8	
                    //???????t:	0.7893	0.0002	90.8879	0.1807	1.3212	0.0041	0.0075	0	11.4619	
                    //???e???????????}???v:	50.00%									
                    //?????????????}???v:	70.83%									
                    //R^2???}???v:	0.9969									
                    //TOTAL:	NG									

                    List<string[]> la_data = new List<string[]>();//data[row][col]
                    char[] delimiters = new char[] { ',' };
                    using (StreamReader reader = new StreamReader(s_LatestFile))
                    {
                        while (true)
                        {
                            string line = reader.ReadLine();
                            if (line == null)
                            {
                                break;
                            }
                            l_Line.Add(line);
                            string[] l = line.Split(delimiters);
                            la_data.Add(l);
                        }
                    }

                    DBFilename = la_data[1][1];
                    List<double> Y = new List<double>();
                    int HeaderLine = -1;
                    int FirstDataLine = -1;
                    for (int i = 0; i < la_data.Count(); i++)
                    {
                        //FrontTestBox
                        if (la_data[i][0].Contains("FrontTestBox"))
                        {
                            List<double> l_Spec_X = new List<double>();
                            List<double> l_Spec_Y = new List<double>();

                            if (la_data[i].Count() < 9) throw new Exception("Invalid Spec. ");

                            for (int j = 1; j < 9; j++)
                            {
                                double d = 0;
                                try
                                {
                                    d = Convert.ToDouble(la_data[i][j]);
                                }
                                catch
                                {
                                    throw new Exception("Convert Spec. ");
                                }

                                if (j % 2 == 1)
                                    l_Spec_X.Add(d);
                                else
                                    l_Spec_Y.Add(d);
                            }
                            SpecX = Math.Round(l_Spec_X.Average(), 4);
                            SpecY = Math.Round(l_Spec_Y.Average(), 4);
                        }

                        //Header Line
                        if (la_data[i][1].Contains("uF"))
                        {
                            HeaderLine = i;
                        }

                        if (FirstDataLine < 0 && la_data[i][0].StartsWith("1_"))
                        {
                            FirstDataLine = i;
                        }

                        if (FirstDataLine > 0)
                        {
                            if (!la_data[i][0].StartsWith("1_")) break;

                            double d_Y = 0;
                            string s_Y = la_data[i][7];
                            try
                            {
                                d_Y = Convert.ToDouble(s_Y);
                            }
                            catch
                            {
                                //throw new Exception("Convert Data Err. ");
                            }
                            Y.Add(d_Y);
                        }
                    }

                    if (Y.Count % 2 != 0) throw new Exception("Row Count Err. ");

                    //if ((Y.Count / 2) % 8 != 0) throw new Exception("Col Count Err. ");

                    int ColCount = Y.Count / 2;

                    l_Line[HeaderLine] = l_Line[HeaderLine] + "," + "NSW Needle No";

                    List<double> N1 = new List<double>();
                    List<double> N2 = new List<double>();
                    for (int i = 0; i < Y.Count; i++)
                    {
                        if ((i < ColCount / 2) || (i >= ColCount * 3 / 2))
                        {
                            l_Line[FirstDataLine + i] = l_Line[FirstDataLine + i] + "," + 2;
                            N2.Add(Y[i]);
                        }
                        else
                        {
                            l_Line[FirstDataLine + i] = l_Line[FirstDataLine + i] + "," + 1;
                            N1.Add(Y[i]);
                        }
                    }

                    NSW.Net.Stats Stat = new NSW.Net.Stats();

                    MedianN1 = Stat.Median(N1);
                    MedianN2 = Stat.Median(N2);

                    AddToLog("DataFile Info " + "CompFile: " + DBFilename + " " +
                        "SpecXY: " + SpecX.ToString("f4") + "," + SpecY.ToString("f4") + " " +
                        "Median N1 N2: " + MedianN1.ToString("f4") + "," + MedianN2.ToString("f4"));
                }
                catch (Exception Ex)
                {
                    string Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + "_DecodeErr" + Path.GetExtension(s_LatestFile);
                    int Index = 0;
                    while (File.Exists(Dest))
                    {
                        Index++;
                        Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + "_DecodeErr(" + Index.ToString() + ")" + Path.GetExtension(s_LatestFile);
                    }
                    File.Move(s_LatestFile, Dest);
                    string S = "Error File. " + s_LatestFile + ". " + Ex.Message.ToString();
                    AddToLog(S);
                    throw;
                }
                #endregion

                try
                {
                    #region Decode CompFile and Calculate Ofst
                    List<double[]> Lookup = new List<double[]>();
                    DecodeCompFile(DBFilename, ref TargetY, ref Lookup);

                    AddToLog("Decoding CompFile " + CompFile_Path + "\\" + DBFilename + ".csv");

                    if (TargetY < SpecY - 0.0005 || TargetY > SpecY + 0.0005)
                        throw new Exception("Spec not in range. SpecY " + SpecY.ToString("f4") + " TargetY " + TargetY.ToString("f4"));

                    AddToLog("CompFile Info " + "TargetY: " + TargetY.ToString("f4") + " TableRow: " + Lookup.Count.ToString());

                    Diff1 = MedianN1 - TargetY;
                    Diff2 = MedianN2 - TargetY;

                    int Lookup_Zero = 0;
                    for (int i = 0; i < Lookup.Count + 1; i++)
                    {
                        if (i == Lookup.Count)
                            throw new Exception("Lookup Zero not found. Check Compensation File.");

                        if (Lookup[i][0] == 0)
                        {
                            Lookup_Zero = i;
                            break;
                        }
                    }

                    if (Diff1 == 0)
                    {
                        Ofst1 = Lookup[Lookup_Zero][1];
                    }
                    else
                        if (Diff1 > 0)
                            for (int i = Lookup_Zero - 1; i >= 0; i--)
                            {
                                Ofst1 = Lookup[i][1];
                                if (Diff1 <= Lookup[i][0]) break;
                            }
                        else
                            for (int i = Lookup_Zero + 1; i < Lookup.Count; i++)
                            {
                                Ofst1 = Lookup[i][1];
                                if (Diff1 >= Lookup[i][0]) break;
                            }

                    if (Diff2 == 0)
                    {
                        Ofst2 = Lookup[Lookup_Zero][1];
                    }
                    if (Diff2 > 0)
                        for (int i = Lookup_Zero - 1; i >= 0; i--)
                        {
                            Ofst2 = Lookup[i][1];
                            if (Diff2 <= Lookup[i][0]) break;
                        }
                    else
                        for (int i = Lookup_Zero + 1; i < Lookup.Count; i++)
                        {
                            Ofst2 = Lookup[i][1];
                            if (Diff2 >= Lookup[i][0]) break;
                        }


                    //for (int i = 0; i < Lookup.Count; i++)
                    //{
                    //    Ofst2 = Lookup[i][1];
                    //    if (Diff2 > Lookup[i][0]) break;
                    //}

                    AddToLog("Comp Info " + "OffsetY N1 N2: " + Diff1.ToString("f4") + "," + Diff2.ToString("f4") + " " +
                        "uol Offset N1 N2: " + Ofst1.ToString("F4") + "," + Ofst2.ToString("F4"));
                    #endregion

                    frm_AOT_TestCloseLoop frm = new frm_AOT_TestCloseLoop();
                    frm.Type = frm_AOT_TestCloseLoop.EType.Prompt;

                    frm.TestFile = Path.GetFileName(s_LatestFile);
                    frm.Time = DateTime.Now;
                    frm.M1 = MedianN1;
                    frm.M2 = MedianN2;
                    frm.T1 = TargetY;
                    frm.T2 = TargetY;
                    frm.D1 = Diff1;
                    frm.D2 = Diff2;
                    frm.O1 = Ofst1;
                    frm.O2 = Ofst2;

                    DialogResult dr = frm.ShowDialog();

                    string Dest = "Dest";
                    if (dr == DialogResult.Yes)
                    {
                        Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + Path.GetExtension(s_LatestFile);
                        int Index = 0;
                        while (File.Exists(Dest))
                        {
                            Index++;
                            Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + "(" + Index.ToString() + ")" + Path.GetExtension(s_LatestFile);
                        }

                        AddToLog("update File. " + s_LatestFile);

                        LastupdateTime = DateTime.Now;
                    }
                    else
                    {
                        Ofst1 = 0;
                        Ofst2 = 0;

                        Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + "_Cancel" + Path.GetExtension(s_LatestFile);
                        int Index = 0;
                        while (File.Exists(Dest))
                        {
                            Index++;
                            Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + "_Cancel" + "(" + Index.ToString() + ")" + Path.GetExtension(s_LatestFile);
                        }

                        AddToLog("Cancel File. " + s_LatestFile);
                    }

                    LastTestFile = Path.GetFileName(s_LatestFile);
                    LastOfst1 = Ofst1;
                    LastOfst2 = Ofst2;

                    using (StreamWriter writer = new StreamWriter(Dest))
                    {
                        foreach (string l in l_Line)
                        {
                            writer.WriteLine(l);
                        }
                    }
                    File.Delete(s_LatestFile);

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(DTFile))
                        {
                            writer.WriteLine(DateTime.Now.ToString());
                        }
                    }
                    catch { };
                }
                catch
                {
                    string Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + "_DecodeErr" + Path.GetExtension(s_LatestFile);
                    int Index = 0;
                    while (File.Exists(Dest))
                    {
                        Index++;
                        Dest = YearMonthPath + "\\" + Path.GetFileNameWithoutExtension(s_LatestFile) + "_DecodeErr(" + Index.ToString() + ")" + Path.GetExtension(s_LatestFile);
                    }
                    File.Move(s_LatestFile, Dest);

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(DTFile))
                        {
                            writer.WriteLine(DateTime.Now.ToString());
                        }
                    }
                    catch { };

                    throw;
                }

                return true;
            }
            catch (Exception Ex)
            {
                string S = "Error - Decode DataFile. " + s_LatestFile + ". " + Ex.Message.ToString();
                AddToLog(S);
                throw new Exception(S);
            }
        }
        /// <summary>
        /// Decode Compensation File
        /// </summary>
        /// <param name="CompFileName">Full Filename</param>
        /// <param name="TargetY">Target Y</param>
        /// <param name="Lookup">Lookup Table</param>
        /// <returns>Success</returns>
        public static bool DecodeCompFile(string CompFilename, ref double TargetY, ref List<double[]> Lookup)
        {
            string CompFileName = CompFilePath + "\\" + CompFilename + ".csv";

            try
            {
                if (!Directory.Exists(CompFilePath)) throw new Exception("CompFilePath not found.");

                if (!File.Exists(CompFileName)) throw new Exception("CompFile not found.");

                #region Decode File
                //0.2789,
                //??????????,????????????(??l)
                //0.02,0.018
                //0.019,0.018
                //0.018,0.018
                //0.017,0.015
                //..
                //0,0
                //-0.001,0
                //..
                //-0.02,-0.018

                List<string[]> la_Line = new List<string[]>();
                char[] delimiters = new char[] { ',' };
                using (StreamReader reader = new StreamReader(CompFileName))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        //l_Line.Add(line);
                        string[] l = line.Split(delimiters);
                        la_Line.Add(l);
                    }
                }

                //List<double[]> la_Table = new List<double[]>();

                try
                {
                    double Target = Convert.ToDouble(la_Line[0][0]);
                    TargetY = Target;
                }
                catch { throw new Exception("Convert TargetY Ex Error."); }

                for (int i = 2; i < la_Line.Count(); i++)
                {
                    double OfstY = 0;
                    try
                    {
                        OfstY = Convert.ToDouble(la_Line[i][0]);
                    }
                    catch { throw new Exception("Convert OfstY Ex Error."); }
                    double Adjuol = 0;
                    try
                    {
                        Adjuol = Convert.ToDouble(la_Line[i][1]);
                    }
                    catch { throw new Exception("Convert uolume Adjust Ex Error."); }

                    Lookup.Add(new double[] { OfstY, Adjuol });
                }
                #endregion

                //AddToLog("Decoded CompFile. " + CompFileName);
            }
            catch (Exception Ex)
            {
                string S = "Error - Decode Comp File. " + CompFileName + ". " + Ex.Message.ToString();
                AddToLog(S);
                throw new Exception(S);
            }

            return true;
        }

        public static bool ShowInfo()
        {
            frm_AOT_TestCloseLoop frm = new frm_AOT_TestCloseLoop();
            frm.Type = frm_AOT_TestCloseLoop.EType.Info;

            frm.TestFile = LastTestFile;
            frm.Time = LastupdateTime;
            frm.M1 = MedianN1;
            frm.M2 = MedianN2;
            frm.T1 = TargetY;
            frm.T2 = TargetY;
            frm.D1 = Diff1;
            frm.D2 = Diff2;
            frm.O1 = LastOfst1;
            frm.O2 = LastOfst2;

            frm.ShowDialog();

            return true;
        }
    }
  
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
}
