﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NDispWin
{
    internal partial class frmSystemConfig : Form
    {
        enum ECom { None, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10, COM11, COM12, COM13, COM14, COM15, COM16 }
        enum EIPCom { IPAddress, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10, COM11, COM12, COM13, COM14, COM15, COM16 }

        public frmSystemConfig()
        {
            InitializeComponent();
            GControl.LogForm(this);

            StartPosition = FormStartPosition.CenterScreen;

            Button btn_Connect = new Button(); btn_Connect.AccessibleDescription = "Connect"; btn_Connect.Visible = false; this.Controls.Add(btn_Connect);
            Button btn_Disconnect = new Button(); btn_Disconnect.AccessibleDescription = "Disconnect"; btn_Disconnect.Visible = false; this.Controls.Add(btn_Disconnect);

            #region Motion Control
            combox_GantryConfig.DataSource = Enum.GetNames(typeof(GDefine.EGantryConfig));
            combox_HeadConfig.DataSource = Enum.GetNames(typeof(GDefine.EHeadConfig));
            cbxConveyorSystem.DataSource = Enum.GetNames(typeof(GDefine.EConveyorType));
            #endregion

            #region Camera and Lightings
            combox_Camera1Type.DataSource = Enum.GetNames(typeof(GDefine.ECameraType));
            combox_Camera2Type.DataSource = Enum.GetNames(typeof(GDefine.ECameraType));
            combox_Camera3Type.DataSource = Enum.GetNames(typeof(GDefine.ECameraType));

            cbxMCamera1Type.DataSource = Enum.GetNames(typeof(GDefine.ECameraType));
            cbxMCamera2Type.DataSource = Enum.GetNames(typeof(GDefine.ECameraType));

            combox_BottomCamType.DataSource = Enum.GetNames(typeof(GDefine.EBottomCamType));
            combox_LCType.DataSource = Enum.GetNames(typeof(GDefine.ELCType));
            combox_LCComport.DataSource = Enum.GetNames(typeof(ECom));
            combox_ExtVisType.DataSource = Enum.GetNames(typeof(ExtVision.EType));
            #endregion

            #region Height and ZSensor
            combox_HSensorType.DataSource = Enum.GetNames(typeof(GDefine.EHeightSensorType));
            combox_HSensorComport.DataSource = Enum.GetNames(typeof(EIPCom));
            combox_ZSensorType.DataSource = Enum.GetNames(typeof(GDefine.EZSensorType));
            #endregion

            #region Weight Station
            combox_WeightStType.DataSource = Enum.GetNames(typeof(WGH_Series.TEWeight.EWeighType));
            combox_WeightComport.DataSource = Enum.GetNames(typeof(ECom));
            #endregion

            #region Disp Control
            combox_DispCtrl1Type.DataSource = Enum.GetNames(typeof(GDefine.EDispCtrlType));
            combox_DispCtrl1Comport.DataSource = Enum.GetNames(typeof(ECom));
            combox_DispCtrl2Type.DataSource = Enum.GetNames(typeof(GDefine.EDispCtrlType));
            combox_DispCtrl2Comport.DataSource = Enum.GetNames(typeof(ECom));

            cbxDispHeaterType.DataSource = Enum.GetNames(typeof(GDefine.EDispHeaterType));
            cbxDispHeaterComport.DataSource = Enum.GetNames(typeof(ECom));
            combox_FPressType.DataSource = Enum.GetNames(typeof(SysConfig.EFPressAdjType));
            combox_FPress1Comport.DataSource = Enum.GetNames(typeof(ECom));
            combox_Pressunit.DataSource = Enum.GetNames(typeof(FPressCtrl.EPressUnit));

            combox_FPressCh.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                combox_FPressCh.Items.Add("Channel " + i.ToString());
            }
            combox_FPressCh.SelectedIndex = 0;
            #endregion

            #region Tab - Others
            cbxTempSensorType.DataSource = Enum.GetNames(typeof(SysConfig.ETempSensorType));
            cbxTempSensorComport.DataSource = Enum.GetNames(typeof(SysConfig.EComPorts));

            combox_TempCtrl_Type.DataSource = Enum.GetNames(typeof(GDefine.ETempCtrl));
            combox_TempCtrl_PortName.DataSource = Enum.GetNames(typeof(ECom));

            combox_TempCtrl0.DataSource = Enum.GetNames(typeof(GDefine.ETempCtrlModule));
            combox_TempCtrl1.DataSource = Enum.GetNames(typeof(GDefine.ETempCtrlModule));
            combox_TempCtrl2.DataSource = Enum.GetNames(typeof(GDefine.ETempCtrlModule));
            combox_TempCtrl3.DataSource = Enum.GetNames(typeof(GDefine.ETempCtrlModule));
            #endregion

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            #region Motion Control
            combox_GantryConfig.SelectedIndex = (int)GDefine.GantryConfig;
            combox_HeadConfig.SelectedIndex = (int)GDefine.HeadConfig;
            cbxConveyorSystem.SelectedIndex = (int)GDefine.ConveyorType;
            GDefine.UpdateInfo(lbl_Device0Info, TaskGantry.Device_0);
            GDefine.UpdateInfo(lbl_Device1Info, TaskGantry.Device_1);
            tbox_EquipmentName.Text = GDefine.EquipmentID;
            #endregion

            #region Camera & Lightings
            combox_Camera1Type.SelectedIndex = (int)GDefine.CameraType[0];
            combox_Camera2Type.SelectedIndex = (int)GDefine.CameraType[1];
            combox_Camera3Type.SelectedIndex = (int)GDefine.CameraType[2];
            tbxCam1IPAddress.Text = GDefine.CameraIPAddress[0];
            tbxCam2IPAddress.Text = GDefine.CameraIPAddress[1];
            tbxCam3IPAddress.Text = GDefine.CameraIPAddress[2];
            tbxCam1SerialNo.Text = GDefine.CameraSerialNo[0];
            tbxCam2SerialNo.Text = GDefine.CameraSerialNo[1];
            tbxCam3SerialNo.Text = GDefine.CameraSerialNo[2];

            cbxMCamera1Type.SelectedIndex = (int)GDefine.MCameraType[0];
            cbxMCamera2Type.SelectedIndex = (int)GDefine.MCameraType[1];
            tbxMCamera1IPAddress.Text = GDefine.MCameraIPAddress[0];
            tbxMCamera2IPAddress.Text = GDefine.MCameraIPAddress[1];
            tbxMCamera1SerialNo.Text = GDefine.MCameraSerialNo[0];
            tbxMCamera2SerialNo.Text = GDefine.MCameraSerialNo[1];
            btnMCamera1Connect.Text = TaskMCamera.CameraOpened(0) ? Lang_Disconnect : Lang_Connect;
            btnMCamera1Connect.BackColor = TaskMCamera.CameraOpened(0) ? Color.Lime : this.BackColor;
            btnMCamera2Connect.Text = TaskMCamera.CameraOpened(1) ? Lang_Disconnect : Lang_Connect;
            btnMCamera2Connect.BackColor = TaskMCamera.CameraOpened(1) ? Color.Lime : this.BackColor;

            combox_BottomCamType.SelectedIndex = (int)GDefine.BottomCamType;

            combox_LCType.SelectedIndex = (int)GDefine.LCType;
            combox_LCComport.SelectedIndex = GDefine.LCComport;

            combox_ExtVisType.Text = GDefine.ExtVisType.ToString();
            tbox_ExtVisIPAddress.Text = GDefine.ExtVisIPAddress;
            tbox_ExtVisPort.Text = GDefine.ExtVisPort.ToString();

            btn_Camera1Connect.Text = TaskVision.CameraOpened(0) ? Lang_Disconnect : Lang_Connect;
            btn_Camera1Connect.BackColor = TaskVision.CameraOpened(0) ? Color.Lime : this.BackColor;
            btn_Camera2Connect.Text = TaskVision.CameraOpened(1) ? Lang_Disconnect : Lang_Connect;
            btn_Camera2Connect.BackColor = TaskVision.CameraOpened(1) ? Color.Lime : this.BackColor;
            btn_Camera3Connect.Text = TaskVision.CameraOpened(2) ? Lang_Disconnect : Lang_Connect;
            btn_Camera3Connect.BackColor = TaskVision.CameraOpened(2) ? Color.Lime : this.BackColor;
            btn_LCConnect.Text = TaskVision.LightingOpened ? Lang_Disconnect : Lang_Connect;
            btn_LCConnect.BackColor = TaskVision.LightingOpened ? Color.Lime : this.BackColor;
            btn_ExtVisConnect.Text = ExtVision.Connected ? Lang_Disconnect : Lang_Connect;
            btn_ExtVisConnect.BackColor = ExtVision.Connected ? Color.Lime : this.BackColor;
            #endregion

            #region Height and ZSensor
            try { combox_HSensorType.SelectedIndex = (int)GDefine.HSensorType; } catch { };
            combox_HSensorComport.SelectedIndex = GDefine.HSensorComport;
            tbox_HSensorIPAddress.Text = GDefine.HSensorIPAddress;
            combox_ZSensorType.SelectedIndex = (int)GDefine.ZSensorType;

            btn_HSensorConnect.Text = TaskLaser.LaserOpened ? Lang_Disconnect : Lang_Connect;
            btn_HSensorConnect.BackColor = TaskLaser.LaserOpened ? Color.Lime : this.BackColor;
            #endregion

            #region Weight Statin
            combox_WeightStType.SelectedIndex = (int)GDefine.WeightStType;
            combox_WeightComport.SelectedIndex = GDefine.WeightComport;

            btn_WeightConnect.Text = TaskWeight.WeightIsOpen ? Lang_Disconnect : Lang_Connect;
            btn_WeightConnect.BackColor = TaskWeight.WeightIsOpen ? Color.Lime : this.BackColor;
            #endregion

            #region Disp Controls
            try { combox_DispCtrl1Type.SelectedIndex = (int)GDefine.DispCtrlType[0]; } catch { };
            combox_DispCtrl1Comport.SelectedIndex = GDefine.DispCtrlComport[0];
            try { combox_DispCtrl2Type.SelectedIndex = (int)GDefine.DispCtrlType[1]; } catch { };
            combox_DispCtrl2Comport.SelectedIndex = GDefine.DispCtrlComport[1];

            combox_DispCtrl1Comport.Enabled = GDefine.DispCtrlType[0] != GDefine.EDispCtrlType.HPC3;
            btn_DispCtrl1Connect.Enabled = GDefine.DispCtrlType[0] != GDefine.EDispCtrlType.HPC3;
            combox_DispCtrl2Comport.Enabled = GDefine.DispCtrlType[1] != GDefine.EDispCtrlType.HPC3;
            btn_DispCtrl2Connect.Enabled = GDefine.DispCtrlType[1] != GDefine.EDispCtrlType.HPC3;

            cbxDispHeaterType.Text = GDefine.DispHeaterType[0].ToString();
            cbxDispHeaterComport.Text = GDefine.DispHeaterComport[0];

            combox_FPressType.SelectedIndex = (int)SysConfig.FPressAdjType;
            combox_Pressunit.SelectedIndex = (int)FPressCtrl.PressUnit;
            combox_FPress1Comport.SelectedIndex = GDefine.FPressComport[combox_FPressCh.SelectedIndex];

            btn_DispCtrl1Connect.Text = TaskDisp.DispCtrlOpened(0) ? Lang_Disconnect : Lang_Connect;
            btn_DispCtrl1Connect.BackColor = TaskDisp.DispCtrlOpened(0) ? Color.Lime : this.BackColor;

            btn_DispCtrl2Connect.Text = TaskDisp.DispCtrlOpened(1) ? Lang_Disconnect : Lang_Connect;
            btn_DispCtrl2Connect.BackColor = TaskDisp.DispCtrlOpened(1) ? Color.Lime : this.BackColor;

            btnDispHeaterConnect.Text = TaskDisp.Vermes_HC48[0].IsOpen ? Lang_Disconnect : Lang_Connect;
            btnDispHeaterConnect.BackColor = TaskDisp.Vermes_HC48[0].IsOpen ? Color.Lime : this.BackColor;

            lbl_FPress1Connect.Text = FPressCtrl.ITV[combox_FPressCh.SelectedIndex].IsOpen ? Lang_Disconnect : Lang_Connect;
            lbl_FPress1Connect.BackColor = FPressCtrl.ITV[combox_FPressCh.SelectedIndex].IsOpen ? Color.Lime : this.BackColor;
            #endregion

            #region Others
            cbxTempSensorType.Text = SysConfig.TempSensorType.ToString();
            cbxTempSensorComport.Text = SysConfig.TempSensorComport.ToString();

            combox_TempCtrl_Type.Text = GDefine.TempCtrl_Type.ToString();
            combox_TempCtrl_PortName.Text = GDefine.TempCtrl_PortName.ToString();

            try { combox_TempCtrl0.SelectedIndex = (int)GDefine.TempCtrl_Module[0]; } catch { combox_TempCtrl0.SelectedIndex = 0; }
            try { combox_TempCtrl1.SelectedIndex = (int)GDefine.TempCtrl_Module[1]; } catch { combox_TempCtrl0.SelectedIndex = 1; }
            try { combox_TempCtrl2.SelectedIndex = (int)GDefine.TempCtrl_Module[2]; } catch { combox_TempCtrl0.SelectedIndex = 2; }
            try { combox_TempCtrl3.SelectedIndex = (int)GDefine.TempCtrl_Module[3]; } catch { combox_TempCtrl0.SelectedIndex = 3; }

            btnTempSensorConnect.Enabled = !TFTempSensor.IsOpen;
            btnTempSensorDisconnect.Enabled = TFTempSensor.IsOpen;

            btn_TempCtrl_Connect.Text = GDefine.Autonics_TX.IsOpen ? Lang_Disconnect : Lang_Connect;
            btn_TempCtrl_Connect.BackColor = GDefine.Autonics_TX.IsOpen ? Color.Lime : this.BackColor;

            nud_LogLevel.Value = GDefine.LogLevel;
            #endregion
        }

        string Lang_Connect = "";
        string Lang_Disconnect = "";
        private void frmGantryConfig_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            this.Text = "System Config";

            AppLanguage.Func2.UpdateText(this);
            Lang_Connect = AppLanguage.Func2.GetText(this, "Button", "Connect");
            Lang_Disconnect = AppLanguage.Func2.GetText(this, "Button", "Disconnect");

            UpdateDisplay();
        }
        private void frmSystemConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        private void frmSystemConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!this.Visible) return;
            //updateDisplay();
        }

        #region Motion Control
        private void combox_GantryConfig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.GantryConfig = (GDefine.EGantryConfig)combox_GantryConfig.SelectedIndex;
        }
        private void combox_HeadConfig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.HeadConfig = (GDefine.EHeadConfig)combox_HeadConfig.SelectedIndex;
        }
        private void cbxConveyorSystem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try { GDefine.ConveyorType = (GDefine.EConveyorType)cbxConveyorSystem.SelectedIndex; } catch { };
        }
        private void tbox_EquipmentName_MouseLeave(object sender, EventArgs e)
        {
            GDefine.EquipmentID = tbox_EquipmentName.Text;
        }

        private void lbl_Device0Info_Click(object sender, EventArgs e)
        {
            CControl2.TDevice OldDev = new CControl2.TDevice();
            OldDev = TaskGantry.Device_0;
            frmDeviceConfigEditor frm = new frmDeviceConfigEditor(TaskGantry.Device_0, "Device_0");
            if (frm.ShowDialog() == DialogResult.OK) TaskGantry.Device_0 = frm.Device;
            TaskGantry.UpdateDeviceConfig(OldDev, TaskGantry.Device_0);

            UpdateDisplay();
        }
        private void lbl_Device1Info_Click(object sender, EventArgs e)
        {
            CControl2.TDevice OldDev = new CControl2.TDevice();
            OldDev = TaskGantry.Device_1;
            frmDeviceConfigEditor frm = new frmDeviceConfigEditor(TaskGantry.Device_1, "Device_1");
            if (frm.ShowDialog() == DialogResult.OK) TaskGantry.Device_1 = frm.Device;
            TaskGantry.UpdateDeviceConfig(OldDev, TaskGantry.Device_1);

            UpdateDisplay();
        }

        #endregion

        #region Cameras and Lightings
        private void combox_Camera1Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.CameraType[0] = (GDefine.ECameraType)combox_Camera1Type.SelectedIndex;
            UpdateDisplay();
        }
        private void tbxCamera1IPAdress_TextChanged(object sender, EventArgs e)
        {
            GDefine.CameraIPAddress[0] = tbxCam1IPAddress.Text;
        }
        private void btn_Cam1ControlDlg_Click(object sender, EventArgs e)
        {
            //if (GDefine.CameraType[0] == GDefine.ECameraType.PtGrey)
            //    if (TaskVision.PGCamera[0] != null && TaskVision.PGCamera[0].IsConnected)
            //        TaskVision.PGCamera[0].ControlDlg = true;
        }
        private void btn_Camera1Connect_Click(object sender, EventArgs e)
        {
            int CamNo = 0;
            if (!TaskVision.CameraOpened(CamNo))
                TaskVision.OpenCamera(CamNo);
            else
                TaskVision.CloseCamera(CamNo);

            UpdateDisplay();
        }
        private void tbxCam1SerialNo_TextChanged(object sender, EventArgs e)
        {
            GDefine.CameraSerialNo[0] = tbxCam1SerialNo.Text;
        }

        private void combox_Camera2Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.CameraType[1] = (GDefine.ECameraType)combox_Camera2Type.SelectedIndex;
            UpdateDisplay();
        }
        private void tbxCamera2IPAdress_TextChanged(object sender, EventArgs e)
        {
            GDefine.CameraIPAddress[1] = tbxCam2IPAddress.Text;
        }
        private void btn_Cam2ControlDlg_Click(object sender, EventArgs e)
        {
            //if (GDefine.CameraType[1] == GDefine.ECameraType.PtGrey)
            //    if (TaskVision.PGCamera[1] != null && TaskVision.PGCamera[1].IsConnected)
            //        TaskVision.PGCamera[1].ControlDlg = true;
        }
        private void btn_Camera2Connect_Click(object sender, EventArgs e)
        {

            int CamNo = 1;
            if (!TaskVision.CameraOpened(CamNo))
            {
                TaskVision.OpenCamera(CamNo);
            }
            else
                TaskVision.CloseCamera(CamNo);

            UpdateDisplay();
        }
        private void tbxCam2SerialNo_TextChanged(object sender, EventArgs e)
        {
            GDefine.CameraSerialNo[1] = tbxCam2SerialNo.Text;
        }

        private void combox_Camera3Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.CameraType[2] = (GDefine.ECameraType)combox_Camera3Type.SelectedIndex;
            UpdateDisplay();
        }
        private void tbxCamera3IPAdress_TextChanged(object sender, EventArgs e)
        {
            GDefine.CameraIPAddress[2] = tbxCam3IPAddress.Text;
        }
        private void btn_Cam3ControlDlg_Click(object sender, EventArgs e)
        {
            //if (GDefine.CameraType[2] == GDefine.ECameraType.PtGrey)
            //    if (TaskVision.PGCamera[2] != null && TaskVision.PGCamera[2].IsConnected)
            //        TaskVision.PGCamera[2].ControlDlg = true;
        }
        private void btn_Camera3Connect_Click(object sender, EventArgs e)
        {
            int CamNo = 2;
            if (!TaskVision.CameraOpened(CamNo))
                TaskVision.OpenCamera(CamNo);
            else
                TaskVision.CloseCamera(CamNo);

            UpdateDisplay();
        }
        private void tbxCam3SerialNo_TextChanged(object sender, EventArgs e)
        {
            GDefine.CameraSerialNo[2] = tbxCam3SerialNo.Text;
        }

        private void cbxMCamera1Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.MCameraType[0] = (GDefine.ECameraType)cbxMCamera1Type.SelectedIndex;
            UpdateDisplay();
        }
        private void btnMCamera1Connect_Click(object sender, EventArgs e)
        {
            try
            {
                int CamNo = 0;
                if (!TaskMCamera.CameraOpened(CamNo))
                    TaskMCamera.OpenCamera(CamNo);
                else
                    TaskMCamera.CloseCamera(CamNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            UpdateDisplay();
        }
        private void tbxMCamera1IPAddress_TextChanged(object sender, EventArgs e)
        {
            GDefine.MCameraIPAddress[0] = tbxMCamera1IPAddress.Text;
        }
        private void tbxMCamera1SerialNo_TextChanged(object sender, EventArgs e)
        {
            GDefine.MCameraSerialNo[0] = tbxMCamera1SerialNo.Text;
        }

        private void cbxMCamera2Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.MCameraType[1] = (GDefine.ECameraType)cbxMCamera2Type.SelectedIndex;
            UpdateDisplay();
        }
        private void btnMCamera2Connect_Click(object sender, EventArgs e)
        {
            try
            {
                int CamNo = 1;
                if (!TaskMCamera.CameraOpened(CamNo))
                    TaskMCamera.OpenCamera(CamNo);
                else
                    TaskMCamera.CloseCamera(CamNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateDisplay();
        }
        private void tbxMCamera2IPAddress_TextChanged(object sender, EventArgs e)
        {
            GDefine.MCameraIPAddress[1] = tbxMCamera2IPAddress.Text;
        }
        private void tbxMCamera2SerialNo_TextChanged(object sender, EventArgs e)
        {
            GDefine.MCameraSerialNo[1] = tbxMCamera2SerialNo.Text;
        }

        private void combox_BottomCamType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.BottomCamType = (GDefine.EBottomCamType)combox_BottomCamType.SelectedIndex;
        }

        private void combox_LCType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.LCType = (GDefine.ELCType)combox_LCType.SelectedIndex;
        }
        private void combox_LCComport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.LCComport = combox_LCComport.SelectedIndex;
        }
        private void btn_LCConnect_Click(object sender, EventArgs e)
        {
            if (!TaskVision.LightingOpened)
                TaskVision.OpenLighting();
            else
                TaskVision.CloseLighting();
            UpdateDisplay();
        }

        private void btnMonitorWindow_Click(object sender, EventArgs e)
        {
            frmMonCamera frmMonitoring = new frmMonCamera();
            frmMonitoring.TopMost = true;
            frmMonitoring.ShowDialog();
        }

        private void combox_ExtVisType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.ExtVisType = (ExtVision.EType)combox_ExtVisType.SelectedIndex;
        }
        private void tbox_ExtVisIPAddress_TextChanged(object sender, EventArgs e)
        {
            GDefine.ExtVisIPAddress = tbox_ExtVisIPAddress.Text;
        }
        private void tbox_ExtVisPort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GDefine.ExtVisPort = Convert.ToInt16(tbox_ExtVisPort.Text);
            }
            catch { GDefine.ExtVisPort = 0; }
        }
        private void btn_RunMode_Click(object sender, EventArgs e)
        {
            try
            {
                ExtVision.Send_RunMode();
            }
            catch { };
        }
        private void btn_SetupMode_Click(object sender, EventArgs e)
        {
            try
            {
                ExtVision.Send_SetupMode();
            }
            catch { };
        }
        private void btn_Echo_Click(object sender, EventArgs e)
        {
            try
            {
                ExtVision.Send_Echo("echo");
            }
            catch { };
        }
        private void btn_Trig1_Click(object sender, EventArgs e)
        {
            btn_Trig1.Text = "Trig";
            try
            {
                bool OK = false;
                ExtVision.Send_Trig1(ref OK);
                btn_Trig1.Text = "Trig=" + OK.ToString();
            }
            catch
            {
                btn_Trig1.Text = "Trig Err";
            };
        }
        private void btn_ExtVisConnect_Click(object sender, EventArgs e)
        {
            if (ExtVision.Connected)
                ExtVision.Disconnect();
            else
                ExtVision.Connect();
            UpdateDisplay();
        }

        #endregion

        #region Height and ZSensor
        private void combox_HSensorType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.HSensorType = (GDefine.EHeightSensorType)combox_HSensorType.SelectedIndex;
        }
        private void combox_HSensorComport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.HSensorComport = combox_HSensorComport.SelectedIndex;
        }
        private void tbox_HSensorIPAddress_TextChanged(object sender, EventArgs e)
        {
            GDefine.HSensorIPAddress = tbox_HSensorIPAddress.Text;
        }
        private void btn_HSensorConnect_Click(object sender, EventArgs e)
        {
            if (!TaskLaser.LaserOpened)
            {
                TaskLaser.CloseLaser();
                TaskLaser.OpenLaser();
            }
            else
                TaskLaser.CloseLaser();
            UpdateDisplay();
        }

        private void combox_ZSensorType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.ZSensorType = (GDefine.EZSensorType)combox_ZSensorType.SelectedIndex;
        }

        #endregion

        #region Weight Station
        private void combox_WeightStType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.WeightStType = (WGH_Series.TEWeight.EWeighType)combox_WeightStType.SelectedIndex;
        }
        private void combox_WeightComport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.WeightComport = combox_WeightComport.SelectedIndex;
        }
        private void btn_WeightDlg_Click(object sender, EventArgs e)
        {
            WGH_Series.TEWeight.ShowDialog();
        }
        private void btn_WeightConnect_Click(object sender, EventArgs e)
        {
            if (!TaskWeight.WeightIsOpen)
                TaskWeight.WeightOpen();
            else
                TaskWeight.WeightClose();
            UpdateDisplay();
        }

        #endregion

        #region Disp Controls
        private void combox_DispCtrl1Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.DispCtrlType[0] = (GDefine.EDispCtrlType)combox_DispCtrl1Type.SelectedIndex;
            UpdateDisplay();
        }
        private void combox_DispCtrl1Comport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.DispCtrlComport[0] = combox_DispCtrl1Comport.SelectedIndex;
        }
        private void btn_ShowCtrl1_Click(object sender, EventArgs e)
        {
            TaskDisp.ShowDispCtrl(0);
            UpdateDisplay();
        }
        private void btn_DispCtrl1Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TaskDisp.DispCtrlOpened(0))
                    TaskDisp.OpenDispCtrl(0);
                else
                    TaskDisp.CloseDispCtrl(0);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }

            UpdateDisplay();
        }

        private void combox_DispCtrl2Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.DispCtrlType[1] = (GDefine.EDispCtrlType)combox_DispCtrl2Type.SelectedIndex;
            UpdateDisplay();
        }
        private void combox_DispCtrl2Comport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.DispCtrlComport[1] = combox_DispCtrl2Comport.SelectedIndex;
        }
        private void btn_ShowCtrl2_Click(object sender, EventArgs e)
        {
            TaskDisp.ShowDispCtrl(1);
            UpdateDisplay();
        }
        private void btn_DispCtrl2Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TaskDisp.DispCtrlOpened(1))
                    TaskDisp.OpenDispCtrl(1);
                else
                    TaskDisp.CloseDispCtrl(1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }

            UpdateDisplay();
        }

        private void cbxDispHeaterType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Enum.TryParse(cbxDispHeaterType.Text, out GDefine.DispHeaterType[0]);
        }
        private void cbxDispHeaterComport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.DispHeaterComport[0] = cbxDispHeaterComport.Items[cbxDispHeaterComport.SelectedIndex].ToString();
            try
            {
                TaskDisp.Vermes_HC48[0].sp.PortName = GDefine.DispHeaterComport[0];
            }
            catch { };
        }
        private void btnDispHeaterDlg_Click(object sender, EventArgs e)
        {
            switch (GDefine.DispCtrlType[0])
            {
                default:
                    break;
                case GDefine.EDispCtrlType.Vermes:
                    {
                        Vermes.frmVermesMDS3200 frm = new Vermes.frmVermesMDS3200();
                        frm.CtrlNo = 0;
                        frm.TopMost = true;
                        frm.ShowDialog();
                        frm.BringToFront();
                        break;
                    }
                case GDefine.EDispCtrlType.Vermes1560:
                    {
                        frmVermesMDS1560 frm = new frmVermesMDS1560();
                        frm.vm = TaskDisp.Vermes1560[0];
                        frm.TopMost = true;
                        frm.ShowDialog();
                        GDefine.DispHeaterComport[0] = TaskDisp.Vermes_HC48[0].sp.PortName;
                        break;
                    }
            }

            UpdateDisplay();
        }
        private void btnDispHeaterConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (TaskDisp.Vermes_HC48[0].IsOpen)
                    TaskDisp.Vermes_HC48[0].Close();
                else
                    TaskDisp.Vermes_HC48[0].Open(cbxDispHeaterComport.Text);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }
            UpdateDisplay();
        }

        private void combox_FPressType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SysConfig.FPressAdjType = (SysConfig.EFPressAdjType)combox_FPressType.SelectedIndex;
        }
        private void combox_Pressunit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FPressCtrl.PressUnit = (FPressCtrl.EPressUnit)combox_Pressunit.SelectedIndex;
            UpdateDisplay();
        }
        private void combox_FPressCh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void combox_FPress1Comport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.FPressComport[combox_FPressCh.SelectedIndex] = combox_FPress1Comport.SelectedIndex;
        }

        private void lbl_FPress1Connect_Click(object sender, EventArgs e)
        {
            if (FPressCtrl.ITV[combox_FPressCh.SelectedIndex].IsOpen)
                FPressCtrl.ITV[combox_FPressCh.SelectedIndex].Close();
            else
                FPressCtrl.ITV[combox_FPressCh.SelectedIndex].Open("COM" + GDefine.FPressComport[combox_FPressCh.SelectedIndex].ToString());

            UpdateDisplay();
        }
        private void btn_FPressSet_Click(object sender, EventArgs e)
        {
            try
            {
                double d = Convert.ToDouble(tbox_FPressSet.Text);
                FPressCtrl.Set_PressUnit(combox_FPressCh.SelectedIndex, d);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }
        }
        private void btn_FPressGet_Click(object sender, EventArgs e)
        {
            try
            {
                double d = 0;
                FPressCtrl.GetR(combox_FPressCh.SelectedIndex, ref d);
                lbl_FPressGet.Text = d.ToString(FPressCtrl.PressUnitStrFmt);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }
        }
        #endregion

        #region Others
        private void cbxTempSensorType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SysConfig.TempSensorType = (SysConfig.ETempSensorType)cbxTempSensorType.SelectedIndex;
            UpdateDisplay();
        }
        private void cbxTempSensorComport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SysConfig.TempSensorComport = cbxTempSensorComport.SelectedItem.ToString();
            UpdateDisplay();
        }
        private void btnTempSensorConnect_Click(object sender, EventArgs e)
        {
            try
            {
                TFTempSensor.Open();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }
            UpdateDisplay();
        }
        private void btnTempSensorDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                TFTempSensor.Close();
            }
            catch { }
            UpdateDisplay();
        }
        private void btnTempSensorInfo_Click(object sender, EventArgs e)
        {
            try
            {
                int modelWord = 0;
                double lowerTemp = 0;
                double upperTemp = 0;
                TFTempSensor.ThermoCT.ReadSensorInfo(ref modelWord, ref lowerTemp, ref upperTemp);

                string serialNo = "";
                TFTempSensor.ThermoCT.ReadSerialNo(ref serialNo);

                string fwVer = "";
                TFTempSensor.ThermoCT.ReadFWVer(ref fwVer);

                double proc = 0;
                TFTempSensor.GetTemp(ref proc);

                MessageBox.Show(
                    $"Model: {modelWord}\n" +
                    $"Temp Range: {lowerTemp:f1} ~ {upperTemp:f1}\n" +
                    $"SerialNo: {serialNo}\n" +
                    $"FwVer: {fwVer}\n" +
                    $"Process: {proc} C"
                    , "Temp Sensor Info"
                    );
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }
            UpdateDisplay();
        }

        private void combox_TempCtrl_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.TempCtrl_Type = (GDefine.ETempCtrl)combox_TempCtrl_Type.SelectedIndex;
        }

        private void combox_TempCtrl_PortName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.TempCtrl_PortName = combox_TempCtrl_PortName.Items[combox_TempCtrl_PortName.SelectedIndex].ToString();
        }
        private void btn_TempCtrl_CtrlDlg_Click(object sender, EventArgs e)
        {
            Modbus.frm_Autonics_TX frm = new Modbus.frm_Autonics_TX();
            frm.at = GDefine.Autonics_TX;
            frm.ShowDialog();

            UpdateDisplay();
        }
        private void btn_TempCtrl_Connect_Click(object sender, EventArgs e)
        {
            //GDefine.TempCtrl_PortName = combox_TempCtrl_PortName.Text;

            if (!GDefine.Autonics_TX.IsOpen)
            {
                if (!GDefine.Autonics_TX.OpenPort(GDefine.TempCtrl_PortName))
                    MessageBox.Show(GDefine.Autonics_TX.GetStatus);
            }
            else
                GDefine.Autonics_TX.ClosePort();

            UpdateDisplay();
        }
        private void combox_TempCtrl0_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GDefine.TempCtrl_Module[0] = (GDefine.ETempCtrlModule)combox_TempCtrl0.SelectedIndex;
            GDefine.TempCtrl_Module[1] = (GDefine.ETempCtrlModule)combox_TempCtrl1.SelectedIndex;
            GDefine.TempCtrl_Module[2] = (GDefine.ETempCtrlModule)combox_TempCtrl2.SelectedIndex;
            GDefine.TempCtrl_Module[3] = (GDefine.ETempCtrlModule)combox_TempCtrl3.SelectedIndex;
        }
        private void btnTempCtrlUpdate_Click(object sender, EventArgs e)
        {
            GDefine.TempCtrl_Module[0] = (GDefine.ETempCtrlModule)combox_TempCtrl0.SelectedIndex;
            GDefine.TempCtrl_Module[1] = (GDefine.ETempCtrlModule)combox_TempCtrl1.SelectedIndex;
            GDefine.TempCtrl_Module[2] = (GDefine.ETempCtrlModule)combox_TempCtrl2.SelectedIndex;
            GDefine.TempCtrl_Module[3] = (GDefine.ETempCtrlModule)combox_TempCtrl3.SelectedIndex;

            TempCtrl.Init();
            if (!TempCtrl.Run())
                MessageBox.Show(GDefine.Autonics_TX.GetStatus);
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = GDefine.ConfigPath;
            ofd.Filter = "GantryConfig|*.ini";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string ConfigName = ofd.FileName;
                ConfigName = Path.GetFileNameWithoutExtension(ConfigName);
                GDefine.LoadSystemConfig(ConfigName);
            }

            UpdateDisplay();
        }
        private void btn_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = GDefine.ConfigPath;
            sfd.Filter = "GantryConfig|*.ini";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string ConfigName = sfd.FileName;
                ConfigName = Path.GetFileNameWithoutExtension(ConfigName);
                GDefine.SaveSystemConfig(ConfigName);
            }
        }
        private void nud_LogLevel_Click(object sender, EventArgs e)
        {
            GDefine.LogLevel = (int)nud_LogLevel.Value;

        }
        #endregion
    }
}
