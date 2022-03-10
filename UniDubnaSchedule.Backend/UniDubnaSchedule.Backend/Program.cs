void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
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