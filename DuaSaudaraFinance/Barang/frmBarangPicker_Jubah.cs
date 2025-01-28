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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangPicker_Jubah : Form
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

        public Action<string> frmBarangPicker_Jubah_Callback { get; set; }
        public string FormName { get; set; }

        public frmBarangPicker_Jubah()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = Check_frmBarangPicker_Jubah();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_Jubah_button1_Click ";        
                Kalimat += ((ComboboxValue)comboBox1.SelectedItem).Id + ", ";
                Kalimat += "0, ";
                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", ";
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                frmBarangPicker_Jubah_Callback.Invoke("fillListView1");

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

        private string Check_frmBarangPicker_Jubah()
        {
            string Kalimat = "";
                       

            if (comboBox1.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Jenis Jubah Tidak Boleh Kosong";
            }
            if (comboBox2.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Warna Tidak Boleh Kosong";
            }
            //if (comboBox3.Text == "")
            //{
            //    if (Kalimat != "")
            //    {
            //        Kalimat += System.Environment.NewLine;
            //    }
            //    Kalimat += "Size Tidak Boleh Kosong";
            //}

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

        private void fillComboBox1()
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_Jubah_fillComboBox1 ");
            while (myReader.Read())
            {
                comboBox1.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaJenisJubah"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox1.SelectedIndex = -1;
        }

        private void fillComboBox2()
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_Jubah_fillComboBox2 ");
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaWarnaBajuJaga"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox2.SelectedIndex = -1;


        }

        private void fillComboBox3()
        {
            //comboBox3.Text = "";
            //comboBox3.Items.Clear();

            //SqlDataReader myReader = null;
            //ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_JasDokter_fillComboBox2 ");
            //while (myReader.Read())
            //{
            //    comboBox3.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaSetBarang"].ToString()));
            //}
            //CloseReadSqlData(ref myReader);

            //comboBox3.SelectedIndex = -1;


        }

        private void frmBarangPicker_Jubah_Load(object sender, EventArgs e)
        {
            fillComboBox1();
            fillComboBox2();
            //fillComboBox3();

            groupBox4.Enabled = false;
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

        private void frmBarangPicker_Jubah_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmBarangPicker_Jubah_Callback.Invoke("Enabled");
        }
    }
}
