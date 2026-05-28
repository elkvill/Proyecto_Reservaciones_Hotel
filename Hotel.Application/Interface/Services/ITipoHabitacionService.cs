
using Hotel.Application.DTOs.Tipo_habitacion;

namespace Hotel.Application.Interface.Services
{
    public interface ITipoHabitacionService
    {
        Task<TipoHabitacionDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<TipoHabitacionDto>> ObtenerTodosAsync();
        Task<TipoHabitacionDto> CrearAsync(TipoHabitacionCrearDto dto);
        Task<TipoHabitacionDto> ActualizarAsync(int id, TipoHabitacionActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
