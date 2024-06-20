using UltimateTeam.Toolkit.Models.Generic;

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
