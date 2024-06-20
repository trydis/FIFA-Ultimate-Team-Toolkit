using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class TemporaryBanException : FutErrorException
    {
        public TemporaryBanException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}