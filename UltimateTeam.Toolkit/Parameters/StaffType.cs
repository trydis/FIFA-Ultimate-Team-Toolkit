namespace UltimateTeam.Toolkit.Parameters
{
    public class StaffType : SearchParameterBase<string>
    {
        public const string Manager = "manager";

        private StaffType(string descripton, string value)
        {
            Description = descripton;
            Value = value;
        }

        public static IEnumerable<StaffType> GetAll()
        {
            yield return new StaffType("manager", Manager);
        }
    }
}