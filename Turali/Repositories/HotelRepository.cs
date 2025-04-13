using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class HotelRepository(TuraliDBContext context) 
        : RepositoryBase<Hotel>(context), IHotelRepository
    {
        public async Task<IEnumerable<Hotel>> GetHotelsByDestinationIdAsync(int destinationId)
        {
            return await _dbSet.Where(h => h.DestinationId == destinationId).ToListAsync();
        }
    }
}
