using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Model
{
    internal class JasDokter : BarangItem
    {
        public AttributeLengan AttributeLengan;
        public AttributeJasDokterBahan AttributeJasDokterBahan { get; set; }
        public AttributeSize AttributeSize { get; set; }

        public JasDokter(Sex sex, AttributeLengan attributeLengan,AttributeJasDokterBahan attributeJasDokterBahan, AttributeSize attributeSize,
            decimal buyingPrice, int quantity) {
            JenisBarangId = 1;
            Sex = sex;
            AttributeLengan = attributeLengan;
            AttributeJasDokterBahan = attributeJasDokterBahan;
            AttributeSize = attributeSize;            
            BuyingPrice = buyingPrice;
            Quantity = quantity;        
        }


        public JasDokter(Sex sex, AttributeLengan attributeLengan, AttributeJasDokterBahan attributeJasDokterBahan, AttributeSize attributeSize, int quantity)
        {
            SqlDataReader myReader = null;

            JenisBarangId = 1;
            Sex = sex;
            AttributeLengan = attributeLengan;
            AttributeJasDokterBahan = attributeJasDokterBahan;
            AttributeSize = attributeSize;

            string Kalimat = "SELECT PriceBuyDefault FROM tblBarangJasDokter WHERE JenisKelamin=" + Sex.Id
            + " AND Lengan=" + AttributeLengan.Id
            + " AND Bahan=" + AttributeJasDokterBahan.Id
            + " AND Size=" + AttributeSize.Id;
            ReadSqlData(ref Conn, ref myReader, Kalimat);
            myReader.Read();
            BuyingPrice = decimal.Parse(myReader["PriceBuyDefault"].ToString());
            CloseReadSqlData(ref myReader);

            Quantity = quantity;
        }



        public override string GetName()
        {
            return "Jas Dokter " + Sex.Name + " " + AttributeLengan.Name + " " + AttributeJasDokterBahan.Name;
        }

        public override string GetSize()
        {
            return AttributeSize.Name;
        }        

    }
}
