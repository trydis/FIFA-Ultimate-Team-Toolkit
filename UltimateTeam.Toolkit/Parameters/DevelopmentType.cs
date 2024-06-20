namespace UltimateTeam.Toolkit.Parameters
{
    public class DevelopmentType : SearchParameterBase<string>
    {
        public const string Contract = "contract";

        public const string Healing = "healing";



        private DevelopmentType(string descripton, string value)
        {
            Description = descripton;
            Value = value;
        }

        public static IEnumerable<DevelopmentType> GetAll()
        {
            yield return new DevelopmentType("contract", Contract);
            yield return new DevelopmentType("healing", Healing);

        }
    }
}