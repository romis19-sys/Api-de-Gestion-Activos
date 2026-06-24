using GestionActivos.Application.Interface.Repository;
using GestionActivos.Domain.Entities;
using GestionActivos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionActivos.Infrastructure.Repository
{
    public class MultaRepository : IMultaRepository
    {
        private readonly ApplicationDbContext _context;

        public MultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Multas multa)
        {
            _context.Multas.Update(multa);
            await _context.SaveChangesAsync();
        }

        public async Task CrearAsync(Multas multa)
        {
            _context.Multas.Add(multa);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Multas.Where(m => m.MultasId == id).ExecuteDeleteAsync();
        }

        public async Task<Multas?> ObtenerPorIdAsync(int id)
        {
            return await _context.Multas
                .Include(m => m.Prestamo)
                .Include(m => m.ApplicationUser)
                .Include(m => m.DetallesMultas)
                .FirstOrDefaultAsync(m => m.MultasId == id);
        }

        public async Task<IEnumerable<Multas>> ObtenerTodosAsync()
        {
            return await _context.Multas
                .Include(m => m.Prestamo)
                .Include(m => m.ApplicationUser)
                .Include(m => m.DetallesMultas)
                .ToListAsync();
        }
    }
}
