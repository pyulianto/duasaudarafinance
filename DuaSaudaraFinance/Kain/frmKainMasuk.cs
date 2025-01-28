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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DuaSaudaraFinance.Kain
{
    public partial class frmKainMasuk : Form
    {
        public int IsIn = 1;

        public frmKainMasuk()
        {
            InitializeComponent();
        }

        private void frmKainMasuk_Load(object sender, EventArgs e)
        {

            if (IsIn==1)
            {
                BackColor = Color.FromArgb(152, 251, 152);
                this.Text = "Kain Masuk";
            }
            else
            {
                BackColor = Color.FromArgb(240, 128, 128);
                this.Text = "Kain Keluar";
            }

            FillComboBox1();
            FillComboBox3();
            FillComboBox4();
            FillComboBox5();
        }        

        

        #region SubAndFuction

        
        private void FillComboBox1()
        {

            comboBox1.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmDaftarJenisKain_AddColor_FillComboBox1";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox1.Items.Add(myReader["KodeKain"].ToString());
            }



            CloseReadSqlData(ref myReader);

        }

        private void FillComboBox3()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Meter");
            comboBox3.Items.Add("Yard");
        }

        private void FillComboBox2()
        {

            comboBox2.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmDaftarArusKain_FillListView2 '" + comboBox1.Text + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox2.Items.Add(myReader["KodeWarna"].ToString());

            }

            CloseReadSqlData(ref myReader);

        }

        private void FillComboBox4()
        {
            comboBox4.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_FillComboBox4 ";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox4.Items.Add(myReader["NamaEkspedisi"].ToString());

            }

            CloseReadSqlData(ref myReader);
        }
        
        private void FillComboBox5()
        {
            comboBox5.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_FillComboBox5 " + IsIn;

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox5.Items.Add(myReader["NamaKainArusItem"].ToString());

            }

            CloseReadSqlData(ref myReader);
        }

        private void FillTextBox4()
        {

            textBox4.Text = "";
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_FillTextBox4 '" + comboBox1.Text + "', '" + comboBox2.Text + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            myReader.Read();

            textBox4.Text = myReader["NamaWarna"].ToString();

            CloseReadSqlData(ref myReader);

        }

        private void FillTextBox5()
        {

            if (textBox1.Text != "" && textBox6.Text != "")
            {
                if (Convert.ToInt64(textBox1.Text) > 0 && Convert.ToInt64(textBox6.Text) > 0 && Convert.ToInt64(textBox6.Text) > Convert.ToInt64(textBox1.Text))
                {
                    textBox5.Text = (Convert.ToInt64(textBox6.Text) / Convert.ToInt64(textBox1.Text)).ToString();
                }
            }

        }

        private string CheckKainMasuk() {
            string Kalimat = "";


            if (textBox4.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Kain Tidak Boleh Kosong";
            }

            if (comboBox5.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Jenis Tidak Boleh Kosong";
            }
            else
            {
                if (comboBox5.Text == "Kain Datang")
                {
                    if (comboBox4.Text == "")
                    {
                        if (Kalimat != "")
                        {
                            Kalimat += System.Environment.NewLine;
                        }
                        Kalimat += "Ekspedisi Tidak Boleh Kosong";
                    }

                    if (textBox2.Text == "")
                    {
                        if (Kalimat != "")
                        {
                            Kalimat += System.Environment.NewLine;
                        }
                        Kalimat += "Ongkos kirim Tidak Boleh Kosong";
                    }
                }

                if (comboBox5.Text == "Beli Kain")
                {
                    if (textBox6.Text == "")
                    {
                        if (Kalimat != "")
                        {
                            Kalimat += System.Environment.NewLine;
                        }
                        Kalimat += "Total Tidak Boleh Kosong";
                    }

                    
                }

                //if (comboBox5.Text.Length > 14)
                //{
                //    if (comboBox5.Text.Substring(0, 14) == "Retur Penjahit")
                //    {
                //        if (Kalimat != "")
                //        {
                //            Kalimat += System.Environment.NewLine;
                //        }
                //        Kalimat += "Qty Tidak Boleh Kosong";

                //    }
                //}
                
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


        #endregion

        #region ComboBox

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = "";
            FillComboBox2();
            AssignComboBox3();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTextBox4();
        }

        private void AssignComboBox3()
        {
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_AssignComboBox3 '" + comboBox1.Text + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox3.Text = myReader["Satuan"].ToString();

            }

            CloseReadSqlData(ref myReader);

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = -1;
            textBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            switch (comboBox5.Text)
            {
                case "Kain Datang":
                    panel1.Enabled = true;
                    panel2.Enabled = false;
                    break;
                case "Beli Kain":
                    panel1.Enabled = false;
                    panel2.Enabled = true;
                    break;

                default:
                    panel1.Enabled = false;
                    panel2.Enabled = false;
                    break;
            }
        }


        #endregion

        #region TextBox

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
                    e.Handled = false;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
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
                //case 46:
                //    //Dot Key
                //    e.Handled = false;
                //    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            FillTextBox5();
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

        #region Button
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckKainMasuk();

            if (ResultCheck == "")
            {
                string Harga = "0";
                string Ongkir = "0";
                decimal Qty = 0;

                if (Convert.ToDecimal(textBox1.Text) != 0)
                {
                    if (comboBox2.Text == "Yard")
                    {
                        Qty = Convert.ToDecimal(textBox1.Text) * Convert.ToDecimal(0.9144);
                    }
                    else
                    {
                        Qty = Convert.ToDecimal(textBox1.Text);
                    }
                }

                if (textBox2.Text != "")
                {
                    Ongkir = textBox2.Text;
                }

                if (textBox5.Text != "")
                {
                    Harga = textBox5.Text;
                }

                string Kalimat = "Exec spINSERT_frmKainMasuk_button2_Click ";
                Kalimat += "'" + dateTimePicker1.Value + "', '" + comboBox1.Text + "', '" + comboBox2.Text + "', ";
                Kalimat += "'" + comboBox5.Text + "', " + Qty + ", '" + comboBox3.Text + "', ";
                Kalimat += "'" + comboBox4.Text + "', " + Ongkir + ", " + Harga + ", ";
                Kalimat += "'" + textBox3.Text + "', '" + UserName + "'";


                ExecuteSqlCommand(ref Conn, Kalimat);

                Close();
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FillTextBox5();
        }
    }
}
