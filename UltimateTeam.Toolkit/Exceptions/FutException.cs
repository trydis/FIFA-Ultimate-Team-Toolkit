using System;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class FutException : Exception
    {
        public FutException(string message)
            : base(message)
        {
        }

        public FutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
