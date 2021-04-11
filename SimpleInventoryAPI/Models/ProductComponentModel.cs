using System.Collections.Generic;

namespace SimpleInventoryAPI.Models
{
    public class ProductComponentModel
    {
        public int Id         { get; set; }
        public int ProductId  { get; set; }
        public string Type    { get; set; }   
        public string User    { get; set; }
        
        public List<ProductComponentItemModel> Items { get; set; }
    }
}
