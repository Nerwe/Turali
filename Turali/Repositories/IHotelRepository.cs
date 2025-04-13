using Turali.Models;

namespace Turali.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetHotelsByDestinationIdAsync(int destinationId);
    }
}