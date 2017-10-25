using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class TrainingItem
    {
        public int Id { get; set; }
        public int Timestamp { get; set; }
        public bool Untradeable { get; set; }
        public int Rating { get; set; }
        public int ResourceId { get; set; }
        public int Owners { get; set; }
        public int Cardsubtypeid { get; set; }
        public int LastSalePrice { get; set; }
        public int Morale { get; set; }
        public int Fitness { get; set; }
        public int InjuryGames { get; set; }
        public List<Attribute> StatsList { get; set; }
        public List<Attribute> LifeTimeStats { get; set; }
        public int Training { get; set; }
        public int Contract { get; set; }
        public int Suspension { get; set; }
        public List<Attribute> AttributeList { get; set; }
        public int Pile { get; set; }
        public int Nation { get; set; }
        public int Cardassetid { get; set; }
        public int Weightrare { get; set; }
        public int Amount { get; set; }
    }
}
