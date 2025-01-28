using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DuaSaudaraFinance.GlobalVariable;

namespace DuaSaudaraFinance.Model
{
    public class ItemKain
    {

        public int ItemKainId { get; set; }
        public string KodeKain { get; set;}
        public decimal HargaDefault { get; set; }
        public string Satuan { get; set; }
        public string Keterangan { get; set; }
        public AttributeKainWarna AttributeKainWarna { get; set; }
        public decimal CurrentQty { get; set; }


        public ItemKain(int itemKainId, string kodeKain, decimal hargaDefault, string satuan, string keterangan) { 
            ItemKainId = itemKainId;
            KodeKain = kodeKain;
            HargaDefault = hargaDefault;
            Satuan = satuan;    
            Keterangan = keterangan;
        }

        public void setWarnaQtyKain(int attributeKainWarnaId, decimal qty, string externalSatuan)
        {
            AttributeKainWarna = ListAttributeKainWarna.FirstOrDefault(k => k.AttributeKainWarnaId == attributeKainWarnaId);            
            if (Satuan != externalSatuan)
            {
                switch (Satuan)
                {
                    case "Yard":
                        CurrentQty = qty * (decimal)1.09361;
                        break;
                    case "Meter":
                        CurrentQty = qty / (decimal)1.09361;
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                CurrentQty = qty;
            }
        }

        public decimal CurrentQtyMeter()
        {
            decimal currentQty = 0;

            switch (Satuan)
            {
                case "Yard":
                    currentQty = CurrentQty * (decimal)1.09361;
                    break;
                case "Meter":
                    CurrentQty = currentQty;
                    break;
                default:
                    break;
            }
            return currentQty;
        }

        //public ItemKain(int itemKainId, string kodeKain, decimal hargaDefault, string satuan, string keterangan, int attributeKainWarnaId)
        //{
        //    ItemKainId = itemKainId;
        //    KodeKain = kodeKain;
        //    HargaDefault = hargaDefault;
        //    Satuan = satuan;
        //    Keterangan = keterangan;
        //    AttributeKainWarna = ListAttributeKainWarna.FirstOrDefault(k => k.AttributeKainWarnaId == attributeKainWarnaId);
        //}


    }
}
