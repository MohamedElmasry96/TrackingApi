using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderTrackingApi.Data;
using OrderTrackingApi.Interfaces;
using OrderTrackingApi.Services;
using TrackingApi.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<TrackingDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddControllers();
// Add Services
builder.Services.AddScoped<ITrackingQueryService, TrackingQueryService>();
builder.Services.AddScoped<ITrackingCommandService, TrackingCommandService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
