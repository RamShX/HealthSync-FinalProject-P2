using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Repositories.Validations;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace HealtSync.Persistence.Repositories.Users
{
    internal class EmployeesRepository : BaseRepository<Employees>, IEmployeesRepository
    {

        readonly HealtSyncContext _context = new();
        readonly ILogger _logger;

        private OperationResult ValidateEntity(Employees employee)
        {
            var validation = new Validation<Employees>();
            OperationResult result = new();


            validation.ValidateNotNull(employee, "El Empleado");
            validation.ValidateNumber(employee.EmployeeID, "El ID del empleado");
            validation.ValidateNotNullOrEmpty(employee.JobTitle!, "El titulo del empleo");
            validation.ValidateNotNullOrEmpty(employee.PhoneNumber!, "El número telefónico");
            validation.ValidateDate(employee.CreatedAt, "La fecha de creación");
            validation.ValidateNumber(employee.PersonID, "El ID de Persona");


            return validation.IsValid
                ? new OperationResult { Success = true }
                : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };

        }


        public EmployeesRepository(HealtSyncContext context, ILogger logger) : base(context) 
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Employees entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (await base.Exists(employee => employee.UserID == entity.UserID))
            {
                result.Success = false;
                result.Message = "Ya existe un empleado con ese usuario";
                return result;
            }
               
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el empleado";
                _logger.LogError(result.Message, ex.ToString());
                return result;
            }

            return result;
        }

        public async override Task<OperationResult> Update(Employees entity)
        {
            OperationResult result = ValidateEntity(entity);



            try
            {
                await base.Update(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el empleado";
                _logger.LogError(result.Message, ex.ToString());
                return result;
            }

            return result;
        }

        public async override Task<OperationResult> Remove(Employees entity)
        { 
            OperationResult result = new();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad es requerida.";
                return result;
            }

            if (!await base.Exists(employee => employee.EmployeeID == entity.EmployeeID))
            {
                result.Success = true;
                result.Message = "Ese empleado no está registrada";
                return result;
            }

            try
            {
                await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando el el empleado";
                _logger.LogError(result.Message, ex.ToString());
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
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el empleado";
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
                await base.GetAll();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un obteniendo los Empleados";
                _logger.LogError(result.Message, ex.ToString());
                return result;
            }
            return result;
        }
    
    }
}
