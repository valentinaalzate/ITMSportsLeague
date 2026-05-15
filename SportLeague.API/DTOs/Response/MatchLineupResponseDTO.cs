namespace SportLeague.API.DTOs.Response
{
    public class MatchLineupResponseDTO
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public bool IsStarter { get; set; }
        public int? JerseyNumber { get; set; }
        public string Position { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
    }
}
