using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class BadRequestException : FutErrorException
    {
        public BadRequestException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
