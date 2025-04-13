using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class CommunicationRepository(TuraliDBContext context) 
        : RepositoryBase<Communication>(context), ICommunicationRepository
    {
        public async Task<IEnumerable<Communication>> GetCommunicationsByClientIdAsync(int clientId)
        {
            return await _dbSet.Where(c => c.ClientId == clientId).ToListAsync();
        }
    }
}
