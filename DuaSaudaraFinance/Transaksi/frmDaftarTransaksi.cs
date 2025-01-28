using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;
using System.Data.SqlClient;
using DuaSaudaraFinance.Transaksi;
using DuaSaudaraFinance.Barang;

namespace DuaSaudaraFinance
{
    public partial class frmDaftarTransaksi : Form
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

        public frmDaftarTransaksi()
        {
            InitializeComponent();
        }

        #region Sub And Function

        public void RetrieveCallBack(string _Message)
        {
            switch (_Message)
            {
                case "Enabled":
                    Enabled = true;
                    break;
                case "fillListView1":
                    FillListView1();
                    break;
                default:
                    break;
            }




        }

        public void ExecuteButton(string _ButtonName)
        {
            switch (_ButtonName)
            {
                case "Add Transaksi Masuk":
                    button1.PerformClick();
                    break;
                case "Add Transaksi Keluar":
                    button5.PerformClick();
                    break ;
                default:
                    break;
            }
        }
        private void PrepareListView1()
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;

            
            listView1.Columns.Add("ID", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("No", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("Tanggal", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Nama Transaksi", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Payment", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Masuk", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Keluar", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Keterangan", 500, HorizontalAlignment.Left);
        }
               
        private void FillListView1()
        {
            //listView1.Items.Clear();
            //SqlDataReader myReader = null;
            //ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmPembelian_PreparefrmPembelian '" + Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd") + "'");
            //myReader.Read();

            listView1.Items.Clear();
            SqlDataReader myReader = null;
            int _Number = 1;
            int IsIn;

            //string test = "Exec spSELECT_DaftarTransaksi_fillListView1 '" + Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd") + "', '" + Convert.ToDateTime(dateTimePicker2.Value).ToString("yyyyMMdd") + "', " + IsIn + "," + ((ComboboxValue)comboBox2.SelectedItem).Id;

            switch (comboBox1.Text)
            {
                case "Masuk":
                    IsIn = 1;
                    break;
                case "Keluar":
                    IsIn = 0;
                    break;
                default:
                    IsIn = 2;
                    break;
            }


            string KalimatQuery = "Exec spSELECT_DaftarTransaksi_fillListView1 '" + Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd") + "', '" + Convert.ToDateTime(dateTimePicker2.Value).ToString("yyyyMMdd") + "', " + IsIn + "," + ((ComboboxValue)comboBox2.SelectedItem).Id;
            if (checkBox1.Checked)
            {
                KalimatQuery += ", 1";
            }
            else
            {
                KalimatQuery += ", 0";
            }
            KalimatQuery += ", " + IsOwner;


            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["id"].ToString());
                lvitem.SubItems.Add(_Number.ToString());
                lvitem.SubItems.Add(myReader["DateTrans"].ToString());
                lvitem.SubItems.Add(myReader["TransName"].ToString());
                lvitem.SubItems.Add(myReader["Payment"].ToString());
                if (string.Format("{0:n0}", myReader["Masuk"])=="0")
                {
                    lvitem.SubItems.Add("");
                }
                else
                {
                    lvitem.SubItems.Add(string.Format("{0:n0}", myReader["Masuk"]));
                }

                if (string.Format("{0:n0}", myReader["Keluar"]) == "0")
                {
                    lvitem.SubItems.Add("");
                }
                else
                {
                    lvitem.SubItems.Add(string.Format("{0:n0}", myReader["Keluar"]));
                }
                lvitem.SubItems.Add(myReader["TransDesc"].ToString());

                if (Convert.ToInt32(myReader["Masuk"]) == 0)
                {
                    lvitem.BackColor = Color.Pink;
                }
                else
                {
                    lvitem.BackColor = Color.LightGreen;
                }
                

                listView1.Items.Add(lvitem);

                _Number++;
            }
            CloseReadSqlData(ref myReader);





            FillTextBox1();



        }

        private void FillComboBox1()
        {

            comboBox1.Items.Clear();

            comboBox1.Items.Add("All");
            comboBox1.Items.Add("Masuk");
            comboBox1.Items.Add("Keluar");

            comboBox1.SelectedIndex = 0;


        }
        
        private void FillComboBox2()
        {

            comboBox2.Items.Clear();
            comboBox2.Items.Add(new ComboboxValue(0, "All"));

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_TambahTransaksi_fillComboBox1 2," + IsOwner);
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["TransName"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox2.SelectedIndex = 0;


        }

        private void FillTextBox1()
        {
            SqlDataReader myReader = null;
            string sqlStr;

            sqlStr = "Exec spSELECT_DaftarTransaksi_fillTextBox1 '" + Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd") + "', '" + Convert.ToDateTime(dateTimePicker2.Value).ToString("yyyyMMdd") + "'";
            //if (checkBox2.Checked)
            //{
            //    sqlStr = sqlStr + ", 1";
            //}
            //else
            //{
            //    sqlStr = sqlStr + ", 0";
            //}

            ReadSqlData(ref Conn, ref myReader, sqlStr);
            myReader.Read();
            textBox1.Text = string.Format("{0:n0}", myReader["TotalCashOnly"]);
            textBox2.Text = string.Format("{0:n0}", myReader["TotalNonCash"]);
            CloseReadSqlData(ref myReader);

        }


        #endregion

        #region Button

        private void Button1_Click(object sender, EventArgs e)
        {

            frmTambahTransaksi TambahTransaksiInstance = new frmTambahTransaksi
            {
                TambahTransaksiCallback = RetrieveCallBack,
                IsIn = 1
            };
            TambahTransaksiInstance.Show();
        }




        #endregion

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DaftarTransaksi_Load(object sender, EventArgs e)
        {            
            
            FillComboBox1();
            FillComboBox2();
            PrepareListView1();
            FillListView1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillListView1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Konfirmasi untuk menghapus transaksi no: " + listView1.SelectedItems[0].SubItems[1].Text + "?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ExecuteSqlCommand(ref Conn, "Exec spDELETE_DaftarTransaksi_button2_Click " + listView1.SelectedItems[0].SubItems[0].Text + ", '" + listView1.SelectedItems[0].SubItems[3].Text + "'");
                    FillListView1();
                }

                
            }
            
            
            
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[3].Text == "Penjualan Lunas")
            {
                if (listView1.SelectedItems.Count == 1 && Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) != 0)
                {

                    frmTambahTransaksiPenjualan TambahTransaksiPenjualanInstance = new frmTambahTransaksiPenjualan();
                    TambahTransaksiPenjualanInstance.TambahTransaksiPenjualanCallback = RetrieveCallBack;
                    TambahTransaksiPenjualanInstance.TransId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    TambahTransaksiPenjualanInstance.Show();


                }
            }
            else
            {
                if (listView1.SelectedItems.Count == 1 && Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) != 0)
                {

                    frmTambahTransaksi TambahTransaksiInstance = new frmTambahTransaksi();
                    TambahTransaksiInstance.TambahTransaksiCallback = RetrieveCallBack;
                    TambahTransaksiInstance.TransId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    TambahTransaksiInstance.Show();


                }
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmTambahTransaksi TambahTransaksiInstance = new frmTambahTransaksi();
            TambahTransaksiInstance.TambahTransaksiCallback = RetrieveCallBack;
            TambahTransaksiInstance.IsIn = 0;
            TambahTransaksiInstance.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FillListView1();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmTambahTransaksiPenjualan>().Any())
            {
                Application.OpenForms.OfType<frmTambahTransaksiPenjualan>().First().Close();
            }
            frmTambahTransaksiPenjualan frmTambahTransaksiPenjualan_Instance = new frmTambahTransaksiPenjualan
            {
                TambahTransaksiPenjualanCallback = RetrieveCallBack,
                MdiParent = MdiParent
            };
            Enabled = false;
            frmTambahTransaksiPenjualan_Instance.Show();            
        }
    }
}
