namespace SimpleInventoryAPI.Models
{
    public class ComponentModel
    {
        public int Id                 { get; set; }
        public string PartNumber      { get; set; }
        public string PartDescription { get; set; }
        public int Stock              { get; set; }
        public int Threshold          { get; set; }
        public string User            { get; set; }
    }
}
