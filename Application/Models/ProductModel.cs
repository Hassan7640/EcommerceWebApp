﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
   public class ProductModel
    {
        public string Id { get; set; }
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        //public List<Category> SelectedCategories { get; set; }

    }
}
