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
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords>, IMedicalRecordsRepository, IValidation<MedicalRecords>
    {
        private readonly HealtSyncContext _context;
        private readonly ILogger _logger;

        public MedicalRecordsRepository(ILogger logger, HealtSyncContext context) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public OperationResult ValidateEntity(MedicalRecords entity)
        {
            var validation = new Validation<MedicalRecords>();

            validation.ValidateNotNull(entity, "El registro médico");
            validation.ValidateNumber(entity.PatientID, "El ID del paciente");
            validation.ValidateNumber(entity.DoctorID, "El ID del doctor");
            validation.ValidateNotNullOrEmpty(entity.Diagnosis!, "El diagnóstico");
            validation.ValidateNotNullOrEmpty(entity.Treatment!, "El tratamiento");
            validation.ValidateDate(entity.DateOfVisit, "La fecha de visita");
            validation.ValidateDate(entity.CreatedAt, "La fecha de creación");

            return validation.IsValid
                ? new OperationResult { Success = true }
                : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };
        }

        public async override Task<OperationResult> Save(MedicalRecords entity)
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
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el registro médico.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }

        public async override Task<OperationResult> Update(MedicalRecords entity)
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
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el registro médico.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }

        public async override Task<OperationResult> Remove(MedicalRecords entity)
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
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando el registro médico.";
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
                result.Success = true;
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "No se pudo encontrar el registro médico.";
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
                result.Success = true;
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo los registros médicos.";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }
    }
}
