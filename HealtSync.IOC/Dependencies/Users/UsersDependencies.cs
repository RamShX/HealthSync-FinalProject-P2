
using HealtSync.Application.Contracts.Users;
using HealtSync.Persistence.Interfaces.Users;
using HealtSync.Persistence.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;
using HealtSync.Application.Services.Users;

namespace HealtSync.IOC.Dependencies.Users
{
    public static class UsersDependencies
    {
        public static void AddUserDependency(this IServiceCollection service)
        {
            service.AddScoped<IDoctorsRepository, DoctorsRepository>();
            service.AddScoped<IPersonsRepository, PersonsRepository>();
            service.AddScoped<IUsersRepository, UsersRepository>();
            service.AddScoped<IPatientsRepository, PatientsRepository>();
            service.AddScoped<IEmployeesRepository, EmployeesRepository>();

            service.AddTransient<IEmployeesService, EmployeesService>();
        }
    }
}
