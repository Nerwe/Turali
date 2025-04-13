using Turali.Models;

namespace Turali.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetDocumentsByClientIdAsync(int clientId);
    }
}