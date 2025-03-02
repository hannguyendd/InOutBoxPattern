using OutboxService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Shared.Middlewares;
using OutboxService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<OutboxDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllers();

builder.Services.AddCap(option =>
{
    option.UseEntityFramework<OutboxDbContext>();

    option.UseKafka("localhost:9092");

    option.UseDashboard();
});

builder.Services.AddTransient<PatientEventConsumer>();
builder.Services.AddScoped<PatientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

