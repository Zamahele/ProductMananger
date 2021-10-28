using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Product
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category.Category Category { get; set; }
    }
}
