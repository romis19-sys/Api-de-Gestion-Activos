using AutoMapper;
using GestionActivos.Application.DTOs.Prestamo;
using GestionActivos.Application.Interface.Repository;
using GestionActivos.Application.Interface.Service;
using GestionActivos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionActivos.Application.Service
{
    public class PrestamoService : IPrestamoServices
    {
        private readonly IPrestamoRepository _repository;
        private readonly IMapper _mapper;

        public PrestamoService(IPrestamoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PrestamoDto> ActualizarAsync(int id, PrestamoActualizarDto dto)
        {
            if (dto.FechaCuota.Day < 1 || dto.FechaCuota.Day > 5)
                throw new ArgumentException("La fecha de cuota debe estar dentro de los primeros 5 días del mes.");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);

            return _mapper.Map<PrestamoDto>(registro);
        }

        public async Task<IEnumerable<PrestamoDto>> BuscarPrestamosAsync(string filtro)
        {
            var registros = await _repository.BuscarPrestamosAsync(filtro);
            return _mapper.Map<IEnumerable<PrestamoDto>>(registros);
        }

        public async Task<PrestamoDto> CrearAsync(PrestamoCrearDto dto)
        {
            if (dto.FechaCuota.Day < 1 || dto.FechaCuota.Day > 5)
                throw new ArgumentException("La fecha de cuota debe estar dentro de los primeros 5 días del mes.");

            var registro = _mapper.Map<Prestamos>(dto);
            await _repository.CrearAsync(registro);

            return _mapper.Map<PrestamoDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            if (registro.Multas != null && registro.Multas.Any())
                throw new InvalidOperationException($"No se puede eliminar el préstamo porque tiene multas asociadas.");

            if (registro.Pagos != null && registro.Pagos.Any())
                throw new InvalidOperationException($"No se puede eliminar el préstamo porque tiene pagos asociados.");

            await _repository.EliminarAsync(id);
        }

        public async Task<PrestamoDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            return _mapper.Map<PrestamoDto>(registro);
        }

        public async Task<IEnumerable<PrestamoDto>> ObtenerTodosAsync()
        {
            var registros = await _repository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<PrestamoDto>>(registros);
        }
    }
}
