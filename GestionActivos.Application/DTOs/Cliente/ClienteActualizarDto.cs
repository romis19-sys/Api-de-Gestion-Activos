using System.ComponentModel.DataAnnotations;

namespace GestionActivos.Application.DTOs.Cliente
{
    public class ClienteActualizarDto
    {
        [Required(ErrorMessage = "El DNI es requerido.")]
        [MaxLength(20, ErrorMessage = "El DNI no puede exceder los 20 caracteres.")]
        public string Dni { get; set; } = null!;

        [Required(ErrorMessage = "El nombre completo es requerido.")]
        [MaxLength(200, ErrorMessage = "El nombre completo no puede exceder los 200 caracteres.")]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [MaxLength(150, ErrorMessage = "El correo electrónico no puede exceder los 150 caracteres.")]
        public string Email { get; set; } = null!;

        [MaxLength(250, ErrorMessage = "La dirección no puede exceder los 250 caracteres.")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El estado es requerido.")]
        [MaxLength(20, ErrorMessage = "El estado no puede exceder los 20 caracteres.")]
        [RegularExpression("^(Activo|Inactivo)$", ErrorMessage = "El estado debe ser 'Activo' o 'Inactivo'.")]
        public string Estado { get; set; } = null!;
    }
}
