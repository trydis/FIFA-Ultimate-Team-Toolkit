namespace UltimateTeam.Toolkit.Extensions
{
    public static class LongExtensions
    {
        public static long CalculateBaseId(this long resourceId)
        {
            var baseId = resourceId;
            var version = 0;

            while (baseId > 16777216)
            {
                version++;
                switch (version)
                {
                    case 1:
                        baseId -= 1342177280;
                        break;
                    case 2:
                        baseId -= 50331648;
                        break;
                    default:
                        baseId -= 16777216;
                        break;
                }
            }

            return baseId;
        }
    }
}