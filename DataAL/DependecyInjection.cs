using DataAL.Repository;
using DataAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataAL
{
    public static class DependecyInjection
    {
        public static IServiceCollection ImplementPersistence(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped<IUserservce>(provider =>
            provider.GetService<Userservce>());
            services.AddScoped<IUserservce, Userservce>();

            services.AddScoped<ICountryservce>(provider =>
            provider.GetService<Countryservce>());
            services.AddScoped<ICountryservce, Countryservce>();
            return services;
        }
    }
}
