namespace GestionActivos.Application.DTOs.Multa
{
    public class DetalleMultaDto
    {
        public int DetallesMultaId { get; set; }
        public int MultasId { get; set; }
        public int PrestamoId { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public string Gravedad { get; set; } = null!;
        public DateOnly FechaRegistro { get; set; }
    }
}
