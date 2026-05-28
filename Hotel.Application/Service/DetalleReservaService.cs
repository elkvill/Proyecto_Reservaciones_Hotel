
using AutoMapper;
using Hotel.Application.DTOs.Detalle_Reserva;
using Hotel.Application.Interface.Repositorys;
using Hotel.Application.Interface.Services;
using Hotel.Domain.Entities;

namespace Hotel.Application.Service
{
    public class DetalleReservaService : IDetalleReservaService
    {
        private readonly IDetalleReservaRepository _repository;
        private readonly IMapper _mapper;

        public DetalleReservaService(IDetalleReservaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DetalleReservaDto?> ObtenerPorIdAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Detalle de reserva no encontrado.");
            return _mapper.Map<DetalleReservaDto>(registro);
        }

        public async Task<IEnumerable<DetalleReservaDto>> ObtenerPorReservaIdAsync(int reservaId)
        {
            var registros = await _repository.ObtenerPorReservaIdAsync(reservaId);
            return _mapper.Map<IEnumerable<DetalleReservaDto>>(registros);
        }

        public async Task<DetalleReservaDto> CrearAsync(DetalleReservaCrearDto dto)
        {
            var registro = _mapper.Map<DetalleReserva>(dto);
            await _repository.CrearAsync(registro);
            return _mapper.Map<DetalleReservaDto>(registro);
        }

        public async Task<DetalleReservaDto> ActualizarAsync(int id, DetalleReservaActualizarDto dto)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Detalle de reserva no encontrado.");

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);
            return _mapper.Map<DetalleReservaDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null) throw new KeyNotFoundException("Detalle de reserva no encontrado.");
            await _repository.EliminarAsync(id);
        }
    }
}
