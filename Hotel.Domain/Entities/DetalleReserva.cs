namespace Hotel.Domain.Entities
{
    public class DetalleReserva
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public int HabitacionId { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public int CantidadDeNoches { get; set; }
        public decimal Subtotal { get; set; }

        // Propiedades de navegación
        public virtual Reserva Reserva { get; set; } = null!;
        public virtual Habitacion Habitacion { get; set; } = null!;
    }
}
