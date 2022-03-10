using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Models.SettingsModels;
using UniDubnaSchedule.Services.Abstractions;
using UniDubnaSchedule.Services.Implementations;

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    
    // Get configuration from appsettings.json
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", true, true)
        .AddUserSecrets<Program>()
        .Build();
    
    // Getting the connection string and configuring the database context.
    var dbConnectionSettings = configuration.GetSection("MsSql").Get<DbConnection>();
    services.AddDbContext<ApplicationDbContext>(
        o => o.UseSqlServer(
            dbConnectionSettings.ConnectionString
        )
    );
    
    // Setting up DI
    services.AddScoped<IScheduleRepository, ScheduleRepository>();
    services.AddScoped<IScheduleService, ScheduleService>();
}

void ConfigureApplication(WebApplication application)
{
    if (application.Environment.IsDevelopment())
    {
        application.UseSwagger();
        application.UseSwaggerUI();
    }

    application.UseHttpsRedirection();
    application.UseAuthorization();
    application.MapControllers();
}

// Create builder and configure services

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

// Create, configure and run WebApi application

var app = builder.Build();
ConfigureApplication(app);
app.Run();