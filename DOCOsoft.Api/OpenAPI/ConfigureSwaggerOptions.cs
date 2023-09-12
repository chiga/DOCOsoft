using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DOCOsoft.Api.OpenAPI;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var descrition in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                descrition.GroupName,
                new OpenApiInfo()
                {
                    Title = $"DOCOsoft API {descrition.ApiVersion}",
                    Version = descrition.ApiVersion.ToString(),
                    Description = "Manages the Users"
                }
            );
        }
    }
}
