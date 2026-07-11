using MarketplaceApi.src.Application.Extensions;
using MarketplaceApi.src.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();
builder.AddApplication();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

