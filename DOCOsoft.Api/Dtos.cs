using System.ComponentModel.DataAnnotations;

namespace DOCOsoft.Api.Dtos;

public record UserDto(
    int Id,
    string FirstName,
    string LastName,
    DateTime Birthday,
    string Email,
    bool Active
);

public record CreateUserDto(
    [Required][StringLength(50)] string FirstName,
    [Required][StringLength(50)] string LastName,
    DateTime Birthday,
    [EmailAddress] string Email,
    bool Active
);

public record UpdateUserDto(
    [Required][StringLength(50)] string FirstName,
    [Required][StringLength(50)] string LastName,
    DateTime Birthday,
    [EmailAddress] string Email,
    bool Active
);