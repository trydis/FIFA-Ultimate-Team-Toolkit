using System;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class ExpiredSessionException : FutException
    {
        public FutErrorWithMessage FutError { get; private set; }

        public ExpiredSessionException(FutErrorWithMessage futError, Exception exception)
            : base(futError.Message, exception)
        {
            futError.ThrowIfNullArgument();
            FutError = futError;
        }
    }
}