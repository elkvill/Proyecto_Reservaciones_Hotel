using Hotel.Application.Interface.Repositorys;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repository
{
    public class TipoHabitacionRepository : ITipoHabitacionRepository
    {
        private readonly ApplicationDbContent _context;

        public TipoHabitacionRepository(ApplicationDbContent context)
        {
            _context = context;
        }

        public async Task<TipoHabitacion?> ObtenerPorIdAsync(int id)
        {
            return await _context.TipoHabitaciones.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TipoHabitacion>> ObtenerTodosAsync()
        {
            return await _context.TipoHabitaciones.ToListAsync();
        }

        public async Task CrearAsync(TipoHabitacion tipoHabitacion)
        {
            _context.TipoHabitaciones.Add(tipoHabitacion);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(TipoHabitacion tipoHabitacion)
        {
            _context.TipoHabitaciones.Update(tipoHabitacion);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.TipoHabitaciones.Where(t => t.Id == id).ExecuteDeleteAsync();
        }
    }
}
