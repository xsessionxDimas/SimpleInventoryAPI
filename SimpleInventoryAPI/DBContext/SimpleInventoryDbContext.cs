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
        public DbSet<StockOpname> StockOpnames           { get; set; }
        public DbSet<CurrencyRate> CurrencyRates         { get; set; }

        /* query */
        public DbSet<SelectTwoModel> SelectTwoModels     { get; set; }
        public DbSet<UserModel> UserModels               { get; set; }
        public DbSet<COGSModel> COGS                     { get; set; }
        public DbSet<COGSItemModel> COGSItems            { get; set; }
        public DbSet<POModel> PO                         { get; set; }
        public DbSet<POItemModel> POItems                { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<SelectTwoModel>().HasNoKey();
            builder.Entity<UserModel>().HasNoKey();
            builder.Entity<COGSModel>().HasNoKey();
            builder.Entity<COGSItemModel>().HasNoKey();
            builder.Entity<POModel>().HasNoKey();
            builder.Entity<POItemModel>().HasNoKey();
            base.OnModelCreating(builder);
        }
    }
}
