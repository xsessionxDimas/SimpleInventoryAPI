using System;

namespace SimpleInventoryAPI.QueryDTOs
{
    public class POModel
    {
        public int RowNo                  { get; set; }
        public int Id                     { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Supplier            { get; set; }
        public bool IsDraft               { get; set; } = true; 
        public DateTime OrderDate         { get; set; }
        public string Notes               { get; set; }
        public decimal SubTotal           { get; set; }
        public decimal Discount           { get; set; }
        public decimal Tax                { get; set; }
        public decimal Additional         { get; set; }
        public decimal GrandTotal         { get; set; }
    }
}
