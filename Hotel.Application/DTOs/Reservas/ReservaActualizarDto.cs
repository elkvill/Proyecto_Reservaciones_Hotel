
namespace Hotel.Application.DTOs.Reservas
{
    public class ReservaActualizarDto
    {
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
    }
}
