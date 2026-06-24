using GestionActivos.Application.DTOs.Pago;

namespace GestionActivos.Application.DTOs.Multa
{
    public class MultaDto
    {
        public int MultasId { get; set; }
        public int PrestamoId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public float Monto { get; set; }
        public string TipoMulta { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateOnly FechaGenerada { get; set; }

        public List<DetalleMultaDto> Detalles { get; set; } = new List<DetalleMultaDto>();
    }
}
