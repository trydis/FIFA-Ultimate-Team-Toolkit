using System.Collections.Generic;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit.Models
{
    public class ItemData
    {
        public long AssetId { get; set; }

        public ushort Assists { get; set; }

        public ushort Gold { get; set; }

        public ushort Silver { get; set; }

        public ushort Bronze { get; set; }

        public int WeightRare { get; set; }

        public int Amount { get; set; }

        public int? Negotiation { get; set; }

        public string Description { get; set; }

        public string BioDescription { get; set; }

        public List<Attribute> AttributeList { get; set; }

        public ushort Category { get; set; }

        public int Capacity { get; set; }

        public string Name { get; set; }

        public ushort CardSubTypeId { get; set; }

        public ushort CardAssetId { get; set; }

        public byte Contract { get; set; }

        public bool Dream { get; set; }

        public uint? DiscardValue { get; set; }

        public int? Value { get; set; }

        public int? Weightrare { get; set; }

        public string Header { get; set; }

        public string Manufacturer { get; set; }

        public int? Year { get; set; }

        public byte Fitness { get; set; }

        public string Formation { get; set; }

        public long Id { get; set; }

        public byte InjuryGames { get; set; }

        public string InjuryType { get; set; }

        public string ItemState { get; set; }

        public string ItemType { get; set; }

        public uint LastSalePrice { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public uint LeagueId { get; set; }

        public ushort LifeTimeAssists { get; set; }

        public ushort Loans { get; set; }

        public List<Attribute> LifeTimeStats { get; set; }

        public byte LoyaltyBonus { get; set; }

        public uint MarketDataMaxPrice { get; set; }

        public uint MarketDataMinPrice { get; set; }

        public byte Morale { get; set; }

        public byte Owners { get; set; }

        public ChemistryStyle PlayStyle { get; set; }

        public string PreferredPosition { get; set; }

        public byte RareFlag { get; set; }

        public byte Rating { get; set; }

        public long ResourceId { get; set; }

        public List<Attribute> StatsList { get; set; }

        public byte Suspension { get; set; }

        public uint TeamId { get; set; }

        public uint StadiumId { get; set; }

        public string Timestamp { get; set; }

        public int Training { get; set; }

        public bool Untradeable { get; set; }

        public int Pile { get; set; }

        public int Nation { get; set; }

        public TrainingItem TrainingItem { get; set; }

        public string ResourceGameYear { get; set; }

        public string FieldPos { get; set; }

        public string PosBonus { get; set; }

        public string GuidAssetId { get; set; }

        public List<uint> AttributeArray { get; set; }

        public List<uint> StatsArray { get; set; }

        public List<uint> LifetimeStatsArray { get; set; }

        public int Skillmoves { get; set; }

        public int WeakFootAbilityTypeCode { get; set; }

        public int AttackingWorkrate { get; set; }

        public int DefensiveWorkrate { get; set; }

        public int Trait1 { get; set; }

        public int Trait2 { get; set; }

        public int PreferredFoot { get; set; }
    }
}
