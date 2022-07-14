using DataAL.Repository;
using DataAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.BLL.Models.Countries.CountryServices;
using UserManagement.BLL.Models.Countries.ICountryServices;
using UserManagement.BLL.Models.Users;

namespace UserManagement.BLL
{
    public static class Injector
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICountryService, CountryService>();

        }
    }
}
