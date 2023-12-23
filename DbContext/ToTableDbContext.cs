using Microsoft.EntityFrameworkCore;

namespace ToTable.Models
{
    public class ToTableDbContext : DbContext
    {
        public ToTableDbContext(DbContextOptions<ToTableDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> OrderItems { get; set; } = null!;
        public DbSet<OrderItem> OrderItemItems { get; set; } = null!;
        public DbSet<Payment> PaymentItems { get; set; } = null!;
        public DbSet<Product> ProductItems { get; set; } = null!;
        public DbSet<Table> TableItems { get; set; } = null!;
        public DbSet<Waiter> WaiterItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja relacji między Order a Payment
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Opcjonalne - określa zachowanie kasowania w przypadku usunięcia zamówienia

            // Konfiguracja relacji między OrderItem a Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Opcjonalne - określa zachowanie kasowania w przypadku usunięcia zamówienia
        }
    }
}


    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //
    //     modelBuilder.Entity<Order>()
    //         .WithOne(p => p.Order)
    //                     .HasForeignKey<Payment>(p => p.Order); .HasOne(o => o.Payment)
    //         
    // }

