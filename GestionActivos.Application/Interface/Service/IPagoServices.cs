using GestionActivos.Application.DTOs.Pago;

namespace GestionActivos.Application.Interface.Service
{
    public interface IPagoServices
    {
        Task<PagoDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<PagoDto>> ObtenerTodosAsync();
        Task<PagoDto> CrearAsync(PagoCrearDto dto);
        Task<PagoDto> ActualizarAsync(int id, PagoActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
