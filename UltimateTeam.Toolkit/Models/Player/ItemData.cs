using UltimateTeam.Toolkit.Models.Store;

namespace UltimateTeam.Toolkit.Models.Player
{
    /// <summary>
    /// Needs to be splitted by "ItemType". Implement an IItem Interface.
    /// </summary>
    public class ItemData
    {
        public int? Amount { get; set; }
        public bool? Authenticity { get; set; }
        public uint AssetId { get; set; }
        public int? Assists { get; set; }
        public int? AttackingWorkRate { get; set; }
        public List<int>? AttributeArray { get; set; }
        public List<ItemAttribute>? AttributeList { get; set; } = new List<ItemAttribute>();
        public bool? BallRestricted { get; set; }
        public bool? BannerRestricted { get; set; }
        public string? BioDescription { get; set; }
        public List<int>? BaseTraits { get; set; }
        public int? Bronze { get; set; }
        public int? Capacity { get; set; }
        public int? CardAssetId { get; set; }
        public int? CardSubTypeId { get; set; }
        public int? Category { get; set; }
        public int? ChantsCount { get; set; }
        public int? Contract { get; set; }
        public int? DefensiveWorkrate { get; set; }
        public string? Description { get; set; }
        public string? DetailDescription { get; set; }
        public int? DiscardValue { get; set; }
        public bool? Dream { get; set; }
        public string? Fitness { get; set; }
        public string? FirstName { get; set; }
        public string? Formation { get; set; }
        public int? Gender { get; set; }
        public List<int>? Groups { get; set; }
        public string? GuidAssetId { get; set; }
        public int? Gold { get; set; }
        public string? Header { get; set; }
        public List<int>? IconTraits { get; set; }
        public long Id { get; set; }
        public int? InjuryGames { get; set; }
        public string? InjuryType { get; set; }
        public bool? ItemPronoun { get; set; }
        public string? ItemState { get; set; }
        public string? ItemType { get; set; }
        public bool? IsPlatformSpecific { get; set; }
        public string? LastName { get; set; }
        public uint LastSalePrice { get; set; }
        public int? LeagueId { get; set; }
        public int? LifetimeAssists { get; set; }
        public List<ItemStat>? LifetimeStats { get; set; } = new List<ItemStat>();
        public List<int>? LifetimeStatsArray { get; set; }
        public LoansInfo? LoansInfo { get; set; }
        public ushort? Loans { get; set; }
        public int? LoyaltyBonus { get; set; }
        public string? Manufacturer { get; set; }
        public uint MarketDataMaxPrice { get; set; }
        public uint MarketDataMinPrice { get; set; }
        public int? Morale { get; set; }
        public bool? MyStadium { get; set; }
        public string? Name { get; set; }
        public int? Nation { get; set; }
        public int? Negotiation { get; set; }
        public int? Owners { get; set; }
        public List<string>? PossiblePositions { get; set; }
        public int? Pile { get; set; }
        public string? PreferredPosition { get; set; }
        public int? Preferredfoot { get; set; }
        public int? PreferredTime1 { get; set; }
        public int? PreferredTime2 { get; set; }
        public int? PreferredWeather { get; set; }
        public int? PlayStyle { get; set; }
        public uint RankId { get; set; }
        public int? Rareflag { get; set; }
        public int? Rating { get; set; }
        public int? ResourceGameYear { get; set; }
        public long ResourceId { get; set; }
        public int? Silver { get; set; }
        public int? Skillmoves { get; set; }
        public int? ShowCasePriority { get; set; }
        public int? StadiumId { get; set; }
        public List<int>? StatsArray { get; set; }
        public List<ItemStat>? StatsList { get; set; } = new List<ItemStat>();
        public int? Suspension { get; set; }
        public int? TeamId { get; set; }
        public int? Tier { get; set; }
        public bool? TifoRestricted { get; set; }
        public int? TifoSupportType { get; set; }
        public int? Timestamp { get; set; }
        public int? Training { get; set; }
        public bool? Untradeable { get; set; }
        public bool? UnDiscardable { get; set; }
        public int? WeakfootabilityTypecode { get; set; }
        public int? WeightRare { get; set; }
        public int? Value { get; set; }
        public int? Year { get; set; }
    }
}
