namespace SimpleInventoryAPI.DataAccess
{
    public class StockOpnameProduct : BaseEntity<int>
    {
        public int HeaderId    { get; set; }
        public int ProductId   { get; set; }
        public int ExpectedQty { get; set; }
        public int ActualQty   { get; set; }
        public string Remarks  { get; set; }
    }
}
