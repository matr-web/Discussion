using Discussion.DAL;
using Microsoft.EntityFrameworkCore;

namespace Discussion.WebAPI.ProgramExtensions;

public static class DbContextSetup
{
    /// <summary>
    /// Configure Context class.
    /// </summary>
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Register and get Configuration for DiscussDbContext class.
        return services.AddDbContext<DiscussDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
    }
}
