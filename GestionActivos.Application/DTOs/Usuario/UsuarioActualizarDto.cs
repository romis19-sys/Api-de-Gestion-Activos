using System.ComponentModel.DataAnnotations;

namespace GestionActivos.Application.DTOs.Usuario
{
    public class UsuarioActualizarDto
    {
        [Required(ErrorMessage = "El nombre completo es requerido.")]
        [MaxLength(75, ErrorMessage = "El nombre completo no puede exceder los 75 caracteres.")]
        public string NombreCompleto { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [MaxLength(256, ErrorMessage = "El correo electrónico no puede exceder los 256 caracteres.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        [MaxLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        public string? PhoneNumber { get; set; }
    }
}
