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
        }
    }

}
