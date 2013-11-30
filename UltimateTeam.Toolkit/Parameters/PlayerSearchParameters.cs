using System;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Parameters
{
    public class PlayerSearchParameters : SearchParameters
    {
        public PlayerSearchParameters()
            : base(ResourceType.Player)
        {
        }

        internal override string BuildUriString(ref string uriString)
        {
            if (League > 0)
                uriString += "&leag=" + League;

            SetLevel(ref uriString);

            if (Nation > 0)
                uriString += "&nat=" + Nation;

            if (ChemistryStyle != ChemistryStyle.All)
                uriString += "&playStyle=" + (ushort)ChemistryStyle;

            if (Team > 0)
                uriString += "&team=" + Team;

            if (MinBuy > 0)
                uriString += "&minb=" + MinBuy;

            if (MaxBuy > 0)
                uriString += "&maxb=" + MaxBuy;

            if (MinBid > 0)
                uriString += "&micr=" + MinBid;

            if (MaxBid > 0)
                uriString += "&macr=" + MaxBid;

            if (ResourceId > 0)
                uriString += "&maskedDefId=" + ResourceId.CalculateBaseId(); // previously was "&definitionId="

            SetPosition(ref uriString);

            uriString += "&type=" + Type.ToLower();

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

        private void SetPosition(ref string uriString)
        {
            if (!string.IsNullOrEmpty(Position))
                uriString += (
                    Position == Parameters.Position.Defenders ||
                    Position == Parameters.Position.Midfielders ||
                    Position == Parameters.Position.Attackers
                        ? "&zone="
                        : "&pos=")
                             + Position;
        }
    }
}