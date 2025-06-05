using Domain.DTO;
using Microsoft.AspNetCore.Identity.Data;
using Domain.Entities;

namespace Act1_Seguridad.Services.IServices
{
    public interface IJwtServices
    {
        public Task<InicioResponse?> Autenticacion(InicioRequest request);
    }
}