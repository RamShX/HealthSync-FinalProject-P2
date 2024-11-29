

using HealtSync.Application.Contracts.Appoiments;
using HealtSync.Application.Dtos.Appoiments.Appoiments;
using HealtSync.Application.Response.Appoiments;
using HealtSync.Persistence.Interfaces.Appointments;

namespace HealtSync.Application.Services.Appoiments
{
    public class AppoimentsService : IAppoimentsService
    {
        IAppoimentsRepository _appoimentRepository;

        public AppoimentsService(IAppoimentsRepository appoimentsRepository)
        {
            _appoimentRepository = appoimentsRepository;
        }
        public Task<AppoimentResponse> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppoimentResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AppoimentResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppoimentResponse> SaveAsync(AppoimentSaveDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<AppoimentResponse> UpdateAsync(AppoimentUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
