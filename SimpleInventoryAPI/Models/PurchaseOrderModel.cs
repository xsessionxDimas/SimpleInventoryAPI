﻿using System;
using System.Collections.Generic;

namespace SimpleInventoryAPI.Models
{
    public class PurchaseOrderModel
    {
        public int Id                     { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int SupplierId             { get; set; }
        public DateTime OrderDate         { get; set; }
        public string User                { get; set; }

        public IEnumerable<PurchaseOrderItemModel> Items { get; set; } = new List<PurchaseOrderItemModel>();
    }
}
