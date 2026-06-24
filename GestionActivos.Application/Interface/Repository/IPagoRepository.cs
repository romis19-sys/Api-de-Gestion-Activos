using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Interface.Repository
{
    public interface IPagoRepository
    {
        Task<Pagos?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Pagos>> ObtenerTodosAsync();
        Task CrearAsync(Pagos pago);
        Task ActualizarAsync(Pagos pago);
        Task EliminarAsync(int id);
    }
}
