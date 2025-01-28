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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangPicker_BajuPerawat : Form
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
        public Action<string> BarangPicker_BajuPerawat_Callback { get; set; }
        public string FormName { get; set; }
        public frmBarangPicker_BajuPerawat()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void frmBarangPicker_BajuPerawat_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarangPicker_BajuPerawat_Callback.Invoke("Enabled");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckBarangPicker_BajuPerawat();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_BajuPerawat_button1_Click ";

                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                if (radioButton1.Checked)
                {
                    Kalimat += "'Cowo', ";
                }
                else
                {
                    Kalimat += "'Cewe', ";
                }                
                Kalimat += ((ComboboxValue)comboBox1.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", ";
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                BarangPicker_BajuPerawat_Callback.Invoke("fillListView1");
                Close();
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBarangPicker_BajuPerawat_Load(object sender, EventArgs e)
        {
            fillComboBox1();
            fillComboBox2();
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
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_BajuPerawat_fillComboBox2 ");
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaJenisBajuPerawat"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox2.SelectedIndex = -1;


        }
        private string CheckBarangPicker_BajuPerawat()
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
                Kalimat += "Jenis Tidak Boleh Kosong";
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
    }
}
