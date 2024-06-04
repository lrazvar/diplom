// See https://aka.ms/new-console-template for more information

using Grpc.Core;
using Grpc.Net.Client;
using DataBaseService.Db;
using DataBaseService.Db.Repository;
using DataBaseService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// var configuration = new ConfigurationBuilder()
//     .AddJsonFile("C:\\Users\\lrazv\\unityProjects\\diplomchik\\DataBaseService\\appsettings.json", optional: false, reloadOnChange: true)
//     .Build();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
// builder.Services.AddDbContext<GameDbContext>(options =>
//     options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddScoped<UserRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();
app.MapGrpcService<GameService>();

app.Run();