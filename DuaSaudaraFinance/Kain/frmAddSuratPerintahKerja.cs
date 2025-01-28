using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuaSaudaraFinance.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Kain
{
    public partial class frmAddSuratPerintahKerja : Form
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
        public frmAddSuratPerintahKerja()
        {
            InitializeComponent();
        }

        public List<ItemKain> ListItemKainCurrent = new List<ItemKain>();

        private int NoSPK = 0;

        private void Button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckSuratPerintahKerja();

            if (ResultCheck == "")
            {                

                string kalimat = "Exec spINSERT_frmSuratPerintahKerja_Button1_Click ";

                kalimat += "'" + dateTimePicker1.Value + "', ";
                kalimat += NoSPK + ", ";
                kalimat += ((ComboboxValue)comboBox5.SelectedItem).Id + ", '";
                kalimat += NormalizeText(textBox3.Text) + "', '" + UserName + "'";

                int SPKId = ExecuteSqlCommandReturnId(ref Conn, kalimat);

                if (ListItemKainCurrent.Count > 0)
                {
                    kalimat = "";
                    foreach (var item in ListItemKainCurrent)
                    {
                        kalimat += "Exec spINSERT_frmSuratPerintahKerja_Button1_Click_KainItems ";
                        kalimat += SPKId + ", ";
                        kalimat += item.AttributeKainWarna.AttributeKainWarnaId + ", ";
                        kalimat += item.CurrentQtyMeter() + "; ";
                    }
                    ExecuteSqlCommand(ref Conn, kalimat);                    
                }

                Close();
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string NormalizeText(string text)
        {
            // Remove leading and trailing whitespace from each line
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }

            // Remove excess whitespace within each line
            string normalizedText = string.Join(Environment.NewLine, lines);
            normalizedText = Regex.Replace(normalizedText, @"\s+", " ");

            return normalizedText;
        }

        private string CheckSuratPerintahKerja()
        {
            string Kalimat = "";


            if (textBox2.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "No Nota Tidak Boleh Kosong";
            }

            if (comboBox5.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Penjahit Tidak Boleh Kosong";
            }

            if (textBox3.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Catatan Tidak Boleh Kosong";
            }

            return Kalimat;

        }      

        private string CheckSuratPerintahKerja_addkain()
        {
            string Kalimat = "";

            if (comboBox1.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Kain Tidak Boleh Kosong";
            }
            if (comboBox2.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Warna Tidak Boleh Kosong";
            }

            if (textBox1.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Qty Tidak Boleh Kosong";
            }

            if (Kalimat == "")
            {
                bool itemExists = ListItemKainCurrent.Any(k => k.ItemKainId == ((ComboboxValue)comboBox1.SelectedItem).Id && k.AttributeKainWarna != null
                && k.AttributeKainWarna.AttributeKainWarnaId == ((ComboboxValue)comboBox2.SelectedItem).Id);

                if (itemExists)
                {
                    if (Kalimat != "")
                    {
                        Kalimat += System.Environment.NewLine;
                    }
                    Kalimat += "Kain sudah di input";
                }
            }
                

            return Kalimat;

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckSuratPerintahKerja_addkain();

            if (ResultCheck == "")
            {

                ItemKain ItemKainTemp = ListItemKain.FirstOrDefault(k => k.ItemKainId == ((ComboboxValue)comboBox1.SelectedItem).Id);

                ItemKain newItemKain = new ItemKain(
                    ItemKainTemp.ItemKainId,
                    ItemKainTemp.KodeKain,
                    ItemKainTemp.HargaDefault,
                    ItemKainTemp.Satuan,
                    ItemKainTemp.Keterangan
                );
                newItemKain.setWarnaQtyKain(((ComboboxValue)comboBox2.SelectedItem).Id, decimal.Parse(textBox1.Text), comboBox3.Text);

                ListItemKainCurrent.Add(newItemKain);
                FillListView1();

                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                textBox1.Text = "";

            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void FrmSuratPerintahKerja_Load(object sender, EventArgs e)
        {
            PrepareListView1();
            FillComboBox5();
            FillComboBox1();
            FillTextBox2();
            FillComboBox3();
        }

        private void FillListView1()
        {
            listView1.Items.Clear();

            foreach (var item in ListItemKainCurrent)
            {
                ListViewItem lvitem = new ListViewItem(item.ItemKainId.ToString());
                lvitem.SubItems.Add(item.AttributeKainWarna.AttributeKainWarnaId.ToString());
                lvitem.SubItems.Add(item.KodeKain);
                lvitem.SubItems.Add(item.AttributeKainWarna.KodeWarna + " " + item.AttributeKainWarna.NamaWarna);
                lvitem.SubItems.Add(item.CurrentQty + " " + item.Satuan);
                listView1.Items.Add(lvitem);
            }

            listView1.Columns[0].Width = 0;
            listView1.Columns[1].Width = 0;
        }
        
        private void FillComboBox5()
        {
            comboBox5.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmSuratPerintahKerja_FillComboBox5";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox5.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["Id"]), myReader["NamaPenjahit"].ToString()));

            }

            CloseReadSqlData(ref myReader);
        }

        private void FillComboBox1()
        {

            comboBox1.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmDaftarJenisKain_AddColor_FillComboBox1";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox1.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["Id"]), myReader["KodeKain"].ToString()));
            }



            CloseReadSqlData(ref myReader);

        }

        private void FillComboBox2()
        {

            comboBox2.Items.Clear();
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmDaftarArusKain_FillListView2 '" + comboBox1.Text + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox2.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["Id"]), myReader["KodeWarna"].ToString()));

            }

            CloseReadSqlData(ref myReader);

        }

        private void FillComboBox3()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Meter");
            comboBox3.Items.Add("Yard");
        }

        private void AssignComboBox3()
        {
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_AssignComboBox3 '" + comboBox1.Text + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            while (myReader.Read())
            {
                comboBox3.Text = myReader["Satuan"].ToString();

            }

            CloseReadSqlData(ref myReader);

        }

        private void FillTextBox4()
        {

            textBox4.Text = "";
            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_FillTextBox4 '" + comboBox1.Text + "', '" + comboBox2.Text + "'";

            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            myReader.Read();

            textBox4.Text = myReader["NamaWarna"].ToString();

            CloseReadSqlData(ref myReader);

        }

        private void FillTextBox2()
        {

            SqlDataReader myReader = null;

            string KalimatQuery = "Exec spSELECT_frmKainMasuk_FillTextBox2 '" + dateTimePicker1.Value + "'";
            ReadSqlData(ref Conn, ref myReader, KalimatQuery);
            myReader.Read();

            NoSPK = Convert.ToInt32(myReader["NoSPK"]);
            textBox2.Text = dateTimePicker1.Value.ToString("yyyyMM") +"-"+ NoSPK.ToString("D3");



            CloseReadSqlData(ref myReader);

        }


        private void PrepareListView1()
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.FullRowSelect = true;

            listView1.Columns.Add("IdKain", 0, HorizontalAlignment.Left); 
            listView1.Columns.Add("IdWarna", 0, HorizontalAlignment.Left);

            listView1.Columns.Add("Nama Kain", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Warna", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Qty", 100, HorizontalAlignment.Right);
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = "";
            FillComboBox2();
            AssignComboBox3();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTextBox4();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) != 0)
            {
                ListItemKainCurrent.RemoveAll(k => k.ItemKainId == Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) && k.AttributeKainWarna != null && k.AttributeKainWarna.AttributeKainWarnaId == Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text));
                FillListView1();
            }
                
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FillTextBox2();
        }
    }
}
