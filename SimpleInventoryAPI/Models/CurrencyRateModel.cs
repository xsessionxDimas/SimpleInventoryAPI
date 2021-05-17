namespace SimpleInventoryAPI.Models
{
    public class CurrencyRateModel
    {
        public int Id          { get; set; }
        public string Currency { get; set; }
        public decimal Rate    { get; set; }
        public string User     { get; set; }
    }
}
