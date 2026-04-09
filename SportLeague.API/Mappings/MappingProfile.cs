using AutoMapper;
using SportLeague.API.DTOs.Request;
using SportLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;

namespace SportLeague.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Team mappings
            CreateMap<TeamRequestDTO, Team>();
            CreateMap<Team, TeamResponseDTO>();

            // Player mappings
            CreateMap<PlayerRequestDTO, Player>();
            CreateMap<Player, PlayerResponseDTO>()
                .ForMember(
                    dest => dest.TeamName,
                    opt => opt.MapFrom(src => src.Team.Name)); //FORMEMBER para mapear el nombre del equipo desde la entidad Player a PlayerResponseDTO

        }
    }

}
