using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class BookingRepository(TuraliDBContext context)
        : RepositoryBase<Booking>(context), IBookingRepository
    {
        public async Task<IEnumerable<Booking>> GetBookingsByClientIdAsync(int clientId)
        {
            return await _dbSet
                .Where(b => b.ClientId == clientId)
                .ToListAsync();
        }
    }
}
