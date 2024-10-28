

using HealtSync.Domain.Entities.Insurance;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace HealtSync.Persistence.Repositories.Insurance
{
    public class InsuranceProvidersRepository(HealtSyncContext healtSyncContext,
        ILogger<IInsuranceProviders> logger) : BaseRepository<InsuranceProviders>(healtSyncContext), IInsuranceProviders
    {
        private readonly HealtSyncContext _healtSyncContext = healtSyncContext;
        private readonly ILogger<IInsuranceProviders> logger = logger;
        public async override Task<bool> Exists(Expression<Func<InsuranceProviders, bool>> filter)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                bool exists = await _healtSyncContext.InsuranceProviders.AnyAsync(filter);
                operationResult.Success = true;
                operationResult.Data = exists;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al verificar la existencia del proveedor de seguro.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult.Data;
        }

        public async override Task<OperationResult> Remove(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null || entity.InsuranceProviderID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID del proveedor de seguro es requerido para eliminar.";
                return operationResult;
            }

            try
            {
                InsuranceProviders? providerToRemove = await _healtSyncContext.InsuranceProviders.FindAsync(entity.InsuranceProviderID);

                if (providerToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Proveedor de seguro no encontrado.";
                    return operationResult;
                }
                
                return operationResult = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al eliminar el proveedor de seguro.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Save(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                operationResult.Success = false;
                operationResult.Message = "El nombre es requerido.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.PhoneNumber) || entity.PhoneNumber.Length > 10)
            {
                operationResult.Success = false;
                operationResult.Message = "El número de contacto es requerido y debe ser de máximo 15 caracteres.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Email))
            {
                operationResult.Success = false;
                operationResult.Message = "El correo electrónico es requerido.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Address))
            {
                operationResult.Success = false;
                operationResult.Message = "La dirección es requerida.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.CoverageDatails))
            {
                operationResult.Success = false;
                operationResult.Message = "Los detalles de cobertura son requeridos.";
                return operationResult;
            }

            if (entity.NetworkTypeld <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El tipo de red es requerido.";
                return operationResult;
            }

            
            entity.CreatedAt = DateTime.Now;
            entity.IsActive = true; 

            try
            {
                return operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al guardar el proveedor de seguro.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Update(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                operationResult.Success = false;
                operationResult.Message = "El nombre es requerido.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.PhoneNumber) || entity.PhoneNumber.Length > 10)
            {
                operationResult.Success = false;
                operationResult.Message = "El número de contacto es requerido y debe ser de máximo 10 caracteres.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Email))
            {
                operationResult.Success = false;
                operationResult.Message = "El correo electrónico es requerido.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Address))
            {
                operationResult.Success = false;
                operationResult.Message = "La dirección es requerida.";
                return operationResult;
            }

            if (string.IsNullOrWhiteSpace(entity.CoverageDatails))
            {
                operationResult.Success = false;
                operationResult.Message = "Los detalles de cobertura son requeridos.";
                return operationResult;
            }

            if (entity.NetworkTypeld <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El tipo de red es requerido.";
                return operationResult;
            }

            try
            {
                InsuranceProviders? providerToUpdate = await _healtSyncContext.InsuranceProviders.FindAsync(entity.InsuranceProviderID);

                if (providerToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Proveedor de seguro no encontrado.";
                    return operationResult;

                }

                providerToUpdate.Name = entity.Name;
                providerToUpdate.PhoneNumber = entity.PhoneNumber;
                providerToUpdate.Email = entity.Email;
                providerToUpdate.Website = entity.Website;
                providerToUpdate.Address = entity.Address;
                providerToUpdate.City = entity.City;
                providerToUpdate.State = entity.State;
                providerToUpdate.Country = entity.Country;
                providerToUpdate.ZipCode = entity.ZipCode;
                providerToUpdate.CoverageDatails = entity.CoverageDatails;
                providerToUpdate.LogoUrl = entity.LogoUrl;
                providerToUpdate.IsPreferred = entity.IsPreferred;
                providerToUpdate.NetworkTypeld = entity.NetworkTypeld;
                providerToUpdate.CustomerSupportContact = entity.CustomerSupportContact;
                providerToUpdate.AcceptedRegions = entity.AcceptedRegions;
                providerToUpdate.MaxCoverageAmount = entity.MaxCoverageAmount;
                providerToUpdate.IsActive = entity.IsActive;
                providerToUpdate.UpdatedAt = DateTime.Now;

                return operationResult = await base.Update(providerToUpdate);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al actualizar el proveedor de seguro.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
    }
}
