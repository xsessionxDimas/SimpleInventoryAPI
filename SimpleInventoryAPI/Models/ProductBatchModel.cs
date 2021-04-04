using System;

namespace SimpleInventoryAPI.Models
{
    public class ProductBatchModel
    {
        public int Id             { get; set; }
        public string BatchNo     { get; set; }
        public int ProductId      { get; set; }
        public int Qty            { get; set; }
        public DateTime BatchDate { get; set; }
        public string User        { get; set; }
    }
}
