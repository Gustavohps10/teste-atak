using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using teste_atak.Application.DTOs;
using teste_atak.Application.UseCases;
using teste_atak.Domain.Contracts;
using teste_atak.Domain.Entities;

namespace teste_atak.Application.Services
{
    public class UserAuthenticationService : IUserAuthenticationUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICrypterRepository _crypterRepository;
        private readonly string _secretKey;

        public UserAuthenticationService(IUserRepository userRepository, ICrypterRepository crypterRepository, string secretKey)
        {
            _userRepository = userRepository;
            _crypterRepository = crypterRepository;
            _secretKey = secretKey;
        }

        public async Task<(string token, UserDTO user)> Execute(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetByEmail(loginDTO.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            var passwordMatch = await _crypterRepository.Compare(loginDTO.Password, user.PasswordHash);
            if (!passwordMatch)
            {
                throw new InvalidOperationException("Email ou senha incorretos.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return (tokenString, new UserDTO
            {
                Email = user.Email,
                Name = user.Name
            });
        }
    }
}
