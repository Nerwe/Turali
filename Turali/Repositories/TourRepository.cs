using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class TourRepository(TuraliDBContext context) 
        : RepositoryBase<Tour>(context), ITourRepository
    {
        public async Task<IEnumerable<Tour>> GetActiveToursAsync()
        {
            return await _dbSet.Where(t => t.Status == "active").ToListAsync();
        }
    }
}
