using AutoMapper;
using GestionActivos.Application.DTOs.Usuario;
using GestionActivos.Application.Interface.Repository;
using GestionActivos.Application.Interface.Service;
using GestionActivos.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GestionActivos.Application.Service
{
    public class UsuarioService : IUsuarioServices
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> ContarAsync()
        {
            return await _repository.ContarAsync();
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es requerido.");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            return await MapearConRolAsync(registro);
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync(int pagina, int tamano)
        {
            var registros = await _repository.ObtenerTodosAsync(pagina, tamano);
            return await MapearListaConRolAsync(registros);
        }

        public async Task<IEnumerable<UsuarioDto>> BuscarPorEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es requerido para la búsqueda.");

            var registros = await _repository.BuscarPorEmailAsync(email);
            return await MapearListaConRolAsync(registros);
        }

        public async Task<UsuarioDto> ActualizarAsync(string id, UsuarioActualizarDto dto)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es requerido.");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            registro.NombreCompleto = dto.NombreCompleto.Trim();
            registro.PhoneNumber = dto.PhoneNumber?.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailNuevo = dto.Email.Trim().ToLower();
                if (!string.Equals(registro.Email, emailNuevo, StringComparison.OrdinalIgnoreCase))
                {
                    var existeEmail = await _userManager.FindByEmailAsync(emailNuevo);
                    if (existeEmail != null && existeEmail.Id != id)
                        throw new InvalidOperationException($"El email '{dto.Email}' ya está registrado por otro usuario.");

                    registro.Email = emailNuevo;
                    registro.UserName = emailNuevo;
                }
            }

            await _repository.ActualizarAsync(registro);
            return await MapearConRolAsync(registro);
        }

        public async Task EliminarAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es requerido.");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            await _repository.EliminarAsync(registro);
        }

        private async Task<UsuarioDto> MapearConRolAsync(ApplicationUser usuario)
        {
            var dto = _mapper.Map<UsuarioDto>(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);
            dto.Rol = roles.FirstOrDefault() ?? "";
            return dto;
        }

        private async Task<IEnumerable<UsuarioDto>> MapearListaConRolAsync(IEnumerable<ApplicationUser> usuarios)
        {
            var dtos = new List<UsuarioDto>();
            foreach (var usuario in usuarios)
            {
                dtos.Add(await MapearConRolAsync(usuario));
            }
            return dtos;
        }
    }
}
