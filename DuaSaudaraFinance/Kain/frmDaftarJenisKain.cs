using DuaSaudaraFinance.Administrator;
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

namespace DuaSaudaraFinance.Kain
{
    public partial class frmDaftarJenisKain : Form
    {
        public frmDaftarJenisKain()
        {
            InitializeComponent();
        }

        public void RetrieveCallBack(string _Message)
        {

            if (_Message == "fillListView1")
            {
                
                FillListView1();
            }


        }


        private void button4_Click(object sender, EventArgs e)
        {
            frmDaftarJenisKain_AddColor frmDaftarJenisKain_AddColorInstance = new frmDaftarJenisKain_AddColor();
            frmDaftarJenisKain_AddColorInstance.frmDaftarJenisKainCallback = RetrieveCallBack;
            frmDaftarJenisKain_AddColorInstance.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void PrepareListView1()
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;


            listView1.Columns.Add("KainMerkId", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("ColorId", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Kode Kain", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Kode Warna", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Nama", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Stok Awal", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Stok Akhir", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Satuan", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left);

        }


        private void FillListView1()
        {

            listView1.Items.Clear();
            SqlDataReader myReader = null;
            int _Number = 1;

            string KalimatQuery = "Exec spSELECT_frmDaftarJenisKain_FillListView1 '" + comboBox1.Text + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["KainMerkId"].ToString());
                lvitem.SubItems.Add(myReader["ColorId"].ToString());
                lvitem.SubItems.Add(myReader["KodeKain"].ToString());
                lvitem.SubItems.Add(myReader["KodeWarna"].ToString());
                lvitem.SubItems.Add(myReader["NamaWarna"].ToString());
                if (string.Format("{0:n}", myReader["StockAwal"]) == "0.00")
                {
                    lvitem.SubItems.Add("-");
                }
                else
                {
                    lvitem.SubItems.Add(string.Format("{0:n}", myReader["StockAwal"]));
                }
                if (string.Format("{0:n}", myReader["StockAkhir"]) == "0.00")
                {
                    lvitem.SubItems.Add("-");
                }
                else
                {
                    lvitem.SubItems.Add(string.Format("{0:n}", myReader["StockAkhir"]));
                }
                lvitem.SubItems.Add(myReader["Satuan"].ToString());
                lvitem.SubItems.Add(myReader["Keterangan"].ToString());

                listView1.Items.Add(lvitem);

                _Number++;
            }

            //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            //listView1.Columns[0].Width = 0;
            //listView1.Columns[1].Width = 0;
            //listView1.Columns[2].Width = 100;
            //listView1.Columns[3].Width = 100;
            //listView1.Columns[4].Width = 100;
            //listView1.Columns[5].Width = 100;
            //listView1.Columns[6].Width = 100;
            //listView1.Columns[7].Width = 200;


            CloseReadSqlData(ref myReader);

        }

        private void frmDaftarJenisKain_Load(object sender, EventArgs e)
        {
            FillComboBox1();
            PrepareListView1();
            FillListView1();

        }

        private void FillComboBox1()
        {
            comboBox1.Items.Clear();
            SqlDataReader myReader = null;

            comboBox1.Items.Add("All");

            string KalimatQuery = "Exec spSELECT_frmDaftarJenisKain_AddColor_FillComboBox1";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox1.Items.Add(myReader["KodeKain"].ToString());
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillListView1();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                frmDaftarJenisKain_AddColor frmDaftarJenisKain_AddColorInstance = new frmDaftarJenisKain_AddColor();
                frmDaftarJenisKain_AddColorInstance.IsEdit = 1;
                frmDaftarJenisKain_AddColorInstance.KodeKainId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                frmDaftarJenisKain_AddColorInstance.KodeWarnaId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text);
                frmDaftarJenisKain_AddColorInstance.frmDaftarJenisKainCallback = RetrieveCallBack;
                frmDaftarJenisKain_AddColorInstance.Show();
            }

            
        }
    }
}
