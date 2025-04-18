using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
    public class AddProjectFormData
    {
        public string? Image { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string ClientId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int StatusId { get; set; }
    }
}