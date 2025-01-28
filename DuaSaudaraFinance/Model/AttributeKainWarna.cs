using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    public class AttributeKainWarna
    {

        public int AttributeKainWarnaId { get; set; }
        public int ItemKainId { get; set; }
        public string KodeWarna { get; set; }
        public string NamaWarna { get; set; }
        public string Keterangan { get; set; }
        public decimal StokAwal { get; set; }

        public AttributeKainWarna(int attributeKainWarnaId, int itemKainId, string kodeWarna, string namaWarna, string keterangan, decimal stokAwal) {
            AttributeKainWarnaId = attributeKainWarnaId;
            ItemKainId = itemKainId;
            KodeWarna = kodeWarna;
            NamaWarna = namaWarna;
            Keterangan = keterangan;
            StokAwal = stokAwal;
        }



    }
}
