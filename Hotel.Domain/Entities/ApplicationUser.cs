using Microsoft.AspNetCore.Identity;

namespace Hotel.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //public string NombreCompleto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Activo";
    }
}
