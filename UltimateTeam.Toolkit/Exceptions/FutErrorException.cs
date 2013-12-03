using System;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;


namespace UltimateTeam.Toolkit.Exceptions
{
    public class FutErrorException : FutException
    {
        public FutError FutError { get; private set; }

        public FutErrorException(FutError futError)
            : base(ExtractMessage(futError))
        {
            FutError = futError;
        }

        public FutErrorException(FutError futError, Exception innerException)
            : base(ExtractMessage(futError), innerException)
        {
            FutError = futError;
        }

        private static string ExtractMessage(FutError futError)
        {
            futError.ThrowIfNullArgument();
            
            string result = "Code: " + futError.Code;
            if (!string.IsNullOrEmpty(futError.Reason))
                result += ", Reason: " + futError.Reason;
            if (!string.IsNullOrEmpty(futError.Message))
                result += ", Message: " + futError.Message;
            if (!string.IsNullOrEmpty(futError.Debug))
                result += ", Debug: " + futError.Debug;
            if (!string.IsNullOrEmpty(futError.String))
                result += ", String: " + futError.String;

            return result;
        }
    }
}
