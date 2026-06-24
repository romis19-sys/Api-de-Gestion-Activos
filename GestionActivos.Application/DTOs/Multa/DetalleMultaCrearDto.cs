using System.ComponentModel.DataAnnotations;

namespace GestionActivos.Application.DTOs.Multa
{
    public class DetalleMultaCrearDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El ID del préstamo debe ser mayor a cero.")]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        [MaxLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "Las observaciones son requeridas.")]
        public string Observaciones { get; set; } = null!;

        [Required(ErrorMessage = "La gravedad es requerida.")]
        [MaxLength(30, ErrorMessage = "La gravedad no puede exceder los 30 caracteres.")]
        public string Gravedad { get; set; } = null!;
    }
}
