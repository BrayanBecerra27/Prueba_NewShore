
using Microsoft.EntityFrameworkCore;
using NEWSHORE_AIR_DATAACCESS.Context;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_DATAACCESS.Implementation;
using NEWSHORE_AIR_DATAACCESS.Repositories;
using System.Globalization;
using System.Text;
using Serilog;
using Serilog.Events;
using NEWSHORE_AIR_BUSINESS.Implementation;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

//Create Loggger and store in amazon S3
Log.Logger = new LoggerConfiguration()
    .WriteTo.AmazonS3(
        $"logBackend-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt",
        "newshore-air-logs",
        Amazon.RegionEndpoint.USEast1,
        builder.Configuration.GetSection("awsCredentials:awsAccessKeyId").Value,
        builder.Configuration.GetSection("awsCredentials:awsSecretAccessKey").Value,
        restrictedToMinimumLevel: LogEventLevel.Error,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        new CultureInfo("es-CO"),
        levelSwitch: null,
        rollingInterval: Serilog.Sinks.AmazonS3.RollingInterval.Day,
        encoding: Encoding.Unicode)
    .CreateLogger();
// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injection services
builder.Services.AddScoped<IQueryRoute, QueryRoute>();
builder.Services.AddScoped<IFlightService, FlightService>();

//injection repository
builder.Services.AddDbContext<NewShoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IJourneyRepository, JourneyRepository>();
builder.Services.AddScoped<ITransportRepository, TransportRepository>();
builder.Services.AddScoped<ICalculateRouteInteractor, CalculateRouteInteractor>();
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
