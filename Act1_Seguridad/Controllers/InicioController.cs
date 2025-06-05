using Act1_Seguridad.Services.IServices;
using Act1_Seguridad.Services.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Act1_Seguridad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InicioController : ControllerBase
    {
        private readonly IJwtServices _jwtServices;
        public InicioController(IJwtServices jwtServices)
        {
            _jwtServices = jwtServices;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] InicioRequest request)
        {
            var response = await _jwtServices.Autenticacion(request);
            if (response == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(response);
        }
    }
}