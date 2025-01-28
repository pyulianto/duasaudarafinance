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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangPicker_BajuLab : Form
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
        public Action<string> frmBarangPicker_BajuLab_Callback { get; set; }

        public frmBarangPicker_BajuLab()
        {
            InitializeComponent();
        }
        public string FormName { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string CheckBarangPicker_BajuLab()
        {
            string Kalimat = "";

            if (radioButton3.Checked == false && radioButton4.Checked == false)
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Lengan Tidak Boleh Kosong";
            }
            
            if (comboBox1.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Size Tidak Boleh Kosong";
            }

            if (textBox1.Text == "" || textBox1.Text == "0")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Qty Tidak Boleh Kosong";
            }

            if (textBox2.Text == "" || textBox2.Text == "0")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Harga Tidak Boleh Kosong";
            }





            return Kalimat;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckBarangPicker_BajuLab();

            if (ResultCheck == "")
            {
                string Kalimat = "Exec spINSERT_frmBarangPicker_JasLab_button1_Click ";

                if (radioButton4.Checked)
                {
                    Kalimat += "'Panjang', ";
                }
                else
                {
                    Kalimat += "'Pendek', ";
                }                               

                Kalimat += ((ComboboxValue)comboBox1.SelectedItem).Id + ", ";
                Kalimat += textBox1.Text + ", ";
                Kalimat += textBox2.Text + ", ";
                Kalimat += "'" + FormName + "', ";
                Kalimat += "'" + UserName + "'";

                ExecuteSqlCommand(ref Conn, Kalimat);

                frmBarangPicker_BajuLab_Callback.Invoke("fillListView1");

                Close();
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBarangPicker_BajuLab_Load(object sender, EventArgs e)
        {
            fillComboBox1();
        }

        private void fillComboBox1()
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "Exec spSELECT_frmBarangPicker_JasDokter_fillComboBox1 ");
            while (myReader.Read())
            {
                comboBox1.Items.Add(new ComboboxValue(Convert.ToInt32(myReader["id"]), myReader["NamaSize"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            comboBox1.SelectedIndex = -1;


        }

        private void frmBarangPicker_BajuLab_FormClosed(object sender, FormClosedEventArgs e)
        {

            frmBarangPicker_BajuLab_Callback.Invoke("Enabled");
        }
    }
}
