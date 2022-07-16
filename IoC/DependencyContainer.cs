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
            services.AddSingleton<MongoDBContext>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
