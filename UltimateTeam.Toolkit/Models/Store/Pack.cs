using UltimateTeam.Toolkit.Models.Player;

namespace UltimateTeam.Toolkit.Models.Store
{
    public class Pack
    {
        public int? Id { get; set; }
        public string? State { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public int? AssetId { get; set; }
        public string? GuidAssetId { get; set; }
        public int? Coins { get; set; }
        public string? ActionType { get; set; }
        public string? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? FcCashPrice { get; set; }
        public string? CustomAttribute { get; set; }
        public List<PackCurrency>? Currencies { get; set; }
        public List<Category>? CategoryList { get; set; }
        public string? SaleType { get; set; }
        public string? DealType { get; set; }
        public int? SaleId { get; set; }
        public DisplayGroup? DisplayGroup { get; set; }
        public int? SortPriority { get; set; }
        public int? PurchaseLimit { get; set; }
        public int? PurchaseCount { get; set; }
        public bool? IsPremium { get; set; }
        public bool? IsSeasonTicketDiscount { get; set; }
        public long? PreviewCreateTime { get; set; }
        public long? PreviewExpireTime { get; set; }
        public ItemData? PreviewItem { get; set; }
        public bool? UseDefaultImage { get; set; }
        public string? PurchaseMethod { get; set; }
        public int? DisplayGroupAssetId { get; set; }
        public long? LastPurchasedTime { get; set; }
        public bool? DisplayGroupUseDefaultImage { get; set; }
        public bool? Unopened { get; set; }
        public string? PackType { get; set; }
        public PackContentInfo? PackContentInfo { get; set; }
        public List<PackOdds>? PackOdds { get; set; }
        public int? GlobalAvailableQuantity { get; set; }
        public bool? Untradeable { get; set; }
        public int? SubArticleId { get; set; }
        public bool? IsPreviewable { get; set; }
        public bool? WasRefreshed { get; set; }
        public bool? IsSegmented { get; set; }
        public bool? IsPlayerPickPack { get; set; }
        public long? Start { get; set; }
        public int? SecondsUntilStart { get; set; }
        public long? End { get; set; }
        public int? SecondsUntilEnd { get; set; }
        public int? Points { get; set; }
        public int? FirstPartyStoreId { get; set; }
        public ExtPrice? ExtPrice { get; set; }
        public PriceDetails? OriginalPrice { get; set; }
        public PriceDetails? FinalPrice { get; set; }
        public List<BundleItem>? BundleItems { get; set; }
        public bool? Visible { get; set; }
        public int? Bonus { get; set; }
        public string? NameLoc { get; set; }
        public string? DescriptionLoc { get; set; }
    }
}
