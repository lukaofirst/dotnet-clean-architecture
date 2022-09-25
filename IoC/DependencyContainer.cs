using Application.Interfaces.Services;
using Application.Mappings;
using Application.Services;
using Core.Interfaces.Repositories;
using Infraestructure.Data;
using Infraestructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public class DependencyContainer
    {
        public static void InjectServices(IServiceCollection services)
        {
            // Database Context
            services.AddSingleton<MongoDBContext>();

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Services
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
