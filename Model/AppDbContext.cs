using Microsoft.EntityFrameworkCore;

namespace MiniProjectTwo.Model
{
    class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Office> Offices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=THINKPAD;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>()
        //        .HasOne(p => p.Category)
        //        .WithMany(b => b.Products)
        //        ;
        //}
    }
}
