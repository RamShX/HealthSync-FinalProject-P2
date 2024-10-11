
using HealtSync.Domain.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Entities.System;
using HealtSync.Domain.Entities.Medical;
using HealtSync.Domain.Entities.Insurance;

namespace HealtSync.Persistence.Context
{
    internal class HealtSyncContext : DbContext
    {
        public HealtSyncContext(DbContextOptions<HealtSyncContext> options) : base(options)
        { 

        }

        #region *Appointments Entities*
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        #endregion

        #region *Insurance Entities* 
        public DbSet<InsuranceProviders> InsuranceProviders { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }
        #endregion

        #region *Medical Entities*
        public DbSet<AvailabilityModes> AvailabilityModes { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Specialties> Specialties { get; set; }

        #endregion

        #region *System Entities*
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        #endregion

        #region *Users Entities*
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Persons> Persons { get; set; }
        #endregion

    }
}
