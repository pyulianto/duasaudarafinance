using DuaSaudaraFinance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static DuaSaudaraFinance.GlobalVariable;

namespace DuaSaudaraFinance.Barang
{
    public partial class FrmBarangMasuk_SmartInput : Form
    {
        List<BarangItem> ListOfItemsSub = new List<BarangItem>();
        List<BarangItem> ListOfItems = new List<BarangItem>();

        public FrmBarangMasuk_SmartInput()
        {
            InitializeComponent();
        }
        public Action<string> FrmBarangMasuk_SmartInput_Callback { get; set; }

        private void FrmBarangMasuk_SmartInput_Load(object sender, EventArgs e)
        {

        }

        private void FrmBarangMasuk_SmartInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmBarangMasuk_SmartInput_Callback.Invoke("Enabled");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string ResultCheck = CheckfrmBarangMasuk_SmartInput();

            if (ResultCheck == "")
            {
                ProcessSmartInput(textBox1.Text.Trim());
            }

        }

        private string CheckfrmBarangMasuk_SmartInput()
        {
            string Kalimat = "";


            if (textBox1.Text == "")
            {
                if (Kalimat != "")
                {
                    Kalimat += System.Environment.NewLine;
                }
                Kalimat += "Data Tidak Boleh Kosong";
            }





            return Kalimat;

        }

        private void ProcessSmartInput(string Kalimat)
        {
            string[] lines = Kalimat.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if(line.Count(c => c == ' ') != 3)
                {
                    textBox2.AppendText("Spasi tidak sama dengan 3" + Environment.NewLine);
                }
                else
                {
                    ListOfItemsSub.Clear();
                    string[] InvidualLine = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] InvidualLine_Size = InvidualLine[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string InvidualLine_Size_Item in InvidualLine_Size)
                    {
                        string[] SpecificSize = InvidualLine_Size_Item.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                        ListOfItemsSub.Add(
                            new Model.JasDokter(
                            ListSex.FirstOrDefault(sex => sex.KodeJenisKelamin == InvidualLine[0]),
                            ListAttributeLengan.FirstOrDefault(lengan => lengan.KodeLengan == InvidualLine[1]),
                            ListAttributeJasDokterBahan.FirstOrDefault(bahan => bahan.KodeBahan == InvidualLine[2]),
                            ListAttributeSize.FirstOrDefault(size => size.Name == SpecificSize[0]),
                            Convert.ToInt16(SpecificSize[1])
                            )
                        );

                        ListOfItems.Add(
                            new Model.JasDokter(
                            ListSex.FirstOrDefault(sex => sex.KodeJenisKelamin == InvidualLine[0]),
                            ListAttributeLengan.FirstOrDefault(lengan => lengan.KodeLengan == InvidualLine[1]),
                            ListAttributeJasDokterBahan.FirstOrDefault(bahan => bahan.KodeBahan == InvidualLine[2]),
                            ListAttributeSize.FirstOrDefault(size => size.Name == SpecificSize[0]),
                            Convert.ToInt16(SpecificSize[1])
                            )
                        );
                    }

                    textBox2.AppendText(line + " | " 
                        + ((int)ListOfItemsSub.FirstOrDefault().BuyingPrice).ToString() + " | " 
                        + ListOfItemsSub.Sum(item => item.Quantity * item.BuyingPrice) + Environment.NewLine);


                }




            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
