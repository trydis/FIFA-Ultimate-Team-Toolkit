using System;
using UltimateTeam.Toolkit.Models;

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
