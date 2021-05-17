namespace SimpleInventoryAPI.DataAccess
{
    public class Component : BaseEntity<int>
    {
        public string PartNumber      { get; set; }
        public string PartDescription { get; set; }
        public int InStock            { get; set; }
        public int Threshold          { get; set; }
    }
}
