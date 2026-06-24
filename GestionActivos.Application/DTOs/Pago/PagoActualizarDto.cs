using System.ComponentModel.DataAnnotations;

namespace GestionActivos.Application.DTOs.Pago
{
    public class PagoActualizarDto
    {
        public int? MultasId { get; set; }

        public int? PrestamoId { get; set; }

        [Required(ErrorMessage = "El usuario es requerido.")]
        public string ApplicationUserId { get; set; } = null!;

        [Required(ErrorMessage = "El monto pagado es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public float MontoPagado { get; set; }

        [Required(ErrorMessage = "La fecha de pago es requerida.")]
        public DateOnly FechaPago { get; set; }

        [Required(ErrorMessage = "El método de pago es requerido.")]
        [MaxLength(50, ErrorMessage = "El método de pago no puede exceder los 50 caracteres.")]
        public string MetodoPago { get; set; } = null!;

        [Required(ErrorMessage = "El estado es requerido.")]
        [MaxLength(20, ErrorMessage = "El estado no puede exceder los 20 caracteres.")]
        [RegularExpression("^(Completado|Pendiente|Cancelado)$", ErrorMessage = "El estado debe ser 'Completado', 'Pendiente' o 'Cancelado'.")]
        public string Estado { get; set; } = null!;
    }
}
