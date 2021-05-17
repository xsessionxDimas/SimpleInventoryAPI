namespace SimpleInventoryAPI.DataAccess
{
    public class CurrencyRate : BaseEntity<int>
    {
        public string Currency { get; set; }
        public decimal Rate    { get; set; }
    }
}
