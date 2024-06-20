namespace UltimateTeam.Toolkit.Models.Auction
{
    public class TradePileItem
    {
        public long Id { get; set; }

        public string? Pile { get; set; }

        public bool Success { get; set; }
        public string? Reason { get; set; }
        public string? ErrorCode { get; set; }
    }
}