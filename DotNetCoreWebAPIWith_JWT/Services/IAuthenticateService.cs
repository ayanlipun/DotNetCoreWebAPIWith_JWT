using DotNetCoreWebAPIWith_JWT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPIWith_JWT.Services
{
    public interface IAuthenticateService
    {
        UserDTO Authenticate(string userName, string password);
    }
}
