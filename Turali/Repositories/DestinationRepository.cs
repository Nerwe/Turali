using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class DestinationRepository(TuraliDBContext context) 
        : RepositoryBase<Destination>(context), IDestinationRepository
    {
    }
}
