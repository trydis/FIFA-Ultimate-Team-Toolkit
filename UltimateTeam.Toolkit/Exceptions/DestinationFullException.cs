using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class DestinationFullException : FutErrorException
    {
        public DestinationFullException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
