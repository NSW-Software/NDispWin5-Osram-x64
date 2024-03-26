using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NDispWin
{
    public partial class frm_OsramSCC_LotInfo : Form
    {
        public frm_OsramSCC_LotInfo()
        {
            InitializeComponent();
            GControl.LogForm(this);
        }

        private void frm_OsramSCC_Lot_Load(object sender, EventArgs e)
        {
            AppLanguage.Func2.UpdateText(this);
            
            this.Text = "OSRAM SCC Lot Info";

            cbox_Disable_2.Checked = !TaskDisp.OsramSCC.Enabled;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lbl_LotID.Text = TaskDisp.OsramSCC.LotID;
            lbl_11Series.Text = TaskDisp.OsramSCC.Series;
            lbl_DAStart.Text = TaskDisp.OsramSCC.DAStart;
            lbl_EmpID.Text = TaskDisp.OsramSCC.EmpID;
            lbl_TargetWeight.Text = DispProg.Target_Weight.ToString("f4");
            lbl_Weight1.Text = DispProg.Disp_Weight[0].ToString("f4");
            lbl_Weight2.Text = DispProg.Disp_Weight[1].ToString("f4");

            lbl_Density1.Text = TaskWeight.CurrentCal[0].ToString("f4");
            lbl_Density2.Text = TaskWeight.CurrentCal[1].ToString("f4");
        }

        private void btn_EndLot_Click(object sender, EventArgs e)
        {
            Log.AddToLog("Event" + (char)9 + "OsramSCC.LotInfo Click EndLot.");

            if (TaskDisp.OsramSCC.LotID.Length == 0) return;

            TaskDisp.OsramSCC.SendEndLot();
            DispProg.Stats.BoardCount = 0;

            UpdateDisplay();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbl_LotID_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.ClearLotInfo();
        }

        private void btn_ForceEndLot_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.ForceEndLot();
            DispProg.Stats.BoardCount++;

            Log.AddToLog("Event" + (char)9 + "OsramSCC.LotInfo Click ForceEndLot.");

            UpdateDisplay();
        }

        private void cbox_Disable_Click(object sender, EventArgs e)
        {
            TaskDisp.OsramSCC.Enabled = !TaskDisp.OsramSCC.Enabled;

            Log.AddToLog("Event" + (char)9 + "OsramSCC.LotInfo Enable " + TaskDisp.OsramSCC.Enabled.ToString() + ".");
        }
    }
}
