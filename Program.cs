using FinanceTracker.API.App.Configurations;
using FinanceTracker.API.App.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();


builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddApplicationIdentity();


builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
