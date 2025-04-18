using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using static Business.Helpers.ServiceResultHelper;

namespace Business.Services;

public class AuthService(IUserService userService, SignInManager<UserEntity> signInManager, ILogger<AuthService> logger) : IAuthService
{
    private readonly IUserService _userService = userService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly ILogger<AuthService> _logger = logger;



    public async Task<ServiceResult<SignInResult>> SignInAsync(SignInFormData formdata)
    {

        if (formdata == null)
            return ResultFailure<SignInResult>(400, "Formuläret var tomt. Kontrollera att alla fält är ifyllda.");

        try
        {
            var result = await _signInManager.PasswordSignInAsync(
                formdata.Email,
                formdata.Password,
                formdata.IsPersistent,
                lockoutOnFailure: false
             );

            if (!result.Succeeded)
                return ResultFailure<SignInResult>(401, "Inloggningen misslyckades.");

            return ResultSuccess(200, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignInAsync misslyckades.");
            return ResultFailure<SignInResult>(500, "Något gick fel vid inloggning.");

        }
    }

    public async Task<ServiceResult<bool>> SignUpAsync(SignUpFormData formData)
    {
        if (formData == null)
            return ResultFailure<bool>(400, "Formuläret var tomt. Kontrollera att alla fält är ifyllda.");

        try
        {
            var result = await _userService.CreateUserAsync(formData);
            if (!result.Succeeded)
                return ResultFailure<bool>(400, "Användare kunde inte skapas.");

            return ResultSuccess(201, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignUpAsync misslyckades.");
            return ResultFailure<bool>(500, "Något gick fel vid registrering.");
        }
    }

    public async Task<ServiceResult<bool>> SignOutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return ResultSuccess(200, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignOutAsync misslyckades.");
            return ResultFailure<bool>(500, "Något gick fel vid utloggning.");
        }
    }
}
