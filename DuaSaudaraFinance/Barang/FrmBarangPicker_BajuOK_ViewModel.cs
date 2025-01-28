using DuaSaudaraFinance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Barang
{
    internal class FrmBarangPicker_BajuOK_ViewModel
    {
        public List<AttributeBajuOKSet> _attributeBajuOKSetList = new List<AttributeBajuOKSet>();
        public List<AttributeBajuOKJenis> _attributeBajuOKJenisList = new List<AttributeBajuOKJenis>();
        public List<AttributeBajuOKWarna> _attributeBajuOKWarnaList = new List<AttributeBajuOKWarna>();
        public List<AttributeSize> _attributeSizeList = new List<AttributeSize>();

        public FrmBarangPicker_BajuOK_ViewModel()
        {

            Fill_attributeBajuOKSetList();
            Fill_attributeBajuOKSetJenis();
            Fill_attributeBajuOKWarna();
            Fill_attributeSize();
        }

        private void Fill_attributeBajuOKSetList()
        {           

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeBajuOKSet");
            while (myReader.Read())
            {
                _attributeBajuOKSetList.Add(new AttributeBajuOKSet(Convert.ToInt32(myReader["Id"]), myReader["NamaSetBajuOK"].ToString()));
            }
            CloseReadSqlData(ref myReader);
        }
        private void Fill_attributeBajuOKSetJenis()
        {

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeBajuOKJenis");
            while (myReader.Read())
            {
                _attributeBajuOKJenisList.Add(new AttributeBajuOKJenis(Convert.ToInt32(myReader["Id"]), myReader["NamaJenisBajuJaga"].ToString()));
            }
            CloseReadSqlData(ref myReader);
        }
        private void Fill_attributeBajuOKWarna()
        {

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeBajuOKWarna ORDER BY NamaWarnaBajuJaga");
            while (myReader.Read())
            {
                _attributeBajuOKWarnaList.Add(new AttributeBajuOKWarna(Convert.ToInt32(myReader["Id"]), myReader["NamaWarnaBajuJaga"].ToString()));
            }
            CloseReadSqlData(ref myReader);
        }
        private void Fill_attributeSize()
        {

            SqlDataReader myReader = null;
            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeSize");
            while (myReader.Read())
            {
                _attributeSizeList.Add(new AttributeSize(Convert.ToInt32(myReader["Id"]), myReader["NamaSize"].ToString()));
            }
            CloseReadSqlData(ref myReader);
        }

        public void UpsertBajuOK(int AttributeBajuOKSetId, int AttributeBajuOKJenisId, int AttributeSizeId, int AttributeBajuOKWarnaId, int Qty, decimal Harga, string FormName)
        {
            string Kalimat = "Exec spINSERT_frmBarangPicker_BajuOK_button1_Click ";

            Kalimat += AttributeBajuOKSetId + ", ";
            Kalimat += AttributeBajuOKJenisId + ", ";
            Kalimat += AttributeSizeId + ", ";
            Kalimat += AttributeBajuOKWarnaId + ", ";
            Kalimat += Qty + ", ";
            Kalimat += Harga + ", ";
            Kalimat += "'" + FormName + "', ";
            Kalimat += "'" + UserName + "'";

            ExecuteSqlCommand(ref Conn, Kalimat);
        }






    }
}
