using DuaSaudaraFinance.Model;
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
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmDaftarBarangMasuk : Form
    {
        List<BarangItem> ListOfItems = new List<BarangItem>();
        List<ItemSimplified> ListOfItemsSimplified = new List<ItemSimplified>();

        public frmDaftarBarangMasuk()
        {
            InitializeComponent();
        }
        public void RetrieveCallBack(string _Message)
        {
            switch (_Message)
            {
                case "fillListView1":
                    FillListView1();
                    break;
                case "enable":
                    Enabled = true;
                    break;
                
            }
        }
        private void frmDaftarBarangMasuk_Load(object sender, EventArgs e)
        {
            PreparefrmDaftarBarangMasuk();
            FillListView1();
        }

        private void PreparefrmDaftarBarangMasuk()
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ID", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("Tgl Masuk", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Inv No", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Supplier", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Qty", 50, HorizontalAlignment.Right);
            listView1.Columns.Add("Harga", 75, HorizontalAlignment.Right);
            listView1.Columns.Add("Ket", 0, HorizontalAlignment.Right);
            
            listView2.View = View.Details;
            listView2.Clear();
            listView2.FullRowSelect = true;

            listView2.Columns.Add("JenisBarangId", 0, HorizontalAlignment.Left);
            listView2.Columns.Add("Nama Barang", 275, HorizontalAlignment.Left);
            listView2.Columns.Add("Size", 50, HorizontalAlignment.Left);
            listView2.Columns.Add("Qty", 50, HorizontalAlignment.Right);
            listView2.Columns.Add("Harga", 100, HorizontalAlignment.Right);

        }

        private void FillListView1()
        {
            listView1.Items.Clear();
            SqlDataReader myReader = null;
            
            string kalimatQuery = "Exec spSELECT_frmDaftarBarangMasuk_fillListView1 '" + Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd") + "', '" + Convert.ToDateTime(dateTimePicker2.Value).ToString("yyyyMMdd") + "'";
            int _Number = 0;
            
            ReadSqlData(ref Conn, ref myReader, kalimatQuery);
            while (myReader.Read())
            {
                ListViewItem lvitem = new ListViewItem(myReader["id"].ToString());
                lvitem.SubItems.Add(myReader["Tanggal"].ToString());
                lvitem.SubItems.Add(myReader["InvoiceNo"].ToString());
                lvitem.SubItems.Add(myReader["NamaSupplier"].ToString());
                lvitem.SubItems.Add(myReader["Qty"].ToString());
                lvitem.SubItems.Add(string.Format("{0:n0}", myReader["Harga"]));
                lvitem.SubItems.Add(myReader["Keterangan"].ToString());

                listView1.Items.Add(lvitem);
                _Number++;
            }
            CloseReadSqlData(ref myReader);
        }
        
        private void FillListView2()
        {

            SqlDataReader myReader = null;
            SqlDataReader myReader2 = null;

            ListOfItems.Clear();
            ListOfItemsSimplified.Clear();

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblPenerimaanBarangItem WHERE PenerimaanBarangId = " + listView1.SelectedItems[0].SubItems[0].Text);
            while (myReader.Read())
            {
                switch (Convert.ToInt32(myReader["JenisBarangId"]))
                {
                    case 1:
                        ReadSqlData(ref Conn, ref myReader2, "SELECT * FROM tblBarangJasDokter WHERE Id = " + Convert.ToInt32(myReader["BarangId"]));
                        myReader2.Read();
                        ListOfItems.Add(new Model.JasDokter(
                            ListSex.FirstOrDefault(sex => sex.Id == Convert.ToInt32(myReader2["JenisKelamin"])),
                            ListAttributeLengan.FirstOrDefault(lengan => lengan.Id == Convert.ToInt32(myReader2["Lengan"])),
                            ListAttributeJasDokterBahan.FirstOrDefault(bahan => bahan.Id == Convert.ToInt32(myReader2["Bahan"])),
                            ListAttributeSize.FirstOrDefault(size => size.Id == Convert.ToInt32(myReader2["Size"])),
                            Convert.ToDecimal(myReader["Harga"]),
                            Convert.ToInt16(myReader["Qty"])
                        ));
                        CloseReadSqlData(ref myReader2);
                        break;
                    case 2:
                        ReadSqlData(ref Conn, ref myReader2, "SELECT * FROM tblBarangResiden WHERE Id = " + Convert.ToInt32(myReader["BarangId"]));
                        myReader2.Read();
                        ListOfItems.Add(new Model.BajuResiden(
                            ListSex.FirstOrDefault(sex => sex.Id == Convert.ToInt32(myReader2["JenisKelamin"])),
                            ListAttributeSize.FirstOrDefault(size => size.Id == Convert.ToInt32(myReader2["Size"])),
                            ListAttributeResidenSetBarang.FirstOrDefault(residensetbarang => residensetbarang.Id == Convert.ToInt32(myReader2["NamaSetBarang"])),
                            Convert.ToDecimal(myReader["Harga"]),
                            Convert.ToInt16(myReader["Qty"]),
                            myReader["Remark"].ToString()
                        ));
                        CloseReadSqlData(ref myReader2);
                        break;

                    case 3:
                        ReadSqlData(ref Conn, ref myReader2, "SELECT * FROM tblBarangBajuOK WHERE Id = " + Convert.ToInt32(myReader["BarangId"]));
                        myReader2.Read();
                        ListOfItems.Add(new Model.BajuOK(
                            ListAttributeBajuOKSet.FirstOrDefault(bajuOKSet => bajuOKSet.Id == Convert.ToInt32(myReader2["AttributeBajuOKSetId"])),
                            ListAttributeBajuOKJenis.FirstOrDefault(bajuOKJenis => bajuOKJenis.Id == Convert.ToInt32(myReader2["AttributeBajuOKJenisId"])),
                            ListAttributeSize.FirstOrDefault(size => size.Id == Convert.ToInt32(myReader2["AttributeSizeId"])),
                            ListAttributeBajuOKWarna.FirstOrDefault(bajuOKWarna => bajuOKWarna.Id == Convert.ToInt32(myReader2["AttributeBajuOKWarnaId"])),
                            Convert.ToDecimal(myReader["Harga"]),
                            Convert.ToInt16(myReader["Qty"])
                        ));
                        CloseReadSqlData(ref myReader2);
                        break;


                    case 10:
                        ReadSqlData(ref Conn, ref myReader2, "SELECT * FROM tblBarangJasIDI WHERE Id = " + Convert.ToInt32(myReader["BarangId"]));
                        myReader2.Read();
                        ListOfItems.Add(new Model.JasIDI(
                            ListSex.FirstOrDefault(sex => sex.Id == Convert.ToInt32(myReader2["AttributeJenisKelaminId"])),
                            ListAttributeSize.FirstOrDefault(size => size.Id == Convert.ToInt32(myReader2["AttributeSizeId"])),
                            Convert.ToDecimal(myReader["Harga"]),
                            Convert.ToInt16(myReader["Qty"])
                        ));
                        CloseReadSqlData(ref myReader2);
                        break;


                    default:
                        break;
                }

            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblPenerimaanBarangItemLain WHERE PenerimaanBarangId = " + listView1.SelectedItems[0].SubItems[0].Text);
            while (myReader.Read())
            {
                ListOfItems.Add(new Model.ItemLain(
                    myReader["NamaBarang"].ToString(),
                    Convert.ToDecimal(myReader["Harga"]),
                    Convert.ToInt16(myReader["Qty"])
                ));
            }
            CloseReadSqlData(ref myReader);

            SimplifyJasDokter();

            listView2.Items.Clear();

            foreach (ItemSimplified item in ListOfItemsSimplified)
            {
                ListViewItem lvitem = new ListViewItem(item.JenisBarangId.ToString());
                lvitem.SubItems.Add(item.GetName());
                lvitem.SubItems.Add("");
                lvitem.SubItems.Add(item.GetQuantity().ToString());
                lvitem.SubItems.Add(string.Format("{0:n0}", item.BuyingPrice));
                listView2.Items.Add(lvitem);
            }

            foreach (BarangItem item in ListOfItems.Where(barang => barang.JenisBarangId != 1))
            {
                ListViewItem lvitem = new ListViewItem(item.JenisBarangId.ToString());
                lvitem.SubItems.Add(item.GetName());
                lvitem.SubItems.Add(item.GetSize());
                lvitem.SubItems.Add(item.Quantity.ToString());
                lvitem.SubItems.Add(string.Format("{0:n0}", item.BuyingPrice));
                listView2.Items.Add(lvitem);
            }


        }

        private void SimplifyJasDokter()
        {
            foreach (BarangItem item in ListOfItems.Where(item => item.JenisBarangId == 1))
            {
                JasDokter jasDokterItem = (JasDokter)item;


                if (ListOfItemsSimplified.Where(
                    jasDokter => jasDokter.Sex == jasDokterItem.Sex
                    && jasDokter.AttributeLengan == jasDokterItem.AttributeLengan
                    && jasDokter.AttributeJasDokterBahan == jasDokterItem.AttributeJasDokterBahan
                    && jasDokter.BuyingPrice == jasDokterItem.BuyingPrice
                    ).ToList().Count > 0)
                {
                    foreach (var item1 in ListOfItemsSimplified.Where(
                    jasDokter => jasDokter.Sex == jasDokterItem.Sex
                    && jasDokter.AttributeLengan == jasDokterItem.AttributeLengan
                    && jasDokter.AttributeJasDokterBahan == jasDokterItem.AttributeJasDokterBahan
                    && jasDokter.BuyingPrice == jasDokterItem.BuyingPrice
                    ))
                    {
                        item1.AttributeSizeList.Add(new AttributeSize(jasDokterItem.AttributeSize, jasDokterItem.Quantity));
                    }
                }
                else
                {
                    ListOfItemsSimplified.Add(new ItemSimplified(
                        jasDokterItem.Sex,
                        jasDokterItem.AttributeLengan,
                        jasDokterItem.AttributeJasDokterBahan,
                        new AttributeSize(jasDokterItem.AttributeSize, jasDokterItem.Quantity),
                        jasDokterItem.BuyingPrice));
                }                    
            }
        }

        private void FillTextBox1()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[6].Text;
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            FillListView1();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                FillListView2();
                FillTextBox1();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                DialogResult dialogResult = MessageBox.Show("Konfirmasi untuk menghapus barang masuk tanggal : " + listView1.SelectedItems[0].SubItems[1].Text + " dari "+ listView1.SelectedItems[0].SubItems[2].Text + "?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ExecuteSqlCommand(ref Conn, "Exec spDELETE_frmDaftarBarangMasuk_button2_Click " + listView1.SelectedItems[0].SubItems[0].Text);
                    FillListView1();
                    listView2.Items.Clear();

                }


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangMasuk>().Any())
            {
                Application.OpenForms.OfType<frmBarangMasuk>().First().Close();
            }

            frmBarangMasuk frmBarangMasukInstance = new frmBarangMasuk();
            frmBarangMasukInstance.frmBarangMasukCallback = RetrieveCallBack;
            frmBarangMasukInstance.MdiParent = MdiParent;
            frmBarangMasukInstance.Show();
            Enabled = false;
            
        }

    }
}
