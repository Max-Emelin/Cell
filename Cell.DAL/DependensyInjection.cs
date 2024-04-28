using Cell.Domain.Entities;
using Cell.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cell.DAL
{
    public static class DependensyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection? services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient);
            services.InitRepositories();
        }

        public static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<Announcement>, BaseRepository<Announcement>>();
            services.AddScoped<IBaseRepository<Comment>, BaseRepository<Comment>>();
            services.AddScoped<IBaseRepository<Image>, BaseRepository<Image>>();
        }
    }
}
