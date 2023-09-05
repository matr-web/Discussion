using Discussion.BLL.Services;
using Discussion.BLL.Services.Interfaces;
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

        // Register Services.
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
