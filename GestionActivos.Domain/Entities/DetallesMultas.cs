namespace GestionActivos.Domain.Entities
{
    public class DetallesMultas
    {
        public int DetallesMultaId { get; set; }
        public int MultasId { get; set; }
        public int PrestamoId { get; set; }
        public string Descripcion { get; set; } = null!; // si es por retraso, cuota incompleta
        public string Observaciones { get; set; } = null!; // si es la primera vez, si no, si es constante
        public string Gravedad { get; set; } = null!; // segun las veces que se repita la multa

        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        // propiedad de navegacion
        public Multas Multa { get; set; } = null!;
        public Prestamos Prestamo { get; set; } = null!;
    }
}