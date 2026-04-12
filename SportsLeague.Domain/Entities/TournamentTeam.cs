namespace SportsLeague.Domain.Entities
{
    public class TournamentTeam : AuditBase
    {
        public int TournamentId { get; set; }//fk
        public int TeamId { get; set; }//fk
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow; //fecha que se reguistra un equipo a un torneo

        // Navigation Properties
        public Tournament Tournament { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }

}
