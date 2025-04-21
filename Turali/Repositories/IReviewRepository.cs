using Turali.Models;

namespace Turali.Repositories
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByClientIdAsync(int clientId);
        Task<IEnumerable<Review>> GetReviewsByTourIdAsync(int tourId);
    }
}