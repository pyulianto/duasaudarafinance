using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangMasuk : Form
    {
        public Action<string> frmBarangMasukCallback { get; set; }

        public frmBarangMasuk()
        {
            InitializeComponent();
        }

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
                get { return Id; }
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public void RetrieveCallBack(string _Message)
        {
            switch (_Message)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_JasDokter>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_JasDokter>().First().Close();
            }
            frmBarangPicker_JasDokter BarangPicker_JasDokter_Instance = new frmBarangPicker_JasDokter
            {
                BarangPicker_JasDokter_Callback = RetrieveCallBack,
                IsCowo = 1,
                FormName = "BarangMasuk",
            };
            BarangPicker_JasDokter_Instance.MdiParent = MdiParent;
            Enabled = false;
            BarangPicker_JasDokter_Instance.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_JasDokter>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_JasDokter>().First().Close();
            }
            frmBarangPicker_JasDokter BarangPicker_JasDokter_Instance = new frmBarangPicker_JasDokter
            {
                BarangPicker_JasDokter_Callback = RetrieveCallBack,
                IsCowo = 0,
                FormName = "BarangMasuk"
            };
            BarangPicker_JasDokter_Instance.MdiParent = MdiParent;
            Enabled = false;
            BarangPicker_JasDokter_Instance.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Lain>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Lain>().First().Close();
            }


            frmBarangPicker_Lain BarangPicker_Lain_Instance = new frmBarangPicker_Lain
            {
                BarangPicker_Lain_Callback = RetrieveCallBack,
                FormName = "BarangMasuk"
            };
            BarangPicker_Lain_Instance.MdiParent = MdiParent;
            Enabled = false;
            BarangPicker_Lain_Instance.Show();

        }



        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmBarangMasuk_Load(object sender, EventArgs e)
        {
            PrepareForm();
        }

        private void PrepareForm()
        {
            ExecuteSqlCommand(ref Conn, "Exec spDELETE_frmBarangMasuk_PrepareForm '" + UserName + "'");

            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;

            listView1.Columns.Add("NamaTable", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("ID", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left);
            listView1.Columns.Add("Size", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Qty", 50, HorizontalAlignment.Right);
            listView1.Columns.Add("Harga", 80, HorizontalAlignment.Right);


            fillComboBox1();
            fillComboBox2();
        }

        private void fillListView1()
        {
            listView1.Items.Clear();
            SqlDataReader myReader = null;
            int _Number = 1;

            string KalimatQuery = "Exec spSELECT_frmBarangMasuk_fillListView1 '" + UserName + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["NamaTable"].ToString());
                lvitem.SubItems.Add(myReader["Id"].ToString());
                lvitem.SubItems.Add(myReader["NamaBarang"].ToString());
                lvitem.SubItems.Add(myReader["Size"].ToString());
                lvitem.SubItems.Add(myReader["Qty"].ToString());
                lvitem.SubItems.Add(string.Format("{0:n0}", myReader["Harga"]));

                listView1.Items.Add(lvitem);

                _Number++;
            }

            CloseReadSqlData(ref myReader);


            fillTextBox2();
        }

        private void fillTextBox2()
        {


            if (listView1.Items.Count > 0)
            {
                int TotalQty = 0;
                decimal TotalPrice = 0;

                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    TotalQty += Convert.ToInt32(listView1.Items[i].SubItems[4].Text);
                    TotalPrice += Convert.ToDecimal(listView1.Items[i].SubItems[5].Text)*Convert.ToDecimal(listView1.Items[i].SubItems[4].Text);
                }

                textBox2.Text = TotalQty.ToString();
                textBox5.Text = string.Format("{0:n0}", TotalPrice);
            }
            else
            {
                textBox2.Text = "0";
            }




        }

        private void fillComboBox1()
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangMasuk_fillComboBox1");
            while (myReader.Read())
            {
                comboBox1.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]),
                    myReader["NamaSupplier"].ToString()));
            }

            CloseReadSqlData(ref myReader);

            comboBox1.SelectedIndex = -1;
        }
        
        private void fillComboBox2()
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangMasuk_fillComboBox2");
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]),
                    myReader["NamaEkspedisi"].ToString()));
            }

            CloseReadSqlData(ref myReader);

            comboBox1.SelectedIndex = -1;
        }


        private string CheckBarangMasuk()
        {
            var kalimat = "";

            if (comboBox1.Text == "")
            {
                if (kalimat != "")
                {
                    kalimat += System.Environment.NewLine;
                }

                kalimat += "Supplier Tidak Boleh Kosong";
            }

            if (comboBox2.Text == "")
            {
                if (kalimat != "")
                {
                    kalimat += System.Environment.NewLine;
                }

                kalimat += "Ekspedisi Tidak Boleh Kosong";
            }


            if (listView1.Items.Count == 0)
            {
                if (kalimat != "")
                {
                    kalimat += System.Environment.NewLine;
                }

                kalimat += "Barang Tidak Boleh Kosong";
            }

            return kalimat;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var kalimat = "";

                if (listView1.SelectedItems[0].SubItems[3].Text == "N/A")
                {
                    kalimat = "Konfirmasi untuk menghapus barang : " + listView1.SelectedItems[0].SubItems[2].Text +
                              "/" + listView1.SelectedItems[0].SubItems[4].Text + "?";
                }
                else
                {
                    kalimat = "Konfirmasi untuk menghapus barang : " + listView1.SelectedItems[0].SubItems[2].Text +
                              " " +
                              listView1.SelectedItems[0].SubItems[3].Text + "/" +
                              listView1.SelectedItems[0].SubItems[4].Text + "?";
                }

                DialogResult dialogResult = MessageBox.Show(kalimat, "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ExecuteSqlCommand(ref Conn,
                        "Exec spDELETE_frmBarangMasuk_button3_Click '" + listView1.SelectedItems[0].SubItems[0].Text +
                        "', " + listView1.SelectedItems[0].SubItems[1].Text + ", '" + UserName + "'");
                    fillListView1();
                }
            }
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            string resultCheck = CheckBarangMasuk();

            if (resultCheck == "")
            {
                if (textBox3.Text=="")
                {
                    textBox3.Text = "0";
                }

                string kalimat = "Exec spINSERT_frmBarangMasuk_Button6_Click ";

                kalimat += "'" + dateTimePicker1.Value + "', " + ((ComboboxValue)comboBox1.SelectedItem).Id + ", " +
                           ((ComboboxValue)comboBox2.SelectedItem).Id + ", " + textBox3.Text + ", 1, '" + textBox1.Text + "', '" + UserName + "', '" + textBox4.Text + "'";

                ExecuteSqlCommand(ref Conn, kalimat);

                if (frmBarangMasukCallback != null)
                {
                    frmBarangMasukCallback.Invoke("fillListView1");
                }

                //BarangPicker_JasDokter_Callback.Invoke("fillListView1");
                Close();
            }
            else
            {
                MessageBox.Show(resultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBarangMasuk_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmBarangMasukCallback != null)
            {
                frmBarangMasukCallback.Invoke("enable");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Residen>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Residen>().First().Close();
            }
            frmBarangPicker_Residen frmBarangPicker_Residen_Instance = new frmBarangPicker_Residen
            {
                frmBarangPicker_Residen_Callback = RetrieveCallBack,
                FormName = "BarangMasuk"
            };
            frmBarangPicker_Residen_Instance.MdiParent = MdiParent;
            Enabled = false;
            frmBarangPicker_Residen_Instance.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(Application.OpenForms.OfType<frmBarangPicker_BajuOK>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_BajuOK>().First().Close();
            }
            frmBarangPicker_BajuOK frmBarangPicker_BajuOK_Instance = new frmBarangPicker_BajuOK
            {
                frmBarangPicker_Callback = RetrieveCallBack,
                FormName = "BarangMasuk",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_BajuOK_Instance.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangPicker_Universal>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_Universal>().First().Close();
            }
            frmBarangPicker_Universal frmBarangPicker_Universal_Instance = new frmBarangPicker_Universal
            {
                BarangPicker_Universal_Callback = RetrieveCallBack,
                FormName = "BarangMasuk",
                NamaBarang = "Jas IDI",
                MdiParent = MdiParent
            };
            Enabled = false;
            frmBarangPicker_Universal_Instance.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //if (Application.OpenForms.OfType<FrmBarangMasuk_SmartInput>().Any())
            //{
            //    Application.OpenForms.OfType<FrmBarangMasuk_SmartInput>().First().Close();
            //}
            //FrmBarangMasuk_SmartInput frmBarangMasuk_SmartInput_Instance = new FrmBarangMasuk_SmartInput
            //{
            //    FrmBarangMasuk_SmartInput_Callback = RetrieveCallBack,
            //    MdiParent = MdiParent
            //};
            //Enabled = false;
            //frmBarangMasuk_SmartInput_Instance.Show();

            if (Application.OpenForms.OfType<frmBarangPicker_JasDokter>().Any())
            {
                Application.OpenForms.OfType<frmBarangPicker_JasDokter>().First().Close();
            }
            frmBarangPicker_JasDokter BarangPicker_JasDokter_Instance = new frmBarangPicker_JasDokter
            {
                BarangPicker_JasDokter_Callback = RetrieveCallBack,
                IsCowo = 1,
                FormName = "BarangMasuk",
            };

            BarangPicker_JasDokter_Instance.IsSmart = 1;
            BarangPicker_JasDokter_Instance.MdiParent = MdiParent;
            Enabled = false;
            BarangPicker_JasDokter_Instance.Show();


        }
    }
}
