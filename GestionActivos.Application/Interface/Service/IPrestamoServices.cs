using GestionActivos.Application.DTOs.Prestamo;

namespace GestionActivos.Application.Interface.Service
{
    public interface IPrestamoServices
    {
        Task<PrestamoDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<PrestamoDto>> ObtenerTodosAsync();
        Task<IEnumerable<PrestamoDto>> BuscarPrestamosAsync(string filtro);
        Task<PrestamoDto> CrearAsync(PrestamoCrearDto dto);
        Task<PrestamoDto> ActualizarAsync(int id, PrestamoActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
