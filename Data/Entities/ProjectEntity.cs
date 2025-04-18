using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public string Id  { get; set; } = Guid.NewGuid().ToString();
    public string? Image { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? Description { get; set; }


    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }


    public decimal Budget { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; } = string.Empty;
    public virtual ClientEntity? Client { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;
    public virtual UserEntity? User { get; set; }

    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public virtual StatusEntity? Status { get; set; }

}
