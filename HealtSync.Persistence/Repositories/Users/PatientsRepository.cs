using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Repositories;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace HealtSync.Persistence.Repositories.Users
{
    public class PatientsRepository : BaseRepository<Patients>, IPatientsRepository
    {
        readonly HealtSyncContext _context = new();
        readonly ILogger _logger;

        public PatientsRepository(HealtSyncContext context, ILogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Patients entity)
        {
            OperationResult result = new();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Empleado es requerida.";
                return result;
            }

            if (entity.PatientID< 0)
            {
                result.Success = false;
                result.Message = "El ID del paciente es requerido.";
                return result;
            }

            if (entity.Address.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La dirección es requerida";
                return result;
            }

            if (await base.Exists(patient => patient.UserID == entity.UserID))
            {
                result.Success = false;
                result.Message = "Ya existe un paciente con ese usuario";
                return result;
            }

            if (entity.PhoneNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de teléfono es requerido.";
                return result;
            }

            if (entity.EmergencyContactName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El nombre del contaxto de emergencia es requerido.";
                return result;
            }

            if (entity.EmergencyContactPhone.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de emergencia es requerido.";
                return result;
            }

            if (entity.CreatedAt == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Creación es requerida.";
                return result;
            }

            if (entity.Allergies.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La  alergia es requerida.";
                return result;
            }


            try
            {
                await base.Save(entity);
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
            OperationResult result = new();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Empleado es requerida.";
                return result;
            }

            if (entity.PatientID < 0)
            {
                result.Success = false;
                result.Message = "El ID del paciente es requerido.";
                return result;
            }

            if (entity.Address.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La dirección es requerida";
                return result;
            }

            if (entity.PhoneNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de teléfono es requerido.";
                return result;
            }

            if (entity.EmergencyContactName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El nombre del contaxto de emergencia es requerido.";
                return result;
            }

            if (entity.EmergencyContactPhone.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de emergencia es requerido.";
                return result;
            }

            if (entity.CreatedAt == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Creación es requerida.";
                return result;
            }

            if (entity.Allergies.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La  alergia es requerida.";
                return result;
            }

            try
            {
                await base.Update(entity);
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
                await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando el doctor";
                return result;
            }

            return result;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult result = new();

            if (id < 0)
            {
                result.Success = false;
                result.Message = "Se Requiere el id";
                return result;
            }

            try
            {
                await base.GetEntityBy(id);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = "No se pudo encontrar la entidad";
                return result;
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
