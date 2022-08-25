using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OAuth.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OAuth.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly List<(string name, string password, string role)> UserRecords = new()
        {
            ("user1", "password1", "admin"),
            ("user2", "password2", "guest" ),
            ("user3", "password3", "hacker")
        };

        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		public Tokens Authenticate(Users users)
		{
			if (!UserRecords.Any(x => x.name == users.Name && x.password == users.Password))
			{
				return null;
			}

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
			List<Claim> claims = new();
            foreach (var role in users.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }
            claims.Add(new Claim(ClaimTypes.Name, users.Name));
			var tokenDescriptor = new SecurityTokenDescriptor
			{

                Subject = new ClaimsIdentity(claims.ToArray()),
                Issuer = _configuration["JWT:Issuer"],
				Audience = _configuration["JWT:Audience"],
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
	}
}
