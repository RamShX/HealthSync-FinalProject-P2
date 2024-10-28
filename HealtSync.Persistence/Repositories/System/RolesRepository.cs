using HealtSync.Domain.Entities.System;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.System;
using HealtSync.Persistence.Repositories.Validations;
using Microsoft.Extensions.Logging;

namespace HealtSync.Persistence.Repositories.System
{
    public sealed class RolesRepository : BaseRepository<Roles>, IRolesRepository, IValidation<Roles>
    {
        private readonly HealtSyncContext _context;
        private readonly ILogger<RolesRepository> _logger;

        public RolesRepository(HealtSyncContext context, ILogger<RolesRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public OperationResult ValidateEntity(Roles role)
        {
            var validation = new Validation<Roles>();

            validation.ValidateNotNull(role, "El Rol");
            validation.ValidateNotNullOrEmpty(role.RoleName, "El Nombre del Rol");
            validation.ValidateDate(role.CreatedAt, "La Fecha de Creación");

            return validation.IsValid
                ? new OperationResult { Success = true }
                : new OperationResult { Success = false, Message = string.Join(", ", validation.ErrorMessages) };
        }

        public async override Task<OperationResult> Save(Roles entity)
        {
            OperationResult result = ValidateEntity(entity);

            try
            {
                if (await base.Exists(role => role.RoleName == entity.RoleName))
                {
                    result.Success = false;
                    result.Message = "Ya existe un rol con este nombre";
                    return result;
                }

                await base.Save(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error guardando el rol.";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Update(Roles entity)
        {
            OperationResult result = ValidateEntity(entity);

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
                result.Success = false;
                result.Message = "Ocurrió un error actualizando el rol.";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Remove(Roles entity)
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

                if (!await base.Exists(role => role.RoleID == entity.RoleID))
                {
                    result.Success = false;
                    result.Message = "El rol no está registrado";
                    return result;
                }

                await base.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando el rol";
                _logger.LogError(result.Message, ex.ToString());
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
                result = await base.GetEntityBy(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "No se pudo encontrar el rol.";
                _logger.LogError(result.Message, ex.ToString());
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
                result.Message = "Ocurrió un error obteniendo los datos de los roles.";
                _logger.LogError(result.Message, ex);
            }
            return result;
        }
    }
}
