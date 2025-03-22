using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace NDispWin
{
    internal partial class frm_DispCore_DispSetup_Custom : Form
    {
        public frm_DispCore_DispSetup_Custom()
        {
            InitializeComponent();
            GControl.LogForm(this);
        }

        private void frm_DispCore_DispSetup_HeadCal_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);
        }

        private void frm_DispCore_DispSetup_HeadCal_VisibleChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            btn_EditVolumeOfst.Visible = TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC;

            lbl_VolumeOfstProtocol.Text = ((int)TaskDisp.VolumeOfst_Protocol).ToString() + " : " + TaskDisp.VolumeOfst_Protocol.ToString();

            pnlOsramICC.Visible = TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_ICC;
            tbxOsramICCInputPath.Text = TaskDisp.OsramICC_InputPath;
            tbxOsramICCOutputPath.Text = TaskDisp.OsramICC_OutputPath;

            lbl_InputMap_Protocol.Text = ((int)TaskDisp.InputMap_Protocol).ToString() + " : " + TaskDisp.InputMap_Protocol.ToString();
            btnInputMapSetup.Visible = TaskDisp.InputMap_Protocol == TaskDisp.EInputMapProtocol.OSRAM_eMos;
            pnl_InputMapPaths_Lmd_EMap.Visible = TaskDisp.InputMap_Protocol == TaskDisp.EInputMapProtocol.Lumileds_EMap;
            lbl_InputMap_DataPath.Text = Task_InputMap.Lumileds_SS_EMap.MapPath[0];
            tbox_Prefix.Text = Task_InputMap.Lumileds_SS_EMap.FilenamePrefix[0];
            tbox_Suffix.Text = Task_InputMap.Lumileds_SS_EMap.FilenameSuffix[0];
            lbl_InputMap_DataPath2.Text = Task_InputMap.Lumileds_SS_EMap.MapPath[1];
            tbox_Prefix2.Text = Task_InputMap.Lumileds_SS_EMap.FilenamePrefix[1];
            tbox_Suffix2.Text = Task_InputMap.Lumileds_SS_EMap.FilenameSuffix[1];
            lbl_InputMap_DataPath3.Text = Task_InputMap.Lumileds_SS_EMap.MapPath[2];
            tbox_Prefix3.Text = Task_InputMap.Lumileds_SS_EMap.FilenamePrefix[2];
            tbox_Suffix3.Text = Task_InputMap.Lumileds_SS_EMap.FilenameSuffix[2];
        }

        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            UpdateDisplay();
        }

        private void lbl_Protocol_Click(object sender, EventArgs e)
        {
            TaskDisp.EVolumeOfstProtocol E = TaskDisp.EVolumeOfstProtocol.None;
            int i = (int)TaskDisp.VolumeOfst_Protocol;
            UC.AdjustExec("DispSetup, VolumeOfst Protocol", ref i, E);
            TaskDisp.VolumeOfst_Protocol = (TaskDisp.EVolumeOfstProtocol)i;

            if (TaskDisp.VolumeOfst_Protocol == TaskDisp.EVolumeOfstProtocol.OSRAM_SCC)
            {
                TaskDisp.OsramSCC.LoadSetup();
            }
            UpdateDisplay();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            switch (TaskDisp.VolumeOfst_Protocol)
            {
                case TaskDisp.EVolumeOfstProtocol.OSRAM_SCC:
                    TaskDisp.OsramSCC.ShowWindow();
                    break;
                default:
                    MessageBox.Show("No Setup Available.");
                    break;
            }
        }

        private void lbl_InputMap_Protocol_Click(object sender, EventArgs e)
        {
            int i_Max = Enum.GetNames(typeof(TaskDisp.EInputMapProtocol)).Length;

            TaskDisp.EInputMapProtocol E = TaskDisp.EInputMapProtocol.None;
            int i = (int)TaskDisp.InputMap_Protocol;
            UC.AdjustExec("DispSetup, Input Map Protocol", ref i, E);
            TaskDisp.InputMap_Protocol = (TaskDisp.EInputMapProtocol)i;

            if (TaskDisp.InputMap_Protocol == TaskDisp.EInputMapProtocol.OSRAM_eMos)
            {
                Task_InputMap.OsramEMos.LoadSetup();
            }
            UpdateDisplay();
        }
        private void lbl_InputMap_DataPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Task_InputMap.Lumileds_SS_EMap.MapPath[0];
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                Task_InputMap.Lumileds_SS_EMap.MapPath[0] = fbd.SelectedPath;
                if (!Task_InputMap.Lumileds_SS_EMap.MapPath[0].EndsWith(@"\"))
                    Task_InputMap.Lumileds_SS_EMap.MapPath[0] = Task_InputMap.Lumileds_SS_EMap.MapPath[0] + @"\";
            }

            UpdateDisplay();
        }
        private void lbl_InputMap_DataPath2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Task_InputMap.Lumileds_SS_EMap.MapPath[1];
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                Task_InputMap.Lumileds_SS_EMap.MapPath[1] = fbd.SelectedPath;
                if (!Task_InputMap.Lumileds_SS_EMap.MapPath[1].EndsWith(@"\"))
                    Task_InputMap.Lumileds_SS_EMap.MapPath[1] = Task_InputMap.Lumileds_SS_EMap.MapPath[1] + @"\";
            }

            UpdateDisplay();
        }
        private void lbl_InputMap_DataPath3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Task_InputMap.Lumileds_SS_EMap.MapPath[2];
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                Task_InputMap.Lumileds_SS_EMap.MapPath[2] = fbd.SelectedPath;
                if (!Task_InputMap.Lumileds_SS_EMap.MapPath[2].EndsWith(@"\"))
                    Task_InputMap.Lumileds_SS_EMap.MapPath[2] = Task_InputMap.Lumileds_SS_EMap.MapPath[2] + @"\";
            }

            UpdateDisplay();
        }

        private void btn_InputMap_CheckDataPath_Click(object sender, EventArgs e)
        {
            lbl_InputMap_DataPath.BackColor = Directory.Exists(Task_InputMap.Lumileds_SS_EMap.MapPath[0]) ? Color.Lime : Color.Red;

            if (Task_InputMap.Lumileds_SS_EMap.MapPath[1].Length > 0)
                lbl_InputMap_DataPath2.BackColor = Directory.Exists(Task_InputMap.Lumileds_SS_EMap.MapPath[1]) ? Color.Lime : Color.Red;

            if (Task_InputMap.Lumileds_SS_EMap.MapPath[2].Length > 0)
                lbl_InputMap_DataPath3.BackColor = Directory.Exists(Task_InputMap.Lumileds_SS_EMap.MapPath[2]) ? Color.Lime : Color.Red;

            System.Threading.Thread.Sleep(1000);

            lbl_InputMap_DataPath.BackColor = Color.White;
            lbl_InputMap_DataPath2.BackColor = Color.White;
            lbl_InputMap_DataPath3.BackColor = Color.White;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (!Task_InputMap.Lumileds_SS_EMap.MapPath[0].EndsWith(@"\"))
                Task_InputMap.Lumileds_SS_EMap.MapPath[0] = Task_InputMap.Lumileds_SS_EMap.MapPath[0] + @"\";
            Task_InputMap.Lumileds_SS_EMap.FilenamePrefix[0] = tbox_Prefix.Text;
            Task_InputMap.Lumileds_SS_EMap.FilenameSuffix[0] = tbox_Suffix.Text;

            if (Task_InputMap.Lumileds_SS_EMap.MapPath[1].Length > 0)
            {
                if (!Task_InputMap.Lumileds_SS_EMap.MapPath[1].EndsWith(@"\"))
                    Task_InputMap.Lumileds_SS_EMap.MapPath[1] = Task_InputMap.Lumileds_SS_EMap.MapPath[1] + @"\";
                Task_InputMap.Lumileds_SS_EMap.FilenamePrefix[1] = tbox_Prefix2.Text;
                Task_InputMap.Lumileds_SS_EMap.FilenameSuffix[1] = tbox_Suffix2.Text;
            }

            if (Task_InputMap.Lumileds_SS_EMap.MapPath[2].Length > 0)
            {
                if (!Task_InputMap.Lumileds_SS_EMap.MapPath[2].EndsWith(@"\"))
                    Task_InputMap.Lumileds_SS_EMap.MapPath[2] = Task_InputMap.Lumileds_SS_EMap.MapPath[2] + @"\";
                Task_InputMap.Lumileds_SS_EMap.FilenamePrefix[2] = tbox_Prefix3.Text;
                Task_InputMap.Lumileds_SS_EMap.FilenameSuffix[2] = tbox_Suffix3.Text;
            }

            UpdateDisplay();
        }

        private void btnInputMapSetup_Click(object sender, EventArgs e)
        {
            if (TaskDisp.InputMap_Protocol == TaskDisp.EInputMapProtocol.OSRAM_eMos)
            {
                frm_Osram_eMos_Setup frm = new frm_Osram_eMos_Setup();
                frm.ShowDialog();
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                Task_InputMap.Lumileds_SS_EMap.MapPath[i] = "";
                Task_InputMap.Lumileds_SS_EMap.FilenamePrefix[i] = "";
                Task_InputMap.Lumileds_SS_EMap.FilenameSuffix[i] = "";
            }
            UpdateDisplay();
        }

        private void btnCheckOsramICCPath_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Color inputColor = Directory.Exists(TaskDisp.OsramICC_InputPath) ? Color.Lime : Color.Red;
                Color outputColor = Directory.Exists(TaskDisp.OsramICC_OutputPath) ? Color.Lime : Color.Red;

                tbxOsramICCInputPath.Invoke(new Action(() =>
                {
                    tbxOsramICCInputPath.BackColor = inputColor;
                }));

                tbxOsramICCOutputPath.Invoke(new Action(() =>
                {
                    tbxOsramICCOutputPath.BackColor = outputColor;
                }));

                System.Threading.Thread.Sleep(500);

                tbxOsramICCInputPath.Invoke(new Action(() =>
                {
                    tbxOsramICCInputPath.BackColor = Color.White;
                }));

                tbxOsramICCOutputPath.Invoke(new Action(() =>
                {
                    tbxOsramICCOutputPath.BackColor = Color.White;
                }));
            });
        }

        private void btnInputFileLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = TaskDisp.OsramICC_InputPath,
                Title = "Select a Input File",
                Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;

                double d = 0;
                OsramICC.ReadInputFile(fileName, ref d);

                MessageBox.Show($"Input File InitialDispenserSetting: {d:f4}.");
            }
        }

        private void btnOutputFileLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = TaskDisp.OsramICC_OutputPath,
                Title = "Select a Output File",
                Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;

                if (OsramICC.ReadOutputFile(fileName))
                   MessageBox.Show($"Output File load success.");
                else
                    MessageBox.Show($"Output File load fail.");
            }
        }
        private void btnOutputFileTileLookUp_Click(object sender, EventArgs e)
        {
            double[] d = new double[2] { 0, 0 };

            if (OsramICC.PanelLookup(tbxLookUpTileID.Text, ref d))
            {
                MessageBox.Show($"{tbxLookUpTileID.Text},{d[0]:f3},{d[1]:f4}");
            }
            else
            {
                MessageBox.Show($"{tbxLookUpTileID.Text} lookup fail.");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OsramICC.Pass1.ReadFile();
            OsramICC.Pass2.ReadFile();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            OsramICC.Pass1.WriteFile();
            OsramICC.Pass2.WriteFile();
        }
        private void btnEditPass1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "notepad.exe",
                    Arguments = OsramICC.Pass1PanelListFile,
                    Verb = "runas", // this is the key to run as admin
                    UseShellExecute = false
                };

                try
                {
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open file as admin: " + ex.Message);
                }
            }
            catch { MessageBox.Show("Pass 1 PanelList File not found."); }
        }
        private void btnEditPass2_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "notepad.exe",
                    Arguments = OsramICC.Pass2PanelListFile,
                    Verb = "runas", // this is the key to run as admin
                    UseShellExecute = false
                };

                try
                {
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open file as admin: " + ex.Message);
                }
            }
            catch { MessageBox.Show("Pass 2 PanelList File not found."); }
        }

        private void btnOsramICCTest_Click(object sender, EventArgs e)
        {
            double[] d = new double[2] { 0, 0 };
            if (OsramICC.Execute(tbxLookUpTileID.Text, LotInfo2.LotNumber, LotInfo2.Osram.ElevenSeries, LotInfo2.Osram.DAStartNumber, ref d))
            {
                MessageBox.Show($"{tbxLookUpTileID.Text},{d[0]:f3},{d[1]:f4}");
            }
            else
            {
                MessageBox.Show($"{tbxLookUpTileID.Text} test fail.");
            }
        }
    }
}
