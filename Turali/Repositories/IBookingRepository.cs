using Turali.Models;

namespace Turali.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsByClientIdAsync(int clientId);
    }
}