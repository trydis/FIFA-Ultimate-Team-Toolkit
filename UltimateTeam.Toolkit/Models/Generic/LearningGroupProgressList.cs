namespace UltimateTeam.Toolkit.Models.Generic
{
    public class LearningGroupProgressList
    {
        public int CategoryId { get; set; }
        public List<ScmpGroupProgressList>? ScmpGroupProgressList { get; set; }
    }
}