using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class ExpiredSessionException : FutErrorException
    {
        public ExpiredSessionException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}