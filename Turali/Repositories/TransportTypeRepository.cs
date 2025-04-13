using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class TransportTypeRepository(TuraliDBContext context) 
        : RepositoryBase<TransportType>(context), ITransportTypeRepository
    {
    }
}
