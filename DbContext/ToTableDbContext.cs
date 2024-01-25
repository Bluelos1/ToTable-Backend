using Microsoft.EntityFrameworkCore;

namespace ToTable.Models
{
    public class ToTableDbContext : DbContext
    {
        public ToTableDbContext(DbContextOptions<ToTableDbContext> options)
            : base(options) { }

        public DbSet<Order> OrderObject { get; set; } = null!;
        public DbSet<OrderItem> OrderItemObject { get; set; } = null!;
        public DbSet<Product> ProductObject { get; set; } = null!;
        public DbSet<Table> TableObject { get; set; } = null!;
        public DbSet<Waiter> WaiterObject { get; set; } = null!;
        public DbSet<Restaurant> RestaurantObject { get; set; } = null!;

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Waiter>()
        //         .HasMany(e => e.Bars)
        //         .WithRequired(e => e.Foo)
        //         .HasForeignKey(e => e.FooId)
        //         .WillCascadeOnDelete(false);
        // }   

    }
    
}
