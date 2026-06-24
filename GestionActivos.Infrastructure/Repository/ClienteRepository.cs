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
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Clientes cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Clientes>> BuscarClientesAsync(string nombre)
        {
            var query = _context.Clientes
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var busqueda = nombre.Trim().ToLower();

                query = query.Where(c =>
                    c.NombreCompleto.ToLower().Contains(busqueda) || 
                    c.Dni.ToLower().Contains(busqueda));
            }

            return await query
                .OrderBy(c => c.NombreCompleto)
                .ToListAsync();
        }

        public async Task CrearAsync(Clientes cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Clientes.Where(c => c.ClienteId == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExisteDniAsync(string dni)
        {
            var dniNormalizado = dni.Trim();
            return _context.Clientes.AnyAsync(c => c.Dni.Trim() == dniNormalizado);
        }

        public Task<bool> ExisteEmailAsync(string email)
        {
            var emailNormalizado = email.Trim().ToLower();
            return _context.Clientes.AnyAsync(c => c.Email.Trim().ToLower() == emailNormalizado);
        }

        public async Task<Clientes?> ObtenerPorIdAsync(int id)
        {
            // Incluimos Prestamos para poder realizar la validación de eliminación (si tiene préstamos asociados)
            return await _context.Clientes
                .Include(c => c.Prestamos)
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<IEnumerable<Clientes>> ObtenerTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}
