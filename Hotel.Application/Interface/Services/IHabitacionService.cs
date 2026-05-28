using Hotel.Application.DTOs.Habitaciones;


namespace Hotel.Application.Interface.Services
{
    public interface IHabitacionService
    {
        Task<HabitacionDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<HabitacionDto>> ObtenerTodasAsync();
        Task<HabitacionDto> CrearAsync(HabitacionCrearDto dto);
        Task<HabitacionDto> ActualizarAsync(int id, HabitacionEditarDto dto);
        Task EliminarAsync(int id);
    }
}
