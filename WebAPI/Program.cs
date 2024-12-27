using Infraestructure.Settings;
using IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding the SettingConfig class
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(MongoDBSettings.BindName));

builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

DependencyContainer.AddServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
