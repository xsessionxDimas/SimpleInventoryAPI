using System;

namespace SimpleInventoryAPI.DataAccess
{
    public class ProductBatch : BaseEntity<int>
    {
        public string BatchNo     { get; set; }
        public int ProductId      { get; set; }
        public int Qty            { get; set; }
        public DateTime BatchDate { get; set; }

        /* naviation property */
        public Product Product    { get; set; }
    }
}
