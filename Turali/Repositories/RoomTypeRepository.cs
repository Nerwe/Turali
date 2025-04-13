using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class RoomTypeRepository(TuraliDBContext context) 
        : RepositoryBase<RoomType>(context), IRoomTypeRepository
    {
        public async Task<IEnumerable<RoomType>> GetRoomTypesWithBasePriceAboveAsync(decimal price)
        {
            return await _dbSet.Where(rt => rt.BasePrice > price).ToListAsync();
        }
    }
}
