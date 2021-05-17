namespace SimpleInventoryAPI.QueryDTOs
{
    public class POItemModel
    {
        public int RowNo           { get; set; }
        public int ComponentId     { get; set; }
        public string PartNumber   { get; set; }
        public string PartDesc     { get; set; }
        public int Qty             { get; set; }
        public decimal Price       { get; set; }
        public decimal Discount    { get; set; } = 0;
        public decimal Total       { get; set; }
    }
}
