using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Repositories;
using HealtSync.Domain.Result;

namespace HealtSync.Persistence.Interfaces.Users
{
    public interface IPersonsRepository : IBaseRepository <Persons>
    {
        Task<OperationResult> SaveChanges();
    }
}
