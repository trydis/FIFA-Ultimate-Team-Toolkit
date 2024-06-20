namespace UltimateTeam.Toolkit.Models.Generic
{
    public class ScmpGroupProgressList
    {
        public int? GroupId { get; set; }
        public string? GroupType { get; set; }
        public string? ObjectiveId { get; set; }
        public string? State { get; set; }
        public string? ProgressCount { get; set; }
        public List<ObjectiveProgressList>? ObjectiveProgressList { get; set; }
    }
}