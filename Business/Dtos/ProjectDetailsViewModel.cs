namespace Business.Dtos;

public class ProjectDetailsViewModel
{
    public string Id { get; set; } = string.Empty;
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }

    public string? Image {  get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; } 

    public string StatusName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
