using GestionActivos.Application.DTOs.Multa;

namespace GestionActivos.Application.Interface.Service
{
    public interface IMultaServices
    {
        Task<MultaDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<MultaDto>> ObtenerTodosAsync();
        Task<MultaDto> CrearAsync(MultaCrearDto dto);
        Task<MultaDto> ActualizarAsync(int id, MultaActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
