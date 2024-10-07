using HealtSync.Domain.Result;
using System.Linq.Expressions;


namespace HealtSync.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> Save (TEntity entity);
        Task<OperationResult> Update (TEntity entity);
        Task<OperationResult> Delete(TEntity entity);
        Task<OperationResult> GetAll ();
        Task<OperationResult> GetEntittyBy (int id);
        Task<OperationResult> Exists(Expression<Func<TEntity, bool>> filter);
    }
}
