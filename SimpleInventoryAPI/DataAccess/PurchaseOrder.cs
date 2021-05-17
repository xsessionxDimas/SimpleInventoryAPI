using System;
using System.Collections.Generic;

namespace SimpleInventoryAPI.DataAccess
{
    public class PurchaseOrder : BaseEntity<int>
    {
        public PurchaseOrder()
        {
            Items = new List<PurchaseOrderItem>();
        }

        public string PurchaseOrderNumber { get; set; }
        public int SupplierId             { get; set; }
        public bool IsDraft               { get; set; } = true; 
        public DateTime OrderDate         { get; set; }
        public string Notes               { get; set; }
        public decimal SubTotal           { get; set; }
        public decimal Discount           { get; set; }
        public decimal Tax                { get; set; }
        public decimal Additional         { get; set; }
        public decimal GrandTotal         { get; set; }

        /* naviation property */
        public List<PurchaseOrderItem> Items { get; set; }
    }
}
