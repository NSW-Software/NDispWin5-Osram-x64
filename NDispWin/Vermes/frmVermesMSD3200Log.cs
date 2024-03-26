using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vermes
{
    public partial class frmVermesMSD3200Log : Form
    {
        public frmVermesMSD3200Log()
        {
            InitializeComponent();
            NDispWin.GControl.LogForm(this);

            Text = "Vermes Log";
        }

        public void AddLog(string S)
        {
            //lbox_Log.Invoke(new EventHandler(delegate
            //{
                lbox_Log.Items.Insert(0, DateTime.Now.ToLongTimeString() + " " + S);
                while (lbox_Log.Items.Count > 100)
                {
                    lbox_Log.Items.RemoveAt(lbox_Log.Items.Count - 1);
                }
            //}));
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            lbox_Log.Items.Clear();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string FileName = "c:\\Vermes" + "\\Vermes" + DateTime.Now.ToString("yyyyMMddHHmm") + ".log";
            NUtils.LogFileW File = new NUtils.LogFileW(FileName);
            foreach (string s in lbox_Log.Items)
            {
                File.Write(s);
            }
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void frmVermesMSD3200Log_Load(object sender, EventArgs e)
        {

        }
    }
}
