using Turali.Models;

namespace Turali.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsByClientIdAsync(int clientId);
    }
}