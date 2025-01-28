using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class AttributeJasDokterBahan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string KodeBahan { get; set; }

        public AttributeJasDokterBahan(int id, string name, string kodebahan)
        {
            Id = id;
            Name = name;
            KodeBahan = kodebahan;
        }
    }
}
