using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class ItemData
    {
        public List<Attribute> AttributeList { get; set; }

        public uint CardSubTypeId { get; set; }

        public uint Contract { get; set; }

        public uint? DiscardValue { get; set; }

        public uint Fitness { get; set; }

        public string Formation { get; set; }

        public long Id { get; set; }

        public uint InjuryGames { get; set; }

        public string InjuryType { get; set; }

        public string ItemState { get; set; }

        public uint LastSalePrice { get; set; }

        public List<Attribute> LifeTimeStats { get; set; }

        public uint Morale { get; set; }

        public uint Owners { get; set; }

        public string PreferredPosition { get; set; }

        public uint RareFlag { get; set; }

        public uint Rating { get; set; }

        public long ResourceId { get; set; }

        public List<Attribute> StatsList { get; set; }

        public uint Suspension { get; set; }

        public uint TeamId { get; set; }

        public string Timestamp { get; set; }

        public uint Training { get; set; }
    }
}