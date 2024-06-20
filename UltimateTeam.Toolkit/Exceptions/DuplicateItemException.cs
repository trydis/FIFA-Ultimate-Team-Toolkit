using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class DuplicateItemException : FutErrorException
    {
        public DuplicateItemException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
