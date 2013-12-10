using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class NotEnoughCreditException : FutErrorException
    {
        public NotEnoughCreditException(FutError futError, Exception innerException)
            : base(futError, innerException)
        {
        }
    }
}
