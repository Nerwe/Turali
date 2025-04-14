using Turali.Models;

namespace Turali.Repositories
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByClientIdAsync(int clientId);
    }
}