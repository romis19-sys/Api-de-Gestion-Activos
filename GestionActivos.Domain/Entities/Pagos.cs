namespace GestionActivos.Domain.Entities
{
    public class Pagos
    {
        public int PagoId { get; set; }
        public int? MultasId { get; set; }
        public int? PrestamoId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public float MontoPagado { get; set; }
        public DateOnly FechaPago { get; set; }
        public string MetodoPago { get; set; } = null!;
        public string Estado { get; set; } = null!;

        // propiedad de navegacion
        public Prestamos? Prestamo { get; set; }
        public Multas? Multa { get; set; }

        // un pago es registrado por un usuario
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        // un pago puede tener muchos detalles
        public virtual ICollection<DetallesPagos> DetallesPagos { get; set; } = new List<DetallesPagos>();
    }
}