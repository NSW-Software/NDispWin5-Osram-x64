using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace NDispWin
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
            GControl.LogForm(this);

            List<string> LangList = AppLanguage.Func2.GetLangList();
            
            cbxLanguage.Items.Clear();
            cbxLanguage2.Items.Clear();
            for (int i = 0; i < LangList.Count(); i++)
            {
                cbxLanguage.Items.Add(LangList[i]);
                cbxLanguage2.Items.Add(LangList[i]);
            }
            cbxLanguage.SelectedIndex = GDefineN.Language1;
            cbxLanguage2.SelectedIndex = GDefineN.Language2;

            cbxCustomerList.DataSource = Enum.GetNames(typeof(LotInfo2.ECustomer));
                try { cbxCustomerList.SelectedIndex = (int)LotInfo2.Customer; } catch { };

            groupBox6.Visible = NUtils.UserAcc.Active.GroupID >= (int)ELevel.Admin;

            UpdateDisplay();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            this.Text = "Options";
            StartPosition = FormStartPosition.CenterScreen;
            UpdateDisplay();

            if (NUtils.UserAcc.Active.GroupID < (int)ELevel.Admin)
                tcOptions.TabPages.Remove(tpAdvance);
        }

        private void UpdateDisplay()
        {
            tboxCustomPath.Text = TaskDisp.CustomPath;

            #region Process
            cbEnableMaterialLow.Checked = TaskDisp.Option_EnableMaterialLow;
            cbEnableDualMaterial.Checked = TaskDisp.Option_EnableDualMaterial;
            cbMaterialLowForbidContinue.Checked = TaskDisp.MaterialLowForbidContinue;

            cbEnableMaterialExpiry.Checked = TaskDisp.Material_EnableTimer;
            lblMaterialLifeTimeMultipler.Text = TaskDisp.Material_Life_Multiplier.ToString();
            lblMaterialExpiryPreAlertTime.Text = TaskDisp.Material_ExpiryPreAlertTime.ToString();
            cbEnableValveExpiry.Checked = TaskDisp.Valve_EnableTimer;
            cbMaterialExpiryForbidContinue.Checked = TaskDisp.MaterialExpiryForbidContinue;

            cbEnableMaterialCounter.Checked = Material.EnableUnitCounter;
            lblMaterialUnitCounterALimit.Text = UI_Utils.GetKK(Material.Unit.Count[0]) + " / " + UI_Utils.GetKK(Material.Unit.Limit[0]);
            lblMaterialUnitCounterBLimit.Text = UI_Utils.GetKK(Material.Unit.Count[1]) + " / " + UI_Utils.GetKK(Material.Unit.Limit[1]);
            cbEnableMaterialPanelCounter.Checked = Material.EnablePanelCounter;
            lblMaterialPanelCounterLimit.Text = UI_Utils.GetKK(Material.Panel.Count) + " / " + UI_Utils.GetKK(Material.Panel.Limit);

            lblDensityRange.Text = $"{TaskDisp.Option_DensityRange:f3}";
            cbEnableNeedleShort.Checked = TaskDisp.Option_EnableNeedleShort;
            #endregion

            lblDefZPos.Text = TaskDisp.ZDefPos.ToString("f3");
            lblDefLaserValue.Text = TaskDisp.Laser_CalValue.ToString("f3");

            lblCustomerPreference.Text = TaskDisp.Preference.ToString();
            cbEnableLotEntry.Checked = GDefine.EnableLotEntry;

            cbEnableStartButton.Checked = GDefineN.Enabled_BtnStart;
            cbEnableStopButton.Checked = GDefineN.Enabled_BtnStop;
            cbEnableResetButton.Checked = GDefineN.Enabled_BtnReset;

            cbEnableLowPressure.Checked = GDefineN.Enabled_LowPressure;
            cbEnableBuzzer.Checked = GDefineN.Enable_Buzzer;

            cbEnableDoorSensor.Checked = GDefineN.EnableDoorSens;
            cbEnableDoorLock.Checked = GDefineN.EnableDoorLock;
            lblDTEnable.Text = DateTime.Now < DefineSafety.dtEnable ? "Disabled Until: " + DefineSafety.dtEnable.ToString("yyyy-MM-dd HH:mm:ss"): "-";

            if (TaskDisp.Preference == TaskDisp.EPreference.Lumileds)
            {
                TaskConv.EnableDoorSens = true;
            }

            cbEnableDoorSensor.Enabled = TaskDisp.Preference != TaskDisp.EPreference.Lumileds;
            cboxEnableMapEditLock.Checked = GDefineN.EnableMapEditLock;
            cbAutoPageShowImage.Checked = GDefineN.AutoPageShowImage;

            lblSECSGEMProtocol.Text = TaskDisp.SECSGEMProtocol.ToString();

            lblUseRecipeFile.Text = TaskDisp.EnableRecipeFile.ToString();
            rbLoadLastDevice.Checked = !TaskDisp.LoadLastRecipe;
            rbLoadLastRecipe.Checked = TaskDisp.LoadLastRecipe;

            cbEnableEventDebugLog.Checked = GDefineN.EnableEventDebugLog;
        }

        private void cbox_CustomerList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LotInfo2.Customer = (LotInfo2.ECustomer)cbxCustomerList.SelectedIndex;
        }
        private void btnEditDataTable_Click(object sender, EventArgs e)
        {
            //LotCtrl.frmData fData = new LotCtrl.frmData();
            //fData.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {

            GDefineN.Language1 = cbxLanguage.SelectedIndex;
            GDefineN.Language2 = cbxLanguage2.SelectedIndex;

            GDefine.EnableLotEntry = cbEnableLotEntry.Checked;
            GDefineN.Enabled_BtnStart = cbEnableStartButton.Checked;

            GDefineN.Enabled_BtnStop = cbEnableStopButton.Checked;
            GDefineN.Enabled_BtnReset = cbEnableResetButton.Checked;

            GDefineN.Enabled_LowPressure = cbEnableLowPressure.Checked;
            GDefineN.Enable_Buzzer = cbEnableBuzzer.Checked;

            GDefineN.EnableDoorSens = cbEnableDoorSensor.Checked;
            GDefineN.EnableDoorLock = cbEnableDoorLock.Checked;

            GDefineN.EnableMapEditLock = cboxEnableMapEditLock.Checked;
            GDefineN.AutoPageShowImage = cbAutoPageShowImage.Checked;

            bool.TryParse(lblUseRecipeFile.Text, out TaskDisp.EnableRecipeFile);

            Close();
        }

        private void btnAccessConfig_Click(object sender, EventArgs e)
        {
            NUtils.UserAcc.Users.ConfigDlg();
        }

        private void lblCustomerPreference_Click(object sender, EventArgs e)
        {
            int i = (int)TaskDisp.Preference;
            if (!UC.AdjustExec("Options, Customer Name", ref i, TaskDisp.EPreference.None)) return;
            lblCustomerPreference.Text = ((TaskDisp.EPreference)i).ToString();
            Enum.TryParse(lblCustomerPreference.Text, out TaskDisp.Preference);

            UpdateDisplay();
        }

        private void lblEnableRecipeFile_Click(object sender, EventArgs e)
        {
            bool b = (bool)TaskDisp.EnableRecipeFile;
            if (!UC.AdjustExec("Options, Enable Recipe File", ref b)) return;
            TaskDisp.EnableRecipeFile = b;
            lblUseRecipeFile.Text = b.ToString();

            UpdateDisplay();
        }

        private void btnSECSGEMConnect2_Click(object sender, EventArgs e)
        {
            if (TaskDisp.SECSGEMProtocol == TaskDisp.ESECSGEMProtocol.SECSGEMConnect2)
            {
                frmSECSGEMConnect2 frm = new frmSECSGEMConnect2();
                frm.sgc2 = GDefine.sgc2;
                frm.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TaskDisp.SaveSetup();
        }

        private async void btnCalDefZPos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Calibrate Default Z Position?", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (MessageBox.Show("Please move camera at Calibrate Height Position. Press OK to Continue.", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            frm_ProgressReport frmPR = new frm_ProgressReport();
            try
            {
                this.Enabled = false;

                frmPR = new frm_ProgressReport();
                frmPR.Message = "Def Z Calibration in Progress. Please wait...";
                frmPR.Show();

                double z = 5;
                await Task.Run(() =>
                {
                    if (!TaskDisp.TaskAutoFocus()) return;

                    z = TaskGantry.GZPos();
                });
                frmPR.Close();//Done = true;

                if (MessageBox.Show("Update DefZPos from " + TaskDisp.ZDefPos.ToString("f3") + " to " + z.ToString("f3"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TaskDisp.ZDefPos = z;
                    Event.CAL_DEFZPOS_UPDATE.Set("Default Z Position", z.ToString("f3"));
                }
                else
                {
                    Event.CAL_DEFZPOS_CANCEL.Set();
                }
            }
            catch
            { }
            finally
            {
                frmPR.Close();//.Done = true;
                this.Enabled = true;
                UpdateDisplay();
            }
        }
        private void btn_GotoDefZPos_Click(object sender, EventArgs e)
        {
            if (!TaskDisp.TaskMoveAbsGZZ2(TaskDisp.ZDefPos, TaskDisp.ZDefPos)) return;
        }
        private void lbl_ZDefPos_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Options, Z Default Position", ref TaskDisp.ZDefPos, 0.1, 5);
            if (!TaskDisp.TaskMoveAbsGZZ2(TaskDisp.ZDefPos, TaskDisp.ZDefPos)) return;
            UpdateDisplay();
        }

        private void btnCalDefLaserValue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Calibrate Laser Default Value?", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (MessageBox.Show("Please move laser to Calibrate Height position. Press OK to Continue.", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            if (!TaskLaser.LaserOpened) return;

            double d = 0;
            if (TaskLaser.GetHeight(ref d, false))
            {
                //double dNew = d - TaskDisp.Laser_RefPosZ;

                if (MessageBox.Show("Update Laser Default Value from " + TaskDisp.Laser_CalValue.ToString("f3") + " to " + d.ToString("f3"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TaskDisp.Laser_CalValue = d;
                    Event.CAL_LASER_CAL_VALUE_UPDATE.Set("Laser Cal Value", d.ToString("f3"));
                }
                else
                {
                    Event.CAL_LASER_CAL_VALUE_CANCEL.Set();
                }
            }

            UpdateDisplay();
        }
        private void lblDefLaserValue_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Options, Laser Cal Value", ref TaskDisp.Laser_CalValue, -15, 15);
            UpdateDisplay();
        }

        frmJogGantry frm_Jog = new frmJogGantry();
        private void btnJog_Click(object sender, EventArgs e)
        {
            frm_Jog.TopMost = true;
            frm_Jog.Show();
        }

        private void frmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm_Jog.Close();
            frm_Jog.Dispose();
        }

        private void lblSECSGEMProtocol_Click(object sender, EventArgs e)
        {
            int i = (int)TaskDisp.SECSGEMProtocol;
            if (!UC.AdjustExec("Options, SECSGEM Protocol", ref i, TaskDisp.ESECSGEMProtocol.None)) return;
            Enum.TryParse(((TaskDisp.ESECSGEMProtocol)i).ToString(), out TaskDisp.SECSGEMProtocol);

            lblSECSGEMProtocol.Text = ((TaskDisp.ESECSGEMProtocol)i).ToString();

            UpdateDisplay();
        }

        #region Process
        private void cbEnableMaterialLow_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Option, Enable Material Low", ref TaskDisp.Option_EnableMaterialLow);
            UpdateDisplay();
        }
        private void cbEnableDualMaterial_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Option, Enable Dual Material", ref TaskDisp.Option_EnableDualMaterial);
            UpdateDisplay();
        }
        private void cbMaterialLowForbidContinue_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Option, MaterialExpiryForbidContinue", ref TaskDisp.MaterialExpiryForbidContinue);
            UpdateDisplay();
        }

        private void cbEnableMaterial_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Option, EnableMaterialExpiry", ref TaskDisp.Material_EnableTimer);
            UpdateDisplay();
        }
        private void cbEnableValveExpiry_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Option, EnableValveExpiry", ref TaskDisp.Valve_EnableTimer);
            UpdateDisplay();
        }
        private void cbForbidMaterialExpriyContinue_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Option, MateriaLowForbidContinue", ref TaskDisp.MaterialLowForbidContinue);
            UpdateDisplay();
        }

        private void lblMaterialLifeTimeMultipler_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Disp Option, MaterialLifeMultiplier", ref TaskDisp.Material_Life_Multiplier, 1, 3600);
            UpdateDisplay();
        }
        private void lblMaterialExpiryPreAlertTime_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Material, ExpiryPreAlertTime", ref TaskDisp.Material_ExpiryPreAlertTime, 0, 720);
            UpdateDisplay();
        }

        private void cbEnableMaterialCounter_Click(object sender, EventArgs e)
        {
            Material.EnableUnitCounter = !Material.EnableUnitCounter;
            Log.OnSet("Material, EnableUnitCounter", !Material.EnableUnitCounter, Material.EnableUnitCounter);
            UpdateDisplay();
        }
        private void lblMaterialUnitCounterALimit_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Material, Unit Counter A Limit", ref Material.Unit.Limit[0], 0, 1000000000);
            UpdateDisplay();
        }
        private void lblMaterialUnitCounterAReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset Material Unit Counter A", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Log.OnSet("Material, Unit Counter A Reset", Material.Unit.Count[0], 0);
                Material.Unit.Count[0] = 0;
                GDefine.SaveDefault();
            }
            UpdateDisplay();
        }
        private void lblMaterialUnitCounterBLimit_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Material, Unit Counter B Limit", ref Material.Unit.Limit[1], 0, 1000000000);
            UpdateDisplay();
        }
        private void lblMaterialUnitCounterBReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset Material Unit Counter B", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Log.OnSet("Material, Unit Counter B Reset", Material.Unit.Count[1], 0);
                Material.Unit.Count[1] = 0;
                GDefine.SaveDefault();
            }
            UpdateDisplay();
        }

        private void cbEnablePanelCounter_Click(object sender, EventArgs e)
        {
            Material.EnablePanelCounter = !Material.EnablePanelCounter;
            Log.OnSet("Material, Panel Counter Enable", !Material.EnablePanelCounter, Material.EnablePanelCounter);

            UpdateDisplay();
        }
        private void lblMaterialPanelCounterReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset Material Panel Counter", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Log.OnSet("Material, Panel Counter Reset", Material.Panel.Count, 0);
                Material.Panel.Count = 0;
                GDefine.SaveDefault();
            }
            UpdateDisplay();
        }
        private void lblMaterialPanelCounterLimit_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Material, Panel Counter Limit", ref Material.Panel.Limit, 0, 1000000000);
            UpdateDisplay();
        }

        private void lblDensityRange_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("Material, Density Range", ref TaskDisp.Option_DensityRange, 0, 1);
            UpdateDisplay();
        }

        private void cbEnableNeedleShort_Click(object sender, EventArgs e)
        {
            TaskDisp.Option_EnableNeedleShort = !TaskDisp.Option_EnableNeedleShort;
            Log.OnSet("Option_EnableNeedleShort", !TaskDisp.Option_EnableNeedleShort, TaskDisp.Option_EnableNeedleShort);
            UpdateDisplay();
        }
        #endregion


        private async void btnConvertProgramToRecipe_ClickAsync(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Convert all Programs to Recipe?. Existing recipes will be overwritten.", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;

            this.Enable(false);
            frm_ProgressReport frm = new frm_ProgressReport();
            frm.Message = "Conversion in Progress. Please wait...";
            frm.Remark = $"Remaining time estimating...";
            frm.ShowCancel = true;
            frm.Show();

            int iConverted = 0;
            int iProcessed = 0;
            int iTotal = 0;
            try
            {
                var sw = Stopwatch.StartNew();

                await Task.Factory.StartNew(() =>
                {
                    string[] progFiles = Directory.GetFiles(GDefine.ProgPath, "*.prg");
                    iTotal = progFiles.Count();

                    foreach (string s in progFiles)
                    {
                        iProcessed++;

                        string progName = Path.GetFileNameWithoutExtension(s);

                        string sourceFileName = GDefine.ProgPath + "\\" + progName + "." + GDefine.ProgExt;
                        string destFileName = GDefine.RecipeDir.FullName + progName + GDefine.RecipeExt;
                        if (cbSkipIfExists.Checked && File.Exists(destFileName)) continue;
                        
                        if (!DispProg.Load(sourceFileName, true))
                        {
                            Log.AddToLog("Convert " + progName + ".prg to recipe failed.");
                        }

                        //string destFileName = GDefine.RecipeDir.FullName + progName + GDefine.RecipeExt;
                        DispProg.Save(destFileName);

                        Log.AddToLog("Convert " + progName + ".prg to recipe success.");
                        iConverted++;

                        int tTotalSecRemain = (int)(sw.Elapsed.TotalSeconds / iProcessed) * (iTotal - iProcessed);
                        int tSecRemain = tTotalSecRemain % 60;
                        int tTotalMinRemain = tTotalSecRemain / 60;
                        int tMinRemain = tTotalMinRemain % 60;
                        int tHourRemain = tTotalMinRemain % 60;

                        frm.Remark = $"Remaining time {tHourRemain} H : {tMinRemain} M : {tSecRemain} S";

                        if (frm.Cancel) break;
                    }
                });
            }
            catch
            {

            }
            finally
            {
                frm.Close();
                this.Enable(true);
            }
            MessageBox.Show("Converted " + iConverted.ToString() + " Programs to Recipes.");
        }

        private void tboxCustomPath_Leave(object sender, EventArgs e)
        {
            TaskDisp.CustomPath = tboxCustomPath.Text;
        }

        private void btnSelectCustomPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = TaskDisp.CustomPath;
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
                TaskDisp.CustomPath = fbd.SelectedPath;

            UpdateDisplay();
        }

        private void btnSafetyEnable_Click(object sender, EventArgs e)
        {
            if (NUtils.UserAcc.Active.GroupID < (int)ELevel.Engineer)
            {
                int i_UserIdx = NUtils.UserAcc.Active.UserIndex;
                NUtils.UserAcc.Users.LoginDlg();
                if (NUtils.UserAcc.Active.GroupID < (int)ELevel.Engineer)
                {
                    return;
                }
                Event.OP_MAIN_LOGIN.Set(NUtils.UserAcc.Active.GroupName, NUtils.UserAcc.Active.UserName);
            }

            int m = 0;
            UC.AdjustExec("Safety Bypass (minutes)", ref m, 0, 60);
            DefineSafety.dtEnable = DateTime.Now.AddMinutes(m);

            UpdateDisplay();
        }


        private void cbEnableEventDebugLog_Click(object sender, EventArgs e)
        {
            GDefineN.EnableEventDebugLog = !GDefineN.EnableEventDebugLog;
            UpdateDisplay();
        }

        private void cbSkipIfExists_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbLoadLastRecipe_Click(object sender, EventArgs e)
        {
            TaskDisp.LoadLastRecipe = true;// (sender as RadioButton).Checked;
        }

        private void rbLoadLastDevice_Click(object sender, EventArgs e)
        {
            TaskDisp.LoadLastRecipe = false;
        }

        private void btnSaveMsgFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(GDefine.MsgFile))
            {
                if (MessageBox.Show($"{GDefine.MsgFile} exist. Overwrite existing file?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (TEMessage.SaveList()) MessageBox.Show($"{GDefine.MsgFile} was saved.");
                    return;
                }
            }
            if (TEMessage.SaveList()) MessageBox.Show($"{GDefine.MsgFile} was created.");
        }

        private void btnLoadMgFile_Click(object sender, EventArgs e)
        {
            if (TEMessage.LoadList()) MessageBox.Show($"{GDefine.MsgFile} was loaded.");
        }

        private void btnSaveTwrLightStatusFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(GDefine.TLStatusFile))
            {
                if (MessageBox.Show($"{GDefine.TLStatusFile} exist. Overwrite existing file?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (TCTwrLight.SaveStatus()) MessageBox.Show($"{GDefine.TLStatusFile} was saved.");
                    return;
                }
            }

            if (TCTwrLight.SaveStatus()) MessageBox.Show($"{GDefine.TLStatusFile} was created.");
        }

        private void btnLoadTwrLightStatusFile_Click(object sender, EventArgs e)
        {
            if (TCTwrLight.LoadStatus()) MessageBox.Show($"{GDefine.TLStatusFile} was loaded.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSecsGem frm = new frmSecsGem();
            frm.ShowDialog();
        }
    }
}
