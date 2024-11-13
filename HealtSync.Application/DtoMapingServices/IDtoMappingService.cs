

using HealtSync.Application.Dtos.Users.Doctors;

namespace HealtSync.Application.DtoMapingServices
{
    public interface IDtoMappingService <TEntity, TSaveDto, TUpdateDto, TGetSimpleDto, TGetDetailDto> 
    {
        public TEntity ConvertUpdateDtoToEntity(TUpdateDto updateDto);
        public TEntity ConvertSaveDtoToEntity(TSaveDto saveDto);
        public TGetSimpleDto ConvertEntityToGetSimpletDto(TEntity entity);
        public TGetDetailDto ConvertEntityToGetDetailedDto(TEntity entity);
    }
}
