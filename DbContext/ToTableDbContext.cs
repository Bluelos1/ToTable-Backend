using Microsoft.EntityFrameworkCore;

namespace ToTable.Models
{
    public class ToTableDbContext : DbContext
    {
        public ToTableDbContext(DbContextOptions<ToTableDbContext> options)
            : base(options) { }

        public DbSet<Order> OrderItems { get; set; } = null!;
        public DbSet<OrderItem> OrderItemItems { get; set; } = null!;
        public DbSet<Payment> PaymentItems { get; set; } = null!;
        public DbSet<Product> ProductItems { get; set; } = null!;
        public DbSet<Table> TableItems { get; set; } = null!;
        public DbSet<Waiter> WaiterItems { get; set; } = null!;

    }
}
