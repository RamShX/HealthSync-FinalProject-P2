using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace HealtSync.Persistence.Repositories.Users
{
    public class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        HealtSyncContext _context;
        ILogger<DoctorsRepository> _logger;

        public DoctorsRepository(HealtSyncContext context, ILogger<DoctorsRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Doctors entity)
        {
            OperationResult result = new ();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Doctor es requerida.";
                return result;
            }

            if (entity.DoctorID < 0)
            {
                result.Success = false;
                result.Message = "El ID del Doctor es requerido.";
                return result;
            }

            if (entity.LicenseNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de licencia es requerido.";
                return result;
            }

            if (await base.Exists(doctor => doctor.LicenseNumber == entity.LicenseNumber))
            {
                result.Success = false;
                result.Message = "Existe un doctor con esa licencia";
                return result;
            }

            if (entity.PhoneNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de teléfono es requerido.";
                return result;
            }

            if (entity.YearsOfExperiencie < 0)
            {
                result.Success = false;
                result.Message = "Los años de experiencia son requeridos";
                return result;
            }

            if (entity.Education.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de licencia es requerido.";
                return result;
            }

            if (entity.LicenseExpirationDate == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Expiración de la Licencia es requerida.";
                return result;
            }

            if (entity.CreatedAt == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Creación es requerida.";
                return result;
            }

            try
            { 
                await base.Save(entity);
            }
            catch(Exception ex)
            {
                result.Message = "Ocurrio un error guardando el Doctor.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Update(Doctors entity)
        {
            OperationResult result = new();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Doctor es requerida.";
                return result;
            }

            if (entity.DoctorID < 0)
            {
                result.Success = false;
                result.Message = "El ID del Doctor es requerido.";
                return result;
            }

            if (entity.LicenseNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de licencia es requerido.";
                return result;
            }

            if (await base.Exists(doctor => doctor.LicenseNumber == entity.LicenseNumber))
            {
                result.Success = false;
                result.Message = "Existe un doctor con esa licencia";
                return result;
            }

            if (entity.PhoneNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de teléfono es requerido.";
                return result;
            }

            if (entity.YearsOfExperiencie < 0)
            {
                result.Success = false;
                result.Message = "Los años de experiencia son requeridos";
                return result;
            }

            if (entity.Education.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de licencia es requerido.";
                return result;
            }

            if (entity.LicenseExpirationDate == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Expiración de la Licencia es requerida.";
                return result;
            }

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
                result.Message = "Ocurrio un error actualizando los datos del doctor.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;


            return result;
        }

        public async override Task<OperationResult> Remove(Doctors entity)
        {
            OperationResult result = new();

            if(entity == null)
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
            
            if(id < 0)
            {
                result.Success = false;
                result.Message = "Se Requiere el id";
                return result;
            }

            try
            {
                await base.GetEntityBy(id);
            }
            catch(Exception e)
            {
                result.Success = false;
                result.Message = "No se pudo encontrar la entidad";
                return result;
            }

            

            return result;
        }
        public async override Task<OperationResult> GetAll()
        {
           
        }


    }
}
