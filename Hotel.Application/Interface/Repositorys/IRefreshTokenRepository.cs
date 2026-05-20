

using Hotel.Domain.Entities;

namespace Hotel.Application.Interface.Repositorys
{
    public interface IRefreshTokenRepository
    {
        Task GuardarAsync(RefreshToken token);
        Task<RefreshToken?> ObtenerAsync(string token);
        Task ActualizarAsync(RefreshToken token);
    }
}
