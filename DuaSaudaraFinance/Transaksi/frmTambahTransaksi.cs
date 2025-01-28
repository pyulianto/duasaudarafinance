using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;
using System.Runtime.Remoting.Messaging;
using DuaSaudaraFinance.Barang;
using DuaSaudaraFinance.Model;

namespace DuaSaudaraFinance
{
    public partial class frmTambahTransaksi : Form
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
                
        public Action<string> TambahTransaksiCallback { get; set; }
        public int IsIn { get; set; }
        public int TransId = 0;
        private bool isLoad = true;
        public frmTambahTransaksi()
        {
            InitializeComponent();
        }
        private void TambahTransaksi_Load(object sender, EventArgs e)
        {
            if (IsOwner==0)
            {
                textBox3.Enabled = false;
                button3.Enabled = false;
            }

            if (TransId!=0)            
            {
                SqlDataReader myReader = null;
                ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_TambahTransaksi_TambahTransaksi_Load " + TransId);
                myReader.Read();
                dateTimePicker1.Value = Convert.ToDateTime(myReader["DateTrans"]);


                IsIn = Convert.ToInt32(myReader["IsIn"]);

                fillComboBox1();
                fillComboBox2();

                comboBox1.SelectedIndex = GetComboBox1Index(Convert.ToInt32(myReader["TransItemId"]));
                comboBox2.SelectedIndex = GetComboBox2Index(Convert.ToInt32(myReader["PaymentTypeId"]));
                textBox1.Text = myReader["TransDesc"].ToString();
                textBox2.Text = Convert.ToDouble(myReader["Amount"]).ToString();

                CloseReadSqlData(ref myReader);

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;


            }
            else
            {
                fillComboBox1();
                fillComboBox2();
            }

            ChangeTransactionType();

            isLoad = false;         
        }

        #region Sub And Function
        public void RetrieveCallBack(string _Message)
        {
            string[] strData = _Message.Split('#');
            switch (strData[0])
            {
                case "Enabled":
                    Enabled = true;
                    break;
                case "fillTextBox3":
                    fillTextBox3(strData[1]);
                    break;
                default:
                    break;
            }
        }
        private void fillTextBox3(string ItemType)
        {
            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_TambahTransaksi_fillTextBox3 '" + ItemType + "', '" + UserName + "'");
            myReader.Read();
            dateTimePicker1.Value = Convert.ToDateTime(myReader["DateTrans"]);
        }
        private void fillComboBox1()
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_TambahTransaksi_fillComboBox1 " + IsIn + ", " + IsOwner);
            while (myReader.Read())
            {
                comboBox1.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["TransName"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox1.SelectedIndex = -1;


        }
        private void fillComboBox2()
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_TambahTransaksi_fillComboBox2 " + IsIn + ", " + IsOwner);
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["PaymentTypeName"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox2.SelectedIndex = -1;


        }
        private int GetComboBox1Index(int TempId)
        {
            int ResultIndex = 0;

            foreach (ComboboxValue item in comboBox1.Items)
            {
                if (item.Id == TempId)
                {
                    return comboBox1.Items.IndexOf(item); 
                }
            }

            return ResultIndex;

        }
        private int GetComboBox2Index(int TempId)
        {
            int ResultIndex = 0;

            foreach (ComboboxValue item in comboBox2.Items)
            {
                if (item.Id == TempId)
                {
                    return comboBox2.Items.IndexOf(item);
                }
            }

            return ResultIndex;

        }
        private string CheckTambahTransaksi()
        {
            string Temp = "";

            if (textBox2.Text=="")
            {
                if (Temp!="")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Nilai Tidak Boleh Kosong";
            }


            if (comboBox1.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Jenis Tidak Boleh Kosong";
            }


            if (comboBox2.Text == "")
            {
                if (Temp != "")
                {
                    Temp += System.Environment.NewLine;
                }
                Temp += "Pembayaran Tidak Boleh Kosong";
            }

            return Temp;

        }
        private void ChangeTransactionType()
        {
            if (IsIn == 1)
            {
                BackColor = Color.FromArgb(152, 251, 152);
                radioButton1.Checked = true;
                Icon = Properties.Resources.login_128;
            }
            else
            {
                BackColor = Color.FromArgb(240, 128, 128);
                radioButton2.Checked = true;
                Icon = Properties.Resources.logout_128;
            }
        }
        #endregion

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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoad==false)
            {

            
                if (radioButton1.Checked==true)
                {                    
                    IsIn = 1;
                }
                else
                {                    
                    IsIn = 0;
                }

                ChangeTransactionType();
                fillComboBox1();
                fillComboBox2();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckTambahTransaksi();

            if (ResultCheck == "")
            {

                ExecuteSqlCommand(ref Conn, "Exec spINSERT_TambahTransaksi_button1_Click '" + dateTimePicker1.Value + "'," + ((ComboboxValue)comboBox1.SelectedItem).Id + "," + textBox2.Text + ",'" + textBox1.Text + "', " + TransId + ", " + ((ComboboxValue)comboBox2.SelectedItem).Id + ", '" + UserName + "'");
                if (TambahTransaksiCallback != null)
                {
                    TambahTransaksiCallback.Invoke("fillListView1");
                }
                
                

                textBox1.Text = "";
                textBox2.Text = "";


                if (Application.OpenForms.OfType<frmDaftarTransaksi>().Any())
                {
                    Application.OpenForms.OfType<frmDaftarTransaksi>().First().RetrieveCallBack("fillListView1");
                }



                if (TransId != 0)
                {
                    Close();

                }



            }
            else
            {
                MessageBox.Show(ResultCheck, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Penjualan Jas Dokter":

                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_JasDokter>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_JasDokter>().First().Close();
            }
            frmBarangPicker_JasDokter BarangPicker_JasDokter_Instance = new frmBarangPicker_JasDokter
            {
                BarangPicker_JasDokter_Callback = RetrieveCallBack,
                IsCowo = 1,
                FormName = "TambahTransaksi",
            };
            BarangPicker_JasDokter_Instance.MdiParent = MdiParent;
            Enabled = false;
            BarangPicker_JasDokter_Instance.Show();
        }
    }
}
