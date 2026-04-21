using SportsLeague.Domain.Enums;

namespace SportLeague.API.DTOs.Request
{
    public class UpdateTournamentStatusDTO
    {
        public TournamentStatus Status { get; set; }
    }

}
