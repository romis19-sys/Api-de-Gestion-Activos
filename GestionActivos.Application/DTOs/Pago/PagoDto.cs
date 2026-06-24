namespace GestionActivos.Application.DTOs.Pago
{
    public class PagoDto
    {
        public int PagoId { get; set; }
        public int? MultasId { get; set; }
        public int? PrestamoId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public float MontoPagado { get; set; }
        public DateOnly FechaPago { get; set; }
        public string MetodoPago { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public List<DetallePagoDto> Detalles { get; set; } = new List<DetallePagoDto>();
    }
}
