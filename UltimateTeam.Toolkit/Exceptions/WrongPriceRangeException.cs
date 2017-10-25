using UltimateTeam.Toolkit.Models;
using System;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class WrongPriceRangeException : FutErrorException
    {
        public WrongPriceRangeException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}