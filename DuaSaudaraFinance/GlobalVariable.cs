using DuaSaudaraFinance.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance
{
    class GlobalVariable
    {

        //public static string ConnString = "Server=PRIMA-THINKPAD\\SQLEXPRESS; User Id=sa; password=ilc960x6; Database=DSFinance; MultipleActiveResultSets=True";
        //public static string ConnString = "Server=tcp:primasqldatabase.database.windows.net,1433;Initial Catalog=DSFinance;Persist Security Info=False;User ID=strangerman;Password=Ilc960x6;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;";


        public static string ConnString;

        public static SqlConnection Conn = new SqlConnection();

        public static string UserName = "Karyawan01";

        public static int IsOwner = 0;


        public static List<AttributeJasDokterBahan> ListAttributeJasDokterBahan = new List<AttributeJasDokterBahan>();
        public static List<Sex> ListSex = new List<Sex>();
        public static List<AttributeLengan> ListAttributeLengan = new List<AttributeLengan>();
        public static List<AttributeSize> ListAttributeSize = new List<AttributeSize>();
        public static List<AttributeResidenSetBarang> ListAttributeResidenSetBarang = new List<AttributeResidenSetBarang>();
        public static List<AttributeBajuOKSet> ListAttributeBajuOKSet = new List<AttributeBajuOKSet>();
        public static List<AttributeBajuOKJenis> ListAttributeBajuOKJenis = new List<AttributeBajuOKJenis>();
        public static List<AttributeBajuOKWarna> ListAttributeBajuOKWarna = new List<AttributeBajuOKWarna>();

        public static List<ItemKain> ListItemKain = new List<ItemKain>();
        public static List<AttributeKainWarna> ListAttributeKainWarna = new List<AttributeKainWarna>();


    }
}
