using Turali.Models;

namespace Turali.Repositories
{
    public interface IRoomRepository : IRepositoryBase<Room>
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
    }
}