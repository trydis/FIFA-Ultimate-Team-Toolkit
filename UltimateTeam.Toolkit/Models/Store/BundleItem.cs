namespace UltimateTeam.Toolkit.Models.Store
{
    public class BundleItem
    {
        public int? Id { get; set; }
        public long? ResourceId { get; set; }
        public int? AssetId { get; set; }
        public int? CardSubTypeId { get; set; }
        public int? CardAssetId { get; set; }
        public List<int>? AttributeArray { get; set; }
        public int? ItemId { get; set; }
        public string? ItemType { get; set; }
        public int? RareFlag { get; set; }
        public int? Rating { get; set; }
        public int? TeamId { get; set; }
        public int? Category { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public bool? Authenticity { get; set; }

    }
}
