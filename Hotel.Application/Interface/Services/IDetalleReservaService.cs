
using Hotel.Application.DTOs.Detalle_Reserva;

namespace Hotel.Application.Interface.Services
{
    public interface IDetalleReservaService
    {
        Task<DetalleReservaDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<DetalleReservaDto>> ObtenerPorReservaIdAsync(int reservaId);
        Task<DetalleReservaDto> CrearAsync(DetalleReservaCrearDto dto);
        Task<DetalleReservaDto> ActualizarAsync(int id, DetalleReservaActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
