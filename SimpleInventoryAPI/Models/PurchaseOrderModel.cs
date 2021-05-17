using System;
using System.Collections.Generic;

namespace SimpleInventoryAPI.Models
{
    public class PurchaseOrderModel
    {
        public int Id                     { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int SupplierId             { get; set; }
        public DateTime OrderDate         { get; set; }
        public string Notes               { get; set; }
        public decimal SubTotal           { get; set; }
        public decimal Discount           { get; set; }
        public decimal Tax                { get; set; }
        public decimal Additional         { get; set; }
        public decimal GrandTotal         { get; set; }
        public string User                { get; set; }

        public IEnumerable<PurchaseOrderItemModel> Items { get; set; } = new List<PurchaseOrderItemModel>();
    }
}
