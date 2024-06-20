using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class FunCaptchaTriggeredException : FutErrorException
    {
        public FunCaptchaTriggeredException(FutError futError, Exception innerException)
            : base(futError, innerException)
        {
        }
    }
}