using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class BadRequestException : PermissionDeniedException
    {
        public BadRequestException(FutErrorWithDebugString futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
