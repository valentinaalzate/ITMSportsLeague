using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public SponsorService(
            ISponsorRepository sponsorRepository,
            ITournamentSponsorRepository tournamentSponsorRepository,
            ITournamentRepository tournamentRepository)
        {
            _sponsorRepository = sponsorRepository;
            _tournamentSponsorRepository = tournamentSponsorRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync() =>
            await _sponsorRepository.GetAllAsync();

        public async Task<Sponsor?> GetByIdAsync(int id) =>
            await _sponsorRepository.GetByIdAsync(id);

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            var existing = await _sponsorRepository.GetByNameAsync(sponsor.Name);
            if (existing != null)
                throw new InvalidOperationException($"El patrocinador '{sponsor.Name}' ya se encuentra registrado en el sistema");

            if (!sponsor.ContactEmail.Contains("@") || !sponsor.ContactEmail.Contains("."))
                throw new InvalidOperationException($"El correo '{sponsor.ContactEmail}' no tiene un formato valido");

            return await _sponsorRepository.CreateAsync(sponsor);
        }

        public async Task UpdateAsync(int id, Sponsor sponsor)
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontro ningun patrocinador con el ID {id}");

            var nameConflict = await _sponsorRepository.GetByNameAsync(sponsor.Name);
            if (nameConflict != null && nameConflict.Id != id)
                throw new InvalidOperationException($"El nombre '{sponsor.Name}' ya esta siendo usado por otro patrocinador");

            if (!sponsor.ContactEmail.Contains("@") || !sponsor.ContactEmail.Contains("."))
                throw new InvalidOperationException($"El correo '{sponsor.ContactEmail}' no tiene un formato valido");

            existing.Name = sponsor.Name;
            existing.ContactEmail = sponsor.ContactEmail;
            existing.Phone = sponsor.Phone;
            existing.WebsiteUrl = sponsor.WebsiteUrl;
            existing.Category = sponsor.Category;

            await _sponsorRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _sponsorRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"No se encontro ningun patrocinador con el ID {id}");

            await _sponsorRepository.DeleteAsync(id);
        }

        public async Task<TournamentSponsor> LinkToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount)
        {
            if (!await _sponsorRepository.ExistsAsync(sponsorId))
                throw new KeyNotFoundException($"El patrocinador con ID {sponsorId} no existe en el sistema");

            if (!await _tournamentRepository.ExistsAsync(tournamentId))
                throw new KeyNotFoundException($"El torneo con ID {tournamentId} no existe en el sistema");

            var existing = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
            if (existing != null)
                throw new InvalidOperationException($"El patrocinador ya esta vinculado a este torneo");

            if (contractAmount <= 0)
                throw new InvalidOperationException($"El monto del contrato debe ser un valor mayor a cero");

            var ts = new TournamentSponsor
            {
                SponsorId = sponsorId,
                TournamentId = tournamentId,
                ContractAmount = contractAmount,
                JoinedAt = DateTime.UtcNow
            };

            return await _tournamentSponsorRepository.CreateAsync(ts);
        }

        public async Task<IEnumerable<TournamentSponsor>> GetTournamentsBySponsorAsync(int sponsorId)
        {
            if (!await _sponsorRepository.ExistsAsync(sponsorId))
                throw new KeyNotFoundException($"El patrocinador con ID {sponsorId} no existe en el sistema");

            return await _tournamentSponsorRepository.GetBySponsorIdAsync(sponsorId);
        }

        public async Task UnlinkFromTournamentAsync(int sponsorId, int tournamentId)
        {
            var link = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
            if (link == null)
                throw new KeyNotFoundException($"No existe una vinculacion entre el patrocinador {sponsorId} y el torneo {tournamentId}");

            await _tournamentSponsorRepository.DeleteAsync(link.Id);
        }
    }
}
