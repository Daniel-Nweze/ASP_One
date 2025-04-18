using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ServiceResult<ProjectEntity>> CreateProjectAsync(AddProjectFormData formData);
        Task<ServiceResult<ProjectEntity>> DeleteProjectAsync(ProjectEntity entity);
        Task<ServiceResult<IEnumerable<ProjectEntity>>> GetAllProjectsAsync();
        Task<ServiceResult<ProjectEntity>> GetProjectAsync(string id);
        Task<ServiceResult<ProjectEntity>> UpdateProjectAsync(ProjectEntity entity);
        
    }
}