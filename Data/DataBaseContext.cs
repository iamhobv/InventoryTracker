using InventoryTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Data
{
    public class DataBaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ArchivedInventoryTransaction> ArchivedInventoryTransactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductWarehouse>().HasKey(pw => new { pw.WarehouseId, pw.ProductID });
            base.OnModelCreating(builder);
        }
    }
}
