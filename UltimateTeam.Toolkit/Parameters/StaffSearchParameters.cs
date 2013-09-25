using System;

namespace UltimateTeam.Toolkit.Parameters 
{
    public class StaffSearchParameters : SearchParameters
    {
        public StaffSearchParameters()
            : base(ResourceType.Staff)
        {
        }

        internal override string BuildUriString(ref string uriString)
        {
        
            SetLevel(ref uriString);

            // only the manager type has Nation category
            if (Convert.ToBoolean(Nation) && StaffType == "manager")
            uriString += "&nat=" + Nation;

            // only the manager type has League category
            if (Convert.ToBoolean(League) && StaffType == "manager")
            uriString += "&leag=" + League;

            uriString += "&cat=" + StaffType;

            uriString += "&type=" + Type.ToLower();

            if (MinBuy > 0)
                uriString += "&minb=" + MinBuy;

            if (MaxBuy > 0)
                uriString += "&maxb=" + MaxBuy;

            if (MinBid > 0)
                uriString += "&micr=" + MinBid;

            if (MaxBid > 0)
                uriString += "&macr=" + MaxBid;


            return uriString;
        }

        private void SetLevel(ref string uriString)
        {
            switch (Level)
            {
                case Level.All:
                    break;
                case Level.Bronze:
                case Level.Silver:
                case Level.Gold:
                    uriString += "&lev=" + Level.ToString().ToLower();
                    break;
                default:
                    throw new ArgumentException("Level");
            }
        }

     
    }
}

     