using HealtSync.Domain.Entities.Appointments;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Appointments;
using Microsoft.Extensions.Logging;

namespace HealtSync.Persistence.Repositories.Appointments
{
    public sealed class DoctorAvailabilityRepository(HealtSyncContext healtSyncContext,
    ILogger<DoctorAvailabilityRepository> logger) : BaseRepository<DoctorAvailability>(healtSyncContext), IDoctorAvailability
    {
        private readonly ILogger<DoctorAvailabilityRepository> logger = logger;
        private readonly HealtSyncContext _healtSyncContext = healtSyncContext;

        public async override Task<OperationResult> Save(DoctorAvailability availability) 
        {
            OperationResult operationResult = new OperationResult();
            if (availability == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (availability.AvailabilityID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID de disponibilidad es requerido.";
                return operationResult;
            }

            if (availability.AvailableDate.Date < DateTime.Today)
            {
                operationResult.Success = false;
                operationResult.Message = "La fecha de disponibilidad debe ser hoy o en el futuro.";
                return operationResult;
            }

            if (availability.Startime < TimeOnly.MinValue || availability.Startime > TimeOnly.MaxValue)
            {
                operationResult.Success = false;
                operationResult.Message = "La hora de inicio debe estar en el rango válido (00:00 - 23:59).";
                return operationResult;
            }

            if (availability.EndTime < TimeOnly.MinValue || availability.EndTime > TimeOnly.MaxValue)
            {
                operationResult.Success = false;
                operationResult.Message = "La hora de fin debe estar en el rango válido (00:00 - 23:59).";
                return operationResult;
            }

            if (availability.Startime>= availability.EndTime)
            {
                operationResult.Success = false;
                operationResult.Message = "La hora de inicio debe ser anterior a la hora de fin.";
                return operationResult;
            }

            try
            {
                operationResult = await base.Save(availability);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al guardar la disponibilidad del doctor.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Update(DoctorAvailability availability)
        {
            OperationResult operationResult = new OperationResult();

            if (availability == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad de disponibilidad es requerida.";
                return operationResult;
            }

            if (availability.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID del doctor es requerido.";
                return operationResult;
            }

            if (availability.AvailableDate.Date < DateTime.Today)
            {
                operationResult.Success = false;
                operationResult.Message = "La fecha de disponibilidad debe ser hoy o en el futuro.";
                return operationResult;
            }

            if (availability.Startime >= availability.EndTime)
            {
                operationResult.Success = false;
                operationResult.Message = "La hora de inicio debe ser anterior a la hora de fin.";
                return operationResult;
            }

            try
            {
                DoctorAvailability? existingAvailability = await _healtSyncContext.DoctorAvailability.FindAsync(availability.AvailabilityID);

                if (existingAvailability == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró la disponibilidad.";
                    return operationResult;
                }


                existingAvailability.DoctorID = availability.DoctorID;
                existingAvailability.AvailableDate = availability.AvailableDate;
                existingAvailability.Startime = availability.Startime;
                existingAvailability.EndTime = availability.EndTime;

                
                return operationResult = await base.Save(existingAvailability);

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al actualizar la disponibilidad del doctor.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Remove(DoctorAvailability availability) 
        {
           OperationResult operationResult = new OperationResult();

            if(availability == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad disponibilidad es requerida.";
                return operationResult;
            }

            if(availability.AvailabilityID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID de la disponibilidad es requerido para eliminar.";
                return operationResult;
            }

            try
            {
                DoctorAvailability? availabilityToRemove = await _healtSyncContext.DoctorAvailability.FindAsync(availability.AvailabilityID);

                if (availabilityToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró la disponibilidad para eliminar.";
                    return operationResult;
                }

                return operationResult = await base.Remove(availabilityToRemove);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al eliminar la disponibilidad del doctor.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;

        }    

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();

            if (id < 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID de la disponibilidad es requerido.";
                return operationResult;
            }

            try
            {
                DoctorAvailability? availabilityToGetById = await _healtSyncContext.DoctorAvailability.FindAsync(id);

                if (availabilityToGetById == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró la disponibilidad con el ID proporcionado.";
                    return operationResult;
                }

                return operationResult.Data = availabilityToGetById;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al buscar la disponibilidad del doctor.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

    }
}
