using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace DuaSaudaraFinance.Model
{
    internal class ItemSimplified
    {
        public int JenisBarangId { get; set; }
        public Sex Sex { get; set; }
        public AttributeLengan AttributeLengan { get; set; }
        public AttributeJasDokterBahan AttributeJasDokterBahan { get; set; }
        public List<AttributeSize> AttributeSizeList { get; set; } = new List<AttributeSize>();
        public decimal BuyingPrice { get; set; }
        public ItemSimplified(Sex sex, AttributeLengan attributeLengan, AttributeJasDokterBahan attributeJasDokterBahan, AttributeSize attributeSize, decimal buyingPrice)
        {
            JenisBarangId = 1;
            Sex = sex;
            AttributeLengan = attributeLengan;
            AttributeJasDokterBahan = attributeJasDokterBahan;
            AttributeSizeList.Add(attributeSize);
            BuyingPrice = buyingPrice;
        }
        public string GetName()
        {
            var sentence = "";
            
            sentence += (Sex.Name == "Cowo") ? "SID " : "ALE ";
            sentence += (AttributeLengan.Name == "Pendek") ? "S/S " : "L/S ";
            switch (AttributeJasDokterBahan.Name)
            {
                case "Serat":
                    sentence += "GB ";
                    break;
                case "Halus":
                    sentence += "GBL ";
                    break;
                case "Platinum":
                    sentence += "PL ";
                    break;
                default:
                    break;
            }

            var groupedAttributeSizeList = AttributeSizeList
                .GroupBy(item => new { item.Id, item.Name })
                .Select(group => new
                {
                    group.Key.Id,
                    group.Key.Name,
                    Quantity = group.Sum(item => item.Quantity)
                })
                .ToList();

            foreach (var item in AttributeSizeList.OrderBy(item => item.Id).ToList())
            {
                sentence = sentence + item.Name + "/" + item.Quantity + ", "; 
            }

            if (sentence.EndsWith(", "))
            {
                sentence = sentence.Substring(0, sentence.Length - 2);
            }
            return sentence;
        }

        public int GetQuantity()
        {
            return AttributeSizeList
            .Sum(item => item.Quantity);
        }
    }
}
