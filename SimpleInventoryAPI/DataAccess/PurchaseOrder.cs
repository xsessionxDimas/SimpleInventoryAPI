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
        public DateTime OrderDate         { get; set; }

        /* naviation property */
        public List<PurchaseOrderItem> Items { get; set; }
    }
}
