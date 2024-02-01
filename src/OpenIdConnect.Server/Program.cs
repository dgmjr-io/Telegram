using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Dgmjr.Configuration.Extensions;
using Telegram.OpenIdConnect.Json;
using Telegram.OpenIdConnect.Middleware;
using Telegram.OpenIdConnect.Services;
using Telegram.OpenIdConnect.Services.CodeService;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

var builder = WebApplication.CreateBuilder(args);
builder.AutoConfigure<Program>();
builder.Services.AddTransient(
    provider => provider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session
);
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.AutoConfigure();
app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials());
app.UseMiddleware<ClientSessionMiddleware>();
app.Run();
