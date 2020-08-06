using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electrosur.Models;

namespace Electrosur
{
    public interface ILoginService
    {
        string GenerateJWT(FormLogin user);
    }
}
