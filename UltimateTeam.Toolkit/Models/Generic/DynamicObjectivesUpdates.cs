namespace UltimateTeam.Toolkit.Models.Generic
{
    public class DynamicObjectivesUpdates
    {
        public uint? NewDailyObjectives { get; set; }
        public List<uint>? CompletedDailyObjectives { get; set; }
        public string? NeedsGroupsRefresh { get; set; }
        public string? AutoRedeemedXp { get; set; }
        public string? NeedsAutoClaim { get; set; }
        public bool? NeedsMilestonesAutoClaim { get; set; }
        public ProgressOnAcademyObjectives? ProgressOnAcademyObjectives { get; set; }
        public List<ScmpCategoryProgressList>? ScmpCategoryProgressList { get; set; }
        public List<LearningGroupProgressList>? LearningGroupProgressList { get; set; }
    }
}