using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class InternalServerException : FutErrorException
    {
        public InternalServerException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}