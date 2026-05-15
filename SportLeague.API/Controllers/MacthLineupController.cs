using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportLeague.API.DTOs.Request;
using SportLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportLeague.API.Controllers

{
    [ApiController]
    [Route("api/match/{matchId}")]
    public class MacthLineupController : ControllerBase
    {
        private readonly IMatchLineupService _matchLineupService;
        private readonly IMapper _mapper;

        public MacthLineupController(
            IMatchLineupService matchLineupService, IMapper mapper)
        {
            _matchLineupService = matchLineupService;
            _mapper = mapper;
        }

        [HttpPost("lineup")]
        public async Task<ActionResult> RegisterLineup(
            int matchId, MatchLineupRequestDTO dto)
        {
            try
            {
                var lineup = _mapper.Map<MatchLineup>(dto);
                lineup.MatchId = matchId;
                var created = await _matchLineupService.CreateAsync(lineup);
                var lineupWithDetails = await _matchLineupService.GetByIdAsync(created.Id);
                var responseDto = _mapper.Map<MatchLineupResponseDTO>(lineupWithDetails);
                return CreatedAtAction(nameof(GetById), new { matchId = matchId, id = created.Id }, responseDto);


            }

            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }

        }

        [HttpGet("lineup/{id}")]
        public async Task<ActionResult<MatchLineupResponseDTO>> GetById(int matchId, int id)
        {
            try
            {
                var lineup = await _matchLineupService.GetByIdAsync(id);
                if (lineup == null)
                    return NotFound(new { message = $"Alineación con ID {id} no encontrada" });
                return Ok(_mapper.Map<MatchLineupResponseDTO>(lineup));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpGet("lineup")]
        public async Task<ActionResult<IEnumerable<MatchLineupResponseDTO>>> GetLineupByMatch(int matchId)
        {
            try
            {
                var lineups = await _matchLineupService.GetByMatchAsync(matchId);
                return Ok(_mapper.Map<IEnumerable<MatchLineupResponseDTO>>(lineups));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpGet("lineup/team/{teamId}")]
        public async Task<ActionResult<IEnumerable<MatchLineupResponseDTO>>> GetLineupByMatchAndTeam(int matchId, int teamId)
        {
            try
            {
                var lineups = await _matchLineupService.GetByMatchAndTeamAsync(matchId, teamId);
                return Ok(_mapper.Map<IEnumerable<MatchLineupResponseDTO>>(lineups));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }

        }

        [HttpDelete("lineup/{id}")]
        public async Task<ActionResult> DeleteLineup(int matchId, int id)
        {
            try
            {
                await _matchLineupService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
