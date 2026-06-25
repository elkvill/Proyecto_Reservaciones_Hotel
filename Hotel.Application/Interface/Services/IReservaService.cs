
using Hotel.Application.DTOs.Reservas;

namespace Hotel.Application.Interface.Services
{
    public interface IReservaService
    {
        Task<ReservaDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<ReservaDto>> ObtenerTodasAsync();

        Task<IEnumerable<ReservaDto>> ObtenerPorUsuarioAsync(string usuarioId);
        Task<ReservaDto> CrearAsync(ReservaCrearDto dto);
        Task<ReservaDto> ActualizarAsync(int id, ReservaActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
