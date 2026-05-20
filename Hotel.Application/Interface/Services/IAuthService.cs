

using Hotel.Application.DTOs.Usuario;
using Hotel.Application.Response;

namespace Hotel.Application.Interface.Services
{
    public interface IAuthService
    {
        Task<RespuestaLoginDto> LoginAsync(UsuarioLoginDto dto);
        Task<UsuarioDto> RegistrarUsuarioAsync(UsuarioCrearDto dto);
        Task<RespuestaLoginDto> RefreshTokenAsync(string refreshToken);
    }
}
