using System;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class InternalServerException : FutException
    {
        public FutErrorWithDebugString FutError { get; private set; }

        public InternalServerException(FutErrorWithDebugString futError, Exception exception)
            : base(futError.Reason, exception)
        {
            futError.ThrowIfNullArgument();
            FutError = futError;
        }
    }
}