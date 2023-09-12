using DOCOsoft.Api.Dtos;

namespace DOCOsoft.Api.Entities;

public static class EntityExtensios
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Birthday,
            user.Email,
            user.Active
            );
    }
}
