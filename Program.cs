using FinanceTracker.API.DataBase.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")!
    )
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
