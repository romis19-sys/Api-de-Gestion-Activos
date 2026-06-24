using GestionActivos.Application.DTOs.Prestamo;
using GestionActivos.Application.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionActivos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoServices _prestamoServices;

        public PrestamoController(IPrestamoServices prestamoServices)
        {
            _prestamoServices = prestamoServices;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PrestamoDto>>> ObtenerTodos()
        {
            var prestamos = await _prestamoServices.ObtenerTodosAsync();
            if (prestamos == null || !prestamos.Any())
                return NotFound("No hay préstamos registrados.");

            return Ok(prestamos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrestamoDto>> ObtenerPorId(int id)
        {
            var prestamo = await _prestamoServices.ObtenerPorIdAsync(id);
            return Ok(prestamo);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<PrestamoDto>>> Buscar([FromQuery] string filtro)
        {
            var prestamos = await _prestamoServices.BuscarPrestamosAsync(filtro);
            return Ok(prestamos);
        }

        [HttpPost]
        public async Task<ActionResult<PrestamoDto>> Crear([FromBody] PrestamoCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoPrestamo = await _prestamoServices.CrearAsync(dto);
            return Ok(nuevoPrestamo);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] PrestamoActualizarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var prestamoActualizado = await _prestamoServices.ActualizarAsync(id, dto);
            return Ok(prestamoActualizado);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _prestamoServices.EliminarAsync(id);
            return NoContent();
        }
    }
}
