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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Barang
{
    public partial class frmBarangPicker_JasDokter : Form
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

        public Action<string> BarangPicker_JasDokter_Callback { get; set; }

        public int IsCowo = 1;
        public int IsSmart = 0;
        
        public string FormName { get; set; }

        public frmBarangPicker_JasDokter()
        {
            InitializeComponent();
        }

        private void frmBarangPicker_JasDokter_Load(object sender, EventArgs e)
        {
            if (IsCowo==1)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            if (IsSmart==1)
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
            else
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }

            fillComboBox1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckBarangPicker_JasDokter();

            if (ResultCheck=="")
            {
                string Kalimat = "";

                if (IsSmart==0)
                {
                    Kalimat = "Exec spINSERT_frmBarangPicker_JasDokter_button1_Click ";

                    if (radioButton1.Checked)
                    {
                        Kalimat += "'Cowo', ";
                    }
                    else
                    {
                        Kalimat += "'Cewe', ";
                    }

                    if (radioButton4.Checked)
                    {
                        Kalimat += "'Panjang', ";
                    }
                    else
                    {
                        Kalimat += "'Pendek', ";
                    }

                    if (radioButton6.Checked)
                    {
                        Kalimat += "'Serat', ";
                    }
                    else
                    {
                        if (radioButton5.Checked)
                        {
                            Kalimat += "'Halus', ";
                        }
                        else
                        {
                            Kalimat += "'Platinum', ";
                        }

                    }

                    Kalimat += ((ComboboxValue)comboBox1.SelectedItem).Id + ", ";
                    Kalimat += textBox1.Text + ", ";
                    if (textBox2.Text == "")
                    {
                        Kalimat += "0, ";
                    }
                    else
                    {
                        Kalimat += textBox2.Text + ", ";
                    }
                    Kalimat += "'" + FormName + "', ";
                    Kalimat += "'" + UserName + "'";

                    ExecuteSqlCommand(ref Conn, Kalimat);
                }
                else
                {
                    try
                    {
                        string rawSize = textBox3.Text.Replace(" ", "");
                        List<string> SizeList = rawSize.Split(',').ToList();
                        foreach (var item in SizeList)
                        {
                            string[] parts = item.Split('/');

                            Kalimat += "Exec spINSERT_frmBarangPicker_JasDokter_button1_Click ";

                            if (radioButton1.Checked)
                            {
                                Kalimat += "'Cowo', ";
                            }
                            else
                            {
                                Kalimat += "'Cewe', ";
                            }

                            if (radioButton4.Checked)
                            {
                                Kalimat += "'Panjang', ";
                            }
                            else
                            {
                                Kalimat += "'Pendek', ";
                            }

                            if (radioButton6.Checked)
                            {
                                Kalimat += "'Serat', ";
                            }
                            else
                            {
                                if (radioButton5.Checked)
                                {
                                    Kalimat += "'Halus', ";
                                }
                                else
                                {
                                    Kalimat += "'Platinum', ";
                                }

                            }

                            var foundItem = ListAttributeSize.FirstOrDefault(size => size.Name == parts[0].ToUpper());
                            if (foundItem != null)
                            {
                                Kalimat += ListAttributeSize.FirstOrDefault(size => size.Name == parts[0].ToUpper()).Id;
                            }
                            else
                            {
                                throw new Exception("Size "+ parts[0] + " does not exist.");
                            }                            

                            Kalimat += ", " + parts[1] + ", ";
                            Kalimat += "-1, ";
                            Kalimat += "'" + FormName + "', ";
                            Kalimat += "'" + UserName + "'";

                            Kalimat += Environment.NewLine;



                        }

                        ExecuteSqlCommand(ref Conn, Kalimat);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    

                }
                

                BarangPicker_JasDokter_Callback.Invoke("fillListView1");


                if (FormName!="BarangMasuk")
                {
                    Close();
                }
                
            }
            else
            {
                MessageBox.Show(ResultCheck, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private string CheckBarangPicker_JasDokter()
        {
            string Kalimat = "";


            if (radioButton1.Checked==false && radioButton2.Checked == false)
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Jenis Kelamin Tidak Boleh Kosong";
            }

            if (radioButton3.Checked == false && radioButton4.Checked == false)
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Lengan Tidak Boleh Kosong";
            }

            if (radioButton5.Checked == false && radioButton6.Checked == false && radioButton7.Checked == false)
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Bahan Tidak Boleh Kosong";
            }

            if (IsSmart == 0)
            {
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
            }
            else
            {
                if (textBox3.Text == "")
                {
                    if (Kalimat != "")
                    {
                        Kalimat += System.Environment.NewLine;
                    }
                    Kalimat += "Size Tidak Boleh Kosong";
                }
            }

            return Kalimat;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 13:
                    //Enter Key
                    button1.PerformClick();
                    break;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    //Number Key
                    e.Handled = false;
                    break;
                case 8:
                    //Backspace Key
                    e.Handled = false;
                    break;
                case 127:
                    //Delete Key
                    e.Handled = false;
                    break;
                case 46:
                    //Dot Key
                    //e.Handled = false;
                    //break;
                default:
                    e.Handled = true;
                    break;
            }
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

        private void frmBarangPicker_JasDokter_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarangPicker_JasDokter_Callback.Invoke("Enabled");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                //case 13:
                //    //Enter Key
                //    button1.PerformClick();
                //    break;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    //Number Key
                    e.Handled = false;
                    break;
                case 8:
                    //Backspace Key
                    e.Handled = false;
                    break;
                case 127:
                    //Delete Key
                    e.Handled = false;
                    break;
                case 46:
                //Dot Key
                //e.Handled = false;
                //break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int Qty = 0;
            string kalimat = textBox3.Text.Replace(" ","");
            string[] Item01 = kalimat.Split(',');

            foreach (var item in Item01)
            {
                string[] Item02 = item.Split('/');
                if (Item02.Length==2) {
                    try
                    {
                        Qty += int.Parse(Item02[1]);
                        label3.Text = Qty.ToString();
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
        }
    }
}
