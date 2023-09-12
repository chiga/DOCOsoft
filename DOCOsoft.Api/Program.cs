using DOCOsoft.Api.Data;
using DOCOsoft.Api.Endpoints;
using DOCOsoft.Api.OpenAPI;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
})
.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddSwaggerGen()
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
    .AddEndpointsApiExplorer();

var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapUsersEndpoints();

app.UserSwagger();

app.Run();