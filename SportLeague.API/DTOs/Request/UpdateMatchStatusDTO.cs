using SportsLeague.Domain.Enums;

namespace SportLeague.API.DTOs.Request
{
    public class UpdateMatchStatusDTO
    {
        public MatchStatus Status { get; set; }
    }

}
