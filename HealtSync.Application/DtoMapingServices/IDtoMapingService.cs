

namespace HealtSync.Application.DtoMapingServices
{
    public interface IDtoMappingService <TEntity, TSaveDto, TUpdateDto, TGetDto> where TEntity : class
    {
        public TUpdateDto ConvertToUpdateDto(TEntity entity);
        public TSaveDto ConvertToSaveDto(TEntity entity);
        public TGetDto ConvertToGetDto(TEntity entity); 
    }
}
