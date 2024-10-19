using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace HealtSync.Persistence.Repositories.Users
{
    public class PersonsRepository : BaseRepository<Persons>, IPersonsRepository
    {
        readonly HealtSyncContext _context;
        readonly ILogger _logger;

        public PersonsRepository(HealtSyncContext context, ILogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }


        public async override Task<OperationResult> Save(Persons entity)
        {
            OperationResult result = new();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Persona es requerida.";
                return result;
            }

            if (entity.PersonID < 0)
            {
                result.Success = false;
                result.Message = "El ID de la Persona es requerido.";
                return result;
            }

            if (entity.FirstName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El nombre es requerido.";
                return result;
            }

            if (entity.LastName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El apellido es requerido.";
                return result;
            }

            if (await base.Exists(person => person.IdentificationNumber == entity.IdentificationNumber))
            {
                result.Success = false;
                result.Message = "Ya esa persona está registrada";
                return result;
            }

            if (entity.DateOfBirth == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Nacimiento es requerida.";
                return result;
            }

            try
            {
                await base.Save(entity);
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
            OperationResult result = new();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Persona es requerida.";
                return result;
            }

            if (entity.PersonID < 0)
            {
                result.Success = false;
                result.Message = "El ID de la Persona es requerido.";
                return result;
            }

            if (entity.FirstName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El nombre es requerido.";
                return result;
            }

            if (entity.LastName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El apellido es requerido.";
                return result;
            }

            if (entity.DateOfBirth == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Nacimiento es requerida.";
                return result;
            }

            try
            {
                await base.Save(entity);
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

            try
            {
                await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando a la persona";
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
