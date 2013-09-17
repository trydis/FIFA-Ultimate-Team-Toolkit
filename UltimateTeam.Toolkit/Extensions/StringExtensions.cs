using System;

namespace UltimateTeam.Toolkit.Extensions
{
    internal static class StringExtensions
    {
        public static void ThrowIfInvalidArgument(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException();
            }
        }

        public static DateTime FromUnixTime(this string unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(Convert.ToDouble(unixTime));
        }
    }
}