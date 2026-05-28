using Hotel.Domain.Entities;


namespace Hotel.Application.Interface.Repositorys
{
    public interface IHabitacionRepository
    {
        Task<Habitacion?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Habitacion>> ObtenerTodasAsync();
        Task<IEnumerable<Habitacion>> ObtenerPorTipoAsync(int tipoHabitacionId);
        Task<bool> ExisteHabitacionAsync(int numero);

        Task CrearAsync(Habitacion habitacion);
        Task ActualizarAsync(Habitacion habitacion);
        Task EliminarAsync(int id);
    }
}
