﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuaSaudaraFinance.Model
{
    internal class AttributeBajuOKSet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AttributeBajuOKSet(int id, string name) {
            Id = id;
            Name = name;
        }

    }
}
