using AutoMapper;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Configs
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig()
        {
            CreateMap<SupplierModel, Supplier>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<ComponentModel, Component>().ReverseMap();
            CreateMap<ProductComponentModel, ProductComponent>().ReverseMap();
            CreateMap<ProductBatchModel, ProductBatch>().ReverseMap();
            CreateMap<PurchaseOrderModel, PurchaseOrder>().ReverseMap();
            CreateMap<PurchaseOrderItemModel, PurchaseOrderItem>().ReverseMap();
        }
    }
}
