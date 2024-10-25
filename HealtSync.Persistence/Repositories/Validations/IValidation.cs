

using HealtSync.Domain.Result;

namespace HealtSync.Persistence.Repositories.Validations
{
    internal interface IValidation<TEntity>
    {
        protected OperationResult ValidateEntity(TEntity entity);
    }
}
