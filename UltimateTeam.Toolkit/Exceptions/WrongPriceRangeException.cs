using UltimateTeam.Toolkit.Models.Generic;

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