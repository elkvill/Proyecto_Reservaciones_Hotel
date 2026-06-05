namespace Hotel.Application.DTOs.Reservas
{
    public class DetalleReservaDto
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public int HabitacionId { get; set; }
        public int HabitacionNumero { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public int CantidadDeNoches { get; set; }
        public decimal Subtotal { get; set; }
    }
}
