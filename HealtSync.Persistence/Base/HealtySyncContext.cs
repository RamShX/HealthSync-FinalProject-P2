using HealtSync.Domain.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HealtSync.Persistence.Base
{
    public partial class HealtySyncContext : DbContext
    {
        public HealtySyncContext(DbContextOptions<HealtySyncContext> option) : base(option)
        {

            #region *Appointments Entities*
            public DbSet<Appointments> Appointments { get; set; }
            public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
            #endregion

            #region *Insurance Entities*

            #endregion

            #region *Medical Entities*

            #endregion

            #region *System Entities*

            #endregion

            #region *Users Entities*

            #endregion

        }
    }
}
