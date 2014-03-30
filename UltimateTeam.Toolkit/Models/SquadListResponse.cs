using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class SquadListResponse
    {
        public ushort ActiveSquadId { get; set; }

        public List<SquadDetailsResponse> Squad { get; set; }
    }
}
