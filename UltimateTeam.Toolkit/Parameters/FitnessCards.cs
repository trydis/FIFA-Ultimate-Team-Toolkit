using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    public class FitnessCard : SearchParameterBase<uint>
    {
        public const uint ShinyBronzeFitness = 1615614740;
        public const uint ShinySilverFitness = 1615614741;
        public const uint ShinyGoldFitness = 1615614742;
        public const uint NonShinyBronzeFitness = 1615614737;
        public const uint NonShinySilverFitness = 1615614738;
        public const uint NonShinyGoldFitness = 1615614739;
        
        private FitnessCard(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<FitnessCard> GetAll()
        {
            yield return new FitnessCard("Shiny Bronze Fitness", ShinyBronzeFitness);
            yield return new FitnessCard("Shiny Silver Fitness", ShinySilverFitness);
            yield return new FitnessCard("Shiny Gold Fitness", ShinyGoldFitness);
            yield return new FitnessCard("Non-Shiny Bronze Fitness", NonShinyBronzeFitness);
            yield return new FitnessCard("Non-Shiny Silver Fitness", NonShinySilverFitness);
            yield return new FitnessCard("Non-Shiny Gold Fitness", NonShinyGoldFitness);
            
        }
    }
}
