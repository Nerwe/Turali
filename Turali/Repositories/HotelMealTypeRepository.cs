using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class HotelMealTypeRepository(TuraliDBContext context)
        : RepositoryBase<HotelMealType>(context), IHotelMealTypeRepository
    {
        public async Task<IEnumerable<HotelMealType>> GetByHotelIdAsync(int hotelId)
        {
            return await _dbSet.Where(hmt => hmt.HotelId == hotelId).ToListAsync();
        }
    }
}
