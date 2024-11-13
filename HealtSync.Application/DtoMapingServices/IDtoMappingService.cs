

namespace HealtSync.Application.DtoMapingServices
{
    public interface IDtoMapingService <TEntity, TSaveDto, TUpdateDto, TGetDto> 
    {
        public TEntity UpdateDtoToEntity(TUpdateDto updateDto);
        public TEntity SaveDtoToEntity(TSaveDto saveDto);
        public TGetDto EntityToGetDto(TEntity entity); 
    }
}
