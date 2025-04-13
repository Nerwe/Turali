using Turali.Models;

namespace Turali.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsByTourIdAsync(int tourId);
    }
}