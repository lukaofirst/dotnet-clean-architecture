using Infraestructure.Settings;
using IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices();

builder.Services.AddOpenApi();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding the SettingConfig class
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(MongoDBSettings.BindName));

builder.Services
	.AddMvc()
	.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/openapi/v1.json", "WebAPI v1");
		options.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
