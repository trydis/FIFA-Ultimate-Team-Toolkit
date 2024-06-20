namespace UltimateTeam.Toolkit.Models.Generic
{
    public class ScmpCategoryProgressList
    {
        public string GroupId { get; set; }
        public string State { get; set; }
        public string CategoryId { get; set; }
        public List<ObjectiveProgressList> ObjectiveProgressList { get; set; }
        public List<ScmpGroupProgressList> ScmpGroupProgressList { get; set; }
    }
}