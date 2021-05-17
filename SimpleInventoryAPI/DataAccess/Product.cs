using System.Collections.Generic;

namespace SimpleInventoryAPI.DataAccess
{
    public class Product : BaseEntity<int>
    {
        public Product()
        {
            ProductComponents = new HashSet<ProductComponent>();
            Batches           = new HashSet<ProductBatch>();
        }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public float VAT          { get; set; }
        public float SalesFee     { get; set; }
        public decimal GrossSales { get; set; }
        public int InStock        { get; set; }

        /* navigation property */
        public ICollection<ProductComponent> ProductComponents { get; set; }
        public ICollection<ProductBatch> Batches               { get; set; }
    }
}
