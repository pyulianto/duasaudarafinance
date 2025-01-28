using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Kain
{
    public partial class frmDaftarSuratPerintahKerja : Form
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

        public frmDaftarSuratPerintahKerja()
        {
            InitializeComponent();
        }

        private void FrmDaftarSuratPerintahKerja_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ID", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Tanggal", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("No SPK", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Penjahit", 130, HorizontalAlignment.Left);

            listView2.View = View.Details;
            listView2.Clear();
            listView2.FullRowSelect = true;

            listView2.Columns.Add("Kain", 200, HorizontalAlignment.Left);
            listView2.Columns.Add("Qty", 50, HorizontalAlignment.Left);
            listView2.Columns.Add("Satuan", 50, HorizontalAlignment.Left);

            FillComboBox2();
        }

        private void FillComboBox2()
        {
            comboBox2.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmSuratPerintahKerja_FillComboBox5";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox2.Items.Add(myReader["NamaPenjahit"].ToString());

            }

            CloseReadSqlData(ref myReader);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmAddSuratPerintahKerja>().Any())
            {
                Application.OpenForms.OfType<frmAddSuratPerintahKerja>().First().Close();
            }

            frmAddSuratPerintahKerja frmSuratPerintahKerjaInstance = new frmAddSuratPerintahKerja();
            frmSuratPerintahKerjaInstance.MdiParent = this.MdiParent;
            frmSuratPerintahKerjaInstance.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillListview1();
        }

        private void FillListview1()
        {
            listView1.Items.Clear();
            SqlDataReader myReader = null;
            int _Number = 1;

            string KalimatQuery = "Exec spSELECT_frmSuratPerintahKerja_FillListview1 ";
            KalimatQuery = KalimatQuery + "'" + dateTimePicker1.Value.ToString("yyyyMMdd") + "', ";
            KalimatQuery = KalimatQuery + "'" + dateTimePicker2.Value.ToString("yyyyMMdd") + "', ";
            if (textBox2.Text.Length > 0)
            {
                KalimatQuery = KalimatQuery + textBox2.Text + ", ";
            }
            else
            {
                KalimatQuery = KalimatQuery + "0, ";
            }
            if (comboBox2.Text.Length > 0)
            {
                KalimatQuery = KalimatQuery + ((ComboboxValue)comboBox2.SelectedItem).Id;
            }
            else
            {
                KalimatQuery = KalimatQuery + "0";
            }
            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["Id"].ToString());
                lvitem.SubItems.Add(myReader["Tanggal"].ToString());
                lvitem.SubItems.Add(myReader["NoSPK"].ToString());
                lvitem.SubItems.Add(myReader["NamaPenjahit"].ToString());
                listView1.Items.Add(lvitem);

                _Number++;
            }

            CloseReadSqlData(ref myReader);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                FillListview2();
                FillTextBox3();
            }
        }


        private void FillListview2()
        {
            listView2.Items.Clear();
            SqlDataReader myReader = null;
            int _Number = 1;

            string KalimatQuery = "Exec spSELECT_frmSuratPerintahKerja_FillListview2 ";
            KalimatQuery = KalimatQuery + Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["Kain"].ToString());
                lvitem.SubItems.Add(string.Format("{0:n}", myReader["Qty"]));
                lvitem.SubItems.Add(myReader["Satuan"].ToString());
                listView2.Items.Add(lvitem);

                _Number++;
            }

            CloseReadSqlData(ref myReader);
        }

        private void FillTextBox3()
        {
            textBox3.Text = string.Empty;

            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmSuratPerintahKerja_FillTextBox3 ";
            KalimatQuery = KalimatQuery + Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            myReader.Read();
            textBox3.Text = myReader["Catatan"].ToString();

            CloseReadSqlData(ref myReader);
        }


    }
}
