using Microsoft.AspNetCore.Identity;

namespace GestionActivos.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; } = null!;

        // navegación
        public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();
        public virtual ICollection<Multas> Multas { get; set; } = new List<Multas>();
        public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();
    }
}
