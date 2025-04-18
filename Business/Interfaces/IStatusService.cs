using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<ServiceResult<IEnumerable<StatusEntity>>> GetAllStatusesAsync();
        Task<ServiceResult<StatusEntity>> GetStatusByIdAsync(int id);
        Task<ServiceResult<StatusEntity>> GetStatusByNameAsync(string statusName);

    }
}