using Discussion.DAL.Repository.UnitOfWork;

namespace Discussion.WebAPI.ProgramExtensions;

public static class DependencyInjectionSetup
{
    /// <summary>
    /// Dependency Injection Container.
    /// </summary>
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        // Register UnitOfWork for repositories.
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
