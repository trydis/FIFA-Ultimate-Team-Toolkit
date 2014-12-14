using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeam.Toolkit
{
    public interface ITwoFactorCodeProvider
    {
        Task<string> GetTwoFactorCodeAsync();
    }
}
