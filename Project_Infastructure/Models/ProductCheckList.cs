﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Infastructure.Models
{
    public class ProductCheckList
    {
        public string ProductName { get; set; }

		public int ProductId { get; set; }
        public bool Checked { get; set; }
    }
}
