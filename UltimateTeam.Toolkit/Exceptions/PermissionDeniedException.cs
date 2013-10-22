using System;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class PermissionDeniedException : FutException
    {
        public FutErrorWithDebugString FutError { get; private set; }

        public PermissionDeniedException(FutErrorWithDebugString futError, Exception exception)
            : base(futError.Reason, exception)
        {
            futError.ThrowIfNullArgument();
            FutError = futError;
        }
    }
}