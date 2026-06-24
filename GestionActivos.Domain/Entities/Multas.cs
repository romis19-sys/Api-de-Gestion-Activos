namespace GestionActivos.Domain.Entities
{
    public class Multas
    {
        public int MultasId { get; set; }
        public int PrestamoId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public float Monto { get; set; }
        public string TipoMulta { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateOnly FechaGenerada { get; set; }

        // propiedad de navegacion
        public Prestamos Prestamo { get; set; } = null!;

        // una multa es registrada por un usuario
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        // relacion: una multa puede tener muchos detalles
        public virtual ICollection<DetallesMultas> DetallesMultas { get; set; }  = new List<DetallesMultas>();

        // relacion: una multa puede tener muchos pagos
        public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();
    }
}