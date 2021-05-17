namespace SimpleInventoryAPI.DataAccess
{
    public class StockOpnameComponent : BaseEntity<int>
    {
        public int HeaderId    { get; set; }
        public int ComponentId { get; set; }
        public int ExpectedQty { get; set; }
        public int ActualQty   { get; set; }
        public string Remarks  { get; set; }
    }
}
