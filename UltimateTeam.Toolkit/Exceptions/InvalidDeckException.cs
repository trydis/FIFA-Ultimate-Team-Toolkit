using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class InvalidDeckException : FutErrorException
    {
        public InvalidDeckException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
