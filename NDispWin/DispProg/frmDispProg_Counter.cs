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
    internal partial class frm_DispCore_DispProg_Counter : Form
    {
        public DispProg.TLine CmdLine = new DispProg.TLine();
        public int ProgNo = 0;
        public int LineNo = 0;

        public frm_DispCore_DispProg_Counter()
        {
            InitializeComponent();
            GControl.LogForm(this);

            TopLevel = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            //AppLanguage.Func.SetComponent(this);
        }

        private void UpdateDisplay()
        {
            lbl_CounterID.Text = CmdLine.ID.ToString();
            if (CmdLine.IPara[0] == 0) lbl_Event.Text = "Reset";
            else
                if (CmdLine.IPara[0] > 0)
                    lbl_Event.Text = "Inc " + CmdLine.IPara[0].ToString();
                else
                    if (CmdLine.IPara[0] < 0)
                        lbl_Event.Text = "Dec " + CmdLine.IPara[0].ToString();
            lbl_Value.Text = DispProg.Counter.Count[CmdLine.ID].ToString();
        }

        private string CmdName
        {
            get
            {
                return LineNo.ToString("d3") + " " + CmdLine.Cmd.ToString();
            }
        }

        private void frmDispProg_Counter_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);

            CmdLine.Copy(DispProg.Script[ProgNo].CmdList.Line[LineNo]);

            this.Text = CmdName;
            
            UpdateDisplay();
        }

        private void lbl_CounterID_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", ID", ref CmdLine.ID, 0, 9);
            UpdateDisplay();
        }

        private void lbl_Event_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Event", ref CmdLine.IPara[0], -100, 100);
            UpdateDisplay();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DispProg.Script[ProgNo].CmdList.Line[LineNo].Copy(CmdLine);
            //frm_DispProg2.Done = true;
            Log.OnAction("OK", CmdName);
            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //frm_DispProg2.Done = true;
            Log.OnAction("Cancel", CmdName); 
            Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frm_DispCore_DispProg_Counter_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_DispProg2.Done = true;
        }
    }
}
