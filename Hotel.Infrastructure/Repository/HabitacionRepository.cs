using Hotel.Application.Interface.Repositorys;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repository
{
    public class HabitacionRepository : IHabitacionRepository
    {
        private readonly ApplicationDbContent _context;

        public HabitacionRepository(ApplicationDbContent context)
        {
            _context = context;
        }

        public async Task<Habitacion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Habitaciones
                .Include(h => h.TipoHabitacion)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Habitacion>> ObtenerTodasAsync()
        {
            return await _context.Habitaciones
                .Include(h => h.TipoHabitacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Habitacion>> ObtenerPorTipoAsync(int tipoHabitacionId)
        {
            return await _context.Habitaciones
                .Where(h => h.TipoHabitacionId == tipoHabitacionId)
                .ToListAsync();
        }

        public async Task<bool> ExisteHabitacionAsync(int numero)
        {
            return await _context.Habitaciones.AnyAsync(h => h.Numero == numero);
        }

        public async Task CrearAsync(Habitacion habitacion)
        {
            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Habitacion habitacion)
        {
            _context.Habitaciones.Update(habitacion);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Habitaciones.Where(h => h.Id == id).ExecuteDeleteAsync();
        }
    }
}
