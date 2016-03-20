using System;

namespace UltimateTeam.Toolkit.Extensions
{
    internal static class DateTimeExtensions
    {
        public static void ThrowIfInvalidArgument(this DateTime input)
        {
            if (DateTime.TryParse(input.ToString(), out input) == false)
            {
                throw new ArgumentException();
            }
        }

        public static long ToUnixTime(this DateTime input)
        {
            var duration = input - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)(1000 * duration.TotalSeconds);
        }
    }
}