using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Deposito.BLL.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims);
        public bool ValidarUsuario(string username, string password);
    }
}