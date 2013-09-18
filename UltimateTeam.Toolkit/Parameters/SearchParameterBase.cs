namespace UltimateTeam.Toolkit.Parameters
{
    public abstract class SearchParameterBase<TValue>
    {
        public string Description { get; set; }

        public TValue Value { get; set; }

        public TValue Parent { get; set; }
    }
}