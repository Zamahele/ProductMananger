﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManangerAPI.Model
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CategoryCode { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
