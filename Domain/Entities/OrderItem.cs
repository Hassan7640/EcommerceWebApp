﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem : EntityBase
    {

        public int Quantity { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }

    }
}
