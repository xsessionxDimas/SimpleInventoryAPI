namespace SimpleInventoryAPI.DataAccess
{
    public class ProductComponentItem : BaseEntity<int>
    {       
        public int HeaderId            { get; set; }
        public int ComponentId         { get; set; }
        public int Usage               { get; set; }
        public decimal CostPerUnit     { get; set; }
        public decimal? FreightPerUnit { get; set; }
        public decimal Total           { get; set; }
        public string Notes            { get; set; }
        /* navigation property */
        public Component Component     { get; set; }       
    }
}
