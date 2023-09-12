using DOCOsoft.Api.Dtos;
using DOCOsoft.Api.Entities;
using DOCOsoft.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DOCOsoft.Api.Endpoints;

public static class UsersEndpoints
{
    const string GetUserEndpointName = "GetUser";

    public static RouteGroupBuilder MapUsersEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
                .NewVersionedApi()
                .MapGroup("/users")
                .HasApiVersion(1.0)
                .WithParameterValidation()
                .WithOpenApi()
                .WithTags("Users");

        group.MapGet("/", async (IUsersRepository repository) =>
            (await repository.GetAllAsync()).Select(user => user.AsDto())).
            MapToApiVersion(1.0)
            .WithSummary("Gets all Users")
            .WithDescription("Gets all available Users");

        group.MapGet("/{id}", async Task<Results<Ok<UserDto>, NotFound>> (IUsersRepository repository, int id) =>
        {
            User? user = await repository.GetAsync(id);
            return user is not null ? TypedResults.Ok(user.AsDto()) : TypedResults.NotFound();
        })
        .WithName(GetUserEndpointName)
        .WithSummary("Gets a user by Id")
        .WithDescription("Gets the user that has the specified Id");

        group.MapPost("/", async Task<CreatedAtRoute<UserDto>> (IUsersRepository repository, CreateUserDto userDto) =>
        {
            User user = new()
            {
                FirstName = userDto.LastName,
                LastName = userDto.FirstName,
                Email = userDto.Email,
                Birthday = userDto.Birthday,
                Active = userDto.Active
            };

            await repository.CreateAsync(user);
            return TypedResults.CreatedAtRoute(user.AsDto(), GetUserEndpointName, new { id = user.Id });
        })
        .WithSummary("Creates a new user")
        .WithDescription("Creates a new game with the specified properties");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (IUsersRepository repository, int id, UpdateUserDto updatedUserDto) =>
        {
            User? existingUser = await repository.GetAsync(id);

            if (existingUser is null)
            {
                return TypedResults.NotFound();
            }

            existingUser.FirstName = updatedUserDto.FirstName;
            existingUser.LastName = updatedUserDto.LastName;
            existingUser.Birthday = updatedUserDto.Birthday;
            existingUser.Active = updatedUserDto.Active;

            await repository.UpdateAsync(existingUser);

            return TypedResults.NoContent();
        })
        .WithSummary("Updates a user")
        .WithDescription("Updates all user with the specified properties");

        group.MapDelete("/{id}", async (IUsersRepository repository, int id) =>
        {
            User? user = await repository.GetAsync(id);

            if (user is not null)
            {
                await repository.DeleteAsync(id);
            }

            return TypedResults.NoContent();
        })
        .WithSummary("Deletes a user")
        .WithDescription("Deletes the user that has the specified id");

        return group;
    }
}
