using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class NotFoundException : FutErrorException
    {
        public NotFoundException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
