using System.ComponentModel.DataAnnotations;
using GestionActivos.Application.Validations;

namespace GestionActivos.Application.DTOs.Prestamo
{
    public class PrestamoActualizarDto
    {
        [Required(ErrorMessage = "El cliente es requerido.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El usuario es requerido.")]
        public string ApplicationUserId { get; set; } = null!;

        [Required(ErrorMessage = "El monto es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public float Monto { get; set; }

        [Required(ErrorMessage = "El monto de cuota es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto de cuota debe ser mayor a cero.")]
        public float MontoCuota { get; set; }

        [Required(ErrorMessage = "La fecha límite es requerida.")]
        public DateOnly FechaLimite { get; set; }

        [Required(ErrorMessage = "La fecha de cuota es requerida.")]
        [PrimerosCincoDias]
        public DateOnly FechaCuota { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        [MaxLength(20, ErrorMessage = "El estado no puede exceder los 20 caracteres.")]
        [RegularExpression("^(Pendiente|Pagado|Cancelado)$", ErrorMessage = "El estado debe ser 'Pendiente', 'Pagado' o 'Cancelado'.")]
        public string Estado { get; set; } = null!;
    }
}
