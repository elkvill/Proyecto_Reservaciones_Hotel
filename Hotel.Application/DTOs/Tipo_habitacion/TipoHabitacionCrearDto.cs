
using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.DTOs.Tipo_habitacion
{
    public class TipoHabitacionCrearDto
    {
        [Required(ErrorMessage = "El nombre completo es requerido.")]
        public string Nombre { get; set; } = null!;
        
        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; } = null!;
    }
}
