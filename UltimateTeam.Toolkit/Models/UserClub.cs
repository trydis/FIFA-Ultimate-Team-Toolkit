using System;
using System.Collections.Generic;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Models
{
    public class UserClub
    {
        private string _lastAccessTime;

        private string _established;

        public string Year { get; set; }

        public string LastAccessTime
        {
            get { return _lastAccessTime; }
            set
            {
                _lastAccessTime = value;
                LastAccessDateTime = value.FromUnixTime();
            }
        }

        public DateTime LastAccessDateTime { get; set; }

        public string Platform { get; set; }

        public string ClubAbbr { get; set; }

        public string ClubName { get; set; }

        public uint TeamId { get; set; }

        public string Established
        {
            get { return _established; }
            set
            {
                _established = value;
                EstablishedDateTime = value.FromUnixTime();
            }
        }

        public DateTime EstablishedDateTime { get; set; }

        public byte DivisionOnline { get; set; }

        public uint BadgeId { get; set; }

        public Dictionary<string, long> SkuAccessList { get; set; }

        public uint AssetId { get; set; }
    }
}