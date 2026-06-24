namespace GestionActivos.Application.DTOs.Prestamo
{
    public class PrestamoDto
    {
        public int PrestamoId { get; set; }
        public int ClienteId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public float Monto { get; set; }
        public float MontoCuota { get; set; }
        public DateOnly FechaLimite { get; set; }
        public DateOnly FechaCuota { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public string Estado { get; set; } = null!;
    }
}
