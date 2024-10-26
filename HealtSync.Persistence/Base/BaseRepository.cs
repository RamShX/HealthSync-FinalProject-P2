using HealtSync.Domain.Repositories;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealtSync.Persistence.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly HealtSyncContext _healtSyncContext;
        private DbSet<TEntity> _entities;

        public BaseRepository(HealtSyncContext healtSyncContext)
        {
            _healtSyncContext = healtSyncContext;
            _entities = _healtSyncContext.Set<TEntity>();
        }
        public virtual async Task<OperationResult> Exists(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                var exists = await _entities.FindAsync(filter);
                result.Data = exists;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error obteniendo verificando el registrio: {ex}";
            }

            return result;
        }

        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var data = await _entities.ToListAsync();
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error obteniendo los datos: {ex}";
            }

            return result;
        }

        public virtual async Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var data = await _entities.FindAsync(id);
                result.Data = data;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió el error: {ex} verificando que existe el registro";
            }

            return result;
        }

        public virtual async Task<OperationResult> Remove(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _entities.Remove(entity);
                await _healtSyncContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió el error: {ex} elminando el registro";
            }

            return result;
        }

        public virtual async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _entities.Add(entity);
                await _healtSyncContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió el error: {ex} guardando el registro";
            }

            return result;
        }

        public virtual async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _entities.Update(entity);
                await _healtSyncContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió el error: {ex} actualizando el registro";
            }

            return result;
        }
    }
}
