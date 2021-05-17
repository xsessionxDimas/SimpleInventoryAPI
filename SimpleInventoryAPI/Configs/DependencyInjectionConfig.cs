using Microsoft.Extensions.DependencyInjection;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.Repositories;
using SimpleInventoryAPI.Services;

namespace SimpleInventoryAPI.Configs
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            /* repositories */
            services.AddScoped<IRepository<Supplier, int>, SupplierRepository>();
            services.AddScoped<IRepository<CurrencyRate, int>, CurrencyRateRepository>();
            services.AddScoped<IRepository<Product, int>, ProductRepository>();
            services.AddScoped<IRepository<Component, int>, ComponentRepository>();
            services.AddScoped<IRepository<ProductComponent, int>, ProductComponentRepository>();
            services.AddScoped<IRepository<ProductBatch, int>, ProductBatchRepository>();
            services.AddScoped<IRepository<PurchaseOrder, int>, PurchaseOrderRepository>();

            /* services */
            services.AddScoped<SupplierService>();
            services.AddScoped<CurrencyRateService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ComponentService>();
            services.AddScoped<ProductComponentService>();
            services.AddScoped<ProductBatchService>();
            services.AddScoped<PurchaseOrderService>();
            services.AddScoped<RoleService>();
            services.AddScoped<UserService>();

            /* queries */
            services.AddScoped<SelectTwoQuery>();
            services.AddScoped<UserQuery>();
            services.AddScoped<COGSQuery>();
            services.AddScoped<PurchaseOrderQuery>();
        }
    }
}
