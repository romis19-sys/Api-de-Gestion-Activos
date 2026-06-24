using AutoMapper;
using GestionActivos.Application.DTOs.Cliente;
using GestionActivos.Application.Interface.Repository;
using GestionActivos.Application.Interface.Service;
using GestionActivos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionActivos.Application.Service
{
    public class ClienteService : IClienteServices
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClienteDto> ActualizarAsync(int id, ClienteActualizarDto dto)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            var nuevoDni = dto.Dni.Trim();
            var nuevoEmail = dto.Email.Trim().ToLower();

            // Validar DNI duplicado solamente si cambió
            if (!string.Equals(registro.Dni.Trim(), nuevoDni, StringComparison.OrdinalIgnoreCase))
            {
                var siExisteDni = await _repository.ExisteDniAsync(nuevoDni);
                if (siExisteDni)
                    throw new InvalidOperationException($"Ya existe un registro con el DNI: '{dto.Dni}.'");
            }

            // Validar Email duplicado solamente si cambió
            if (!string.Equals(registro.Email.Trim().ToLower(), nuevoEmail, StringComparison.OrdinalIgnoreCase))
            {
                var siExisteEmail = await _repository.ExisteEmailAsync(nuevoEmail);
                if (siExisteEmail)
                    throw new InvalidOperationException($"Ya existe un registro con el correo electrónico: '{dto.Email}.'");
            }

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);

            return _mapper.Map<ClienteDto>(registro);
        }

        public async Task<IEnumerable<ClienteDto>> BuscarClientesAsync(string nombre)
        {
            var registros = await _repository.BuscarClientesAsync(nombre);
            return _mapper.Map<IEnumerable<ClienteDto>>(registros);
        }

        public async Task<ClienteDto> CrearAsync(ClienteCrearDto dto)
        {
            var siExisteDni = await _repository.ExisteDniAsync(dto.Dni);
            if (siExisteDni)
                throw new InvalidOperationException($"Ya existe un registro con el DNI: '{dto.Dni}.'");

            var siExisteEmail = await _repository.ExisteEmailAsync(dto.Email);
            if (siExisteEmail)
                throw new InvalidOperationException($"Ya existe un registro con el correo electrónico: '{dto.Email}.'");

            var registro = _mapper.Map<Clientes>(dto);
            await _repository.CrearAsync(registro);

            return _mapper.Map<ClienteDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            if (registro.Prestamos != null && registro.Prestamos.Any())
                throw new InvalidOperationException($"No se puede eliminar el cliente: '{registro.NombreCompleto}.' porque tiene préstamos asociados.");

            await _repository.EliminarAsync(id);
        }

        public async Task<ClienteDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            return _mapper.Map<ClienteDto>(registro);
        }

        public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
        {
            var registros = await _repository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(registros);
        }
    }
}
