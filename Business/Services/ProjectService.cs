using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Business.Helpers.ServiceResultHelper;

namespace Business.Services;

public class ProjectService(IBaseRepository<ProjectEntity> baseRepository, IStatusService statusService, ILogger<ProjectService> logger, AppDbContext context) : IProjectService
{
    private readonly IBaseRepository<ProjectEntity> _baseRepository = baseRepository;
    private readonly IStatusService _statusService = statusService;
    private readonly ILogger<ProjectService> _logger = logger;
    private readonly AppDbContext _context = context;

    public async Task<ServiceResult<ProjectEntity>> CreateProjectAsync(AddProjectFormData formData)
    {
        try
        {
            if (formData == null)
            {
                _logger.LogWarning("FormData var null i CreateProjectAsync.");
                return ResultFailure<ProjectEntity>(500, "Formuläret var tomt. Kontrollera att alla fält är ifyllda.");
            }

            var statusResult = await _statusService.GetStatusByNameAsync("Not Started");
            if (!statusResult.Succeeded || statusResult.Data == null)
                return ResultFailure<ProjectEntity>(404, "Status 'Not Started' kunde inte hämtas.");

            var entity = formData.ToEntity(statusResult.Data.Id);
            var result = await _baseRepository.AddAsync(entity);

            return HandleRepoResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateProjectAsync misslyckades.");
            return ResultFailure<ProjectEntity>(500, "Ett oväntat fel inträffade. Försök igen senare.");
        }
    }

    public async Task<ServiceResult<IEnumerable<ProjectEntity>>> GetAllProjectsAsync()
    {
        try
        {
            var projects = await _context.Projects
                .Include(p => p.Status)
                .Include(p => p.Client)
                .ToListAsync();

            return ResultSuccess(200, projects.AsEnumerable());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAllProjectsAsync misslyckades.");
            return ResultFailure<IEnumerable<ProjectEntity>>(500, "Ett oväntat fel inträffade. Försök igen senare.");
        }
    }

    public async Task<ServiceResult<ProjectEntity>> GetProjectAsync(string id)
    {
        try
        {
            var project = await _baseRepository.GetAsync(p => p.Id == id);
            return HandleRepoResult(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetProjectAsync misslyckades för projekt med ID {Id}", id);
            return ResultFailure<ProjectEntity>(500, "Ett oväntat fel inträffade. Försök igen senare.");
        }
    }

    public async Task<ServiceResult<ProjectEntity>> UpdateProjectAsync(ProjectEntity entity)
    {
        try
        {
            var existingProject = await _baseRepository.GetAsync(p => p.Id == entity.Id);
            if (!existingProject.Succeeded || existingProject.Data == null)
                return ResultFailure<ProjectEntity>(404, "Projektet hittades inte.");

            var updatedProject = existingProject.Data!;
            updatedProject.ProjectName = entity.ProjectName;
            updatedProject.Description = entity.Description;
            updatedProject.StartDate = entity.StartDate;
            updatedProject.EndDate = entity.EndDate;
            updatedProject.Budget = entity.Budget;
            updatedProject.ClientId = entity.ClientId;
            updatedProject.StatusId = entity.StatusId;

            var result = await _baseRepository.UpdateAsync(updatedProject);
            return HandleRepoResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateProjectAsync misslyckades för projekt med ID {Id}", entity.Id);
            return ResultFailure<ProjectEntity>(500, "Ett oväntat fel inträffade. Försök igen senare.");
        }
    }

    public async Task<ServiceResult<ProjectEntity>> DeleteProjectAsync(ProjectEntity entity)
    {
        try
        {
            var result = await _baseRepository.DeleteAsync(entity);
            return HandleRepoResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteProjectAsync misslyckades för projekt med ID {Id}", entity.Id);
            return ResultFailure<ProjectEntity>(500, "Ett oväntat fel inträffade. Försök igen senare.");
        }
    }
}
