
namespace Hotel.Application.DTOs.Detalle_Reserva
{
    public class DetalleReservaCrearDto
    {
        public int ReservaId { get; set; }
        public int HabitacionId { get; set; }
        //public int HabitacionNumero { get; set; } no estoy seguro si es necesario enviar el numero de habitacion,
        //ya que se puede obtener a través del id de la habitacion
        public decimal PrecioPorNoche { get; set; }
        public int CantidadDeNoches { get; set; }
        public decimal Subtotal { get; set; }
    }
}
