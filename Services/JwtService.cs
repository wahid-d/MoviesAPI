using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Service.Services
{
    public class JwtService
    {
        public static string SecurityKey = "3bf2c345-c4f1-4201-b87f-60a5ab6302be";
        private static string _issuer =  "some-issuer";
        private static string _audience =  "some-audience";

        public static string GenerateToken(string userName, string role)
        {
            var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));

            var authClaims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName),
                    new Claim("role", role)
                };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    
        public static bool IsValidToken(string token)
        {
            var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var result = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secret
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static string GetClaim(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return securityToken.Claims.FirstOrDefault().Value;
        }
    
    }

}