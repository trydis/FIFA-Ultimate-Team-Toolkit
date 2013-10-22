namespace UltimateTeam.Toolkit.Parameters 
{
    public class BallSearchParameters : SearchParameters
    {
        public BallSearchParameters()
            : base(ResourceType.Ball)
        {
        }

        internal override string BuildUriString(ref string uriString)
        {
      
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
    }
}

     