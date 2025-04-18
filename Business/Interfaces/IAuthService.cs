using Business.Dtos;
using Business.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<SignInResult>> SignInAsync(SignInFormData formdata);
        Task<ServiceResult<bool>> SignOutAsync();
        Task<ServiceResult<bool>> SignUpAsync(SignUpFormData formData);
    }
}