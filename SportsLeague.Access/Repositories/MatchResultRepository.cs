using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.DataAccess.Repositories;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.Access.Repositories
{
    public class MatchResultRepository : GenericRepository<MatchResult>, IMatchResultRepository
    {
        public MatchResultRepository(LeagueDbContext context) : base(context) { }

        public async Task<MatchResult?> GetByMatchIdAsync(int matchId)
        {
            return await _dbSet.Where(mr => mr.MatchId == matchId).FirstOrDefaultAsync();
        }
    }

}
