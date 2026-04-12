namespace SportsLeague.Domain.Entities
{
    public class TournamentSponsor : AuditBase
    {
        public int TournamentId { get; set; }//fk
        public int SponsorId { get; set; }//fk
        public Decimal ContractAmount { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Tournament Tournament { get; set; } = null!;
        public Sponsor Sponsor { get; set; } = null!;
    }
}
