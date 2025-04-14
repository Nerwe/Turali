using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class ManagerRepository(TuraliDBContext context)
        : RepositoryBase<Manager>(context), IManagerRepository
    {
        public async Task<IEnumerable<Manager>> GetActiveManagersAsync()
        {
            return await _dbSet.Where(m => m.IsActive).ToListAsync();
        }
    }
}
