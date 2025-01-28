using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;
using DuaSaudaraFinance.Barang;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
using System.Runtime.Remoting.Messaging;

namespace DuaSaudaraFinance.Transaksi
{
    public partial class frmTambahTransaksiPenjualan : Form
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

        public Action<string> TambahTransaksiPenjualanCallback { get; set; }
        public frmTambahTransaksiPenjualan()
        {
            InitializeComponent();
        }

        public void RetrieveCallBack(string _Message)
        {
            string[] strData = _Message.Split('#');
            switch (strData[0])
            {
                case "Enabled":
                    Enabled = true;
                    break;
                case "fillListView1":
                    fillListView1();
                    break;
                default:
                    break;
            }
        }

        public int TransId = 0;
        private void frmTambahTransaksiPenjualan_Load(object sender, EventArgs e)
        {
            PrepareListView1();
            fillComboBox2();
            ExecuteSqlCommand(ref Conn, "Exec spDELETE_frmTambahTransaksi_frmTambahTransaksiPenjualan_Load 'TambahTransaksiPenjualan', '" + UserName + "'");

            if (TransId !=0)
            {
                fillDataTransaksiPenjualan();
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
                FormName = "TambahTransaksiPenjualan",
                MdiParent = MdiParent
            };
            Enabled = false;
            BarangPicker_JasDokter_Instance.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_BajuOK>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_BajuOK>().First().Close();
            }
            frmBarangPicker_BajuOK frmBarangPicker_BajuOK_Instance = new frmBarangPicker_BajuOK
            {
                frmBarangPicker_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_BajuOK_Instance.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Residen>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Residen>().First().Close();
            }
            frmBarangPicker_Residen frmBarangPicker_Residen_Instance = new frmBarangPicker_Residen
            {
                frmBarangPicker_Residen_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Residen_Instance.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Lain>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Lain>().First().Close();
            }
            frmBarangPicker_Lain frmBarangPicker_Lain_Instance = new frmBarangPicker_Lain
            {
                BarangPicker_Lain_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Lain_Instance.Show();
        }

        private void fillDataTransaksiPenjualan()
        {
            SqlDataReader myReader = null;

            string Kalimat = "Exec spINSERT_frmTambahTransaksiPenjualan_fillDataTransaksiPenjualan " + TransId + ", '" + UserName + "'";
            ExecuteSqlCommand(ref Conn, Kalimat);
            
            Kalimat = "Exec spSELECT_frmTambahTransaksiPenjualan_fillDataTransaksiPenjualan " + TransId;
            ReadSqlData(ref Conn, ref myReader, Kalimat);
            myReader.Read();
            textBox3.Text = myReader["NoNota"].ToString();
            dateTimePicker1.Value = myReader.GetDateTime(myReader.GetOrdinal("DateTrans"));
            textBox1.Text = myReader["TransDesc"].ToString();

            foreach (ComboboxValue item in comboBox2.Items)
            {
                if (item.Id == Convert.ToInt32(myReader["PaymentTypeId"]))
                {
                    comboBox2.SelectedItem = item;
                    break; // Exit the loop once the desired item is found
                }
            }
            myReader.Close();

            fillListView1();


        }

        private void fillListView1()
        {
            listView1.Items.Clear();
            SqlDataReader myReader = null;
            int _Number = 1;

            string KalimatQuery = "Exec spSELECT_frmTambahTransaksiPenjualan_fillListView1 '" + UserName + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["ItemType"].ToString());
                lvitem.SubItems.Add(myReader["Id"].ToString());
                lvitem.SubItems.Add(myReader["Details"].ToString());
                lvitem.SubItems.Add(myReader["Size"].ToString());
                lvitem.SubItems.Add(myReader["Qty"].ToString());
                lvitem.SubItems.Add(string.Format("{0:n0}", myReader["Harga"]));
                lvitem.SubItems.Add(string.Format("{0:n0}", myReader["Total"]));

                listView1.Items.Add(lvitem);

                _Number++;
            }

            CloseReadSqlData(ref myReader);

            fillTextBox2();


        }

        private void PrepareListView1()
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ItemType", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Id", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left);
            listView1.Columns.Add("Size", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("Qty", 50, HorizontalAlignment.Right);
            listView1.Columns.Add("Harga", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Total", 90, HorizontalAlignment.Right);
        }

        private void fillTextBox2()
        {


            decimal GrandTotal = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                GrandTotal = GrandTotal + decimal.Parse(item.SubItems[6].Text);
            }

            textBox2.Text = string.Format("{0:n0}", GrandTotal);


        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                DialogResult dialogResult = MessageBox.Show("Konfirmasi untuk menghapus barang : " + listView1.SelectedItems[0].SubItems[2].Text + "?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ExecuteSqlCommand(ref Conn, "Exec spDELETE_frmTambahTransaksiPenjualan_listView1_DoubleClick '" + listView1.SelectedItems[0].SubItems[0].Text + "', " + listView1.SelectedItems[0].SubItems[1].Text);
                    fillListView1();
                }


            }
        }

        private void fillComboBox2()
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_TambahTransaksi_fillComboBox2 1, " + IsOwner);
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["PaymentTypeName"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox2.SelectedIndex = -1;


        }

        private string CheckBarangTambahTransaksiPenjualan()
        {
            string Kalimat = "";

            if (listView1.Items.Count==0)
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Barang Tidak Boleh Kosong";
            }

            if (textBox3.Text == "" || textBox3.Text == "0")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "No Nota Tidak Boleh Kosong";
            }

            if (comboBox2.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Pembayaran Tidak Boleh Kosong";
            }

            return Kalimat;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckBarangTambahTransaksiPenjualan();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmTambahTransaksiPenjualan_button1_Click '";
                Kalimat += (textBox3.Text.ToUpper()).Replace(" ", "") + "', '";
                Kalimat += dateTimePicker1.Value + "', '";
                Kalimat += textBox1.Text + "', ";
                Kalimat += ((ComboboxValue)comboBox2.SelectedItem).Id + ", ";
                Kalimat += "'" + UserName + "', " + TransId;

                ExecuteSqlCommand(ref Conn, Kalimat);

                try
                {
                    TambahTransaksiPenjualanCallback.Invoke("fillListView1");
                }
                catch (Exception)
                {
                    throw;
                }


                Close();
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_BajuLab>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_BajuLab>().First().Close();
            }
            frmBarangPicker_BajuLab frmBarangPicker_BajuLab_Instance = new frmBarangPicker_BajuLab
            {
                frmBarangPicker_BajuLab_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_BajuLab_Instance.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Jubah>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Jubah>().First().Close();
            }
            frmBarangPicker_Jubah frmBarangPicker_Jubah_Instance = new frmBarangPicker_Jubah
            {
                frmBarangPicker_Jubah_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Jubah_Instance.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_BajuPerawat>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_BajuPerawat>().First().Close();
            }
            frmBarangPicker_BajuPerawat frmBarangPicker_BajuPerawat_Instance = new frmBarangPicker_BajuPerawat
            {
                BarangPicker_BajuPerawat_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_BajuPerawat_Instance.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Universal>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Universal>().First().Close();
            }
            frmBarangPicker_Universal frmBarangPicker_Universal_Instance = new frmBarangPicker_Universal
            {
                BarangPicker_Universal_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                NamaBarang = "Baju Korpri",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Universal_Instance.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Universal>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Universal>().First().Close();
            }
            frmBarangPicker_Universal frmBarangPicker_Universal_Instance = new frmBarangPicker_Universal
            {
                BarangPicker_Universal_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                NamaBarang = "Baju Koki",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Universal_Instance.Show();
        }

        private void frmTambahTransaksiPenjualan_FormClosed(object sender, FormClosedEventArgs e)
        {
            TambahTransaksiPenjualanCallback.Invoke("Enabled");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Universal>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Universal>().First().Close();
            }
            frmBarangPicker_Universal frmBarangPicker_Universal_Instance = new frmBarangPicker_Universal
            {
                BarangPicker_Universal_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                NamaBarang = "Jas PDGI",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Universal_Instance.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Universal>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Universal>().First().Close();
            }
            frmBarangPicker_Universal frmBarangPicker_Universal_Instance = new frmBarangPicker_Universal
            {
                BarangPicker_Universal_Callback = RetrieveCallBack,
                FormName = "TambahTransaksiPenjualan",
                NamaBarang = "Jas IDI",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Universal_Instance.Show();
        }
    }
}
