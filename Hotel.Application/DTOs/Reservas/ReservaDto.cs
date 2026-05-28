

namespace Hotel.Application.DTOs.Reservas
{
    public class ReservaDto
    {
        public int Id { get; set; }

        public string UsuarioId { get; set; } = null!;
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }
}
