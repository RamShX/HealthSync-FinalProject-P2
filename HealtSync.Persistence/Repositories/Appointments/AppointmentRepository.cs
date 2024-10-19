
using HealtSync.Domain.Entities.Appointments;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Base;
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Appointments;
using Microsoft.Extensions.Logging;



namespace HealtSync.Persistence.Repositories.Appointments
{
    public sealed class AppointmentRepository(HealtSyncContext healtSyncContext, 
     ILogger<AppointmentRepository> logger) :BaseRepository<Appointment>(healtSyncContext), IAppointments
    {
        private readonly ILogger<AppointmentRepository> logger = logger;
        private readonly HealtSyncContext _healtSyncContext = healtSyncContext;

        public async override Task<OperationResult> Save(Appointment appointment) 
        {
            OperationResult operationResult = new OperationResult();

            if (appointment == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (appointment.PatientID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID del paciente es requerido.";
                return operationResult;
            }

            if (appointment.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID del doctor es requerido.";
                return operationResult;
            }



            if (appointment.AppointmentDate <= DateTime.Now)
            {
                operationResult.Success = false;
                operationResult.Message = "La fecha de la cita debe ser en el futuro.";
                return operationResult;
            }

            if (appointment.StatusID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El estado de la cita es requerido.";
                return operationResult;
            }

            //Establecer fecha de creación.
            appointment.CreatedAt = DateTime.Now;

            try
            {
                operationResult = await base.Save(appointment);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error a la hora de guardar la cita.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
        
        public async override Task<OperationResult> Update(Appointment appointment)
        {
            OperationResult operationResult = new OperationResult();

            if (appointment == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (appointment.PatientID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID del paciente es requerido.";
                return operationResult;
            }

            if (appointment.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID del doctor es requerido.";
                return operationResult;
            }



            if (appointment.AppointmentDate <= DateTime.Now)
            {
                operationResult.Success = false;
                operationResult.Message = "La fecha de la cita debe ser en el futuro.";
                return operationResult;
            }

            if (appointment.StatusID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El estado de la cita es requerido.";
                return operationResult;
            }

            //Establecer la fecha de la actualización.
            appointment.UpdatedAt = DateTime.Now;

            try
            {
                Appointment? appointmentToUpdate = await _healtSyncContext.Appointments.FindAsync(appointment.AppointmentID);

                if (appointmentToUpdate != null)
                {
                    appointmentToUpdate.PatientID = appointment.PatientID;
                    appointmentToUpdate.DoctorID = appointment.DoctorID;
                    appointmentToUpdate.AppointmentDate = appointment.AppointmentDate;
                    appointmentToUpdate.StatusID = appointment.StatusID;
                    appointmentToUpdate.UpdatedAt = DateTime.Now;

                    operationResult = await base.Update(appointmentToUpdate);
                }
            }
            catch (Exception ex) 
            {
                operationResult.Success = false;
                operationResult.Message = "Error actualizando la cita.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Appointment appointment)
        {

            OperationResult operationResult = new OperationResult();

            if (appointment == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (appointment.AppointmentID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El ID de la cita es requerido para eliminar.";
                return operationResult;
            }

            try
            {
                Appointment? appointmentToRemove = await _healtSyncContext.Appointments.FindAsync(appointment.AppointmentID);

                if (appointmentToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "La cita no se encuentra.";
                    return operationResult;
                    
                }

                return operationResult = await base.Remove(appointmentToRemove);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al eliminar la cita.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id) 
        {
            OperationResult operationResult = new OperationResult();

            if(id <= 0) 
            {
                operationResult.Success = false;
                operationResult.Message = "El ID de la cita es inválido.";
                return operationResult;
            }

            try
            {
                Appointment? appointmentGetById = await _healtSyncContext.Appointments.FindAsync(id);

                if (appointmentGetById == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró ninguna cita con el ID proporcionado.";
                    return operationResult;
                }

                operationResult.Success = true;
                operationResult.Data = appointmentGetById;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al buscar la cita.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

    }

}
