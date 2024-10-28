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
    public class UsersRepository : BaseRepository<Domain.Entities.Users.Users>, IUsersRepository
    {
        readonly HealtSyncContext _context = new();
        readonly ILogger _logger;

        private OperationResult ValidateEntity(Domain.Entities.Users.Users user)
        {
            var validation = new Validation<Domain.Entities.Users.Users>();

            validation.ValidateNotNull(user, "El Usuario");
            validation.ValidateNumber(user.UserID, "El ID del Usuario");
            validation.ValidateNotNullOrEmpty(user.Email!, "El Email");
            validation.ValidateNotNullOrEmpty(user.Password!, "La Contraseña");
            validation.ValidateNumber(user.RoleID, "EL Rol");
            validation.ValidateDate(user.CreatedAt, "Fecha de creación");

            return validation.IsValid
              ? new OperationResult { Success = true }
              : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };
        }

        public UsersRepository(HealtSyncContext context, ILogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Domain.Entities.Users.Users entity)
        {
            OperationResult result = ValidateEntity(entity);

            if (!result.Success) 
                return result;

            try
            {
                if (await base.Exists(user => user.UserID == entity.UserID))
                {
                    result.Success = true;
                    result.Message = "Ya este usuario está registrada";
                    return result;
                }

                await base.Save(entity);
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error guardando el Usuario.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;

        }

        public async override Task<OperationResult> Update(Domain.Entities.Users.Users entity)
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
                result.Message = "Ocurrio un error guardando el Usuario.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }


            return result;
        }

        public async override Task<OperationResult> Remove(Domain.Entities.Users.Users entity)
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
                result.Message = "Ha ocurrido un error borrando el usuario";
                _logger.LogError(result.Message, ex);

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
                await base.GetEntityBy(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "No se pudo encontrar la entidad";
                _logger.LogError(result.Message, ex);

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





