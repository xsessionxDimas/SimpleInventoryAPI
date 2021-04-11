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
            CreateMap<ProductComponentModel, ProductComponent>()
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ProductComponentItemModel, ProductComponentItem>().ReverseMap();
            CreateMap<ProductBatchModel, ProductBatch>().ReverseMap();
            CreateMap<PurchaseOrderModel, PurchaseOrder>().ReverseMap();
            CreateMap<PurchaseOrderItemModel, PurchaseOrderItem>().ReverseMap();
        }
    }
}
