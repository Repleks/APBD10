using APBD10.Models;
using APBD10;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Context
{
    public class ShopContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<ProductCategories> ProductsCategories { get; set; } = null!;

        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=master;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategories>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });
            
            modelBuilder.Entity<ProductCategories>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductsCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategories>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductsCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<ShoppingCart>()
                .HasKey(sc => new { sc.AccountId, sc.ProductId });

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Account)
                .WithMany(a => a.ShoppingCarts)
                .HasForeignKey(sc => sc.AccountId);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Product)
                .WithMany(p => p.ShoppingCarts)
                .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.RoleId);
        }
    }
}
