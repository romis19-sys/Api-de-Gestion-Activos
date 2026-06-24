using GestionActivos.Application.DTOs.Usuario;
using GestionActivos.Application.Interface.Service;
using GestionActivos.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionActivos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _service;
        public UsuarioController(IUsuarioServices service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObtenerTodos([FromQuery] int pagina = 1, [FromQuery] int tamanio = 10)
        {
            var registros = await _service.ObtenerTodosAsync(pagina, tamanio);
            var total = await _service.ContarAsync();
            return Ok(new RespuestaPaginada<UsuarioDto>(registros, total, pagina, tamanio));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UsuarioDto>> ObtenerPorId(string id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);
            return Ok(registro);
        }

        [HttpGet("buscarPorEmail")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> BuscarPorEmail([FromQuery] string email)
        {
            var registros = await _service.BuscarPorEmailAsync(email);
            return Ok(registros);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UsuarioDto>> Actualizar(string id, [FromBody] UsuarioActualizarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _service.ActualizarAsync(id, dto);
            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Eliminar(string id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }
}
