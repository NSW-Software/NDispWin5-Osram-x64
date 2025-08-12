using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NDispWin.TFSecsGem;

namespace NDispWin
{
    public partial class frmBinCode : Form
    {
        private BindingList<BinCodeItem> BinCodes;

        public frmBinCode()
        {
            InitializeComponent();



        }
        public frmBinCode(BindingList<BinCodeItem> binCodes) : this()
        {
            Text = "BinCode Editor";
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            BinCodes = binCodes;
        }
        private void frmBinCode_Load(object sender, EventArgs e)
        {
            //var allowedStates = BinCodes.Select(b => b.BinState).Distinct().ToList();
            //GControl2.UpdateDGV(dgvBinCode, nameof(BinCodes));

            dgvBinCode.AutoGenerateColumns = false;
            dgvBinCode.DataSource = BinCodes;


            var colState = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BinCodeItem.BinState),
                HeaderText = "BIN STATE",
                ReadOnly = true,
                Width = 200
            };

            var colHex = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BinCodeItem.Value),
                HeaderText = "BinCode (0000-9999)",
                Width = 80
            };
            dgvBinCode.Columns.AddRange(colState, colHex);
        }
    }
}
