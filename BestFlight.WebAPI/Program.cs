using BestFlight.Infrastructure.IoC;
using BestFlight.Application.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Host.ConfigureServices((hostContext, services) =>
{
    var config = hostContext.Configuration;
    services
        .AddSwagger()
        .AddDbContext(config)
        .AddRepositories()
        .AddServices()
        .AddAutoMapper();
    //.AddHealthChecks();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RunMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
