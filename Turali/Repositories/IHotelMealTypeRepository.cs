using Turali.Models;

namespace Turali.Repositories
{
    public interface IHotelMealTypeRepository
    {
        Task<IEnumerable<HotelMealType>> GetByHotelIdAsync(int hotelId);
    }
}