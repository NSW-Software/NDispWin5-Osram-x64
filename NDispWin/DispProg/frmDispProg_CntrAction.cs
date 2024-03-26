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
    internal partial class frm_DispCore_DispProg_CntrAction : Form
    {
        public DispProg.TLine CmdLine = new DispProg.TLine();
        public int ProgNo = 0;
        public int LineNo = 0;
        
        public frm_DispCore_DispProg_CntrAction()
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
            try
            {
                lbl_Type.Text = CmdLine.IPara[0].ToString() + " - " + Enum.GetName(typeof(ECntrType), CmdLine.IPara[0]).ToString();
                lbl_Value.Text = CmdLine.IPara[1].ToString();
                lbl_Action.Text = CmdLine.IPara[2].ToString() + " - " + Enum.GetName(typeof(ECntrActionType), CmdLine.IPara[2]).ToString();
            }
            catch { };
        }

        private string CmdName
        {
            get
            {
                return LineNo.ToString("d3") + " " + CmdLine.Cmd.ToString();
            }
        }

        private void frmDispProg_CntrAct_Load(object sender, EventArgs e)
        {
            GControl.UpdateFormControl(this);
            AppLanguage.Func2.UpdateText(this);

            CmdLine.Copy(DispProg.Script[ProgNo].CmdList.Line[LineNo]);

            this.Text = CmdName;

            UpdateDisplay();
        }

        private void lbl_Type_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Type", ref CmdLine.IPara[0], 0, 2);
            UpdateDisplay();
        }

        private void lbl_Value_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Value", ref CmdLine.IPara[1], 1, 1000000000);
            UpdateDisplay();
        }

        private void lbl_Action_Click(object sender, EventArgs e)
        {
            UC.AdjustExec(CmdName + ", Action", ref CmdLine.IPara[2], 0, 1);
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

        private void frm_DispCore_DispProg_CntrAction_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_DispProg2.Done = true;
        }

    }
}
