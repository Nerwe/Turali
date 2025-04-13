using Turali.Models;

namespace Turali.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
    }
}