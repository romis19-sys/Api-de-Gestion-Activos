using GestionActivos.Application.DTOs.Pago;
using GestionActivos.Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestionActivos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoServices _pagoServices;

        public PagoController(IPagoServices pagoServices)
        {
            _pagoServices = pagoServices;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PagoDto>>> ObtenerTodos()
        {
            var pagos = await _pagoServices.ObtenerTodosAsync();
            if (pagos == null || !pagos.Any())
                return NotFound("No hay pagos registrados.");

            return Ok(pagos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PagoDto>> ObtenerPorId(int id)
        {
            var pago = await _pagoServices.ObtenerPorIdAsync(id);
            return Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult<PagoDto>> Crear([FromBody] PagoCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoPago = await _pagoServices.CrearAsync(dto);
            return Ok(nuevoPago);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] PagoActualizarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pagoActualizado = await _pagoServices.ActualizarAsync(id, dto);
            return Ok(pagoActualizado);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _pagoServices.EliminarAsync(id);
            return NoContent();
        }
    }
}
