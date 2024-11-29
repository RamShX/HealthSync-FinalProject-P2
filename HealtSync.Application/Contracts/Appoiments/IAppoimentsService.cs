using HealtSync.Application.Base;
using HealtSync.Application.Dtos.Appoiments.Appoiments;
using HealtSync.Application.Response.Appoiments;

namespace HealtSync.Application.Contracts.Appoiments
{
    public interface IAppoimentsService : IBaseService<AppoimentResponse, AppoimentSaveDto, AppoimentUpdateDto>
    {

    }
}
