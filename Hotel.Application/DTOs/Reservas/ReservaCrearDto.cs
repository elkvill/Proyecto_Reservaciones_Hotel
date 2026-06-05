using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.DTOs.Reservas
{
    public class ReservaCrearDto
    {
        public string? UsuarioId { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es requerida.")]
        public DateOnly FechaInicio { get; set; }
        [Required(ErrorMessage = "La fecha de fin es requerida.")]
        public DateOnly FechaFin { get; set; }
        public decimal Total { get; set; }
        //public string Estado { get; set; } = null!; no estoy seguro
        [Required(ErrorMessage = "Debe incluir al menos una habitación en la reserva.")]
        public List<DetalleReservaCrearDto> DetalleReservas { get; set; } = new List<DetalleReservaCrearDto>();
    }
}
