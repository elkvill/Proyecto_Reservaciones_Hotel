
using Hotel.Application.Interface.Repositorys;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repository
{
    public class DetalleReservaRepository : IDetalleReservaRepository
    {
        private readonly ApplicationDbContent _context;

        public DetalleReservaRepository(ApplicationDbContent context)
        {
            _context = context;
        }

        public async Task<DetalleReserva?> ObtenerPorIdAsync(int id)
        {
            return await _context.DetalleReservas
                .Include(d => d.Habitacion)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DetalleReserva>> ObtenerPorReservaIdAsync(int reservaId)
        {
            return await _context.DetalleReservas
                .Where(d => d.ReservaId == reservaId)
                .Include(d => d.Habitacion)
                .ToListAsync();
        }

        public async Task CrearAsync(DetalleReserva detalle)
        {
            _context.DetalleReservas.Add(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(DetalleReserva detalle)
        {
            _context.DetalleReservas.Update(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.DetalleReservas.Where(d => d.Id == id).ExecuteDeleteAsync();
        }
    }
}
