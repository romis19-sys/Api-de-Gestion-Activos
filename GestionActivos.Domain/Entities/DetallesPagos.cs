namespace GestionActivos.Domain.Entities
{
    public class DetallesPagos
    {
        public int DetallesPagoId { get; set; }
        public int PagoId { get; set; }
        public string Concepto { get; set; } = null!;
        public float Monto { get; set; }
        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Descripcion { get; set; } = null!;

        // propiedad de navegacion
        public Pagos Pago { get; set; } = null!;
    }
}