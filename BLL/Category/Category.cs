using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Category
{
    public class Category
    {
        [Required]
        [DisplayName("Category Id")]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Category Code")]
        [MaxLength(6,ErrorMessage ="Incorect Format,Please use 3 alphabet letters and three numeric characters e.g., ABC123")]
        [MinLength(6,ErrorMessage ="Incorect Format,Please use 3 alphabet letters and three numeric characters e.g., ABC123")]
        [Remote("ValidateCategory", "Categories")]
        public string CategoryCode { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public ICollection<Product.Product> Products { get; set; }
    }
}
    