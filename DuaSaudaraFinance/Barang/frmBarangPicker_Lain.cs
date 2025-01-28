using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangPicker_Lain : Form
    {
        
        public Action<string> BarangPicker_Lain_Callback { get; set; }

        public string FormName { get; set; }

        public frmBarangPicker_Lain()
        {
            InitializeComponent();
        }

        private void frmBarangPicker_Lain_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckBarangPicker_Lain();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_Lain_button1_Click ";              


                Kalimat += "'" + textBox2.Text + "', ";
                Kalimat += textBox1.Text + ", ";


                if (textBox3.Text == "")
                {
                    Kalimat += "0, ";
                }
                else
                {
                    Kalimat += textBox3.Text + ", ";
                }

                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                BarangPicker_Lain_Callback.Invoke("fillListView1");

                if (FormName == "TambahTransaksiPenjualan")
                {
                    Close();
                }

            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string CheckBarangPicker_Lain()
        {
            string Kalimat = "";

            if (textBox2.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Nama Barang Tidak Boleh Kosong";
            }

            if (textBox1.Text == "" || textBox1.Text == "0")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Qty Tidak Boleh Kosong";
            }





            return Kalimat;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 13:
                    //Enter Key
                    button1.PerformClick();
                    break;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    //Number Key
                    e.Handled = false;
                    break;
                case 8:
                    //Backspace Key
                    e.Handled = false;
                    break;
                case 127:
                    //Delete Key
                    e.Handled = false;
                    break;
                case 46:
                //Dot Key
                //e.Handled = false;
                //break;
                default:
                    e.Handled = true;
                    break;
            }
        }
        
        private void frmBarangPicker_Lain_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarangPicker_Lain_Callback.Invoke("Enabled");
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 13:
                    //Enter Key
                    button1.PerformClick();
                    break;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    //Number Key
                    e.Handled = false;
                    break;
                case 8:
                    //Backspace Key
                    e.Handled = false;
                    break;
                case 127:
                    //Delete Key
                    e.Handled = false;
                    break;
                //case 46:
                //    //Dot Key
                //    e.Handled = false;
                //    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                //case 13:
                //    //Enter Key
                //    button1.PerformClick();
                //    break;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    //Number Key
                    e.Handled = false;
                    break;
                case 8:
                    //Backspace Key
                    e.Handled = false;
                    break;
                case 127:
                    //Delete Key
                    e.Handled = false;
                    break;
                case 46:
                //Dot Key
                //e.Handled = false;
                //break;
                default:
                    e.Handled = true;
                    break;
            }
        }
    }
}
