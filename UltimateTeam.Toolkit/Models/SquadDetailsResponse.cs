using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class SquadDetailsResponse
    {
        public List<ItemData> Actives { get; set; }

        public long? Captain { get; set; } //seems to be resource ID

        public ushort? Changed { get; set; }

        public ushort Chemistry { get; set; }

        public string Formation { get; set; }

        public ushort Id { get; set; }

        public List<Kicktaker> Kicktakers { get; set; }

        public List<ItemData> Manager { get; set; }

        public ushort? Newsquad { get; set; }

        public long? PersonaId { get; set; }

        public List<SquadPlayer> Players { get; set; }

        public ushort? Rating { get; set; }

        public string SquadName { get; set; }

        public ushort StarRating { get; set; }

        public bool Valid { get; set; }

    }
}