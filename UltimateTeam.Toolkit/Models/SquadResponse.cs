using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimateTeam.Toolkit.Models
{
    public class SquadPlayer
    {
        public ushort? index { get; set; }

        public ItemData itemData { get; set; }

        public ushort? kitNumber { get; set; }

        public ushort? loyaltyBonus { get; set; }
    }
    
    public class kicktaker
    {
        public long id { get; set; }

        public ushort index { get; set; }
    }

    public class SquadDetailResponse
    {
        public List<ItemData> actives { get; set; }

        public long? captain { get; set; } //seems to be resource ID

        public ushort? changed { get; set; }

        public ushort chemistry { get; set; }

        public string formation { get; set; }

        public ushort id { get; set; }

        public List<kicktaker> kicktakers { get; set; }

        public List<ItemData> manager { get; set; }

        public ushort? newsquad { get; set; }

        public long? personaId { get; set; }

        public List<SquadPlayer> players { get; set; }

        public ushort? rating { get; set; }

        public string squadName { get; set; }

        public ushort starRating { get; set; }

        public bool valid { get; set; }

    }

    public class SquadListResponse
    {
        public ushort activeSquadId { get; set; }

        public List<SquadDetailResponse> squad { get; set; }
    }

}
