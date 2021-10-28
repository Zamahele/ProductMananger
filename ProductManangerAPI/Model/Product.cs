using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManangerAPI.Model
{
    [Index(nameof(ProductCode), IsUnique = true)]
    public class Product
    {

        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
