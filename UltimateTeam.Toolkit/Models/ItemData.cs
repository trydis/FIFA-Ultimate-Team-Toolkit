using System.Collections.Generic;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit.Models
{
    public class ItemData
    {
        public long AssetId { get; set; }

        public ushort Assists { get; set; }

        public List<Attribute> AttributeList { get; set; }

        public ushort CardSubTypeId { get; set; }

        public byte Contract { get; set; }

        public ushort? DiscardValue { get; set; }

        public byte Fitness { get; set; }

        public string Formation { get; set; }

        public long Id { get; set; }

        public byte InjuryGames { get; set; }

        public string InjuryType { get; set; }

        public string ItemState { get; set; }

        public string ItemType { get; set; }

        public uint LastSalePrice { get; set; }

		public uint LeagueId { get; set; }

        public ushort LifeTimeAssists { get; set; }

        public List<Attribute> LifeTimeStats { get; set; }

        public byte LoyaltyBonus { get; set; }

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

        public string Timestamp { get; set; }

        public int Training { get; set; }

        public bool Untradeable { get; set; }
    }
}
