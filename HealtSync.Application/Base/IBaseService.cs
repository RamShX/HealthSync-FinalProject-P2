

namespace HealtSync.Application.Base
{
    public interface IBaseService <TResponse, TSaveDto, TUpdateDto>
    {
        Task<TResponse> SaveeAsync(TSaveDto dto);
        Task<TResponse> UpdateAsync(TUpdateDto dto);
        Task<TResponse> GetAll();
        Task<TResponse> GetById(int id);
    }
}
