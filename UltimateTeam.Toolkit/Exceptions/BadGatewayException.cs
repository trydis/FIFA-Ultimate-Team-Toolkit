using UltimateTeam.Toolkit.Models;
using System;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class BadGatewayException : FutErrorException
    {
        public BadGatewayException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
