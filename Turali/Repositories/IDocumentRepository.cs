using Turali.Models;

namespace Turali.Repositories
{
    public interface IDocumentRepository : IRepositoryBase<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsByClientIdAsync(int clientId);
    }
}