using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class InvalidDeckException : FutErrorException
    {
        public InvalidDeckException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
