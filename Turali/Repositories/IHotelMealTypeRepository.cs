using Turali.Models;

namespace Turali.Repositories
{
    public interface IHotelMealTypeRepository : IRepositoryBase<HotelMealType>
    {
        Task<IEnumerable<HotelMealType>> GetByHotelIdAsync(int hotelId);
    }
}