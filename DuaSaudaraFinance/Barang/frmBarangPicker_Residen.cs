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

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangPicker_Residen : Form
    {
        class ComboboxValue
        {
            public int Id { get; private set; }
            public string Name { get; private set; }


            public ComboboxValue(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int IndexValue
            {
                get
                {
                    return Id;
                }
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public Action<string> frmBarangPicker_Residen_Callback { get; set; }
        public string FormName { get; set; }
        public frmBarangPicker_Residen()
        {
            InitializeComponent();
        }

        private void frmBarangPicker_Residen_Load(object sender, EventArgs e)
        {
            fillComboBox1();
            fillComboBox2();

            if (FormName== "TambahTransaksiPenjualan")
            {
                groupBox7.Enabled = false;
            }
        }

        private void fillComboBox1()
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_JasDokter_fillComboBox1 ");
            while (myReader.Read())
            {
                comboBox1.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaSize"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox1.SelectedIndex = -1;


        }

        private void fillComboBox2()
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_JasDokter_fillComboBox2 ");
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaSetBarang"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox2.SelectedIndex = -1;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = Check_frmBarangPicker_Residen();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_Residen_button1_Click ";
                
                if (radioButton1.Checked)
                {
                    Kalimat += "'Cowo', ";
                }
                else
                {
                    Kalimat += "'Cewe', ";
                }

                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                Kalimat += ((ComboboxValue)comboBox1.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", '";

                if (textBox3.Text=="")
                {
                    Kalimat += "Stok', ";
                }
                else
                {
                    Kalimat += textBox3.Text + "', ";
                }
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                frmBarangPicker_Residen_Callback.Invoke("fillListView1");

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

        private string Check_frmBarangPicker_Residen()
        {
            string Kalimat = "";


            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Jenis Kelamin Tidak Boleh Kosong";
            }

            if (comboBox1.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Size Tidak Boleh Kosong";
            }
            if (comboBox2.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Set Tidak Boleh Kosong";
            }

            if (textBox1.Text == "" || textBox1.Text == "0")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Qty Tidak Boleh Kosong";
            }

            if (textBox2.Text == "" || textBox2.Text == "0")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Harga Tidak Boleh Kosong";
            }





            return Kalimat;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void frmBarangPicker_Residen_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmBarangPicker_Residen_Callback.Invoke("Enabled");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }
    }
}
