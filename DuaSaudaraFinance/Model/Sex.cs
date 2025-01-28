using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class Sex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string KodeJenisKelamin { get; set; }
        public Sex(int id, string name, string kodejeniskelamin)
        {
            Id = id;
            Name = name;
            KodeJenisKelamin = kodejeniskelamin;
        }
    }
}
