

namespace HealtSync.Application.DtoMapingServices
{
    public interface IDtoMappingService <TEntity, TSaveDto, TUpdateDto, TGetDto> 
    {
        public TEntity ConvertUpdateDtoToEntity(TUpdateDto updateDto);
        public TEntity ConvertSaveDtoToEntity(TSaveDto saveDto);
        public TGetDto ConvertEntityToGetDto(TEntity entity); 
    }
}
