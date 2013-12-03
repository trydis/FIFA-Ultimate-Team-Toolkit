using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class ExpiredSessionException : FutErrorException
    {
        public ExpiredSessionException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}