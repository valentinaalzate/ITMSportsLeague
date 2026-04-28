using SportsLeague.Domain.Enums;

namespace SportLeague.API.DTOs.Request
{
    public class GoalRequestDTO
    {
        public int PlayerId { get; set; }
        public int Minute { get; set; }
        public GoalType Type { get; set; }
    }

}
