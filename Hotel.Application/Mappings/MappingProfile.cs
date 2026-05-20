using AutoMapper;
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
            }
        
    }

}
