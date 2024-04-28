using Cell.Application.Mapping.AnnouncementMapping;
using Cell.Application.Mapping.CommentMapping;
using Cell.Application.Mapping.UserMapping;
using Cell.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trashcan.Application.Services;

namespace Cell.Application;

/// <summary>
/// Внедрение зависимостей.
/// </summary>
public static class DependensyInjection
{
    /// <summary>
    /// Создание приложения.
    /// </summary>
    /// <param name="services"> Список сервисов. </param>
    /// <param name="configuration"> Конфигурация. </param>
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMappers();
        services.InitServices();
    }

    /// <summary>
    /// Добавление автомаперов.
    /// </summary>
    /// <param name="services"> Сервисы. </param>
    private static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AnnouncementMapping));
        services.AddAutoMapper(typeof(CommentMapping));
        services.AddAutoMapper(typeof(CommentDtoMapping));
        services.AddAutoMapper(typeof(CreateCommentMapping));
        services.AddAutoMapper(typeof(UserDtoMapping));
        services.AddAutoMapper(typeof(RegisterUserMapping));
        services.AddAutoMapper(typeof(UserMapping));
    }

    /// <summary>
    /// Инициализация сервисов.
    /// </summary>
    /// <param name="services"> Сервисы. </param>
    private static void InitServices(this IServiceCollection services)
    {
        services.AddTransient<IAnnouncementService, AnnouncementService>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<IUserService, UserService>();
    }
}