using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Repositories;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using HealtSync.Persistence.Repositories.Validations;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace HealtSync.Persistence.Repositories.Users
{
    public class PatientsRepository : BaseRepository<Patients>, IPatientsRepository, IValidation<Patients>
    {
        readonly HealtSyncContext _context = new();
        readonly ILogger _logger;

        public OperationResult ValidateEntity(Patients patient)
        {
            var validation = new Validation<Patients>();

            validation.ValidateNotNull(patient, "El Paciente");
            validation.ValidateNotNullOrEmpty(patient.Address!, "La Dirección");
            validation.ValidateNotNullOrEmpty(patient.PhoneNumber!, "El número telefónico");
            validation.ValidateNotNullOrEmpty(patient.EmergencyContactName!, "El nombre del contacto de emergencia");
            validation.ValidateNotNullOrEmpty(patient.EmergencyContactPhone!, "El nombre del contacto de emergencia");
            validation.ValidateNotNullOrEmpty(patient.Allergies!, "La alergia ");
            validation.ValidateDate(patient.CreatedAt, "La fecha de creación");


            return validation.IsValid
              ? new OperationResult { Success = true }
              : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };

        }

        public PatientsRepository(HealtSyncContext context, ILogger<PatientsRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }


        public async override Task<OperationResult> Save(Patients entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success)
                return result;

            if (await base.Exists(patient => patient.PatientID == entity.PatientID))
            {
                result.Success = false;
                result.Message = "Ya existe un paciente con ese usuario";
                return result;
            }


            try
            {
                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error guardando el Doctor.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Update(Patients entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success)
                return result;


            try
            {
                result = await base.Update(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error guardando el Doctor.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Remove(Patients entity)
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
                result =  await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando el doctor";
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
                result.Message = "Se Requiere el id";
                return result;
            }

            try
            {
                result = await base.GetEntityBy(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "No se pudo encontrar la entidad";
                _logger.LogError(result.Message, ex.ToString());
                return result;
            }



            return result;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new();

            try
            {
                result = await base.GetAll();
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

