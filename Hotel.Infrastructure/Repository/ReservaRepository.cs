
using Hotel.Application.Interface.Repositorys;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContent _context;

        public ReservaRepository(ApplicationDbContent context)
        {
            _context = context;
        }

        public async Task<Reserva?> ObtenerPorIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.DetalleReservas)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reserva>> ObtenerTodasAsync()
        {
            return await _context.Reservas
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorUsuarioAsync(string usuarioId)
        {
            return await _context.Reservas
                .Include(r => r.DetalleReservas)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }
        public async Task CrearAsync(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Reservas.Where(r => r.Id == id).ExecuteDeleteAsync();
        }
    }
}
