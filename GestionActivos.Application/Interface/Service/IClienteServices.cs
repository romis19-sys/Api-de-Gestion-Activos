using GestionActivos.Application.DTOs.Cliente;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionActivos.Application.Interface.Service
{
    public interface IClienteServices
    {
        // Métodos de consulta
        Task<ClienteDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<ClienteDto>> ObtenerTodosAsync();
        Task<IEnumerable<ClienteDto>> BuscarClientesAsync(string nombre);

        // Métodos CRUD
        Task<ClienteDto> CrearAsync(ClienteCrearDto dto);
        Task<ClienteDto> ActualizarAsync(int id, ClienteActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
