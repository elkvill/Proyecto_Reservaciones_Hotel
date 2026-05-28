
using Hotel.Domain.Entities;

namespace Hotel.Application.Interface.Repositorys
{
    public interface IReservaRepository
    {
        Task<Reserva?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Reserva>> ObtenerTodasAsync();
        Task<IEnumerable<Reserva>> ObtenerPorUsuarioAsync(string usuarioId);

        Task CrearAsync(Reserva reserva);
        Task ActualizarAsync(Reserva reserva);
        Task EliminarAsync(int id);
    }
}
