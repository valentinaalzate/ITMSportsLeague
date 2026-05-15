namespace SportLeague.API.DTOs.Request
{
    public class MatchLineupRequestDTO
    {
        public int PlayerId { get; set; }
        public bool IsStarter { get; set; }
        public int? JerseyNumber { get; set; }
        public string Position { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
    }
}
