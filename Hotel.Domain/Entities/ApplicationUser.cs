using Microsoft.AspNetCore.Identity;

namespace Hotel.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; } = null!;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Activo";
    }
}
