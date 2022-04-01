using H5_Svendeprove_Web_API.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;

TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var connection_string = builder.Configuration.GetConnectionString("Customer_Context");
builder.Services.AddDbContext<Customer_Context>(x => x.UseNpgsql(connection_string));

builder.Services.AddCors(options =>  // LTPE Enable Cors
{
    options.AddPolicy("idk",
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true);
    });
});

//builder.WebHost.UseUrls("http://localhost:5002");

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenLocalhost(5002);
//});

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
