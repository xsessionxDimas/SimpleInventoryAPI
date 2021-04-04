namespace SimpleInventoryAPI.Models
{
    public class ProductComponentModel
    {
        public int Id                  { get; set; }
        public int ProductId           { get; set; }
        public int ComponentId         { get; set; }
        public int Usage               { get; set; }
        public decimal CostPerUnit     { get; set; }
        public decimal? FreightPerUnit { get; set; }
        public string User             { get; set; }
    }
}
