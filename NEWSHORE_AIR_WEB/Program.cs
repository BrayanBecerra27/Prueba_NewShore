
using Microsoft.EntityFrameworkCore;
using NEWSHORE_AIR_BUSINESS.Context;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_DATAACCESS.Implementation;
using NEWSHORE_AIR_DATAACCESS.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
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
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
