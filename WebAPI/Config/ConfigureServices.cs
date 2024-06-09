using Application.Services.Implementation;
using Application.Services.Interfaces;
using Infrastructure.Repository.Implementation;
using Infrastructure.Repository.Interfaces;

namespace WebAPI.Config
{
    public static class ConfigureServices
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
 
            services.AddTransient<IFlightRepository, FlightRepository>();

            services.AddScoped<IFlightService, FlightService>();



        }
    }
}
