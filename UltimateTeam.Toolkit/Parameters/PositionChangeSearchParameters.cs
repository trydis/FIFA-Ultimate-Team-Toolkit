using System;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Parameters
{
    /// <summary>
    /// Search parameters for Consumable Search / Position Change
    /// </summary>
    public class PositionChangeSearchParameters : SearchParameters
    {

        public PositionChangeSearchParameters()
            : base(ResourceType.Training)
        {
        }

        internal override string BuildUriString(ref string uriString)
        {

            // Sample string: 
            // Request	POST /ut/game/fifa14/transfermarket?start=0&pos=LWB%2DLB&type=training&cat=position&num=16&lev=gold 
            // Quality level doesn't make sense since all position change cards are gold

            SetLevel(ref uriString);

            uriString += "&cat=position";

            uriString += "&type=" + Type.ToLower();

            if (MinBuy > 0)
                uriString += "&minb=" + MinBuy;

            if (MaxBuy > 0)
                uriString += "&maxb=" + MaxBuy;

            if (MinBid > 0)
                uriString += "&micr=" + MinBid;

            if (MaxBid > 0)
                uriString += "&macr=" + MaxBid;

            if (ChemistryStyle > 0)
                uriString += "&playStyle=" + (ushort)ChemistryStyle;

            if (Position != "")
                uriString += @"&pos=" + EncodePosString(Position);


            return uriString;
        }

        /// <summary>
        /// Url encoding the position change string
        /// </summary>
        /// <param name="poschange">a valid position change string</param>
        /// <returns></returns>
        private static string EncodePosString(string poschange)
        {
            var pc = (PosChangeEnum)Enum.Parse(typeof(PosChangeEnum), poschange, true);
            if (!Enum.IsDefined(typeof(PosChangeEnum), pc))
            {
                throw new ArgumentException("Position Change Parameter is invalid");
            }

            return poschange.Replace(@"_", @"%2D");
        }

        private void SetLevel(ref string uriString)
        {
            switch (Level)
            {
                case Level.All:
                    break;
                case Level.Bronze:
                    break;
                case Level.Silver:
                    break;
                case Level.Gold:
                    uriString += "&lev=" + Level.ToString().ToLower();
                    break;
                default:
                    throw new ArgumentException("Level");
            }
        }

    }
}
