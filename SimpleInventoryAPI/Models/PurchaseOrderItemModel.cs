﻿namespace SimpleInventoryAPI.Models
{
    public class PurchaseOrderItemModel
    {
        public int Id              { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ComponentId     { get; set; }
        public int Qty             { get; set; }
    }
}
