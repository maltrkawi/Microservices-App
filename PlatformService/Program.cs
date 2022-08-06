using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// My endpoints using minimal APIs
//app.MapGet("/api/platforms", (IPlatformRepo repo, IMapper mapper) =>
//{
//    Console.WriteLine("--> Getting platforms...");
//    var platforms = repo.GetAllPlatforms();
//    return mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
//});

//app.MapGet("/api/platforms/{id}", (int id, IPlatformRepo repo, IMapper mapper) =>
//{
//    Console.WriteLine("--> Getting platform by Id...");
//    var platform = repo.GetPlatformById(id);
//    return mapper.Map<IEnumerable<PlatformReadDto>>(platform);
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

SeedDb.SeedPopulation(app);

app.Run();
