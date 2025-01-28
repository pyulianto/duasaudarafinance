using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class BajuResiden : BarangItem
    {
        protected AttributeSize AttributeSize { get; set; }
        protected AttributeResidenSetBarang AttributeResidenSetBarang { get; set; }

        protected string Remark { get; set; }

        public BajuResiden(Sex sex, AttributeSize attributeSize, AttributeResidenSetBarang attributeResidenSetBarang,
            decimal buyingPrice, int quantity, string remark)
        {
            JenisBarangId = 2;
            Sex = sex;
            AttributeSize = attributeSize;
            AttributeResidenSetBarang = attributeResidenSetBarang;
            BuyingPrice = buyingPrice;
            Quantity = quantity;
            Remark = remark;
        }

        public override string GetName()
        {
            if (Remark == "Stok")
            {
                return "Residen " + Sex.Name + " " + AttributeResidenSetBarang.Name;
            }
            else
            {
                return "Residen " + Remark;
            }
            
        }

        public override string GetSize()
        {
            return AttributeSize.Name;
        }

    }
}
