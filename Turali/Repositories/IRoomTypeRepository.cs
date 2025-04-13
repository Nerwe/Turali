using Turali.Models;

namespace Turali.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetRoomTypesWithBasePriceAboveAsync(decimal price);
    }
}