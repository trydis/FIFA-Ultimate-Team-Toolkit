namespace UltimateTeam.Toolkit.Models
{
    public class PriceRange
    {
        public long DefId { get; set; }
        public long ItemId { get; set; }
        public uint MaxPrice { get; set; }
        public uint MinPrice { get; set; }
        public string Source { get; set; }
    }
}