using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    public class Position : SearchParameterBase<string>
    {
        public const string Defenders = "defense";

        public const string Midfielders = "midfield";

        public const string Attackers = "attacker";

        public const string GoalKeeper = "GK";

        public const string RightWingBack = "RWB";

        public const string RightBack = "RB";

        public const string CenterBack = "CB";

        public const string LeftBack = "LB";

        public const string LeftWingBack = "LWB";

        public const string CentralDefensiveMidfielder = "CDM";

        public const string RightMidfielder = "RM";

        public const string CentralMidfielder = "CM";

        public const string LeftMidfielder = "LM";

        public const string CentralAttackingMidfielder = "CAM";

        public const string RightForward = "RF";

        public const string CentralForward = "CF";

        public const string LeftForward = "LF";

        public const string RightWinger = "RW";

        public const string Striker = "ST";

        public const string LeftWinger = "LW";

        private Position(string descripton, string value)
        {
            Description = descripton;
            Value = value;
        }

        public static IEnumerable<Position> GetAll()
        {
            yield return new Position("Defenders", Defenders);
            yield return new Position("Midfielders", Midfielders);
            yield return new Position("Attackers", Attackers);
            yield return new Position("Goalkeeper", GoalKeeper);
            yield return new Position("Right wing back", RightWingBack);
            yield return new Position("Right back", RightBack);
            yield return new Position("Center back", CenterBack);
            yield return new Position("Left back", LeftBack);
            yield return new Position("Left wing back", LeftWingBack);
            yield return new Position("Central defensive midfielder", CentralDefensiveMidfielder);
            yield return new Position("Right midfielder", RightMidfielder);
            yield return new Position("Central midfielder", CentralMidfielder);
            yield return new Position("Left midfielder", LeftMidfielder);
            yield return new Position("Central attacking midfielder", CentralAttackingMidfielder);
            yield return new Position("Right forward", RightForward);
            yield return new Position("Center forward", CentralForward);
            yield return new Position("Left forward", LeftForward);
            yield return new Position("Right winger", RightWinger);
            yield return new Position("Striker", Striker);
            yield return new Position("Left winger", LeftWinger);
        }
    }
}