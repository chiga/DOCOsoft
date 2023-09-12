namespace DOCOsoft.Api.OpenAPI;

public static class SwaggerExtensions
{
    public static IApplicationBuilder UserSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in app.DescribeApiVersions())
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToLowerInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }
        return app;
    }
}
