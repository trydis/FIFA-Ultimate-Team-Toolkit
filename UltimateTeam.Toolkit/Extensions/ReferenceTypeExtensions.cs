using System;

namespace UltimateTeam.Toolkit.Extensions
{
    internal static class ReferenceTypeExtensions
    {
        public static void ThrowIfNullArgument<T>(this T input) where T : class
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}