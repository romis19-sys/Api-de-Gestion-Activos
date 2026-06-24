using GestionActivos.Application.DTOs.Cliente;
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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices _clienteServices;

        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ClienteDto>>> ObtenerTodos()
        {
            var clientes = await _clienteServices.ObtenerTodosAsync();
            if (clientes == null || !clientes.Any())
                return NotFound("No hay clientes registrados.");

            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDto>> ObtenerPorId(int id)
        {
            var cliente = await _clienteServices.ObtenerPorIdAsync(id);
            return Ok(cliente);
        }

        [HttpGet("buscarPorNombre")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Buscar([FromQuery] string nombre)
        {
            var clientes = await _clienteServices.BuscarClientesAsync(nombre);
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Crear([FromBody] ClienteCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoCliente = await _clienteServices.CrearAsync(dto);
            return Ok(nuevoCliente);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ClienteActualizarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteActualizado = await _clienteServices.ActualizarAsync(id, dto);
            return Ok(clienteActualizado);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _clienteServices.EliminarAsync(id);
            return NoContent();
        }
    }
}
