using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CreativeCollab.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; } 
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<OrderVehicle> OrderVehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product → Supplier (many-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId);

            // Order → User (optional)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Order → Store (optional)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.StoreID)
                .OnDelete(DeleteBehavior.SetNull);

            // OrderDetail → Order (many-to-one)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDetail → Product (many-to-one)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // prevent delete of product if it's in an order

            // Configure the NEW M-M relationship for Order and Car via OrderVehicle
            // modelBuilder.Entity<OrderVehicle>()
            //     .HasKey(ov => new { ov.OrderId, ov.CarId }); 

            modelBuilder.Entity<OrderVehicle>()
                .HasOne(ov => ov.Order)
                .WithMany(o => o.OrderVehicles)
                .HasForeignKey(ov => ov.OrderId);

            modelBuilder.Entity<OrderVehicle>()
                .HasOne(ov => ov.Car)
                .WithMany(c => c.OrderVehicles)
                .HasForeignKey(ov => ov.CarId);

            modelBuilder.Entity<CarCategory>()
                .HasKey(cc => new { cc.CarID, cc.CategoryID });

            modelBuilder.Entity<CarCategory>()
                .HasOne(cc => cc.Car)
                .WithMany(c => c.CarCategories)
                .HasForeignKey(cc => cc.CarID);

            modelBuilder.Entity<CarCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.CarCategories)
                .HasForeignKey(cc => cc.CategoryID);

            //modelBuilder.Entity<CarOrder>()
            //    .HasOne(co => co.Car)
            //    .WithMany(c => c.CarOrders)
            //    .HasForeignKey(co => co.CarID);

            //modelBuilder.Entity<CarOrder>()
            //    .HasOne(co => co.User)
            //    .WithMany()
            //    .HasForeignKey(co => co.UserID);


        }

    }
}