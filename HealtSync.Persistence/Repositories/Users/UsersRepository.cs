using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace HealtSync.Persistence.Repositories.Users
{
    public class UsersRepository: BaseRepository<Domain.Entities.Users.Users>, IUsersRepository
    {
        readonly HealtSyncContext _context = new();
        readonly ILogger _logger;

        public UsersRepository(HealtSyncContext context, ILogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Domain.Entities.Users.Users entity)
        {
            OperationResult result = new();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad usuario es requerida.";
                return result;
            }

            if (entity.UserID < 0)
            {
                result.Success = false;
                result.Message = "El ID del los usuarios es requerido.";
                return result;
            }

            if (entity.Email.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La el EMail es requerido";
                return result;
            }

            if (entity.Password.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La Contraseña es requerida";
                return result;
            }

            if (entity.CreatedAt == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "La Fecha de Creación es requerida.";
                return result;
            }

            if (entity.RoleID <= 0)
            {
                result.Success = false;
                result.Message = "El Rol es requerido";
                return result;
            }

            try
            {
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
            OperationResult result = new();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad usuario es requerida.";
                return result;
            }

            if (entity.UserID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del los usuarios es requerido.";
                return result;
            }

            if (entity.Email.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La el EMail es requerido";
                return result;
            }

            if (entity.Password.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "La Contraseña es requerida";
                return result;
            }

            if (entity.RoleID <= 0)
            {
                result.Success = false;
                result.Message = "El Rol es requerido";
                return result;
            }

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





