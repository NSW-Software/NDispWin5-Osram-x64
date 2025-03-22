using System;
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
    public partial class frm_LotEntryOsramICC : Form
    {
        public frm_LotEntryOsramICC()
        {
            InitializeComponent();
        }

        private void frm_LotEntryOsramType2_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            Text = "Lot Entry Osram ICC";

            LotInfo2.Osram.LoadSetup();

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            bool enabled = LotInfo2.LotStatus != LotInfo2.ELotStatus.Activated;
            tbLotNumber.Enabled = enabled;
            tb11Series.Enabled = enabled;
            tbEmployeeID.Enabled = enabled;
            tbDAStartNumber.Enabled = enabled;

            btn_StartLot.Enabled = enabled;
            btn_EndLot.Enabled = !enabled;

            tbLotNumber.Text = LotInfo2.LotNumber;
            tb11Series.Text = LotInfo2.Osram.ElevenSeries;
            tbEmployeeID.Text = LotInfo2.sOperatorID;
            tbDAStartNumber.Text = LotInfo2.Osram.DAStartNumber;

            tbField5Name.Visible = LotInfo2.Osram.F5Name.Length > 0 || edit;
            tbFieldName6.Visible = LotInfo2.Osram.F6Name.Length > 0 || edit;
            tbFieldName7.Visible = LotInfo2.Osram.F7Name.Length > 0 || edit;
            tbFieldName8.Visible = LotInfo2.Osram.F8Name.Length > 0 || edit;
            tbFieldValue5.Visible = LotInfo2.Osram.F5Name.Length > 0 || edit;
            tbFieldValue6.Visible = LotInfo2.Osram.F6Name.Length > 0 || edit;
            tbFieldValue7.Visible = LotInfo2.Osram.F7Name.Length > 0 || edit;
            tbFieldValue8.Visible = LotInfo2.Osram.F8Name.Length > 0 || edit;
            tbField5Name.Text = LotInfo2.Osram.F5Name;
            tbFieldName6.Text = LotInfo2.Osram.F6Name;
            tbFieldName7.Text = LotInfo2.Osram.F7Name;
            tbFieldName8.Text = LotInfo2.Osram.F8Name;
            tbField5Name.Enabled = edit;
            tbFieldName6.Enabled = edit;
            tbFieldName7.Enabled = edit;
            tbFieldName8.Enabled = edit;
        }

        private void UpdateVar()
        {
            LotInfo2.LotNumber = tbLotNumber.Text;
            LotInfo2.Osram.ElevenSeries = tb11Series.Text;
            LotInfo2.sOperatorID = tbEmployeeID.Text;
            LotInfo2.Osram.DAStartNumber = tbDAStartNumber.Text;
        }

        private void btn_StartLot_Click(object sender, EventArgs e)
        {
            UpdateVar();

            if (LotInfo2.sOperatorID.Length == 0)
            {
                MessageBox.Show("Please enter Employee ID.", "Error", MessageBoxButtons.OK);
                tbEmployeeID.Focus();
                return;
            }
            if (LotInfo2.LotNumber.Length == 0)
            {
                MessageBox.Show("Please enter Lot Number.", "Error", MessageBoxButtons.OK);
                tbLotNumber.Focus();
                return;
            }
            if (LotInfo2.Osram.ElevenSeries.Length == 0)
            {
                MessageBox.Show("Please enter 11-Series.", "Error", MessageBoxButtons.OK);
                tb11Series.Focus();
                return;
            } 
            if (LotInfo2.Osram.DAStartNumber.Length == 0)
            {
                MessageBox.Show("Please enter DA Start Number.", "Error", MessageBoxButtons.OK);
                tbDAStartNumber.Focus();
                return;
            }
            if (LotInfo2.Osram.F5Name.Length > 0 && LotInfo2.Osram.F5Value.Length == 0)
            {
                MessageBox.Show($"Please enter {LotInfo2.Osram.F5Name}.", "Error", MessageBoxButtons.OK);
                tbField5Name.Focus();
                return;
            }
            if (LotInfo2.Osram.F6Name.Length > 0 && LotInfo2.Osram.F6Value.Length == 0)
            {
                MessageBox.Show($"Please enter {LotInfo2.Osram.F6Name}.", "Error", MessageBoxButtons.OK);
                tbFieldName6.Focus();
                return;
            }
            if (LotInfo2.Osram.F7Name.Length > 0 && LotInfo2.Osram.F7Value.Length == 0)
            {
                MessageBox.Show($"Please enter {LotInfo2.Osram.F7Name}.", "Error", MessageBoxButtons.OK);
                tbFieldName7.Focus();
                return;
            }
            if (LotInfo2.Osram.F8Name.Length > 0 && LotInfo2.Osram.F8Value.Length == 0)
            {
                MessageBox.Show($"Please enter {LotInfo2.Osram.F8Name}.", "Error", MessageBoxButtons.OK);
                tbFieldName8.Focus();
                return;
            }

            bool loadOK = false;
            MsgBox.Processing("Load Dispense Recipe in Progress. Please wait.", LoadRecipe);
            void LoadRecipe()
            {
                string RecipeName = LotInfo2.Osram.ElevenSeries;
                if (RecipeName.Length == 0)
                {
                    Event.OP_DISP_AUTO_LOAD_DEVICE_INVALID.Set();
                    Msg MsgBox = new Msg();
                    MsgBox.Show("Auto Load - Invalid Recipe.");
                    return;
                }
                else
                if (RecipeName.Length >= 0)
                {
                    string fileName = GDefine.RecipeDir.FullName + RecipeName + GDefine.RecipeExt;
                    if (!File.Exists(fileName))
                    {
                        Event.OP_DISP_AUTO_LOAD_DEVICE_NO_FOUND.Set("Name", fileName);
                        Msg MsgBox = new Msg();
                        MsgBox.Show("Auto Load - Recipe not found.");
                        return;
                    }
                    else
                    {
                        if (!DispProg.LoadProgName(RecipeName)) return;
                    }
                }

                GDefineN.PerformanceReset();
                DispProg.Stats.Reset();

                Event.OP_DISP_AUTO_LOAD_SUCCESSFUL.Set("File", GDefine.ProgRecipeName);
                loadOK = true;
            }
            if (!loadOK)
            {
                return;
            }

            LotInfo2.LotActive = true;
            Event.OP_LOT_START.Set("LotInfo", $"{LotInfo2.sOperatorID},{LotInfo2.LotNumber},{LotInfo2.Osram.ElevenSeries},{LotInfo2.Osram.DAStartNumber},{LotInfo2.Osram.F5Value},{LotInfo2.Osram.F6Value},{LotInfo2.Osram.F7Value},{LotInfo2.Osram.F8Value}");

            UpdateDisplay();

            btn_Close.Focus();
        }
        private void btn_EndLot_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm End Lot?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LotInfo2.LotActive = false;
                Event.OP_LOT_END.Set("LotInfo", $"{LotInfo2.sOperatorID},{LotInfo2.LotNumber},{LotInfo2.Osram.ElevenSeries},{LotInfo2.Osram.DAStartNumber},{LotInfo2.Osram.F5Value},{LotInfo2.Osram.F6Value},{LotInfo2.Osram.F7Value},{LotInfo2.Osram.F8Value}");

                LotInfo2.LotNumber = "";
                LotInfo2.Osram.ElevenSeries = "";
                LotInfo2.sOperatorID = "";
                LotInfo2.Osram.DAStartNumber = "";

                LotInfo2.Osram.F5Value = "";
                LotInfo2.Osram.F6Value = "";
                LotInfo2.Osram.F7Value = "";
                LotInfo2.Osram.F8Value = "";

                UpdateDisplay();

                tbEmployeeID.Focus();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbox_EmployeeID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                tbLotNumber.Focus();
            }
        }
        private void tbox_LotNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                tb11Series.Focus();
            }
        }
        private void tbox_11Series_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                tbDAStartNumber.Focus();
            }
        }
        private void tbox_DAStartNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (tbFieldValue5.Visible) tbFieldValue5.Focus();
                else
                    btn_StartLot.Focus();
            }
        }
        private void tbFieldValue5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (tbFieldValue6.Visible) tbFieldValue6.Focus();
                else
                    btn_StartLot.Focus();
            }
        }
        private void tbFieldValue6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (tbFieldValue7.Visible) tbFieldValue7.Focus();
                else
                    btn_StartLot.Focus();
            }
        }
        private void tbFieldValue7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (tbFieldValue8.Visible) tbFieldValue8.Focus();
                else
                    btn_StartLot.Focus();
            }
        }
        private void tbFieldValue8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btn_StartLot.Focus();
            }
        }
        private void cbLoadRecipe_Click(object sender, EventArgs e)
        {

        }

        bool edit = false;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            edit = !edit;

            LotInfo2.Osram.F5Name = tbField5Name.Text;
            LotInfo2.Osram.F6Name = tbFieldName6.Text;
            LotInfo2.Osram.F7Name = tbFieldName7.Text;
            LotInfo2.Osram.F8Name = tbFieldName8.Text;

            if (!edit) LotInfo2.Osram.SaveSetup();

            UpdateDisplay();
        }
    }
}
