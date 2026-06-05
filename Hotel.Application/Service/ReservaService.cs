
using AutoMapper;
using Hotel.Application.DTOs.Reservas;
using Hotel.Application.Interface.Repositorys;
using Hotel.Application.Interface.Services;
using Hotel.Domain.Entities;

namespace Hotel.Application.Service
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _repository;
        private readonly IHabitacionRepository _habitacionRepository;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository repository, IHabitacionRepository habitacionRepository, IMapper mapper)
        {
            _repository = repository;
            _habitacionRepository = habitacionRepository;
            _mapper = mapper;
        }

        public async Task<ReservaDto?> ObtenerPorIdAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Reserva no encontrada.");
            return _mapper.Map<ReservaDto>(registro);
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerTodasAsync()
        {
            var registros = await _repository.ObtenerTodasAsync();
            return _mapper.Map<IEnumerable<ReservaDto>>(registros);
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerPorUsuarioAsync(string usuarioId)
        {
            var registros = await _repository.ObtenerPorUsuarioAsync(usuarioId);
            return _mapper.Map<IEnumerable<ReservaDto>>(registros);
        }

        public async Task<ReservaDto> CrearAsync(ReservaCrearDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "La reserva no puede ser nula.");

            if (string.IsNullOrEmpty(dto.UsuarioId))
                throw new ArgumentException("El ID del usuario es requerido para crear una reserva.");

            if (dto.DetalleReservas == null || !dto.DetalleReservas.Any())
                throw new ArgumentException("La reserva debe contener al menos una habitación.", nameof(dto.DetalleReservas));

            if (dto.FechaInicio >= dto.FechaFin)
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");

            // Validar habitaciones repetidas
            if (dto.DetalleReservas.GroupBy(d => d.HabitacionId).Any(g => g.Count() > 1))
                throw new ArgumentException("No se permiten habitaciones repetidas en la misma reserva.");

            // Esto es para calcular la cantidad de noches
            int noches = dto.FechaFin.DayNumber - dto.FechaInicio.DayNumber;

            var reserva = new Reserva
            {
                UsuarioId = dto.UsuarioId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado = "Pendiente",
                DetalleReservas = new List<DetalleReserva>()
            };

            foreach (var d in dto.DetalleReservas)
            {
                var habitacion = await _habitacionRepository.ObtenerPorIdAsync(d.HabitacionId);
                if (habitacion == null)
                    throw new KeyNotFoundException($"La habitación con ID {d.HabitacionId} no existe.");

                //if (habitacion.Estado != "Disponible")
                //    throw new InvalidOperationException($"La habitación número {habitacion.Numero} no está disponible (Estado actual: {habitacion.Estado}).");

                if (habitacion.Estado != "Disponible")
                    
                    if (habitacion.Estado != "Disponible" && habitacion.Estado != "Ocupada")
                        throw new InvalidOperationException($"La habitación número {habitacion.Numero} " +
                            $"no está disponible (Estado actual: {habitacion.Estado}).");

                // Validar que no haya reservas repetidas en la misma fechas
                if (await _repository.TieneReservaOcupadaAsync(habitacion.Id, dto.FechaInicio, dto.FechaFin))
                    throw new InvalidOperationException($"La habitación número {habitacion.Numero} " +
                        $"ya está reservada para el rango de fechas seleccionado ({dto.FechaInicio} - {dto.FechaFin}).");


                // Crear detalle de reserva
                var detalle = new DetalleReserva
                {
                    HabitacionId = habitacion.Id,
                    PrecioPorNoche = d.PrecioPorNoche,
                    CantidadDeNoches = noches,
                    Subtotal = d.PrecioPorNoche * noches
                };

                reserva.DetalleReservas.Add(detalle);

                // Marcar habitación como ocupada
                habitacion.Estado = "Ocupada";
                await _habitacionRepository.ActualizarAsync(habitacion);
            }

            // Calcular el total de la reserva
            reserva.Total = reserva.DetalleReservas.Sum(d => d.Subtotal);

            // Guardar reserva
            await _repository.CrearAsync(reserva);

            
            var creado = await _repository.ObtenerPorIdAsync(reserva.Id);
            return _mapper.Map<ReservaDto>(creado);
        }

        public async Task<ReservaDto> ActualizarAsync(int id, ReservaActualizarDto dto)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Reserva no encontrada.");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);
            return _mapper.Map<ReservaDto>(registro);
        }

        //public async Task EliminarAsync(int id)
        //{
        //    var registro = await _repository.ObtenerPorIdAsync(id);
        //    if (registro == null) throw new KeyNotFoundException("Reserva no encontrada.");
        //    await _repository.EliminarAsync(id);
        //}
    }
}
