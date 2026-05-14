namespace Hotel.Domain.Entities
{
    public class Habitacion
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int TipoHabitacionId { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; } = "Disponible";

        // Propiedades de navegación
        public virtual TipoHabitacion TipoHabitacion { get; set; } = null!;
        public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();
    }
}
