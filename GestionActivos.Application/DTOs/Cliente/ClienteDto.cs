namespace GestionActivos.Application.DTOs.Cliente
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string Dni { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}
