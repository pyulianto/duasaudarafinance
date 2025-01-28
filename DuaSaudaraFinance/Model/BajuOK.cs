using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class BajuOK : BarangItem
    {
        protected AttributeBajuOKSet AttributeBajuOKSet { get; set; }
        protected AttributeBajuOKJenis AttributeBajuOKJenis { get; set; }
        protected AttributeSize AttributeSize { get; set; }
        protected AttributeBajuOKWarna AttributeBajuOKWarna { get; set; }

        public BajuOK(AttributeBajuOKSet attributeBajuOKSet, AttributeBajuOKJenis attributeBajuOKJenis, AttributeSize attributeSize, 
            AttributeBajuOKWarna attributeBajuOKWarna, decimal buyingPrice, int quantity)
        {
            JenisBarangId = 3;
            AttributeBajuOKSet = attributeBajuOKSet;
            AttributeBajuOKJenis = attributeBajuOKJenis;
            AttributeSize = attributeSize;
            AttributeBajuOKWarna = attributeBajuOKWarna;            
            BuyingPrice = buyingPrice;
            Quantity = quantity;
        }

        public override string GetName()
        {
            return "Baju OK " + AttributeBajuOKJenis.Name + " " + AttributeBajuOKSet.Name + ", Warna " + AttributeBajuOKWarna.Name;
        }

        public override string GetSize()
        {
            return AttributeSize.Name;
        }
    }
}
