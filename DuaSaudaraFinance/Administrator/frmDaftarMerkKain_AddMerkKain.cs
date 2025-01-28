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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Administrator
{
    public partial class frmDaftarMerkKain_AddMerkKain : Form
    {
        public Action<string> frmDaftarMerkKainCallback { get; set; }
        private int MerkKainId = 0;
        public frmDaftarMerkKain_AddMerkKain()
        {
            InitializeComponent();
        }

        public frmDaftarMerkKain_AddMerkKain(int _MerkKainId)
        {
            InitializeComponent();
            MerkKainId = _MerkKainId;

            fillForm();
        }

        private string CheckAddMerkKain()
        {
            string Temp = "";

            if (textBox1.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Kode Tidak Boleh Kosong";
            }


            if (textBox2.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Harga Tidak Boleh Kosong";
            }           

            return Temp;

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 13:
                    //Enter Key
                    button2.PerformClick();
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

        private void button2_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckAddMerkKain();

            if (ResultCheck == "")
            {
                string Kalimat = "";
                if (MerkKainId == 0)
                {


                    Kalimat = "Exec spINSERT_frmDaftarMerkKain_AddMerkKain_button2_Click '" +
                        textBox1.Text + "'," +
                        textBox2.Text + ",";

                    if (radioButton1.Checked)
                    {
                        Kalimat = Kalimat + "'Meter',";
                    }
                    else
                    {
                        Kalimat = Kalimat + "'Yard',";
                    }


                    if (checkBox1.Checked)
                    {
                        Kalimat = Kalimat + "1,";
                    }
                    else
                    {
                        Kalimat = Kalimat + "0,";
                    }

                    Kalimat = Kalimat + "'" + textBox3.Text + "', ";

                    Kalimat = Kalimat + "'" + UserName + "'";
                }
                else
                {
                    Kalimat = "Exec spUPDATE_frmDaftarMerkKain_AddMerkKain_button2_Click '" +
                        textBox1.Text + "'," +
                        textBox2.Text + ",";

                    if (radioButton1.Checked)
                    {
                        Kalimat = Kalimat + "'Meter',";
                    }
                    else
                    {
                        Kalimat = Kalimat + "'Yard',";
                    }


                    if (checkBox1.Checked)
                    {
                        Kalimat = Kalimat + "1,";
                    }
                    else
                    {
                        Kalimat = Kalimat + "0,";
                    }

                    Kalimat = Kalimat + "'" + textBox3.Text + "', ";

                    Kalimat = Kalimat + "'" + UserName + "', " + MerkKainId;
                }

                ExecuteSqlCommand(ref Conn, Kalimat);

                Application.OpenForms.OfType<frmDaftarMerkKain>().First().RetrieveCallBack("fillListView1");


                ResetForm();

                if (MerkKainId != 0)
                {

                    Close();
                }
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void fillForm()
        {

            SqlDataReader myReader = null;
            string sqlStr;

            sqlStr = "Exec spSELECT_frmDaftarMerkKain_AddMerkKain_fillForm " + MerkKainId;            

            ReadSqlData(ref Conn, ref myReader, sqlStr);
            myReader.Read();

            textBox1.Text = myReader["KodeKain"].ToString();
            textBox2.Text = Convert.ToDouble(myReader["HargaDefault"]).ToString();
            textBox3.Text = myReader["Keterangan"].ToString();
            if (myReader["Satuan"].ToString() == "Meter")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            if (myReader["IsActive"].ToString() == "1")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }



            CloseReadSqlData(ref myReader);

        }

        private void ResetForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            radioButton1.Checked = true;
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDaftarMerkKain_AddMerkKain_Load(object sender, EventArgs e)
        {

        }
    }

}
