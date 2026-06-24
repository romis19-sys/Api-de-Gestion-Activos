using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Interface.Repository
{
    public interface IMultaRepository
    {
        Task<Multas?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Multas>> ObtenerTodosAsync();
        Task CrearAsync(Multas multa);
        Task ActualizarAsync(Multas multa);
        Task EliminarAsync(int id);
    }
}
