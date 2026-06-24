using AutoMapper;
using GestionActivos.Application.DTOs.Multa;
using GestionActivos.Application.Interface.Repository;
using GestionActivos.Application.Interface.Service;
using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Service
{
    public class MultaService : IMultaServices
    {
        private readonly IMultaRepository _repository;
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IMapper _mapper;

        public MultaService(IMultaRepository repository, IPrestamoRepository prestamoRepository, IMapper mapper)
        {
            _repository = repository;
            _prestamoRepository = prestamoRepository;
            _mapper = mapper;
        }

        public async Task<MultaDto> ActualizarAsync(int id, MultaActualizarDto dto)
        {
            var prestamo = await _prestamoRepository.ObtenerPorIdAsync(dto.PrestamoId);
            if (prestamo == null)
                throw new KeyNotFoundException("El préstamo especificado no existe.");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);

            return _mapper.Map<MultaDto>(registro);
        }

        public async Task<MultaDto> CrearAsync(MultaCrearDto dto)
        {
            var prestamo = await _prestamoRepository.ObtenerPorIdAsync(dto.PrestamoId);
            if (prestamo == null)
                throw new KeyNotFoundException("El préstamo especificado no existe.");

            var registro = _mapper.Map<Multas>(dto);

            if (dto.Detalles != null && dto.Detalles.Any())
            {
                foreach (var detalleDto in dto.Detalles)
                {
                    var detalle = _mapper.Map<DetallesMultas>(detalleDto);
                    registro.DetallesMultas.Add(detalle);
                }
            }

            await _repository.CrearAsync(registro);

            var prestamoActualizar = await _prestamoRepository.ObtenerPorIdAsync(dto.PrestamoId);
            if (prestamoActualizar != null)
            {
                prestamoActualizar.Monto += registro.Monto;
                await _prestamoRepository.ActualizarAsync(prestamoActualizar);
            }

            return _mapper.Map<MultaDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            if (registro.Pagos != null && registro.Pagos.Any())
                throw new InvalidOperationException("No se puede eliminar la multa porque tiene pagos asociados.");

            await _repository.EliminarAsync(id);
        }

        public async Task<MultaDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            return _mapper.Map<MultaDto>(registro);
        }

        public async Task<IEnumerable<MultaDto>> ObtenerTodosAsync()
        {
            var registros = await _repository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<MultaDto>>(registros);
        }
    }
}
