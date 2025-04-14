using Turali.Models;

namespace Turali.Repositories
{
    public interface IRoomTypeRepository : IRepositoryBase<RoomType>
    {
        Task<IEnumerable<RoomType>> GetRoomTypesWithBasePriceAboveAsync(decimal price);
    }
}