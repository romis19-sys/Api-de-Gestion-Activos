using System.ComponentModel.DataAnnotations;

namespace GestionActivos.Application.DTOs.Pago
{
    public class DetallePagoCrearDto
    {
        [Required(ErrorMessage = "El concepto es requerido.")]
        [MaxLength(100, ErrorMessage = "El concepto no puede exceder los 100 caracteres.")]
        public string Concepto { get; set; } = null!;

        [Required(ErrorMessage = "El monto es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public float Monto { get; set; }

        [MaxLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string? Descripcion { get; set; }
    }
}
