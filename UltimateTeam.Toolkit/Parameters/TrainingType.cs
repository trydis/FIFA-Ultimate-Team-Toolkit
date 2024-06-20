namespace UltimateTeam.Toolkit.Parameters
{
    public class TrainingType : SearchParameterBase<string>
    {
        public const string ChemistryStyles = "playStyle";

        public const string ManagerLeagues = "managerLeagueModifier";


        private TrainingType(string descripton, string value)
        {
            Description = descripton;
            Value = value;
        }

        public static IEnumerable<TrainingType> GetAll()
        {
            yield return new TrainingType("playStyle", ChemistryStyles);
            yield return new TrainingType("managerLeagueModifier", ManagerLeagues);
        }
    }
}