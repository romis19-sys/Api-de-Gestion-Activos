using GestionActivos.Application.Interface.Repository;
using GestionActivos.Domain.Entities;
using GestionActivos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionActivos.Infrastructure.Repository
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Pagos pago)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
        }

        public async Task CrearAsync(Pagos pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Pagos.Where(p => p.PagoId == id).ExecuteDeleteAsync();
        }

        public async Task<Pagos?> ObtenerPorIdAsync(int id)
        {
            return await _context.Pagos
                .Include(p => p.Prestamo)
                .Include(p => p.Multa)
                .Include(p => p.ApplicationUser)
                .Include(p => p.DetallesPagos)
                .FirstOrDefaultAsync(p => p.PagoId == id);
        }

        public async Task<IEnumerable<Pagos>> ObtenerTodosAsync()
        {
            return await _context.Pagos
                .Include(p => p.Prestamo)
                .Include(p => p.Multa)
                .Include(p => p.DetallesPagos)
                .ToListAsync();
        }
    }
}
