using System;
using System.Collections.Generic;

namespace SimpleInventoryAPI.DataAccess
{
    public class PurchaseOrder : BaseEntity<int>
    {
        public PurchaseOrder()
        {
            Items = new HashSet<PurchaseOrderItem>();
        }

        public string PurchaseOrderNumber { get; set; }
        public int SupplierId             { get; set; }
        public DateTime OrderDate         { get; set; }

        /* naviation property */
        public ICollection<PurchaseOrderItem> Items { get; set; }
    }
}
