using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class ServiceUnavailableException : FutErrorException
    {
        public ServiceUnavailableException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
