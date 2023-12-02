using IdentityService.Api.Application.Models;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityService.Api.Application.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<LoginResponseModel> Login(LoginRequestModel requestModel)
        {
            //JWT 3 parçaya ayrılır. Header + Payload(claimsler) + İmza(secret key)
            //tokenda gelen değeri jwt.io ya yapıştırınca payload kısmındaki alanlar burasıdır.
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,requestModel.UserName),
                new Claim(ClaimTypes.Name, "oaslan"),
            };

            // jwt nin doğrulanabilmesi için imza için oluşturulan secret key ile kontrol yapılabilir.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("oaslanSecretKeyValue"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: creds, notBefore: DateTime.Now);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            LoginResponseModel response = new()
            {
                UserToken = encodedJwt,
                UserName = requestModel.UserName
            };

            return Task.FromResult(response);
        }
    }
}
