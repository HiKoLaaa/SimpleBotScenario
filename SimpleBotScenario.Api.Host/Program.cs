using Microsoft.AspNetCore.HttpLogging;
using SimpleBotScenario.Api.Host;
using SimpleBotScenario.Api.Rest;
using SimpleBotScenario.Application;
using SimpleBotScenario.Infrastructure;
using SimpleBotScenario.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddHttpLogging(logging => { logging.LoggingFields = HttpLoggingFields.All; });

var app = builder.Build();

app.UseHttpLogging();
app.MapRest();

app.Run();
