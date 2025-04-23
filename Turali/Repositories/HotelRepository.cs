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

        public async Task<Hotel?> GetByRoomIdAsync(int roomId)
        {
            return await _dbSet
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Rooms.Any(r => r.Id == roomId));
        }
    }
}
