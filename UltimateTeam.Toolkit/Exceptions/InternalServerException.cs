using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class InternalServerException : FutErrorException
    {
        public InternalServerException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}