using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    public class StaffType : SearchParameterBase<string>
    {
        public const string Manager = "manager";

        public const string HeadCoach = "headCoach";

        public const string GkCoach = "GkCoach";

        public const string FitnessCoach = "fitnessCoach";

        public const string PhysioCoach = "physio";

        private StaffType(string descripton, string value)
        {
            Description = descripton;
            Value = value;
        }

        public static IEnumerable<StaffType> GetAll()
        {
            yield return new StaffType("manager", Manager);
            yield return new StaffType("headCoach", HeadCoach);
            yield return new StaffType("GkCoach", GkCoach);
            yield return new StaffType("fitnessCoach", FitnessCoach);
            yield return new StaffType("physio", PhysioCoach);
 
        }
    }
}