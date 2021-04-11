namespace SimpleInventoryAPI.Models
{
    public class ProductModel
    {
        public int Id             { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public float VAT          { get; set; }
        public float SalesFee     { get; set; }
        public decimal GrossSales { get; set; }
        public string User        { get; set; }
    }
}
