using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class MealTypeRepository(TuraliDBContext context) 
        : RepositoryBase<MealType>(context), IMealTypeRepository
    {
    }
}
