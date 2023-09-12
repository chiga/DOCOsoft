using DOCOsoft.Api.Entities;
using DOCOsoft.Api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;
using System.Net;
using System.Text.Json;

namespace DOCOsoft.Api.Test;
public class UnitTest1
//: IClassFixture<webApplicationFactory<Startup>>
{
    private IUsersRepository _usersRepository;

    public UnitTest1(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [Fact]
    public async void Get_OnSuccess()
    {
        // Arrange
        var id = 1;
        User user = new User
        {
            FirstName = "FirstName1",
            LastName = "FirstName1",
            Email = "FirstName1",
            Active = true,
            Birthday = DateTime.UtcNow,
        };

        _usersRepository.GetAsync(Arg.Is(id)).Returns(user);

        using var app = new UserEndpointTests(x =>
        {
            x.AddSingleton(_usersRepository);
        });

        var httpClient = app.CreateClient();

        var response = await httpClient.GetAsync($"/users/{id}");
        var responseText = await response.Content.ReadAsStringAsync();
        var userResult = JsonSerializer.Deserialize<User>(responseText);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        userResult.Should().BeEquivalentTo(user);
    }

    internal class UserEndpointTests : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _serviceOverride;

        public UserEndpointTests(Action<IServiceCollection> serviceOverride)
        {
            _serviceOverride = serviceOverride;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(_serviceOverride);
            return base.CreateHost(builder);
        }

    }


}