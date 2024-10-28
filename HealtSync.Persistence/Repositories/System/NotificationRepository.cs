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
    public sealed class NotificationsRepository : BaseRepository<Notifications>, INotificationsRepository, IValidation<Notifications>
    {
        readonly HealtSyncContext _context = new();
        readonly ILogger<NotificationsRepository> _logger;

        public OperationResult ValidateEntity(Notifications notification)
        {
            var validation = new Validation<Notifications>();
            validation.ValidateNotNull(notification, "La Notificación");
            validation.ValidateNumber(notification.NotificationID, "El ID de la Notificación");
            validation.ValidateNumber(notification.UserID, "El ID del Usuario");
            validation.ValidateNotNullOrEmpty(notification.Message!, "El mensaje");
            validation.ValidateDate(notification.SentAt, "La fecha de envío");

            return validation.IsValid
                ? new OperationResult { Success = true }
                : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };
        }

        public NotificationsRepository(HealtSyncContext context, ILogger<NotificationsRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Notifications entity)
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
                result.Message = "Ocurrió un error guardando la Notificación.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Update(Notifications entity)
        {
            OperationResult result = ValidateEntity(entity);
            if (!result.Success)
                return result;
            if (entity.UpdatedAt == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Actualización es requerida.";
                return result;
            }
            try
            {
                await base.Update(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrió un error actualizando la Notificación.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Remove(Notifications entity)
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
                if (!await base.Exists(notification => notification.NotificationID == entity.NotificationID))
                {
                    result.Success = true;
                    result.Message = "Esa notificación no está registrada";
                    return result;
                }
                await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando la notificación";
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
