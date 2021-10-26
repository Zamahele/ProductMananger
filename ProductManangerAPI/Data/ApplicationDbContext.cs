using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManangerAPI.Model;

namespace ProductManangerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductManangerAPI.Model.Product> Product { get; set; }

        public DbSet<ProductManangerAPI.Model.Category> Category { get; set; }
    }
}
