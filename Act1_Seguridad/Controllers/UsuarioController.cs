using Act1_Seguridad.Services.IServices;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Act1_Seguridad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;
        public UsuarioController(IUsuarioServices usuarioServices) {
            _usuarioServices = usuarioServices;
        }

        [HttpGet (Name = "GETUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _usuarioServices.GetAll();
            return Ok(response);
        }
        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _usuarioServices.GetById(id));
        }
        //[HttpPost]
        //public async Task<IActionResult> PostUser([FromBody] UsuarioResponse request)
        //{

        //}
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioRequest request)
        {
         var response = await _usuarioServices.Create(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UsuarioRequest request)
        {
            var response = await _usuarioServices.Update(request);
            return Ok(response);
        }
    }
}
