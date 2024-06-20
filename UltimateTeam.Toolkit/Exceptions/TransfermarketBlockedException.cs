using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class TransfermarketBlockedException : FutErrorException
    {
        public TransfermarketBlockedException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}