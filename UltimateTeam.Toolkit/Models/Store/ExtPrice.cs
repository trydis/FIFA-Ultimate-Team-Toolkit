namespace UltimateTeam.Toolkit.Models.Store
{
    public class ExtPrice
    {
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public PriceDetails? OriginalPrice { get; set; }
        public PriceDetails? FinalPrice { get; set; } // Neu hinzugefügt
    }
}
