using Microsoft.AspNetCore.Builder;

using Telegram.OpenIdConnect.Json;
using Telegram.OpenIdConnect.Services;
using Telegram.OpenIdConnect.Services.CodeService;

IHostApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AutoConfigure<Program>();

var app = (builder as WebApplicationBuilder).Build();
app.AutoConfigure();
app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials());
app.Run();
