using GestionActivos.Application.Interface.Repository;
using GestionActivos.Domain.Entities;
using GestionActivos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionActivos.Infrastructure.Repository
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly ApplicationDbContext _context;

        public PrestamoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Prestamos prestamo)
        {
            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prestamos>> BuscarPrestamosAsync(string filtro)
        {
            var query = _context.Prestamos
                .Include(p => p.Cliente)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var busqueda = filtro.Trim().ToLower();

                query = query.Where(p =>
                    p.Cliente.NombreCompleto.ToLower().Contains(busqueda) ||
                    p.Estado.ToLower().Contains(busqueda));
            }

            return await query
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();
        }

        public async Task CrearAsync(Prestamos prestamo)
        {
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Prestamos.Where(p => p.PrestamoId == id).ExecuteDeleteAsync();
        }

        public async Task<Prestamos?> ObtenerPorIdAsync(int id)
        {
            return await _context.Prestamos
                .Include(p => p.Cliente)
                .Include(p => p.ApplicationUser)
                .Include(p => p.Multas)
                .Include(p => p.Pagos)
                .FirstOrDefaultAsync(p => p.PrestamoId == id);
        }

        public async Task<IEnumerable<Prestamos>> ObtenerTodosAsync()
        {
            return await _context.Prestamos
                .Include(p => p.Cliente)
                .ToListAsync();
        }
    }
}
