using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IClientService
    {
        Task<ServiceResult<IEnumerable<ClientEntity>>> GetAllClientsAsync();
    }
}