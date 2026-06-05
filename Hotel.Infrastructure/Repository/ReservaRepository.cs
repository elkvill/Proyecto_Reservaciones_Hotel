
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
                    .ThenInclude(d => d.Habitacion)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reserva>> ObtenerTodasAsync()
        {
            return await _context.Reservas
                .Include(r => r.DetalleReservas)
                    .ThenInclude(d => d.Habitacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorUsuarioAsync(string usuarioId)
        {
            return await _context.Reservas
                .Include(r => r.DetalleReservas)
                    .ThenInclude(d => d.Habitacion)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        //Esto es para la validacion de que no haya reservas con las mismas fechas y habitacion
        public async Task<bool> TieneReservaOcupadaAsync(int habitacionId, DateOnly fechaInicio, DateOnly fechaFin)
        {
            return await _context.DetalleReservas
                .AnyAsync(d => d.HabitacionId == habitacionId
                               && d.Reserva.Estado != "Cancelada"
                               && d.Reserva.Estado != "0"
                               && d.Reserva.FechaInicio < fechaFin
                               && d.Reserva.FechaFin > fechaInicio);
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

        //public async Task EliminarAsync(int id)
        //{
        //    await _context.Reservas.Where(r => r.Id == id).ExecuteDeleteAsync();
        //}
    }
}
