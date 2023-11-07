using AppTurismoAPI.DbContexts;
using AppTurismoAPI.Models;
using AppTurismoAPI.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/appturismoinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

#if DEBUG
builder.Services.AddTransient<IEmailService, EmailService>();
#else
builder.Services.AddTransient<IEmailService, CloudMailService>();
#endif

builder.Services.AddSingleton<CidadesDados>();
builder.Services.AddDbContext<AppTurismoContext>();
builder.Services.AddScoped<IAppTurismoRepository, AppTurismoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Injeção do Auto Mapper

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
