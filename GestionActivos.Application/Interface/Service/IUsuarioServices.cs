using GestionActivos.Application.DTOs.Usuario;

namespace GestionActivos.Application.Interface.Service
{
    public interface IUsuarioServices
    {
        Task<UsuarioDto?> ObtenerPorIdAsync(string id);
        Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync(int pagina, int tamano);
        Task<IEnumerable<UsuarioDto>> BuscarPorEmailAsync(string email);
        Task<int> ContarAsync();
        Task<UsuarioDto> ActualizarAsync(string id, UsuarioActualizarDto dto);
        Task EliminarAsync(string id);
    }
}
