using GestionActivos.Application.DTOs.Multa;
using GestionActivos.Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestionActivos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultaController : ControllerBase
    {
        private readonly IMultaServices _multaServices;

        public MultaController(IMultaServices multaServices)
        {
            _multaServices = multaServices;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<MultaDto>>> ObtenerTodos()
        {
            var multas = await _multaServices.ObtenerTodosAsync();
            if (multas == null || !multas.Any())
                return NotFound("No hay multas registradas.");

            return Ok(multas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MultaDto>> ObtenerPorId(int id)
        {
            var multa = await _multaServices.ObtenerPorIdAsync(id);
            return Ok(multa);
        }

        [HttpPost]
        public async Task<ActionResult<MultaDto>> Crear([FromBody] MultaCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaMulta = await _multaServices.CrearAsync(dto);
            return Ok(nuevaMulta);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] MultaActualizarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var multaActualizada = await _multaServices.ActualizarAsync(id, dto);
            return Ok(multaActualizada);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _multaServices.EliminarAsync(id);
            return NoContent();
        }
    }
}
