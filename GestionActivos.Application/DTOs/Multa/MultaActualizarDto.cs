using System.ComponentModel.DataAnnotations;

namespace GestionActivos.Application.DTOs.Multa
{
    public class MultaActualizarDto
    {
        [Required(ErrorMessage = "El préstamo es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del préstamo debe ser mayor a cero.")]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage = "El usuario es requerido.")]
        public string ApplicationUserId { get; set; } = null!;

        [Required(ErrorMessage = "El código es requerido.")]
        [MaxLength(50, ErrorMessage = "El código no puede exceder los 50 caracteres.")]
        public string Codigo { get; set; } = null!;

        [Required(ErrorMessage = "El monto es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public float Monto { get; set; }

        [Required(ErrorMessage = "El tipo de multa es requerido.")]
        [MaxLength(50, ErrorMessage = "El tipo de multa no puede exceder los 50 caracteres.")]
        public string TipoMulta { get; set; } = null!;

        [Required(ErrorMessage = "El estado es requerido.")]
        [MaxLength(20, ErrorMessage = "El estado no puede exceder los 20 caracteres.")]
        [RegularExpression("^(Pendiente|Pagado|Anulada)$", ErrorMessage = "El estado debe ser 'Pendiente', 'Pagado' o 'Anulada'.")]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "La fecha generada es requerida.")]
        public DateOnly FechaGenerada { get; set; }
    }
}
