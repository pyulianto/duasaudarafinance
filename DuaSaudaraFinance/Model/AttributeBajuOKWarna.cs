using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class AttributeBajuOKWarna
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AttributeBajuOKWarna(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
