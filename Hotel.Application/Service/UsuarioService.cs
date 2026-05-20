

using AutoMapper;
using Hotel.Application.DTOs.Usuario;
using Hotel.Application.Interface.Repositorys;
using Hotel.Application.Interface.Services;

namespace Hotel.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> ContarAsync()
        {
            return await _repository.ContarAsync();
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es requerido.");

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            return _mapper.Map<UsuarioDto>(registro);
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync(int pagina, int tamano)
        {
            var registros = await _repository.ObtenerTodosAsync(pagina, tamano);
            return _mapper.Map<IEnumerable<UsuarioDto>>(registros);
        }
    }
}
