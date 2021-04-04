namespace SimpleInventoryAPI.DataAccess
{
    public class ProductComponent : BaseEntity<int>
    {
        public int ProductId           { get; set; }
        public int ComponentId         { get; set; }
        public int Usage               { get; set; }
        public decimal CostPerUnit     { get; set; }
        public decimal? FreightPerUnit { get; set; }

        /* navigation property */
        public Product Product         { get; set; }
        public Component Component     { get; set; }
    }
}
