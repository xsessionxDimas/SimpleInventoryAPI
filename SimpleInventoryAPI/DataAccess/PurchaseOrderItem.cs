namespace SimpleInventoryAPI.DataAccess
{
    public class PurchaseOrderItem : BaseEntity<int>
    {
        public int PurchaseOrderId { get; set; }
        public int ComponentId     { get; set; }
        public int Qty             { get; set; }
        public decimal Price       { get; set; }
        public decimal Discount    { get; set; }
        public decimal Total       { get; set; }

        /* navigation property */
        public Component Component { get; set; }
    }
}
