using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.DataAccess.Repositories;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.Access.Repositories
{
    public class RefereeRepository : GenericRepository<Referee>, IRefereeRepository
    {
        public RefereeRepository(LeagueDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Referee>> GetByNationalityAsync(string nationality)
        {
            return await _dbSet
                .Where(r => r.Nationality.ToLower() == nationality.ToLower())
                .ToListAsync();
        }
    }

}
