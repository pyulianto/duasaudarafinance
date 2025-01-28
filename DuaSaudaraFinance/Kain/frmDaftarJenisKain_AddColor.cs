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
using System.Xml;
using DuaSaudaraFinance.Administrator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Kain
{
    public partial class frmDaftarJenisKain_AddColor : Form
    {
        public int KodeKainId = 0;
        public int KodeWarnaId = 0;
        public int IsEdit = 0;

        public Action<string> frmDaftarJenisKainCallback { get; set; }

        public frmDaftarJenisKain_AddColor()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmDaftarJenisKain_AddColor_Load(object sender, EventArgs e)
        {
            
            FillComboBox1();
            FillComboBox2();

            if (IsEdit == 1)
            {

                SqlDataReader myReader = null;

                comboBox1.Enabled = false;         

                string KalimatQuery = "Exec spSELECT_frmDaftarJenisKain_AddColor_Load_Edit " + KodeKainId + ", " + KodeWarnaId;

                ReadSqlData(ref Conn, ref myReader, KalimatQuery);
                myReader.Read();

                comboBox1.Text = myReader["KodeKain"].ToString();
                textBox1.Text = myReader["KodeWarna"].ToString();
                textBox2.Text = myReader["NamaWarna"].ToString();
                textBox4.Text = myReader["StokAwal"].ToString();
                textBox3.Text = myReader["Keterangan"].ToString();

                comboBox2.Text = "Meter";

            }
        }

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
        }

        private void FillComboBox2()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Meter");
            comboBox2.Items.Add("Yard");

            comboBox2.SelectedIndex = 1;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string CheckAddColor()
        {
            string Temp = "";

            if (comboBox1.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Kode Kain Tidak Boleh Kosong";
            }

            if (textBox1.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Kode Warna Tidak Boleh Kosong";
            }


            if (textBox2.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Nama Warna Tidak Boleh Kosong";
            }

            if (textBox4.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Stok Awal Tidak Boleh Kosong";


            }

            if (Temp == "")
            {

                SqlDataReader myReader = null;
                string KalimatQuery = "Exec spSELECT_frmDaftarJenisKain_AddColor_CheckAddColor '"+ comboBox1.Text +"','" + textBox1.Text + "', " + KodeWarnaId;

                ReadSqlData(ref Conn, ref myReader, KalimatQuery);
                if (myReader.HasRows)
                {
                    if (Temp != "")
                    {
                        Temp += System.Environment.NewLine;
                    }
                    Temp = "Kain " + comboBox1.Text + "dengan kode warna " + textBox1.Text + " sudah ada di database";
                }
            }

            return Temp;

        }


        private void ResetForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "0";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckAddColor();

            



            if (ResultCheck == "")
            {
                decimal StokAwal = 0;

                if (Convert.ToDecimal(textBox4.Text) != 0)
                {
                    if (comboBox2.Text == "Yard")
                    {
                        StokAwal = Convert.ToDecimal(textBox4.Text) * Convert.ToDecimal(0.9144);
                    }
                    else
                    {
                        StokAwal = Convert.ToDecimal(textBox4.Text);
                    }
                }

                string Kalimat = "Exec spINSERT_frmDaftarJenisKain_AddColor_button2_Click '" + comboBox1.Text +  "', '" 
                    + textBox1.Text + "', '" + textBox2.Text + "', " + StokAwal + ",'" + textBox3.Text + "', " + KodeKainId + ", " + KodeWarnaId;







                ExecuteSqlCommand(ref Conn, Kalimat);

                Application.OpenForms.OfType<frmDaftarJenisKain>().First().RetrieveCallBack("fillListView1");

                if (IsEdit == 0)
                {
                    ResetForm();
                }
                else
                {
                    Close();
                
                }
                


            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
