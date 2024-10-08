﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NDispWin
{
    public partial class frmVisionFailMsg2 : Form
    {
        frmMVCGenTLCamera TaskVisionfrmMVCGenTLCamera = new frmMVCGenTLCamera();

        public string Message = "";
        public bool ShowAccept = false;
        public bool ShowSkip = true;
        public bool ShowManual = true;

        public frmVisionFailMsg2()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            AutoSize = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            panel1.Top = 0;
            panel1.Left = this.Width - panel1.Width;
            panel1.AutoSize = true;
            this.TopMost = true;
            this.BringToFront();

            TaskVisionfrmMVCGenTLCamera = new frmMVCGenTLCamera();
            TaskVisionfrmMVCGenTLCamera.CamReticles = Reticle.Reticles;
            TaskVisionfrmMVCGenTLCamera.FormBorderStyle = FormBorderStyle.None;
            TaskVisionfrmMVCGenTLCamera.TopLevel = false;
            TaskVisionfrmMVCGenTLCamera.Parent = this;
            TaskVisionfrmMVCGenTLCamera.Dock = DockStyle.Fill;
            TaskVisionfrmMVCGenTLCamera.Show();
        } 

        Size s_Form = new Size(0,0);
        Point p_Form = new Point(0, 0);
        private void frmVisionFailMsg2_Load(object sender, EventArgs e)
        {
            btn_Accept.Visible = ShowAccept;
            btn_Skip.Visible = ShowSkip;
            btn_Manual.Visible = ShowManual;
            rtbMessage.Text = Message;
            rtbMessage.Visible = rtbMessage.Text.Length > 0;

            Left = 0;
            Top = 0;

            UpdateDisplay();

            Text = "Vision Fail Message";


                TaskVisionfrmMVCGenTLCamera.ShowCamReticles = true;
            TaskVisionfrmMVCGenTLCamera.SelectCamera(0);

            TCTwrLight.SetStatus(TwrLight.Error);
        }
        private void frmVisionFailMsg2_FormClosing(object sender, FormClosingEventArgs e)
        {
                TaskVisionfrmMVCGenTLCamera.Close();
        }

        enum EJogWindPos { TR, BR, BL, TL };
        EJogWindPos JogWindPos = EJogWindPos.TR;
        private void UpdateDisplay()
        {
            switch (JogWindPos)
            {
                case EJogWindPos.TR:
                    panel1.Left = this.Width - panel1.Width;
                    panel1.Top = 0;
                    break;
                case EJogWindPos.BR:
                    panel1.Left = this.Width - panel1.Width;
                    panel1.Top = this.Height - panel1.Height;
                    break;
                case EJogWindPos.BL:
                    panel1.Left = 0;
                    panel1.Top = this.Height - panel1.Height;
                    break;
                case EJogWindPos.TL:
                    panel1.Left = 0;
                    panel1.Top = 0;
                    break;
            }
        }

        private void btn_AlmClr_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetMute();//IO.SetState(EMcState.Mute);
        }

        private void btn_Accept_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.Yes;
        }

        private void btn_Retry_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.Retry;
        }

        private void btn_Skip_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.Cancel;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Idle);//TCTowerLight.SetStatus(TowerLight.Idle);
            DialogResult = DialogResult.Abort;
        }

        private void btn_Manual_Click(object sender, EventArgs e)
        {
            TCTwrLight.SetStatus(TwrLight.Run);//IO.SetState(EMcState.Last);
            DialogResult = DialogResult.OK;
        }


        private void pbox_Image_Click(object sender, EventArgs e)
        {

        }

        private void btn_JogPos_Click(object sender, EventArgs e)
        {
            if (JogWindPos < EJogWindPos.TL)
                JogWindPos++;
            else
                JogWindPos = EJogWindPos.TR;

            UpdateDisplay();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
