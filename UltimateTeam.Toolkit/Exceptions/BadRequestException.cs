using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class BadRequestException : FutErrorException
    {
        public BadRequestException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
