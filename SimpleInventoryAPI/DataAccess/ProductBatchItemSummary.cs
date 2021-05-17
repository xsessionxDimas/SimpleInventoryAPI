namespace SimpleInventoryAPI.DataAccess
{
    public class ProductBatchItemSummary : BaseEntity<int>
    {
        public int HeaderId        { get; set; }
        public int ComponentId     { get; set; }
        public int StandardUsage   { get; set; }
        public int Spoiled         { get; set; }
        public int AdditionalCOGS  { get; set; }
        public int ActualCOGS      { get; set; }
        public string Notes        { get; set; }
        /* navigation property */
        public Component Component { get; set; }
    }
}
