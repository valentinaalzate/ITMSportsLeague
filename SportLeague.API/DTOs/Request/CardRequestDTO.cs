using SportsLeague.Domain.Enums;

namespace SportLeague.API.DTOs.Request
{
    public class CardRequestDTO
    {
        public int PlayerId { get; set; }
        public int Minute { get; set; }
        public CardType Type { get; set; }
    }

}
