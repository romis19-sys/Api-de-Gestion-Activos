namespace GestionActivos.Domain.Entities
{
    public class Prestamos
    {
        public int PrestamoId { get; set; }
        public int ClienteId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public float Monto { get; set; }

        // añadimos los campos de monto y fecha cuota para llevar un mejor control acerca de que se generen las multas
        public float MontoCuota { get; set; }
        public DateOnly FechaLimite { get; set; }
        public DateOnly FechaCuota { get; set; }
        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Estado { get; set; } = null!;

        // propiedade de navegacion

        // muchos préstamos pertenecen a un cliente
        public Clientes Cliente { get; set; } = null!;

        // un prestamo es registrado por un usuario
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        // un prestamo puede tener muchas multas
        public virtual ICollection<Multas> Multas { get; set; } = new List<Multas>();

        // un préstamo puede tener muchos pagos
        public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();

        // un préstamo puede tener muchos detalles de multa
        public virtual ICollection<DetallesMultas> DetallesMultas { get; set; } = new List<DetallesMultas>();
    }
}