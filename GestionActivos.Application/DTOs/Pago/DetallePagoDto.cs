namespace GestionActivos.Application.DTOs.Pago
{
    public class DetallePagoDto
    {
        public int DetallesPagoId { get; set; }
        public int PagoId { get; set; }
        public string Concepto { get; set; } = null!;
        public float Monto { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public string Descripcion { get; set; } = null!;
    }
}
