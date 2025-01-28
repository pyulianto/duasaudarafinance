using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Kain
{
    public partial class frmDaftarArusKain : Form
    {
        public frmDaftarArusKain()
        {
            InitializeComponent();

            tabControl1.TabPages[0].Text = "Individual List";
            tabControl1.TabPages[1].Text = "Filtering List";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDaftarArusKain_Load(object sender, EventArgs e)
        {
            PrepareListView1();
            PrepareListView2();
            FillListBox1();

            FillComboBox1();
            FillComboBox2();
        }

        private void FillListBox1()
        {

            listBox1.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmDaftarJenisKain_AddColor_FillComboBox1";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                listBox1.Items.Add(myReader["KodeKain"].ToString());
            }



            CloseReadSqlData(ref myReader);

        }

        private void FillListView2()
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                listView2.Items.Clear();
                SqlDataReader myReader = null;
                int _Number = 1;

                string KalimatQuery = "Exec spSELECT_frmDaftarArusKain_FillListView2 '" + listBox1.SelectedItems[0] + "'";

                ReadSqlData(ref Conn, ref myReader, KalimatQuery);
                while (myReader.Read())
                {



                    ListViewItem lvitem = new ListViewItem(myReader["Id"].ToString());
                    lvitem.SubItems.Add(myReader["KodeWarna"].ToString());
                    lvitem.SubItems.Add(myReader["NamaWarna"].ToString());

                    listView2.Items.Add(lvitem);

                    _Number++;



                }
                listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView2.Columns[0].Width = 0;

                CloseReadSqlData(ref myReader);
            }


        }


        private void FillComboBox1()
        {

            comboBox1.Items.Clear();
            comboBox1.Items.Add("All");

            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_FillComboBox5 0";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox1.Items.Add(myReader["NamaKainArusItem"].ToString());
            }

            CloseReadSqlData(ref myReader);

            comboBox1.SelectedIndex = 0;

        }

        private void FillComboBox2()
        {

            comboBox2.Items.Clear();
            comboBox2.Items.Add("All");

            //SqlDataReader myReader = null;

            //string KalimatQuery = "Exec spSELECT_frmKainMasuk_FillComboBox5 0";

            //ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            //while (myReader.Read())
            //{
            //    comboBox2.Items.Add(myReader["NamaKainArusItem"].ToString());
            //}

            //CloseReadSqlData(ref myReader);

            comboBox2.SelectedIndex = 0;

        }


        private void PrepareListView1()
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;


            listView1.Columns.Add("KainArusId", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Tanggal", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Masuk", 50, HorizontalAlignment.Right);
            listView1.Columns.Add("Keluar", 50, HorizontalAlignment.Right);
            listView1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Keterangan 2", 200, HorizontalAlignment.Left);
        }

        private void PrepareListView2()
        {
            listView2.View = View.Details;
            listView2.Clear();
            listView2.FullRowSelect = true;


            listView2.Columns.Add("MerkWarnaId", 0, HorizontalAlignment.Left);
            listView2.Columns.Add("Kode", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("Warna", 100, HorizontalAlignment.Left);

        }


        private void FillListView1()
        {
            if (listView2.SelectedItems.Count == 1)
            {

                
                listView1.Items.Clear();
                SqlDataReader myReader = null;
                int _Number = 1;
                decimal _TotalAmount = 0;

                string KalimatQuery = "Exec spSELECT_frmDaftarArusKain_FillListView1 " + listView2.SelectedItems[0].SubItems[0].Text + ", 1";

                ReadSqlData(ref Conn, ref myReader, KalimatQuery);
                while (myReader.Read())
                {
                    ListViewItem lvitem = new ListViewItem(myReader["Id"].ToString());
                    lvitem.SubItems.Add(myReader["Tanggal"].ToString());
                    if (myReader["Masuk"].ToString() == "0")
                    {
                        lvitem.SubItems.Add("");
                    }
                    else
                    {
                        //lvitem.SubItems.Add(string.Format("{0:n}", myReader["Masuk"]));
                        lvitem.SubItems.Add(Convert.ToDecimal(myReader["Masuk"]).ToString("0.##"));

                        _TotalAmount += Convert.ToDecimal(myReader["Masuk"]);
                    }

                    if (myReader["Keluar"].ToString() == "0")
                    {
                        lvitem.SubItems.Add("");
                    }
                    else
                    {
                        //lvitem.SubItems.Add(string.Format("{0:n}", myReader["Keluar"]));
                        lvitem.SubItems.Add(Convert.ToDecimal(myReader["Keluar"]).ToString("0.##"));
                        _TotalAmount -= Convert.ToDecimal(myReader["Keluar"]);
                    }

                    lvitem.SubItems.Add(myReader["Keterangan"].ToString());
                    lvitem.SubItems.Add(myReader["Keterangan2"].ToString());

                    listView1.Items.Add(lvitem);

                    _Number++;
                }


                CloseReadSqlData(ref myReader);

                label1.Text = listBox1.SelectedItems[0].ToString() 
                    + " - " + listView2.SelectedItems[0].SubItems[1].Text 
                    + " - " + listView2.SelectedItems[0].SubItems[2].Text
                    + " | Stock = " + string.Format("{0:n}", _TotalAmount);


            }


            

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Konfirmasi untuk menghapus Arus Kain tanggal: " + listView1.SelectedItems[0].SubItems[1].Text + "?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ExecuteSqlCommand(ref Conn, "Exec spDELETE_frmDaftarArusKain_button2_Click " + listView1.SelectedItems[0].SubItems[0].Text);
                    FillListView1();
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmKainMasuk>().Any())
            {
                Application.OpenForms.OfType<frmKainMasuk>().First().Close();
            }

            frmKainMasuk frmKainMasukInstance = new frmKainMasuk();
            frmKainMasukInstance.IsIn = 1;
            frmKainMasukInstance.MdiParent = this.MdiParent;
            frmKainMasukInstance.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmKainMasuk>().Any())
            {
                Application.OpenForms.OfType<frmKainMasuk>().First().Close();
            }

            frmKainMasuk frmKainMasukInstance = new frmKainMasuk();
            frmKainMasukInstance.IsIn = 0;
            frmKainMasukInstance.MdiParent = this.MdiParent;
            frmKainMasukInstance.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillListView1();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            label1.Text = "N/A";
            FillListView2();
        }

        private void listView2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillListView1();
        }
    }
}
