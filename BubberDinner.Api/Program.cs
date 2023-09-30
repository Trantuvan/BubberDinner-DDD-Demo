using BubberDinner.Api.Middleware;
using BubberDinner.Application;
using BubberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
};

var app = builder.Build();
{
    //* Global Error Handling middleware should be at 1st of the pipeline
    //* to catch all exceptions
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseHsts();
    app.MapControllers();
    app.Run();
}

