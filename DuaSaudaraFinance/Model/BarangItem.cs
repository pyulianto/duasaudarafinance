using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance.Model
{
    internal abstract class BarangItem
    {
        public int JenisBarangId { get; internal set; }
        protected int Id { get; set; }        
        public Sex Sex { get; set; }
        protected Supplier Supplier { get; set; }
        public decimal BuyingPrice { get; set; }
        protected decimal SellingPrice { get; set; }
        protected int InitialQty { get; set; }
        public int Quantity { get; set; }
        protected string LastEditor { get; set; }
        protected DateTime LastUpdate { get; set; }

        public abstract string GetName();

        public abstract string GetSize();

        

    }
}
