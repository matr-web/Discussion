namespace Discussion.WebAPI.ProgramExtensions;

/// <summary>
/// CORS Configuration.
/// </summary>
public static class CorsSetup
{
    public static IServiceCollection RegisterCors(this IServiceCollection services, WebApplicationBuilder _builder)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("FrontEndClient", builder =>
            builder.AllowAnyMethod() 
            .AllowAnyHeader()
            .WithOrigins(_builder.Configuration["AllowedOrigins"])); 
        });

        return services;
    }
}

