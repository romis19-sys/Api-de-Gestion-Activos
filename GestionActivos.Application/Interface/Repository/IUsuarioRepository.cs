using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Interface.Repository
{
    public interface IUsuarioRepository
    {
        Task<ApplicationUser?> ObtenerPorIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync(int pagina, int tamano);
        Task<IEnumerable<ApplicationUser>> BuscarPorEmailAsync(string email);
        Task<int> ContarAsync();
        Task ActualizarAsync(ApplicationUser usuario);
        Task EliminarAsync(ApplicationUser usuario);
    }
}
