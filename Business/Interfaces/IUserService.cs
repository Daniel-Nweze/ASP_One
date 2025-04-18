using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<UserEntity>> CreateUserAsync(SignUpFormData formData);
        Task<ServiceResult<IEnumerable<UserEntity>>> GetAllUsersAsync();


    }
}