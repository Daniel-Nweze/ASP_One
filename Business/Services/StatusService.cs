using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using static Business.Helpers.ServiceResultHelper;

namespace Business.Services;

public class StatusService(IBaseRepository<StatusEntity> baseRepository) : IStatusService
{
    private readonly IBaseRepository<StatusEntity> _baseRepository = baseRepository;

    public async Task<ServiceResult<IEnumerable<StatusEntity>>> GetAllStatusesAsync()
    {
        try
        {
            var statuses = await _baseRepository.GetAllAsync();
            return HandleRepoResult(statuses);
        }
        catch (Exception ex)
        {
            return ResultFailure<IEnumerable<StatusEntity>>(500, $"Oväntat fel: {ex.Message}");
        }
    }

    public async Task<ServiceResult<StatusEntity>> GetStatusByIdAsync(int id)
    {
        try
        {
            var result = await _baseRepository.GetAsync(x => x.Id == id);
            return HandleRepoResult(result);
        }
        catch (Exception ex)
        {
            return ResultFailure<StatusEntity>(500, $"Oväntat fel: {ex.Message}");
        }

    }

    public async Task<ServiceResult<StatusEntity>> GetStatusByNameAsync(string statusName)
    {
        try
        {
            var result = await _baseRepository.GetAsync(x => x.StatusName == statusName);
            return HandleRepoResult(result);
        }
        catch (Exception ex)
        {
            return ResultFailure<StatusEntity>(500, $"Oväntat fel: {ex.Message}");
        }

    }



}





