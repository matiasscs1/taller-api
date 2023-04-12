using Microsoft.EntityFrameworkCore;

namespace Taller.Models
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options) : base(options)
        {
            
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Producto> Products { get; set; }


    }
}
