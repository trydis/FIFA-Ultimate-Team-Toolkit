using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class CreditsResponse
    {
        public uint Credits { get; set; }

        public List<Currency> Currencies { get; set; }

        public UnopenedPacks UnopenedPacks { get; set; }

        public BidTokens BidTokens { get; set; }
    }
}
