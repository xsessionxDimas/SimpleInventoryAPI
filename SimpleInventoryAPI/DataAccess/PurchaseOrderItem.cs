namespace SimpleInventoryAPI.DataAccess
{
    public class PurchaseOrderItem : BaseEntity<int>
    {
        public int PurchaseOrderId { get; set; }
        public int ComponentId     { get; set; }
        public int Qty             { get; set; }

        /* navigation property */
        public Component Component { get; set; }
    }
}
