using UltimateTeam.Toolkit.Models;
using System;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class ServiceDisabledException : FutErrorException
    {
        public ServiceDisabledException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}