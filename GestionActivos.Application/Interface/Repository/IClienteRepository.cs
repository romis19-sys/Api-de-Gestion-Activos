using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Interface.Repository
{
    public interface IClienteRepository
    {
        // Métodos de consulta
        Task<Clientes?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Clientes>> ObtenerTodosAsync();
        Task<IEnumerable<Clientes>> BuscarClientesAsync(string nombre);
        Task<bool> ExisteDniAsync(string dni);
        Task<bool> ExisteEmailAsync(string email);

        // Métodos de comando (CRUD)
        Task CrearAsync(Clientes cliente);
        Task ActualizarAsync(Clientes cliente);
        Task EliminarAsync(int id);
    }
}
