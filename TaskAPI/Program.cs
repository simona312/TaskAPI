using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Middleware;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ако користиш AutoMapper профили:
builder.Services.AddAutoMapper(typeof(Program));

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();  // после Swagger, пред MapControllers
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
