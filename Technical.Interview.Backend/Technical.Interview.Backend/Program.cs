using Microsoft.EntityFrameworkCore;

using Technical.Interview.Backend.Extensions;

var Builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Builder.Services.AddControllers();
Builder.Services.AddExtensions(Builder.Configuration);

//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
Builder.Services.AddEndpointsApiExplorer();
Builder.Services.AddSwaggerGen();

var Application = Builder.Build();

// Configure the HTTP request pipeline.
Application.UseSwagger(static Options => Options.SerializeAsV2 = true);
Application.UseSwaggerUI(static Options =>
{
    Options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    Options.RoutePrefix = string.Empty;
});

Application.UseHttpsRedirection();
Application.MapControllers();
Application.MapSwagger();

using var Scope = Application.Services.CreateScope();
var InterviewContext = Scope.ServiceProvider.GetRequiredService<Technical.Interview.Backend.Data.InterviewContext>();
var Logger = Scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    await InterviewContext.Database.EnsureCreatedAsync();
    await InterviewContext.Database.MigrateAsync();
}
catch (Exception Ex)
{
    Logger.LogError(Ex, "An error occured during migration");
}

Application.Run();
