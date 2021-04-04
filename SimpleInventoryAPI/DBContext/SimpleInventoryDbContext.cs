using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.DataAccess.Identity;
using SimpleInventoryAPI.QueryDTOs;
using System.Reflection;

namespace SimpleInventoryAPI.DBContext
{
    public class SimpleInventoryDbContext : IdentityDbContext<ApplicationUser>
    {
        public SimpleInventoryDbContext(DbContextOptions<SimpleInventoryDbContext> options) : base(options)
        {
            /* empty constructor */
        }

        /* properties */
        public DbSet<Supplier> Suppliers                 { get; set; }
        public DbSet<Product> Products                   { get; set; }
        public DbSet<Component> Components               { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        public DbSet<ProductBatch> ProductBatches        { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders       { get; set; }

        /* query */
        public DbSet<SelectTwoModel> SelectTwoModels   { get; set; }
        public DbSet<UserModel> UserModels             { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<SelectTwoModel>().HasNoKey();
            builder.Entity<UserModel>().HasNoKey();
            base.OnModelCreating(builder);
        }
    }
}
