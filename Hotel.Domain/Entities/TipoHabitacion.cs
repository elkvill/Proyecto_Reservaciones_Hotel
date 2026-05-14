
namespace Hotel.Domain.Entities
{
    public class TipoHabitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        // Propiedades de navegación
        public virtual ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }
}
