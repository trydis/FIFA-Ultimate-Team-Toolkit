using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class DynamicObjectivesUpdates
    {
        public uint newDailyObjectives { get; set; }
        public List<uint> completedDailyObjectives { get; set; }
        public string needsGroupsRefresh { get; set; }
        public string autoRedeemedXp { get; set; }
        public string needsAutoClaim { get; set; }
        public List<ScmpCategoryProgressList> scmpCategoryProgressList { get; set; }
    }
}