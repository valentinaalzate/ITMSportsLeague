namespace SportsLeague.Domain.Entities
{
    public class MatchLineup : AuditBase
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public bool IsStarter { get; set; }
        public string Position { get; set; } = string.Empty;

        public int JerseyNumber { get; set; }

        public string? Notes { get; set; }

        public Match Match { get; set; } = null!;
        public Player Player { get; set; } = null!;
    }
}
