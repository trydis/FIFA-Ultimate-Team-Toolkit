using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class DestinationFullException : FutErrorException
    {
        public DestinationFullException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
