using Infrastructure.Data;
using Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Controllers 
builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketService API v1");
    options.RoutePrefix = string.Empty; // hace que Swagger esté en la raíz "/"
});

// Middlewares
app.UseHttpsRedirection();
app.UseAuthorization();

// Rutas 
app.MapControllers();

// Ejecutar 
app.Run();
