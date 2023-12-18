using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Product : EntityBase
    {
        public string Id { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }

        public Category Category { get; set; }

    }
}
