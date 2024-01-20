using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestHiberusNet.DTOs.FabricatorDTOs;
using TestHiberusNet.DTOs.StatusDTOs;
using TestHiberusNet.DTOs.TerminalDTOs;
using TestHiberusNet.Models;

namespace TestHiberusNet.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            #region Status
            CreateMap<Estado, StatusDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom<int>(e => e.IdEstado))
                .ForMember(dest => dest.Name, src => src.MapFrom<string>(e => e.EstadoName))
                .ForMember(dest => dest.Description, src => src.MapFrom<string>(e => e.EstadoDesc));
            #endregion

            #region Fabricator
            CreateMap<Fabricantes, FabricatorDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom<int>(f => f.IdFab))
                .ForMember(dest => dest.Name, src => src.MapFrom<string>(f => f.FabName))
                .ForMember(dest => dest.Description, src => src.MapFrom<string>(f => f.FabDesc));
            #endregion

            #region Terminal
            CreateMap<Terminales, TerminalDto>()
               .ForMember(dest => dest.Name, src => src.MapFrom<string>(t => t.TerminalName))
               .ForMember(dest => dest.Description, src => src.MapFrom<string>(t => t.TerminalDesc))
               .ForMember(dest => dest.FabricatorName, src => src.MapFrom<string>(t => t.IdFabNavigation.FabName))
               .ForMember(dest => dest.StatusName, src => src.MapFrom<string>(t => t.IdEstadoNavigation.EstadoName))
               .ForMember(dest => dest.ManufacturingDate, src => src.MapFrom(t => t.FechaFabricacion.ToString("dd/MM/yyyy") ?? ""))
               .ForMember(dest => dest.StatusDate, src => src.MapFrom<string>(t => t.FechaEstado.ToString("dd/MM/yyyy") ?? ""));
            #endregion
        }
    }
}
