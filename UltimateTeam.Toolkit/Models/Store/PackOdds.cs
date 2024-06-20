namespace UltimateTeam.Toolkit.Models.Store
{
    public class PackOdds
    {
        public int? PackId { get; set; }
        public int? TierId { get; set; }
        public int? CategoryId { get; set; }
        public int? RarityId { get; set; }
        public long? StartTimeSec { get; set; }
        public string? OddsFormatted { get; set; }
        public int? PlayerCount { get; set; }
        public PackOddsCategory? PackOddsCategory { get; set; }
    }
}
