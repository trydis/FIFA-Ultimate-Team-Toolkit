using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class CaptchaTriggeredException : FutErrorException
    {
        public CaptchaTriggeredException(FutError futError, Exception innerException)
            : base(futError, innerException)
        {
        }
    }
}