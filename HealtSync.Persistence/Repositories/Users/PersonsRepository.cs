using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;
using HealtSync.Persistence.Repositories.Validations;
using Microsoft.IdentityModel.Tokens;


namespace HealtSync.Persistence.Repositories.Users
{
    public class PersonsRepository : BaseRepository<Persons>, IPersonsRepository
    {
        readonly HealtSyncContext _context;
        readonly ILogger _logger;

        public PersonsRepository(HealtSyncContext context, ILogger<PersonsRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        //Handle Validations
        private OperationResult ValidateEntity(Persons person)
        {
            var validation = new Validation<Persons>();
            OperationResult result = new ();


            validation.ValidateNotNull(person, "Persona");
            validation.ValidateNotNullOrEmpty(person.FirstName!, "El nombre");
            validation.ValidateNotNullOrEmpty(person.LastName!, "El apellido");
            validation.ValidateDate(person.DateOfBirth, "La fecha de nacimiento");

            return validation.IsValid 
                ? new OperationResult { Success = true } 
                : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) }; 
        }


        public async override Task<OperationResult> Save(Persons entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success)
                return result;
            try
            {

                if (await base.Exists(person => person.IdentificationNumber == entity.IdentificationNumber))
                {
                    result.Success = true;
                    result.Message = "Ya esa persona está registrada";
                    return result;
                }
              

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error guardando el la persona.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Update(Persons entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success)
                return result;

            try
            {
                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error actualizando el la persona.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Remove(Persons entity)
        {
            OperationResult result = new();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad es requerida.";
                return result;
            }

            if (!await base.Exists(person => person.IdentificationNumber == entity.IdentificationNumber))
            {
                result.Success = false;
                result.Message = "La entidad no existe";
                return result;
            }

            try
            {
                result = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando a la persona";
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

        public async override Task<OperationResult> SaveChanges()
        {
            OperationResult result = new();

            try
            {
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error guardando cambios.";
                _logger.LogError(result.Message, ex);
            }
            return result;

        }
    }
}
