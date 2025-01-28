using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class ItemLain : BarangItem
    {
        public string NamaBarang { get; set; }
        public ItemLain(string namaBarang, decimal buyingPrice, int quantity)
        {
            JenisBarangId = 9999;
            NamaBarang = namaBarang;
            BuyingPrice = buyingPrice;
            Quantity = quantity;
        }

        public override string GetName()
        {
            return NamaBarang;
        }

        public override string GetSize()
        {
            return "N/A";
        }


    }
}
