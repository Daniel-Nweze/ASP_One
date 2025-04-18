using static Business.Helpers.ServiceResultHelper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ClientService(IBaseRepository<ClientEntity> baseRepository) : IClientService
{
    private readonly IBaseRepository<ClientEntity> _baseRepository = baseRepository;

    public async Task<ServiceResult<IEnumerable<ClientEntity>>> GetAllClientsAsync()
    {
        try
        {
            var result = await _baseRepository.GetAllAsync();
            return HandleRepoResult(result);
        }
        catch (Exception ex)
        {
            return ResultFailure<IEnumerable<ClientEntity>>(500, $"Oväntat fel: {ex.Message}");
        }
    }
}



    

