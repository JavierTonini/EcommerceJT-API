using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(IUserRepository<User> userRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private User ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.UserName) || string.IsNullOrEmpty(authenticationRequest.Password))
                throw new ArgumentException("Username and password must be provided.");

            var user = _userRepository.GetByUserName(authenticationRequest.UserName);

            if (user == null)
                throw new NotAllowedException("User authentication failed");

            // Aquí puedes agregar más lógica de validación si es necesario
            if (user.Password != authenticationRequest.Password)
                throw new NotAllowedException("User authentication failed");

            return user;
        }

        public string Authentication(AuthenticationRequest authenticationRequest)
        {
            // Paso 1: Validamos las credenciales
            var user = ValidateUser(authenticationRequest);

            // Paso 2: Crear el token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.Name),
                new Claim("family_name", user.LastName),
                new Claim("role", user.UserType)
            };

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public class AuthenticationServiceOptions
        {
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}