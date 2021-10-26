using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Product;
using BLL.Category;

namespace ProductMananger.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BLL.Product.Product> Product { get; set; }
        public DbSet<BLL.Category.Category> Category { get; set; }
    }
}
