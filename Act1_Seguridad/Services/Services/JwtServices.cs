using Act1_Seguridad.Context;
using Act1_Seguridad.Services.IServices;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.DTO;
using Domain.Entities;


namespace Act1_Seguridad.Services.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public JwtServices(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<InicioResponse?> Autenticacion(InicioRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }
            var usuario = await _context.Usuarios.
                FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Password == request.Password);
            if (usuario == null)
            {
                return null;
            }

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var Expires = _configuration.GetValue<int>("JwtConfig:TokenExpirationInMinutes");
            var tokenexpires = DateTime.UtcNow.AddMinutes(Expires);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, usuario.UserName),
                }),
                Expires = tokenexpires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new InicioResponse
            {
                UserName = usuario.UserName,
                AccesToken = token,
                ExpirationToken = (int)tokenexpires.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }

   
    }
}