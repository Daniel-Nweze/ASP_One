using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

[Index(nameof(ClientName), IsUnique = true)]
public class ClientEntity
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
