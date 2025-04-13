using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class PaymentRepository(TuraliDBContext context) 
        : RepositoryBase<Payment>(context), IPaymentRepository
    {
        public async Task<IEnumerable<Payment>> GetPaymentsByClientIdAsync(int clientId)
        {
            return await _dbSet.Where(p => p.ClientId == clientId).ToListAsync();
        }
    }
}
