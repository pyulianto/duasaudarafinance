using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class AttributeLengan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string KodeLengan { get; set; }

        public AttributeLengan(int id, string name, string kodelengan)
        {
            Id = id;
            Name = name;
            KodeLengan = kodelengan;
        }

    }
}
