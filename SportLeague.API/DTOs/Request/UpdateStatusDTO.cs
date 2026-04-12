using SportsLeague.Domain.Enums;

namespace SportLeague.API.DTOs.Request
{
    public class UpdateStatusDTO
    {
        public TournamentStatus Status { get; set; }
    }

}
