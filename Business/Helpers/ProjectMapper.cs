using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Helpers
{
    public static class ProjectMapper
    {
        public static ProjectEntity ToEntity(this AddProjectFormData formData, int statusId)
        {
            return new ProjectEntity
            {
                Image = formData.Image,
                ProjectName = formData.ProjectName,
                Description = formData.Description,
                StartDate = formData.StartDate,
                EndDate = formData.EndDate,
                Budget = formData.Budget,
                CreatedAt = DateTime.Now,
                ClientId = formData.ClientId,
                UserId = formData.UserId,
                StatusId = formData.StatusId
            };
        }

        public static ProjectDetailsViewModel ToDetailsViewModel(this ProjectEntity project)
        {
            return new ProjectDetailsViewModel
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                Image = project.Image,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Budget = project.Budget,
                StatusName = project.Status?.StatusName ?? "Okänd Status",
                ClientName = project.Client?.ClientName ?? "Okänd Klient",
                CreatedAt = project.CreatedAt,

            };
        }
    }
}
