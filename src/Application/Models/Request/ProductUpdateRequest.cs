﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class ProductUpdateRequest
    {
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool ProductState { get; set; }
    }
}
