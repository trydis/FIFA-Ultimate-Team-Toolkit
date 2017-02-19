using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class ConsumablesDetails
    {
        public uint Count { get; set; }

        public ushort? DiscardValue { get; set; }

        public long ResourceId { get; set; }

        public uint UntradeableCount { get; set; }
    }
}
