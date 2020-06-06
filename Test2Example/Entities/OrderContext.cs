using Microsoft.EntityFrameworkCore;

namespace Test2Example.Entities
{
    public class OrderContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Confectionery> Confectioneries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Confectionery_Order> ConfectioneryOrders { get; set; }

        public OrderContext()
        {
        }

        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmpl);
                entity.Property(e => e.IdEmpl).ValueGeneratedOnAdd();
                entity.ToTable("Employee");
                entity.HasMany(e => e.Orders)
                    .WithOne(o => o.Employee)
                    .HasForeignKey(o => o.IdEmployee)
                    .IsRequired();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdClient);
                entity.Property(e => e.IdClient).ValueGeneratedOnAdd();
                entity.ToTable("Customer");
                entity.HasMany(e => e.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.IdCustomer)
                    .IsRequired();
            });
            modelBuilder.Entity<Confectionery>(entity =>
            {
                entity.HasKey(e => e.IdConfect);
                entity.Property(e => e.IdConfect).ValueGeneratedOnAdd();
                entity.ToTable("Confectionery");
                entity.HasMany(e => e.Conf_Orders)
                    .WithOne(o => o.Confectionery)
                    .HasForeignKey(o => o.IdConfect)
                    .IsRequired();
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder);
                entity.Property(e => e.IdOrder).ValueGeneratedOnAdd();
                entity.ToTable("Order");
                entity.HasMany(e => e.Conf_Orders)
                    .WithOne(o => o.Order)
                    .HasForeignKey(o => o.IdOrder)
                    .IsRequired();
            });

            modelBuilder.Entity<Confectionery_Order>(entity =>
            {
                entity.HasKey(e => new {e.IdConfect, e.IdOrder});
                entity.ToTable("Confectionery_Order");
            });

        }
    }
}