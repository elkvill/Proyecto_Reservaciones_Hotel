
using Hotel.Domain.Entities;

namespace Hotel.Application.Interface.Repositorys
{
    public interface IDetalleReservaRepository
    {
        Task<DetalleReserva?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<DetalleReserva>> ObtenerPorReservaIdAsync(int reservaId);

        Task CrearAsync(DetalleReserva detalle);
        Task ActualizarAsync(DetalleReserva detalle);
        Task EliminarAsync(int id);
    }
}
