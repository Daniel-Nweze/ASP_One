using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class UserEntity : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; } 
    [PersonalData]
    public string? LastName { get; set; } 

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
