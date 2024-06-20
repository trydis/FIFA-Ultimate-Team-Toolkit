using UltimateTeam.Toolkit.Models.Generic;

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