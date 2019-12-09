using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class ScmpCategoryProgressList
    {
        public string groupId { get; set; }
        public string state { get; set; }
        public string categoryId { get; set; }
        public List<ObjectiveProgressList> objectiveProgressList { get; set; }
        public List<ScmpGroupProgressList> scmpGroupProgressList { get; set; }
    }
}