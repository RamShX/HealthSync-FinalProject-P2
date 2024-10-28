using HealtSync.Domain.Entities.System;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.System;
using HealtSync.Persistence.Interfaces.Users;
using HealtSync.Persistence.Repositories.Validations;
using Microsoft.Extensions.Logging;

namespace HealtSync.Persistence.Repositories.Users
{
    public sealed class StatusRepository : BaseRepository<Status>, IStatusRepository, IValidation<Status>
    {
        readonly HealtSyncContext _context = new();
        readonly ILogger<StatusRepository> _logger;

        public OperationResult ValidateEntity(Status status)
        {
            var validation = new Validation<Status>();
            validation.ValidateNotNull(status, "El Status");
            validation.ValidateNumber(status.StatusID, "El ID del Status");
            validation.ValidateNotNullOrEmpty(status.StatusName!, "El nombre del Status");

            return validation.IsValid
                ? new OperationResult { Success = true }
                : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };
        }

        public StatusRepository(HealtSyncContext context, ILogger<StatusRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Status entity)
        {
            OperationResult result = ValidateEntity(entity);
            if (!result.Success)
                return result;
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrió un error guardando el Status.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Update(Status entity)
        {
            OperationResult result = ValidateEntity(entity);
            if (!result.Success)
                return result;
           
            try
            {
                await base.Update(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrió un error actualizando el Status.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Remove(Status entity)
        {
            OperationResult result = new();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad es requerida.";
                    return result;
                }
                if (!await base.Exists(status => status.StatusID == entity.StatusID))
                {
                    result.Success = true;
                    result.Message = "Ese status no está registrado";
                    return result;
                }
                await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando el status";
                _logger.LogError(result.Message, ex.ToString());
                return result;
            }
            return result;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult result = new();
            if (id <= 0)
            {
                result.Success = false;
                result.Message = "Se requiere el ID";
                return result;
            }
            try
            {
                await base.GetEntityBy(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "No se pudo encontrar la entidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new();
            try
            {
                await base.GetAll();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo los datos.";
                _logger.LogError(result.Message, ex);
            }
            return result;
        }
    }
}
