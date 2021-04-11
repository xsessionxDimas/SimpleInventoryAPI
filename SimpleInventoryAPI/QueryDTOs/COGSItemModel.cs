namespace SimpleInventoryAPI.QueryDTOs
{
    public class COGSItemModel
    {
        public string Component        { get; set; }
        public int Usage               { get; set; }
        public decimal CostPerUnit     { get; set; }
        public decimal? FreightPerUnit { get; set; }
        public decimal Total           { get; set; }
        public string Notes            { get; set; }
    }
}
