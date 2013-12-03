using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class PermissionDeniedException : FutErrorException
    {
        public PermissionDeniedException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}