using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Interface.Repository
{
    public interface IPrestamoRepository
    {
        Task<Prestamos?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Prestamos>> ObtenerTodosAsync();
        Task<IEnumerable<Prestamos>> BuscarPrestamosAsync(string filtro);
        Task CrearAsync(Prestamos prestamo);
        Task ActualizarAsync(Prestamos prestamo);
        Task EliminarAsync(int id);
    }
}
