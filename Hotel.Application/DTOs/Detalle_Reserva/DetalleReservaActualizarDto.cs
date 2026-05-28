
namespace Hotel.Application.DTOs.Detalle_Reserva
{
    public class DetalleReservaActualizarDto
    {
        public int HabitacionId { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public int CantidadDeNoches { get; set; }
        public decimal Subtotal { get; set; }
    }
}
