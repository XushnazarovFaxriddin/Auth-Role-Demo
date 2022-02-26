using AuthRoleDemo.Entites;
using AuthRoleDemo.Helpers;
using AuthRoleDemo.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthRoleDemo.Auth
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UserValid user);
        public int? ValidateJwtToken(string token);
    }
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string GenerateJwtToken(UserValid user)
        {
            //7 kun davomida amal qiladigan token yarating
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("Role",user.Role.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public int? ValidateJwtToken(string token)
        {
            if (token is null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Tokenlarning amal qilish muddati tokenning amal qilish muddati tugashi uchun (5 daqiqadan keyin o'rniga) soat burilish chizig'ini nolga o'rnating.
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // tekshirish muvaffaqiyatli bo'lsa, JWT tokenidan foydalanuvchi identifikatorini qaytaring
                return userId;

            }
            catch
            {
                //tekshirish muvaffaqiyatsiz bo'lsa, nullni qaytaring
                return null;
            }
        }
    }
}
