using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class PurchasedItemsFullException : FutErrorException
    {
        public PurchasedItemsFullException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
