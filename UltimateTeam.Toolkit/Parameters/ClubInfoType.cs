using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    public class ClubInfoType : SearchParameterBase<string>
    {
        public const string Kit = "kit";

        public const string Badge = "badge";



        private ClubInfoType(string descripton, string value)
        {
            Description = descripton;
            Value = value;
        }

        public static IEnumerable<ClubInfoType> GetAll()
        {
            yield return new ClubInfoType("kit", Kit);
            yield return new ClubInfoType("badge", Badge);   
        }
    }
}