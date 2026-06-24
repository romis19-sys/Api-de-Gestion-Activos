namespace GestionActivos.Domain.Entities
{
    public class Clientes
    {
        public int ClienteId { get; set; }
        public string Dni { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Estado { get; set; } = null!;

        // relacion: un cliente puede tener muchos prestamos
        public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();
    }
}