using System;

namespace UltimateTeam.Toolkit.Parameters 
{
    public class StadiumSearchParameters : SearchParameters
    {
        public StadiumSearchParameters()
            : base(ResourceType.Stadium)
        {
        }

        internal override string BuildUriString(ref string uriString)
        {
        
            SetLevel(ref uriString);

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

     