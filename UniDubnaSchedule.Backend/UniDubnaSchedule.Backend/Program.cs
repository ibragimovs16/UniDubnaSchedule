using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using UniDubnaSchedule.DAL;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Models.SettingsModels;
using UniDubnaSchedule.Services.Abstractions;
using UniDubnaSchedule.Services.Implementations;
using UniDubnaSchedule.Services.Implementations.AuthServices;

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    
    // Swagger configuration for working with the token.
    services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard auth",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
        });
    
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });
    
    // Get configuration from appsettings.json
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", true, true)
        .AddUserSecrets<Program>()
        .Build();
    
    // Getting the connection string and configuring the database context.
    var dbConnectionSettings = configuration.GetSection("MsSql").Get<DbConnectionModel>();
    services.AddDbContext<ApplicationDbContext>(
        o => o.UseSqlServer(
            dbConnectionSettings.ConnectionString
        )
    );
    
    // Authentication configuration.
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var securityKey = configuration.GetSection("JWT:SecurityKey").Value;

            if (securityKey is null)
                throw new Exception("Security key is not configured in appsettings.json");
            
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                LifetimeValidator = (before, expires, token, parameters) =>
                    expires is not null && expires > DateTime.UtcNow
            };
        });
    
    // Setting up DI
    services.AddScoped<IScheduleRepository, ScheduleRepository>();
    services.AddScoped<IScheduleService, ScheduleService>();
    services.AddScoped<IBellsScheduleRepository, BellsScheduleRepository>();
    services.AddScoped<IBellsScheduleService, BellsScheduleService>();
    services.AddScoped<ISubjectsRepository, SubjectsRepository>();
    services.AddScoped<ISubjectsService, SubjectsService>();
    services.AddScoped<ITeachersRepository, TeacherRepository>();
    services.AddScoped<ITeacherService, TeacherService>();
    services.AddScoped<IUsersRepository, UsersRepository>();
    services.AddScoped<IUsersService, UsersService>();
    services.AddScoped<IAuthService, AuthService>();
}

void ConfigureApplication(WebApplication application)
{
    if (application.Environment.IsDevelopment())
    {
        application.UseSwagger();
        application.UseSwaggerUI();
    }

    // application.UseHttpsRedirection();
    application.UseAuthentication();
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