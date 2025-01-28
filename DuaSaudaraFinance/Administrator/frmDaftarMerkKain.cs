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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Administrator
{
    public partial class frmDaftarMerkKain : Form
    {
        public frmDaftarMerkKain()
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



        private void frmDaftarMerkKain_Load(object sender, EventArgs e)
        {

            PrepareListView1();
            FillListView1();
        }


        private void PrepareListView1()
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;


            listView1.Columns.Add("ID", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("No", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Kode Kain", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Harga", 0, HorizontalAlignment.Right);
            listView1.Columns.Add("Satuan", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Is Active", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Keterangan", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Last Editor", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Last Update", 0, HorizontalAlignment.Left);
        }

        private void FillListView1()
        {

            listView1.Items.Clear();
            SqlDataReader myReader = null;
            int _Number = 1;
            
            string KalimatQuery = "Exec spSELECT_frmDaftarMerkKain_FillListView1";
            
            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["Id"].ToString());
                lvitem.SubItems.Add(_Number.ToString());
                lvitem.SubItems.Add(myReader["KodeKain"].ToString());
                lvitem.SubItems.Add(string.Format("{0:n0}", myReader["HargaDefault"]));
                lvitem.SubItems.Add(myReader["Satuan"].ToString());
                lvitem.SubItems.Add(myReader["IsActive"].ToString());
                lvitem.SubItems.Add(myReader["Keterangan"].ToString());
                lvitem.SubItems.Add(myReader["LastEditor"].ToString());
                lvitem.SubItems.Add(myReader["LastUpdate"].ToString());      

                listView1.Items.Add(lvitem);

                _Number++;
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            listView1.Columns[0].Width = 0;


            CloseReadSqlData(ref myReader);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmDaftarMerkKain_AddMerkKain frmDaftarMerkKain_AddMerkKainInstance = new frmDaftarMerkKain_AddMerkKain();
            frmDaftarMerkKain_AddMerkKainInstance.frmDaftarMerkKainCallback = RetrieveCallBack;
            frmDaftarMerkKain_AddMerkKainInstance.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) != 0)
            {
                frmDaftarMerkKain_AddMerkKain frmDaftarMerkKain_AddMerkKainInstance = new frmDaftarMerkKain_AddMerkKain(Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                frmDaftarMerkKain_AddMerkKainInstance.frmDaftarMerkKainCallback = RetrieveCallBack;
                frmDaftarMerkKain_AddMerkKainInstance.Show();


            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            button3.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Konfirmasi untuk menghapus Merk Kain: " + listView1.SelectedItems[0].SubItems[2].Text + "?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ExecuteSqlCommand(ref Conn, "Exec spDELETE_frmDaftarMerkKain_button2_Click " + listView1.SelectedItems[0].SubItems[0].Text);
                    FillListView1();
                }


            }
        }
    }
}
