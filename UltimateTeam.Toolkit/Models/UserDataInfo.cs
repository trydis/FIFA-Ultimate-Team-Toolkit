using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class UserDataInfo
    {
        public string DisplayName { get; set; }

        public string LastLogin { get; set; }

        public string LastLogout { get; set; }

        public string NucPersId { get; set; }

        public string NucUserId { get; set; }

        public List<UserDataInfoSettings> Settings { get; set; }

        public string Sku { get; set; }
    }
}