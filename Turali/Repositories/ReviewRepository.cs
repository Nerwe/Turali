using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class ReviewRepository(TuraliDBContext context)
        : RepositoryBase<Review>(context), IReviewRepository
    {
        public async Task<IEnumerable<Review>> GetReviewsByTourIdAsync(int tourId)
        {
            return await _dbSet.Where(r => r.TourId == tourId).ToListAsync();
        }
    }
}
