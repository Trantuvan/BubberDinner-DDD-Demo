using BubberDinner.Api.Filters;
using BubberDinner.Application;
using BubberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers(opt => opt.Filters.Add<ErrorHandlingFilterAttribute>());
};

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseHsts();
    app.MapControllers();
    app.Run();
}

