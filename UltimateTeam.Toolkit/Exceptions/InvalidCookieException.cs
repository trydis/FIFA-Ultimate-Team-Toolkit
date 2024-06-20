using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class InvalidCookieException : FutErrorException
    {
        public InvalidCookieException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
