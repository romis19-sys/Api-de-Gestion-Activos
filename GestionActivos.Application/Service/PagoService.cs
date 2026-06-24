using AutoMapper;
using GestionActivos.Application.DTOs.Pago;
using GestionActivos.Application.Interface.Repository;
using GestionActivos.Application.Interface.Service;
using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Service
{
    public class PagoService : IPagoServices
    {
        private readonly IPagoRepository _repository;
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IMapper _mapper;

        public PagoService(IPagoRepository repository, IPrestamoRepository prestamoRepository, IMapper mapper)
        {
            _repository = repository;
            _prestamoRepository = prestamoRepository;
            _mapper = mapper;
        }

        public async Task<PagoDto> ActualizarAsync(int id, PagoActualizarDto dto)
        {
            if (dto.MultasId.HasValue && dto.MultasId.Value <= 0) dto.MultasId = null;
            if (dto.PrestamoId.HasValue && dto.PrestamoId.Value <= 0) dto.PrestamoId = null;

            ValidarPago(dto.MultasId, dto.PrestamoId);

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);

            return _mapper.Map<PagoDto>(registro);
        }

        public async Task<PagoDto> CrearAsync(PagoCrearDto dto)
        {
            if (dto.MultasId.HasValue && dto.MultasId.Value <= 0) dto.MultasId = null;
            if (dto.PrestamoId.HasValue && dto.PrestamoId.Value <= 0) dto.PrestamoId = null;

            ValidarPago(dto.MultasId, dto.PrestamoId);

            if (dto.PrestamoId.HasValue)
            {
                var prestamoValidar = await _prestamoRepository.ObtenerPorIdAsync(dto.PrestamoId.Value);
                if (prestamoValidar != null && prestamoValidar.Multas.Any(m => m.Estado == "Pendiente"))
                    throw new InvalidOperationException("Este préstamo tiene una multa, favor de pagar la multa primero.");
            }

            var registro = _mapper.Map<Pagos>(dto);

            if (dto.Detalles != null && dto.Detalles.Any())
            {
                foreach (var detalleDto in dto.Detalles)
                {
                    var detalle = _mapper.Map<DetallesPagos>(detalleDto);
                    registro.DetallesPagos.Add(detalle);
                }
            }

            await _repository.CrearAsync(registro);

            if (registro.PrestamoId.HasValue)
            {
                var prestamo = await _prestamoRepository.ObtenerPorIdAsync(registro.PrestamoId.Value);
                if (prestamo != null)
                {
                    prestamo.Monto -= registro.MontoPagado;
                    if (prestamo.Monto <= 0)
                    {
                        prestamo.Monto = 0;
                        prestamo.Estado = "Pagado";
                    }
                    await _prestamoRepository.ActualizarAsync(prestamo);
                }
            }

            return _mapper.Map<PagoDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            if (registro.DetallesPagos != null && registro.DetallesPagos.Any())
                throw new InvalidOperationException("No se puede eliminar el pago porque tiene detalles asociados.");

            await _repository.EliminarAsync(id);
        }

        public async Task<PagoDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado");

            return _mapper.Map<PagoDto>(registro);
        }

        public async Task<IEnumerable<PagoDto>> ObtenerTodosAsync()
        {
            var registros = await _repository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<PagoDto>>(registros);
        }

        // creamos el metodo para validar si es pago de una multa o prestamo
        // pq no se puede pagar dos cosas al mismo tiempo
        private static void ValidarPago(int? multasId, int? prestamoId)
        {
            var tieneMulta = multasId.HasValue && multasId.Value > 0;
            var tienePrestamo = prestamoId.HasValue && prestamoId.Value > 0;

            if (tieneMulta && tienePrestamo)
                throw new InvalidOperationException("No se puede pagar una multa y un préstamo al mismo tiempo.");

            if (!tieneMulta && !tienePrestamo)
                throw new InvalidOperationException("Debe especificar una multa o un préstamo para realizar el pago.");
        }
    }
}
