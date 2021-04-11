using System.Collections.Generic;

namespace SimpleInventoryAPI.DataAccess
{
    public class ProductComponent : BaseEntity<int>
    {
        public ProductComponent()
        {
            Items = new List<ProductComponentItem>();
        }

        public int ProductId           { get; set; }
        public string Type             { get; set; }       

        /* navigation property */
        public Product Product         { get; set; }
        public List<ProductComponentItem> Items { get; set; }
    }
}
