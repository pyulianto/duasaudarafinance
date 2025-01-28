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
    public partial class frmBarangPicker_Universal : Form
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
        public Action<string> BarangPicker_Universal_Callback { get; set; }
        public string FormName { get; set; }
        public string NamaBarang { get; set; }
        public frmBarangPicker_Universal()
        {
            InitializeComponent();
        }

        private void frmBarangPicker_Universal_Load(object sender, EventArgs e)
        {
            this.Text = "Tambah: " + NamaBarang;
            ActivateGroupBox();

        }

        private void ActivateGroupBox()
        {
            switch (NamaBarang)
            {
                case "Baju Korpri":
                    ActivateGroupBox_JenisKelamin();
                    ActivateGroupBox_Lengan();
                    ActivateGroupBox_Size();
                    break;
                case "Baju Koki":
                    ActivateGroupBox_Lengan();
                    ActivateGroupBox_Warna();
                    ActivateGroupBox_Size();
                    break;
                case "Jas PDGI":
                    ActivateGroupBox_JenisKelamin();
                    ActivateGroupBox_Size();
                    break;
                case "Jas IDI":
                    ActivateGroupBox_JenisKelamin();
                    ActivateGroupBox_Size();
                    break;

            }
        }        

        private void ActivateGroupBox_JenisKelamin()
        {
            groupBox2.Enabled = true;
        }
        private void ActivateGroupBox_Lengan()
        {
            groupBox3.Enabled = true;
        }
        private void ActivateGroupBox_Size()
        {
            FillComboBox2();
            groupBoxRight1.Enabled = true;
        }
        private void ActivateGroupBox_Warna()
        {
            FillComboBox3();
            groupBox4.Enabled = true;
        }
        
        private void FillComboBox2()
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_JasDokter_fillComboBox1 ");
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaSize"].ToString()));
            }
            CloseReadSqlData(ref myReader);

        }
        private void FillComboBox3()
        {
            comboBox3.Text = "";
            comboBox3.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_Jubah_fillComboBox2 ");
            while (myReader.Read())
            {
                comboBox3.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaWarnaBajuJaga"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox3.SelectedIndex = -1;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (NamaBarang)
            {
                case "Baju Korpri":
                    button_Click_BajuKorpri();
                    break;
                case "Baju Koki":
                    button_Click_BajuKoki();
                    break;
                case "Jas PDGI":
                    button_Click_JasPDGI();
                    break;
                case "Jas IDI":
                    button_Click_JasIDI();
                    break;
                default:
                    break;
            }

            if (FormName != "BarangMasuk")
            {
                Close();
            }
            

        }

        private void button_Click_BajuKorpri()
        {
            string ResultCheck = CheckBarangPicker_Universal();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_Universal_button_Click_BajuKorpri ";

                if (radioButton1.Checked)
                {
                    Kalimat += "'Cowo', ";
                }
                else
                {
                    Kalimat += "'Cewe', ";
                }

                if (radioButton3.Checked)
                {
                    Kalimat += "'Pendek', ";
                }
                else
                {
                    Kalimat += "'Panjang', ";
                }
                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", ";
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                BarangPicker_Universal_Callback.Invoke("fillListView1");
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     
        private void button_Click_BajuKoki()
        {
            string ResultCheck = CheckBarangPicker_Universal();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_Universal_button_Click_BajuKoki ";

                Kalimat += ((ComboboxValue)comboBox3.SelectedItem).Id + ", ";

                if (radioButton3.Checked)
                {
                    Kalimat += "'Pendek', ";
                }
                else
                {
                    Kalimat += "'Panjang', ";
                }
                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", ";
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                BarangPicker_Universal_Callback.Invoke("fillListView1");
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_Click_JasPDGI()
        {
            string ResultCheck = CheckBarangPicker_Universal();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_Universal_button_Click_JasPDGI ";

                if (radioButton1.Checked)
                {
                    Kalimat += "'Cowo', ";
                }
                else
                {
                    Kalimat += "'Cewe', ";
                }
                
                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", ";
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                BarangPicker_Universal_Callback.Invoke("fillListView1");
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_Click_JasIDI()
        {
            string ResultCheck = CheckBarangPicker_Universal();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_Universal_button_Click_JasIDI ";

                if (radioButton1.Checked)
                {
                    Kalimat += "'Cowo', ";
                }
                else
                {
                    Kalimat += "'Cewe', ";
                }

                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", ";
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                BarangPicker_Universal_Callback.Invoke("fillListView1");
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private string CheckBarangPicker_Universal()
        {
            string Kalimat = "";

            if (groupBox2.Enabled)
            {
                if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    if (Kalimat != "")
                    {
                        Kalimat += System.Environment.NewLine;
                    }
                    Kalimat += "Jenis Kelamin Tidak Boleh Kosong";
                }
            }

            if (groupBox3.Enabled)
            {
                if (radioButton3.Checked == false && radioButton4.Checked == false)
                {
                    if (Kalimat != "")
                    {
                        Kalimat += System.Environment.NewLine;
                    }
                    Kalimat += "Lengan Tidak Boleh Kosong";
                }
            }

            if (groupBox4.Enabled)
            {
                if (comboBox3.Text == "")
                {
                    if (Kalimat != "")
                    {
                        Kalimat += System.Environment.NewLine;
                    }
                    Kalimat += "Warna Tidak Boleh Kosong";
                }
            }

            if (groupBoxRight1.Enabled)
            {
                if (comboBox2.Text == "")
                {
                    if (Kalimat != "")
                    {
                        Kalimat += System.Environment.NewLine;
                    }
                    Kalimat += "Size Tidak Boleh Kosong";
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void frmBarangPicker_Universal_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarangPicker_Universal_Callback.Invoke("Enabled");
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

        
    }
}
