using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce_Project.Configurations;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Utilities;

namespace Mini_E_Commerce_Project.Contexts
{
    public class AppDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDetailConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
