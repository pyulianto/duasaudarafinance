using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class JasIDI : BarangItem
    {
        protected AttributeSize AttributeSize { get; set; }

        public JasIDI(Sex sex, AttributeSize attributeSize,
            decimal buyingPrice, int quantity)
        {
            JenisBarangId = 10;
            Sex = sex;
            AttributeSize = attributeSize;
            BuyingPrice = buyingPrice;
            Quantity = quantity;
        }

        public override string GetName()
        {
            return "Jas IDI " + Sex.Name;
        }

        public override string GetSize()
        {
            return AttributeSize.Name;
        }

    }
}
