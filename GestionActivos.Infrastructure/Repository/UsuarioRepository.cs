using GestionActivos.Application.Interface.Repository;
using GestionActivos.Domain.Entities;
using GestionActivos.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestionActivos.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<ApplicationUser?> ObtenerPorIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync(int pagina, int tamano)
        {
            return await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.NombreCompleto)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> BuscarPorEmailAsync(string email)
        {
            var query = _context.Users
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
            {
                var busqueda = email.Trim().ToLower();
                query = query.Where(u => u.Email != null && u.Email.ToLower().Contains(busqueda));
            }

            return await query
                .OrderBy(u => u.NombreCompleto)
                .ToListAsync();
        }

        public async Task ActualizarAsync(ApplicationUser usuario)
        {
            _context.Users.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(ApplicationUser usuario)
        {
            _context.Users.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
