using Act1_Seguridad.Services.IServices;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Act1_Seguridad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolServices _rolServices;
        public RolController(IRolServices rolServices)
        {
            _rolServices = rolServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _rolServices.GetAll();
            return Ok(response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetRolById(int id)
        {
            return Ok(await _rolServices.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> PostRol([FromBody] RolRequest request)
        {
            return Ok(await _rolServices.Create(request));
        }
        [HttpPut("id")]
        public async Task<IActionResult> PutRol(RolRequest request, int id)
        {
            return Ok(await _rolServices.Update(request, id));
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _rolServices.Delete(id));
        }
    }
}
