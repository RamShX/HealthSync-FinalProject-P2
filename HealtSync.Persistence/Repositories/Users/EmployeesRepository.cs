using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Persistence.Repositories.Users
{
    internal class EmployeesRepository : BaseRepository<Employees>, IEmployeesRepository
    {

        readonly HealtSyncContext _context = new();
        readonly ILogger _logger;

        public EmployeesRepository(HealtSyncContext context, ILogger logger) : base(context) 
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<OperationResult> Save(Employees entity)
        {
            OperationResult result = new();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Empleado es requerida.";
                return result;
            }

            if (entity.EmployeeID < 0)
            {
                result.Success = false;
                result.Message = "El ID del empleado es requerido.";
                return result;
            }

            if (entity.JobTitle.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El  nombre del puesto es requerido";
                return result;
            }

            if (await base.Exists(employee => employee.UserID == entity.UserID))
            {
                result.Success = false;
                result.Message = "Ya existe un empleado con ese usuario";
                return result;
            }

            if (entity.PhoneNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de teléfono es requerido.";
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
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error guardando el Doctor.";
                result.Success = false;

                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Update(Employees entity)
        {
            OperationResult result = new();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad Empleado es requerida.";
                return result;
            }

            if (entity.EmployeeID < 0)
            {
                result.Success = false;
                result.Message = "El ID del empleado es requerido.";
                return result;
            }

            if (entity.JobTitle.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El  nombre del puesto es requerido";
                return result;
            }

            if (await base.Exists(employee => employee.UserID == entity.UserID))
            {
                result.Success = false;
                result.Message = "Ya existe un empleado con ese usuario";
                return result;
            }

            if (entity.PhoneNumber.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "El número de teléfono es requerido.";
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

        public async override Task<OperationResult> Remove(Employees entity)
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
