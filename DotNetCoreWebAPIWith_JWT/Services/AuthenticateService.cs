using DotNetCoreWebAPIWith_JWT.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DotNetCoreWebAPIWith_JWT.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;

        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private List<UserDTO> users = new List<UserDTO>()
        {
             new UserDTO
             {
                 UserId=1,
                 FirstName ="Gleain" ,
                 LastName="Maxwell",
                 UserName="gleainmaxwell",
                 Password="maxwell@gleain"
             }
        };
        
        public UserDTO Authenticate(string userName, string password)
        {
            var user = users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            // if user not found 
            if (user == null)
                return null;

            // if user is found

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);


            user.Password = null;
            return user;
        }
    }
}
