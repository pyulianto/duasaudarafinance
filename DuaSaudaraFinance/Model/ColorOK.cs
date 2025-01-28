using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class ColorOK
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ColorOK(int _Id, string _Name)
        {
            Id = _Id;
            Name = _Name;
        }

    }
}
