using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

[Index(nameof(StatusName), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int Id { get; set; }
    public string StatusName { get; set; } = string.Empty;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}