using Application.Interfaces.Services;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces.Repositories;
using Infraestructure.Persistence.MongoDB.Context;
using Infraestructure.Persistence.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DependencyContainer
{
	public static void AddServices(this IServiceCollection services)
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
