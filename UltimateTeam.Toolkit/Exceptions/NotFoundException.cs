using System;
using UltimateTeam.Toolkit.Models;

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
