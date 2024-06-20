using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class AccountLockedException : FutErrorException
    {
        public AccountLockedException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}