namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IStandingsService
    {
        //keyword o palabra reservada "object"
        //se utiliza para indicar que el metodo puede devolver
        //cualquier tipo de dato. En este caso, se espera que el metodo devuelva un objeto que contenga la informacion de las posiciones, goleadores o estadisticas de tarjetas, dependiendo del metodo llamado.
        Task<object> GetStandingsAsync(int tournamentId); //obtener la tabla de posiciones de un torneo específico
        Task<object> GetTopScorersAsync(int tournamentId); //obtener la lista de los máximos goleadores para un torneo específico
        Task<object> GetCardStatsAsync(int tournamentId);// obtener las estadísticas de tarjetas para un torneo específico, incluyendo el número de tarjetas amarillas y rojas por jugador.

    }
}
