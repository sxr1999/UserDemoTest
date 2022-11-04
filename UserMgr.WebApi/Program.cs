using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserMgr.infrastracture;
using UserMgr.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x =>
{
    x.Filters.Add<UnitOfWorkFilter>();
});

string mySqlConnectionStr = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));
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