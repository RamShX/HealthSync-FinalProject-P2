
using HealtSync.Persistence.Context;
using HealtSync.Persistence.Interfaces.Users;
using HealtSync.Persistence.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using HealtSync.IOC.Dependencies.Users;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HealtSyncContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HealtSyncDB")));


//Registro de cada una de las dependencias de repositorios de Users

builder.Services.AddUserDependency();

//

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
