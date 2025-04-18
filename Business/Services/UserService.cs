
using Business.Dtos;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using static Business.Helpers.ServiceResultHelper;

namespace Business.Services;

public class UserService(IBaseRepository<UserEntity> baseRepository, UserManager<UserEntity> userManager, ILogger<UserService> logger) : IUserService
{
    private readonly IBaseRepository<UserEntity> _baseRepository = baseRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ILogger<UserService> _logger = logger;


    public async Task<ServiceResult<IEnumerable<UserEntity>>> GetAllUsersAsync()
    {
        try
        {
            var users = await _baseRepository.GetAllAsync();
            return HandleRepoResult(users);
        }
        catch (Exception ex)
        {
            return ResultFailure<IEnumerable<UserEntity>>(500, $"Oväntat fel {ex.Message}");
        }
    }

    public async Task<ServiceResult<UserEntity>> CreateUserAsync(SignUpFormData formData)
    {
        try
        {
            if (formData == null)
                return ResultFailure<UserEntity>(400, "Formuläret var tomt. Kontrollera att alla fält är ifyllda.");

            var exists = await _baseRepository.ExistsAsync(x => x.Email == formData.Email);
            if (exists.Data)
                return ResultFailure<UserEntity>(400, "E-postadressen är redan registrerad.");

            var userEntity = formData.ToEntity();
            var result = await _userManager.CreateAsync(userEntity, formData.Password);

            if (!result.Succeeded)
            {
                var identityErrors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ResultFailure<UserEntity>(400, $"Fel vid skapande: {identityErrors}");
            }
                

            return ResultSuccess(201, userEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateUserAsync misslyckades");
            return ResultFailure<UserEntity>(500, "Ett oväntat fel inträffade.");
        }
    }
}