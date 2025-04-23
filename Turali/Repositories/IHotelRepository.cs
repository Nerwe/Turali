using Turali.Models;

namespace Turali.Repositories
{
    public interface IHotelRepository : IRepositoryBase<Hotel>
    {
        Task<IEnumerable<Hotel>> GetHotelsByDestinationIdAsync(int destinationId);
        Task<Hotel?> GetByRoomIdAsync(int roomId);
    }
}