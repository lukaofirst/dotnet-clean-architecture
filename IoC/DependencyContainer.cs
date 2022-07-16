using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public class DependencyContainer
    {
        public static void InjectServices(IServiceCollection services)
        {
            // Database Context
            services.AddSingleton<MongoDBContext>();

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Services
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
