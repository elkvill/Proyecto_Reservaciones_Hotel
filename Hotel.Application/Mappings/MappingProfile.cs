using AutoMapper;
using Hotel.Application.DTOs.Detalle_Reserva;
using Hotel.Application.DTOs.Habitaciones;
using Hotel.Application.DTOs.Reservas;
using Hotel.Application.DTOs.Tipo_habitacion;
using Hotel.Application.DTOs.Usuario;
using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.Mappings
{
    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {   
            #region Mapeo de Usuario

            CreateMap<ApplicationUser, UsuarioDto>()
                    .ForMember(dest => dest.Rol, opt => opt.Ignore());

            CreateMap<UsuarioCrearDto, ApplicationUser>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro.ToDateTime(TimeOnly.MinValue)));

            #endregion

            #region Mapeo de Habitacion
            CreateMap<Habitacion, HabitacionDto>()
                .ForMember(dest => dest.TipoHabitacionNombre, opt => opt.MapFrom(src => src.TipoHabitacion.Nombre));
            CreateMap<HabitacionCrearDto, Habitacion>();
            CreateMap<HabitacionEditarDto, Habitacion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion

            #region Mapeo de TipoHabitacion
            CreateMap<TipoHabitacion, TipoHabitacionDto>();
            CreateMap<TipoHabitacionCrearDto, TipoHabitacion>();
            CreateMap<TipoHabitacionActualizarDto, TipoHabitacion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion

            #region Mapeo de Reserva
            CreateMap<Reserva, ReservaDto>();
            CreateMap<ReservaCrearDto, Reserva>();
            CreateMap<ReservaActualizarDto, Reserva>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore());

            #endregion

            #region Mapeo de DetalleReserva
            CreateMap<DetalleReserva, DetalleReservaDto>()
               .ForMember(dest => dest.HabitacionNumero, opt => opt.MapFrom(src => src.Habitacion.Numero));
            CreateMap<DetalleReservaCrearDto, DetalleReserva>();
            CreateMap<DetalleReservaActualizarDto, DetalleReserva>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ReservaId, opt => opt.Ignore());

            #endregion
        }

    }

}
