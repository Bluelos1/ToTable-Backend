

using Microsoft.EntityFrameworkCore;

namespace ToTable.Models;

public class ToTableDbContext : Microsoft.EntityFrameworkCore.DbContext
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
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //
    //     modelBuilder.Entity<Order>()
    //         .WithOne(p => p.Order)
    //                     .HasForeignKey<Payment>(p => p.Order); .HasOne(o => o.Payment)
    //         
    // }

}