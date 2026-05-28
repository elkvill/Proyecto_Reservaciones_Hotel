
using AutoMapper;
using Hotel.Application.DTOs.Tipo_habitacion;
using Hotel.Application.Interface.Repositorys;
using Hotel.Application.Interface.Services;
using Hotel.Domain.Entities;

namespace Hotel.Application.Service
{
    public class TipoHabitacionService : ITipoHabitacionService
    {
        private readonly ITipoHabitacionRepository _repository;
        private readonly IMapper _mapper;

        public TipoHabitacionService(ITipoHabitacionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TipoHabitacionDto?> ObtenerPorIdAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Tipo de habitación no encontrado.");
            return _mapper.Map<TipoHabitacionDto>(registro);
        }

        public async Task<IEnumerable<TipoHabitacionDto>> ObtenerTodosAsync()
        {
            var registros = await _repository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<TipoHabitacionDto>>(registros);
        }

        public async Task<TipoHabitacionDto> CrearAsync(TipoHabitacionCrearDto dto)
        {
            var registro = _mapper.Map<TipoHabitacion>(dto);
            await _repository.CrearAsync(registro);
            return _mapper.Map<TipoHabitacionDto>(registro);
        }

        public async Task<TipoHabitacionDto> ActualizarAsync(int id, TipoHabitacionActualizarDto dto)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Tipo de habitación no encontrado.");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);
            return _mapper.Map<TipoHabitacionDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Tipo de habitación no encontrado.");
            await _repository.EliminarAsync(id);
        }
    }
}
