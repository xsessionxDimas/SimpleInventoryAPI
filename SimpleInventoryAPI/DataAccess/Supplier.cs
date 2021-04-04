namespace SimpleInventoryAPI.DataAccess
{
    public class Supplier : BaseEntity<int>
    {
        public string SupplierName  { get; set; }
        public string Address       { get; set; }
        public string ContactPerson { get; set; }
        public string Phone         { get; set; }
    }
}
