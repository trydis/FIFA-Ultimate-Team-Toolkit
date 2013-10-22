using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class QuickSellResponse
    {
        public List<ItemData> Items { get; set; }

        public uint TotalCredits { get; set; }
    }
}