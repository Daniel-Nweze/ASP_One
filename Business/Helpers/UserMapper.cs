using Business.Dtos;
using Data.Entities;

namespace Business.Helpers;

public static class UserMapper
{
    public static UserEntity ToEntity(this SignUpFormData formData)
    {
        return new UserEntity
        {
            UserName = formData.Email,
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            Email = formData.Email,
        };
    }
}
