using Hotel.Application.DTOs.Usuario;
using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.Interface.Services
{
    public interface IUsuarioService
    {
        //Task<IEnumerable<UsuarioDto>> ObtenerTodasAsync();
        //Task<UsuarioDto?> ObtenerPorIdAsync(int id);

        //// Task<UsuarioDto?> BuscarPorNombreAsync(string nombre);
        //Task<IEnumerable<UsuarioDto>> BuscarUsuarioAsync(string nombre);
        ////Task<IEnumerable<UsuarioDto>> ObtenerPorPaisAsync(string pais);


        //Task<UsuarioDto> CrearAsync(UsuarioCrearDto dto);
        //Task<UsuarioDto> ActualizarAsync(int id, UsuarioActualizarDto dto);
        //Task EliminarAsync(int id);
        Task<UsuarioDto?> ObtenerPorIdAsync(string id);
        Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync(int pagina, int tamano);
        Task<int> ContarAsync();
    }
}
