using System;
using System.Collections.Generic;

namespace SimpleInventoryAPI.DataAccess
{
    public class StockOpname : BaseEntity<int>
    {
        public StockOpname()
        {
            Products   = new List<StockOpnameProduct>();
            Components = new List<StockOpnameComponent>();
        }

        public DateTime StockOpnameDate { get; set; }
        public string Remarks           { get; set; }

        /* naviation property */
        public List<StockOpnameProduct> Products     { get; set; }
        public List<StockOpnameComponent> Components { get; set; }

    }
}
