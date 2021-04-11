namespace SimpleInventoryAPI.Models
{
    public class ProductComponentItemModel
    {
        public int Id                   { get; set; }
        public int HeaderId             { get; set; }
        public int ComponentId          { get; set; }
        public int Usage                { get; set; }
        public decimal CostPerUnit      { get; set; }
        public decimal? FreightPerUnit  { get; set; }
        public decimal Total            { get; set; }
        public string Notes             { get; set; }
        public string User              { get; set; }
    }
}
