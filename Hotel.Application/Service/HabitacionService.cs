using AutoMapper;
using Hotel.Application.DTOs.Habitaciones;
using Hotel.Application.Interface.Repositorys;
using Hotel.Application.Interface.Services;
using Hotel.Domain.Entities;


namespace Hotel.Application.Service
{
    public class HabitacionService : IHabitacionService
    {
        private readonly IHabitacionRepository _repository;
        private readonly IMapper _mapper;

        public HabitacionService(IHabitacionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HabitacionDto?> ObtenerPorIdAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Habitación no encontrada.");
            return _mapper.Map<HabitacionDto>(registro);
        }

        public async Task<IEnumerable<HabitacionDto>> ObtenerTodasAsync()
        {
            var registros = await _repository.ObtenerTodasAsync();
            return _mapper.Map<IEnumerable<HabitacionDto>>(registros);
        }

        public async Task<HabitacionDto> CrearAsync(HabitacionCrearDto dto)
        {
            if (await _repository.ExisteHabitacionAsync(dto.Numero))
                throw new InvalidOperationException("El número de habitación ya existe.");

            var registro = _mapper.Map<Habitacion>(dto);
            await _repository.CrearAsync(registro);
            return _mapper.Map<HabitacionDto>(registro);
        }

        public async Task<HabitacionDto> ActualizarAsync(int id, HabitacionEditarDto dto)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Habitación no encontrada.");

            if (registro.Numero != dto.Numero && await _repository.ExisteHabitacionAsync(dto.Numero))
                throw new InvalidOperationException("El nuevo número de habitación ya existe.");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);
            return _mapper.Map<HabitacionDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Habitación no encontrada.");
            await _repository.EliminarAsync(id);
        }
    }
}
