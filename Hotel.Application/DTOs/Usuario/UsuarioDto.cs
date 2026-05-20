using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.DTOs.Usuario
{
    public class UsuarioDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Activo";

        //Esto es nuevo de hoy
        public string Id { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
