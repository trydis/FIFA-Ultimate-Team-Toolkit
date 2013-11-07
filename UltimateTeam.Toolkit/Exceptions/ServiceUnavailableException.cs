using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class ServiceUnavailableException : PermissionDeniedException
    {
        public ServiceUnavailableException(FutErrorWithDebugString futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
