using HealtSync.Application.Base;
using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Application.Response.Users.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.Contracts.Users
{
    public interface IDoctorsService : IBaseService<DoctorResponse, DoctorSaveDto, DoctorUpdateDto>
    {
        Task<DoctorResponse> GetByAvailabilityDate(DateTime? availabilityDate);
    }
}
