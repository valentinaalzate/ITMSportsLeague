using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchEventService
    {
        #region
        // MatchResult
        Task<MatchResult> RegisterResultAsync(int matchId, MatchResult result);
        Task<MatchResult?> GetResultByMatchAsync(int matchId);
        #endregion

        #region
        // Goals
        Task<Goal> RegisterGoalAsync(int matchId, Goal goal);
        Task<IEnumerable<Goal>> GetGoalsByMatchAsync(int matchId);
        Task DeleteGoalAsync(int goalId);
        #endregion

        #region
        // Cards
        Task<Card> RegisterCardAsync(int matchId, Card card);
        Task<IEnumerable<Card>> GetCardsByMatchAsync(int matchId);
        Task DeleteCardAsync(int cardId);
        #endregion

        //con esta unificacion de servicios, se puede manejar toda la lógica relacionada con los eventos del partido (resultado, goles, tarjetas) en un solo lugar, lo que facilita el mantenimiento y la coherencia de las reglas de negocio. Además, se pueden implementar validaciones específicas para cada tipo de evento dentro de este servicio, asegurando que se cumplan las reglas del torneo y del deporte en cuestión. ademas me evita tener que crear múltiples servicios para manejar cada tipo de evento, lo que simplifica la arquitectura y reduce la cantidad de código necesario para gestionar los eventos del partido.
    }

}
