using Infrastructure.Repository.Implementation;
using Infrastructure.Repository.Interfaces;

namespace WebAPI.Config
{
    public static class ConfigureServices
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Capa de Infraestructura
            services.AddTransient<IFlightRepository, FlightRepository>();
           


            // Capa de Aplicación
            //services.AddScoped<IFlightService, FlightService>();



        }
    }
}
