using System.Security.Claims;
using System.Text;
using BookClubProj.Server.Data;
using BookClubProj.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace BookClubProj.Server.Services
{
    public class AuthService
    {
        private readonly BookClubContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(BookClubContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync( u => u.Name == username);
            if (user != null)
            {
                throw new Exception("пользователь уже существует");
            }

            PasswordHash passwordHash = new PasswordHash(password);
            byte[] hashBytes = passwordHash.ToArray();

            user = new User
            {
                Name = username,
                PasswordHash = hashBytes

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == username);
            
            if (user == null)
            {
                throw new UnauthorizedAccessException("неправильный логин или пароль");
            }
            byte[] hashBytes = user.PasswordHash;
            var passwordHash = new PasswordHash(hashBytes);
            if (!passwordHash.Verify(password))
            {
                throw new UnauthorizedAccessException("Неправильный логин или пароль");
            }

            return GenerateJwtToken(user);
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<bool> ValidateUserPassword(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == username);
            if (user == null)
            {
                return false;
            }

            var passwordHash = new PasswordHash(user.PasswordHash);
            return passwordHash.Verify(password);
        }


    }
}
