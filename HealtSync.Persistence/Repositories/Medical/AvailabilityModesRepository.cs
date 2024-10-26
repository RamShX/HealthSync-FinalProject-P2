using HealtSync.Domain.Entities.Medical;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Medical;
using HealtSync.Persistence.Repositories.Validations;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HealtSync.Persistence.Repositories.Medical
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes>, IAvailabilityModesRepository, IValidation<AvailabilityModes>
    {
        private readonly HealtSyncContext _context;
        private readonly ILogger _logger;

        public AvailabilityModesRepository(ILogger logger, HealtSyncContext context) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public OperationResult ValidateEntity(AvailabilityModes entity)
        {
            var validation = new Validation<AvailabilityModes>();

            validation.ValidateNotNull(entity, "El modo de disponibilidad");
            validation.ValidateNumber(entity.AvailabilityModeID, "El ID del modo de disponibilidad");
            validation.ValidateDate(entity.CreatedAt, "La fecha de creación");

            return validation.IsValid
                ? new OperationResult { Success = true }
                : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };
        }

        public async override Task<OperationResult> Save(AvailabilityModes entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success)
                return result;

            try
            {
                await base.Save(entity);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el modo de disponibilidad.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }

        public async override Task<OperationResult> Update(AvailabilityModes entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success)
                return result;

            try
            {
                await base.Update(entity);
                result.Success = true;
                result.Message = "Modo de disponibilidad actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el modo de disponibilidad.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }

        public async override Task<OperationResult> Remove(AvailabilityModes entity)
        {
            OperationResult result = new();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad es requerida.";
                return result;
            }

            try
            {
                await base.Remove(entity);
                result.Success = true;
                result.Message = "Modo de disponibilidad eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el modo de disponibilidad.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult result = new();

            if (id <= 0)
            {
                result.Success = false;
                result.Message = "Se requiere un ID válido.";
                return result;
            }

            try
            {
                var entity = await base.GetEntityBy(id);
                result.Data = entity;
                result.Success = entity != null;
                result.Message = entity != null ? "Modo de disponibilidad encontrado." : "No se encontró el modo de disponibilidad.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el modo de disponibilidad.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new();

            try
            {
                var entities = await base.GetAll();
                result.Data = entities;
                result.Success = true;
                result.Message = "Modos de disponibilidad obtenidos exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los modos de disponibilidad.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }
    }
}
