using Turali.Models;

namespace Turali.Repositories
{
    public interface IBookingRepository : IRepositoryBase<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByClientIdAsync(int clientId);
        Task<IEnumerable<Booking>> GetBookingsByOrderIdAsync(int orderId);
    }
}