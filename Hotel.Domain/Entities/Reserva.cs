namespace Hotel.Domain.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = null!;//esto lo agregue hace poco

        public ApplicationUser? Usuario { get; set; }

        //Esto estaba en el video pero no estoy seguro si es necesario,
        //ya que tengo el usuario relacionado, pero lo dejo por si acaso, es para guardar
        //el nombre del cliente que hizo la reserva, aunque ya tengo el usuario relacionado,
        //esto es para tener un campo adicional con el nombre del cliente por si acaso

        //public string Cliente {  get; set; } = null!; 

        //aqui termina la parte de usuario que agregue

        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();
    }
}
