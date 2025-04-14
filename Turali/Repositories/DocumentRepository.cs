using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class DocumentRepository(TuraliDBContext context)
        : RepositoryBase<Document>(context), IDocumentRepository
    {
        public async Task<IEnumerable<Document>> GetDocumentsByClientIdAsync(int clientId)
        {
            return await _dbSet.Where(d => d.ClientId == clientId).ToListAsync();
        }
    }
}
