using Application.Interfaces;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Security
{
    public class JwtJenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;
        public JwtJenerator()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7592d6c3-fe20-488b-8761-1f5fe317a96c"));
        }  
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {

                new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)

            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(100),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
