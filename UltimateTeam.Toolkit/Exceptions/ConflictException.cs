using System;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class ConflictException : FutErrorException
    {
        public ConflictException(FutError futError, Exception innerException)
            : base(futError, innerException)
        {
        }
    }
}
