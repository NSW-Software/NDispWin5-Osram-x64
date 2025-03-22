using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDispWin
{
    public partial class frmSetupPP_HPC3 : Form
    {
        public frmSetupPP_HPC3()
        {
            InitializeComponent();

            Text = "SetupPP_HPC3";
        }

        private void frmSetupPP_HPC3_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        public void ToolsOnly()
        {
            tabControl1.TabPages.Remove(tpParameters);
            tabControl1.TabPages.Remove(tpIO);
            gbxAmount.Visible = false;
        }

        private void UpdateDisplay()
        {
            UI_Utils.SetControlSelected(btn_HeadA, pumpSelect[0] && !pumpSelect[1]);
            UI_Utils.SetControlSelected(btn_HeadB, !pumpSelect[0] && pumpSelect[1]);
            UI_Utils.SetControlSelected(btn_HeadAB, pumpSelect[0] && pumpSelect[1]);

            lblHADispAmount.Text = $"{TFPump.PP4.DispAmounts[0]:f3}";
            lblHABSuckAmount.Text = $"{TFPump.PP4.BSuckAmounts[0]:f3}";

            lblHBDispAmount.Text = $"{TFPump.PP4.DispAmounts[1]:f3}";
            lblHBBSuckAmount.Text = $"{TFPump.PP4.BSuckAmounts[1]:f3}";

            lblRemoveAirTime.Text = $"{TFPump.PP4.RemoveAirTime:f1} s";
            lblCleanFillCount.Text = $"{TFPump.PP4.CleanFillCount} x";
            lblShotCount.Text = $"{TFPump.PP4.ShotCount} x";
            
            #region Para
            lblDispSpd.Text = $"{TFPump.PP4.DispSpeed:f2} mm/s";
            double dispADT1 = (TFPump.PP4.DispSpeed - TFPump.PP4.VelL) / TFPump.PP4.DispAD;
            lblDispAcc.Text = $"{TFPump.PP4.DispAD:f2} mm/s2\n{dispADT1*1000:f2} ms";

            lblBSuckSpd.Text = $"{TFPump.PP4.BSuckSpeed:f2} mm/s";
            double bSuckADT = (TFPump.PP4.BSuckSpeed - TFPump.PP4.VelL) / TFPump.PP4.BSuckAD;
            lblBSuckAcc.Text = $"{TFPump.PP4.BSuckAD:f2} mm/s2\n{bSuckADT * 1000:f2} ms";

            lblAccDec.Text = $"{TFPump.PP4.AccDec} mm/s2";
            lblInitSpeed.Text = $"{TFPump.PP4.InitSpeed} mm/s";
            lblCleanSpd.Text = $"{TFPump.PP4.CleanSpeed} mm/s";
            lblFillSpd.Text = $"{TFPump.PP4.FillSpeed} mm/s";
            lblAfFillSpd.Text = $"{TFPump.PP4.AfFillSpeed} mm/s";

            lblPAFPress.Text = $"{DispProg.FPress[0]:f2} MPa";
            lblPBFPress.Text = $"{DispProg.FPress[1]:f2} MPa";

            lblPistDia.Text = $"{TFPump.PP4.PistonDiameter} mm";
            lblPistStroke.Text = $"{TFPump.PP4.PistonStroke} mm";
            lblPistOverFlow.Text = $"{TFPump.PP4.RemoveAirPos} mm";
            lblRemoveAirPress.Text = $"{TFPump.PP4.RemoveAirPress} MPa";
            lblAfFillDist.Text = $"{TFPump.PP4.AfFillDist} mm";

            lblProcAmt.Text = $"{TFPump.PP4.ProcessAmount} %";
            lblProcTimeout.Text = $"{TFPump.PP4.ProcessTimeOut} s";

            lblMoveDelay.Text = $"{TFPump.PP4.MoveDelay} ms";
            lblVacDur.Text = $"{TFPump.PP4.VacDuration} ms";
            lblPressOnDelay.Text = $"{TFPump.PP4.PressOnDelay} ms";
            lblPressOffDelay.Text = $"{TFPump.PP4.PressOffDelay} ms";
            lblRotaryDelay.Text = $"{TFPump.PP4.RotaryDelay} ms";
            #endregion
        }

        bool[] pumpSelect = new bool[] {true, false };

        private void lblHADispAmount_Click(object sender, EventArgs e)
        {
            double d = TFPump.PP4.DispAmounts[0];
            if (UC.AdjustExec("PP PA Disp Amount", ref d, 0.001, 1300))
                TFPump.PP4.DispAmounts = new double[]{ d, TFPump.PP4.DispAmounts[1] };
            UpdateDisplay();
        }
        private void lblHABSuckAmount_Click(object sender, EventArgs e)
        {
            double d = TFPump.PP4.BSuckAmounts[0];
            UC.AdjustExec("PP PA BSuck Amount", ref d, 0, 1300);
            TFPump.PP4.BSuckAmounts = new double[] {d, TFPump.PP4.BSuckAmounts[1] };
            UpdateDisplay();
        }
        private void lblHBDispAmount_Click(object sender, EventArgs e)
        {
            double d = TFPump.PP4.DispAmounts[1];
            if (UC.AdjustExec("PP PB Disp Amount", ref d, 0.001, 1300))
                TFPump.PP4.DispAmounts = new double[] { TFPump.PP4.DispAmounts[0], d };
            UpdateDisplay();
        }
        private void lblHBBSuckAmount_Click(object sender, EventArgs e)
        {
            double d = TFPump.PP4.BSuckAmounts[1];
            if (UC.AdjustExec("PP PB BSuck Amount", ref d, 0, 1300))
                TFPump.PP4.BSuckAmounts = new double[] { TFPump.PP4.BSuckAmounts[0] , d};
            UpdateDisplay();
        }

        private async void btnInit_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TFPump.PP4.Init(pumpSelect));
            GControl.UI_Enable();
        }

        private void btnFpressIO_Click(object sender, EventArgs e)
        {
            if (pumpSelect[0]) TaskGantry.BPress1 = !TaskGantry.BPress1;
            if (pumpSelect[1]) TaskGantry.BPress2 = !TaskGantry.BPress2;
        }

        private void lblRemoveAirTime_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Remove Air Time (s)", ref TFPump.PP4.RemoveAirTime, 0.01, 30);
            UpdateDisplay();
        }
        private async void btn_RemoveAirTimed_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable(btnRemoveAirCancel);
            await Task.Run(() => TFPump.PP4.PRemoveAir(pumpSelect));
            GControl.UI_Enable();
        }
        private void btnRemoveAirCancel_Click(object sender, EventArgs e)
        {
            TFPump.PP4.PRemoveAirCancel = true;
        }

        private void lblCleanFillCount_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Clean Fill Count", ref TFPump.PP4.CleanFillCount, 1, 20);
            UpdateDisplay();
        }
        bool CleanFillCancel = false;
        private async void btn_CleanFill_Click(object sender, EventArgs e)
        {
            CleanFillCancel = false;

            GControl.UI_Disable(btnCleanFillCancel);

            for (int i = 0; i < TFPump.PP4.CleanFillCount; i++)
            {
                lblCleanFillCount.Text = $"({i + 1}/{TFPump.PP4.CleanFillCount})";
                await Task.Run(() => { return TFPump.PP4.PCleanFill(pumpSelect); });
                if (CleanFillCancel) break;
            }

            UpdateDisplay();
            GControl.UI_Enable();
        }
        private void btnCleanFillCancel_Click(object sender, EventArgs e)
        {
            CleanFillCancel = true;
        }

        private async void btnFill_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TFPump.PP4.PFill(pumpSelect));
            GControl.UI_Enable();
        }

        private void lblShotCount_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Shot Count", ref TFPump.PP4.ShotCount, 1, 20);
            UpdateDisplay();
        }
        bool CancelShot = false;
        private async void btnShot_Click(object sender, EventArgs e)
        {
            CancelShot = false;
            GControl.UI_Disable(btnShotCancel);

            for (int i = 0; i < TFPump.PP4.ShotCount; i++)
            {
                lblShotCount.Text = $"({i + 1}/{TFPump.PP4.ShotCount})";

                await Task.Run(() =>
                {
                    TFPump.PP4.CheckStrokeThenFill(pumpSelect);
                    TFPump.PP4.SingleShot(pumpSelect);
                });
                if (CancelShot) break;
            }

            UpdateDisplay();
            GControl.UI_Enable();
        }
        private void btnShotCancel_Click(object sender, EventArgs e)
        {
            CancelShot = true;
        }

        private void lblDispSpd_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Disp Speed (mm/s)", ref TFPump.PP4.DispSpeed, 0.01, 30);
            UpdateDisplay();
        }
        private void lblDispAcc_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Disp AD (mm/s2)", ref TFPump.PP4.DispAD, 10, 500);
            UpdateDisplay();
        }
        private void lblBSuckSpd_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP BSuck Speed (mm/s)", ref TFPump.PP4.BSuckSpeed, 0.01, 30);
            UpdateDisplay();
        }
        private void lblBSuckAcc_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP BSuck AD (mm/s2)", ref TFPump.PP4.BSuckAD, 10, 500);
            UpdateDisplay();
        }

        private void lblAccDec_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP AccDec (mm/s2)", ref TFPump.PP4.AccDec, 10, 500);
            UpdateDisplay();
        }
        private void lblInitSpeed_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Init Speed (mm/s)", ref TFPump.PP4.InitSpeed, 0.01, 30);
            UpdateDisplay();
        }
        private void lblCleanSpd_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Clean Speed (mm/s)", ref TFPump.PP4.CleanSpeed, 0.01, 30);
            UpdateDisplay();
        }
        private void lblFillSpd_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Fill Speed (mm/s)", ref TFPump.PP4.FillSpeed, 0.01, 30);
            UpdateDisplay();
        }
        private void lblAfFillSpd_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP AfFill Speed (mm/s)", ref TFPump.PP4.AfFillSpeed, 0.01, 30);
            UpdateDisplay();
        }

        private void lblPAFPress_Click(object sender, EventArgs e)
        {
            double d_Min = FPressCtrl.GetPressMin;
            double d_Max = FPressCtrl.GetPressMax;

            FPressCtrl.AdjustPress_MPa(0, ref DispProg.FPress, d_Min, d_Max);
            UpdateDisplay();
        }
        private void lblPBFPress_Click(object sender, EventArgs e)
        {
            double d_Min = FPressCtrl.GetPressMin;
            double d_Max = FPressCtrl.GetPressMax;

            FPressCtrl.AdjustPress_MPa(1, ref DispProg.FPress, d_Min, d_Max);
            UpdateDisplay();
        }

        private void lblPistDia_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Piston Diameter (mm)", ref TFPump.PP4.PistonDiameter, 0.5, 10);
            UpdateDisplay();
        }
        private void lblPistStroke_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Piston Stroke (mm)", ref TFPump.PP4.PistonStroke, 1, 100);
            UpdateDisplay();
        }
        private void lblPistOverFlow_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Remove Air Pos (mm)", ref TFPump.PP4.RemoveAirPos, 0, 10);
            UpdateDisplay();
        }
        private void lblRemoveAirPress_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Remove Air Press (MPa)", ref TFPump.PP4.RemoveAirPress, 0, 10);
            UpdateDisplay();
        }
        private void lblAfFillDist_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP AfFill Dist (mm)", ref TFPump.PP4.AfFillDist, -5, 5);
            UpdateDisplay();
        }

        private void lblProcAmt_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Process Amount (%)", ref TFPump.PP4.ProcessAmount, 10, 100);
            UpdateDisplay();
        }
        private void lblProcTimeout_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Process Timeout (s)", ref TFPump.PP4.ProcessTimeOut, 10, 100);
            UpdateDisplay();
        }

        private void lblMoveDelay_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Move Delay (ms)", ref TFPump.PP4.MoveDelay, 0, 5000);
            UpdateDisplay();
        }
        private void lblPressOnDelay_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Press On Delay (ms)", ref TFPump.PP4.PressOnDelay, 0, 5000);
            UpdateDisplay();
        }
        private void lblPressOffDelay_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Press Off Delay (ms)", ref TFPump.PP4.PressOffDelay, 0, 5000);
            UpdateDisplay();
        }
        private void lblRotaryDelay_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Rotary Delay (ms)", ref TFPump.PP4.RotaryDelay, 0, 5000);
            UpdateDisplay();
        }
        private void lblVacDur_Click(object sender, EventArgs e)
        {
            UC.AdjustExec("PP Vacuum Duration (ms)", ref TFPump.PP4.VacDuration, 0, 5000);
            UpdateDisplay();
        }

        private void lbl_SensDAHome_Click(object sender, EventArgs e)
        {

        }

        private void lbl_DispSensB_Click(object sender, EventArgs e)
        {

        }

        private void lbl_DispSensA_Click(object sender, EventArgs e)
        {

        }

        private void lbl_FillSensA_Click(object sender, EventArgs e)
        {

        }

        private void lbl_FillSensB_Click(object sender, EventArgs e)
        {

        }

        private void lbl_SensDBHome_Click(object sender, EventArgs e)
        {

        }

        private void UpdateIO()
        {
            lblPASensHome.BackColor = TaskGantry.PASensHome ? Color.Lime : Color.LightGray;
            lblPASensDisp.BackColor = TaskGantry.PASensDisp ? Color.Lime : Color.LightGray;
            lblPASensFill.BackColor = TaskGantry.PASensFill ? Color.Lime : Color.LightGray;
            btnPASvFPress.BackColor = TaskGantry.BPress1 ? Color.Red : Color.LightGray;
            btnPASvVac.BackColor = TaskGantry.BVac1 ? Color.Red : Color.LightGray;
            btnPASvRotDisp.BackColor = TaskGantry.PASvRotDisp ? Color.Red : Color.LightGray;
            btnPASvRotFill.BackColor = TaskGantry.PASvRotFill ? Color.Red : Color.LightGray;

            lblPBSensHome.BackColor = TaskGantry.PBSensHome ? Color.Lime : Color.LightGray;
            lblPBSensDisp.BackColor = TaskGantry.PBSensDisp ? Color.Lime : Color.LightGray;
            lblPBSensFill.BackColor = TaskGantry.PBSensFill ? Color.Lime : Color.LightGray;
            btnPBSvFPress.BackColor = TaskGantry.BPress2 ?  Color.Red : Color.LightGray;
            btnPBSvVac.BackColor = TaskGantry.BVac2 ? Color.Red : Color.LightGray;
            btnPBSvRotDisp.BackColor = TaskGantry.PBSvRotDisp ? Color.Red : Color.LightGray;
            btnPBSvRotFill.BackColor = TaskGantry.PBSvRotFill ? Color.Red : Color.LightGray;
        }
        private void tmr100ms_Tick(object sender, EventArgs e)
        {
            if (Visible && tabControl1.SelectedTab == tpIO) UpdateIO();
        }

        private void btnPASvFPress_Click(object sender, EventArgs e)
        {
            TaskGantry.BPress1 = !TaskGantry.BPress1;
        }
        private void btnPASvVac_Click(object sender, EventArgs e)
        {
            TaskGantry.BVac1 = !TaskGantry.BVac1;
        }
        private void btnPASvRotDisp_Click(object sender, EventArgs e)
        {
            TaskGantry.PASvRotDisp = !TaskGantry.PASvRotDisp;
        }
        private void btnPASvRotFill_Click(object sender, EventArgs e)
        {
            TaskGantry.PASvRotFill = !TaskGantry.PASvRotFill;
        }

        private void btnPBSvFPress_Click(object sender, EventArgs e)
        {
            TaskGantry.BPress2 = !TaskGantry.BPress2;
        }
        private void btnPBSvVac_Click(object sender, EventArgs e)
        {
            TaskGantry.BVac2 = !TaskGantry.BVac2;
        }
        private void btnPBSvRotDisp_Click(object sender, EventArgs e)
        {
            TaskGantry.PBSvRotDisp = !TaskGantry.PBSvRotDisp;
        }
        private void btnPBSvRotFill_Click(object sender, EventArgs e)
        {
            TaskGantry.PBSvRotFill = !TaskGantry.PBSvRotFill;
        }

        private void btn_HeadA_Click(object sender, EventArgs e)
        {
            pumpSelect[0] = true;
            pumpSelect[1] = false;
            UpdateDisplay();
        }
        private void btn_HeadB_Click(object sender, EventArgs e)
        {
            pumpSelect[0] = false;
            pumpSelect[1] = true;
            UpdateDisplay();
        }
        private void btn_HeadAB_Click(object sender, EventArgs e)
        {
            pumpSelect[0] = true;
            pumpSelect[1] = true;
            UpdateDisplay();
        }

        private void gbxAmount_Enter(object sender, EventArgs e)
        {

        }
    }
}
