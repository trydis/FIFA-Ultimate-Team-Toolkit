using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class UserData
    {
        public string Count { get; set; }

        public List<UserDataInfo> Data { get; set; }

        public string Offset { get; set; }

        public string TotalRecords { get; set; }
    }
}