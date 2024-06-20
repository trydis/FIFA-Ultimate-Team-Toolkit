using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class PermissionDeniedException : FutErrorException
    {
        public PermissionDeniedException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}