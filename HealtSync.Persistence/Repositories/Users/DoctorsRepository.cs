using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using HealtSync.Persistence.Repositories.Validations;
using Microsoft.Extensions.Logging;


namespace HealtSync.Persistence.Repositories.Users
{
    public sealed class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository, IValidation<Doctors>
    {
        readonly HealtSyncContext _context = new();
        readonly ILogger<DoctorsRepository> _logger;

        public  OperationResult ValidateEntity(Doctors doctor)
        {
            var validation = new Validation<Doctors>();

            validation.ValidateNotNull(doctor, "El Doctor");
            validation.ValidateNumber(doctor.DoctorID, "El ID del Doctor");
            validation.ValidateNotNullOrEmpty(doctor.LicenseNumber!, "El número de licencia");
            validation.ValidateNotNullOrEmpty(doctor.PhoneNumber!, "El número telefónico");
            validation.ValidateNotNullOrEmpty(doctor.Education!, "La educación");
            validation.ValidateDate(doctor.LicenseExpirationDate, "La fecha de expiración de la licencia");
            validation.ValidateDate(doctor.CreatedAt, "La fecha de creación");
            validation.ValidateNumber(doctor.YearsOfExperiencie, "Los años de experiencia ");


            return validation.IsValid
              ? new OperationResult { Success = true }
              : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };

        }

        public DoctorsRepository(HealtSyncContext context, ILogger<DoctorsRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Doctors entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success)
                return result;

            try
            {
                if (await base.Exists(doctor => doctor.LicenseNumber == entity.LicenseNumber))
                {
                    result.Success = true;
                    result.Message = "Ya esa persona está registrada";
                    return result;
                }


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

        public async override Task<OperationResult> Update(Doctors entity)
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
                result = await base.Update(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error actualizando los datos del doctor.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Remove(Doctors entity) 
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

                if (!await base.Exists(doctor => doctor.LicenseNumber == entity.LicenseNumber))
                {
                    result.Success = true;
                    result.Message = "Esa persona no está registrada";
                    return result;
                }

                result = await base.Remove(entity);
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
