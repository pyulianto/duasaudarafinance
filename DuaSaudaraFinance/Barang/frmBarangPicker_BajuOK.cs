using DuaSaudaraFinance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangPicker_BajuOK : Form
    {
        public Action<string> frmBarangPicker_Callback { get; set; }
        public string FormName { get; set; }

        private FrmBarangPicker_BajuOK_ViewModel _viewModel = new FrmBarangPicker_BajuOK_ViewModel();



        public frmBarangPicker_BajuOK()
        {
            InitializeComponent();

            BindSetBarangOK();
            BindJenisBarangOK();
            BindWarnaBarangOK();
            BindSize();
        }
        private void BindSetBarangOK()
        {
            setBarangOKComboBox.DataSource = _viewModel._attributeBajuOKSetList;
            setBarangOKComboBox.DisplayMember = "Name";
            setBarangOKComboBox.ValueMember = "Id";
            setBarangOKComboBox.SelectedIndex = -1;
        }
        private void BindJenisBarangOK()
        {
            jenisBarangOKComboBox.DataSource = _viewModel._attributeBajuOKJenisList;
            jenisBarangOKComboBox.DisplayMember = "Name";
            jenisBarangOKComboBox.ValueMember = "Id";
            jenisBarangOKComboBox.SelectedIndex = -1;
        }
        private void BindWarnaBarangOK()
        {
            warnaBarangOKComboBox.DataSource = _viewModel._attributeBajuOKWarnaList;
            warnaBarangOKComboBox.DisplayMember = "Name";
            warnaBarangOKComboBox.ValueMember = "Id";
            warnaBarangOKComboBox.SelectedIndex = -1;
        }
        private void BindSize()
        {
            sizeComboBox.DataSource = _viewModel._attributeSizeList;
            sizeComboBox.DisplayMember = "Name";
            sizeComboBox.ValueMember = "Id";
            sizeComboBox.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private string Check_frmBarangPicker_BajuOK()
        {
            string Kalimat = "";

            if (string.IsNullOrEmpty(jenisBarangOKComboBox.Text))
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Jenis Baju OK Tidak Boleh Kosong";
            }
            if (string.IsNullOrEmpty(setBarangOKComboBox.Text))
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Set Barang OK Tidak Boleh Kosong";
            }
            if (string.IsNullOrEmpty(warnaBarangOKComboBox.Text))
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Set Warna Tidak Boleh Kosong";
            }
            if (string.IsNullOrEmpty(sizeComboBox.Text))
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Size Tidak Boleh Kosong";
            }
            if (string.IsNullOrEmpty(quantityTextBox.Text))
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Qty Tidak Boleh Kosong";
            }           

            return Kalimat;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hargaTextBox.Text))
            {
                hargaTextBox.Text = "0";
            }

            string ResultCheck = Check_frmBarangPicker_BajuOK();

            if (ResultCheck == "")
            {              


                _viewModel.UpsertBajuOK((int)setBarangOKComboBox.SelectedValue, (int)jenisBarangOKComboBox.SelectedValue, (int)sizeComboBox.SelectedValue,
                    (int)warnaBarangOKComboBox.SelectedValue, Convert.ToInt32(quantityTextBox.Text), Convert.ToDecimal(hargaTextBox.Text), FormName);

                frmBarangPicker_Callback.Invoke("fillListView1");

                if (FormName== "TambahTransaksiPenjualan")
                {
                    Close();
                }
                

            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmBarangPicker_BajuOK_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmBarangPicker_Callback.Invoke("Enabled");
        }

        private void frmBarangPicker_BajuOK_Load(object sender, EventArgs e)
        {

        }
    }
}
