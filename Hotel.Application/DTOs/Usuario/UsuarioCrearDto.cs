using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hotel.Application.DTOs.Usuario
{
    public class UsuarioCrearDto
    {
        [Required(ErrorMessage = "El nombre completo es requerido.")]
        [MaxLength(75, ErrorMessage = "El nombre completo no puede exceder los 75 caracteres.")]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de Registro es requerida.")]
        public DateOnly FechaRegistro { get; set; }

        [Required(ErrorMessage = "El Estado es requerido.")]
        public string Estado { get; set; } = null!;

        //Esto lo hice hoy
        [Required(ErrorMessage = "El email del usuario es requerido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña del usuario es requerida.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El rol del usuario es requerido.")]
        public string Rol { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono del usuario es requerido.")]
        public string PhoneNumber { get; set; } = null!;

    }
}
