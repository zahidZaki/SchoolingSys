using Microsoft.EntityFrameworkCore;
using Serilog;
using StudentInfo.DLL.Db_Context;
using StudentInfo.BLL.RegisterServices;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager _configuration = builder.Configuration;
// Add services to the container.

builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console()
.ReadFrom.Configuration(ctx.Configuration));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterDataServices();
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
