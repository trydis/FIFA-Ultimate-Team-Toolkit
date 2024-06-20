using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class NoSuchTradeExistsException : FutErrorException
    {
        public NoSuchTradeExistsException(FutError futError, Exception innerException)
            : base(futError, innerException)
        {
        }
    }
}
