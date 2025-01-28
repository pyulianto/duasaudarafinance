using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class AttributeSize
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public AttributeSize(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public AttributeSize(AttributeSize attibuteSize, int quantity)
        {
            Id = attibuteSize.Id;
            Name = attibuteSize.Name;
            Quantity = quantity;
        }
    }
}
