using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class RoomRepository(TuraliDBContext context) 
        : RepositoryBase<Room>(context), IRoomRepository
    {
        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
        {
            return await _dbSet.Where(r => r.IsAvailable).ToListAsync();
        }
    }
}
