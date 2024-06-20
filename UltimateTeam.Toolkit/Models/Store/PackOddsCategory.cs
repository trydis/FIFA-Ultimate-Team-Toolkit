namespace UltimateTeam.Toolkit.Models.Store
{
    public class PackOddsCategory
    {
        public int? CategoryId { get; set; }
        public int? TierId { get; set; }
        public int? RarityAssetId { get; set; }
        public int? Order { get; set; }
        public string? Description { get; set; }
        public int? QualityId { get; set; }
        public List<PackOddsRule>? PackOddsRules { get; set; }
    }
}
