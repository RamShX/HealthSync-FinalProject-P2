using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Persistence.Interfaces.Users
{
    public interface IPatientsRepository : IBaseRepository<Patients>
    {
    }
}
