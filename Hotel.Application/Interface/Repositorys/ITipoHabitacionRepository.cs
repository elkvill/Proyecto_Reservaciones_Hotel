
using Hotel.Domain.Entities;

namespace Hotel.Application.Interface.Repositorys
{
    public interface ITipoHabitacionRepository
    {
        Task<TipoHabitacion?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<TipoHabitacion>> ObtenerTodosAsync();

        Task CrearAsync(TipoHabitacion tipoHabitacion);
        Task ActualizarAsync(TipoHabitacion tipoHabitacion);
        Task EliminarAsync(int id);
    }
}
