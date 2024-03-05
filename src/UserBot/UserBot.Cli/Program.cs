using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddUserBot(_ => {});

var app = builder.Build();
await app.StartAsync();
var bot = app.
