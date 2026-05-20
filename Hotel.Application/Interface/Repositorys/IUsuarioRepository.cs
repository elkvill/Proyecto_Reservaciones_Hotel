using Hotel.Domain.Entities;

namespace Hotel.Application.Interface.Repositorys
{
    public interface IUsuarioRepository
    {
        // Task<IEnumerable<Productora>> ObtenerTodosAsync();
        //Task<IEnumerable<ApplicationUser>> ObtenerTodasAsync();
        //Task<ApplicationUser?> ObtenerPorIdAsync(int id);
        //// Task<Productora?> BuscarPorNombreAsync(string nombre);
        //Task<IEnumerable<ApplicationUser>> BuscarUsuarioAsync(string nombre);
        ////Task<IEnumerable<ApplicationUser>> ObtenerPorPaisAsync(string pais);
        //Task<bool> ExisteNombreAsync(string nombre);


        //Task CrearAsync(ApplicationUser user);
        //Task ActualizarAsync(ApplicationUser user);
        //Task EliminarAsync(int id);

        //Solo esto estaba en el video
        Task<ApplicationUser?> ObtenerPorIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync(int pagina, int tamano);
        Task<int> ContarAsync();
    }
}
