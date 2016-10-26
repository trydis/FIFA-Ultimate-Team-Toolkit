using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class StoreResponse
    {
        public string Id { get; set; }

        public List<Pack> Purchase { get; set; }

        public string TimeStamp { get; set; }
    }
}