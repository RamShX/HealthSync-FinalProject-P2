using HealtSync.Application.Base;
using HealtSync.Application.Dtos.Users.Persons;
using HealtSync.Application.Response.Users.Users;
using HealtSync.Persistence.Interfaces.Users;
using HealtSync.Persistence.Repositories.Users;
using Microsoft.Extensions.Logging;


namespace HealtSync.Application.Services.Users
{
    public class UsersService : IBaseService<UsersResponse, UsersSaveDto, UsersUpdateDto>
    {

        private readonly IUsersRepository _userRepository;
        private readonly ILogger<UsersService> _logger;
        public UsersService(UsersRepository rutaRepository)
        { 
            if(rutaRepository is null)
            {
                throw new ArgumentNullException(nameof(rutaRepository));
            }
            _userRepository = rutaRepository;
        }   
        public async Task<UsersResponse> GetAll()
        {
            UsersResponse userResponse = new();

            try
            {
                var result = await _userRepository.GetAll();

                userResponse.IsSuccess = result.Success;
                userResponse.Model = result.Data;
               
            }
            catch (Exception ex)
            {
                userResponse.IsSuccess = false;
                userResponse.Message = "Error obteniendo los usuarios";
                _logger.LogError(userResponse.Message, ex);

            }

            return userResponse;

        }

        public async Task<UsersResponse> GetById(int id)
        {
            UsersResponse userResponse = new();

            try
            {
                var result = await _userRepository.GetEntityBy(id);

                userResponse.IsSuccess = result.Success;
                userResponse.Model = result.Data;

            }
            catch (Exception ex)
            {
                userResponse.IsSuccess = false;
                userResponse.Message = "Error obteniendo el usuario";
                _logger.LogError(userResponse.Message, ex);

            }

            return userResponse;

        }

        public async Task<UsersResponse> SaveeAsync(UsersSaveDto dto)
        {
            UsersResponse userResponse = new();

            try
            {
                var result = await _userRepository.Save(dto);

                userResponse.IsSuccess = result.Success;
                userResponse.Model = result.Data;

            }
            catch (Exception ex)
            {
                userResponse.IsSuccess = false;
                userResponse.Message = "Error guardando el usuario";
                _logger.LogError(userResponse.Message, ex);

            }

            return userResponse;

        }

        Task<UsersResponse> IBaseService<UsersResponse, UsersSaveDto, UsersUpdateDto>.UpdateAsync(UsersUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
