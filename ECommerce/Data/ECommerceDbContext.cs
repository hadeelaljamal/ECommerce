using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class ECommerceDbContext:DbContext
    {
        // Type ctrl shortcut to create the constructor
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext>options):base(options)
        {
            //Add configure in the ConfigureServices function wich is in statup.cs for the EcommerceDbContext     
        }

        public DbSet<Category>Categories { get; set; } //here I make Renaming for Category to Categories Table To use it in this name in the Project
        public DbSet<Product> Products { get; set; }
    }
}
