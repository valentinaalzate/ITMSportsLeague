using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Helpers;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Numerics;
using System.Text.RegularExpressions;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchLineupRepository _matchLineupRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly MatchValidationHelper _validationHelper;
        private readonly ILogger<MatchLineupService> _logger;

        public MatchLineupService(
            IMatchLineupRepository matchLineupRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            MatchValidationHelper validationHelper,
            ILogger<MatchLineupService> logger

            )


        {
            _matchLineupRepository = matchLineupRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _validationHelper = validationHelper;
            _logger = logger;
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");

            return await _matchLineupRepository.GetByMatchAsync(matchId);

        }

       

        public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId)
        {

            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");

            if (match.HomeTeamId != teamId && match.AwayTeamId != teamId)
                throw new InvalidOperationException(
                    "El equipo no pertenece a este partido");
            return await _matchLineupRepository.GetByMatchAndTeamAsync(matchId, teamId);

        }

        public async Task<MatchLineup?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving match lineup with ID: {MatchLineupId}", id);
            return await _matchLineupRepository.GetByIdAsync(id);
        }

        public async Task<MatchLineup> CreateAsync(MatchLineup matchLineup)
        {

            var match = await _matchRepository.GetByIdAsync(matchLineup.MatchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido {matchLineup.MatchId}");

            if (match.Status != MatchStatus.Scheduled)
                throw new InvalidOperationException(
                    "Solo se pueden registrar alineaciones en partidos Scheduled");

            var player = await _validationHelper.ValidatePlayerInMatchAsync(matchLineup.PlayerId, match);

            
            var exists = await _matchLineupRepository.ExistsByMatchAndPlayerAsync(matchLineup.MatchId, matchLineup.PlayerId);
            if (exists)
                throw new InvalidOperationException(
                    "El jugador ya está registrado en la alineación de este partido");

           
            var teamLineup = await _matchLineupRepository.GetByMatchAndTeamAsync(matchLineup.MatchId, player.TeamId);
            var starterCount = teamLineup.Count(ml => ml.IsStarter);
            if (matchLineup.IsStarter && starterCount >= 11)
                throw new InvalidOperationException(
                    "El equipo ya tiene 11 titulares registrados en este partido");

            

            exists = await _matchLineupRepository.ExistsByMatchAndJerseyNumberAsync(matchLineup.MatchId, matchLineup.JerseyNumber);
            if (exists)
                throw new InvalidOperationException(
                    "El número de camiseta ya está registrado en la alineación de este partido");

            _logger.LogInformation(
            "Registering lineup: Match {MatchId}, Player {PlayerId}, IsStarter {IsStarter}",
             matchLineup.MatchId, matchLineup.PlayerId, matchLineup.IsStarter);

            return await _matchLineupRepository.CreateAsync(matchLineup);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _matchLineupRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException(
                    $"No se encontró la alineación con ID {id}");

            _logger.LogInformation("Deleting match lineup with ID: {MatchLineupId}", id);
            await _matchLineupRepository.DeleteAsync(id);
        }
    }
}
