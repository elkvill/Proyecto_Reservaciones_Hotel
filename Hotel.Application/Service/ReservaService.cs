
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
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository repository, IMapper mapper)
        {
            _repository = repository;
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
            var registro = _mapper.Map<Reserva>(dto);
            await _repository.CrearAsync(registro);
            return _mapper.Map<ReservaDto>(registro);
        }

        public async Task<ReservaDto> ActualizarAsync(int id, ReservaActualizarDto dto)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Reserva no encontrada.");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);
            return _mapper.Map<ReservaDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Reserva no encontrada.");
            await _repository.EliminarAsync(id);
        }
    }
}
