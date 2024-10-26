
using HealtSync.Domain.Entities.Insurance;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace HealtSync.Persistence.Repositories.Insurance
{
    public class NetworkTypeRepository(HealtSyncContext healtSyncContext,
        ILogger<NetworkTypeRepository> logger) : BaseRepository<NetworkType>(healtSyncContext), INetworkType
    {
        private readonly ILogger<NetworkTypeRepository> logger = logger;
        private readonly HealtSyncContext _healtSyncContext = healtSyncContext;

        public async override Task<OperationResult> Exists(Expression<Func<NetworkType, bool>> filter)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                bool exists = await _healtSyncContext.NetworkType.AnyAsync(filter);
                operationResult.Success = true;
                operationResult.Data = exists;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al verificar la existencia del tipo de red.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Remove(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (entity.NetworkTypeld <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID del tipo de red es requerido para eliminar.";
                return operationResult;
            }

            try
            {
                NetworkType? networkTypeToRemove = await _healtSyncContext.NetworkType.FindAsync(entity.NetworkTypeld);

                if (networkTypeToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Tipo de red no encontrado.";
                    return operationResult;
                }

                return operationResult = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al eliminar el tipo de red.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Save(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                operationResult.Success = false;
                operationResult.Message = "El nombre es requerido.";
                return operationResult;
            }

            entity.CreatedAt = DateTime.Now;
            entity.IsActive = true;

            try
            {
                return operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al guardar el tipo de red.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Update(NetworkType entity) 
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                operationResult.Success = false;
                operationResult.Message = "El nombre es requerido.";
                return operationResult;
            }

            try
            {
                NetworkType? networkTypeToUpdate = await _healtSyncContext.NetworkType.FindAsync(entity.NetworkTypeld);

                if (networkTypeToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Tipo de red no encontrado.";
                    return operationResult;
                }

                networkTypeToUpdate.Name = entity.Name;
                networkTypeToUpdate.Description = entity.Description;
                networkTypeToUpdate.IsActive = entity.IsActive;
                networkTypeToUpdate.UpdatedAt = DateTime.Now;

                return operationResult = await base.Update(networkTypeToUpdate);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al actualizar el tipo de red.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

    }
}
